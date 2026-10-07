using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace tv_store.Helpers
{
    public static class DatabaseHelper
    {
        public static SqlConnection Con;
        private static string workingConnStr = null;
        private static string[] candidateSources = new string[]
        {
            @".\SQLEXPRESS",
            @".",
            @"(local)\SQLEXPRESS",
            @"(local)",
            @"localhost\SQLEXPRESS",
            @"localhost"
        };

        public static string ConnectionString
        {
            get
            {
                if (!string.IsNullOrEmpty(workingConnStr))
                    return workingConnStr;

                try
                {
                    if (ConfigurationManager.ConnectionStrings["QLBanTivi"] != null)
                    {
                        return ConfigurationManager.ConnectionStrings["QLBanTivi"].ConnectionString;
                    }
                }
                catch { }
                return @"Data Source=.\SQLEXPRESS;Initial Catalog=QLBanTivi;Integrated Security=True";
            }
        }

        public static void Connect()
        {
            if (Con != null && Con.State == ConnectionState.Open)
                return;

            if (Con != null)
            {
                try { Con.Dispose(); } catch { }
                Con = null;
            }

            string initialConn = ConnectionString;
            try
            {
                Con = new SqlConnection(initialConn);
                Con.Open();
                workingConnStr = initialConn;
                return;
            }
            catch
            {
                // Thử lần lượt các Server/Instance phổ biến để tự tương thích mọi máy
                foreach (string src in candidateSources)
                {
                    string tryConn = string.Format("Data Source={0};Initial Catalog=QLBanTivi;Integrated Security=True", src);
                    if (tryConn == initialConn) continue;
                    try
                    {
                        Con = new SqlConnection(tryConn);
                        Con.Open();
                        workingConnStr = tryConn;
                        return;
                    }
                    catch { }
                }
            }

            MessageBox.Show("Không thể kết nối cơ sở dữ liệu QLBanTivi!\n" +
                            "Vui lòng kiểm tra:\n" +
                            "1. Service 'SQL Server (SQLEXPRESS)' hoặc 'SQL Server (MSSQLSERVER)' đã được BẬT (Start).\n" +
                            "2. Đã chạy script tạo CSDL 'QLBanTivi' trong thư mục Database_Scripts.", 
                            "Lỗi kết nối CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Disconnect()
        {
            try
            {
                if (Con != null)
                {
                    if (Con.State == ConnectionState.Open)
                        Con.Close();
                    Con.Dispose();
                    Con = null;
                }
            }
            catch { }
        }

        public static DataTable GetDataToTable(string sql)
        {
            Connect();
            DataTable table = new DataTable();
            if (Con == null || Con.State != ConnectionState.Open) return table;

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, Con))
                using (SqlDataAdapter dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(table);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy vấn dữ liệu: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return table;
        }

        public static bool RunSql(string sql)
        {
            Connect();
            if (Con == null || Con.State != ConnectionState.Open) return false;

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, Con))
                {
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực thi câu lệnh SQL:\n" + ex.Message, "Lỗi thực thi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool RunSqlTransaction(System.Collections.Generic.IEnumerable<string> sqlQueries)
        {
            Connect();
            if (Con == null || Con.State != ConnectionState.Open)
            {
                MessageBox.Show("Không thể kết nối CSDL để thực hiện giao dịch!", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            using (SqlTransaction tran = Con.BeginTransaction())
            {
                try
                {
                    foreach (string sql in sqlQueries)
                    {
                        if (string.IsNullOrWhiteSpace(sql)) continue;
                        using (SqlCommand cmd = new SqlCommand(sql, Con, tran))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    try { tran.Rollback(); } catch { }
                    MessageBox.Show("Lỗi thực thi giao dịch SQL (Đã hủy toàn bộ thay đổi):\n" + ex.Message, 
                        "Lỗi giao dịch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public static bool CheckKey(string sql)
        {
            Connect();
            if (Con == null || Con.State != ConnectionState.Open) return false;

            try
            {
                using (SqlDataAdapter dap = new SqlDataAdapter(sql, Con))
                {
                    DataTable table = new DataTable();
                    dap.Fill(table);
                    return table.Rows.Count > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public static string GetFieldValue(string sql)
        {
            Connect();
            if (Con == null || Con.State != ConnectionState.Open) return "";

            string val = "";
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, Con))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        val = reader[0].ToString();
                    }
                }
            }
            catch { }
            return val;
        }

        public static void FillCombo(string sql, ComboBox cbo, string ma, string ten)
        {
            Connect();
            if (Con == null || Con.State != ConnectionState.Open) return;

            try
            {
                using (SqlDataAdapter dap = new SqlDataAdapter(sql, Con))
                {
                    DataTable table = new DataTable();
                    dap.Fill(table);
                    cbo.DataSource = table;
                    cbo.ValueMember = ma;
                    cbo.DisplayMember = ten;
                    cbo.SelectedIndex = -1;
                }
            }
            catch { }
        }

        public static string FormatTien(double tien)
        {
            return string.Format("{0:N0} VNĐ", tien);
        }

        private static readonly string[] ChuSo = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
        private static readonly string[] DonViTien = { "", "nghìn", "triệu", "tỷ", "nghìn tỷ", "triệu tỷ", "tỷ tỷ" };

        private static string Doc3So(int baso, bool isFirstGroup)
        {
            int tram = baso / 100;
            int chuc = (baso % 100) / 10;
            int donvi = baso % 10;
            string res = "";

            if (tram == 0 && isFirstGroup)
            {
                // Nhóm đầu tiên nhỏ hơn 100 thì không đọc "không trăm"
            }
            else
            {
                res += ChuSo[tram] + " trăm ";
            }

            if (chuc > 1)
            {
                res += ChuSo[chuc] + " mươi ";
                if (donvi == 1) res += "mốt ";
                else if (donvi == 5) res += "lăm ";
                else if (donvi > 0) res += ChuSo[donvi] + " ";
            }
            else if (chuc == 1)
            {
                res += "mười ";
                if (donvi == 1) res += "một ";
                else if (donvi == 5) res += "lăm ";
                else if (donvi > 0) res += ChuSo[donvi] + " ";
            }
            else // chuc == 0
            {
                if (donvi > 0)
                {
                    if (tram > 0 || !isFirstGroup) res += "lẻ ";
                    res += ChuSo[donvi] + " ";
                }
            }
            return res.Trim();
        }

        public static string ChuyenSoSangChu(double number)
        {
            if (number == 0) return "Không đồng chẵn.";
            long n = (long)Math.Round(Math.Abs(number));
            if (n == 0) return "Không đồng chẵn.";

            System.Collections.Generic.List<int> groups = new System.Collections.Generic.List<int>();
            while (n > 0)
            {
                groups.Add((int)(n % 1000));
                n /= 1000;
            }

            string result = "";
            for (int i = groups.Count - 1; i >= 0; i--)
            {
                int g = groups[i];
                if (g == 0) continue;
                bool isFirst = (i == groups.Count - 1);
                string gText = Doc3So(g, isFirst);
                string scale = (i < DonViTien.Length) ? DonViTien[i] : "tỷ";
                result += gText + " " + scale + " ";
            }

            result = result.Trim();
            if (string.IsNullOrEmpty(result)) return "Không đồng chẵn.";
            result = char.ToUpper(result[0]) + result.Substring(1) + " đồng chẵn.";
            if (number < 0) result = "Âm " + result;

            return result;
        }
    }
}
