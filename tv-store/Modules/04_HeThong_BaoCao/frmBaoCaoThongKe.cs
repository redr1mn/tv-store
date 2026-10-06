using System;
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
            DatabaseHelper.FillCombo("SELECT MaKhach, TenKhach FROM tblKhachHang", cboKhachHang, "MaKhach", "TenKhach");
            DatabaseHelper.FillCombo("SELECT MaNCC, TenNCC FROM tblNhaCungCap", cboNCC, "MaNCC", "TenNCC");

            cboQuy.SelectedIndex = 0;
            cboThang.SelectedIndex = DateTime.Now.Month - 1;
            txtNamQuy.Text = DateTime.Now.Year.ToString();
            txtNamThang.Text = DateTime.Now.Year.ToString();
        }

        #region BÁO CÁO 1 (YÊU CẦU 6): TOP 3 TIVI MUA NHIỀU NHẤT THEO KHÁCH HÀNG
        private void btnXemBaoCao1_Click(object sender, EventArgs e)
        {
            if (cboKhachHang.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhachHang.Focus();
                return;
            }

            string maKH = cboKhachHang.SelectedValue.ToString();
            string sql = string.Format(
                "SELECT TOP 3 tv.MaTV, tv.TenTV, hsx.TenHangSX, mh.TenManHinh, co.TenCo, " +
                "SUM(ct.SoLuong) AS TongSoLuongMua, SUM(ct.ThanhTien) AS TongTienChi " +
                "FROM tblChiTietHDB ct " +
                "INNER JOIN tblHoaDonBan hdb ON ct.SoHDB = hdb.SoHDB " +
                "INNER JOIN tblTV tv ON ct.MaTV = tv.MaTV " +
                "LEFT JOIN tblHangSX hsx ON tv.MaHangSX = hsx.MaHangSX " +
                "LEFT JOIN tblManHinh mh ON tv.MaManHinh = mh.MaManHinh " +
                "LEFT JOIN tblCoManHinh co ON tv.MaCo = co.MaCo " +
                "WHERE hdb.MaKhach = '{0}' " +
                "GROUP BY tv.MaTV, tv.TenTV, hsx.TenHangSX, mh.TenManHinh, co.TenCo " +
                "ORDER BY TongSoLuongMua DESC", maKH);

            DataTable dt = DatabaseHelper.GetDataToTable(sql);
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
            ExcelExportHelper.ExportToCSV(dgvBaoCao1, "BaoCao_Top3Tivi_KhachHang");
        }
        #endregion

        #region BÁO CÁO 2 (YÊU CẦU 7): HÓA ĐƠN & TỔNG TIỀN NHẬP HÀNG THEO NCC
        private void btnXemBaoCao2_Click(object sender, EventArgs e)
        {
            if (cboNCC.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNCC.Focus();
                return;
            }

            string maNCC = cboNCC.SelectedValue.ToString();
            string sql = string.Format(
                "SELECT hdn.SoHDN, hdn.NgayNhap, nv.TenNV AS NguoiLap, ncc.TenNCC, hdn.TongTien " +
                "FROM tblHoaDonNhap hdn " +
                "INNER JOIN tblNhaCungCap ncc ON hdn.MaNCC = ncc.MaNCC " +
                "INNER JOIN tblNhanVien nv ON hdn.MaNV = nv.MaNV " +
                "WHERE hdn.MaNCC = '{0}' " +
                "ORDER BY hdn.NgayNhap DESC", maNCC);

            DataTable dt = DatabaseHelper.GetDataToTable(sql);
            dgvBaoCao2.DataSource = dt;

            double tongTien = 0;
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
                tongTien += Convert.ToDouble(r["TongTien"]);
            }

            lblTongTienBaoCao2.Text = string.Format("Tổng tiền nhập từ {0}: {1}", cboNCC.Text, DatabaseHelper.FormatTien(tongTien));
        }

        private void btnXuatExcel2_Click(object sender, EventArgs e)
        {
            ExcelExportHelper.ExportToCSV(dgvBaoCao2, "BaoCao_NhapHang_NCC");
        }
        #endregion

        #region BÁO CÁO 3 (YÊU CẦU 8): DOANH THU & HÓA ĐƠN THEO QUÝ
        private void btnXemBaoCao3_Click(object sender, EventArgs e)
        {
            int quy = cboQuy.SelectedIndex + 1;
            int.TryParse(txtNamQuy.Text.Trim(), out int nam);
            if (nam <= 2000) nam = DateTime.Now.Year;

            string sql = "";
            if (rdoBanHang.Checked)
            {
                sql = string.Format(
                    "SELECT hdb.SoHDB AS MaHoaDon, hdb.NgayBan AS NgayLap, nv.TenNV, kh.TenKhach AS DoiTac, hdb.TongTien " +
                    "FROM tblHoaDonBan hdb " +
                    "INNER JOIN tblNhanVien nv ON hdb.MaNV = nv.MaNV " +
                    "INNER JOIN tblKhachHang kh ON hdb.MaKhach = kh.MaKhach " +
                    "WHERE DATEPART(QUARTER, hdb.NgayBan) = {0} AND YEAR(hdb.NgayBan) = {1} " +
                    "ORDER BY hdb.NgayBan DESC", quy, nam);
            }
            else
            {
                sql = string.Format(
                    "SELECT hdn.SoHDN AS MaHoaDon, hdn.NgayNhap AS NgayLap, nv.TenNV, ncc.TenNCC AS DoiTac, hdn.TongTien " +
                    "FROM tblHoaDonNhap hdn " +
                    "INNER JOIN tblNhanVien nv ON hdn.MaNV = nv.MaNV " +
                    "INNER JOIN tblNhaCungCap ncc ON hdn.MaNCC = ncc.MaNCC " +
                    "WHERE DATEPART(QUARTER, hdn.NgayNhap) = {0} AND YEAR(hdn.NgayNhap) = {1} " +
                    "ORDER BY hdn.NgayNhap DESC", quy, nam);
            }

            DataTable dt = DatabaseHelper.GetDataToTable(sql);
            dgvBaoCao3.DataSource = dt;

            double tongTien = 0;
            if (dgvBaoCao3.Columns.Count > 0)
            {
                dgvBaoCao3.Columns["MaHoaDon"].HeaderText = "Mã Hóa đơn";
                dgvBaoCao3.Columns["NgayLap"].HeaderText = "Ngày lập";
                dgvBaoCao3.Columns["TenNV"].HeaderText = "Nhân viên";
                dgvBaoCao3.Columns["DoiTac"].HeaderText = rdoBanHang.Checked ? "Khách hàng" : "Nhà cung cấp";
                dgvBaoCao3.Columns["TongTien"].HeaderText = "Tổng tiền";

                dgvBaoCao3.Columns["TongTien"].DefaultCellStyle.Format = "#,##0";
            }

            foreach (DataRow r in dt.Rows)
            {
                tongTien += Convert.ToDouble(r["TongTien"]);
            }

            lblTongTienBaoCao3.Text = string.Format("Tổng tiền {0} Quý {1}/{2}: {3}", 
                (rdoBanHang.Checked ? "bán hàng" : "nhập hàng"), quy, nam, DatabaseHelper.FormatTien(tongTien));
        }

        private void btnXuatExcel3_Click(object sender, EventArgs e)
        {
            ExcelExportHelper.ExportToCSV(dgvBaoCao3, "BaoCao_DoanhThu_Quy");
        }
        #endregion

        #region BÁO CÁO 4 (YÊU CẦU 9): TOP 5 NHÀ CUNG CẤP GIAO NHIỀU HÀNG TRONG THÁNG
        private void btnXemBaoCao4_Click(object sender, EventArgs e)
        {
            int thang = cboThang.SelectedIndex + 1;
            int.TryParse(txtNamThang.Text.Trim(), out int nam);
            if (nam <= 2000) nam = DateTime.Now.Year;

            string sql = string.Format(
                "SELECT TOP 5 ncc.MaNCC, ncc.TenNCC, ncc.DiaChi, ncc.DienThoai, " +
                "COUNT(DISTINCT hdn.SoHDN) AS SoDonNhap, " +
                "SUM(ct.SoLuong) AS TongSoLuongGiao, " +
                "SUM(ct.ThanhTien) AS TongGiaTriNhap " +
                "FROM tblHoaDonNhap hdn " +
                "INNER JOIN tblNhaCungCap ncc ON hdn.MaNCC = ncc.MaNCC " +
                "INNER JOIN tblChiTietHDN ct ON hdn.SoHDN = ct.SoHDN " +
                "WHERE MONTH(hdn.NgayNhap) = {0} AND YEAR(hdn.NgayNhap) = {1} " +
                "GROUP BY ncc.MaNCC, ncc.TenNCC, ncc.DiaChi, ncc.DienThoai " +
                "ORDER BY TongSoLuongGiao DESC", thang, nam);

            DataTable dt = DatabaseHelper.GetDataToTable(sql);
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
            ExcelExportHelper.ExportToCSV(dgvBaoCao4, "BaoCao_Top5NCC_Thang");
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
