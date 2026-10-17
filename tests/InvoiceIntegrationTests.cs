using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using tv_store.Helpers;
using tv_store.Modules._02_NhapHang;
using tv_store.Modules._03_BanHang;
using tv_store.Modules._04_HeThong_BaoCao;

internal static class InvoiceIntegrationTests
{
    private static int assertions;
    private static void Check(bool condition, string name)
    {
        if (!condition) throw new Exception("FAILED: " + name);
        assertions++; Console.WriteLine("PASS: " + name);
    }
    private static object Scalar(string sql) { return DatabaseHelper.Query(sql).Rows[0][0]; }
    private static DataTable Lines(string product, int quantity, decimal price, decimal discount = 0)
    {
        var t = new DataTable(); t.Columns.Add("MaTV", typeof(string)); t.Columns.Add("SoLuong", typeof(int));
        t.Columns.Add("DonGia", typeof(decimal)); t.Columns.Add("GiamGia", typeof(decimal));
        t.Rows.Add(product, quantity, price, discount); return t;
    }
    private static string Save(bool sale, DataTable lines, string phone = "0999000001")
    {
        return sale
            ? InvoiceRepository.SaveSale(new DateTime(2026, 10, 17), "TEST_NV", lines, "Khách O'Brien", "Địa chỉ tiếng Việt", phone, 10)
            : InvoiceRepository.SavePurchase(new DateTime(2026, 10, 17), "TEST_NV", "TEST_NCC", lines);
    }
    private static T Control<T>(Form f, string name) where T : System.Windows.Forms.Control { return (T)f.Controls.Find(name, true).Single(); }
    private static void Call(object target, string name, params object[] args)
    {
        target.GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Invoke(target, args);
    }
    private static void Render(Form form, string name, Action afterShow = null)
    {
        form.ShowInTaskbar = false; form.StartPosition = FormStartPosition.Manual;
        form.Location = new Point(-32000, -32000);
        form.Show();
        if (afterShow != null) afterShow();
        Application.DoEvents(); form.PerformLayout();
        using (var image = new Bitmap(form.Width, form.Height))
        {
            form.DrawToBitmap(image, new Rectangle(0, 0, form.Width, form.Height));
            image.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, name + ".png"));
        }
        form.Hide();
    }
    [STAThread]
    private static int Main(string[] args)
    {
        try
        {
            var builder = new SqlConnectionStringBuilder(args[0]);
            if (!builder.InitialCatalog.StartsWith("TVStore_Requirements_Test_", StringComparison.Ordinal)) throw new Exception("Tests require a dedicated test database.");
            var configured = new SqlConnectionStringBuilder(DatabaseHelper.ConnectionString);
            Check(configured.InitialCatalog == builder.InitialCatalog && configured.DataSource == builder.DataSource,
                "test configuration points to the dedicated database");
            Check(Convert.ToString(Scalar("SELECT Passw FROM tblNhanVien WHERE MaNV='NV01'")) == "123456", "upgrade preserves/migrates existing employee credentials");
            Check(InvoiceRepository.Header(true, "HDB001").Rows.Count == 1 && Scalar("SELECT GiaVonChot FROM tblChiTietHDB WHERE SoHDB='HDB001'") == DBNull.Value, "upgrade preserves old invoices and marks unknown historical costs");
            DatabaseHelper.Execute("INSERT INTO tblNhanVien(MaNV,TenNV,Passw) VALUES(N'TEST_NV',N'Nhân viên thử',N'test-pass'); INSERT INTO tblNhaCungCap(MaNCC,TenNCC) VALUES(N'TEST_NCC',N'NCC thử'); INSERT INTO tblTV(MaTV,TenTV,SoLuong,DonGiaNhap,DonGiaBan) VALUES(N'TEST_TV',N'Tivi thử',0,0,0),(N'TEST_TV2',N'Tivi thứ hai',0,0,0)");
            Check(InvoiceRepository.LineTotal(3, 1234.56m, 12.34m) == 3246.65m, "decimal line total rounded to cents");
            try { InvoiceRepository.LineTotal(1, 10, 101); throw new Exception("Discount accepted"); } catch (ArgumentException) { Check(true, "invalid discount rejected"); }
            string receipt = Save(false, Lines("TEST_TV", 10, 100));
            Check(Convert.ToInt32(Scalar("SELECT SoLuong FROM tblTV WHERE MaTV='TEST_TV'")) == 10, "purchase adds stock");
            string sold = Save(true, Lines("TEST_TV", 3, 150));
            Check(sold == "HĐB_171020260001", "invoice code follows required date and sequence");
            Check(Convert.ToInt32(Scalar("SELECT SoLuong FROM tblTV WHERE MaTV='TEST_TV'")) == 7, "sale subtracts stock once");
            Check(Convert.ToDecimal(Scalar("SELECT TongTien FROM tblHoaDonBan WHERE SoHDB=N'" + sold + "'")) == 495, "total includes original VAT");
            Check(Convert.ToDecimal(Scalar("SELECT GiaVonChot FROM tblChiTietHDB WHERE SoHDB=N'" + sold + "'")) == 100, "cost is snapshotted when sold");
            Check(DatabaseHelper.Query("SELECT * FROM tblKhachHang WHERE TenKhach=@name", DatabaseHelper.P("@name", "Khách O'Brien")).Rows.Count == 1, "new customer keeps apostrophes and Vietnamese");
            string repeat = Save(true, Lines("TEST_TV", 1, 150));
            Check(Convert.ToInt32(Scalar("SELECT COUNT(*) FROM tblKhachHang WHERE DienThoai='0999000001'")) == 1, "phone lookup reuses existing customer");
            // Khi dòng thứ hai lỗi, phải hoàn tác cả chi tiết, hóa đơn, khách mới và tồn kho.
            Save(false, Lines("TEST_TV2", 2, 50));
            var invalid = Lines("TEST_TV", 1, 150); invalid.Rows.Add("TEST_TV2", 9999, 100m, 0m);
            int before = Convert.ToInt32(Scalar("SELECT COUNT(*) FROM tblHoaDonBan"));
            int stockBefore = Convert.ToInt32(Scalar("SELECT SoLuong FROM tblTV WHERE MaTV='TEST_TV'"));
            try { Save(true, invalid, "0999999999"); throw new Exception("Oversell accepted"); } catch (SqlException) { }
            Check(Convert.ToInt32(Scalar("SELECT COUNT(*) FROM tblHoaDonBan")) == before && Convert.ToInt32(Scalar("SELECT SoLuong FROM tblTV WHERE MaTV='TEST_TV'")) == stockBefore && Convert.ToInt32(Scalar("SELECT COUNT(*) FROM tblKhachHang WHERE DienThoai='0999999999'")) == 0, "oversell rolls back all lines, invoice and new customer");
            InvoiceRepository.Delete(true, sold, "TEST_TV");
            Check(InvoiceRepository.Lines(true, sold).Rows.Count == 0 && Convert.ToInt32(Scalar("SELECT SoLuong FROM tblTV WHERE MaTV='TEST_TV'")) == 9, "return removes persisted line and restores stock");
            Check(Convert.ToDecimal(Scalar("SELECT TongTien FROM tblHoaDonBan WHERE SoHDB=N'" + sold + "'")) == 0, "return recalculates invoice total");
            InvoiceRepository.Delete(true, repeat);
            Check(InvoiceRepository.Header(true, repeat).Rows.Count == 0 && Convert.ToInt32(Scalar("SELECT SoLuong FROM tblTV WHERE MaTV='TEST_TV'")) == 10, "cancel deletes invoice and restores stock");
            // Kiểm tra cập nhật số lượng nhập theo chênh lệch, tránh trừ tạm gây âm kho.
            string activeSale = Save(true, Lines("TEST_TV", 8, 150));
            DatabaseHelper.Execute("UPDATE tblChiTietHDN SET SoLuong=11,ThanhTien=1100 WHERE SoHDN=@id", DatabaseHelper.P("@id", receipt));
            Check(Convert.ToInt32(Scalar("SELECT SoLuong FROM tblTV WHERE MaTV='TEST_TV'")) == 3, "receipt quantity update uses net stock movement");
            try { InvoiceRepository.Delete(false, receipt); throw new Exception("Invalid receipt cancel accepted"); } catch (SqlException) { }
            Check(InvoiceRepository.Header(false, receipt).Rows.Count == 1 && Convert.ToInt32(Scalar("SELECT SoLuong FROM tblTV WHERE MaTV='TEST_TV'")) == 3, "receipt cancel rejects negative stock atomically");
            string later = Save(false, Lines("TEST_TV", 1, 200));
            Check(Convert.ToDecimal(Scalar("SELECT GiaVonChot FROM tblChiTietHDB WHERE SoHDB=N'" + activeSale + "'")) == 100, "later purchase leaves historical sale cost unchanged");
            var report = frmProfitReport.GetReport(2026, 2026, 0, "TEST_TV");
            Check(report.Rows.Count == 1 && Convert.ToDecimal(report.Rows[0]["DoanhThu"]) == 1200 && Convert.ToDecimal(report.Rows[0]["GiaVon"]) == 800 && Convert.ToDecimal(report.Rows[0]["LaiLo"]) == 400, "monthly profit excludes VAT and uses sale cost snapshot");
            Check(frmProfitReport.GetReport(2026, 2026, 1, "TEST_TV").Rows.Count == 1 && frmProfitReport.GetReport(2026, 2026, 2, "TEST_TV").Rows.Count == 1, "quarterly and yearly report queries work");
            DatabaseHelper.Execute("UPDATE tblChiTietHDB SET GiaVonChot=NULL WHERE SoHDB=@id", DatabaseHelper.P("@id", activeSale));
            report = frmProfitReport.GetReport(2026, 2026, 0, "TEST_TV");
            Check(report.Rows[0]["LaiLo"] == DBNull.Value && Convert.ToInt32(report.Rows[0]["DongThieuGiaVon"]) == 1, "legacy unknown cost is flagged instead of fabricated");
            // Kiểm tra dữ liệu trên form mà không thao tác các hộp thoại xác nhận.
            frmDangNhap.CurrentEmployeeId = "TEST_NV"; frmDangNhap.CurrentEmployeeName = "Nhân viên thử";
            using (var form = new frmHoaDonBan())
            {
                Call(form, "InitializeRuntime");
                Check(!Control<ComboBox>(form, "cboNhanVien").Enabled && Control<ComboBox>(form, "cboNhanVien").SelectedValue.ToString() == "TEST_NV", "sales employee is fixed to logged-in employee");
                Check(!Control<TextBox>(form, "txtDienThoaiKH").ReadOnly && Control<ComboBox>(form, "txtMaHDB").Items.Count > 0, "phone is editable and invoice codes are searchable");
                Control<TextBox>(form, "txtDienThoaiKH").Text = "0999000001"; Call(form, "LookupCustomer");
                Check(Control<TextBox>(form, "txtTenKH").Text == "Khách O'Brien", "phone lookup fills customer details on form");
                Control<ComboBox>(form, "cboMaTivi").SelectedValue = "TEST_TV";
                Control<TextBox>(form, "txtSoLuongBan").Text = "2";
                Control<TextBox>(form, "txtGiamGia").Text = "10";
                Check(Convert.ToDecimal(Control<TextBox>(form, "txtThanhTien").Text) == 396m,
                    "draft sale fills product price and recalculates discounted amount");
                Call(form, "AddLine");
                Check(Control<DataGridView>(form, "dgvChiTietHDB").Rows.Count == 1,
                    "draft sale adds a product using direct form controls");
                form.OpenInvoice(activeSale);
                var grid = Control<DataGridView>(form, "dgvChiTietHDB");
                Check(grid.Columns.Contains("MaTV") && grid.Columns.Contains("DonGia") && grid.Columns.Contains("ThanhTien") && grid.Rows.Count == 1, "reopened sale has columns expected by Excel export");
                Check(!Control<Button>(form, "btnLuuHDB").Enabled && Control<Button>(form, "btnInHDB").Enabled, "saved invoice cannot be inserted twice and can be printed");
                Render(form, "sales-invoice-preview", () => form.OpenInvoice(activeSale));
            }
            using (var form = new frmHoaDonNhap())
            {
                Call(form, "InitializeRuntime");
                Control<ComboBox>(form, "cboMaTivi").SelectedValue = "TEST_TV";
                Control<TextBox>(form, "txtSoLuong").Text = "2";
                Check(Convert.ToDecimal(Control<TextBox>(form, "txtThanhTien").Text) == 400m,
                    "draft purchase fills product price and recalculates amount");
                Call(form, "AddLine");
                Check(Control<DataGridView>(form, "dgvChiTietHDN").Rows.Count == 1,
                    "draft purchase adds a product using direct form controls");
                form.OpenInvoice(receipt);
                Check(Control<DataGridView>(form, "dgvChiTietHDN").Columns.Contains("DonGia") && !Control<ComboBox>(form, "cboNhanVien").Enabled, "purchase invoice reopening and export bindings work");
                Render(form, "purchase-invoice-preview", () => form.OpenInvoice(receipt));
            }
            var multipleLines = Lines("TEST_TV", 1, 150); multipleLines.Rows.Add("TEST_TV2", 1, 100m, 0m);
            string sortableInvoice = Save(true, multipleLines);
            using (var form = new frmHoaDonBan())
            {
                Call(form, "InitializeRuntime"); form.OpenInvoice(sortableInvoice);
                var grid = Control<DataGridView>(form, "dgvChiTietHDB");
                grid.Sort(grid.Columns["MaTV"], System.ComponentModel.ListSortDirection.Descending);
                var selected = (DataRow)form.GetType().GetMethod("GetLineAt", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(form, new object[] { 0 });
                Check(selected["MaTV"].ToString() == "TEST_TV2", "return targets clicked product even after grid sorting");
            }
            InvoiceRepository.Delete(true, sortableInvoice, "TEST_TV2");
            Check(Convert.ToDecimal(InvoiceRepository.Header(true, sortableInvoice).Rows[0]["TongTien"]) == 165, "return recalculates total for remaining product");
            InvoiceRepository.Delete(true, sortableInvoice);
            using (var form = new frmProfitReport())
            {
                // Gọi sự kiện nạp dữ liệu trước khi kết xuất ảnh form.
                Call(form, "OnLoad", EventArgs.Empty);
                Render(form, "profit-report-preview");
            }
            // Kiểm tra các báo cáo gọi thủ tục với đúng tham số từ form.
            using (var form = new frmBaoCaoThongKe())
            {
                Call(form, "OnLoad", EventArgs.Empty);
                Control<ComboBox>(form, "cboKhachHang").SelectedValue = Convert.ToString(Scalar("SELECT MaKhach FROM tblKhachHang WHERE DienThoai='0999000001'"));
                Call(form, "btnXemBaoCao1_Click", form, EventArgs.Empty);
                var topProducts = (DataTable)Control<DataGridView>(form, "dgvBaoCao1").DataSource;
                Check(topProducts.Rows.Count == 1 && Convert.ToInt32(topProducts.Rows[0]["TongSoLuongMua"]) == 8,
                    "customer report calls the stored procedure with selected customer");
                Control<ComboBox>(form, "cboNCC").SelectedValue = "TEST_NCC";
                Call(form, "btnXemBaoCao2_Click", form, EventArgs.Empty);
                var receipts = (DataTable)Control<DataGridView>(form, "dgvBaoCao2").DataSource;
                Check(receipts.Rows.Count == 3 && receipts.Rows.Cast<DataRow>().All(r => r["TenNCC"].ToString() == "NCC thử"),
                    "supplier report only contains selected supplier receipts");
                Control<ComboBox>(form, "cboQuy").SelectedIndex = 3;
                Control<TextBox>(form, "txtNamQuy").Text = "2026";
                Control<RadioButton>(form, "rdoBanHang").Checked = true;
                Call(form, "btnXemBaoCao3_Click", form, EventArgs.Empty);
                var sales = (DataTable)Control<DataGridView>(form, "dgvBaoCao3").DataSource;
                Check(sales.Rows.Cast<DataRow>().Any(r => r["MaHoaDon"].ToString() == activeSale),
                    "quarterly sales report uses correct year and invoice type");
                Control<RadioButton>(form, "rdoBanHang").Checked = false;
                Call(form, "btnXemBaoCao3_Click", form, EventArgs.Empty);
                receipts = (DataTable)Control<DataGridView>(form, "dgvBaoCao3").DataSource;
                Check(receipts.Rows.Cast<DataRow>().Any(r => r["MaHoaDon"].ToString() == receipt)
                    && !receipts.Rows.Cast<DataRow>().Any(r => r["MaHoaDon"].ToString() == activeSale),
                    "quarterly purchase report switches invoice type");
                Control<ComboBox>(form, "cboThang").SelectedIndex = 11;
                Control<TextBox>(form, "txtNamThang").Text = "2030";
                Call(form, "btnXemBaoCao4_Click", form, EventArgs.Empty);
                Check(Control<DataGridView>(form, "dgvBaoCao4").Rows.Count == 0,
                    "top suppliers report respects selected month and year");
            }
            // Kiểm tra cấp mã và cập nhật tồn kho khi hai giao dịch bán chạy đồng thời.
            string first = null, second = null;
            Task.WaitAll(Task.Run(() => first = Save(true, Lines("TEST_TV2", 1, 100))), Task.Run(() => second = Save(true, Lines("TEST_TV2", 1, 100))));
            Check(first != second && Convert.ToInt32(Scalar("SELECT SoLuong FROM tblTV WHERE MaTV='TEST_TV2'")) == 0, "concurrent sales receive unique codes without overselling");
            Console.WriteLine("SUCCESS: " + assertions + " checks");
            return 0;
        }
        catch (Exception ex) { Console.Error.WriteLine(ex); return 1; }
    }
}
