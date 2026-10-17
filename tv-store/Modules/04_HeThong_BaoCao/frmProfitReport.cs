using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using tv_store.Helpers;

namespace tv_store.Modules._04_HeThong_BaoCao
{
    public sealed class frmProfitReport : Form
    {
        private readonly ComboBox period = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 100 };
        private readonly NumericUpDown fromYear = new NumericUpDown { Minimum = 1900, Maximum = 2100, Width = 75, Value = DateTime.Now.Year };
        private readonly NumericUpDown toYear = new NumericUpDown { Minimum = 1900, Maximum = 2100, Width = 75, Value = DateTime.Now.Year };
        private readonly ComboBox product = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 260 };
        private readonly DataGridView grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
        private readonly Chart chart = new Chart { Dock = DockStyle.Fill };
        private readonly Label note = new Label { Dock = DockStyle.Bottom, Height = 62, Padding = new Padding(10), Text = "Doanh thu chưa gồm VAT. Lãi/lỗ gộp = doanh thu sau giảm giá − giá vốn; chưa trừ chi phí vận hành.\nKỳ có giao dịch thiếu giá vốn sẽ để trống giá vốn và lãi/lỗ, không ước tính từ giá nhập hiện tại." };
        private readonly Button export = new Button { Text = "Xuất Excel", AutoSize = true, Enabled = false };

        public frmProfitReport()
        {
            Text = "Doanh thu, lãi lỗ và tăng trưởng bán hàng";
            Size = new Size(1150, 760); MinimumSize = new Size(1000, 650);
            StartPosition = FormStartPosition.CenterParent;
            Font = new Font("Segoe UI", 9.5f);
            var filters = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 85, Padding = new Padding(10), AutoScroll = true };
            period.Items.AddRange(new object[] { "Tháng", "Quý", "Năm" }); period.SelectedIndex = 0;
            var view = new Button { Text = "Xem báo cáo", AutoSize = true };
            foreach (Control control in new Control[] { new Label { Text = "Theo", AutoSize = true }, period,
                new Label { Text = "Từ năm", AutoSize = true }, fromYear, new Label { Text = "Đến năm", AutoSize = true }, toYear,
                product, view, export }) filters.Controls.Add(control);
            var split = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal, SplitterDistance = 250, Height = 550 };
            split.Panel1.Controls.Add(grid); split.Panel2.Controls.Add(chart);
            chart.ChartAreas.Add(new ChartArea("Sales"));
            chart.ChartAreas[0].AxisY.LabelStyle.Format = "#,##0,, 'triệu'";
            chart.ChartAreas[0].AxisX.Interval = 1;
            chart.Legends.Add(new Legend());
            Controls.Add(split); Controls.Add(note); Controls.Add(filters);
            view.Click += (s, e) => RefreshReport();
            export.Click += (s, e) => ExcelExportHelper.ExportDataGridViewToExcel(grid,
                "DOANH THU VÀ LÃI LỖ GỘP (CHƯA VAT) - " + period.Text.ToUpper() + " - " + fromYear.Value + " ĐẾN " + toYear.Value + " - " + product.Text,
                "BaoCaoLaiLo", "LaiLo");
            // Xóa báo cáo cũ khi đổi bộ lọc để tránh xuất dữ liệu không khớp.
            foreach (var input in new[] { period, product }) input.SelectedIndexChanged += (s, e) => InvalidateReport();
            fromYear.ValueChanged += (s, e) => InvalidateReport(); toYear.ValueChanged += (s, e) => InvalidateReport();
            Load += (s, e) =>
            {
                if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
                try
                {
                    var data = DatabaseHelper.Query("SELECT MaTV,TenTV FROM tblTV ORDER BY TenTV");
                    data.Rows.InsertAt(data.NewRow(), 0); data.Rows[0]["MaTV"] = ""; data.Rows[0]["TenTV"] = "Tất cả mặt hàng";
                    product.ValueMember = "MaTV"; product.DisplayMember = "TenTV"; product.DataSource = data;
                    RefreshReport();
                }
                catch (Exception ex) { MessageBox.Show(this, ex.Message, "Không thể tải báo cáo"); }
            };
        }

        private void InvalidateReport() { export.Enabled = false; grid.DataSource = null; chart.Series.Clear(); }

        public static DataTable GetReport(int firstYear, int lastYear, int grouping, string productId)
        {
            if (firstYear < 1900 || lastYear > 2100 || firstYear > lastYear || grouping < 0 || grouping > 2)
                throw new ArgumentException("Khoảng năm hoặc kỳ báo cáo không hợp lệ.");
            string group = grouping == 0 ? "MONTH(h.NgayBan)" : grouping == 1 ? "DATEPART(QUARTER,h.NgayBan)" : "0";
            // SUM bỏ qua NULL, nên chỉ tính giá vốn và lãi/lỗ nếu mọi dòng đều có giá vốn.
            // Doanh thu lấy từ chi tiết sau giảm giá, chưa cộng VAT ở phần đầu hóa đơn.
            return DatabaseHelper.Query("SELECT YEAR(h.NgayBan) AS Nam," + group + " AS Ky," +
                "SUM(c.ThanhTien) AS DoanhThu," +
                "CASE WHEN COUNT(*)=COUNT(c.GiaVonChot) THEN SUM(c.SoLuong*c.GiaVonChot) END AS GiaVon," +
                "CASE WHEN COUNT(*)=COUNT(c.GiaVonChot) THEN SUM(c.ThanhTien-c.SoLuong*c.GiaVonChot) END AS LaiLo," +
                "SUM(c.SoLuong) AS SoLuong,SUM(CASE WHEN c.GiaVonChot IS NULL THEN 1 ELSE 0 END) AS DongThieuGiaVon " +
                "FROM tblHoaDonBan h JOIN tblChiTietHDB c ON h.SoHDB=c.SoHDB " +
                "WHERE h.NgayBan>=@start AND h.NgayBan<@end AND (@product='' OR c.MaTV=@product) " +
                "GROUP BY YEAR(h.NgayBan)" + (grouping == 2 ? "" : "," + group) + " ORDER BY Nam,Ky",
                DatabaseHelper.P("@start", new DateTime(firstYear, 1, 1)), DatabaseHelper.P("@end", new DateTime(lastYear + 1, 1, 1)), DatabaseHelper.P("@product", productId ?? ""));
        }

        private void RefreshReport()
        {
            try
            {
                InvalidateReport();
                var data = GetReport((int)fromYear.Value, (int)toYear.Value, period.SelectedIndex, Convert.ToString(product.SelectedValue));
                var display = new DataTable();
                display.Columns.Add("Ky", typeof(string)); display.Columns.Add("DoanhThu", typeof(decimal));
                display.Columns.Add("GiaVon", typeof(decimal)); display.Columns.Add("LaiLo", typeof(decimal));
                display.Columns.Add("SoLuong", typeof(int)); display.Columns.Add("TangTruong", typeof(decimal));
                display.Columns.Add("DongThieuGiaVon", typeof(int));
                // Bổ sung cả kỳ không có giao dịch để biểu đồ không nối tắt qua kỳ trống.
                decimal? previous = null;
                for (int year = (int)fromYear.Value; year <= (int)toYear.Value; year++)
                {
                    int periods = period.SelectedIndex == 0 ? 12 : period.SelectedIndex == 1 ? 4 : 1;
                    for (int n = 1; n <= periods; n++)
                    {
                        int key = period.SelectedIndex == 2 ? 0 : n;
                        var found = data.Select("Nam=" + year + " AND Ky=" + key);
                        DataRow row = found.Length == 0 ? null : found[0];
                        decimal revenue = row == null ? 0 : Convert.ToDecimal(row["DoanhThu"]);
                        // Không xác định tỷ lệ tăng trưởng khi kỳ trước bằng 0 hoặc chưa có kỳ trước.
                        object growth = previous.HasValue && previous.Value != 0 ? (object)Math.Round((revenue - previous.Value) / previous.Value * 100m, 2) : DBNull.Value;
                        string label = period.SelectedIndex == 2 ? year.ToString() : (period.SelectedIndex == 1 ? "Q" : "T") + n + "/" + year;
                        display.Rows.Add(label, revenue, row == null ? (object)0m : row["GiaVon"], row == null ? (object)0m : row["LaiLo"], row == null ? 0 : row["SoLuong"], growth, row == null ? 0 : row["DongThieuGiaVon"]);
                        previous = revenue;
                    }
                }
                grid.DataSource = display;
                string[] labels = { "Kỳ", "Doanh thu chưa VAT", "Giá vốn", "Lãi/lỗ gộp", "SL bán", "Tăng trưởng DT (%)", "Dòng thiếu giá vốn" };
                for (int i = 0; i < labels.Length; i++) grid.Columns[i].HeaderText = labels[i];
                foreach (string name in new[] { "DoanhThu", "GiaVon", "LaiLo", "TangTruong" }) grid.Columns[name].DefaultCellStyle.Format = "N2";
                var revenueSeries = chart.Series.Add("Doanh thu chưa VAT"); revenueSeries.ChartType = SeriesChartType.Column;
                var profitSeries = chart.Series.Add("Lãi/lỗ gộp"); profitSeries.ChartType = SeriesChartType.Line; profitSeries.BorderWidth = 3;
                foreach (DataRow row in display.Rows)
                {
                    revenueSeries.Points.AddXY(row["Ky"], row["DoanhThu"]);
                    var point = new DataPoint { AxisLabel = row["Ky"].ToString(), YValues = new[] { row["LaiLo"] == DBNull.Value ? 0d : Convert.ToDouble(row["LaiLo"]) }, IsEmpty = row["LaiLo"] == DBNull.Value };
                    profitSeries.Points.Add(point);
                }
                export.Enabled = true;
            }
            catch (Exception ex) { MessageBox.Show(this, ex.Message, "Không thể lập báo cáo"); }
        }
    }
}
