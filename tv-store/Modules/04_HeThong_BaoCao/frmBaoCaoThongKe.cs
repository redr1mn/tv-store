using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using tv_store.Helpers;

namespace tv_store.Modules._04_HeThong_BaoCao
{
    public partial class frmBaoCaoThongKe : Form
    {
        public frmBaoCaoThongKe()
        {
            InitializeComponent();
        }

        private void frmBaoCaoThongKe_Load(object sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            if (!tbcBaoCao.TabPages.ContainsKey("tpProfitReport"))
            {
                var page = new TabPage("Lãi lỗ và biểu đồ") { Name = "tpProfitReport" };
                var report = new frmProfitReport { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
                page.Controls.Add(report);
                tbcBaoCao.TabPages.Add(page);
                report.Show();
            }
            DatabaseHelper.FillCombo("SELECT MaKhach, TenKhach FROM tblKhachHang", cboKhachHang, "MaKhach", "TenKhach");
            DatabaseHelper.FillCombo("SELECT MaNCC, TenNCC FROM tblNhaCungCap", cboNCC, "MaNCC", "TenNCC");

            cboQuy.SelectedIndex = 0;
            cboThang.SelectedIndex = DateTime.Now.Month - 1;
            txtNamQuy.Text = DateTime.Now.Year.ToString();
            txtNamThang.Text = DateTime.Now.Year.ToString();
        }

        #region TOP 3 TIVI MUA NHIỀU NHẤT THEO KHÁCH HÀNG
        private void btnXemBaoCao1_Click(object sender, EventArgs e)
        {
            if (cboKhachHang.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhachHang.Focus();
                return;
            }

            DataTable dt = DatabaseHelper.GetDataToTable("EXEC dbo.sp_Top3TiviTheoKhachHang @MaKhach",
                DatabaseHelper.P("@MaKhach", cboKhachHang.SelectedValue));
            dgvBaoCao1.DataSource = dt;

            if (dgvBaoCao1.Columns.Count > 0)
            {
                dgvBaoCao1.Columns["MaTV"].HeaderText = "Mã Tivi";
                dgvBaoCao1.Columns["TenTV"].HeaderText = "Tên Tivi";
                dgvBaoCao1.Columns["TenHangSX"].HeaderText = "Hãng SX";
                dgvBaoCao1.Columns["TenManHinh"].HeaderText = "Màn hình";
                dgvBaoCao1.Columns["TenCo"].HeaderText = "Cỡ màn";
                dgvBaoCao1.Columns["TongSoLuongMua"].HeaderText = "Tổng SL đã mua";
                dgvBaoCao1.Columns["TongTienChi"].HeaderText = "Tổng tiền chi (VNĐ)";

                dgvBaoCao1.Columns["TongTienChi"].DefaultCellStyle.Format = "#,##0";
            }
        }

        private void btnXuatExcel1_Click(object sender, EventArgs e)
        {
            string khName = cboKhachHang.Text.Trim();
            string title = string.IsNullOrEmpty(khName) ? "BÁO CÁO TOP 3 TIVI BÁN CHẠY THEO KHÁCH HÀNG" : "TOP 3 TIVI MUA NHIỀU NHẤT - KHÁCH HÀNG " + khName.ToUpper();
            ExcelExportHelper.ExportDataGridViewToExcel(dgvBaoCao1, title, "BaoCao_Top3Tivi_KhachHang", "Top3Tivi");
        }
        #endregion

        #region HÓA ĐƠN & TỔNG TIỀN NHẬP HÀNG THEO NCC
        private void btnXemBaoCao2_Click(object sender, EventArgs e)
        {
            if (cboNCC.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNCC.Focus();
                return;
            }

            DataTable dt = DatabaseHelper.GetDataToTable("EXEC dbo.sp_BaoCaoNhapHangTheoNCC @MaNCC",
                DatabaseHelper.P("@MaNCC", cboNCC.SelectedValue));
            dgvBaoCao2.DataSource = dt;

            decimal tongTien = 0m;
            if (dgvBaoCao2.Columns.Count > 0)
            {
                dgvBaoCao2.Columns["SoHDN"].HeaderText = "Mã HĐN";
                dgvBaoCao2.Columns["NgayNhap"].HeaderText = "Ngày nhập";
                dgvBaoCao2.Columns["NguoiLap"].HeaderText = "Nhân viên lập";
                dgvBaoCao2.Columns["TenNCC"].HeaderText = "Nhà cung cấp";
                dgvBaoCao2.Columns["TongTien"].HeaderText = "Tổng tiền nhập";

                dgvBaoCao2.Columns["TongTien"].DefaultCellStyle.Format = "#,##0";
            }

            foreach (DataRow r in dt.Rows)
            {
                tongTien += Convert.ToDecimal(r["TongTien"]);
            }

            lblTongTienBaoCao2.Text = string.Format("Tổng tiền nhập từ {0}: {1}", cboNCC.Text, DatabaseHelper.FormatTien(tongTien));
        }

        private void btnXuatExcel2_Click(object sender, EventArgs e)
        {
            string nccName = cboNCC.Text.Trim();
            string title = string.IsNullOrEmpty(nccName) ? "BÁO CÁO NHẬP HÀNG THEO NHÀ CUNG CẤP" : "BÁO CÁO NHẬP HÀNG - NCC " + nccName.ToUpper();
            ExcelExportHelper.ExportDataGridViewToExcel(dgvBaoCao2, title, "BaoCao_NhapHang_NCC", "NhapHangNCC");
        }
        #endregion

        #region DOANH THU & HÓA ĐƠN THEO QUÝ
        private void btnXemBaoCao3_Click(object sender, EventArgs e)
        {
            int quy = cboQuy.SelectedIndex + 1;
            if (!int.TryParse(txtNamQuy.Text.Trim(), out int nam) || nam < 1900 || nam > 2100)
            {
                MessageBox.Show("Vui lòng nhập năm hợp lệ (ví dụ: 2026)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamQuy.Focus();
                return;
            }

            DataTable dt = DatabaseHelper.GetDataToTable("EXEC dbo.sp_BaoCaoHoaDonTheoQuy @Quy,@Nam,@LoaiHoaDon",
                DatabaseHelper.P("@Quy", quy), DatabaseHelper.P("@Nam", nam),
                DatabaseHelper.P("@LoaiHoaDon", rdoBanHang.Checked ? "BAN" : "NHAP"));
            dgvBaoCao3.DataSource = dt;

            decimal tongTien = 0m;
            if (dgvBaoCao3.Columns.Count > 0)
            {
                dgvBaoCao3.Columns["Thue"].Visible = false;
                dgvBaoCao3.Columns["MaHoaDon"].HeaderText = "Mã Hóa đơn";
                dgvBaoCao3.Columns["NgayLap"].HeaderText = "Ngày lập";
                dgvBaoCao3.Columns["TenNV"].HeaderText = "Nhân viên";
                dgvBaoCao3.Columns["DoiTac"].HeaderText = rdoBanHang.Checked ? "Khách hàng" : "Nhà cung cấp";
                dgvBaoCao3.Columns["TongTien"].HeaderText = "Tổng tiền";

                dgvBaoCao3.Columns["TongTien"].DefaultCellStyle.Format = "#,##0";
            }

            foreach (DataRow r in dt.Rows)
            {
                tongTien += Convert.ToDecimal(r["TongTien"]);
            }

            lblTongTienBaoCao3.Text = string.Format("Tổng tiền {0} Quý {1}/{2}: {3}", 
                (rdoBanHang.Checked ? "bán hàng" : "nhập hàng"), quy, nam, DatabaseHelper.FormatTien(tongTien));
        }

        private void btnXuatExcel3_Click(object sender, EventArgs e)
        {
            int quy = cboQuy.SelectedIndex + 1;
            string nam = txtNamQuy.Text.Trim();
            string loai = rdoBanHang.Checked ? "BÁN HÀNG" : "NHẬP HÀNG";
            string title = string.Format("BÁO CÁO DOANH THU {0} - QUÝ {1}/{2}", loai, quy, nam);
            ExcelExportHelper.ExportDataGridViewToExcel(dgvBaoCao3, title, "BaoCao_DoanhThu_Quy", "DoanhThuQuy");
        }
        #endregion

        #region TOP 5 NHÀ CUNG CẤP GIAO NHIỀU HÀNG TRONG THÁNG
        private void btnXemBaoCao4_Click(object sender, EventArgs e)
        {
            int thang = cboThang.SelectedIndex + 1;
            if (!int.TryParse(txtNamThang.Text.Trim(), out int nam) || nam < 1900 || nam > 2100)
            {
                MessageBox.Show("Vui lòng nhập năm hợp lệ (ví dụ: 2026)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamThang.Focus();
                return;
            }

            DataTable dt = DatabaseHelper.GetDataToTable("EXEC dbo.sp_Top5NCCGiaoHangTheoThang @Thang,@Nam",
                DatabaseHelper.P("@Thang", thang), DatabaseHelper.P("@Nam", nam));
            dgvBaoCao4.DataSource = dt;

            if (dgvBaoCao4.Columns.Count > 0)
            {
                dgvBaoCao4.Columns["MaNCC"].HeaderText = "Mã NCC";
                dgvBaoCao4.Columns["TenNCC"].HeaderText = "Tên Nhà cung cấp";
                dgvBaoCao4.Columns["DiaChi"].HeaderText = "Địa chỉ";
                dgvBaoCao4.Columns["DienThoai"].HeaderText = "Điện thoại";
                dgvBaoCao4.Columns["SoDonNhap"].HeaderText = "Số đơn nhập";
                dgvBaoCao4.Columns["TongSoLuongGiao"].HeaderText = "Tổng SL tivi giao";
                dgvBaoCao4.Columns["TongGiaTriNhap"].HeaderText = "Tổng giá trị (VNĐ)";

                dgvBaoCao4.Columns["TongGiaTriNhap"].DefaultCellStyle.Format = "#,##0";
            }
        }

        private void btnXuatExcel4_Click(object sender, EventArgs e)
        {
            int thang = cboThang.SelectedIndex + 1;
            string nam = txtNamThang.Text.Trim();
            string title = string.Format("BÁO CÁO TOP 5 NHÀ CUNG CẤP GIAO NHIỀU HÀNG - THÁNG {0}/{1}", thang, nam);
            ExcelExportHelper.ExportDataGridViewToExcel(dgvBaoCao4, title, "BaoCao_Top5NCC_Thang", "Top5NCC");
        }
        #endregion

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
