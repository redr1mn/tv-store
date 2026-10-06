namespace tv_store.Modules._04_HeThong_BaoCao
{
    partial class frmBaoCaoThongKe
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
            this.tbcBaoCao = new System.Windows.Forms.TabControl();
            this.tpTop3KhachHang = new System.Windows.Forms.TabPage();
            this.dgvBaoCao1 = new System.Windows.Forms.DataGridView();
            this.pnlTop1 = new System.Windows.Forms.Panel();
            this.btnXuatExcel1 = new System.Windows.Forms.Button();
            this.btnXemBaoCao1 = new System.Windows.Forms.Button();
            this.cboKhachHang = new System.Windows.Forms.ComboBox();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.tpNhapHangNCC = new System.Windows.Forms.TabPage();
            this.dgvBaoCao2 = new System.Windows.Forms.DataGridView();
            this.pnlBottom2 = new System.Windows.Forms.Panel();
            this.lblTongTienBaoCao2 = new System.Windows.Forms.Label();
            this.pnlTop2 = new System.Windows.Forms.Panel();
            this.btnXuatExcel2 = new System.Windows.Forms.Button();
            this.btnXemBaoCao2 = new System.Windows.Forms.Button();
            this.cboNCC = new System.Windows.Forms.ComboBox();
            this.lblNCC = new System.Windows.Forms.Label();
            this.tpDoanhThuQuy = new System.Windows.Forms.TabPage();
            this.dgvBaoCao3 = new System.Windows.Forms.DataGridView();
            this.pnlBottom3 = new System.Windows.Forms.Panel();
            this.lblTongTienBaoCao3 = new System.Windows.Forms.Label();
            this.pnlTop3 = new System.Windows.Forms.Panel();
            this.rdoNhapHang = new System.Windows.Forms.RadioButton();
            this.rdoBanHang = new System.Windows.Forms.RadioButton();
            this.txtNamQuy = new System.Windows.Forms.TextBox();
            this.lblNamQuy = new System.Windows.Forms.Label();
            this.cboQuy = new System.Windows.Forms.ComboBox();
            this.lblQuy = new System.Windows.Forms.Label();
            this.btnXuatExcel3 = new System.Windows.Forms.Button();
            this.btnXemBaoCao3 = new System.Windows.Forms.Button();
            this.tpTop5NCC = new System.Windows.Forms.TabPage();
            this.dgvBaoCao4 = new System.Windows.Forms.DataGridView();
            this.pnlTop4 = new System.Windows.Forms.Panel();
            this.txtNamThang = new System.Windows.Forms.TextBox();
            this.lblNamThang = new System.Windows.Forms.Label();
            this.cboThang = new System.Windows.Forms.ComboBox();
            this.lblThang = new System.Windows.Forms.Label();
            this.btnXuatExcel4 = new System.Windows.Forms.Button();
            this.btnXemBaoCao4 = new System.Windows.Forms.Button();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnDong = new System.Windows.Forms.Button();
            this.toolTipMain = new System.Windows.Forms.ToolTip(this.components);
            this.pnlTop.SuspendLayout();
            this.tbcBaoCao.SuspendLayout();
            this.tpTop3KhachHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoCao1)).BeginInit();
            this.pnlTop1.SuspendLayout();
            this.tpNhapHangNCC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoCao2)).BeginInit();
            this.pnlBottom2.SuspendLayout();
            this.pnlTop2.SuspendLayout();
            this.tpDoanhThuQuy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoCao3)).BeginInit();
            this.pnlBottom3.SuspendLayout();
            this.pnlTop3.SuspendLayout();
            this.tpTop5NCC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoCao4)).BeginInit();
            this.pnlTop4.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(43)))), ((int)(((byte)(73)))));
            this.pnlTop.Controls.Add(this.lblTieuDe);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1034, 50);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.ForeColor = System.Drawing.Color.White;
            this.lblTieuDe.Location = new System.Drawing.Point(340, 10);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(542, 45);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "BÁO CÁO THỐNG KÊ (YC 6, 7, 8, 9)";
            // 
            // tbcBaoCao
            // 
            this.tbcBaoCao.Controls.Add(this.tpTop3KhachHang);
            this.tbcBaoCao.Controls.Add(this.tpNhapHangNCC);
            this.tbcBaoCao.Controls.Add(this.tpDoanhThuQuy);
            this.tbcBaoCao.Controls.Add(this.tpTop5NCC);
            this.tbcBaoCao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcBaoCao.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbcBaoCao.ItemSize = new System.Drawing.Size(220, 32);
            this.tbcBaoCao.Location = new System.Drawing.Point(0, 50);
            this.tbcBaoCao.Name = "tbcBaoCao";
            this.tbcBaoCao.SelectedIndex = 0;
            this.tbcBaoCao.Size = new System.Drawing.Size(1034, 506);
            this.tbcBaoCao.TabIndex = 1;
            // 
            // tpTop3KhachHang
            // 
            this.tpTop3KhachHang.Controls.Add(this.dgvBaoCao1);
            this.tpTop3KhachHang.Controls.Add(this.pnlTop1);
            this.tpTop3KhachHang.Location = new System.Drawing.Point(4, 36);
            this.tpTop3KhachHang.Name = "tpTop3KhachHang";
            this.tpTop3KhachHang.Padding = new System.Windows.Forms.Padding(3);
            this.tpTop3KhachHang.Size = new System.Drawing.Size(1026, 466);
            this.tpTop3KhachHang.TabIndex = 0;
            this.tpTop3KhachHang.Text = "1. Top 3 Tivi theo Khách hàng (YC 6)";
            this.tpTop3KhachHang.UseVisualStyleBackColor = true;
            // 
            // dgvBaoCao1
            // 
            this.dgvBaoCao1.AllowUserToAddRows = false;
            this.dgvBaoCao1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBaoCao1.BackgroundColor = System.Drawing.Color.White;
            this.dgvBaoCao1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBaoCao1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBaoCao1.Location = new System.Drawing.Point(3, 63);
            this.dgvBaoCao1.Name = "dgvBaoCao1";
            this.dgvBaoCao1.ReadOnly = true;
            this.dgvBaoCao1.RowHeadersWidth = 62;
            this.dgvBaoCao1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBaoCao1.Size = new System.Drawing.Size(1020, 400);
            this.dgvBaoCao1.TabIndex = 1;
            // 
            // pnlTop1
            // 
            this.pnlTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlTop1.Controls.Add(this.btnXuatExcel1);
            this.pnlTop1.Controls.Add(this.btnXemBaoCao1);
            this.pnlTop1.Controls.Add(this.cboKhachHang);
            this.pnlTop1.Controls.Add(this.lblKhachHang);
            this.pnlTop1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop1.Location = new System.Drawing.Point(3, 3);
            this.pnlTop1.Name = "pnlTop1";
            this.pnlTop1.Size = new System.Drawing.Size(1020, 60);
            this.pnlTop1.TabIndex = 0;
            // 
            // btnXuatExcel1
            // 
            this.btnXuatExcel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnXuatExcel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatExcel1.ForeColor = System.Drawing.Color.White;
            this.btnXuatExcel1.Location = new System.Drawing.Point(620, 13);
            this.btnXuatExcel1.Name = "btnXuatExcel1";
            this.btnXuatExcel1.Size = new System.Drawing.Size(120, 34);
            this.btnXuatExcel1.TabIndex = 3;
            this.btnXuatExcel1.Text = "&Xuất Excel";
            this.toolTipMain.SetToolTip(this.btnXuatExcel1, "Xuất kết quả ra file Excel (Alt+X)");
            this.btnXuatExcel1.UseVisualStyleBackColor = false;
            this.btnXuatExcel1.Click += new System.EventHandler(this.btnXuatExcel1_Click);
            // 
            // btnXemBaoCao1
            // 
            this.btnXemBaoCao1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnXemBaoCao1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemBaoCao1.ForeColor = System.Drawing.Color.White;
            this.btnXemBaoCao1.Location = new System.Drawing.Point(480, 13);
            this.btnXemBaoCao1.Name = "btnXemBaoCao1";
            this.btnXemBaoCao1.Size = new System.Drawing.Size(125, 34);
            this.btnXemBaoCao1.TabIndex = 2;
            this.btnXemBaoCao1.Text = "&Xem báo cáo";
            this.toolTipMain.SetToolTip(this.btnXemBaoCao1, "Xem danh sách Top 3 Tivi mua nhiều nhất (Alt+X)");
            this.btnXemBaoCao1.UseVisualStyleBackColor = false;
            this.btnXemBaoCao1.Click += new System.EventHandler(this.btnXemBaoCao1_Click);
            // 
            // cboKhachHang
            // 
            this.cboKhachHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhachHang.FormattingEnabled = true;
            this.cboKhachHang.Location = new System.Drawing.Point(150, 18);
            this.cboKhachHang.Name = "cboKhachHang";
            this.cboKhachHang.Size = new System.Drawing.Size(300, 33);
            this.cboKhachHang.TabIndex = 1;
            this.cboKhachHang.SelectedIndexChanged += new System.EventHandler(this.cboKhachHang_SelectedIndexChanged);
            // 
            // lblKhachHang
            // 
            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.ForeColor = System.Drawing.Color.Navy;
            this.lblKhachHang.Location = new System.Drawing.Point(20, 21);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new System.Drawing.Size(172, 25);
            this.lblKhachHang.TabIndex = 0;
            this.lblKhachHang.Text = "Chọn Khách hàng:";
            // 
            // tpNhapHangNCC
            // 
            this.tpNhapHangNCC.Controls.Add(this.dgvBaoCao2);
            this.tpNhapHangNCC.Controls.Add(this.pnlBottom2);
            this.tpNhapHangNCC.Controls.Add(this.pnlTop2);
            this.tpNhapHangNCC.Location = new System.Drawing.Point(4, 36);
            this.tpNhapHangNCC.Name = "tpNhapHangNCC";
            this.tpNhapHangNCC.Padding = new System.Windows.Forms.Padding(3);
            this.tpNhapHangNCC.Size = new System.Drawing.Size(1026, 466);
            this.tpNhapHangNCC.TabIndex = 1;
            this.tpNhapHangNCC.Text = "2. Báo cáo Nhập hàng theo NCC (YC 7)";
            this.tpNhapHangNCC.UseVisualStyleBackColor = true;
            // 
            // dgvBaoCao2
            // 
            this.dgvBaoCao2.AllowUserToAddRows = false;
            this.dgvBaoCao2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBaoCao2.BackgroundColor = System.Drawing.Color.White;
            this.dgvBaoCao2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBaoCao2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBaoCao2.Location = new System.Drawing.Point(3, 63);
            this.dgvBaoCao2.Name = "dgvBaoCao2";
            this.dgvBaoCao2.ReadOnly = true;
            this.dgvBaoCao2.RowHeadersWidth = 62;
            this.dgvBaoCao2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBaoCao2.Size = new System.Drawing.Size(1020, 360);
            this.dgvBaoCao2.TabIndex = 1;
            // 
            // pnlBottom2
            // 
            this.pnlBottom2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.pnlBottom2.Controls.Add(this.lblTongTienBaoCao2);
            this.pnlBottom2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom2.Location = new System.Drawing.Point(3, 423);
            this.pnlBottom2.Name = "pnlBottom2";
            this.pnlBottom2.Size = new System.Drawing.Size(1020, 40);
            this.pnlBottom2.TabIndex = 2;
            // 
            // lblTongTienBaoCao2
            // 
            this.lblTongTienBaoCao2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongTienBaoCao2.AutoSize = true;
            this.lblTongTienBaoCao2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTienBaoCao2.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTongTienBaoCao2.Location = new System.Drawing.Point(680, 10);
            this.lblTongTienBaoCao2.Name = "lblTongTienBaoCao2";
            this.lblTongTienBaoCao2.Size = new System.Drawing.Size(307, 30);
            this.lblTongTienBaoCao2.TabIndex = 0;
            this.lblTongTienBaoCao2.Text = "Tổng tiền nhập hàng: 0 VNĐ";
            // 
            // pnlTop2
            // 
            this.pnlTop2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlTop2.Controls.Add(this.btnXuatExcel2);
            this.pnlTop2.Controls.Add(this.btnXemBaoCao2);
            this.pnlTop2.Controls.Add(this.cboNCC);
            this.pnlTop2.Controls.Add(this.lblNCC);
            this.pnlTop2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop2.Location = new System.Drawing.Point(3, 3);
            this.pnlTop2.Name = "pnlTop2";
            this.pnlTop2.Size = new System.Drawing.Size(1020, 60);
            this.pnlTop2.TabIndex = 0;
            // 
            // btnXuatExcel2
            // 
            this.btnXuatExcel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnXuatExcel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatExcel2.ForeColor = System.Drawing.Color.White;
            this.btnXuatExcel2.Location = new System.Drawing.Point(650, 13);
            this.btnXuatExcel2.Name = "btnXuatExcel2";
            this.btnXuatExcel2.Size = new System.Drawing.Size(120, 34);
            this.btnXuatExcel2.TabIndex = 3;
            this.btnXuatExcel2.Text = "Xuất &Excel";
            this.btnXuatExcel2.UseVisualStyleBackColor = false;
            this.btnXuatExcel2.Click += new System.EventHandler(this.btnXuatExcel2_Click);
            // 
            // btnXemBaoCao2
            // 
            this.btnXemBaoCao2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnXemBaoCao2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemBaoCao2.ForeColor = System.Drawing.Color.White;
            this.btnXemBaoCao2.Location = new System.Drawing.Point(510, 13);
            this.btnXemBaoCao2.Name = "btnXemBaoCao2";
            this.btnXemBaoCao2.Size = new System.Drawing.Size(125, 34);
            this.btnXemBaoCao2.TabIndex = 2;
            this.btnXemBaoCao2.Text = "&Xem báo cáo";
            this.btnXemBaoCao2.UseVisualStyleBackColor = false;
            this.btnXemBaoCao2.Click += new System.EventHandler(this.btnXemBaoCao2_Click);
            // 
            // cboNCC
            // 
            this.cboNCC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNCC.FormattingEnabled = true;
            this.cboNCC.Location = new System.Drawing.Point(160, 18);
            this.cboNCC.Name = "cboNCC";
            this.cboNCC.Size = new System.Drawing.Size(320, 33);
            this.cboNCC.TabIndex = 1;
            // 
            // lblNCC
            // 
            this.lblNCC.AutoSize = true;
            this.lblNCC.ForeColor = System.Drawing.Color.Navy;
            this.lblNCC.Location = new System.Drawing.Point(20, 21);
            this.lblNCC.Name = "lblNCC";
            this.lblNCC.Size = new System.Drawing.Size(191, 25);
            this.lblNCC.TabIndex = 0;
            this.lblNCC.Text = "Chọn Nhà cung cấp:";
            // 
            // tpDoanhThuQuy
            // 
            this.tpDoanhThuQuy.Controls.Add(this.dgvBaoCao3);
            this.tpDoanhThuQuy.Controls.Add(this.pnlBottom3);
            this.tpDoanhThuQuy.Controls.Add(this.pnlTop3);
            this.tpDoanhThuQuy.Location = new System.Drawing.Point(4, 36);
            this.tpDoanhThuQuy.Name = "tpDoanhThuQuy";
            this.tpDoanhThuQuy.Size = new System.Drawing.Size(1026, 466);
            this.tpDoanhThuQuy.TabIndex = 2;
            this.tpDoanhThuQuy.Text = "3. Hóa đơn & Tổng tiền theo Quý (YC 8)";
            this.tpDoanhThuQuy.UseVisualStyleBackColor = true;
            // 
            // dgvBaoCao3
            // 
            this.dgvBaoCao3.AllowUserToAddRows = false;
            this.dgvBaoCao3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBaoCao3.BackgroundColor = System.Drawing.Color.White;
            this.dgvBaoCao3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBaoCao3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBaoCao3.Location = new System.Drawing.Point(0, 60);
            this.dgvBaoCao3.Name = "dgvBaoCao3";
            this.dgvBaoCao3.ReadOnly = true;
            this.dgvBaoCao3.RowHeadersWidth = 62;
            this.dgvBaoCao3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBaoCao3.Size = new System.Drawing.Size(1026, 366);
            this.dgvBaoCao3.TabIndex = 1;
            // 
            // pnlBottom3
            // 
            this.pnlBottom3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.pnlBottom3.Controls.Add(this.lblTongTienBaoCao3);
            this.pnlBottom3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom3.Location = new System.Drawing.Point(0, 426);
            this.pnlBottom3.Name = "pnlBottom3";
            this.pnlBottom3.Size = new System.Drawing.Size(1026, 40);
            this.pnlBottom3.TabIndex = 2;
            // 
            // lblTongTienBaoCao3
            // 
            this.lblTongTienBaoCao3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongTienBaoCao3.AutoSize = true;
            this.lblTongTienBaoCao3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTienBaoCao3.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTongTienBaoCao3.Location = new System.Drawing.Point(680, 10);
            this.lblTongTienBaoCao3.Name = "lblTongTienBaoCao3";
            this.lblTongTienBaoCao3.Size = new System.Drawing.Size(292, 30);
            this.lblTongTienBaoCao3.TabIndex = 0;
            this.lblTongTienBaoCao3.Text = "Tổng tiền theo Quý: 0 VNĐ";
            // 
            // pnlTop3
            // 
            this.pnlTop3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlTop3.Controls.Add(this.rdoNhapHang);
            this.pnlTop3.Controls.Add(this.rdoBanHang);
            this.pnlTop3.Controls.Add(this.txtNamQuy);
            this.pnlTop3.Controls.Add(this.lblNamQuy);
            this.pnlTop3.Controls.Add(this.cboQuy);
            this.pnlTop3.Controls.Add(this.lblQuy);
            this.pnlTop3.Controls.Add(this.btnXuatExcel3);
            this.pnlTop3.Controls.Add(this.btnXemBaoCao3);
            this.pnlTop3.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop3.Location = new System.Drawing.Point(0, 0);
            this.pnlTop3.Name = "pnlTop3";
            this.pnlTop3.Size = new System.Drawing.Size(1026, 60);
            this.pnlTop3.TabIndex = 0;
            // 
            // rdoNhapHang
            // 
            this.rdoNhapHang.AutoSize = true;
            this.rdoNhapHang.ForeColor = System.Drawing.Color.Black;
            this.rdoNhapHang.Location = new System.Drawing.Point(450, 20);
            this.rdoNhapHang.Name = "rdoNhapHang";
            this.rdoNhapHang.Size = new System.Drawing.Size(169, 29);
            this.rdoNhapHang.TabIndex = 7;
            this.rdoNhapHang.Text = "HĐ Nhập hàng";
            this.rdoNhapHang.UseVisualStyleBackColor = true;
            // 
            // rdoBanHang
            // 
            this.rdoBanHang.AutoSize = true;
            this.rdoBanHang.Checked = true;
            this.rdoBanHang.ForeColor = System.Drawing.Color.Black;
            this.rdoBanHang.Location = new System.Drawing.Point(335, 20);
            this.rdoBanHang.Name = "rdoBanHang";
            this.rdoBanHang.Size = new System.Drawing.Size(155, 29);
            this.rdoBanHang.TabIndex = 6;
            this.rdoBanHang.TabStop = true;
            this.rdoBanHang.Text = "HĐ Bán hàng";
            this.rdoBanHang.UseVisualStyleBackColor = true;
            // 
            // txtNamQuy
            // 
            this.txtNamQuy.Location = new System.Drawing.Point(235, 18);
            this.txtNamQuy.Name = "txtNamQuy";
            this.txtNamQuy.Size = new System.Drawing.Size(75, 33);
            this.txtNamQuy.TabIndex = 5;
            this.txtNamQuy.Text = "2026";
            // 
            // lblNamQuy
            // 
            this.lblNamQuy.AutoSize = true;
            this.lblNamQuy.ForeColor = System.Drawing.Color.Navy;
            this.lblNamQuy.Location = new System.Drawing.Point(190, 21);
            this.lblNamQuy.Name = "lblNamQuy";
            this.lblNamQuy.Size = new System.Drawing.Size(59, 25);
            this.lblNamQuy.TabIndex = 4;
            this.lblNamQuy.Text = "Năm:";
            // 
            // cboQuy
            // 
            this.cboQuy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboQuy.FormattingEnabled = true;
            this.cboQuy.Items.AddRange(new object[] {
            "Quý 1",
            "Quý 2",
            "Quý 3",
            "Quý 4"});
            this.cboQuy.Location = new System.Drawing.Point(95, 18);
            this.cboQuy.Name = "cboQuy";
            this.cboQuy.Size = new System.Drawing.Size(80, 33);
            this.cboQuy.TabIndex = 1;
            // 
            // lblQuy
            // 
            this.lblQuy.AutoSize = true;
            this.lblQuy.ForeColor = System.Drawing.Color.Navy;
            this.lblQuy.Location = new System.Drawing.Point(20, 21);
            this.lblQuy.Name = "lblQuy";
            this.lblQuy.Size = new System.Drawing.Size(105, 25);
            this.lblQuy.TabIndex = 0;
            this.lblQuy.Text = "Chọn Quý:";
            // 
            // btnXuatExcel3
            // 
            this.btnXuatExcel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnXuatExcel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatExcel3.ForeColor = System.Drawing.Color.White;
            this.btnXuatExcel3.Location = new System.Drawing.Point(740, 13);
            this.btnXuatExcel3.Name = "btnXuatExcel3";
            this.btnXuatExcel3.Size = new System.Drawing.Size(120, 34);
            this.btnXuatExcel3.TabIndex = 3;
            this.btnXuatExcel3.Text = "Xuất &Excel";
            this.btnXuatExcel3.UseVisualStyleBackColor = false;
            this.btnXuatExcel3.Click += new System.EventHandler(this.btnXuatExcel3_Click);
            // 
            // btnXemBaoCao3
            // 
            this.btnXemBaoCao3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnXemBaoCao3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemBaoCao3.ForeColor = System.Drawing.Color.White;
            this.btnXemBaoCao3.Location = new System.Drawing.Point(600, 13);
            this.btnXemBaoCao3.Name = "btnXemBaoCao3";
            this.btnXemBaoCao3.Size = new System.Drawing.Size(125, 34);
            this.btnXemBaoCao3.TabIndex = 2;
            this.btnXemBaoCao3.Text = "&Xem báo cáo";
            this.btnXemBaoCao3.UseVisualStyleBackColor = false;
            this.btnXemBaoCao3.Click += new System.EventHandler(this.btnXemBaoCao3_Click);
            // 
            // tpTop5NCC
            // 
            this.tpTop5NCC.Controls.Add(this.dgvBaoCao4);
            this.tpTop5NCC.Controls.Add(this.pnlTop4);
            this.tpTop5NCC.Location = new System.Drawing.Point(4, 36);
            this.tpTop5NCC.Name = "tpTop5NCC";
            this.tpTop5NCC.Size = new System.Drawing.Size(1026, 466);
            this.tpTop5NCC.TabIndex = 3;
            this.tpTop5NCC.Text = "4. Top 5 NCC giao hàng nhiều nhất (YC 9)";
            this.tpTop5NCC.UseVisualStyleBackColor = true;
            // 
            // dgvBaoCao4
            // 
            this.dgvBaoCao4.AllowUserToAddRows = false;
            this.dgvBaoCao4.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBaoCao4.BackgroundColor = System.Drawing.Color.White;
            this.dgvBaoCao4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBaoCao4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBaoCao4.Location = new System.Drawing.Point(0, 60);
            this.dgvBaoCao4.Name = "dgvBaoCao4";
            this.dgvBaoCao4.ReadOnly = true;
            this.dgvBaoCao4.RowHeadersWidth = 62;
            this.dgvBaoCao4.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBaoCao4.Size = new System.Drawing.Size(1026, 406);
            this.dgvBaoCao4.TabIndex = 1;
            // 
            // pnlTop4
            // 
            this.pnlTop4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlTop4.Controls.Add(this.txtNamThang);
            this.pnlTop4.Controls.Add(this.lblNamThang);
            this.pnlTop4.Controls.Add(this.cboThang);
            this.pnlTop4.Controls.Add(this.lblThang);
            this.pnlTop4.Controls.Add(this.btnXuatExcel4);
            this.pnlTop4.Controls.Add(this.btnXemBaoCao4);
            this.pnlTop4.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop4.Location = new System.Drawing.Point(0, 0);
            this.pnlTop4.Name = "pnlTop4";
            this.pnlTop4.Size = new System.Drawing.Size(1026, 60);
            this.pnlTop4.TabIndex = 0;
            // 
            // txtNamThang
            // 
            this.txtNamThang.Location = new System.Drawing.Point(260, 18);
            this.txtNamThang.Name = "txtNamThang";
            this.txtNamThang.Size = new System.Drawing.Size(85, 33);
            this.txtNamThang.TabIndex = 5;
            this.txtNamThang.Text = "2026";
            // 
            // lblNamThang
            // 
            this.lblNamThang.AutoSize = true;
            this.lblNamThang.ForeColor = System.Drawing.Color.Navy;
            this.lblNamThang.Location = new System.Drawing.Point(215, 21);
            this.lblNamThang.Name = "lblNamThang";
            this.lblNamThang.Size = new System.Drawing.Size(59, 25);
            this.lblNamThang.TabIndex = 4;
            this.lblNamThang.Text = "Năm:";
            // 
            // cboThang
            // 
            this.cboThang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboThang.FormattingEnabled = true;
            this.cboThang.Items.AddRange(new object[] {
            "Tháng 1",
            "Tháng 2",
            "Tháng 3",
            "Tháng 4",
            "Tháng 5",
            "Tháng 6",
            "Tháng 7",
            "Tháng 8",
            "Tháng 9",
            "Tháng 10",
            "Tháng 11",
            "Tháng 12"});
            this.cboThang.Location = new System.Drawing.Point(105, 18);
            this.cboThang.Name = "cboThang";
            this.cboThang.Size = new System.Drawing.Size(95, 33);
            this.cboThang.TabIndex = 1;
            // 
            // lblThang
            // 
            this.lblThang.AutoSize = true;
            this.lblThang.ForeColor = System.Drawing.Color.Navy;
            this.lblThang.Location = new System.Drawing.Point(20, 21);
            this.lblThang.Name = "lblThang";
            this.lblThang.Size = new System.Drawing.Size(125, 25);
            this.lblThang.TabIndex = 0;
            this.lblThang.Text = "Chọn Tháng:";
            // 
            // btnXuatExcel4
            // 
            this.btnXuatExcel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnXuatExcel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatExcel4.ForeColor = System.Drawing.Color.White;
            this.btnXuatExcel4.Location = new System.Drawing.Point(540, 13);
            this.btnXuatExcel4.Name = "btnXuatExcel4";
            this.btnXuatExcel4.Size = new System.Drawing.Size(120, 34);
            this.btnXuatExcel4.TabIndex = 3;
            this.btnXuatExcel4.Text = "Xuất &Excel";
            this.btnXuatExcel4.UseVisualStyleBackColor = false;
            this.btnXuatExcel4.Click += new System.EventHandler(this.btnXuatExcel4_Click);
            // 
            // btnXemBaoCao4
            // 
            this.btnXemBaoCao4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnXemBaoCao4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemBaoCao4.ForeColor = System.Drawing.Color.White;
            this.btnXemBaoCao4.Location = new System.Drawing.Point(400, 13);
            this.btnXemBaoCao4.Name = "btnXemBaoCao4";
            this.btnXemBaoCao4.Size = new System.Drawing.Size(125, 34);
            this.btnXemBaoCao4.TabIndex = 2;
            this.btnXemBaoCao4.Text = "&Xem báo cáo";
            this.btnXemBaoCao4.UseVisualStyleBackColor = false;
            this.btnXemBaoCao4.Click += new System.EventHandler(this.btnXemBaoCao4_Click);
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlBottom.Controls.Add(this.btnDong);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 556);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1034, 50);
            this.pnlBottom.TabIndex = 2;
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(910, 8);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(105, 34);
            this.btnDong.TabIndex = 0;
            this.btnDong.Text = "Đó&ng";
            this.toolTipMain.SetToolTip(this.btnDong, "Đóng màn hình báo cáo (Alt+N)");
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // frmBaoCaoThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1034, 606);
            this.Controls.Add(this.tbcBaoCao);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1050, 645);
            this.Name = "frmBaoCaoThongKe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo thống kê tổng hợp";
            this.Load += new System.EventHandler(this.frmBaoCaoThongKe_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.tbcBaoCao.ResumeLayout(false);
            this.tpTop3KhachHang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoCao1)).EndInit();
            this.pnlTop1.ResumeLayout(false);
            this.pnlTop1.PerformLayout();
            this.tpNhapHangNCC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoCao2)).EndInit();
            this.pnlBottom2.ResumeLayout(false);
            this.pnlBottom2.PerformLayout();
            this.pnlTop2.ResumeLayout(false);
            this.pnlTop2.PerformLayout();
            this.tpDoanhThuQuy.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoCao3)).EndInit();
            this.pnlBottom3.ResumeLayout(false);
            this.pnlBottom3.PerformLayout();
            this.pnlTop3.ResumeLayout(false);
            this.pnlTop3.PerformLayout();
            this.tpTop5NCC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoCao4)).EndInit();
            this.pnlTop4.ResumeLayout(false);
            this.pnlTop4.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.TabControl tbcBaoCao;
        private System.Windows.Forms.TabPage tpTop3KhachHang;
        private System.Windows.Forms.TabPage tpNhapHangNCC;
        private System.Windows.Forms.TabPage tpDoanhThuQuy;
        private System.Windows.Forms.TabPage tpTop5NCC;
        private System.Windows.Forms.Panel pnlTop1;
        private System.Windows.Forms.Button btnXuatExcel1;
        private System.Windows.Forms.Button btnXemBaoCao1;
        private System.Windows.Forms.ComboBox cboKhachHang;
        private System.Windows.Forms.Label lblKhachHang;
        private System.Windows.Forms.DataGridView dgvBaoCao1;
        private System.Windows.Forms.Panel pnlTop2;
        private System.Windows.Forms.Button btnXuatExcel2;
        private System.Windows.Forms.Button btnXemBaoCao2;
        private System.Windows.Forms.ComboBox cboNCC;
        private System.Windows.Forms.Label lblNCC;
        private System.Windows.Forms.DataGridView dgvBaoCao2;
        private System.Windows.Forms.Panel pnlBottom2;
        private System.Windows.Forms.Label lblTongTienBaoCao2;
        private System.Windows.Forms.Panel pnlTop3;
        private System.Windows.Forms.RadioButton rdoNhapHang;
        private System.Windows.Forms.RadioButton rdoBanHang;
        private System.Windows.Forms.TextBox txtNamQuy;
        private System.Windows.Forms.Label lblNamQuy;
        private System.Windows.Forms.ComboBox cboQuy;
        private System.Windows.Forms.Label lblQuy;
        private System.Windows.Forms.Button btnXuatExcel3;
        private System.Windows.Forms.Button btnXemBaoCao3;
        private System.Windows.Forms.DataGridView dgvBaoCao3;
        private System.Windows.Forms.Panel pnlBottom3;
        private System.Windows.Forms.Label lblTongTienBaoCao3;
        private System.Windows.Forms.Panel pnlTop4;
        private System.Windows.Forms.TextBox txtNamThang;
        private System.Windows.Forms.Label lblNamThang;
        private System.Windows.Forms.ComboBox cboThang;
        private System.Windows.Forms.Label lblThang;
        private System.Windows.Forms.Button btnXuatExcel4;
        private System.Windows.Forms.Button btnXemBaoCao4;
        private System.Windows.Forms.DataGridView dgvBaoCao4;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.ToolTip toolTipMain;
    }
}
