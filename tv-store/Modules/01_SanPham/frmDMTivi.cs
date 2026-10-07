using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using tv_store.Helpers;

namespace tv_store.Modules._01_SanPham
{
    public partial class frmDMTivi : Form
    {
        private DataTable tblTiviData;

        public frmDMTivi()
        {
            InitializeComponent();
        }

        private void frmDMTivi_Load(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadComboBoxes();
            LoadDataGridView();
            ResetValues();
        }

        private void LoadComboBoxes()
        {
            DatabaseHelper.FillCombo("SELECT MaHangSX, TenHangSX FROM tblHangSX", cboHangSX, "MaHangSX", "TenHangSX");
            DatabaseHelper.FillCombo("SELECT MaKieu, TenKieu FROM tblKieuDang", cboKieuDang, "MaKieu", "TenKieu");
            DatabaseHelper.FillCombo("SELECT MaMau, TenMau FROM tblMauSac", cboMauSac, "MaMau", "TenMau");
            DatabaseHelper.FillCombo("SELECT MaManHinh, TenManHinh FROM tblManHinh", cboManHinh, "MaManHinh", "TenManHinh");
            DatabaseHelper.FillCombo("SELECT MaCo, TenCo FROM tblCoManHinh", cboCoManHinh, "MaCo", "TenCo");
            DatabaseHelper.FillCombo("SELECT MaNuocSX, TenNuocSX FROM tblNuocSX", cboNuocSX, "MaNuocSX", "TenNuocSX");
        }

        public void LoadDataGridView()
        {
            string sql = "SELECT t.MaTV, t.TenTV, h.TenHangSX, k.TenKieu, m.TenMau, mh.TenManHinh, " +
                         "c.TenCo, n.TenNuocSX, t.SoLuong, t.DonGiaNhap, t.DonGiaBan, t.ThoiGianBaoHanh, t.Anh, t.GhiChu " +
                         "FROM tblTV t " +
                         "LEFT JOIN tblHangSX h ON t.MaHangSX = h.MaHangSX " +
                         "LEFT JOIN tblKieuDang k ON t.MaKieu = k.MaKieu " +
                         "LEFT JOIN tblMauSac m ON t.MaMau = m.MaMau " +
                         "LEFT JOIN tblManHinh mh ON t.MaManHinh = mh.MaManHinh " +
                         "LEFT JOIN tblCoManHinh c ON t.MaCo = c.MaCo " +
                         "LEFT JOIN tblNuocSX n ON t.MaNuocSX = n.MaNuocSX";
            tblTiviData = DatabaseHelper.GetDataToTable(sql);
            dgvTivi.DataSource = tblTiviData;

            if (dgvTivi.Columns.Count > 0)
            {
                dgvTivi.Columns["MaTV"].HeaderText = "Mã Tivi";
                dgvTivi.Columns["TenTV"].HeaderText = "Tên Tivi";
                dgvTivi.Columns["TenHangSX"].HeaderText = "Hãng SX";
                dgvTivi.Columns["TenKieu"].HeaderText = "Kiểu dáng";
                dgvTivi.Columns["TenMau"].HeaderText = "Màu sắc";
                dgvTivi.Columns["TenManHinh"].HeaderText = "Màn hình";
                dgvTivi.Columns["TenCo"].HeaderText = "Cỡ màn";
                dgvTivi.Columns["TenNuocSX"].HeaderText = "Nước SX";
                dgvTivi.Columns["SoLuong"].HeaderText = "Số lượng";
                dgvTivi.Columns["DonGiaNhap"].HeaderText = "Đơn giá nhập";
                dgvTivi.Columns["DonGiaBan"].HeaderText = "Đơn giá bán";
                dgvTivi.Columns["ThoiGianBaoHanh"].HeaderText = "BH (th)";
                dgvTivi.Columns["Anh"].HeaderText = "Ảnh";
                dgvTivi.Columns["GhiChu"].HeaderText = "Ghi chú";

                // Format số tiền
                dgvTivi.Columns["DonGiaNhap"].DefaultCellStyle.Format = "#,##0";
                dgvTivi.Columns["DonGiaBan"].DefaultCellStyle.Format = "#,##0";
            }
        }

