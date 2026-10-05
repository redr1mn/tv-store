using System;
using System.Data;
using System.Windows.Forms;
using tv_store.Helpers;

namespace tv_store.Modules._01_SanPham
{
    public partial class frmDanhMucPhu : Form
    {
        public frmDanhMucPhu()
        {
            InitializeComponent();
        }

        private void frmDanhMucPhu_Load(object sender, EventArgs e)
        {
            LoadDataHangSX();
            LoadDataKieuDang();
            LoadDataMauSac();
            LoadDataManHinh();
            LoadDataCoManHinh();
            LoadDataNuocSX();
        }

        private void tbcDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tbcDanhMuc.SelectedIndex)
            {
                case 0: LoadDataHangSX(); break;
                case 1: LoadDataKieuDang(); break;
                case 2: LoadDataMauSac(); break;
                case 3: LoadDataManHinh(); break;
                case 4: LoadDataCoManHinh(); break;
                case 5: LoadDataNuocSX(); break;
            }
        }

        #region 1. HÃNG SẢN XUẤT
        private void LoadDataHangSX()
        {
            dgvHangSX.DataSource = DatabaseHelper.GetDataToTable("SELECT MaHangSX, TenHangSX FROM tblHangSX");
            if (dgvHangSX.Columns.Count > 0)
            {
                dgvHangSX.Columns["MaHangSX"].HeaderText = "Mã Hãng SX";
                dgvHangSX.Columns["TenHangSX"].HeaderText = "Tên Hãng SX";
            }
            btnLuu1.Enabled = false;
            btnBoQua1.Enabled = false;
        }

        private void dgvHangSX_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvHangSX.Rows[e.RowIndex];
            txtMaHangSX.Text = row.Cells["MaHangSX"].Value.ToString();
            txtTenHangSX.Text = row.Cells["TenHangSX"].Value.ToString();
            btnSua1.Enabled = true;
            btnXoa1.Enabled = true;
            btnBoQua1.Enabled = true;
        }

        private void btnThem1_Click(object sender, EventArgs e)
        {
            txtMaHangSX.Text = "HSX" + DateTime.Now.ToString("yyMMddHHmmss");
            txtTenHangSX.Text = "";
            txtTenHangSX.Focus();
            btnThem1.Enabled = false;
            btnLuu1.Enabled = true;
            btnBoQua1.Enabled = true;
        }

        private void btnLuu1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenHangSX.Text))
            {
                MessageBox.Show("Vui lòng nhập tên Hãng sản xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string ma = txtMaHangSX.Text.Trim();
            string ten = txtTenHangSX.Text.Trim();

            if (DatabaseHelper.CheckKey("SELECT MaHangSX FROM tblHangSX WHERE MaHangSX='" + ma + "'"))
            {
                DatabaseHelper.RunSql("UPDATE tblHangSX SET TenHangSX=N'" + ten + "' WHERE MaHangSX='" + ma + "'");
            }
            else
            {
                DatabaseHelper.RunSql("INSERT INTO tblHangSX(MaHangSX, TenHangSX) VALUES('" + ma + "', N'" + ten + "')");
            }
            LoadDataHangSX();
            btnThem1.Enabled = true;
        }

        private void btnSua1_Click(object sender, EventArgs e)
        {
            btnLuu1.Enabled = true;
            btnBoQua1.Enabled = true;
            btnThem1.Enabled = false;
        }

        private void btnXoa1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHangSX.Text)) return;
            if (MessageBox.Show("Bạn có muốn xóa hãng này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DatabaseHelper.RunSql("DELETE FROM tblHangSX WHERE MaHangSX='" + txtMaHangSX.Text.Trim() + "'");
                LoadDataHangSX();
                txtMaHangSX.Text = "";
                txtTenHangSX.Text = "";
            }
        }

        private void btnBoQua1_Click(object sender, EventArgs e)
        {
            txtMaHangSX.Text = "";
            txtTenHangSX.Text = "";
            btnThem1.Enabled = true;
            btnLuu1.Enabled = false;
            btnBoQua1.Enabled = false;
        }
        #endregion

        #region 2. KIỂU DÁNG
        private void LoadDataKieuDang()
        {
            dgvKieuDang.DataSource = DatabaseHelper.GetDataToTable("SELECT MaKieu, TenKieu FROM tblKieuDang");
            if (dgvKieuDang.Columns.Count > 0)
            {
                dgvKieuDang.Columns["MaKieu"].HeaderText = "Mã kiểu dáng";
                dgvKieuDang.Columns["TenKieu"].HeaderText = "Tên kiểu dáng";
            }
            btnLuu2.Enabled = false;
            btnBoQua2.Enabled = false;
        }

        private void dgvKieuDang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvKieuDang.Rows[e.RowIndex];
            txtMaKieu.Text = row.Cells["MaKieu"].Value.ToString();
            txtTenKieu.Text = row.Cells["TenKieu"].Value.ToString();
            btnSua2.Enabled = true;
            btnXoa2.Enabled = true;
            btnBoQua2.Enabled = true;
        }

        private void btnThem2_Click(object sender, EventArgs e)
        {
            txtMaKieu.Text = "KD" + DateTime.Now.ToString("yyMMddHHmmss");
            txtTenKieu.Text = "";
            txtTenKieu.Focus();
            btnThem2.Enabled = false;
            btnLuu2.Enabled = true;
            btnBoQua2.Enabled = true;
        }

        private void btnLuu2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenKieu.Text)) return;
            string ma = txtMaKieu.Text.Trim();
            string ten = txtTenKieu.Text.Trim();

            if (DatabaseHelper.CheckKey("SELECT MaKieu FROM tblKieuDang WHERE MaKieu='" + ma + "'"))
                DatabaseHelper.RunSql("UPDATE tblKieuDang SET TenKieu=N'" + ten + "' WHERE MaKieu='" + ma + "'");
            else
                DatabaseHelper.RunSql("INSERT INTO tblKieuDang(MaKieu, TenKieu) VALUES('" + ma + "', N'" + ten + "')");

            LoadDataKieuDang();
            btnThem2.Enabled = true;
        }

        private void btnSua2_Click(object sender, EventArgs e)
        {
            btnLuu2.Enabled = true;
            btnBoQua2.Enabled = true;
            btnThem2.Enabled = false;
        }

        private void btnXoa2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKieu.Text)) return;
            if (MessageBox.Show("Bạn có muốn xóa kiểu dáng này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DatabaseHelper.RunSql("DELETE FROM tblKieuDang WHERE MaKieu='" + txtMaKieu.Text.Trim() + "'");
                LoadDataKieuDang();
                txtMaKieu.Text = "";
                txtTenKieu.Text = "";
            }
        }

        private void btnBoQua2_Click(object sender, EventArgs e)
        {
            txtMaKieu.Text = "";
            txtTenKieu.Text = "";
            btnThem2.Enabled = true;
            btnLuu2.Enabled = false;
            btnBoQua2.Enabled = false;
        }
        #endregion

        #region 3. MÀU SẮC
        private void LoadDataMauSac()
        {
            dgvMauSac.DataSource = DatabaseHelper.GetDataToTable("SELECT MaMau, TenMau FROM tblMauSac");
            if (dgvMauSac.Columns.Count > 0)
            {
                dgvMauSac.Columns["MaMau"].HeaderText = "Mã màu sắc";
                dgvMauSac.Columns["TenMau"].HeaderText = "Tên màu sắc";
            }
            btnLuu3.Enabled = false;
            btnBoQua3.Enabled = false;
        }

        private void dgvMauSac_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvMauSac.Rows[e.RowIndex];
            txtMaMau.Text = row.Cells["MaMau"].Value.ToString();
            txtTenMau.Text = row.Cells["TenMau"].Value.ToString();
            btnSua3.Enabled = true;
            btnXoa3.Enabled = true;
            btnBoQua3.Enabled = true;
        }

        private void btnThem3_Click(object sender, EventArgs e)
        {
            txtMaMau.Text = "M" + DateTime.Now.ToString("yyMMddHHmmss");
            txtTenMau.Text = "";
            txtTenMau.Focus();
            btnThem3.Enabled = false;
            btnLuu3.Enabled = true;
            btnBoQua3.Enabled = true;
        }

        private void btnLuu3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenMau.Text)) return;
            string ma = txtMaMau.Text.Trim();
            string ten = txtTenMau.Text.Trim();

            if (DatabaseHelper.CheckKey("SELECT MaMau FROM tblMauSac WHERE MaMau='" + ma + "'"))
                DatabaseHelper.RunSql("UPDATE tblMauSac SET TenMau=N'" + ten + "' WHERE MaMau='" + ma + "'");
            else
                DatabaseHelper.RunSql("INSERT INTO tblMauSac(MaMau, TenMau) VALUES('" + ma + "', N'" + ten + "')");

            LoadDataMauSac();
            btnThem3.Enabled = true;
        }

        private void btnSua3_Click(object sender, EventArgs e)
        {
            btnLuu3.Enabled = true;
            btnBoQua3.Enabled = true;
            btnThem3.Enabled = false;
        }

        private void btnXoa3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaMau.Text)) return;
            if (MessageBox.Show("Bạn có muốn xóa màu sắc này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DatabaseHelper.RunSql("DELETE FROM tblMauSac WHERE MaMau='" + txtMaMau.Text.Trim() + "'");
                LoadDataMauSac();
                txtMaMau.Text = "";
                txtTenMau.Text = "";
            }
        }

        private void btnBoQua3_Click(object sender, EventArgs e)
        {
            txtMaMau.Text = "";
            txtTenMau.Text = "";
            btnThem3.Enabled = true;
            btnLuu3.Enabled = false;
            btnBoQua3.Enabled = false;
        }
        #endregion

        #region 4. LOẠI MÀN HÌNH
        private void LoadDataManHinh()
        {
            dgvManHinh.DataSource = DatabaseHelper.GetDataToTable("SELECT MaManHinh, TenManHinh FROM tblManHinh");
            if (dgvManHinh.Columns.Count > 0)
            {
                dgvManHinh.Columns["MaManHinh"].HeaderText = "Mã màn hình";
                dgvManHinh.Columns["TenManHinh"].HeaderText = "Tên loại màn hình";
            }
            btnLuu4.Enabled = false;
            btnBoQua4.Enabled = false;
        }

        private void dgvManHinh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvManHinh.Rows[e.RowIndex];
            txtMaManHinh.Text = row.Cells["MaManHinh"].Value.ToString();
            txtTenManHinh.Text = row.Cells["TenManHinh"].Value.ToString();
            btnSua4.Enabled = true;
            btnXoa4.Enabled = true;
            btnBoQua4.Enabled = true;
        }

        private void btnThem4_Click(object sender, EventArgs e)
        {
            txtMaManHinh.Text = "MH" + DateTime.Now.ToString("yyMMddHHmmss");
            txtTenManHinh.Text = "";
            txtTenManHinh.Focus();
            btnThem4.Enabled = false;
            btnLuu4.Enabled = true;
            btnBoQua4.Enabled = true;
        }

        private void btnLuu4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenManHinh.Text)) return;
            string ma = txtMaManHinh.Text.Trim();
            string ten = txtTenManHinh.Text.Trim();

            if (DatabaseHelper.CheckKey("SELECT MaManHinh FROM tblManHinh WHERE MaManHinh='" + ma + "'"))
                DatabaseHelper.RunSql("UPDATE tblManHinh SET TenManHinh=N'" + ten + "' WHERE MaManHinh='" + ma + "'");
            else
                DatabaseHelper.RunSql("INSERT INTO tblManHinh(MaManHinh, TenManHinh) VALUES('" + ma + "', N'" + ten + "')");

            LoadDataManHinh();
            btnThem4.Enabled = true;
        }

        private void btnSua4_Click(object sender, EventArgs e)
        {
            btnLuu4.Enabled = true;
            btnBoQua4.Enabled = true;
            btnThem4.Enabled = false;
        }

        private void btnXoa4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaManHinh.Text)) return;
            if (MessageBox.Show("Bạn có muốn xóa màn hình này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DatabaseHelper.RunSql("DELETE FROM tblManHinh WHERE MaManHinh='" + txtMaManHinh.Text.Trim() + "'");
                LoadDataManHinh();
                txtMaManHinh.Text = "";
                txtTenManHinh.Text = "";
            }
        }

        private void btnBoQua4_Click(object sender, EventArgs e)
        {
            txtMaManHinh.Text = "";
            txtTenManHinh.Text = "";
            btnThem4.Enabled = true;
            btnLuu4.Enabled = false;
            btnBoQua4.Enabled = false;
        }
        #endregion

        #region 5. CỠ MÀN HÌNH
        private void LoadDataCoManHinh()
        {
            dgvCoManHinh.DataSource = DatabaseHelper.GetDataToTable("SELECT MaCo, TenCo FROM tblCoManHinh");
            if (dgvCoManHinh.Columns.Count > 0)
            {
                dgvCoManHinh.Columns["MaCo"].HeaderText = "Mã cỡ";
                dgvCoManHinh.Columns["TenCo"].HeaderText = "Tên cỡ (inch)";
            }
            btnLuu5.Enabled = false;
            btnBoQua5.Enabled = false;
        }

        private void dgvCoManHinh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvCoManHinh.Rows[e.RowIndex];
            txtMaCo.Text = row.Cells["MaCo"].Value.ToString();
            txtTenCo.Text = row.Cells["TenCo"].Value.ToString();
            btnSua5.Enabled = true;
            btnXoa5.Enabled = true;
            btnBoQua5.Enabled = true;
        }

        private void btnThem5_Click(object sender, EventArgs e)
        {
            txtMaCo.Text = "CO" + DateTime.Now.ToString("yyMMddHHmmss");
            txtTenCo.Text = "";
            txtTenCo.Focus();
            btnThem5.Enabled = false;
            btnLuu5.Enabled = true;
            btnBoQua5.Enabled = true;
        }

        private void btnLuu5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenCo.Text)) return;
            string ma = txtMaCo.Text.Trim();
            string ten = txtTenCo.Text.Trim();

            if (DatabaseHelper.CheckKey("SELECT MaCo FROM tblCoManHinh WHERE MaCo='" + ma + "'"))
                DatabaseHelper.RunSql("UPDATE tblCoManHinh SET TenCo=N'" + ten + "' WHERE MaCo='" + ma + "'");
            else
                DatabaseHelper.RunSql("INSERT INTO tblCoManHinh(MaCo, TenCo) VALUES('" + ma + "', N'" + ten + "')");

            LoadDataCoManHinh();
            btnThem5.Enabled = true;
        }

        private void btnSua5_Click(object sender, EventArgs e)
        {
            btnLuu5.Enabled = true;
            btnBoQua5.Enabled = true;
            btnThem5.Enabled = false;
        }

        private void btnXoa5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaCo.Text)) return;
            if (MessageBox.Show("Bạn có muốn xóa cỡ màn hình này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DatabaseHelper.RunSql("DELETE FROM tblCoManHinh WHERE MaCo='" + txtMaCo.Text.Trim() + "'");
                LoadDataCoManHinh();
                txtMaCo.Text = "";
                txtTenCo.Text = "";
            }
        }

        private void btnBoQua5_Click(object sender, EventArgs e)
        {
            txtMaCo.Text = "";
            txtTenCo.Text = "";
            btnThem5.Enabled = true;
            btnLuu5.Enabled = false;
            btnBoQua5.Enabled = false;
        }
        #endregion

        #region 6. NƯỚC SẢN XUẤT
        private void LoadDataNuocSX()
        {
            dgvNuocSX.DataSource = DatabaseHelper.GetDataToTable("SELECT MaNuocSX, TenNuocSX FROM tblNuocSX");
            if (dgvNuocSX.Columns.Count > 0)
            {
                dgvNuocSX.Columns["MaNuocSX"].HeaderText = "Mã nước SX";
                dgvNuocSX.Columns["TenNuocSX"].HeaderText = "Tên nước SX";
            }
            btnLuu6.Enabled = false;
            btnBoQua6.Enabled = false;
        }

        private void dgvNuocSX_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvNuocSX.Rows[e.RowIndex];
            txtMaNuocSX.Text = row.Cells["MaNuocSX"].Value.ToString();
            txtTenNuocSX.Text = row.Cells["TenNuocSX"].Value.ToString();
            btnSua6.Enabled = true;
            btnXoa6.Enabled = true;
            btnBoQua6.Enabled = true;
        }

        private void btnThem6_Click(object sender, EventArgs e)
        {
            txtMaNuocSX.Text = "NSX" + DateTime.Now.ToString("yyMMddHHmmss");
            txtTenNuocSX.Text = "";
            txtTenNuocSX.Focus();
            btnThem6.Enabled = false;
            btnLuu6.Enabled = true;
            btnBoQua6.Enabled = true;
        }

        private void btnLuu6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenNuocSX.Text)) return;
            string ma = txtMaNuocSX.Text.Trim();
            string ten = txtTenNuocSX.Text.Trim();

            if (DatabaseHelper.CheckKey("SELECT MaNuocSX FROM tblNuocSX WHERE MaNuocSX='" + ma + "'"))
                DatabaseHelper.RunSql("UPDATE tblNuocSX SET TenNuocSX=N'" + ten + "' WHERE MaNuocSX='" + ma + "'");
            else
                DatabaseHelper.RunSql("INSERT INTO tblNuocSX(MaNuocSX, TenNuocSX) VALUES('" + ma + "', N'" + ten + "')");

            LoadDataNuocSX();
            btnThem6.Enabled = true;
        }

        private void btnSua6_Click(object sender, EventArgs e)
        {
            btnLuu6.Enabled = true;
            btnBoQua6.Enabled = true;
            btnThem6.Enabled = false;
        }

        private void btnXoa6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNuocSX.Text)) return;
            if (MessageBox.Show("Bạn có muốn xóa nước sản xuất này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DatabaseHelper.RunSql("DELETE FROM tblNuocSX WHERE MaNuocSX='" + txtMaNuocSX.Text.Trim() + "'");
                LoadDataNuocSX();
                txtMaNuocSX.Text = "";
                txtTenNuocSX.Text = "";
            }
        }

        private void btnBoQua6_Click(object sender, EventArgs e)
        {
            txtMaNuocSX.Text = "";
            txtTenNuocSX.Text = "";
            btnThem6.Enabled = true;
            btnLuu6.Enabled = false;
            btnBoQua6.Enabled = false;
        }
        #endregion

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
