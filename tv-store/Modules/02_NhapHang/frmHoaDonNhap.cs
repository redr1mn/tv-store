using System;
using System.Data;
using System.Windows.Forms;
using tv_store.Helpers;

namespace tv_store.Modules._02_NhapHang
{
    public partial class frmHoaDonNhap : Form
    {
        private DataTable tblChiTietHDNData;

        public frmHoaDonNhap()
        {
            InitializeComponent();
        }

        private void frmHoaDonNhap_Load(object sender, EventArgs e)
        {
            btnLuuHD.Enabled = false;
            btnHuyHD.Enabled = false;
            btnInHD.Enabled = false;
            LoadComboBoxes();
            ResetForm();
        }

        private void LoadComboBoxes()
        {
            DatabaseHelper.FillCombo("SELECT MaNV, TenNV FROM tblNhanVien", cboNhanVien, "MaNV", "TenNV");
            DatabaseHelper.FillCombo("SELECT MaNCC, TenNCC FROM tblNhaCungCap", cboNCC, "MaNCC", "TenNCC");
            DatabaseHelper.FillCombo("SELECT MaTV, TenTV FROM tblTV", cboMaTivi, "MaTV", "TenTV");
        }

        private void ResetForm()
        {
            txtMaHDN.Text = "";
            dtpNgayNhap.Value = DateTime.Now;
            cboNhanVien.SelectedIndex = -1;
            txtTenNV.Text = "";
            cboNCC.SelectedIndex = -1;
            txtTenNCC.Text = "";
            txtDiaChiNCC.Text = "";
            txtDienThoaiNCC.Text = "";

            cboMaTivi.SelectedIndex = -1;
            txtTenTivi.Text = "";
            txtSoLuong.Text = "1";
            txtDonGiaNhap.Text = "0";
            txtGiamGia.Text = "0";
            txtThanhTien.Text = "0";

            lblTongTien.Text = "0 VNĐ";
            lblBangChu.Text = "Bằng chữ: Không đồng.";

            CreateTempDataTable();
        }

        private void CreateTempDataTable()
        {
            tblChiTietHDNData = new DataTable();
            tblChiTietHDNData.Columns.Add("MaTV", typeof(string));
            tblChiTietHDNData.Columns.Add("TenTV", typeof(string));
            tblChiTietHDNData.Columns.Add("SoLuong", typeof(int));
            tblChiTietHDNData.Columns.Add("DonGia", typeof(double));
            tblChiTietHDNData.Columns.Add("GiamGia", typeof(double));
            tblChiTietHDNData.Columns.Add("ThanhTien", typeof(double));
            dgvChiTietHDN.DataSource = tblChiTietHDNData;

            if (dgvChiTietHDN.Columns.Count > 0)
            {
                dgvChiTietHDN.Columns["MaTV"].HeaderText = "Mã Tivi";
                dgvChiTietHDN.Columns["TenTV"].HeaderText = "Tên Tivi";
                dgvChiTietHDN.Columns["SoLuong"].HeaderText = "Số lượng";
                dgvChiTietHDN.Columns["DonGia"].HeaderText = "Đơn giá nhập";
                dgvChiTietHDN.Columns["GiamGia"].HeaderText = "Giảm giá (%)";
                dgvChiTietHDN.Columns["ThanhTien"].HeaderText = "Thành tiền";

                dgvChiTietHDN.Columns["DonGia"].DefaultCellStyle.Format = "#,##0";
                dgvChiTietHDN.Columns["ThanhTien"].DefaultCellStyle.Format = "#,##0";
            }
        }

