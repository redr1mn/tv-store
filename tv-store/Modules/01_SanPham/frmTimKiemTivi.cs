using System;
using System.Data;
using System.Windows.Forms;
using tv_store.Helpers;

namespace tv_store.Modules._01_SanPham
{
    public partial class frmTimKiemTivi : Form
    {
        public frmTimKiemTivi()
        {
            InitializeComponent();
        }

        private void frmTimKiemTivi_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
            ThucHienTimKiem();
        }

        private void LoadComboBoxes()
        {
            DatabaseHelper.FillCombo("SELECT MaHangSX, TenHangSX FROM tblHangSX", cboHangSX, "MaHangSX", "TenHangSX");
            DatabaseHelper.FillCombo("SELECT MaManHinh, TenManHinh FROM tblManHinh", cboManHinh, "MaManHinh", "TenManHinh");
            DatabaseHelper.FillCombo("SELECT MaCo, TenCo FROM tblCoManHinh", cboCoManHinh, "MaCo", "TenCo");
        }

        private void ThucHienTimKiem()
        {
            string sql = "SELECT t.MaTV, t.TenTV, h.TenHangSX, k.TenKieu, m.TenMau, mh.TenManHinh, " +
                         "c.TenCo, n.TenNuocSX, t.SoLuong, t.DonGiaBan, t.ThoiGianBaoHanh " +
                         "FROM tblTV t " +
                         "LEFT JOIN tblHangSX h ON t.MaHangSX = h.MaHangSX " +
                         "LEFT JOIN tblKieuDang k ON t.MaKieu = k.MaKieu " +
                         "LEFT JOIN tblMauSac m ON t.MaMau = m.MaMau " +
                         "LEFT JOIN tblManHinh mh ON t.MaManHinh = mh.MaManHinh " +
                         "LEFT JOIN tblCoManHinh c ON t.MaCo = c.MaCo " +
                         "LEFT JOIN tblNuocSX n ON t.MaNuocSX = n.MaNuocSX WHERE 1=1 ";

            // Yêu cầu 4: Tìm kiếm theo Hãng SX, Màn hình, Cỡ màn hình
            if (cboHangSX.SelectedValue != null && !string.IsNullOrEmpty(cboHangSX.SelectedValue.ToString()))
            {
                sql += " AND t.MaHangSX = '" + cboHangSX.SelectedValue.ToString() + "' ";
            }

            if (cboManHinh.SelectedValue != null && !string.IsNullOrEmpty(cboManHinh.SelectedValue.ToString()))
            {
                sql += " AND t.MaManHinh = '" + cboManHinh.SelectedValue.ToString() + "' ";
            }

            if (cboCoManHinh.SelectedValue != null && !string.IsNullOrEmpty(cboCoManHinh.SelectedValue.ToString()))
            {
                sql += " AND t.MaCo = '" + cboCoManHinh.SelectedValue.ToString() + "' ";
            }

            // Mở rộng thêm: Tìm theo từ khóa tên
            if (!string.IsNullOrWhiteSpace(txtTuKhoa.Text))
            {
                sql += " AND t.TenTV LIKE N'%" + txtTuKhoa.Text.Trim() + "%' ";
            }

            // Mở rộng thêm: Khoảng giá bán
            if (double.TryParse(txtGiaTu.Text.Trim(), out double giaTu) && giaTu > 0)
            {
                sql += " AND t.DonGiaBan >= " + giaTu;
            }

            if (double.TryParse(txtGiaDen.Text.Trim(), out double giaDen) && giaDen > 0)
            {
                sql += " AND t.DonGiaBan <= " + giaDen;
            }

            DataTable dt = DatabaseHelper.GetDataToTable(sql);
            dgvKetQua.DataSource = dt;

            if (dgvKetQua.Columns.Count > 0)
            {
                dgvKetQua.Columns["MaTV"].HeaderText = "Mã Tivi";
                dgvKetQua.Columns["TenTV"].HeaderText = "Tên Tivi";
                dgvKetQua.Columns["TenHangSX"].HeaderText = "Hãng SX";
                dgvKetQua.Columns["TenKieu"].HeaderText = "Kiểu dáng";
                dgvKetQua.Columns["TenMau"].HeaderText = "Màu sắc";
                dgvKetQua.Columns["TenManHinh"].HeaderText = "Màn hình";
                dgvKetQua.Columns["TenCo"].HeaderText = "Cỡ màn";
                dgvKetQua.Columns["TenNuocSX"].HeaderText = "Nước SX";
                dgvKetQua.Columns["SoLuong"].HeaderText = "Tồn kho";
                dgvKetQua.Columns["DonGiaBan"].HeaderText = "Giá bán (VNĐ)";
                dgvKetQua.Columns["ThoiGianBaoHanh"].HeaderText = "BH (th)";

                dgvKetQua.Columns["DonGiaBan"].DefaultCellStyle.Format = "#,##0";
            }

            lblSoLuongKetQua.Text = string.Format("Tìm thấy: {0} sản phẩm Tivi phù hợp", dt.Rows.Count);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            ThucHienTimKiem();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTuKhoa.Text = "";
            cboHangSX.SelectedIndex = -1;
            cboManHinh.SelectedIndex = -1;
            cboCoManHinh.SelectedIndex = -1;
            txtGiaTu.Text = "";
            txtGiaDen.Text = "";
            ThucHienTimKiem();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
