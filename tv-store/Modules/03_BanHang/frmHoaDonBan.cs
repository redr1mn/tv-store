using System;
using System.Data;
using System.Windows.Forms;
using tv_store.Helpers;

namespace tv_store.Modules._03_BanHang
{
    public partial class frmHoaDonBan : Form
    {
        private DataTable tblChiTietHDBData;

        public frmHoaDonBan()
        {
            InitializeComponent();
        }

        private void frmHoaDonBan_Load(object sender, EventArgs e)
        {
            btnLuuHDB.Enabled = false;
            btnHuyHDB.Enabled = false;
            btnInHDB.Enabled = false;
            btnXuatExcel.Enabled = false;
            LoadComboBoxes();
            ResetForm();
        }

        private void LoadComboBoxes()
        {
            DatabaseHelper.FillCombo("SELECT MaNV, TenNV FROM tblNhanVien", cboNhanVien, "MaNV", "TenNV");
            DatabaseHelper.FillCombo("SELECT MaKhach, TenKhach FROM tblKhachHang", cboKhachHang, "MaKhach", "TenKhach");
            DatabaseHelper.FillCombo("SELECT MaTV, TenTV FROM tblTV", cboMaTivi, "MaTV", "TenTV");
        }

        private void ResetForm()
        {
            txtMaHDB.Text = "";
            dtpNgayBan.Value = DateTime.Now;
            cboNhanVien.SelectedIndex = -1;
            txtTenNV.Text = "";
            cboKhachHang.SelectedIndex = -1;
            txtTenKH.Text = "";
            txtDiaChiKH.Text = "";
            txtDienThoaiKH.Text = "";

            cboMaTivi.SelectedIndex = -1;
            txtTenTivi.Text = "";
            txtTonKho.Text = "0";
            txtSoLuongBan.Text = "1";
            txtDonGiaBan.Text = "0";
            txtGiamGia.Text = "0";
            txtThanhTien.Text = "0";

            lblTongTien.Text = "0 VNĐ";
            lblBangChu.Text = "Bằng chữ: Không đồng.";

            CreateTempDataTable();
        }

        private void CreateTempDataTable()
        {
            tblChiTietHDBData = new DataTable();
            tblChiTietHDBData.Columns.Add("MaTV", typeof(string));
            tblChiTietHDBData.Columns.Add("TenTV", typeof(string));
            tblChiTietHDBData.Columns.Add("SoLuong", typeof(int));
            tblChiTietHDBData.Columns.Add("DonGia", typeof(double));
            tblChiTietHDBData.Columns.Add("GiamGia", typeof(double));
            tblChiTietHDBData.Columns.Add("ThanhTien", typeof(double));
            dgvChiTietHDB.DataSource = tblChiTietHDBData;

            if (dgvChiTietHDB.Columns.Count > 0)
            {
                dgvChiTietHDB.Columns["MaTV"].HeaderText = "Mã Tivi";
                dgvChiTietHDB.Columns["TenTV"].HeaderText = "Tên Tivi";
                dgvChiTietHDB.Columns["SoLuong"].HeaderText = "Số lượng mua";
                dgvChiTietHDB.Columns["DonGia"].HeaderText = "Đơn giá bán";
                dgvChiTietHDB.Columns["GiamGia"].HeaderText = "Giảm giá (%)";
                dgvChiTietHDB.Columns["ThanhTien"].HeaderText = "Thành tiền";

                dgvChiTietHDB.Columns["DonGia"].DefaultCellStyle.Format = "#,##0";
                dgvChiTietHDB.Columns["ThanhTien"].DefaultCellStyle.Format = "#,##0";
            }
        }

