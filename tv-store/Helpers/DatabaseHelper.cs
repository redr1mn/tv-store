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
                if (Con != null && Con.State == ConnectionState.Open)
                {
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

        public static bool CheckKey(string sql)
        {
            Connect();
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

        public static string ChuyenSoSangChu(double number)
        {
            string[] unitNumbers = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] placeValues = { "", "nghìn", "triệu", "tỷ" };
            bool isNegative = false;

            if (number < 0)
            {
                number = -number;
                isNegative = true;
            }

            if (number == 0) return "Không đồng";

            string sNumber = number.ToString("#");
            int positionDigit = sNumber.Length;
            int totalTens = 0;
            string result = "";

            int[] tens = new int[4];
            int groupCount = (int)Math.Ceiling(sNumber.Length / 3.0);

            for (int i = groupCount - 1; i >= 0; i--)
            {
                int len = positionDigit >= 3 ? 3 : positionDigit;
                string sub = sNumber.Substring(positionDigit - len, len);
                positionDigit -= len;

                int num = int.Parse(sub);
                tens[0] = num / 100;
                tens[1] = (num % 100) / 10;
                tens[2] = num % 10;

                string groupResult = "";
                if (tens[0] > 0 || (result != "" && (tens[1] > 0 || tens[2] > 0)))
                {
                    groupResult += unitNumbers[tens[0]] + " trăm ";
                }

                if (tens[1] > 1)
                {
                    groupResult += unitNumbers[tens[1]] + " mươi ";
                    if (tens[2] == 1) groupResult += "mốt ";
                    else if (tens[2] == 5) groupResult += "lăm ";
                    else if (tens[2] > 0) groupResult += unitNumbers[tens[2]] + " ";
                }
                else if (tens[1] == 1)
                {
                    groupResult += "mười ";
                    if (tens[2] == 5) groupResult += "lăm ";
                    else if (tens[2] > 0) groupResult += unitNumbers[tens[2]] + " ";
                }
                else if (tens[2] > 0)
                {
                    if (tens[0] > 0 || result != "") groupResult += "lẻ ";
                    groupResult += unitNumbers[tens[2]] + " ";
                }

                if (groupResult != "")
                {
                    groupResult += placeValues[totalTens] + " ";
                }

                result = groupResult + result;
                totalTens++;
            }

            result = result.Trim();
            if (result.Length > 0)
            {
                result = char.ToUpper(result[0]) + result.Substring(1) + " đồng chẵn.";
            }
            if (isNegative) result = "Âm " + result;

            return result;
        }
    }
}