        private void ResetValues()
        {
            txtMaTivi.Text = "";
            txtTenTivi.Text = "";
            cboHangSX.SelectedIndex = -1;
            cboKieuDang.SelectedIndex = -1;
            cboMauSac.SelectedIndex = -1;
            cboManHinh.SelectedIndex = -1;
            cboCoManHinh.SelectedIndex = -1;
            cboNuocSX.SelectedIndex = -1;
            txtSoLuong.Text = "0";
            txtDonGiaNhap.Text = "0";
            txtDonGiaBan.Text = "0";
            txtBaoHanh.Text = "24";
            txtAnh.Text = "";
            txtGhiChu.Text = "";
            picAnhTivi.Image = null;
        }

        private void dgvTivi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || tblTiviData.Rows.Count == 0) return;

            DataGridViewRow row = dgvTivi.Rows[e.RowIndex];
            txtMaTivi.Text = row.Cells["MaTV"].Value.ToString();
            txtTenTivi.Text = row.Cells["TenTV"].Value.ToString();
            cboHangSX.Text = row.Cells["TenHangSX"].Value.ToString();
            cboKieuDang.Text = row.Cells["TenKieu"].Value.ToString();
            cboMauSac.Text = row.Cells["TenMau"].Value.ToString();
            cboManHinh.Text = row.Cells["TenManHinh"].Value.ToString();
            cboCoManHinh.Text = row.Cells["TenCo"].Value.ToString();
            cboNuocSX.Text = row.Cells["TenNuocSX"].Value.ToString();
            txtSoLuong.Text = row.Cells["SoLuong"].Value.ToString();
            txtDonGiaNhap.Text = row.Cells["DonGiaNhap"].Value.ToString();
            txtDonGiaBan.Text = row.Cells["DonGiaBan"].Value.ToString();
            txtBaoHanh.Text = row.Cells["ThoiGianBaoHanh"].Value.ToString();
            txtAnh.Text = row.Cells["Anh"].Value.ToString();
            txtGhiChu.Text = row.Cells["GhiChu"].Value.ToString();

