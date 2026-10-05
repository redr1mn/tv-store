using System;
using System.Data;
using System.Windows.Forms;
using tv_store.Helpers;

namespace tv_store.Modules._02_NhapHang
{
    public partial class frmTimKiemHDN : Form
    {
        public frmTimKiemHDN()
        {
            InitializeComponent();
        }

        private void frmTimKiemHDN_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
            ThucHienTimKiem();
        }

        private void LoadComboBoxes()
        {
            DatabaseHelper.FillCombo("SELECT MaTV, TenTV FROM tblTV", cboMaTivi, "MaTV", "TenTV");
            DatabaseHelper.FillCombo("SELECT MaNCC, TenNCC FROM tblNhaCungCap", cboNCC, "MaNCC", "TenNCC");
        }

        private void ThucHienTimKiem()
        {
            // Yêu cầu 5: Tìm kiếm hóa đơn nhập theo Mã Tivi và Số lượng nhập
            string sql = "SELECT hdn.SoHDN, hdn.NgayNhap, ncc.TenNCC, nv.TenNV, " +
                         "ct.MaTV, tv.TenTV, ct.SoLuong AS SoLuongNhap, ct.DonGia, ct.ThanhTien " +
                         "FROM tblChiTietHDN ct " +
                         "INNER JOIN tblHoaDonNhap hdn ON ct.MaHDN = hdn.SoHDN " +
                         "INNER JOIN tblTV tv ON ct.MaTV = tv.MaTivi " +
                         "INNER JOIN tblNhaCungCap ncc ON hdn.MaNCC = ncc.MaNCC " +
                         "INNER JOIN tblNhanVien nv ON hdn.MaNV = nv.MaNV " +
                         "WHERE 1=1 ";

            if (cboMaTivi.SelectedValue != null && !string.IsNullOrEmpty(cboMaTivi.SelectedValue.ToString()))
            {
                sql += " AND ct.MaTV = '" + cboMaTivi.SelectedValue.ToString() + "' ";
            }

            if (int.TryParse(txtSoLuongNhap.Text.Trim(), out int slNhap) && slNhap > 0)
            {
                sql += " AND ct.SoLuong >= " + slNhap;
            }

            if (cboNCC.SelectedValue != null && !string.IsNullOrEmpty(cboNCC.SelectedValue.ToString()))
            {
                sql += " AND hdn.MaNCC = '" + cboNCC.SelectedValue.ToString() + "' ";
            }

            sql += " ORDER BY hdn.NgayNhap DESC";

            DataTable dt = DatabaseHelper.GetDataToTable(sql);
            dgvKetQuaHDN.DataSource = dt;

            if (dgvKetQuaHDN.Columns.Count > 0)
            {
                dgvKetQuaHDN.Columns["SoHDN"].HeaderText = "Mã HĐN";
                dgvKetQuaHDN.Columns["NgayNhap"].HeaderText = "Ngày nhập";
                dgvKetQuaHDN.Columns["TenNCC"].HeaderText = "Nhà cung cấp";
                dgvKetQuaHDN.Columns["TenNV"].HeaderText = "Nhân viên";
                dgvKetQuaHDN.Columns["MaTV"].HeaderText = "Mã Tivi";
                dgvKetQuaHDN.Columns["TenTV"].HeaderText = "Tên Tivi";
                dgvKetQuaHDN.Columns["SoLuongNhap"].HeaderText = "SL nhập";
                dgvKetQuaHDN.Columns["DonGia"].HeaderText = "Đơn giá";
                dgvKetQuaHDN.Columns["ThanhTien"].HeaderText = "Thành tiền";

                dgvKetQuaHDN.Columns["DonGia"].DefaultCellStyle.Format = "#,##0";
                dgvKetQuaHDN.Columns["ThanhTien"].DefaultCellStyle.Format = "#,##0";
            }

            lblSoLuongKetQua.Text = string.Format("Tìm thấy: {0} hóa đơn / mặt hàng nhập phù hợp", dt.Rows.Count);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            ThucHienTimKiem();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            cboMaTivi.SelectedIndex = -1;
            cboNCC.SelectedIndex = -1;
            txtSoLuongNhap.Text = "";
            ThucHienTimKiem();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
