using System;
using System.Data;
using System.Windows.Forms;
using tv_store.Helpers;

namespace tv_store.Modules._04_HeThong_BaoCao
{
    public partial class frmDangNhap : Form
    {
        public static string CurrentUser = "admin";
        public static string CurrentRole = "Admin";

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
            string pass = txtMatKhau.Text.Trim();

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

            // Kiểm tra trong CSDL SQL Server hoặc fallback mặc định
            bool isValid = false;
            string sql = string.Format("SELECT TenDangNhap, Quyen FROM tblTaiKhoan WHERE TenDangNhap='{0}' AND MatKhau='{1}'", user, pass);
            DataTable dt = DatabaseHelper.GetDataToTable(sql);

            if (dt.Rows.Count > 0)
            {
                isValid = true;
                CurrentUser = dt.Rows[0]["TenDangNhap"].ToString();
                CurrentRole = dt.Rows[0]["Quyen"].ToString();
            }
            else if (user == "admin" && pass == "123456")
            {
                // Fallback nếu CSDL chưa có dữ liệu tài khoản
                isValid = true;
                CurrentUser = "admin";
                CurrentRole = "Admin";
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
