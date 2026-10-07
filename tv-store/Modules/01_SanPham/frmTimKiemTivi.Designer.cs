namespace tv_store.Modules._01_SanPham
{
    partial class frmTimKiemTivi
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
            this.btnDong = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.txtGiaDen = new System.Windows.Forms.TextBox();
            this.lblGiaDen = new System.Windows.Forms.Label();
            this.txtGiaTu = new System.Windows.Forms.TextBox();
            this.lblGiaTu = new System.Windows.Forms.Label();
            this.txtTuKhoa = new System.Windows.Forms.TextBox();
            this.lblTuKhoa = new System.Windows.Forms.Label();
            this.cboCoManHinh = new System.Windows.Forms.ComboBox();
            this.lblCoManHinh = new System.Windows.Forms.Label();
            this.cboManHinh = new System.Windows.Forms.ComboBox();
            this.lblManHinh = new System.Windows.Forms.Label();
            this.cboHangSX = new System.Windows.Forms.ComboBox();
            this.lblHangSX = new System.Windows.Forms.Label();
            this.grpKetQua = new System.Windows.Forms.GroupBox();
            this.dgvKetQua = new System.Windows.Forms.DataGridView();
            this.pnlThongKe = new System.Windows.Forms.Panel();
            this.lblSoLuongKetQua = new System.Windows.Forms.Label();
            this.toolTipMain = new System.Windows.Forms.ToolTip(this.components);
            this.pnlTop.SuspendLayout();
            this.grpTieuChi.SuspendLayout();
            this.grpKetQua.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).BeginInit();
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
            this.lblTieuDe.Location = new System.Drawing.Point(340, 11);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(392, 41);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "TÌM KIẾM SẢN PHẨM TIVI";
            // 
            // grpTieuChi
            // 
            this.grpTieuChi.Controls.Add(this.btnDong);
            this.grpTieuChi.Controls.Add(this.btnLamMoi);
            this.grpTieuChi.Controls.Add(this.btnTimKiem);
            this.grpTieuChi.Controls.Add(this.txtGiaDen);
            this.grpTieuChi.Controls.Add(this.lblGiaDen);
            this.grpTieuChi.Controls.Add(this.txtGiaTu);
            this.grpTieuChi.Controls.Add(this.lblGiaTu);
            this.grpTieuChi.Controls.Add(this.txtTuKhoa);
            this.grpTieuChi.Controls.Add(this.lblTuKhoa);
            this.grpTieuChi.Controls.Add(this.cboCoManHinh);
            this.grpTieuChi.Controls.Add(this.lblCoManHinh);
            this.grpTieuChi.Controls.Add(this.cboManHinh);
            this.grpTieuChi.Controls.Add(this.lblManHinh);
            this.grpTieuChi.Controls.Add(this.cboHangSX);
            this.grpTieuChi.Controls.Add(this.lblHangSX);
            this.grpTieuChi.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpTieuChi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTieuChi.ForeColor = System.Drawing.Color.Navy;
            this.grpTieuChi.Location = new System.Drawing.Point(0, 50);
            this.grpTieuChi.Name = "grpTieuChi";
            this.grpTieuChi.Size = new System.Drawing.Size(984, 155);
            this.grpTieuChi.TabIndex = 1;
            this.grpTieuChi.TabStop = false;
            this.grpTieuChi.Text = "Tiêu chí tìm kiếm nâng cao";
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(850, 103);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(105, 35);
            this.btnDong.TabIndex = 14;
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
            this.btnLamMoi.Location = new System.Drawing.Point(735, 103);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(105, 35);
            this.btnLamMoi.TabIndex = 13;
            this.btnLamMoi.Text = "Làm &mới";
            this.toolTipMain.SetToolTip(this.btnLamMoi, "Đặt lại tiêu chí tìm kiếm (Alt+M)");
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(620, 103);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(105, 35);
            this.btnTimKiem.TabIndex = 12;
            this.btnTimKiem.Text = "&Tìm kiếm";
            this.toolTipMain.SetToolTip(this.btnTimKiem, "Thực hiện tìm kiếm (Alt+T)");
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // txtGiaDen
            // 
            this.txtGiaDen.Location = new System.Drawing.Point(400, 109);
            this.txtGiaDen.Name = "txtGiaDen";
            this.txtGiaDen.Size = new System.Drawing.Size(180, 33);
            this.txtGiaDen.TabIndex = 11;
            // 
            // lblGiaDen
            // 
            this.lblGiaDen.AutoSize = true;
            this.lblGiaDen.ForeColor = System.Drawing.Color.Black;
            this.lblGiaDen.Location = new System.Drawing.Point(325, 112);
            this.lblGiaDen.Name = "lblGiaDen";
            this.lblGiaDen.Size = new System.Drawing.Size(106, 25);
            this.lblGiaDen.TabIndex = 10;
            this.lblGiaDen.Text = "Đến (VNĐ):";
            // 
            // txtGiaTu
            // 
            this.txtGiaTu.Location = new System.Drawing.Point(125, 109);
            this.txtGiaTu.Name = "txtGiaTu";
            this.txtGiaTu.Size = new System.Drawing.Size(180, 33);
            this.txtGiaTu.TabIndex = 9;
            // 
            // lblGiaTu
            // 
            this.lblGiaTu.AutoSize = true;
            this.lblGiaTu.ForeColor = System.Drawing.Color.Black;
            this.lblGiaTu.Location = new System.Drawing.Point(25, 112);
            this.lblGiaTu.Name = "lblGiaTu";
            this.lblGiaTu.Size = new System.Drawing.Size(122, 25);
            this.lblGiaTu.TabIndex = 8;
            this.lblGiaTu.Text = "Giá từ (VNĐ):";
            // 
            // txtTuKhoa
            // 
            this.txtTuKhoa.Location = new System.Drawing.Point(125, 30);
            this.txtTuKhoa.Name = "txtTuKhoa";
            this.txtTuKhoa.Size = new System.Drawing.Size(455, 33);
            this.txtTuKhoa.TabIndex = 1;
            // 
            // lblTuKhoa
            // 
            this.lblTuKhoa.AutoSize = true;
            this.lblTuKhoa.ForeColor = System.Drawing.Color.Black;
            this.lblTuKhoa.Location = new System.Drawing.Point(25, 33);
            this.lblTuKhoa.Name = "lblTuKhoa";
            this.lblTuKhoa.Size = new System.Drawing.Size(117, 25);
            this.lblTuKhoa.TabIndex = 0;
            this.lblTuKhoa.Text = "Từ khóa Tivi:";
            // 
            // cboCoManHinh
            // 
            this.cboCoManHinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCoManHinh.FormattingEnabled = true;
            this.cboCoManHinh.Location = new System.Drawing.Point(740, 68);
            this.cboCoManHinh.Name = "cboCoManHinh";
            this.cboCoManHinh.Size = new System.Drawing.Size(215, 33);
            this.cboCoManHinh.TabIndex = 7;
            // 
            // lblCoManHinh
            // 
            this.lblCoManHinh.AutoSize = true;
            this.lblCoManHinh.ForeColor = System.Drawing.Color.Black;
            this.lblCoManHinh.Location = new System.Drawing.Point(650, 71);
            this.lblCoManHinh.Name = "lblCoManHinh";
            this.lblCoManHinh.Size = new System.Drawing.Size(124, 25);
            this.lblCoManHinh.TabIndex = 6;
            this.lblCoManHinh.Text = "Cỡ màn hình:";
            // 
            // cboManHinh
            // 
            this.cboManHinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboManHinh.FormattingEnabled = true;
            this.cboManHinh.Location = new System.Drawing.Point(400, 68);
            this.cboManHinh.Name = "cboManHinh";
            this.cboManHinh.Size = new System.Drawing.Size(225, 33);
            this.cboManHinh.TabIndex = 5;
            // 
            // lblManHinh
            // 
            this.lblManHinh.AutoSize = true;
            this.lblManHinh.ForeColor = System.Drawing.Color.Black;
            this.lblManHinh.Location = new System.Drawing.Point(325, 71);
            this.lblManHinh.Name = "lblManHinh";
            this.lblManHinh.Size = new System.Drawing.Size(97, 25);
            this.lblManHinh.TabIndex = 4;
            this.lblManHinh.Text = "Màn hình:";
            // 
            // cboHangSX
            // 
            this.cboHangSX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHangSX.FormattingEnabled = true;
            this.cboHangSX.Location = new System.Drawing.Point(125, 68);
            this.cboHangSX.Name = "cboHangSX";
            this.cboHangSX.Size = new System.Drawing.Size(180, 33);
            this.cboHangSX.TabIndex = 3;
            // 
            // lblHangSX
            // 
            this.lblHangSX.AutoSize = true;
            this.lblHangSX.ForeColor = System.Drawing.Color.Black;
            this.lblHangSX.Location = new System.Drawing.Point(25, 71);
            this.lblHangSX.Name = "lblHangSX";
            this.lblHangSX.Size = new System.Drawing.Size(87, 25);
            this.lblHangSX.TabIndex = 2;
            this.lblHangSX.Text = "Hãng SX:";
            // 
            // grpKetQua
            // 
            this.grpKetQua.Controls.Add(this.dgvKetQua);
            this.grpKetQua.Controls.Add(this.pnlThongKe);
            this.grpKetQua.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpKetQua.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpKetQua.ForeColor = System.Drawing.Color.Navy;
            this.grpKetQua.Location = new System.Drawing.Point(0, 205);
            this.grpKetQua.Name = "grpKetQua";
            this.grpKetQua.Size = new System.Drawing.Size(984, 376);
            this.grpKetQua.TabIndex = 2;
            this.grpKetQua.TabStop = false;
            this.grpKetQua.Text = "Danh sách kết quả tìm thấy";
            // 
            // dgvKetQua
            // 
            this.dgvKetQua.AllowUserToAddRows = false;
            this.dgvKetQua.AllowUserToDeleteRows = false;
            this.dgvKetQua.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKetQua.BackgroundColor = System.Drawing.Color.White;
            this.dgvKetQua.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKetQua.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKetQua.Location = new System.Drawing.Point(3, 29);
            this.dgvKetQua.MultiSelect = false;
            this.dgvKetQua.Name = "dgvKetQua";
            this.dgvKetQua.ReadOnly = true;
            this.dgvKetQua.RowHeadersWidth = 62;
            this.dgvKetQua.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKetQua.Size = new System.Drawing.Size(978, 309);
            this.dgvKetQua.TabIndex = 0;
            // 
            // pnlThongKe
            // 
            this.pnlThongKe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlThongKe.Controls.Add(this.lblSoLuongKetQua);
            this.pnlThongKe.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlThongKe.Location = new System.Drawing.Point(3, 338);
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
            this.lblSoLuongKetQua.Size = new System.Drawing.Size(181, 25);
            this.lblSoLuongKetQua.TabIndex = 0;
            this.lblSoLuongKetQua.Text = "Tìm thấy: 0 kết quả";
            // 
            // frmTimKiemTivi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(984, 581);
            this.Controls.Add(this.grpKetQua);
            this.Controls.Add(this.grpTieuChi);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1000, 620);
            this.Name = "frmTimKiemTivi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tìm kiếm sản phẩm Tivi";
            this.Load += new System.EventHandler(this.frmTimKiemTivi_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.grpTieuChi.ResumeLayout(false);
            this.grpTieuChi.PerformLayout();
            this.grpKetQua.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).EndInit();
            this.pnlThongKe.ResumeLayout(false);
            this.pnlThongKe.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.GroupBox grpTieuChi;
        private System.Windows.Forms.TextBox txtTuKhoa;
        private System.Windows.Forms.Label lblTuKhoa;
        private System.Windows.Forms.ComboBox cboCoManHinh;
        private System.Windows.Forms.Label lblCoManHinh;
        private System.Windows.Forms.ComboBox cboManHinh;
        private System.Windows.Forms.Label lblManHinh;
        private System.Windows.Forms.ComboBox cboHangSX;
        private System.Windows.Forms.Label lblHangSX;
        private System.Windows.Forms.TextBox txtGiaDen;
        private System.Windows.Forms.Label lblGiaDen;
        private System.Windows.Forms.TextBox txtGiaTu;
        private System.Windows.Forms.Label lblGiaTu;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.GroupBox grpKetQua;
        private System.Windows.Forms.DataGridView dgvKetQua;
        private System.Windows.Forms.Panel pnlThongKe;
        private System.Windows.Forms.Label lblSoLuongKetQua;
        private System.Windows.Forms.ToolTip toolTipMain;
    }
}