        private void cboNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNhanVien.SelectedValue != null)
            {
                txtTenNV.Text = DatabaseHelper.GetFieldValue("SELECT TenNV FROM tblNhanVien WHERE MaNV='" + cboNhanVien.SelectedValue.ToString() + "'");
            }
        }

        private void cboKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboKhachHang.SelectedValue != null)
            {
                string ma = cboKhachHang.SelectedValue.ToString();
                DataTable dt = DatabaseHelper.GetDataToTable("SELECT TenKhach, DiaChi, DienThoai FROM tblKhachHang WHERE MaKhach='" + ma + "'");
                if (dt.Rows.Count > 0)
                {
                    txtTenKH.Text = dt.Rows[0]["TenKhach"].ToString();
                    txtDiaChiKH.Text = dt.Rows[0]["DiaChi"].ToString();
                    txtDienThoaiKH.Text = dt.Rows[0]["DienThoai"].ToString();
                }
            }
        }

        private void cboMaTivi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaTivi.SelectedValue != null)
            {
                string ma = cboMaTivi.SelectedValue.ToString();
                DataTable dt = DatabaseHelper.GetDataToTable("SELECT TenTV, DonGiaBan, SoLuong FROM tblTV WHERE MaTV='" + ma + "'");
                if (dt.Rows.Count > 0)
                {
                    txtTenTivi.Text = dt.Rows[0]["TenTV"].ToString();
                    txtDonGiaBan.Text = dt.Rows[0]["DonGiaBan"].ToString();
                    txtTonKho.Text = dt.Rows[0]["SoLuong"].ToString();
                    TinhThanhTien(null, null);
                }
            }
        }

        private void TinhThanhTien(object sender, EventArgs e)
        {
            double.TryParse(txtDonGiaBan.Text.Trim(), out double gia);
            int.TryParse(txtSoLuongBan.Text.Trim(), out int sl);
            double.TryParse(txtGiamGia.Text.Trim(), out double gg);

            double thanhTien = sl * gia * (1 - gg / 100.0);
            txtThanhTien.Text = thanhTien.ToString();
        }

        private void btnThemChiTiet_Click(object sender, EventArgs e)
        {
            if (cboMaTivi.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Tivi cần bán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaTivi.Focus();
                return;
            }

            int.TryParse(txtSoLuongBan.Text.Trim(), out int sl);
            int.TryParse(txtTonKho.Text.Trim(), out int tonKho);

            if (sl <= 0)
            {
                MessageBox.Show("Số lượng mua phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuongBan.Focus();
                return;
            }

            // Nghiệp vụ yêu cầu 1: Kiểm tra tồn kho trước khi bán
            if (sl > tonKho)
            {
                MessageBox.Show(string.Format("Số lượng tồn kho không đủ để bán!\nHiện trong kho chỉ còn {0} chiếc.", tonKho),
                    "Cảnh báo hết hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuongBan.Focus();
                return;
            }

            double.TryParse(txtDonGiaBan.Text.Trim(), out double gia);
            double.TryParse(txtGiamGia.Text.Trim(), out double gg);
            double.TryParse(txtThanhTien.Text.Trim(), out double tt);

            string maTivi = cboMaTivi.SelectedValue.ToString();
            string tenTivi = txtTenTivi.Text.Trim();

            // Kiểm tra mặt hàng đã có trong chi tiết chưa
            foreach (DataRow r in tblChiTietHDBData.Rows)
            {
                if (r["MaTV"].ToString() == maTivi)
                {
                    int oldSl = Convert.ToInt32(r["SoLuong"]);
                    if (oldSl + sl > tonKho)
                    {
                        MessageBox.Show(string.Format("Tổng số lượng đặt mua ({0}) vượt quá tồn kho ({1})!", oldSl + sl, tonKho),
                            "Cảnh báo hết hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    r["SoLuong"] = oldSl + sl;
                    r["DonGia"] = gia;
                    r["GiamGia"] = gg;
                    r["ThanhTien"] = (oldSl + sl) * gia * (1 - gg / 100.0);
                    CapNhatTongTien();
                    return;
                }
            }

            tblChiTietHDBData.Rows.Add(maTivi, tenTivi, sl, gia, gg, tt);
            CapNhatTongTien();
            btnLuuHDB.Enabled = true;
        }

        private void CapNhatTongTien()
        {
            double tongTien = 0;
            foreach (DataRow r in tblChiTietHDBData.Rows)
            {
                tongTien += Convert.ToDouble(r["ThanhTien"]);
            }

            lblTongTien.Text = DatabaseHelper.FormatTien(tongTien);
            lblBangChu.Text = "Bằng chữ: " + DatabaseHelper.ChuyenSoSangChu(tongTien);
        }

        private void dgvChiTietHDB_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || tblChiTietHDBData.Rows.Count == 0) return;

            if (MessageBox.Show("Bạn có muốn xóa mặt hàng này khỏi hóa đơn bán?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                tblChiTietHDBData.Rows.RemoveAt(e.RowIndex);
                CapNhatTongTien();
            }
        }

        private void btnThemHDB_Click(object sender, EventArgs e)
        {
            ResetForm();
            txtMaHDB.Text = "HDB" + DateTime.Now.ToString("yyMMddHHmmss");
            btnThemHDB.Enabled = false;
            btnLuuHDB.Enabled = true;
            btnHuyHDB.Enabled = true;
        }

        private void btnLuuHDB_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaHDB.Text))
            {
                txtMaHDB.Text = "HDB" + DateTime.Now.ToString("yyMMddHHmmss");
            }

            if (cboNhanVien.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Nhân viên lập hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhanVien.Focus();
                return;
            }

            if (cboKhachHang.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhachHang.Focus();
                return;
            }

            if (tblChiTietHDBData.Rows.Count == 0)
            {
                MessageBox.Show("Hóa đơn bán phải có ít nhất 1 mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maHDB = txtMaHDB.Text.Trim();
            string maNV = cboNhanVien.SelectedValue.ToString();
            string maKH = cboKhachHang.SelectedValue.ToString();
            string ngayBan = dtpNgayBan.Value.ToString("yyyy-MM-dd HH:mm:ss");

            double tongTien = 0;
            foreach (DataRow r in tblChiTietHDBData.Rows)
                tongTien += Convert.ToDouble(r["ThanhTien"]);

            // 1. Thêm Hóa đơn bán
            string sqlHDB = string.Format(
                "INSERT INTO tblHoaDonBan(SoHDB, MaNV, NgayBan, MaKhach, Thue, TongTien) VALUES('{0}', '{1}', '{2}', '{3}', {4}, {5})",
                maHDB, maNV, ngayBan, maKH, tongTien);

            if (DatabaseHelper.RunSql(sqlHDB))
            {
                // 2. Thêm Chi tiết HDB (Trigger SQL Server sẽ tự động trừ số lượng tồn kho trong tblTivi)
                foreach (DataRow r in tblChiTietHDBData.Rows)
                {
                    string sqlCT = string.Format(
                        "INSERT INTO tblChiTietHDB(MaHDB, MaTivi, SoLuong, DonGia, GiamGia, ThanhTien) " +
                        "VALUES('{0}', '{1}', {2}, {3}, {4}, {5})",
                        maHDB, r["MaTV"].ToString(), r["SoLuong"], r["DonGia"], r["GiamGia"], r["ThanhTien"]);
                    DatabaseHelper.RunSql(sqlCT);
                }

                MessageBox.Show("Lập Hóa đơn bán thành công!\nTồn kho Tivi đã được tự động trừ tương ứng.", 
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnInHDB.Enabled = true;
                btnXuatExcel.Enabled = true;
                btnThemHDB.Enabled = true;
                btnLuuHDB.Enabled = false;
            }
        }

        private void btnHuyHDB_Click(object sender, EventArgs e)
        {
            ResetForm();
            btnThemHDB.Enabled = true;
            btnLuuHDB.Enabled = false;
            btnHuyHDB.Enabled = false;
            btnInHDB.Enabled = false;
            btnXuatExcel.Enabled = false;
        }

        private void btnInHDB_Click(object sender, EventArgs e)
        {
            ExcelExportHelper.ExportToCSV(dgvChiTietHDB, "HoaDonBan_" + txtMaHDB.Text.Trim());
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            ExcelExportHelper.ExportToCSV(dgvChiTietHDB, "HoaDonBan_" + txtMaHDB.Text.Trim());
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