        private void cboNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNhanVien.SelectedValue != null)
            {
                txtTenNV.Text = DatabaseHelper.GetFieldValue("SELECT TenNV FROM tblNhanVien WHERE MaNV='" + cboNhanVien.SelectedValue.ToString() + "'");
            }
        }

        private void cboNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNCC.SelectedValue != null)
            {
                string ma = cboNCC.SelectedValue.ToString();
                DataTable dt = DatabaseHelper.GetDataToTable("SELECT TenNCC, DiaChi, DienThoai FROM tblNhaCungCap WHERE MaNCC='" + ma + "'");
                if (dt.Rows.Count > 0)
                {
                    txtTenNCC.Text = dt.Rows[0]["TenNCC"].ToString();
                    txtDiaChiNCC.Text = dt.Rows[0]["DiaChi"].ToString();
                    txtDienThoaiNCC.Text = dt.Rows[0]["DienThoai"].ToString();
                }
            }
        }

        private void cboMaTivi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaTivi.SelectedValue != null)
            {
                string ma = cboMaTivi.SelectedValue.ToString();
                DataTable dt = DatabaseHelper.GetDataToTable("SELECT TenTV, DonGiaNhap FROM tblTV WHERE MaTV='" + ma + "'");
                if (dt.Rows.Count > 0)
                {
                    txtTenTivi.Text = dt.Rows[0]["TenTV"].ToString();
                    txtDonGiaNhap.Text = dt.Rows[0]["DonGiaNhap"].ToString();
                    TinhThanhTien(null, null);
                }
            }
        }

        private void TinhThanhTien(object sender, EventArgs e)
        {
            double.TryParse(txtDonGiaNhap.Text.Trim(), out double gia);
            int.TryParse(txtSoLuong.Text.Trim(), out int sl);
            double.TryParse(txtGiamGia.Text.Trim(), out double gg);

            double thanhTien = sl * gia * (1 - gg / 100.0);
            txtThanhTien.Text = thanhTien.ToString();
        }

        private void btnThemChiTiet_Click(object sender, EventArgs e)
        {
            if (cboMaTivi.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Tivi cần nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaTivi.Focus();
                return;
            }

            int.TryParse(txtSoLuong.Text.Trim(), out int sl);
            if (sl <= 0)
            {
                MessageBox.Show("Số lượng nhập phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return;
            }

            double.TryParse(txtDonGiaNhap.Text.Trim(), out double gia);
            double.TryParse(txtGiamGia.Text.Trim(), out double gg);
            double.TryParse(txtThanhTien.Text.Trim(), out double tt);

            string maTivi = cboMaTivi.SelectedValue.ToString();
            string tenTivi = txtTenTivi.Text.Trim();

            // Kiểm tra nếu sản phẩm đã có trong danh sách chi tiết
            foreach (DataRow r in tblChiTietHDNData.Rows)
            {
                if (r["MaTV"].ToString() == maTivi)
                {
                    int oldSl = Convert.ToInt32(r["SoLuong"]);
                    r["SoLuong"] = oldSl + sl;
                    r["DonGia"] = gia;
                    r["GiamGia"] = gg;
                    r["ThanhTien"] = (oldSl + sl) * gia * (1 - gg / 100.0);
                    CapNhatTongTien();
                    return;
                }
            }

            tblChiTietHDNData.Rows.Add(maTivi, tenTivi, sl, gia, gg, tt);
            CapNhatTongTien();
            btnLuuHD.Enabled = true;
        }

        private void CapNhatTongTien()
        {
            double tongTien = 0;
            foreach (DataRow r in tblChiTietHDNData.Rows)
            {
                tongTien += Convert.ToDouble(r["ThanhTien"]);
            }

            lblTongTien.Text = DatabaseHelper.FormatTien(tongTien);
            lblBangChu.Text = "Bằng chữ: " + DatabaseHelper.ChuyenSoSangChu(tongTien);
        }

        private void dgvChiTietHDN_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || tblChiTietHDNData.Rows.Count == 0) return;

            if (MessageBox.Show("Bạn có muốn xóa mặt hàng này khỏi hóa đơn nhập?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                tblChiTietHDNData.Rows.RemoveAt(e.RowIndex);
                CapNhatTongTien();
            }
        }

        private void btnThemHD_Click(object sender, EventArgs e)
        {
            ResetForm();
            txtMaHDN.Text = "HDN" + DateTime.Now.ToString("yyMMddHHmmss");
            btnThemHD.Enabled = false;
            btnLuuHD.Enabled = true;
            btnHuyHD.Enabled = true;
        }

        private void btnLuuHD_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaHDN.Text))
            {
                txtMaHDN.Text = "HDN" + DateTime.Now.ToString("yyMMddHHmmss");
            }

            if (cboNhanVien.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Nhân viên lập hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhanVien.Focus();
                return;
            }

            if (cboNCC.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNCC.Focus();
                return;
            }

            if (tblChiTietHDNData.Rows.Count == 0)
            {
                MessageBox.Show("Hóa đơn nhập phải có ít nhất 1 mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maHDN = txtMaHDN.Text.Trim();
            string maNV = cboNhanVien.SelectedValue.ToString();
            string maNCC = cboNCC.SelectedValue.ToString();
            string ngayNhap = dtpNgayNhap.Value.ToString("yyyy-MM-dd HH:mm:ss");

            double tongTien = 0;
            foreach (DataRow r in tblChiTietHDNData.Rows)
                tongTien += Convert.ToDouble(r["ThanhTien"]);

            // 1. Thêm Hóa đơn nhập
            string sqlHDN = string.Format(
                "INSERT INTO tblHoaDonNhap(SoHDN, MaNV, NgayNhap, MaNCC, TongTien) VALUES('{0}', '{1}', '{2}', '{3}', {4})",
                maHDN, maNV, ngayNhap, maNCC, tongTien);
            
            if (DatabaseHelper.RunSql(sqlHDN))
            {
                // 2. Thêm Chi tiết HDN (Trigger SQL Server sẽ tự động cập nhật số lượng tồn kho và cập nhật giá bán = 1.1 * giá nhập)
                foreach (DataRow r in tblChiTietHDNData.Rows)
                {
                    string sqlCT = string.Format(
                        "INSERT INTO tblChiTietHDN(SoHDN, MaTV, SoLuong, DonGia, GiamGia, ThanhTien) " +
                        "VALUES('{0}', '{1}', {2}, {3}, {4}, {5})",
                        maHDN, r["MaTV"].ToString(), r["SoLuong"], r["DonGia"], r["GiamGia"], r["ThanhTien"]);
                    DatabaseHelper.RunSql(sqlCT);
                }

                MessageBox.Show("Lưu Hóa đơn nhập thành công!\nSố lượng tồn và đơn giá Tivi đã được cập nhật tự động.", 
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnInHD.Enabled = true;
                btnThemHD.Enabled = true;
                btnLuuHD.Enabled = false;
            }
        }

        private void btnHuyHD_Click(object sender, EventArgs e)
        {
            ResetForm();
            btnThemHD.Enabled = true;
            btnLuuHD.Enabled = false;
            btnHuyHD.Enabled = false;
            btnInHD.Enabled = false;
        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            ExcelExportHelper.ExportToCSV(dgvChiTietHDN, "HoaDonNhap_" + txtMaHDN.Text.Trim());
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            frmTimKiemHDN frm = new frmTimKiemHDN();
            frm.ShowDialog();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