            // Load ảnh
            string imgName = txtAnh.Text.Trim();
            if (!string.IsNullOrEmpty(imgName) && File.Exists(imgName))
            {
                try
                {
                    picAnhTivi.Image = Image.FromFile(imgName);
                }
                catch
                {
                    picAnhTivi.Image = null;
                }
            }
            else
            {
                picAnhTivi.Image = null;
            }

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void txtDonGiaNhap_TextChanged(object sender, EventArgs e)
        {
            // Nghiệp vụ yêu cầu 3: Đơn giá bán tự động bằng 110% đơn giá nhập
            if (double.TryParse(txtDonGiaNhap.Text.Trim(), out double giaNhap))
            {
                double giaBan = Math.Round(giaNhap * 1.1, 0);
                txtDonGiaBan.Text = giaBan.ToString();
            }
            else
            {
                txtDonGiaBan.Text = "0";
            }
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            if (dlgOpenImage.ShowDialog() == DialogResult.OK)
            {
                txtAnh.Text = dlgOpenImage.FileName;
                try
                {
                    picAnhTivi.Image = Image.FromFile(dlgOpenImage.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể tải ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ResetValues();
            txtMaTivi.Text = "TV" + DateTime.Now.ToString("yyMMddHHmmss");
            txtTenTivi.Focus();
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenTivi.Text))
            {
                errProvider.SetError(txtTenTivi, "Vui lòng nhập tên Tivi!");
                txtTenTivi.Focus();
                return;
            }
            errProvider.SetError(txtTenTivi, "");

            if (cboHangSX.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Hãng sản xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboHangSX.Focus();
                return;
            }

            string maTivi = txtMaTivi.Text.Trim();
            if (string.IsNullOrEmpty(maTivi))
            {
                maTivi = "TV" + DateTime.Now.ToString("yyMMddHHmmss");
            }

            double.TryParse(txtDonGiaNhap.Text.Trim(), out double giaNhap);
            double.TryParse(txtDonGiaBan.Text.Trim(), out double giaBan);
            int.TryParse(txtSoLuong.Text.Trim(), out int soLuong);
            int.TryParse(txtBaoHanh.Text.Trim(), out int baoHanh);

            string maHang = cboHangSX.SelectedValue != null ? cboHangSX.SelectedValue.ToString() : "";
            string maKieu = cboKieuDang.SelectedValue != null ? cboKieuDang.SelectedValue.ToString() : "";
            string maMau = cboMauSac.SelectedValue != null ? cboMauSac.SelectedValue.ToString() : "";
            string maMH = cboManHinh.SelectedValue != null ? cboManHinh.SelectedValue.ToString() : "";
            string maCo = cboCoManHinh.SelectedValue != null ? cboCoManHinh.SelectedValue.ToString() : "";
            string maNuoc = cboNuocSX.SelectedValue != null ? cboNuocSX.SelectedValue.ToString() : "";

            Func<string, string> toSqlFk = val => string.IsNullOrEmpty(val) ? "NULL" : "'" + val.Replace("'", "''") + "'";

            string fkHang = toSqlFk(maHang);
            string fkKieu = toSqlFk(maKieu);
            string fkMau = toSqlFk(maMau);
            string fkMH = toSqlFk(maMH);
            string fkCo = toSqlFk(maCo);
            string fkNuoc = toSqlFk(maNuoc);

            var ci = System.Globalization.CultureInfo.InvariantCulture;
            bool success = false;

            if (DatabaseHelper.CheckKey("SELECT MaTV FROM tblTV WHERE MaTV = '" + maTivi + "'"))
            {
                // Cập nhật
                string sqlUpdate = string.Format(
                    "UPDATE tblTV SET TenTV=N'{0}', MaHangSX={1}, MaKieu={2}, MaMau={3}, " +
                    "MaManHinh={4}, MaCo={5}, MaNuocSX={6}, SoLuong={7}, DonGiaNhap={8}, " +
                    "DonGiaBan={9}, ThoiGianBaoHanh={10}, Anh=N'{11}', GhiChu=N'{12}' WHERE MaTV='{13}'",
                    txtTenTivi.Text.Trim().Replace("'", "''"), fkHang, fkKieu, fkMau, fkMH, fkCo, fkNuoc,
                    soLuong, giaNhap.ToString(ci), giaBan.ToString(ci), baoHanh, 
                    txtAnh.Text.Trim().Replace("'", "''"), txtGhiChu.Text.Trim().Replace("'", "''"), maTivi);
                
                if (DatabaseHelper.RunSql(sqlUpdate))
                {
                    MessageBox.Show("Cập nhật Tivi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    success = true;
                }
            }
            else
            {
                // Thêm mới
                string sqlInsert = string.Format(
                    "INSERT INTO tblTV(MaTV, TenTV, MaHangSX, MaKieu, MaMau, MaManHinh, MaCo, MaNuocSX, SoLuong, DonGiaNhap, DonGiaBan, ThoiGianBaoHanh, Anh, GhiChu) " +
                    "VALUES('{0}', N'{1}', {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, N'{12}', N'{13}')",
                    maTivi, txtTenTivi.Text.Trim().Replace("'", "''"), fkHang, fkKieu, fkMau, fkMH, fkCo, fkNuoc,
                    soLuong, giaNhap.ToString(ci), giaBan.ToString(ci), baoHanh, 
                    txtAnh.Text.Trim().Replace("'", "''"), txtGhiChu.Text.Trim().Replace("'", "''"));

                if (DatabaseHelper.RunSql(sqlInsert))
                {
                    MessageBox.Show("Thêm mới Tivi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    success = true;
                }
            }

            if (success)
            {
                LoadDataGridView();
                ResetValues();
                btnThem.Enabled = true;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = false;
                btnBoQua.Enabled = false;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaTivi.Text))
            {
                MessageBox.Show("Vui lòng chọn dòng Tivi cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            btnThem.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaTivi.Text))
            {
                MessageBox.Show("Vui lòng chọn dòng Tivi cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm Tivi này?", "Xác nhận xóa", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "DELETE FROM tblTV WHERE MaTV = '" + txtMaTivi.Text.Trim() + "'";
                if (DatabaseHelper.RunSql(sql))
                {
                    MessageBox.Show("Xóa Tivi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataGridView();
                    ResetValues();
                }
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            frmTimKiemTivi frm = new frmTimKiemTivi();
            frm.ShowDialog();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
