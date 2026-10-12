using System;
using System.Data;
using System.Windows.Forms;
using tv_store.Helpers;

namespace tv_store.Modules._03_BanHang
{
    public partial class frmTimKiemHDB : Form
    {
        private DataTable tblKetQua;

        public frmTimKiemHDB()
        {
            InitializeComponent();
        }

        private void frmTimKiemHDB_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
            dtpTuNgay.Value = DateTime.Now.AddMonths(-1);
            dtpDenNgay.Value = DateTime.Now;
            ThucHienTimKiem();
        }

        private void LoadComboBoxes()
        {
            DatabaseHelper.FillCombo("SELECT MaKhach, TenKhach FROM tblKhachHang", cboKhachHang, "MaKhach", "TenKhach");
            DatabaseHelper.FillCombo("SELECT MaNV, TenNV FROM tblNhanVien", cboNhanVien, "MaNV", "TenNV");
            DatabaseHelper.FillCombo("SELECT MaTV, TenTV FROM tblTV", cboMaTivi, "MaTV", "TenTV");
        }

        private void chkLocNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpTuNgay.Enabled = chkLocNgay.Checked;
            dtpDenNgay.Enabled = chkLocNgay.Checked;
        }

        private void ThucHienTimKiem()
        {
            string sql = "SELECT hdb.SoHDB, hdb.NgayBan, kh.TenKhach, kh.DienThoai AS SDTKhach, nv.TenNV, " +
                         "ct.MaTV, tv.TenTV, ct.SoLuong, ct.DonGia, ct.GiamGia, ct.ThanhTien, " +
                         "hdb.Thue, hdb.TongTien " +
                         "FROM tblChiTietHDB ct " +
                         "INNER JOIN tblHoaDonBan hdb ON ct.SoHDB = hdb.SoHDB " +
                         "INNER JOIN tblTV tv ON ct.MaTV = tv.MaTV " +
                         "INNER JOIN tblKhachHang kh ON hdb.MaKhach = kh.MaKhach " +
                         "INNER JOIN tblNhanVien nv ON hdb.MaNV = nv.MaNV " +
                         "WHERE 1=1 ";

            if (!string.IsNullOrWhiteSpace(txtMaHDB.Text))
            {
                sql += " AND hdb.SoHDB LIKE '%" + txtMaHDB.Text.Trim().Replace("'", "''") + "%' ";
            }

            if (cboKhachHang.SelectedValue != null && !string.IsNullOrEmpty(cboKhachHang.SelectedValue.ToString()))
            {
                sql += " AND hdb.MaKhach = '" + cboKhachHang.SelectedValue.ToString() + "' ";
            }

            if (cboNhanVien.SelectedValue != null && !string.IsNullOrEmpty(cboNhanVien.SelectedValue.ToString()))
            {
                sql += " AND hdb.MaNV = '" + cboNhanVien.SelectedValue.ToString() + "' ";
            }

            if (cboMaTivi.SelectedValue != null && !string.IsNullOrEmpty(cboMaTivi.SelectedValue.ToString()))
            {
                sql += " AND ct.MaTV = '" + cboMaTivi.SelectedValue.ToString() + "' ";
            }

            if (chkLocNgay.Checked)
            {
                string tuNgay = dtpTuNgay.Value.ToString("yyyy-MM-dd 00:00:00");
                string denNgay = dtpDenNgay.Value.ToString("yyyy-MM-dd 23:59:59");
                sql += string.Format(" AND hdb.NgayBan >= '{0}' AND hdb.NgayBan <= '{1}' ", tuNgay, denNgay);
            }

            sql += " ORDER BY hdb.NgayBan DESC, hdb.SoHDB DESC";

            tblKetQua = DatabaseHelper.GetDataToTable(sql);
            dgvKetQuaHDB.DataSource = tblKetQua;

            if (dgvKetQuaHDB.Columns.Count > 0)
            {
                dgvKetQuaHDB.Columns["SoHDB"].HeaderText = "Mã HĐB";
                dgvKetQuaHDB.Columns["NgayBan"].HeaderText = "Ngày bán";
                dgvKetQuaHDB.Columns["TenKhach"].HeaderText = "Khách hàng";
                dgvKetQuaHDB.Columns["SDTKhach"].HeaderText = "Điện thoại";
                dgvKetQuaHDB.Columns["TenNV"].HeaderText = "Nhân viên";
                dgvKetQuaHDB.Columns["MaTV"].HeaderText = "Mã Tivi";
                dgvKetQuaHDB.Columns["TenTV"].HeaderText = "Tên Tivi";
                dgvKetQuaHDB.Columns["SoLuong"].HeaderText = "SL";
                dgvKetQuaHDB.Columns["DonGia"].HeaderText = "Đơn giá";
                dgvKetQuaHDB.Columns["GiamGia"].HeaderText = "CK (%)";
                dgvKetQuaHDB.Columns["ThanhTien"].HeaderText = "Thành tiền";
                dgvKetQuaHDB.Columns["Thue"].HeaderText = "VAT (%)";
                dgvKetQuaHDB.Columns["TongTien"].HeaderText = "Tổng HĐ";

                dgvKetQuaHDB.Columns["NgayBan"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvKetQuaHDB.Columns["DonGia"].DefaultCellStyle.Format = "#,##0";
                dgvKetQuaHDB.Columns["ThanhTien"].DefaultCellStyle.Format = "#,##0";
                dgvKetQuaHDB.Columns["TongTien"].DefaultCellStyle.Format = "#,##0";

                dgvKetQuaHDB.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvKetQuaHDB.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvKetQuaHDB.Columns["GiamGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvKetQuaHDB.Columns["ThanhTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvKetQuaHDB.Columns["Thue"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvKetQuaHDB.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                // Đặt độ rộng cột phù hợp để không bị cắt chữ '...' tương tự bên sản phẩm Tivi
                dgvKetQuaHDB.Columns["SoHDB"].Width = 115;
                dgvKetQuaHDB.Columns["NgayBan"].Width = 135;
                dgvKetQuaHDB.Columns["TenKhach"].Width = 180;
                dgvKetQuaHDB.Columns["SDTKhach"].Width = 100;
                dgvKetQuaHDB.Columns["TenNV"].Width = 140;
                dgvKetQuaHDB.Columns["MaTV"].Width = 75;
                dgvKetQuaHDB.Columns["TenTV"].Width = 240;
                dgvKetQuaHDB.Columns["SoLuong"].Width = 60;
                dgvKetQuaHDB.Columns["DonGia"].Width = 110;
                dgvKetQuaHDB.Columns["GiamGia"].Width = 65;
                dgvKetQuaHDB.Columns["ThanhTien"].Width = 115;
                dgvKetQuaHDB.Columns["Thue"].Width = 70;
                dgvKetQuaHDB.Columns["TongTien"].Width = 120;
            }

            // Tính toán tổng hợp số hóa đơn và tổng doanh thu
            System.Collections.Generic.HashSet<string> distinctHDB = new System.Collections.Generic.HashSet<string>();
            double tongTienDoanhThu = 0;

            foreach (DataRow r in tblKetQua.Rows)
            {
                string soHDB = r["SoHDB"].ToString();
                if (!distinctHDB.Contains(soHDB))
                {
                    distinctHDB.Add(soHDB);
                    tongTienDoanhThu += Convert.ToDouble(r["TongTien"]);
                }
            }

            lblSoLuongKetQua.Text = string.Format("Tìm thấy: {0} hóa đơn ({1} dòng mặt hàng bán phù hợp)", distinctHDB.Count, tblKetQua.Rows.Count);
            lblTongTien.Text = string.Format("Tổng doanh thu: {0}", DatabaseHelper.FormatTien(tongTienDoanhThu));
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            ThucHienTimKiem();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaHDB.Text = "";
            cboKhachHang.SelectedIndex = -1;
            cboNhanVien.SelectedIndex = -1;
            cboMaTivi.SelectedIndex = -1;
            chkLocNgay.Checked = false;
            dtpTuNgay.Value = DateTime.Now.AddMonths(-1);
            dtpDenNgay.Value = DateTime.Now;
            ThucHienTimKiem();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            ExcelExportHelper.ExportDataGridViewToExcel(dgvKetQuaHDB, "KẾT QUẢ TRA CỨU HÓA ĐƠN BÁN", "TraCuuHoaDonBan", "KetQuaTraCuu");
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvKetQuaHDB_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || tblKetQua.Rows.Count == 0) return;

            DataGridViewRow r = dgvKetQuaHDB.Rows[e.RowIndex];
            string soHDB = r.Cells["SoHDB"].Value.ToString();
            string ngay = Convert.ToDateTime(r.Cells["NgayBan"].Value).ToString("dd/MM/yyyy HH:mm");
            string khach = r.Cells["TenKhach"].Value.ToString();
            string nv = r.Cells["TenNV"].Value.ToString();
            string tongTien = DatabaseHelper.FormatTien(Convert.ToDouble(r.Cells["TongTien"].Value));

            MessageBox.Show(
                string.Format("THÔNG TIN HÓA ĐƠN BÁN:\n" +
                              "- Số HĐB: {0}\n" +
                              "- Ngày lập: {1}\n" +
                              "- Khách hàng: {2}\n" +
                              "- Nhân viên lập: {3}\n" +
                              "- Tổng tiền thanh toán: {4}",
                              soHDB, ngay, khach, nv, tongTien),
                "Chi tiết hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
