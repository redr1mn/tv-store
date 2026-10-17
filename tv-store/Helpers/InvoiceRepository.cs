using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using static tv_store.Helpers.DatabaseHelper;

namespace tv_store.Helpers
{
    // Lưu và hủy hóa đơn trong giao dịch; trigger chịu trách nhiệm cập nhật kho.
    public static class InvoiceRepository
    {
        private static void Run(SqlConnection connection, SqlTransaction transaction, string sql, params SqlParameter[] parameters)
        {
            using (var command = new SqlCommand(sql, connection, transaction))
            {
                command.Parameters.AddRange(parameters);
                command.ExecuteNonQuery();
            }
        }

        private static string NextCode(bool sale, DateTime date, SqlConnection connection, SqlTransaction transaction)
        {
            string prefix = (sale ? "HĐB_" : "HĐN_") + date.ToString("ddMMyyyy", CultureInfo.InvariantCulture);
            string table = sale ? "tblHoaDonBan" : "tblHoaDonNhap";
            string key = sale ? "SoHDB" : "SoHDN";
            // Giữ khóa theo loại hóa đơn và ngày đến khi giao dịch kết thúc,
            // để hai lượt lưu đồng thời không cùng lấy một số thứ tự.
            if (transaction != null)
                Run(connection, transaction,
                    "DECLARE @r int; EXEC @r=sys.sp_getapplock @Resource=@resource, @LockMode='Exclusive', @LockOwner='Transaction', @LockTimeout=10000; IF @r<0 THROW 50001, N'Không thể cấp mã hóa đơn. Vui lòng thử lại.', 1;",
                    P("@resource", "Invoice:" + prefix));
            using (var command = new SqlCommand("SELECT ISNULL(MAX(TRY_CONVERT(int, SUBSTRING(" + key + ", @start, 20))),0)+1 FROM " + table + " WHERE " + key + " LIKE @prefix", connection, transaction))
            {
                command.Parameters.AddRange(new[] { P("@start", prefix.Length + 1), P("@prefix", prefix + "%") });
                return prefix + Convert.ToInt32(command.ExecuteScalar()).ToString("D4", CultureInfo.InvariantCulture);
            }
        }

        // Mã xem trước chưa được giữ chỗ; lúc lưu mới cấp mã dưới khóa giao dịch.
        public static string PreviewCode(bool sale, DateTime date)
        {
            using (var connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();
                return NextCode(sale, date, connection, null);
            }
        }

        public static decimal LineTotal(int quantity, decimal price, decimal discount)
        {
            if (quantity <= 0 || price < 0 || discount < 0 || discount > 100)
                throw new ArgumentException("Số lượng phải lớn hơn 0, đơn giá không âm và giảm giá từ 0 đến 100%.");
            return Math.Round(quantity * price * (1 - discount / 100m), 2, MidpointRounding.AwayFromZero);
        }

        public static string SaveSale(DateTime date, string employee, DataTable lines,
            string customerName, string address, string phone, decimal tax)
        {
            return SaveNew(true, date, employee, null, lines, customerName, address, phone, tax);
        }

        public static string SavePurchase(DateTime date, string employee, string supplier, DataTable lines)
        {
            return SaveNew(false, date, employee, supplier, lines, null, null, null, 0m);
        }

