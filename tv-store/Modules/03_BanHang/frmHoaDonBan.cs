using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using tv_store.Helpers;
using tv_store.Modules._04_HeThong_BaoCao;

namespace tv_store.Modules._03_BanHang
{
    public partial class frmHoaDonBan : Form
    {
        private DataTable lines;
        private string savedCode;
        private decimal tax;
        private bool loading;
        private bool runtimeInitialized;

        public frmHoaDonBan()
        {
            InitializeComponent();
        }

        private void InitializeRuntime()
        {
            if (runtimeInitialized || DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            runtimeInitialized = true;
            txtMaHDB.Width -= 85;
            var search = new Button { Text = "Tìm HĐ", Left = txtMaHDB.Right + 5, Top = txtMaHDB.Top - 1, Width = 80, Height = txtMaHDB.Height + 2 };
            txtMaHDB.Parent.Controls.Add(search);
            search.Click += (s, e) => Search();
            txtMaHDB.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; Search(); } };
            txtMaHDB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtMaHDB.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboNhanVien.Enabled = false;

            txtDienThoaiKH.Leave += (s, e) => Safe(LookupCustomer);
            txtDienThoaiKH.TextChanged += (s, e) =>
            {
                if (loading || savedCode != null) return;
                loading = true;
                cboKhachHang.SelectedIndex = -1;
                txtTenKH.Clear(); txtDiaChiKH.Clear();
                txtTenKH.ReadOnly = txtDiaChiKH.ReadOnly = false;
                loading = false;
            };

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
                    Bind(cboKhachHang, DatabaseHelper.Query("SELECT MaKhach,TenKhach FROM tblKhachHang"), "MaKhach", "MaKhach");
                    Bind(cboMaTivi, DatabaseHelper.Query("SELECT MaTV,TenTV FROM tblTV"), "MaTV", "MaTV");
                    RefreshCodes();
                }
                finally { loading = false; }
                Reset();
            });
        }

        private void RefreshCodes()
        {
            var data = DatabaseHelper.Query("SELECT SoHDB FROM tblHoaDonBan ORDER BY NgayBan DESC");
            string text = txtMaHDB.Text;
            txtMaHDB.Items.Clear();
            foreach (DataRow row in data.Rows) txtMaHDB.Items.Add(row[0].ToString());
            txtMaHDB.Text = text;
        }

        private void Reset()
        {
            loading = true;
            savedCode = null;
            tax = 10m;
            txtMaHDB.Text = "";
            dtpNgayBan.Value = DateTime.Now;
            cboNhanVien.SelectedValue = frmDangNhap.CurrentEmployeeId ?? "";
            txtTenNV.Text = frmDangNhap.CurrentEmployeeName;
            cboKhachHang.SelectedIndex = cboMaTivi.SelectedIndex = -1;
            txtTenKH.Clear(); txtDiaChiKH.Clear(); txtDienThoaiKH.Clear(); txtTenTivi.Clear();
            txtSoLuongBan.Text = "1"; txtDonGiaBan.Text = txtGiamGia.Text = txtThanhTien.Text = "0";
            txtTonKho.Text = "0";
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
            dgvChiTietHDB.DataSource = lines;
            string[] titles = { "Mã Tivi", "Tên Tivi", "Số lượng", "Đơn giá bán", "Giảm giá (%)", "Thành tiền" };
            for (int i = 0; i < titles.Length; i++) dgvChiTietHDB.Columns[i].HeaderText = titles[i];
            dgvChiTietHDB.Columns["TenTV"].FillWeight = 220;
            foreach (string name in new[] { "DonGia", "ThanhTien" }) dgvChiTietHDB.Columns[name].DefaultCellStyle.Format = "N2";
        }

        private void SetState(bool saved)
        {
            btnLuuHDB.Enabled = !saved;
            btnHuyHDB.Enabled = saved || lines.Rows.Count > 0 || txtMaHDB.Text.Length > 0;
            btnInHDB.Enabled = saved && lines.Rows.Count > 0;
            btnXuatExcel.Enabled = btnInHDB.Enabled;
            btnThemChiTiet.Enabled = !saved;
            grpChiTiet.Enabled = !saved;
            dtpNgayBan.Enabled = cboKhachHang.Enabled = !saved;
            txtDienThoaiKH.ReadOnly = saved;
            txtDienThoaiKH.TabStop = !saved;
            txtTenKH.ReadOnly = txtDiaChiKH.ReadOnly = saved || cboKhachHang.SelectedIndex >= 0;
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
                txtMaHDB.Text = InvoiceRepository.PreviewCode(true, dtpNgayBan.Value);
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
            if (loading || cboKhachHang.SelectedValue == null) return;
            Safe(() =>
            {
                var data = DatabaseHelper.Query("SELECT TenKhach,DiaChi,DienThoai FROM tblKhachHang WHERE MaKhach=@id", DatabaseHelper.P("@id", cboKhachHang.SelectedValue));
                if (data.Rows.Count == 0) return;
                loading = true;
                try { txtTenKH.Text = data.Rows[0][0].ToString(); txtDiaChiKH.Text = data.Rows[0][1].ToString(); txtDienThoaiKH.Text = data.Rows[0][2].ToString(); }
                finally { loading = false; }
                txtTenKH.ReadOnly = txtDiaChiKH.ReadOnly = true;
            });
        }

        private void LookupCustomer()
        {
            if (savedCode != null || string.IsNullOrWhiteSpace(txtDienThoaiKH.Text)) return;
            var data = DatabaseHelper.Query("SELECT TOP 1 MaKhach,TenKhach,DiaChi FROM tblKhachHang WHERE DienThoai=@phone ORDER BY MaKhach", DatabaseHelper.P("@phone", txtDienThoaiKH.Text.Trim()));
            loading = true;
            try
            {
                if (data.Rows.Count > 0)
                {
                    // Nạp lại danh sách để nhận cả khách hàng vừa được tạo ở cửa sổ khác.
                    Bind(cboKhachHang, DatabaseHelper.Query("SELECT MaKhach,TenKhach FROM tblKhachHang"), "MaKhach", "MaKhach");
                    cboKhachHang.SelectedValue = data.Rows[0]["MaKhach"];
                    txtTenKH.Text = data.Rows[0]["TenKhach"].ToString();
                    txtDiaChiKH.Text = data.Rows[0]["DiaChi"].ToString();
                }
                else cboKhachHang.SelectedIndex = -1;
                txtTenKH.ReadOnly = txtDiaChiKH.ReadOnly = data.Rows.Count > 0;
            }
            finally { loading = false; }
        }

        private void ProductChanged()
        {
            if (loading || cboMaTivi.SelectedValue == null) return;
            Safe(() =>
            {
                var data = DatabaseHelper.Query("SELECT TenTV,DonGiaBan,DonGiaNhap,SoLuong FROM tblTV WHERE MaTV=@id", DatabaseHelper.P("@id", cboMaTivi.SelectedValue));
                if (data.Rows.Count == 0) return;
                txtTenTivi.Text = data.Rows[0]["TenTV"].ToString();
                txtDonGiaBan.Text = data.Rows[0]["DonGiaBan"].ToString();
                txtTonKho.Text = data.Rows[0]["SoLuong"].ToString();
                Calculate();
            });
        }

        private void Calculate()
        {
            if (loading) return;
            int count; decimal unitPrice, reduction;
            try
            {
                txtThanhTien.Text = int.TryParse(txtSoLuongBan.Text, out count) && decimal.TryParse(txtDonGiaBan.Text, out unitPrice) && decimal.TryParse(txtGiamGia.Text, out reduction)
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
                if (!int.TryParse(txtSoLuongBan.Text, out count) || !decimal.TryParse(txtDonGiaBan.Text, out unitPrice) || !decimal.TryParse(txtGiamGia.Text, out reduction))
                    throw new ArgumentException("Vui lòng nhập số lượng, đơn giá và giảm giá hợp lệ.");
                InvoiceRepository.LineTotal(count, unitPrice, reduction);
                string id = cboMaTivi.SelectedValue.ToString();
                DataRow existing = lines.Rows.Cast<DataRow>().FirstOrDefault(r => r["MaTV"].ToString() == id);
                int newCount = checked(count + (existing == null ? 0 : Convert.ToInt32(existing["SoLuong"])));

                var stock = DatabaseHelper.Query("SELECT SoLuong FROM tblTV WHERE MaTV=@id", DatabaseHelper.P("@id", id));
                if (stock.Rows.Count == 0 || Convert.ToInt32(stock.Rows[0][0]) < newCount) throw new ArgumentException("Tổng số lượng mua vượt quá tồn kho hiện tại.");
                txtTonKho.Text = stock.Rows[0][0].ToString();

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
            decimal value = Math.Round(subtotal * (1 + tax / 100m), 0, MidpointRounding.AwayFromZero);

            lblTongTien.Text = DatabaseHelper.FormatTien(value);
            lblBangChu.Text = "Bằng chữ (VAT " + tax + "%): " + DatabaseHelper.ChuyenSoSangChu(value);
            return value;
        }

        private void SaveInvoice()
        {
            Safe(() =>
            {
                if (savedCode != null) return;
                LookupCustomer();
                string id = InvoiceRepository.SaveSale(dtpNgayBan.Value, frmDangNhap.CurrentEmployeeId,
                    lines, txtTenKH.Text.Trim(), txtDiaChiKH.Text.Trim(), txtDienThoaiKH.Text.Trim(), tax);
                // Đánh dấu đã lưu trước khi tải lại để không tạo trùng nếu việc tải thất bại.
                savedCode = id; txtMaHDB.Text = id; SetState(true);
                OpenInvoice(id); RefreshCodes();
                MessageBox.Show(this, "Đã lưu hóa đơn và cập nhật tồn kho.", "Thành công");
            });
        }

        private void Search()
        {
            Safe(() =>
            {
                string id = txtMaHDB.Text.Trim();
                if (id.Length == 0 || !CanDiscard()) return;
                OpenInvoice(id);
            });
        }

        public void OpenInvoice(string id)
        {
            InitializeRuntime();
            if (!runtimeInitialized) return;
            var header = InvoiceRepository.Header(true, id);
            if (header.Rows.Count == 0) throw new ArgumentException("Không tìm thấy hóa đơn.");
            var detail = InvoiceRepository.Lines(true, id);
            loading = true;
            try
            {
                var row = header.Rows[0];
                savedCode = id; txtMaHDB.Text = id;
                dtpNgayBan.Value = Convert.ToDateTime(row["NgayBan"]);
                cboNhanVien.SelectedValue = row["MaNV"];
                var staff = DatabaseHelper.Query("SELECT TenNV FROM tblNhanVien WHERE MaNV=@id", DatabaseHelper.P("@id", row["MaNV"]));
                txtTenNV.Text = staff.Rows.Count == 0 ? "" : staff.Rows[0][0].ToString();
                Bind(cboKhachHang, DatabaseHelper.Query("SELECT MaKhach,TenKhach FROM tblKhachHang"), "MaKhach", "MaKhach");
                cboKhachHang.SelectedValue = row["MaKhach"];
                lines = detail; BindLines();
                tax = Convert.ToDecimal(row["Thue"]);
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
                else { InvoiceRepository.Delete(true, savedCode, row["MaTV"].ToString()); OpenInvoice(savedCode); }
            });
        }

        internal DataRow GetLineAt(int displayIndex)
        {
            // Lấy dòng đang hiển thị vì người dùng có thể đã sắp xếp bảng.
            return ((DataRowView)dgvChiTietHDB.Rows[displayIndex].DataBoundItem).Row;
        }

        private void CancelInvoice()
        {
            Safe(() =>
            {
                if (savedCode != null)
                {
                    if (MessageBox.Show(this, "Xóa toàn bộ hóa đơn đã lưu và cập nhật lại tồn kho?", "Xác nhận hủy hóa đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                    InvoiceRepository.Delete(true, savedCode);
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
                OpenInvoice(savedCode); // In dữ liệu đã lưu, bao gồm thuế suất ban đầu.
                if (lines.Rows.Count == 0) throw new ArgumentException("Hóa đơn không còn sản phẩm để in.");
                decimal value = CalculateTotal();
                ExcelExportHelper.ExportHoaDonBanToExcel(savedCode, dtpNgayBan.Value, txtTenNV.Text, txtTenKH.Text, txtDiaChiKH.Text, txtDienThoaiKH.Text, dgvChiTietHDB, value, DatabaseHelper.ChuyenSoSangChu(value));
            });
        }

        private void frmHoaDonBan_Load(object sender, EventArgs e) { InitializeRuntime(); }
        private void cboNhanVien_SelectedIndexChanged(object sender, EventArgs e) { if (runtimeInitialized) EmployeeChanged(); }
        private void cboKhachHang_SelectedIndexChanged(object sender, EventArgs e) { if (runtimeInitialized) PartnerChanged(); }
        private void cboMaTivi_SelectedIndexChanged(object sender, EventArgs e) { if (runtimeInitialized) ProductChanged(); }
        private void TinhThanhTien(object sender, EventArgs e) { if (runtimeInitialized) Calculate(); }
        private void btnThemChiTiet_Click(object sender, EventArgs e) { if (runtimeInitialized) AddLine(); }
        private void btnThemHDB_Click(object sender, EventArgs e) { if (runtimeInitialized) NewInvoice(); }
        private void btnLuuHDB_Click(object sender, EventArgs e) { if (runtimeInitialized) SaveInvoice(); }
        private void btnHuyHDB_Click(object sender, EventArgs e) { if (runtimeInitialized) CancelInvoice(); }
        private void btnInHDB_Click(object sender, EventArgs e) { if (runtimeInitialized) PrintInvoice(); }
        private void btnDong_Click(object sender, EventArgs e) { Close(); }
        private void btnXuatExcel_Click(object sender, EventArgs e) { if (runtimeInitialized) PrintInvoice(); }
        private void dgvChiTietHDB_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { if (runtimeInitialized) RemoveLine(e.RowIndex); }
    }
}
