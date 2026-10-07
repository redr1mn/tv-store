using System;
using System.Data;
using System.Windows.Forms;
using tv_store.Helpers;

namespace tv_store.Modules._04_HeThong_BaoCao
{
    public partial class frmNhanVien : Form
    {
        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadComboBoxes();
            LoadDataGridView();
            ResetValues();
        }

        private void LoadComboBoxes()
        {
            DatabaseHelper.FillCombo("SELECT MaCa, TenCa FROM tblCaLam", cboCaLam, "MaCa", "TenCa");
            DatabaseHelper.FillCombo("SELECT MaCV, TenCV FROM tblCongViec", cboCongViec, "MaCV", "TenCV");
        }

        private void LoadDataGridView()
        {
            string sql = "SELECT nv.MaNV, nv.TenNV, nv.GioiTinh, nv.NgaySinh, nv.DienThoai, nv.DiaChi, " +
                         "ca.TenCa, cv.TenCV " +
                         "FROM tblNhanVien nv " +
                         "LEFT JOIN tblCaLam ca ON nv.MaCa = ca.MaCa " +
                         "LEFT JOIN tblCongViec cv ON nv.MaCV = cv.MaCV";
            DataTable dt = DatabaseHelper.GetDataToTable(sql);
            dgvNhanVien.DataSource = dt;

            if (dgvNhanVien.Columns.Count > 0)
            {
                dgvNhanVien.Columns["MaNV"].HeaderText = "Mã NV";
                dgvNhanVien.Columns["TenNV"].HeaderText = "Tên Nhân viên";
                dgvNhanVien.Columns["GioiTinh"].HeaderText = "Giới tính";
                dgvNhanVien.Columns["NgaySinh"].HeaderText = "Ngày sinh";
                dgvNhanVien.Columns["DienThoai"].HeaderText = "Điện thoại";
                dgvNhanVien.Columns["DiaChi"].HeaderText = "Địa chỉ";
                dgvNhanVien.Columns["TenCa"].HeaderText = "Ca làm việc";
                dgvNhanVien.Columns["TenCV"].HeaderText = "Công việc";

                dgvNhanVien.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }

        private void ResetValues()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            rdoNam.Checked = true;
            dtpNgaySinh.Value = new DateTime(1998, 1, 1);
            txtDienThoai.Text = "";
            txtDiaChi.Text = "";
            cboCaLam.SelectedIndex = -1;
            cboCongViec.SelectedIndex = -1;
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
            txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
            txtTenNV.Text = row.Cells["TenNV"].Value.ToString();

            string gt = row.Cells["GioiTinh"].Value.ToString();
            if (gt == "Nữ") rdoNu.Checked = true;
            else rdoNam.Checked = true;

            if (DateTime.TryParse(row.Cells["NgaySinh"].Value.ToString(), out DateTime ns))
                dtpNgaySinh.Value = ns;

            txtDienThoai.Text = row.Cells["DienThoai"].Value.ToString();
            txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
            cboCaLam.Text = row.Cells["TenCa"].Value.ToString();
            cboCongViec.Text = row.Cells["TenCV"].Value.ToString();

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ResetValues();
            txtMaNV.Text = "NV" + DateTime.Now.ToString("yyMMddHHmmss");
            txtTenNV.Focus();
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenNV.Text))
            {
                errProvider.SetError(txtTenNV, "Vui lòng nhập tên Nhân viên!");
                txtTenNV.Focus();
                return;
            }
            errProvider.SetError(txtTenNV, "");

            string ma = txtMaNV.Text.Trim();
            if (string.IsNullOrEmpty(ma))
                ma = "NV" + DateTime.Now.ToString("yyMMddHHmmss");

            string gt = rdoNam.Checked ? "Nam" : "Nữ";
            string ngaySinh = dtpNgaySinh.Value.ToString("yyyy-MM-dd");
            string maCa = cboCaLam.SelectedValue != null ? cboCaLam.SelectedValue.ToString() : "";
            string maCV = cboCongViec.SelectedValue != null ? cboCongViec.SelectedValue.ToString() : "";

            bool success = false;
            if (DatabaseHelper.CheckKey("SELECT MaNV FROM tblNhanVien WHERE MaNV='" + ma + "'"))
            {
                string sqlUpdate = string.Format(
                    "UPDATE tblNhanVien SET TenNV=N'{0}', GioiTinh=N'{1}', NgaySinh='{2}', DienThoai='{3}', " +
                    "DiaChi=N'{4}', MaCa={5}, MaCV={6} WHERE MaNV='{7}'",
                    txtTenNV.Text.Trim().Replace("'", "''"), gt, ngaySinh, 
                    txtDienThoai.Text.Trim().Replace("'", "''"), txtDiaChi.Text.Trim().Replace("'", "''"), 
                    string.IsNullOrEmpty(maCa) ? "NULL" : "'" + maCa + "'", 
                    string.IsNullOrEmpty(maCV) ? "NULL" : "'" + maCV + "'", ma);

                if (DatabaseHelper.RunSql(sqlUpdate))
                {
                    MessageBox.Show("Cập nhật Nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    success = true;
                }
            }
            else
            {
                string sqlInsert = string.Format(
                    "INSERT INTO tblNhanVien(MaNV, TenNV, GioiTinh, NgaySinh, DienThoai, DiaChi, MaCa, MaCV) " +
                    "VALUES('{0}', N'{1}', N'{2}', '{3}', '{4}', N'{5}', {6}, {7})",
                    ma, txtTenNV.Text.Trim().Replace("'", "''"), gt, ngaySinh, 
                    txtDienThoai.Text.Trim().Replace("'", "''"), txtDiaChi.Text.Trim().Replace("'", "''"), 
                    string.IsNullOrEmpty(maCa) ? "NULL" : "'" + maCa + "'", 
                    string.IsNullOrEmpty(maCV) ? "NULL" : "'" + maCV + "'");

                if (DatabaseHelper.RunSql(sqlInsert))
                {
                    MessageBox.Show("Thêm mới Nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                txtMaNV.ReadOnly = false;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNV.Text)) return;
            txtMaNV.ReadOnly = true;
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            btnThem.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNV.Text)) return;
            if (MessageBox.Show("Bạn có muốn xóa Nhân viên này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DatabaseHelper.RunSql("DELETE FROM tblNhanVien WHERE MaNV='" + txtMaNV.Text.Trim() + "'");
                LoadDataGridView();
                ResetValues();
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

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