        private static string SaveNew(bool sale, DateTime date, string employee, string partner,
            DataTable lines, string customerName, string address, string phone, decimal tax)
        {
            if (string.IsNullOrWhiteSpace(employee)) throw new ArgumentException("Vui lòng đăng nhập bằng tài khoản gắn với nhân viên.");
            if (lines.Rows.Count == 0) throw new ArgumentException("Hóa đơn phải có ít nhất một sản phẩm.");
            using (var connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();
                // Khách mới, hóa đơn và chi tiết phải cùng được lưu hoặc cùng hoàn tác.
                using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        string code = NextCode(sale, date, connection, transaction);
                        if (sale)
                        {
                            if (string.IsNullOrWhiteSpace(phone)) throw new ArgumentException("Vui lòng nhập số điện thoại khách hàng.");
                            using (var command = new SqlCommand("SELECT TOP 1 MaKhach FROM tblKhachHang WITH (UPDLOCK,HOLDLOCK) WHERE DienThoai=@phone ORDER BY MaKhach", connection, transaction))
                            {
                                command.Parameters.Add(P("@phone", phone));
                                object existing = command.ExecuteScalar();
                                partner = existing == null ? null : existing.ToString();
                            }
                            if (partner == null)
                            {
                                if (string.IsNullOrWhiteSpace(customerName)) throw new ArgumentException("Vui lòng nhập tên khách hàng mới.");
                                partner = "KH" + Guid.NewGuid().ToString("N").Substring(0, 18);
                                Run(connection, transaction, "INSERT INTO tblKhachHang(MaKhach,TenKhach,DiaChi,DienThoai) VALUES(@id,@name,@address,@phone)",
                                    P("@id", partner), P("@name", customerName), P("@address", address), P("@phone", phone));
                            }
                        }
                        if (string.IsNullOrWhiteSpace(partner)) throw new ArgumentException("Vui lòng chọn nhà cung cấp.");
                        string header = sale
                            ? "INSERT INTO tblHoaDonBan(SoHDB,MaNV,NgayBan,MaKhach,Thue) VALUES(@id,@employee,@date,@partner,@tax)"
                            : "INSERT INTO tblHoaDonNhap(SoHDN,MaNV,NgayNhap,MaNCC) VALUES(@id,@employee,@date,@partner)";
                        Run(connection, transaction, header, P("@id", code), P("@employee", employee), P("@date", date), P("@partner", partner), P("@tax", tax));
                        // Khóa sản phẩm theo cùng thứ tự để giảm xung đột khi bán đồng thời.
                        foreach (DataRow row in lines.Select("", "MaTV ASC"))
                        {
                            int quantity = Convert.ToInt32(row["SoLuong"]);
                            decimal price = Convert.ToDecimal(row["DonGia"]);
                            decimal discount = Convert.ToDecimal(row["GiamGia"]);
                            decimal total = LineTotal(quantity, price, discount);
                            // Chốt đơn giá nhập lúc bán để lần nhập sau không đổi lãi/lỗ của hóa đơn này.
                            string detail = sale
                                ? "INSERT INTO tblChiTietHDB(SoHDB,MaTV,SoLuong,DonGia,GiamGia,ThanhTien,GiaVonChot) SELECT @id,MaTV,@quantity,@price,@discount,@total,DonGiaNhap FROM tblTV WITH (UPDLOCK,HOLDLOCK) WHERE MaTV=@product; IF @@ROWCOUNT=0 THROW 50002,N'Sản phẩm không còn tồn tại.',1;"
                                : "INSERT INTO tblChiTietHDN(SoHDN,MaTV,SoLuong,DonGia,GiamGia,ThanhTien) VALUES(@id,@product,@quantity,@price,@discount,@total)";
                            Run(connection, transaction, detail, P("@id", code), P("@product", row["MaTV"]), P("@quantity", quantity), P("@price", price), P("@discount", discount), P("@total", total));
                        }
                        transaction.Commit();
                        return code;
                    }
                    catch
                    {
                        try { transaction.Rollback(); } catch { }
                        throw;
                    }
                }
            }
        }

        public static DataTable Header(bool sale, string code)
        {
            return Query(sale
                ? "SELECT h.*,k.TenKhach,k.DiaChi,k.DienThoai FROM tblHoaDonBan h JOIN tblKhachHang k ON h.MaKhach=k.MaKhach WHERE SoHDB=@id"
                : "SELECT h.* FROM tblHoaDonNhap h WHERE SoHDN=@id", P("@id", code));
        }

        public static DataTable Lines(bool sale, string code)
        {
            return Query("SELECT c.MaTV,t.TenTV,c.SoLuong,c.DonGia,c.GiamGia,c.ThanhTien FROM " +
                (sale ? "tblChiTietHDB" : "tblChiTietHDN") + " c JOIN tblTV t ON c.MaTV=t.MaTV WHERE " + (sale ? "SoHDB" : "SoHDN") + "=@id ORDER BY c.MaTV", P("@id", code));
        }

        public static void Delete(bool sale, string code, string product = null)
        {
            string key = sale ? "SoHDB" : "SoHDN";
            string detail = sale ? "tblChiTietHDB" : "tblChiTietHDN";
            string header = sale ? "tblHoaDonBan" : "tblHoaDonNhap";
            string sql = "IF NOT EXISTS(SELECT 1 FROM " + header + " WITH (UPDLOCK,HOLDLOCK) WHERE " + key + "=@id) THROW 50003,N'Hóa đơn đã bị xóa. Vui lòng tải lại.',1; " +
                "DELETE FROM " + detail + " WHERE " + key + "=@id";
            sql += product == null ? "; DELETE FROM " + header + " WHERE " + key + "=@id" : " AND MaTV=@product; IF @@ROWCOUNT=0 THROW 50004,N'Sản phẩm đã được trả hoặc xóa. Vui lòng tải lại hóa đơn.',1;";
            using (var connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();
                // Khách mới, hóa đơn và chi tiết phải cùng được lưu hoặc cùng hoàn tác.
                using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        Run(connection, transaction, sql, P("@id", code), P("@product", product));
                        transaction.Commit();
                    }
                    catch
                    {
                        try { transaction.Rollback(); } catch { }
                        throw;
                    }
                }
            }
        }
    }
}
