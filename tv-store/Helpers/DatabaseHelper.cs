using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace tv_store.Helpers
{
    public static class DatabaseHelper
    {
        public static string ConnectionString
        {
            get
            {
                var setting = ConfigurationManager.ConnectionStrings["QLBanTivi"];
                if (setting == null || string.IsNullOrWhiteSpace(setting.ConnectionString))
                    throw new ConfigurationErrorsException("Chưa cấu hình kết nối QLBanTivi trong App.config.");
                return setting.ConnectionString;
            }
        }

        public static SqlParameter P(string name, object value)
        {
            return new SqlParameter(name, value ?? DBNull.Value);
        }

        // Mỗi lần truy vấn tự mở và giải phóng kết nối theo cấu hình.
        public static DataTable Query(string sql, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(sql, connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddRange(parameters);
                var result = new DataTable();
                connection.Open();
                adapter.Fill(result);
                return result;
            }
        }

        public static void Execute(string sql, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddRange(parameters);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Các form danh mục dùng các hàm này để hiện lỗi và giữ nguyên dữ liệu nhập.
        public static DataTable GetDataToTable(string sql, params SqlParameter[] parameters)
        {
            try { return Query(sql, parameters); }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy vấn dữ liệu: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }

        public static bool RunSql(string sql, params SqlParameter[] parameters)
        {
            try
            {
                Execute(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực thi SQL: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool CheckKey(string sql, params SqlParameter[] parameters)
        {
            return GetDataToTable(sql, parameters).Rows.Count > 0;
        }

        public static string GetFieldValue(string sql, params SqlParameter[] parameters)
        {
            var data = GetDataToTable(sql, parameters);
            return data.Rows.Count == 0 ? "" : Convert.ToString(data.Rows[0][0]);
        }

        public static void FillCombo(string sql, ComboBox cbo, string ma, string ten)
        {
            var data = GetDataToTable(sql);
            if (!data.Columns.Contains(ma) || !data.Columns.Contains(ten)) return;
            cbo.ValueMember = ma;
            cbo.DisplayMember = ten;
            cbo.DataSource = data;
            cbo.SelectedIndex = -1;
        }

        public static string FormatTien(double tien)
        {
            return string.Format("{0:N0} VNĐ", tien);
        }

        public static string FormatTien(decimal tien)
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
            else // Hàng chục bằng 0
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

        public static string ChuyenSoSangChu(decimal number)
        {
            return ChuyenSoSangChu((double)number);
        }
    }
}
