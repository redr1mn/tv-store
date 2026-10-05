namespace tv_store.Modules._02_NhapHang
{
    partial class frmTimKiemHDN
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
            this.cboNCC = new System.Windows.Forms.ComboBox();
            this.lblNCC = new System.Windows.Forms.Label();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.txtSoLuongNhap = new System.Windows.Forms.TextBox();
            this.lblSoLuongNhap = new System.Windows.Forms.Label();
            this.cboMaTivi = new System.Windows.Forms.ComboBox();
            this.lblMaTivi = new System.Windows.Forms.Label();
            this.grpKetQua = new System.Windows.Forms.GroupBox();
            this.dgvKetQuaHDN = new System.Windows.Forms.DataGridView();
            this.pnlThongKe = new System.Windows.Forms.Panel();
            this.lblSoLuongKetQua = new System.Windows.Forms.Label();
            this.toolTipMain = new System.Windows.Forms.ToolTip(this.components);
            this.pnlTop.SuspendLayout();
            this.grpTieuChi.SuspendLayout();
            this.grpKetQua.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQuaHDN)).BeginInit();
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
            this.pnlTop.Size = new System.Drawing.Size(984, 50);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.ForeColor = System.Drawing.Color.White;
            this.lblTieuDe.Location = new System.Drawing.Point(290, 11);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(404, 28);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "TÌM KIẾM HÓA ĐƠN NHẬP THEO TIVI (YC 5)";
            // 
            // grpTieuChi
            // 
            this.grpTieuChi.Controls.Add(this.cboNCC);
            this.grpTieuChi.Controls.Add(this.lblNCC);
            this.grpTieuChi.Controls.Add(this.btnDong);
            this.grpTieuChi.Controls.Add(this.btnLamMoi);
            this.grpTieuChi.Controls.Add(this.btnTimKiem);
            this.grpTieuChi.Controls.Add(this.txtSoLuongNhap);
            this.grpTieuChi.Controls.Add(this.lblSoLuongNhap);
            this.grpTieuChi.Controls.Add(this.cboMaTivi);
            this.grpTieuChi.Controls.Add(this.lblMaTivi);
            this.grpTieuChi.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpTieuChi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTieuChi.ForeColor = System.Drawing.Color.Navy;
            this.grpTieuChi.Location = new System.Drawing.Point(0, 50);
            this.grpTieuChi.Name = "grpTieuChi";
            this.grpTieuChi.Size = new System.Drawing.Size(984, 130);
            this.grpTieuChi.TabIndex = 1;
            this.grpTieuChi.TabStop = false;
            this.grpTieuChi.Text = "Tiêu chí tìm kiếm Hóa đơn nhập";
            // 
            // cboNCC
            // 
            this.cboNCC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNCC.FormattingEnabled = true;
            this.cboNCC.Location = new System.Drawing.Point(145, 75);
            this.cboNCC.Name = "cboNCC";
            this.cboNCC.Size = new System.Drawing.Size(320, 25);
            this.cboNCC.TabIndex = 5;
            // 
            // lblNCC
            // 
            this.lblNCC.AutoSize = true;
            this.lblNCC.ForeColor = System.Drawing.Color.Black;
            this.lblNCC.Location = new System.Drawing.Point(30, 78);
            this.lblNCC.Name = "lblNCC";
            this.lblNCC.Size = new System.Drawing.Size(91, 17);
            this.lblNCC.TabIndex = 4;
            this.lblNCC.Text = "Nhà cung cấp:";
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(840, 70);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(105, 35);
            this.btnDong.TabIndex = 8;
            this.btnDong.Text = "Đó&ng";
            this.toolTipMain.SetToolTip(this.btnDong, "Đóng tìm kiếm (Alt+N)");
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(725, 70);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(105, 35);
            this.btnLamMoi.TabIndex = 7;
            this.btnLamMoi.Text = "Làm &mới";
            this.toolTipMain.SetToolTip(this.btnLamMoi, "Đặt lại bộ lọc (Alt+M)");
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(610, 70);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(105, 35);
            this.btnTimKiem.TabIndex = 6;
            this.btnTimKiem.Text = "&Tìm kiếm";
            this.toolTipMain.SetToolTip(this.btnTimKiem, "Tìm kiếm theo tiêu chí (Alt+T)");
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // txtSoLuongNhap
            // 
            this.txtSoLuongNhap.Location = new System.Drawing.Point(630, 27);
            this.txtSoLuongNhap.Name = "txtSoLuongNhap";
            this.txtSoLuongNhap.Size = new System.Drawing.Size(160, 24);
            this.txtSoLuongNhap.TabIndex = 3;
            // 
            // lblSoLuongNhap
            // 
            this.lblSoLuongNhap.AutoSize = true;
            this.lblSoLuongNhap.ForeColor = System.Drawing.Color.Black;
            this.lblSoLuongNhap.Location = new System.Drawing.Point(510, 30);
            this.lblSoLuongNhap.Name = "lblSoLuongNhap";
            this.lblSoLuongNhap.Size = new System.Drawing.Size(114, 17);
            this.lblSoLuongNhap.TabIndex = 2;
            this.lblSoLuongNhap.Text = "Số lượng nhập >=:";
            // 
            // cboMaTivi
            // 
            this.cboMaTivi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaTivi.FormattingEnabled = true;
            this.cboMaTivi.Location = new System.Drawing.Point(145, 27);
            this.cboMaTivi.Name = "cboMaTivi";
            this.cboMaTivi.Size = new System.Drawing.Size(320, 25);
            this.cboMaTivi.TabIndex = 1;
            // 
            // lblMaTivi
            // 
            this.lblMaTivi.AutoSize = true;
            this.lblMaTivi.ForeColor = System.Drawing.Color.Black;
            this.lblMaTivi.Location = new System.Drawing.Point(30, 30);
            this.lblMaTivi.Name = "lblMaTivi";
            this.lblMaTivi.Size = new System.Drawing.Size(89, 17);
            this.lblMaTivi.TabIndex = 0;
            this.lblMaTivi.Text = "Sản phẩm Tivi:";
            // 
            // grpKetQua
            // 
            this.grpKetQua.Controls.Add(this.dgvKetQuaHDN);
            this.grpKetQua.Controls.Add(this.pnlThongKe);
            this.grpKetQua.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpKetQua.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpKetQua.ForeColor = System.Drawing.Color.Navy;
            this.grpKetQua.Location = new System.Drawing.Point(0, 180);
            this.grpKetQua.Name = "grpKetQua";
            this.grpKetQua.Size = new System.Drawing.Size(984, 401);
            this.grpKetQua.TabIndex = 2;
            this.grpKetQua.TabStop = false;
            this.grpKetQua.Text = "Danh sách Hóa đơn nhập tìm thấy";
            // 
            // dgvKetQuaHDN
            // 
            this.dgvKetQuaHDN.AllowUserToAddRows = false;
            this.dgvKetQuaHDN.AllowUserToDeleteRows = false;
            this.dgvKetQuaHDN.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKetQuaHDN.BackgroundColor = System.Drawing.Color.White;
            this.dgvKetQuaHDN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKetQuaHDN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKetQuaHDN.Location = new System.Drawing.Point(3, 20);
            this.dgvKetQuaHDN.MultiSelect = false;
            this.dgvKetQuaHDN.Name = "dgvKetQuaHDN";
            this.dgvKetQuaHDN.ReadOnly = true;
            this.dgvKetQuaHDN.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKetQuaHDN.Size = new System.Drawing.Size(978, 343);
            this.dgvKetQuaHDN.TabIndex = 0;
            // 
            // pnlThongKe
            // 
            this.pnlThongKe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlThongKe.Controls.Add(this.lblSoLuongKetQua);
            this.pnlThongKe.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlThongKe.Location = new System.Drawing.Point(3, 363);
            this.pnlThongKe.Name = "pnlThongKe";
            this.pnlThongKe.Size = new System.Drawing.Size(978, 35);
            this.pnlThongKe.TabIndex = 1;
            // 
            // lblSoLuongKetQua
            // 
            this.lblSoLuongKetQua.AutoSize = true;
            this.lblSoLuongKetQua.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoLuongKetQua.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblSoLuongKetQua.Location = new System.Drawing.Point(15, 9);
            this.lblSoLuongKetQua.Name = "lblSoLuongKetQua";
            this.lblSoLuongKetQua.Size = new System.Drawing.Size(125, 17);
            this.lblSoLuongKetQua.TabIndex = 0;
            this.lblSoLuongKetQua.Text = "Tìm thấy: 0 kết quả";
            // 
            // frmTimKiemHDN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(984, 581);
            this.Controls.Add(this.grpKetQua);
            this.Controls.Add(this.grpTieuChi);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1000, 620);
            this.Name = "frmTimKiemHDN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tìm kiếm Hóa đơn nhập";
            this.Load += new System.EventHandler(this.frmTimKiemHDN_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.grpTieuChi.ResumeLayout(false);
            this.grpTieuChi.PerformLayout();
            this.grpKetQua.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQuaHDN)).EndInit();
            this.pnlThongKe.ResumeLayout(false);
            this.pnlThongKe.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.GroupBox grpTieuChi;
        private System.Windows.Forms.ComboBox cboNCC;
        private System.Windows.Forms.Label lblNCC;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.TextBox txtSoLuongNhap;
        private System.Windows.Forms.Label lblSoLuongNhap;
        private System.Windows.Forms.ComboBox cboMaTivi;
        private System.Windows.Forms.Label lblMaTivi;
        private System.Windows.Forms.GroupBox grpKetQua;
        private System.Windows.Forms.DataGridView dgvKetQuaHDN;
        private System.Windows.Forms.Panel pnlThongKe;
        private System.Windows.Forms.Label lblSoLuongKetQua;
        private System.Windows.Forms.ToolTip toolTipMain;
    }
}
