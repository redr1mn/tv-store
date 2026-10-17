using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using tv_store.Helpers;
using tv_store.Modules._04_HeThong_BaoCao;

namespace tv_store.Modules._02_NhapHang
{
    public partial class frmHoaDonNhap : Form
    {
        private DataTable lines;
        private string savedCode;
        private bool loading;
        private bool runtimeInitialized;

        public frmHoaDonNhap()
        {
            InitializeComponent();
        }

        private void InitializeRuntime()
        {
            if (runtimeInitialized || DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            runtimeInitialized = true;
            txtMaHDN.Width -= 85;
            var search = new Button { Text = "Tìm HĐ", Left = txtMaHDN.Right + 5, Top = txtMaHDN.Top - 1, Width = 80, Height = txtMaHDN.Height + 2 };
            txtMaHDN.Parent.Controls.Add(search);
            search.Click += (s, e) => Search();
            txtMaHDN.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; Search(); } };
            txtMaHDN.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtMaHDN.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboNhanVien.Enabled = false;

            FormClosing += (s, e) => { if (!CanDiscard()) e.Cancel = true; };

            LoadInvoiceData();
        }

        private void Safe(Action action)
        {
            try { action(); }
            catch (Exception ex) { MessageBox.Show(this, ex.Message, "Không thể thực hiện", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private static void Bind(ComboBox combo, DataTable data, string value, string display)
        {
            combo.DisplayMember = display; combo.ValueMember = value; combo.DataSource = data; combo.SelectedIndex = -1;
        }

        private void LoadInvoiceData()
        {
            Safe(() =>
            {
                loading = true;
                try
                {
                    Bind(cboNhanVien, DatabaseHelper.Query("SELECT MaNV,TenNV FROM tblNhanVien"), "MaNV", "MaNV");
                    Bind(cboNCC, DatabaseHelper.Query("SELECT MaNCC,TenNCC FROM tblNhaCungCap"), "MaNCC", "MaNCC");
                    Bind(cboMaTivi, DatabaseHelper.Query("SELECT MaTV,TenTV FROM tblTV"), "MaTV", "MaTV");
                    RefreshCodes();
                }
                finally { loading = false; }
                Reset();
            });
        }

        private void RefreshCodes()
        {
            var data = DatabaseHelper.Query("SELECT SoHDN FROM tblHoaDonNhap ORDER BY NgayNhap DESC");
            string text = txtMaHDN.Text;
            txtMaHDN.Items.Clear();
            foreach (DataRow row in data.Rows) txtMaHDN.Items.Add(row[0].ToString());
            txtMaHDN.Text = text;
        }

        private void Reset()
        {
            loading = true;
            savedCode = null;
            txtMaHDN.Text = "";
            dtpNgayNhap.Value = DateTime.Now;
            cboNhanVien.SelectedValue = frmDangNhap.CurrentEmployeeId ?? "";
            txtTenNV.Text = frmDangNhap.CurrentEmployeeName;
            cboNCC.SelectedIndex = cboMaTivi.SelectedIndex = -1;
            txtTenNCC.Clear(); txtDiaChiNCC.Clear(); txtDienThoaiNCC.Clear(); txtTenTivi.Clear();
            txtSoLuong.Text = "1"; txtDonGiaNhap.Text = txtGiamGia.Text = txtThanhTien.Text = "0";

            lines = new DataTable();
            lines.Columns.Add("MaTV", typeof(string)); lines.Columns.Add("TenTV", typeof(string));
            lines.Columns.Add("SoLuong", typeof(int)); lines.Columns.Add("DonGia", typeof(decimal));
            lines.Columns.Add("GiamGia", typeof(decimal)); lines.Columns.Add("ThanhTien", typeof(decimal));
            BindLines();
            loading = false;
            SetState(false);
            CalculateTotal();
        }

        private void BindLines()
        {
            dgvChiTietHDN.DataSource = lines;
            string[] titles = { "Mã Tivi", "Tên Tivi", "Số lượng", "Đơn giá nhập", "Giảm giá (%)", "Thành tiền" };
            for (int i = 0; i < titles.Length; i++) dgvChiTietHDN.Columns[i].HeaderText = titles[i];
            dgvChiTietHDN.Columns["TenTV"].FillWeight = 220;
            foreach (string name in new[] { "DonGia", "ThanhTien" }) dgvChiTietHDN.Columns[name].DefaultCellStyle.Format = "N2";
        }

        private void SetState(bool saved)
        {
            btnLuuHD.Enabled = !saved;
            btnHuyHD.Enabled = saved || lines.Rows.Count > 0 || txtMaHDN.Text.Length > 0;
            btnInHD.Enabled = saved && lines.Rows.Count > 0;

            btnThemChiTiet.Enabled = !saved;
            grpChiTiet.Enabled = !saved;
            dtpNgayNhap.Enabled = cboNCC.Enabled = !saved;
            txtDienThoaiNCC.ReadOnly = true;
            txtDienThoaiNCC.TabStop = false;
            txtTenNCC.ReadOnly = txtDiaChiNCC.ReadOnly = true;
            cboNhanVien.Enabled = false;
        }

        private bool CanDiscard()
        {
            return savedCode != null || lines == null || lines.Rows.Count == 0 ||
                MessageBox.Show(this, "Bỏ hóa đơn đang nhập chưa lưu?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private void NewInvoice()
        {
            Safe(() =>
            {
                if (!CanDiscard()) return;
                Reset();
                txtMaHDN.Text = InvoiceRepository.PreviewCode(false, dtpNgayNhap.Value);
                SetState(false);
            });
        }

        private void EmployeeChanged()
        {
            if (loading || cboNhanVien.SelectedValue == null) return;
            Safe(() =>
            {
                var data = DatabaseHelper.Query("SELECT TenNV FROM tblNhanVien WHERE MaNV=@id", DatabaseHelper.P("@id", cboNhanVien.SelectedValue));
                txtTenNV.Text = data.Rows.Count == 0 ? "" : data.Rows[0][0].ToString();
            });
        }

        private void PartnerChanged()
        {
            if (loading || cboNCC.SelectedValue == null) return;
            Safe(() =>
            {
                var data = DatabaseHelper.Query("SELECT TenNCC,DiaChi,DienThoai FROM tblNhaCungCap WHERE MaNCC=@id", DatabaseHelper.P("@id", cboNCC.SelectedValue));
                if (data.Rows.Count == 0) return;
                loading = true;
                try { txtTenNCC.Text = data.Rows[0][0].ToString(); txtDiaChiNCC.Text = data.Rows[0][1].ToString(); txtDienThoaiNCC.Text = data.Rows[0][2].ToString(); }
                finally { loading = false; }
                txtTenNCC.ReadOnly = txtDiaChiNCC.ReadOnly = true;
            });
        }

        private void ProductChanged()
        {
            if (loading || cboMaTivi.SelectedValue == null) return;
            Safe(() =>
            {
                var data = DatabaseHelper.Query("SELECT TenTV,DonGiaBan,DonGiaNhap,SoLuong FROM tblTV WHERE MaTV=@id", DatabaseHelper.P("@id", cboMaTivi.SelectedValue));
                if (data.Rows.Count == 0) return;
                txtTenTivi.Text = data.Rows[0]["TenTV"].ToString();
                txtDonGiaNhap.Text = data.Rows[0]["DonGiaNhap"].ToString();

                Calculate();
            });
        }

        private void Calculate()
        {
            if (loading) return;
            int count; decimal unitPrice, reduction;
            try
            {
                txtThanhTien.Text = int.TryParse(txtSoLuong.Text, out count) && decimal.TryParse(txtDonGiaNhap.Text, out unitPrice) && decimal.TryParse(txtGiamGia.Text, out reduction)
                    ? InvoiceRepository.LineTotal(count, unitPrice, reduction).ToString() : "0";
            }
            catch (ArgumentException) { txtThanhTien.Text = "0"; }
            catch (OverflowException) { txtThanhTien.Text = "0"; }
        }

        private void AddLine()
        {
            Safe(() =>
            {
                if (savedCode != null) return;
                int count; decimal unitPrice, reduction;
                if (cboMaTivi.SelectedValue == null) throw new ArgumentException("Vui lòng chọn Tivi.");
                if (!int.TryParse(txtSoLuong.Text, out count) || !decimal.TryParse(txtDonGiaNhap.Text, out unitPrice) || !decimal.TryParse(txtGiamGia.Text, out reduction))
                    throw new ArgumentException("Vui lòng nhập số lượng, đơn giá và giảm giá hợp lệ.");
                InvoiceRepository.LineTotal(count, unitPrice, reduction);
                string id = cboMaTivi.SelectedValue.ToString();
                DataRow existing = lines.Rows.Cast<DataRow>().FirstOrDefault(r => r["MaTV"].ToString() == id);
                int newCount = checked(count + (existing == null ? 0 : Convert.ToInt32(existing["SoLuong"])));

                if (existing != null)
                {
                    if (MessageBox.Show(this, "Cộng thêm số lượng và áp dụng đơn giá, giảm giá hiện tại cho sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                    existing["SoLuong"] = newCount; existing["DonGia"] = unitPrice; existing["GiamGia"] = reduction;
                    existing["ThanhTien"] = InvoiceRepository.LineTotal(newCount, unitPrice, reduction);
                }
                else lines.Rows.Add(id, txtTenTivi.Text, count, unitPrice, reduction, InvoiceRepository.LineTotal(count, unitPrice, reduction));
                CalculateTotal(); SetState(false);
            });
        }

        private decimal CalculateTotal()
        {
            decimal subtotal = lines.Rows.Cast<DataRow>().Sum(r => Convert.ToDecimal(r["ThanhTien"]));
            decimal value = subtotal;
            lblTongTien.Text = DatabaseHelper.FormatTien(value);
            lblBangChu.Text = "Bằng chữ: " + DatabaseHelper.ChuyenSoSangChu(value);
            return value;
        }

        private void SaveInvoice()
        {
            Safe(() =>
            {
                if (savedCode != null) return;

                string id = InvoiceRepository.SavePurchase(dtpNgayNhap.Value, frmDangNhap.CurrentEmployeeId,
                    cboNCC.SelectedValue == null ? null : cboNCC.SelectedValue.ToString(), lines);
                // Đánh dấu đã lưu trước khi tải lại để không tạo trùng nếu việc tải thất bại.
                savedCode = id; txtMaHDN.Text = id; SetState(true);
                OpenInvoice(id); RefreshCodes();
                MessageBox.Show(this, "Đã lưu hóa đơn và cập nhật tồn kho.", "Thành công");
            });
        }

        private void Search()
        {
            Safe(() =>
            {
                string id = txtMaHDN.Text.Trim();
                if (id.Length == 0 || !CanDiscard()) return;
                OpenInvoice(id);
            });
        }

        public void OpenInvoice(string id)
        {
            InitializeRuntime();
            if (!runtimeInitialized) return;
            var header = InvoiceRepository.Header(false, id);
            if (header.Rows.Count == 0) throw new ArgumentException("Không tìm thấy hóa đơn.");
            var detail = InvoiceRepository.Lines(false, id);
            loading = true;
            try
            {
                var row = header.Rows[0];
                savedCode = id; txtMaHDN.Text = id;
                dtpNgayNhap.Value = Convert.ToDateTime(row["NgayNhap"]);
                cboNhanVien.SelectedValue = row["MaNV"];
                var staff = DatabaseHelper.Query("SELECT TenNV FROM tblNhanVien WHERE MaNV=@id", DatabaseHelper.P("@id", row["MaNV"]));
                txtTenNV.Text = staff.Rows.Count == 0 ? "" : staff.Rows[0][0].ToString();
                Bind(cboNCC, DatabaseHelper.Query("SELECT MaNCC,TenNCC FROM tblNhaCungCap"), "MaNCC", "MaNCC");
                cboNCC.SelectedValue = row["MaNCC"];
                lines = detail; BindLines();
            }
            finally { loading = false; }
            PartnerChanged(); CalculateTotal(); SetState(true);
        }

        private void RemoveLine(int index)
        {
            Safe(() =>
            {
                if (lines == null || index < 0 || index >= lines.Rows.Count) return;
                if (MessageBox.Show(this, savedCode == null ? "Xóa sản phẩm khỏi hóa đơn đang nhập?" : "Xóa sản phẩm khỏi hóa đơn đã lưu và cập nhật lại tồn kho?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                DataRow row = GetLineAt(index);
                if (savedCode == null) { lines.Rows.Remove(row); CalculateTotal(); SetState(false); }
                else { InvoiceRepository.Delete(false, savedCode, row["MaTV"].ToString()); OpenInvoice(savedCode); }
            });
        }

        internal DataRow GetLineAt(int displayIndex)
        {
            // Lấy dòng đang hiển thị vì người dùng có thể đã sắp xếp bảng.
            return ((DataRowView)dgvChiTietHDN.Rows[displayIndex].DataBoundItem).Row;
        }

        private void CancelInvoice()
        {
            Safe(() =>
            {
                if (savedCode != null)
                {
                    if (MessageBox.Show(this, "Xóa toàn bộ hóa đơn đã lưu và cập nhật lại tồn kho?", "Xác nhận hủy hóa đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                    InvoiceRepository.Delete(false, savedCode);
                }
                else if (!CanDiscard()) return;
                Reset(); RefreshCodes();
            });
        }

        private void PrintInvoice()
        {
            Safe(() =>
            {
                if (savedCode == null) return;
                OpenInvoice(savedCode); // In dữ liệu hóa đơn đã lưu.
                if (lines.Rows.Count == 0) throw new ArgumentException("Hóa đơn không còn sản phẩm để in.");
                decimal value = CalculateTotal();
                ExcelExportHelper.ExportHoaDonNhapToExcel(savedCode, dtpNgayNhap.Value, txtTenNV.Text, txtTenNCC.Text, txtDiaChiNCC.Text, txtDienThoaiNCC.Text, dgvChiTietHDN, value, DatabaseHelper.ChuyenSoSangChu(value));
            });
        }

        private void frmHoaDonNhap_Load(object sender, EventArgs e) { InitializeRuntime(); }
        private void cboNhanVien_SelectedIndexChanged(object sender, EventArgs e) { if (runtimeInitialized) EmployeeChanged(); }
        private void cboNCC_SelectedIndexChanged(object sender, EventArgs e) { if (runtimeInitialized) PartnerChanged(); }
        private void cboMaTivi_SelectedIndexChanged(object sender, EventArgs e) { if (runtimeInitialized) ProductChanged(); }
        private void TinhThanhTien(object sender, EventArgs e) { if (runtimeInitialized) Calculate(); }
        private void btnThemChiTiet_Click(object sender, EventArgs e) { if (runtimeInitialized) AddLine(); }
        private void btnThemHD_Click(object sender, EventArgs e) { if (runtimeInitialized) NewInvoice(); }
        private void btnLuuHD_Click(object sender, EventArgs e) { if (runtimeInitialized) SaveInvoice(); }
        private void btnHuyHD_Click(object sender, EventArgs e) { if (runtimeInitialized) CancelInvoice(); }
        private void btnInHD_Click(object sender, EventArgs e) { if (runtimeInitialized) PrintInvoice(); }
        private void btnDong_Click(object sender, EventArgs e) { Close(); }
        private void btnTimKiem_Click(object sender, EventArgs e) { new frmTimKiemHDN().ShowDialog(); }
        private void dgvChiTietHDN_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { if (runtimeInitialized) RemoveLine(e.RowIndex); }
    }
}
