using System;
using System.Data;
using System.Windows.Forms;
using tv_store.Helpers;
using tv_store.Modules._01_SanPham;
using tv_store.Modules._02_NhapHang;
using tv_store.Modules._03_BanHang;

namespace tv_store.Modules._04_HeThong_BaoCao
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            UpdateStatusBar();
            LoadDashboardStats();
        }

        private void UpdateStatusBar()
        {
            lblNguoiDung.Text = "👤 Người dùng: " + frmDangNhap.CurrentUser + " (" + frmDangNhap.CurrentRole + ")";
            lblDongHo.Text = "⏰ " + DateTime.Now.ToString("HH:mm:ss - dd/MM/yyyy");

            // Phân quyền cơ bản: Chỉ người dùng có quyền Admin mới được quản lý Nhân viên
            bool isAdmin = string.Equals(frmDangNhap.CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase);
            btnNavNhanVien.Enabled = isAdmin;
            mnuDMNhanVien.Enabled = isAdmin;
        }

        private void LoadDashboardStats()
        {
            try
            {
                // 1. Số mẫu Tivi đang kinh doanh
                string c1 = DatabaseHelper.GetFieldValue("SELECT COUNT(*) FROM tblTV");
                lblCard1Val.Text = string.IsNullOrEmpty(c1) ? "0" : c1;

                // 2. Tổng số lượng tồn kho
                string c2 = DatabaseHelper.GetFieldValue("SELECT ISNULL(SUM(SoLuong), 0) FROM tblTV");
                lblCard2Val.Text = string.IsNullOrEmpty(c2) ? "0" : c2;

                // 3. Tổng số hóa đơn
                string hdb = DatabaseHelper.GetFieldValue("SELECT COUNT(*) FROM tblHoaDonBan");
                string hdn = DatabaseHelper.GetFieldValue("SELECT COUNT(*) FROM tblHoaDonNhap");
                int totalHd = (int.TryParse(hdb, out int b) ? b : 0) + (int.TryParse(hdn, out int n) ? n : 0);
                lblCard3Val.Text = totalHd.ToString();

                // 4. Số mẫu Tivi sắp hết hàng (<= 3)
                string c4 = DatabaseHelper.GetFieldValue("SELECT COUNT(*) FROM tblTV WHERE SoLuong <= 3");
                lblCard4Val.Text = string.IsNullOrEmpty(c4) ? "0" : c4;
            }
            catch { }
        }

        private void timerDongHo_Tick(object sender, EventArgs e)
        {
            lblDongHo.Text = "⏰ " + DateTime.Now.ToString("HH:mm:ss - dd/MM/yyyy");
        }

        #region ĐIỀU HƯỚNG CÁC PHÂN HỆ
        private void btnNavSanPham_Click(object sender, EventArgs e)
        {
            frmDMTivi frm = new frmDMTivi();
            frm.ShowDialog();
            LoadDashboardStats();
        }

        private void btnNavDanhMucPhu_Click(object sender, EventArgs e)
        {
            frmDanhMucPhu frm = new frmDanhMucPhu();
            frm.ShowDialog();
        }

        private void btnNavBanHang_Click(object sender, EventArgs e)
        {
            frmHoaDonBan frm = new frmHoaDonBan();
            frm.ShowDialog();
            LoadDashboardStats();
        }

        private void btnNavNhapHang_Click(object sender, EventArgs e)
        {
            frmHoaDonNhap frm = new frmHoaDonNhap();
            frm.ShowDialog();
            LoadDashboardStats();
        }

        private void btnNavKhachHang_Click(object sender, EventArgs e)
        {
            frmKhachHang frm = new frmKhachHang();
            frm.ShowDialog();
        }

        private void btnNavNCC_Click(object sender, EventArgs e)
        {
            frmNhaCungCap frm = new frmNhaCungCap();
            frm.ShowDialog();
        }

        private void btnNavNhanVien_Click(object sender, EventArgs e)
        {
            frmNhanVien frm = new frmNhanVien();
            frm.ShowDialog();
        }

        private void btnNavBaoCao_Click(object sender, EventArgs e)
        {
            frmBaoCaoThongKe frm = new frmBaoCaoThongKe();
            frm.ShowDialog();
        }

        private void mnuTimKiemTivi_Click(object sender, EventArgs e)
        {
            frmTimKiemTivi frm = new frmTimKiemTivi();
            frm.ShowDialog();
        }

        private void mnuTimKiemHDN_Click(object sender, EventArgs e)
        {
            frmTimKiemHDN frm = new frmTimKiemHDN();
            frm.ShowDialog();
        }
        #endregion

        #region HỆ THỐNG & TRỢ GIÚP
        private void mnuDangNhap_Click(object sender, EventArgs e)
        {
            frmDangNhap frm = new frmDangNhap();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                UpdateStatusBar();
                LoadDashboardStats();
            }
        }

        private void mnuDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất khỏi tài khoản hiện tại?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                frmDangNhap.CurrentUser = "Khách";
                frmDangNhap.CurrentRole = "Chưa đăng nhập";
                UpdateStatusBar();

                frmDangNhap frm = new frmDangNhap();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    UpdateStatusBar();
                    LoadDashboardStats();
                }
                else
                {
                    // Nếu người dùng hủy form đăng nhập sau khi đã chọn Đăng xuất, đóng màn hình chính
                    this.Close();
                }
            }
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát khỏi chương trình?", "Xác nhận thoát", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void mnuThongTin_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "ĐỒ ÁN LẬP TRÌNH TRỰC QUAN (LTTQ) - C# WINFORMS\n" +
                "Đề tài: Hệ thống Quản lý Bán Tivi (TV Store)\n" +
                "Phiên bản: 2.0 Extended\n" +
                "Nhóm thực hiện: 4 Thành viên\n" +
                "Đầy đủ chức năng CRUD, Phím tắt Alt (&), Trigger CSDL, 4 Báo cáo YC 6-9, Xuất Excel CSV.",
                "Thông tin phần mềm", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
    }
}
