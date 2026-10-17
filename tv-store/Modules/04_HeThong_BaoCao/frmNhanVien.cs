using System;
using System.ComponentModel;
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

        private void SetEmployeePassword()
        {
            if (string.IsNullOrWhiteSpace(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên đã lưu."); return;
            }
            using (var dialog = new Form { Text = "Mật khẩu cho " + txtMaNV.Text, Width = 420, Height = 180, StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false, MinimizeBox = false })
            {
                var input = new TextBox { Left = 20, Top = 35, Width = 360, UseSystemPasswordChar = true };
                var label = new Label { Text = "Nhập mật khẩu mới (ít nhất 6 ký tự)", Left = 20, Top = 12, Width = 360 };
                var confirm = new Button { Text = "Lưu", Left = 210, Top = 80, Width = 80 };
                var cancel = new Button { Text = "Hủy", Left = 300, Top = 80, Width = 80, DialogResult = DialogResult.Cancel };
                dialog.Controls.AddRange(new Control[] { label, input, confirm, cancel });
                dialog.AcceptButton = confirm; dialog.CancelButton = cancel;
                confirm.Click += (sender, args) =>
                {
                    if (input.Text.Length < 6) { MessageBox.Show("Mật khẩu cần ít nhất 6 ký tự."); return; }
                    try
                    {
                        DatabaseHelper.Execute(
                            "UPDATE tblNhanVien SET Passw=@password WHERE MaNV=@id; IF @@ROWCOUNT=0 THROW 50005,N'Hãy lưu nhân viên trước khi đặt mật khẩu.',1;",
                            DatabaseHelper.P("@password", input.Text), DatabaseHelper.P("@id", txtMaNV.Text.Trim()));
                        dialog.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message, "Không thể đặt mật khẩu"); }
                };
                dialog.ShowDialog(this);
            }
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            if (!grpThongTin.Controls.ContainsKey("btnEmployeePassword"))
            {
                grpThongTin.Height += 38;
                var passwordButton = new Button { Name = "btnEmployeePassword", Text = "Đặt mật khẩu đăng nhập", Left = 590, Top = 150, Width = 240, Height = 28 };
                grpThongTin.Controls.Add(passwordButton);
                passwordButton.Click += (buttonSender, args) => SetEmployeePassword();
            }
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
            string maNV = txtMaNV.Text.Trim();

            // Nhân viên có hóa đơn hoặc tài khoản phải được giữ để không mất liên kết dữ liệu.
            bool hasHDB = DatabaseHelper.CheckKey("SELECT TOP 1 MaNV FROM tblHoaDonBan WHERE MaNV = '" + maNV + "'");
            bool hasHDN = DatabaseHelper.CheckKey("SELECT TOP 1 MaNV FROM tblHoaDonNhap WHERE MaNV = '" + maNV + "'");
            bool hasTK = DatabaseHelper.CheckKey("SELECT TOP 1 MaNV FROM tblTaiKhoan WHERE MaNV = '" + maNV + "'");

            if (hasHDB || hasHDN || hasTK)
            {
                MessageBox.Show(
                    "Không thể xóa Nhân viên này vì đã có dữ liệu liên quan (Hóa đơn Bán, Hóa đơn Nhập hoặc Tài khoản đăng nhập)!\n" +
                    "Để bảo toàn tính toàn vẹn dữ liệu hệ thống, vui lòng không xóa nhân viên này.",
                    "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có muốn xóa Nhân viên này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (DatabaseHelper.RunSql("DELETE FROM tblNhanVien WHERE MaNV='" + maNV + "'"))
                {
                    MessageBox.Show("Xóa Nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
