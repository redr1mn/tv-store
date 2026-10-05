using System;
using System.Data;
using System.Windows.Forms;
using tv_store.Helpers;

namespace tv_store.Modules._02_NhapHang
{
    public partial class frmNhaCungCap : Form
    {
        public frmNhaCungCap()
        {
            InitializeComponent();
        }

        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
            ResetValues();
        }

        private void LoadDataGridView()
        {
            DataTable dt = DatabaseHelper.GetDataToTable("SELECT MaNCC, TenNCC, DiaChi, DienThoai FROM tblNhaCungCap");
            dgvNCC.DataSource = dt;
            if (dgvNCC.Columns.Count > 0)
            {
                dgvNCC.Columns["MaNCC"].HeaderText = "Mã NCC";
                dgvNCC.Columns["TenNCC"].HeaderText = "Tên Nhà cung cấp";
                dgvNCC.Columns["DiaChi"].HeaderText = "Địa chỉ";
                dgvNCC.Columns["DienThoai"].HeaderText = "Điện thoại";
            }
        }

        private void ResetValues()
        {
            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
        }

        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvNCC.Rows[e.RowIndex];
            txtMaNCC.Text = row.Cells["MaNCC"].Value.ToString();
            txtTenNCC.Text = row.Cells["TenNCC"].Value.ToString();
            txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
            txtDienThoai.Text = row.Cells["DienThoai"].Value.ToString();

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ResetValues();
            txtMaNCC.Text = "NCC" + DateTime.Now.ToString("yyMMddHHmmss");
            txtTenNCC.Focus();
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenNCC.Text))
            {
                errProvider.SetError(txtTenNCC, "Vui lòng nhập tên Nhà cung cấp!");
                txtTenNCC.Focus();
                return;
            }
            errProvider.SetError(txtTenNCC, "");

            string ma = txtMaNCC.Text.Trim();
            if (string.IsNullOrEmpty(ma))
                ma = "NCC" + DateTime.Now.ToString("yyMMddHHmmss");

            if (DatabaseHelper.CheckKey("SELECT MaNCC FROM tblNhaCungCap WHERE MaNCC='" + ma + "'"))
            {
                string sqlUpdate = string.Format("UPDATE tblNhaCungCap SET TenNCC=N'{0}', DiaChi=N'{1}', DienThoai='{2}' WHERE MaNCC='{3}'",
                    txtTenNCC.Text.Trim(), txtDiaChi.Text.Trim(), txtDienThoai.Text.Trim(), ma);
                DatabaseHelper.RunSql(sqlUpdate);
                MessageBox.Show("Cập nhật Nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string sqlInsert = string.Format("INSERT INTO tblNhaCungCap(MaNCC, TenNCC, DiaChi, DienThoai) VALUES('{0}', N'{1}', N'{2}', '{3}')",
                    ma, txtTenNCC.Text.Trim(), txtDiaChi.Text.Trim(), txtDienThoai.Text.Trim());
                DatabaseHelper.RunSql(sqlInsert);
                MessageBox.Show("Thêm mới Nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LoadDataGridView();
            ResetValues();
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNCC.Text)) return;
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            btnThem.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNCC.Text)) return;
            if (MessageBox.Show("Bạn có muốn xóa Nhà cung cấp này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DatabaseHelper.RunSql("DELETE FROM tblNhaCungCap WHERE MaNCC='" + txtMaNCC.Text.Trim() + "'");
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
