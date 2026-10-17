using System;
using System.Data;
using System.Windows.Forms;
using tv_store.Helpers;

namespace tv_store.Modules._04_HeThong_BaoCao
{
    public partial class frmDangNhap : Form
    {
        public static string CurrentUser = "";
        public static string CurrentEmployeeId;
        public static string CurrentEmployeeName = "";
        public static string CurrentRole = "";

        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string user = txtTenDangNhap.Text.Trim();
            string pass = txtMatKhau.Text;

            if (string.IsNullOrEmpty(user))
            {
                errProvider.SetError(txtTenDangNhap, "Vui lòng nhập tên đăng nhập!");
                txtTenDangNhap.Focus();
                return;
            }
            errProvider.SetError(txtTenDangNhap, "");

            if (string.IsNullOrEmpty(pass))
            {
                errProvider.SetError(txtMatKhau, "Vui lòng nhập mật khẩu!");
                txtMatKhau.Focus();
                return;
            }
            errProvider.SetError(txtMatKhau, "");

            bool isValid = false;
            try
            {
                var dt = DatabaseHelper.Query(
                    "SELECT nv.MaNV,nv.TenNV,nv.Passw,CASE WHEN EXISTS(SELECT 1 FROM tblTaiKhoan t WHERE t.MaNV=nv.MaNV AND t.Quyen=N'Admin') THEN N'Admin' ELSE N'Nhân viên' END AS Quyen FROM tblNhanVien nv WHERE nv.MaNV=@user",
                    DatabaseHelper.P("@user", user));
                // So sánh tại C# để mật khẩu phân biệt hoa/thường dù CSDL dùng collation không phân biệt.
                if (dt.Rows.Count == 1 && dt.Rows[0]["Passw"] != DBNull.Value &&
                    string.Equals(dt.Rows[0]["Passw"].ToString(), pass, StringComparison.Ordinal))
                {
                    isValid = true;
                    CurrentUser = CurrentEmployeeId = dt.Rows[0]["MaNV"].ToString();
                    CurrentEmployeeName = dt.Rows[0]["TenNV"].ToString();
                    CurrentRole = dt.Rows[0]["Quyen"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể đăng nhập: " + ex.Message +
                    "\nNếu CSDL chưa được nâng cấp, hãy chạy Database_Scripts/04_Upgrade_Requirements.sql.",
                    "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (isValid)
            {
                MessageBox.Show("Đăng nhập thành công!\nChào mừng: " + CurrentUser + " (" + CurrentRole + ")", 
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMatKhau.SelectAll();
                txtMatKhau.Focus();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void pnlTop_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
