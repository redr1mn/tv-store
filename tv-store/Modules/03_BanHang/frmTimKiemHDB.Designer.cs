namespace tv_store.Modules._03_BanHang
{
    partial class frmTimKiemHDB
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
            this.grpTieuChi = new System.Windows.Forms.GroupBox();
            this.chkLocNgay = new System.Windows.Forms.CheckBox();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.cboMaTivi = new System.Windows.Forms.ComboBox();
            this.lblMaTivi = new System.Windows.Forms.Label();
            this.cboNhanVien = new System.Windows.Forms.ComboBox();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.cboKhachHang = new System.Windows.Forms.ComboBox();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.txtMaHDB = new System.Windows.Forms.TextBox();
            this.lblMaHDB = new System.Windows.Forms.Label();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.grpKetQua = new System.Windows.Forms.GroupBox();
            this.dgvKetQuaHDB = new System.Windows.Forms.DataGridView();
            this.pnlThongKe = new System.Windows.Forms.Panel();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.lblSoLuongKetQua = new System.Windows.Forms.Label();
            this.toolTipMain = new System.Windows.Forms.ToolTip(this.components);
            this.pnlTop.SuspendLayout();
            this.grpTieuChi.SuspendLayout();
            this.grpKetQua.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQuaHDB)).BeginInit();
            this.pnlThongKe.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(43)))), ((int)(((byte)(73)))));
            this.pnlTop.Controls.Add(this.lblTieuDe);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1080, 52);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.ForeColor = System.Drawing.Color.White;
            this.lblTieuDe.Location = new System.Drawing.Point(340, 10);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(430, 35);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "TRA CỨU & TÌM KIẾM HÓA ĐƠN BÁN";
            // 
            // grpTieuChi
            // 
            this.grpTieuChi.Controls.Add(this.chkLocNgay);
            this.grpTieuChi.Controls.Add(this.dtpDenNgay);
            this.grpTieuChi.Controls.Add(this.lblDenNgay);
            this.grpTieuChi.Controls.Add(this.dtpTuNgay);
            this.grpTieuChi.Controls.Add(this.lblTuNgay);
            this.grpTieuChi.Controls.Add(this.cboMaTivi);
            this.grpTieuChi.Controls.Add(this.lblMaTivi);
            this.grpTieuChi.Controls.Add(this.cboNhanVien);
            this.grpTieuChi.Controls.Add(this.lblNhanVien);
            this.grpTieuChi.Controls.Add(this.cboKhachHang);
            this.grpTieuChi.Controls.Add(this.lblKhachHang);
            this.grpTieuChi.Controls.Add(this.txtMaHDB);
            this.grpTieuChi.Controls.Add(this.lblMaHDB);
            this.grpTieuChi.Controls.Add(this.btnDong);
            this.grpTieuChi.Controls.Add(this.btnXuatExcel);
            this.grpTieuChi.Controls.Add(this.btnLamMoi);
            this.grpTieuChi.Controls.Add(this.btnTimKiem);
            this.grpTieuChi.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpTieuChi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTieuChi.ForeColor = System.Drawing.Color.Navy;
            this.grpTieuChi.Location = new System.Drawing.Point(0, 52);
            this.grpTieuChi.Name = "grpTieuChi";
            this.grpTieuChi.Size = new System.Drawing.Size(1080, 160);
            this.grpTieuChi.TabIndex = 1;
            this.grpTieuChi.TabStop = false;
            this.grpTieuChi.Text = "Bộ lọc tiêu chí tìm kiếm";
            // 
            // chkLocNgay
            // 
            this.chkLocNgay.AutoSize = true;
            this.chkLocNgay.ForeColor = System.Drawing.Color.Black;
            this.chkLocNgay.Location = new System.Drawing.Point(380, 115);
            this.chkLocNgay.Name = "chkLocNgay";
            this.chkLocNgay.Size = new System.Drawing.Size(126, 25);
            this.chkLocNgay.TabIndex = 8;
            this.chkLocNgay.Text = "Lọc theo ngày";
            this.chkLocNgay.UseVisualStyleBackColor = true;
            this.chkLocNgay.CheckedChanged += new System.EventHandler(this.chkLocNgay_CheckedChanged);
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgay.Enabled = false;
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgay.Location = new System.Drawing.Point(720, 113);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(140, 29);
            this.dtpDenNgay.TabIndex = 12;
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.ForeColor = System.Drawing.Color.Black;
            this.lblDenNgay.Location = new System.Drawing.Point(645, 116);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(79, 21);
            this.lblDenNgay.TabIndex = 11;
            this.lblDenNgay.Text = "Đến ngày:";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpTuNgay.Enabled = false;
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgay.Location = new System.Drawing.Point(510, 113);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(130, 29);
            this.dtpTuNgay.TabIndex = 10;
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.ForeColor = System.Drawing.Color.Black;
            this.lblTuNgay.Location = new System.Drawing.Point(445, 116);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(68, 21);
            this.lblTuNgay.TabIndex = 9;
            this.lblTuNgay.Text = "Từ ngày:";
            // 
            // cboMaTivi
            // 
            this.cboMaTivi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaTivi.FormattingEnabled = true;
            this.cboMaTivi.Location = new System.Drawing.Point(110, 113);
            this.cboMaTivi.Name = "cboMaTivi";
            this.cboMaTivi.Size = new System.Drawing.Size(250, 29);
            this.cboMaTivi.TabIndex = 7;
            // 
            // lblMaTivi
            // 
            this.lblMaTivi.AutoSize = true;
            this.lblMaTivi.ForeColor = System.Drawing.Color.Black;
            this.lblMaTivi.Location = new System.Drawing.Point(20, 116);
            this.lblMaTivi.Name = "lblMaTivi";
            this.lblMaTivi.Size = new System.Drawing.Size(83, 21);
            this.lblMaTivi.TabIndex = 6;
            this.lblMaTivi.Text = "Mặt hàng:";
            // 
            // cboNhanVien
            // 
            this.cboNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhanVien.FormattingEnabled = true;
            this.cboNhanVien.Location = new System.Drawing.Point(110, 71);
            this.cboNhanVien.Name = "cboNhanVien";
            this.cboNhanVien.Size = new System.Drawing.Size(250, 29);
            this.cboNhanVien.TabIndex = 3;
            // 
            // lblNhanVien
            // 
            this.lblNhanVien.AutoSize = true;
            this.lblNhanVien.ForeColor = System.Drawing.Color.Black;
            this.lblNhanVien.Location = new System.Drawing.Point(20, 74);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Size = new System.Drawing.Size(84, 21);
            this.lblNhanVien.TabIndex = 2;
            this.lblNhanVien.Text = "Nhân viên:";
            // 
            // cboKhachHang
            // 
            this.cboKhachHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhachHang.FormattingEnabled = true;
            this.cboKhachHang.Location = new System.Drawing.Point(475, 28);
            this.cboKhachHang.Name = "cboKhachHang";
            this.cboKhachHang.Size = new System.Drawing.Size(385, 29);
            this.cboKhachHang.TabIndex = 5;
            // 
            // lblKhachHang
            // 
            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.ForeColor = System.Drawing.Color.Black;
            this.lblKhachHang.Location = new System.Drawing.Point(380, 31);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new System.Drawing.Size(95, 21);
            this.lblKhachHang.TabIndex = 4;
            this.lblKhachHang.Text = "Khách hàng:";
            // 
            // txtMaHDB
            // 
            this.txtMaHDB.Location = new System.Drawing.Point(110, 28);
            this.txtMaHDB.Name = "txtMaHDB";
            this.txtMaHDB.Size = new System.Drawing.Size(250, 29);
            this.txtMaHDB.TabIndex = 1;
            // 
            // lblMaHDB
            // 
            this.lblMaHDB.AutoSize = true;
            this.lblMaHDB.ForeColor = System.Drawing.Color.Black;
            this.lblMaHDB.Location = new System.Drawing.Point(20, 31);
            this.lblMaHDB.Name = "lblMaHDB";
            this.lblMaHDB.Size = new System.Drawing.Size(71, 21);
            this.lblMaHDB.TabIndex = 0;
            this.lblMaHDB.Text = "Mã HĐB:";
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(920, 115);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(140, 35);
            this.btnDong.TabIndex = 16;
            this.btnDong.Text = "❌ Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXuatExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnXuatExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXuatExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatExcel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuatExcel.ForeColor = System.Drawing.Color.White;
            this.btnXuatExcel.Location = new System.Drawing.Point(920, 73);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(140, 35);
            this.btnXuatExcel.TabIndex = 15;
            this.btnXuatExcel.Text = "📊 Xuất CSV";
            this.btnXuatExcel.UseVisualStyleBackColor = false;
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(920, 31);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(140, 35);
            this.btnLamMoi.TabIndex = 14;
            this.btnLamMoi.Text = "🔄 Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnTimKiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(720, 69);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(140, 35);
            this.btnTimKiem.TabIndex = 13;
            this.btnTimKiem.Text = "🔍 Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // grpKetQua
            // 
            this.grpKetQua.Controls.Add(this.dgvKetQuaHDB);
            this.grpKetQua.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpKetQua.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpKetQua.ForeColor = System.Drawing.Color.Navy;
            this.grpKetQua.Location = new System.Drawing.Point(0, 212);
            this.grpKetQua.Name = "grpKetQua";
            this.grpKetQua.Size = new System.Drawing.Size(1080, 428);
            this.grpKetQua.TabIndex = 2;
            this.grpKetQua.TabStop = false;
            this.grpKetQua.Text = "Danh sách kết quả hóa đơn bán";
            // 
            // dgvKetQuaHDB
            // 
            this.dgvKetQuaHDB.AllowUserToAddRows = false;
            this.dgvKetQuaHDB.AllowUserToDeleteRows = false;
            this.dgvKetQuaHDB.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvKetQuaHDB.BackgroundColor = System.Drawing.Color.White;
            this.dgvKetQuaHDB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKetQuaHDB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKetQuaHDB.Location = new System.Drawing.Point(3, 24);
            this.dgvKetQuaHDB.MultiSelect = false;
            this.dgvKetQuaHDB.Name = "dgvKetQuaHDB";
            this.dgvKetQuaHDB.ReadOnly = true;
            this.dgvKetQuaHDB.RowHeadersWidth = 35;
            this.dgvKetQuaHDB.RowTemplate.Height = 28;
            this.dgvKetQuaHDB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dgvKetQuaHDB.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKetQuaHDB.Size = new System.Drawing.Size(1074, 401);
            this.dgvKetQuaHDB.TabIndex = 0;
            this.dgvKetQuaHDB.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKetQuaHDB_CellDoubleClick);
            // 
            // pnlThongKe
            // 
            this.pnlThongKe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.pnlThongKe.Controls.Add(this.lblTongTien);
            this.pnlThongKe.Controls.Add(this.lblSoLuongKetQua);
            this.pnlThongKe.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlThongKe.Location = new System.Drawing.Point(0, 640);
            this.pnlThongKe.Name = "pnlThongKe";
            this.pnlThongKe.Size = new System.Drawing.Size(1080, 40);
            this.pnlThongKe.TabIndex = 3;
            // 
            // lblTongTien
            // 
            this.lblTongTien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTien.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTongTien.Location = new System.Drawing.Point(700, 9);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(193, 23);
            this.lblTongTien.TabIndex = 1;
            this.lblTongTien.Text = "Tổng doanh thu: 0 VNĐ";
            // 
            // lblSoLuongKetQua
            // 
            this.lblSoLuongKetQua.AutoSize = true;
            this.lblSoLuongKetQua.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoLuongKetQua.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(43)))), ((int)(((byte)(73)))));
            this.lblSoLuongKetQua.Location = new System.Drawing.Point(15, 10);
            this.lblSoLuongKetQua.Name = "lblSoLuongKetQua";
            this.lblSoLuongKetQua.Size = new System.Drawing.Size(262, 21);
            this.lblSoLuongKetQua.TabIndex = 0;
            this.lblSoLuongKetQua.Text = "Tìm thấy: 0 dòng kết quả phù hợp";
            // 
            // frmTimKiemHDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1080, 680);
            this.Controls.Add(this.grpKetQua);
            this.Controls.Add(this.pnlThongKe);
            this.Controls.Add(this.grpTieuChi);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(950, 600);
            this.Name = "frmTimKiemHDB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tra cứu & Tìm kiếm Hóa đơn bán hàng";
            this.Load += new System.EventHandler(this.frmTimKiemHDB_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.grpTieuChi.ResumeLayout(false);
            this.grpTieuChi.PerformLayout();
            this.grpKetQua.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQuaHDB)).EndInit();
            this.pnlThongKe.ResumeLayout(false);
            this.pnlThongKe.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.GroupBox grpTieuChi;
        private System.Windows.Forms.TextBox txtMaHDB;
        private System.Windows.Forms.Label lblMaHDB;
        private System.Windows.Forms.ComboBox cboKhachHang;
        private System.Windows.Forms.Label lblKhachHang;
        private System.Windows.Forms.ComboBox cboNhanVien;
        private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.ComboBox cboMaTivi;
        private System.Windows.Forms.Label lblMaTivi;
        private System.Windows.Forms.CheckBox chkLocNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.GroupBox grpKetQua;
        private System.Windows.Forms.DataGridView dgvKetQuaHDB;
        private System.Windows.Forms.Panel pnlThongKe;
        private System.Windows.Forms.Label lblSoLuongKetQua;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.ToolTip toolTipMain;
    }
}
