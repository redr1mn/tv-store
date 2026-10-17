using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using tv_store.Helpers;

namespace tv_store.Modules._03_BanHang
{
    public partial class frmTimKiemHDB : Form
    {
        private DataTable tblKetQua;

        public frmTimKiemHDB()
        {
            InitializeComponent();
        }

        private void frmTimKiemHDB_Load(object sender, EventArgs e)
        {
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadComboBoxes();
            dtpTuNgay.Value = DateTime.Now.AddMonths(-1);
            dtpDenNgay.Value = DateTime.Now;
            ThucHienTimKiem();
        }

        private void LoadComboBoxes()
        {
            DatabaseHelper.FillCombo("SELECT MaKhach, TenKhach FROM tblKhachHang", cboKhachHang, "MaKhach", "TenKhach");
            DatabaseHelper.FillCombo("SELECT MaNV, TenNV FROM tblNhanVien", cboNhanVien, "MaNV", "TenNV");
            DatabaseHelper.FillCombo("SELECT MaTV, TenTV FROM tblTV", cboMaTivi, "MaTV", "TenTV");
        }

        private void chkLocNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpTuNgay.Enabled = chkLocNgay.Checked;
            dtpDenNgay.Enabled = chkLocNgay.Checked;
        }

        private void ThucHienTimKiem()
        {
            var parameters = new List<SqlParameter>();
            string sql = "SELECT hdb.SoHDB, hdb.NgayBan, kh.TenKhach, kh.DienThoai AS SDTKhach, nv.TenNV, " +
                         "ct.MaTV, tv.TenTV, ct.SoLuong, ct.DonGia, ct.GiamGia, ct.ThanhTien, " +
                         "hdb.Thue, hdb.TongTien " +
                         "FROM tblChiTietHDB ct " +
                         "INNER JOIN tblHoaDonBan hdb ON ct.SoHDB = hdb.SoHDB " +
                         "INNER JOIN tblTV tv ON ct.MaTV = tv.MaTV " +
                         "INNER JOIN tblKhachHang kh ON hdb.MaKhach = kh.MaKhach " +
                         "INNER JOIN tblNhanVien nv ON hdb.MaNV = nv.MaNV " +
                         "WHERE 1=1 ";

            if (!string.IsNullOrWhiteSpace(txtMaHDB.Text))
            {
                sql += " AND hdb.SoHDB LIKE @code ";
                parameters.Add(DatabaseHelper.P("@code", "%" + txtMaHDB.Text.Trim() + "%"));
            }

            if (cboKhachHang.SelectedValue != null && !string.IsNullOrEmpty(cboKhachHang.SelectedValue.ToString()))
            {
                sql += " AND hdb.MaKhach = @customer ";
                parameters.Add(DatabaseHelper.P("@customer", cboKhachHang.SelectedValue));
            }

            if (cboNhanVien.SelectedValue != null && !string.IsNullOrEmpty(cboNhanVien.SelectedValue.ToString()))
            {
                sql += " AND hdb.MaNV = @employee ";
                parameters.Add(DatabaseHelper.P("@employee", cboNhanVien.SelectedValue));
            }

            if (cboMaTivi.SelectedValue != null && !string.IsNullOrEmpty(cboMaTivi.SelectedValue.ToString()))
            {
                sql += " AND ct.MaTV = @product ";
                parameters.Add(DatabaseHelper.P("@product", cboMaTivi.SelectedValue));
            }

            if (chkLocNgay.Checked)
            {
                if (dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
                {
                    MessageBox.Show("Ngày bắt đầu phải trước hoặc bằng ngày kết thúc."); return;
                }
                sql += " AND hdb.NgayBan >= @start AND hdb.NgayBan < @end ";
                parameters.Add(DatabaseHelper.P("@start", dtpTuNgay.Value.Date));
                parameters.Add(DatabaseHelper.P("@end", dtpDenNgay.Value.Date.AddDays(1)));
            }

            sql += " ORDER BY hdb.NgayBan DESC, hdb.SoHDB DESC";

            try { tblKetQua = DatabaseHelper.Query(sql, parameters.ToArray()); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Không thể tra cứu hóa đơn"); return; }
            dgvKetQuaHDB.DataSource = tblKetQua;

            if (dgvKetQuaHDB.Columns.Count > 0)
            {
                dgvKetQuaHDB.Columns["SoHDB"].HeaderText = "Mã HĐB";
                dgvKetQuaHDB.Columns["NgayBan"].HeaderText = "Ngày bán";
                dgvKetQuaHDB.Columns["TenKhach"].HeaderText = "Khách hàng";
                dgvKetQuaHDB.Columns["SDTKhach"].HeaderText = "Điện thoại";
                dgvKetQuaHDB.Columns["TenNV"].HeaderText = "Nhân viên";
                dgvKetQuaHDB.Columns["MaTV"].HeaderText = "Mã Tivi";
                dgvKetQuaHDB.Columns["TenTV"].HeaderText = "Tên Tivi";
                dgvKetQuaHDB.Columns["SoLuong"].HeaderText = "SL";
                dgvKetQuaHDB.Columns["DonGia"].HeaderText = "Đơn giá";
                dgvKetQuaHDB.Columns["GiamGia"].HeaderText = "CK (%)";
                dgvKetQuaHDB.Columns["ThanhTien"].HeaderText = "Thành tiền";
                dgvKetQuaHDB.Columns["Thue"].HeaderText = "VAT (%)";
                dgvKetQuaHDB.Columns["TongTien"].HeaderText = "Tổng HĐ";

                dgvKetQuaHDB.Columns["NgayBan"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvKetQuaHDB.Columns["DonGia"].DefaultCellStyle.Format = "#,##0";
                dgvKetQuaHDB.Columns["ThanhTien"].DefaultCellStyle.Format = "#,##0";
                dgvKetQuaHDB.Columns["TongTien"].DefaultCellStyle.Format = "#,##0";

                dgvKetQuaHDB.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvKetQuaHDB.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvKetQuaHDB.Columns["GiamGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvKetQuaHDB.Columns["ThanhTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvKetQuaHDB.Columns["Thue"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvKetQuaHDB.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvKetQuaHDB.Columns["SoHDB"].Width = 115;
                dgvKetQuaHDB.Columns["NgayBan"].Width = 135;
                dgvKetQuaHDB.Columns["TenKhach"].Width = 180;
                dgvKetQuaHDB.Columns["SDTKhach"].Width = 100;
                dgvKetQuaHDB.Columns["TenNV"].Width = 140;
                dgvKetQuaHDB.Columns["MaTV"].Width = 75;
                dgvKetQuaHDB.Columns["TenTV"].Width = 240;
                dgvKetQuaHDB.Columns["SoLuong"].Width = 60;
                dgvKetQuaHDB.Columns["DonGia"].Width = 110;
                dgvKetQuaHDB.Columns["GiamGia"].Width = 65;
                dgvKetQuaHDB.Columns["ThanhTien"].Width = 115;
                dgvKetQuaHDB.Columns["Thue"].Width = 70;
                dgvKetQuaHDB.Columns["TongTien"].Width = 120;
            }

            // Kết quả có nhiều dòng cho một hóa đơn; chỉ cộng tổng tiền hóa đơn một lần.
            System.Collections.Generic.HashSet<string> distinctHDB = new System.Collections.Generic.HashSet<string>();
            double tongTienDoanhThu = 0;

            foreach (DataRow r in tblKetQua.Rows)
            {
                string soHDB = r["SoHDB"].ToString();
                if (!distinctHDB.Contains(soHDB))
                {
                    distinctHDB.Add(soHDB);
                    tongTienDoanhThu += Convert.ToDouble(r["TongTien"]);
                }
            }

            lblSoLuongKetQua.Text = string.Format("Tìm thấy: {0} hóa đơn ({1} dòng mặt hàng bán phù hợp)", distinctHDB.Count, tblKetQua.Rows.Count);
            lblTongTien.Text = string.Format("Tổng doanh thu: {0}", DatabaseHelper.FormatTien(tongTienDoanhThu));
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            ThucHienTimKiem();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaHDB.Text = "";
            cboKhachHang.SelectedIndex = -1;
            cboNhanVien.SelectedIndex = -1;
            cboMaTivi.SelectedIndex = -1;
            chkLocNgay.Checked = false;
            dtpTuNgay.Value = DateTime.Now.AddMonths(-1);
            dtpDenNgay.Value = DateTime.Now;
            ThucHienTimKiem();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            ExcelExportHelper.ExportDataGridViewToExcel(dgvKetQuaHDB, "KẾT QUẢ TRA CỨU HÓA ĐƠN BÁN", "TraCuuHoaDonBan", "KetQuaTraCuu");
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvKetQuaHDB_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || tblKetQua.Rows.Count == 0) return;

            string id = dgvKetQuaHDB.Rows[e.RowIndex].Cells["SoHDB"].Value.ToString();
            using (var invoice = new frmHoaDonBan())
            {
                invoice.Shown += (s, args) => { try { invoice.OpenInvoice(id); } catch (Exception ex) { MessageBox.Show(ex.Message); } };
                invoice.ShowDialog(this);
            }
            ThucHienTimKiem();
        }
    }
}
