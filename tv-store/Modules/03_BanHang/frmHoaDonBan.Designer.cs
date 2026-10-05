namespace tv_store.Modules._03_BanHang
{
    partial class frmHoaDonBan
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.grpThongTinChung = new System.Windows.Forms.GroupBox();
            this.txtDienThoaiKH = new System.Windows.Forms.TextBox();
            this.lblDienThoaiKH = new System.Windows.Forms.Label();
            this.txtDiaChiKH = new System.Windows.Forms.TextBox();
            this.lblDiaChiKH = new System.Windows.Forms.Label();
            this.txtTenKH = new System.Windows.Forms.TextBox();
            this.lblTenKH = new System.Windows.Forms.Label();
            this.cboKhachHang = new System.Windows.Forms.ComboBox();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.txtTenNV = new System.Windows.Forms.TextBox();
            this.lblTenNV = new System.Windows.Forms.Label();
            this.cboNhanVien = new System.Windows.Forms.ComboBox();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.dtpNgayBan = new System.Windows.Forms.DateTimePicker();
            this.lblNgayBan = new System.Windows.Forms.Label();
            this.txtMaHDB = new System.Windows.Forms.TextBox();
            this.lblMaHDB = new System.Windows.Forms.Label();
            this.grpChiTiet = new System.Windows.Forms.GroupBox();
            this.txtTonKho = new System.Windows.Forms.TextBox();
            this.lblTonKho = new System.Windows.Forms.Label();
            this.btnThemChiTiet = new System.Windows.Forms.Button();
            this.txtThanhTien = new System.Windows.Forms.TextBox();
            this.lblThanhTien = new System.Windows.Forms.Label();
            this.txtGiamGia = new System.Windows.Forms.TextBox();
            this.lblGiamGia = new System.Windows.Forms.Label();
            this.txtDonGiaBan = new System.Windows.Forms.TextBox();
            this.lblDonGiaBan = new System.Windows.Forms.Label();
            this.txtSoLuongBan = new System.Windows.Forms.TextBox();
            this.lblSoLuongBan = new System.Windows.Forms.Label();
            this.txtTenTivi = new System.Windows.Forms.TextBox();
            this.lblTenTivi = new System.Windows.Forms.Label();
            this.cboMaTivi = new System.Windows.Forms.ComboBox();
            this.lblMaTivi = new System.Windows.Forms.Label();
            this.dgvChiTietHDB = new System.Windows.Forms.DataGridView();
            this.pnlTongTien = new System.Windows.Forms.Panel();
            this.lblBangChu = new System.Windows.Forms.Label();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.lblTongTienLabel = new System.Windows.Forms.Label();
            this.pnlChucNang = new System.Windows.Forms.Panel();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.btnInHDB = new System.Windows.Forms.Button();
            this.btnHuyHDB = new System.Windows.Forms.Button();
            this.btnLuuHDB = new System.Windows.Forms.Button();
            this.btnThemHDB = new System.Windows.Forms.Button();
            this.toolTipMain = new System.Windows.Forms.ToolTip(this.components);
            this.errProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlTop.SuspendLayout();
            this.grpThongTinChung.SuspendLayout();
            this.grpChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietHDB)).BeginInit();
            this.pnlTongTien.SuspendLayout();
            this.pnlChucNang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(43)))), ((int)(((byte)(73)))));
            this.pnlTop.Controls.Add(this.lblTieuDe);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1084, 50);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.ForeColor = System.Drawing.Color.White;
            this.lblTieuDe.Location = new System.Drawing.Point(380, 10);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(325, 30);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "HÓA ĐƠN BÁN HÀNG (HDB)";
            // 
            // grpThongTinChung
            // 
            this.grpThongTinChung.Controls.Add(this.txtDienThoaiKH);
            this.grpThongTinChung.Controls.Add(this.lblDienThoaiKH);
            this.grpThongTinChung.Controls.Add(this.txtDiaChiKH);
            this.grpThongTinChung.Controls.Add(this.lblDiaChiKH);
            this.grpThongTinChung.Controls.Add(this.txtTenKH);
            this.grpThongTinChung.Controls.Add(this.lblTenKH);
            this.grpThongTinChung.Controls.Add(this.cboKhachHang);
            this.grpThongTinChung.Controls.Add(this.lblKhachHang);
            this.grpThongTinChung.Controls.Add(this.txtTenNV);
            this.grpThongTinChung.Controls.Add(this.lblTenNV);
            this.grpThongTinChung.Controls.Add(this.cboNhanVien);
            this.grpThongTinChung.Controls.Add(this.lblNhanVien);
            this.grpThongTinChung.Controls.Add(this.dtpNgayBan);
            this.grpThongTinChung.Controls.Add(this.lblNgayBan);
            this.grpThongTinChung.Controls.Add(this.txtMaHDB);
            this.grpThongTinChung.Controls.Add(this.lblMaHDB);
            this.grpThongTinChung.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpThongTinChung.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpThongTinChung.ForeColor = System.Drawing.Color.Navy;
            this.grpThongTinChung.Location = new System.Drawing.Point(0, 50);
            this.grpThongTinChung.Name = "grpThongTinChung";
            this.grpThongTinChung.Size = new System.Drawing.Size(1084, 155);
            this.grpThongTinChung.TabIndex = 1;
            this.grpThongTinChung.TabStop = false;
            this.grpThongTinChung.Text = "Thông tin chung Hóa đơn bán";
            // 
            // txtDienThoaiKH
            // 
            this.txtDienThoaiKH.Location = new System.Drawing.Point(670, 119);
            this.txtDienThoaiKH.Name = "txtDienThoaiKH";
            this.txtDienThoaiKH.ReadOnly = true;
            this.txtDienThoaiKH.Size = new System.Drawing.Size(380, 24);
            this.txtDienThoaiKH.TabIndex = 15;
            // 
            // lblDienThoaiKH
            // 
            this.lblDienThoaiKH.AutoSize = true;
            this.lblDienThoaiKH.ForeColor = System.Drawing.Color.Black;
            this.lblDienThoaiKH.Location = new System.Drawing.Point(580, 122);
            this.lblDienThoaiKH.Name = "lblDienThoaiKH";
            this.lblDienThoaiKH.Size = new System.Drawing.Size(70, 17);
            this.lblDienThoaiKH.TabIndex = 14;
            this.lblDienThoaiKH.Text = "Điện thoại:";
            // 
            // txtDiaChiKH
            // 
            this.txtDiaChiKH.Location = new System.Drawing.Point(670, 88);
            this.txtDiaChiKH.Name = "txtDiaChiKH";
            this.txtDiaChiKH.ReadOnly = true;
            this.txtDiaChiKH.Size = new System.Drawing.Size(380, 24);
            this.txtDiaChiKH.TabIndex = 13;
            // 
            // lblDiaChiKH
            // 
            this.lblDiaChiKH.AutoSize = true;
            this.lblDiaChiKH.ForeColor = System.Drawing.Color.Black;
            this.lblDiaChiKH.Location = new System.Drawing.Point(580, 91);
            this.lblDiaChiKH.Name = "lblDiaChiKH";
            this.lblDiaChiKH.Size = new System.Drawing.Size(50, 17);
            this.lblDiaChiKH.TabIndex = 12;
            this.lblDiaChiKH.Text = "Địa chỉ:";
            // 
            // txtTenKH
            // 
            this.txtTenKH.Location = new System.Drawing.Point(670, 57);
            this.txtTenKH.Name = "txtTenKH";
            this.txtTenKH.ReadOnly = true;
            this.txtTenKH.Size = new System.Drawing.Size(380, 24);
            this.txtTenKH.TabIndex = 11;
            // 
            // lblTenKH
            // 
            this.lblTenKH.AutoSize = true;
            this.lblTenKH.ForeColor = System.Drawing.Color.Black;
            this.lblTenKH.Location = new System.Drawing.Point(580, 60);
            this.lblTenKH.Name = "lblTenKH";
            this.lblTenKH.Size = new System.Drawing.Size(50, 17);
            this.lblTenKH.TabIndex = 10;
            this.lblTenKH.Text = "Tên KH:";
            // 
            // cboKhachHang
            // 
            this.cboKhachHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhachHang.FormattingEnabled = true;
            this.cboKhachHang.Location = new System.Drawing.Point(670, 25);
            this.cboKhachHang.Name = "cboKhachHang";
            this.cboKhachHang.Size = new System.Drawing.Size(380, 25);
            this.cboKhachHang.TabIndex = 9;
            this.cboKhachHang.SelectedIndexChanged += new System.EventHandler(this.cboKhachHang_SelectedIndexChanged);
            // 
            // lblKhachHang
            // 
            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.ForeColor = System.Drawing.Color.Black;
            this.lblKhachHang.Location = new System.Drawing.Point(580, 28);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new System.Drawing.Size(79, 17);
            this.lblKhachHang.TabIndex = 8;
            this.lblKhachHang.Text = "Khách hàng:";
            // 
            // txtTenNV
            // 
            this.txtTenNV.Location = new System.Drawing.Point(125, 119);
            this.txtTenNV.Name = "txtTenNV";
            this.txtTenNV.ReadOnly = true;
            this.txtTenNV.Size = new System.Drawing.Size(380, 24);
            this.txtTenNV.TabIndex = 7;
            // 
            // lblTenNV
            // 
            this.lblTenNV.AutoSize = true;
            this.lblTenNV.ForeColor = System.Drawing.Color.Black;
            this.lblTenNV.Location = new System.Drawing.Point(30, 122);
            this.lblTenNV.Name = "lblTenNV";
            this.lblTenNV.Size = new System.Drawing.Size(91, 17);
            this.lblTenNV.TabIndex = 6;
            this.lblTenNV.Text = "Tên nhân viên:";
            // 
            // cboNhanVien
            // 
            this.cboNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhanVien.FormattingEnabled = true;
            this.cboNhanVien.Location = new System.Drawing.Point(125, 88);
            this.cboNhanVien.Name = "cboNhanVien";
            this.cboNhanVien.Size = new System.Drawing.Size(380, 25);
            this.cboNhanVien.TabIndex = 5;
            this.cboNhanVien.SelectedIndexChanged += new System.EventHandler(this.cboNhanVien_SelectedIndexChanged);
            // 
            // lblNhanVien
            // 
            this.lblNhanVien.AutoSize = true;
            this.lblNhanVien.ForeColor = System.Drawing.Color.Black;
            this.lblNhanVien.Location = new System.Drawing.Point(30, 91);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Size = new System.Drawing.Size(69, 17);
            this.lblNhanVien.TabIndex = 4;
            this.lblNhanVien.Text = "Nhân viên:";
            // 
            // dtpNgayBan
            // 
            this.dtpNgayBan.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpNgayBan.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayBan.Location = new System.Drawing.Point(125, 57);
            this.dtpNgayBan.Name = "dtpNgayBan";
            this.dtpNgayBan.Size = new System.Drawing.Size(380, 24);
            this.dtpNgayBan.TabIndex = 3;
            // 
            // lblNgayBan
            // 
            this.lblNgayBan.AutoSize = true;
            this.lblNgayBan.ForeColor = System.Drawing.Color.Black;
            this.lblNgayBan.Location = new System.Drawing.Point(30, 60);
            this.lblNgayBan.Name = "lblNgayBan";
            this.lblNgayBan.Size = new System.Drawing.Size(68, 17);
            this.lblNgayBan.TabIndex = 2;
            this.lblNgayBan.Text = "Ngày bán:";
            // 
            // txtMaHDB
            // 
            this.txtMaHDB.Location = new System.Drawing.Point(125, 25);
            this.txtMaHDB.Name = "txtMaHDB";
            this.txtMaHDB.Size = new System.Drawing.Size(380, 24);
            this.txtMaHDB.TabIndex = 1;
            // 
            // lblMaHDB
            // 
            this.lblMaHDB.AutoSize = true;
            this.lblMaHDB.ForeColor = System.Drawing.Color.Black;
            this.lblMaHDB.Location = new System.Drawing.Point(30, 28);
            this.lblMaHDB.Name = "lblMaHDB";
            this.lblMaHDB.Size = new System.Drawing.Size(60, 17);
            this.lblMaHDB.TabIndex = 0;
            this.lblMaHDB.Text = "Mã HDB:";
            // 
            // grpChiTiet
            // 
            this.grpChiTiet.Controls.Add(this.txtTonKho);
            this.grpChiTiet.Controls.Add(this.lblTonKho);
            this.grpChiTiet.Controls.Add(this.btnThemChiTiet);
            this.grpChiTiet.Controls.Add(this.txtThanhTien);
            this.grpChiTiet.Controls.Add(this.lblThanhTien);
            this.grpChiTiet.Controls.Add(this.txtGiamGia);
            this.grpChiTiet.Controls.Add(this.lblGiamGia);
            this.grpChiTiet.Controls.Add(this.txtDonGiaBan);
            this.grpChiTiet.Controls.Add(this.lblDonGiaBan);
            this.grpChiTiet.Controls.Add(this.txtSoLuongBan);
            this.grpChiTiet.Controls.Add(this.lblSoLuongBan);
            this.grpChiTiet.Controls.Add(this.txtTenTivi);
            this.grpChiTiet.Controls.Add(this.lblTenTivi);
            this.grpChiTiet.Controls.Add(this.cboMaTivi);
            this.grpChiTiet.Controls.Add(this.lblMaTivi);
            this.grpChiTiet.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpChiTiet.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpChiTiet.ForeColor = System.Drawing.Color.Navy;
            this.grpChiTiet.Location = new System.Drawing.Point(0, 205);
            this.grpChiTiet.Name = "grpChiTiet";
            this.grpChiTiet.Size = new System.Drawing.Size(1084, 115);
            this.grpChiTiet.TabIndex = 2;
            this.grpChiTiet.TabStop = false;
            this.grpChiTiet.Text = "Chi tiết mặt hàng bán";
            // 
            // txtTonKho
            // 
            this.txtTonKho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(243)))), ((int)(((byte)(205)))));
            this.txtTonKho.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTonKho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(100)))), ((int)(((byte)(4)))));
            this.txtTonKho.Location = new System.Drawing.Point(985, 30);
            this.txtTonKho.Name = "txtTonKho";
            this.txtTonKho.ReadOnly = true;
            this.txtTonKho.Size = new System.Drawing.Size(65, 24);
            this.txtTonKho.TabIndex = 14;
            // 
            // lblTonKho
            // 
            this.lblTonKho.AutoSize = true;
            this.lblTonKho.ForeColor = System.Drawing.Color.Black;
            this.lblTonKho.Location = new System.Drawing.Point(920, 33);
            this.lblTonKho.Name = "lblTonKho";
            this.lblTonKho.Size = new System.Drawing.Size(60, 17);
            this.lblTonKho.TabIndex = 13;
            this.lblTonKho.Text = "Tồn kho:";
            // 
            // btnThemChiTiet
            // 
            this.btnThemChiTiet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnThemChiTiet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemChiTiet.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemChiTiet.ForeColor = System.Drawing.Color.White;
            this.btnThemChiTiet.Location = new System.Drawing.Point(920, 68);
            this.btnThemChiTiet.Name = "btnThemChiTiet";
            this.btnThemChiTiet.Size = new System.Drawing.Size(130, 32);
            this.btnThemChiTiet.TabIndex = 12;
            this.btnThemChiTiet.Text = "&Thêm món";
            this.toolTipMain.SetToolTip(this.btnThemChiTiet, "Thêm mặt hàng vào hóa đơn bán (Alt+M)");
            this.btnThemChiTiet.UseVisualStyleBackColor = false;
            this.btnThemChiTiet.Click += new System.EventHandler(this.btnThemChiTiet_Click);
            // 
            // txtThanhTien
            // 
            this.txtThanhTien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtThanhTien.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtThanhTien.ForeColor = System.Drawing.Color.DarkRed;
            this.txtThanhTien.Location = new System.Drawing.Point(670, 72);
            this.txtThanhTien.Name = "txtThanhTien";
            this.txtThanhTien.ReadOnly = true;
            this.txtThanhTien.Size = new System.Drawing.Size(220, 24);
            this.txtThanhTien.TabIndex = 11;
            // 
            // lblThanhTien
            // 
            this.lblThanhTien.AutoSize = true;
            this.lblThanhTien.ForeColor = System.Drawing.Color.Black;
            this.lblThanhTien.Location = new System.Drawing.Point(580, 75);
            this.lblThanhTien.Name = "lblThanhTien";
            this.lblThanhTien.Size = new System.Drawing.Size(71, 17);
            this.lblThanhTien.TabIndex = 10;
            this.lblThanhTien.Text = "Thành tiền:";
            // 
            // txtGiamGia
            // 
            this.txtGiamGia.Location = new System.Drawing.Point(380, 72);
            this.txtGiamGia.Name = "txtGiamGia";
            this.txtGiamGia.Size = new System.Drawing.Size(125, 24);
            this.txtGiamGia.TabIndex = 9;
            this.txtGiamGia.Text = "0";
            this.txtGiamGia.TextChanged += new System.EventHandler(this.TinhThanhTien);
            // 
            // lblGiamGia
            // 
            this.lblGiamGia.AutoSize = true;
            this.lblGiamGia.ForeColor = System.Drawing.Color.Black;
            this.lblGiamGia.Location = new System.Drawing.Point(295, 75);
            this.lblGiamGia.Name = "lblGiamGia";
            this.lblGiamGia.Size = new System.Drawing.Size(83, 17);
            this.lblGiamGia.TabIndex = 8;
            this.lblGiamGia.Text = "Giảm giá (%):";
            // 
            // txtDonGiaBan
            // 
            this.txtDonGiaBan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtDonGiaBan.Location = new System.Drawing.Point(670, 30);
            this.txtDonGiaBan.Name = "txtDonGiaBan";
            this.txtDonGiaBan.ReadOnly = true;
            this.txtDonGiaBan.Size = new System.Drawing.Size(220, 24);
            this.txtDonGiaBan.TabIndex = 7;
            // 
            // lblDonGiaBan
            // 
            this.lblDonGiaBan.AutoSize = true;
            this.lblDonGiaBan.ForeColor = System.Drawing.Color.Black;
            this.lblDonGiaBan.Location = new System.Drawing.Point(580, 33);
            this.lblDonGiaBan.Name = "lblDonGiaBan";
            this.lblDonGiaBan.Size = new System.Drawing.Size(83, 17);
            this.lblDonGiaBan.TabIndex = 6;
            this.lblDonGiaBan.Text = "Đơn giá bán:";
            // 
            // txtSoLuongBan
            // 
            this.txtSoLuongBan.Location = new System.Drawing.Point(125, 72);
            this.txtSoLuongBan.Name = "txtSoLuongBan";
            this.txtSoLuongBan.Size = new System.Drawing.Size(150, 24);
            this.txtSoLuongBan.TabIndex = 5;
            this.txtSoLuongBan.Text = "1";
            this.txtSoLuongBan.TextChanged += new System.EventHandler(this.TinhThanhTien);
            // 
            // lblSoLuongBan
            // 
            this.lblSoLuongBan.AutoSize = true;
            this.lblSoLuongBan.ForeColor = System.Drawing.Color.Black;
            this.lblSoLuongBan.Location = new System.Drawing.Point(30, 75);
            this.lblSoLuongBan.Name = "lblSoLuongBan";
            this.lblSoLuongBan.Size = new System.Drawing.Size(90, 17);
            this.lblSoLuongBan.TabIndex = 4;
            this.lblSoLuongBan.Text = "Số lượng mua:";
            // 
            // txtTenTivi
            // 
            this.txtTenTivi.Location = new System.Drawing.Point(295, 30);
            this.txtTenTivi.Name = "txtTenTivi";
            this.txtTenTivi.ReadOnly = true;
            this.txtTenTivi.Size = new System.Drawing.Size(210, 24);
            this.txtTenTivi.TabIndex = 3;
            // 
            // lblTenTivi
            // 
            this.lblTenTivi.AutoSize = true;
            this.lblTenTivi.Location = new System.Drawing.Point(292, 10);
            this.lblTenTivi.Name = "lblTenTivi";
            this.lblTenTivi.Size = new System.Drawing.Size(55, 17);
            this.lblTenTivi.TabIndex = 2;
            this.lblTenTivi.Text = "Tên Tivi:";
            this.lblTenTivi.Visible = false;
            // 
            // cboMaTivi
            // 
            this.cboMaTivi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaTivi.FormattingEnabled = true;
            this.cboMaTivi.Location = new System.Drawing.Point(125, 30);
            this.cboMaTivi.Name = "cboMaTivi";
            this.cboMaTivi.Size = new System.Drawing.Size(150, 25);
            this.cboMaTivi.TabIndex = 1;
            this.cboMaTivi.SelectedIndexChanged += new System.EventHandler(this.cboMaTivi_SelectedIndexChanged);
            // 
            // lblMaTivi
            // 
            this.lblMaTivi.AutoSize = true;
            this.lblMaTivi.ForeColor = System.Drawing.Color.Black;
            this.lblMaTivi.Location = new System.Drawing.Point(30, 33);
            this.lblMaTivi.Name = "lblMaTivi";
            this.lblMaTivi.Size = new System.Drawing.Size(73, 17);
            this.lblMaTivi.TabIndex = 0;
            this.lblMaTivi.Text = "Chọn Tivi:";
            // 
            // dgvChiTietHDB
            // 
            this.dgvChiTietHDB.AllowUserToAddRows = false;
            this.dgvChiTietHDB.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTietHDB.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTietHDB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTietHDB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTietHDB.Location = new System.Drawing.Point(0, 320);
            this.dgvChiTietHDB.Name = "dgvChiTietHDB";
            this.dgvChiTietHDB.ReadOnly = true;
            this.dgvChiTietHDB.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTietHDB.Size = new System.Drawing.Size(1084, 235);
            this.dgvChiTietHDB.TabIndex = 3;
            this.dgvChiTietHDB.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTietHDB_CellDoubleClick);
            // 
            // pnlTongTien
            // 
            this.pnlTongTien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.pnlTongTien.Controls.Add(this.lblBangChu);
            this.pnlTongTien.Controls.Add(this.lblTongTien);
            this.pnlTongTien.Controls.Add(this.lblTongTienLabel);
            this.pnlTongTien.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTongTien.Location = new System.Drawing.Point(0, 555);
            this.pnlTongTien.Name = "pnlTongTien";
            this.pnlTongTien.Size = new System.Drawing.Size(1084, 45);
            this.pnlTongTien.TabIndex = 4;
            // 
            // lblBangChu
            // 
            this.lblBangChu.AutoSize = true;
            this.lblBangChu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBangChu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBangChu.Location = new System.Drawing.Point(30, 14);
            this.lblBangChu.Name = "lblBangChu";
            this.lblBangChu.Size = new System.Drawing.Size(147, 17);
            this.lblBangChu.TabIndex = 2;
            this.lblBangChu.Text = "Bằng chữ: Không đồng.";
            // 
            // lblTongTien
            // 
            this.lblTongTien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTien.ForeColor = System.Drawing.Color.Red;
            this.lblTongTien.Location = new System.Drawing.Point(920, 11);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(61, 21);
            this.lblTongTien.TabIndex = 1;
            this.lblTongTien.Text = "0 VNĐ";
            // 
            // lblTongTienLabel
            // 
            this.lblTongTienLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongTienLabel.AutoSize = true;
            this.lblTongTienLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTienLabel.Location = new System.Drawing.Point(835, 13);
            this.lblTongTienLabel.Name = "lblTongTienLabel";
            this.lblTongTienLabel.Size = new System.Drawing.Size(76, 19);
            this.lblTongTienLabel.TabIndex = 0;
            this.lblTongTienLabel.Text = "Tổng tiền:";
            // 
            // pnlChucNang
            // 
            this.pnlChucNang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlChucNang.Controls.Add(this.btnDong);
            this.pnlChucNang.Controls.Add(this.btnXuatExcel);
            this.pnlChucNang.Controls.Add(this.btnInHDB);
            this.pnlChucNang.Controls.Add(this.btnHuyHDB);
            this.pnlChucNang.Controls.Add(this.btnLuuHDB);
            this.pnlChucNang.Controls.Add(this.btnThemHDB);
            this.pnlChucNang.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlChucNang.Location = new System.Drawing.Point(0, 600);
            this.pnlChucNang.Name = "pnlChucNang";
            this.pnlChucNang.Size = new System.Drawing.Size(1084, 55);
            this.pnlChucNang.TabIndex = 5;
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(950, 10);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(105, 35);
            this.btnDong.TabIndex = 5;
            this.btnDong.Text = "Đó&ng";
            this.toolTipMain.SetToolTip(this.btnDong, "Đóng màn hình (Alt+N)");
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnXuatExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatExcel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuatExcel.ForeColor = System.Drawing.Color.White;
            this.btnXuatExcel.Location = new System.Drawing.Point(540, 10);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(115, 35);
            this.btnXuatExcel.TabIndex = 4;
            this.btnXuatExcel.Text = "&Xuất Excel";
            this.toolTipMain.SetToolTip(this.btnXuatExcel, "Xuất chi tiết hóa đơn ra file Excel/CSV (Alt+X)");
            this.btnXuatExcel.UseVisualStyleBackColor = false;
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // btnInHDB
            // 
            this.btnInHDB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(66)))), ((int)(((byte)(193)))));
            this.btnInHDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInHDB.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInHDB.ForeColor = System.Drawing.Color.White;
            this.btnInHDB.Location = new System.Drawing.Point(410, 10);
            this.btnInHDB.Name = "btnInHDB";
            this.btnInHDB.Size = new System.Drawing.Size(105, 35);
            this.btnInHDB.TabIndex = 3;
            this.btnInHDB.Text = "&In HĐ";
            this.toolTipMain.SetToolTip(this.btnInHDB, "In hóa đơn bán hàng cho khách (Alt+I)");
            this.btnInHDB.UseVisualStyleBackColor = false;
            this.btnInHDB.Click += new System.EventHandler(this.btnInHDB_Click);
            // 
            // btnHuyHDB
            // 
            this.btnHuyHDB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnHuyHDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuyHDB.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuyHDB.ForeColor = System.Drawing.Color.Black;
            this.btnHuyHDB.Location = new System.Drawing.Point(280, 10);
            this.btnHuyHDB.Name = "btnHuyHDB";
            this.btnHuyHDB.Size = new System.Drawing.Size(105, 35);
            this.btnHuyHDB.TabIndex = 2;
            this.btnHuyHDB.Text = "&Hủy HĐ";
            this.toolTipMain.SetToolTip(this.btnHuyHDB, "Hủy làm việc với hóa đơn (Alt+H)");
            this.btnHuyHDB.UseVisualStyleBackColor = false;
            this.btnHuyHDB.Click += new System.EventHandler(this.btnHuyHDB_Click);
            // 
            // btnLuuHDB
            // 
            this.btnLuuHDB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnLuuHDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuuHDB.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuuHDB.ForeColor = System.Drawing.Color.White;
            this.btnLuuHDB.Location = new System.Drawing.Point(150, 10);
            this.btnLuuHDB.Name = "btnLuuHDB";
            this.btnLuuHDB.Size = new System.Drawing.Size(105, 35);
            this.btnLuuHDB.TabIndex = 1;
            this.btnLuuHDB.Text = "&Lưu HĐ";
            this.toolTipMain.SetToolTip(this.btnLuuHDB, "Lưu toàn bộ hóa đơn bán và trừ kho (Alt+L)");
            this.btnLuuHDB.UseVisualStyleBackColor = false;
            this.btnLuuHDB.Click += new System.EventHandler(this.btnLuuHDB_Click);
            // 
            // btnThemHDB
            // 
            this.btnThemHDB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(144)))), ((int)(((byte)(255)))));
            this.btnThemHDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemHDB.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemHDB.ForeColor = System.Drawing.Color.White;
            this.btnThemHDB.Location = new System.Drawing.Point(20, 10);
            this.btnThemHDB.Name = "btnThemHDB";
            this.btnThemHDB.Size = new System.Drawing.Size(105, 35);
            this.btnThemHDB.TabIndex = 0;
            this.btnThemHDB.Text = "&Thêm HĐ";
            this.toolTipMain.SetToolTip(this.btnThemHDB, "Tạo mới Hóa đơn bán (Alt+T)");
            this.btnThemHDB.UseVisualStyleBackColor = false;
            this.btnThemHDB.Click += new System.EventHandler(this.btnThemHDB_Click);
            // 
            // errProvider
            // 
            this.errProvider.ContainerControl = this;
            // 
            // frmHoaDonBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1084, 655);
            this.Controls.Add(this.dgvChiTietHDB);
            this.Controls.Add(this.pnlTongTien);
            this.Controls.Add(this.pnlChucNang);
            this.Controls.Add(this.grpChiTiet);
            this.Controls.Add(this.grpThongTinChung);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1100, 690);
            this.Name = "frmHoaDonBan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Hóa đơn bán hàng";
            this.Load += new System.EventHandler(this.frmHoaDonBan_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.grpThongTinChung.ResumeLayout(false);
            this.grpThongTinChung.PerformLayout();
            this.grpChiTiet.ResumeLayout(false);
            this.grpChiTiet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietHDB)).EndInit();
            this.pnlTongTien.ResumeLayout(false);
            this.pnlTongTien.PerformLayout();
            this.pnlChucNang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.GroupBox grpThongTinChung;
        private System.Windows.Forms.TextBox txtDienThoaiKH;
        private System.Windows.Forms.Label lblDienThoaiKH;
        private System.Windows.Forms.TextBox txtDiaChiKH;
        private System.Windows.Forms.Label lblDiaChiKH;
        private System.Windows.Forms.TextBox txtTenKH;
        private System.Windows.Forms.Label lblTenKH;
        private System.Windows.Forms.ComboBox cboKhachHang;
        private System.Windows.Forms.Label lblKhachHang;
        private System.Windows.Forms.TextBox txtTenNV;
        private System.Windows.Forms.Label lblTenNV;
        private System.Windows.Forms.ComboBox cboNhanVien;
        private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.DateTimePicker dtpNgayBan;
        private System.Windows.Forms.Label lblNgayBan;
        private System.Windows.Forms.TextBox txtMaHDB;
        private System.Windows.Forms.Label lblMaHDB;
        private System.Windows.Forms.GroupBox grpChiTiet;
        private System.Windows.Forms.TextBox txtTonKho;
        private System.Windows.Forms.Label lblTonKho;
        private System.Windows.Forms.Button btnThemChiTiet;
        private System.Windows.Forms.TextBox txtThanhTien;
        private System.Windows.Forms.Label lblThanhTien;
        private System.Windows.Forms.TextBox txtGiamGia;
        private System.Windows.Forms.Label lblGiamGia;
        private System.Windows.Forms.TextBox txtDonGiaBan;
        private System.Windows.Forms.Label lblDonGiaBan;
        private System.Windows.Forms.TextBox txtSoLuongBan;
        private System.Windows.Forms.Label lblSoLuongBan;
        private System.Windows.Forms.TextBox txtTenTivi;
        private System.Windows.Forms.Label lblTenTivi;
        private System.Windows.Forms.ComboBox cboMaTivi;
        private System.Windows.Forms.Label lblMaTivi;
        private System.Windows.Forms.DataGridView dgvChiTietHDB;
        private System.Windows.Forms.Panel pnlTongTien;
        private System.Windows.Forms.Label lblBangChu;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Label lblTongTienLabel;
        private System.Windows.Forms.Panel pnlChucNang;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.Button btnInHDB;
        private System.Windows.Forms.Button btnHuyHDB;
        private System.Windows.Forms.Button btnLuuHDB;
        private System.Windows.Forms.Button btnThemHDB;
        private System.Windows.Forms.ToolTip toolTipMain;
        private System.Windows.Forms.ErrorProvider errProvider;
    }
}
