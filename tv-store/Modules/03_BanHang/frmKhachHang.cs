using System;
using System.Data;
using System.Windows.Forms;
using tv_store.Helpers;

namespace tv_store.Modules._03_BanHang
{
    public partial class frmKhachHang : Form
    {
        public frmKhachHang()
        {
            InitializeComponent();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
            ResetValues();
        }

        private void LoadDataGridView()
        {
            DataTable dt = DatabaseHelper.GetDataToTable("SELECT MaKhach, TenKhach, DiaChi, DienThoai FROM tblKhachHang");
            dgvKhachHang.DataSource = dt;
            if (dgvKhachHang.Columns.Count > 0)
            {
                dgvKhachHang.Columns["MaKhach"].HeaderText = "Mã KH";
                dgvKhachHang.Columns["TenKhach"].HeaderText = "Tên Khách hàng";
                dgvKhachHang.Columns["DiaChi"].HeaderText = "Địa chỉ";
                dgvKhachHang.Columns["DienThoai"].HeaderText = "Điện thoại";
            }
        }

        private void ResetValues()
        {
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
            txtMaKH.Text = row.Cells["MaKhach"].Value.ToString();
            txtTenKH.Text = row.Cells["TenKhach"].Value.ToString();
            txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
            txtDienThoai.Text = row.Cells["DienThoai"].Value.ToString();

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ResetValues();
            txtMaKH.Text = "KH" + DateTime.Now.ToString("yyMMddHHmmss");
            txtTenKH.Focus();
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenKH.Text))
            {
                errProvider.SetError(txtTenKH, "Vui lòng nhập tên Khách hàng!");
                txtTenKH.Focus();
                return;
            }
            errProvider.SetError(txtTenKH, "");

            string ma = txtMaKH.Text.Trim();
            if (string.IsNullOrEmpty(ma))
                ma = "KH" + DateTime.Now.ToString("yyMMddHHmmss");

            if (DatabaseHelper.CheckKey("SELECT MaKhach FROM tblKhachHang WHERE MaKhach='" + ma + "'"))
            {
                string sqlUpdate = string.Format("UPDATE tblKhachHang SET TenKhach=N'{0}', DiaChi=N'{1}', DienThoai='{2}' WHERE MaKhach='{3}'",
                    txtTenKH.Text.Trim(), txtDiaChi.Text.Trim(), txtDienThoai.Text.Trim(), ma);
                DatabaseHelper.RunSql(sqlUpdate);
                MessageBox.Show("Cập nhật Khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string sqlInsert = string.Format("INSERT INTO tblKhachHang(MaKhach, TenKhach, DiaChi, DienThoai) VALUES('{0}', N'{1}', N'{2}', '{3}')",
                    ma, txtTenKH.Text.Trim(), txtDiaChi.Text.Trim(), txtDienThoai.Text.Trim());
                DatabaseHelper.RunSql(sqlInsert);
                MessageBox.Show("Thêm mới Khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (string.IsNullOrEmpty(txtMaKH.Text)) return;
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            btnThem.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text)) return;
            if (MessageBox.Show("Bạn có muốn xóa Khách hàng này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DatabaseHelper.RunSql("DELETE FROM tblKhachHang WHERE MaKhach='" + txtMaKH.Text.Trim() + "'");
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
