using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace tv_store.Helpers
{
    public static class ExcelExportHelper
    {
        /// <summary>
        /// Xuất dữ liệu DataGridView ra file Excel theo chuẩn thư viện Microsoft.Office.Interop.Excel
        /// (Áp dụng kiến thức môn Lập trình trực quan - ThS. Nguyễn Thu Hường)
        /// </summary>
        public static void ExportDataGridViewToExcel(DataGridView dgv, string tieuDeBaoCao, string defaultFileName, string sheetName = "DanhSach")
        {
            if (dgv == null || dgv.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất ra file Excel!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Workbook (*.xlsx)|*.xlsx|Excel 97-2003 (*.xls)|*.xls";
                sfd.FileName = defaultFileName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                Excel.Application exApp = null;
                Excel.Workbook exBook = null;
                Excel.Worksheet exSheet = null;
                Excel.Range exRange = null;

                try
                {
                    // Bước 3: Khởi tạo đối tượng COM Interop Excel
                    exApp = new Excel.Application();
                    exBook = exApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                    exSheet = (Excel.Worksheet)exBook.Worksheets[1];
                    exSheet.Name = string.IsNullOrEmpty(sheetName) ? "Sheet1" : sheetName;
                    exRange = (Excel.Range)exSheet.Cells[1, 1];

                    // Lọc các cột hiển thị
                    var visibleCols = new List<DataGridViewColumn>();
                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        if (col.Visible) visibleCols.Add(col);
                    }

                    int totalCols = visibleCols.Count + 1; // +1 cho cột STT

                    // Bước 4: Định dạng thông tin cửa hàng ở góc trái
                    exRange.Range["A1:D1"].MergeCells = true;
                    exRange.Range["A1"].Value = "CỬA HÀNG KINH DOANH TIVI";
                    exRange.Range["A1"].Font.Bold = true;
                    exRange.Range["A1"].Font.Size = 11;
                    exRange.Range["A1"].Font.Color = ColorTranslator.ToOle(Color.FromArgb(16, 124, 65));

                    exRange.Range["A2:D2"].MergeCells = true;
                    exRange.Range["A2"].Value = "Địa chỉ: Hà Nội - Hotline: 0988.123.456";
                    exRange.Range["A2"].Font.Italic = true;
                    exRange.Range["A2"].Font.Size = 10;

                    // Tiêu đề lớn của báo cáo / danh sách (căn giữa toàn bảng, chữ đỏ, in đậm như slide 13)
                    Excel.Range rTitle = exSheet.Range[exSheet.Cells[4, 1], exSheet.Cells[4, totalCols]];
                    rTitle.MergeCells = true;
                    rTitle.Value = tieuDeBaoCao.ToUpper();
                    rTitle.Font.Bold = true;
                    rTitle.Font.Size = 16;
                    rTitle.Font.Name = "Times New Roman";
                    rTitle.Font.Color = ColorTranslator.ToOle(Color.FromArgb(192, 0, 0));
                    rTitle.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    // Dòng ngày xuất
                    Excel.Range rDate = exSheet.Range[exSheet.Cells[5, 1], exSheet.Cells[5, totalCols]];
                    rDate.MergeCells = true;
                    rDate.Value = "Thời gian xuất: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    rDate.Font.Italic = true;
                    rDate.Font.Size = 10;
                    rDate.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    // Dòng tiêu đề các cột (Dòng 7)
                    int startRow = 7;
                    exSheet.Cells[startRow, 1] = "STT";
                    for (int i = 0; i < visibleCols.Count; i++)
                    {
                        exSheet.Cells[startRow, i + 2] = visibleCols[i].HeaderText;
                    }

                    Excel.Range rHeader = exSheet.Range[exSheet.Cells[startRow, 1], exSheet.Cells[startRow, totalCols]];
                    rHeader.Font.Bold = true;
                    rHeader.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(220, 230, 242));
                    rHeader.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    rHeader.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    rHeader.RowHeight = 26;

                    // Đổ dữ liệu từ DataGridView (Dòng 8 trở đi)
                    int dataStartRow = startRow + 1;
                    int currentRow = dataStartRow;

                    for (int r = 0; r < dgv.Rows.Count; r++)
                    {
                        if (dgv.Rows[r].IsNewRow) continue;

                        exSheet.Cells[currentRow, 1] = (r + 1).ToString();
                        Excel.Range rSTT = (Excel.Range)exSheet.Cells[currentRow, 1];
                        rSTT.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        for (int c = 0; c < visibleCols.Count; c++)
                        {
                            object val = dgv.Rows[r].Cells[visibleCols[c].Index].Value;
                            string valStr = val != null ? val.ToString() : "";
                            string colHeader = visibleCols[c].HeaderText.ToLower();

                            if (val != null && decimal.TryParse(valStr, out decimal numVal) &&
                                (colHeader.Contains("giá") || colHeader.Contains("tiền") || colHeader.Contains("lượng") || colHeader.Contains("tháng")))
                            {
                                exSheet.Cells[currentRow, c + 2] = numVal;
                                if (colHeader.Contains("giá") || colHeader.Contains("tiền"))
                                {
                                    Excel.Range numRange = (Excel.Range)exSheet.Cells[currentRow, c + 2];
                                    numRange.NumberFormat = "#,##0";
                                }
                            }
                            else
                            {
                                exSheet.Cells[currentRow, c + 2] = valStr;
                            }
                        }
                        currentRow++;
                    }

                    int lastDataRow = currentRow - 1;

                    // Kẻ khung viền bảng dữ liệu
                    Excel.Range rAllTable = exSheet.Range[exSheet.Cells[startRow, 1], exSheet.Cells[lastDataRow, totalCols]];
                    rAllTable.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                    // Tự động căn chỉnh độ rộng cột
                    exSheet.Columns.AutoFit();

                    // Bước 5 & 6: Kích hoạt và lưu file
                    exBook.Activate();
                    exBook.SaveAs(sfd.FileName);

                    DialogResult dr = MessageBox.Show(
                        "Xuất file Excel thành công!\nĐường dẫn: " + sfd.FileName + "\n\nBạn có muốn mở file ngay không?",
                        "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (dr == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất file Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Bước 7: Thoát khỏi ứng dụng & Giải phóng tài nguyên COM
                    if (exBook != null)
                    {
                        exBook.Close(false);
                        Marshal.ReleaseComObject(exBook);
                    }
                    if (exApp != null)
                    {
                        exApp.Quit();
                        Marshal.ReleaseComObject(exApp);
                    }
                    if (exSheet != null) Marshal.ReleaseComObject(exSheet);
                    if (exRange != null) Marshal.ReleaseComObject(exRange);

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
        }

        /// <summary>
        /// Xuất Hóa đơn bán hàng chuyên nghiệp ra Excel (bao gồm Thông tin chung, Bảng chi tiết, Tổng tiền, Bằng chữ và Chữ ký)
        /// </summary>
        public static void ExportHoaDonBanToExcel(
            string maHDB,
            DateTime ngayBan,
            string tenNV,
            string tenKH,
            string diaChiKH,
            string dienThoaiKH,
            DataGridView dgvChiTiet,
            decimal tongTien,
            string bangChu)
        {
            if (dgvChiTiet == null || dgvChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Hóa đơn chưa có sản phẩm nào để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Workbook (*.xlsx)|*.xlsx|Excel 97-2003 (*.xls)|*.xls";
                sfd.FileName = "HoaDonBan_" + maHDB.Trim() + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                Excel.Application exApp = null;
                Excel.Workbook exBook = null;
                Excel.Worksheet exSheet = null;

                try
                {
                    exApp = new Excel.Application();
                    exBook = exApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                    exSheet = (Excel.Worksheet)exBook.Worksheets[1];
                    exSheet.Name = "HoaDonBan";

                    // Thông tin cửa hàng
                    Excel.Range rCty = exSheet.Range["A1:D1"];
                    rCty.MergeCells = true;
                    rCty.Value = "CỬA HÀNG KINH DOANH TIVI UY TÍN";
                    rCty.Font.Bold = true;
                    rCty.Font.Size = 12;
                    rCty.Font.Color = ColorTranslator.ToOle(Color.FromArgb(16, 124, 65));

                    Excel.Range rDC = exSheet.Range["A2:D2"];
                    rDC.MergeCells = true;
                    rDC.Value = "Địa chỉ: Số 3 Cầu Giấy, Hà Nội - Hotline: 0988.123.456";
                    rDC.Font.Italic = true;
                    rDC.Font.Size = 10;

                    // Tiêu đề Hóa Đơn
                    Excel.Range rTitle = exSheet.Range["A4:G4"];
                    rTitle.MergeCells = true;
                    rTitle.Value = "HÓA ĐƠN BÁN HÀNG";
                    rTitle.Font.Bold = true;
                    rTitle.Font.Size = 18;
                    rTitle.Font.Name = "Times New Roman";
                    rTitle.Font.Color = ColorTranslator.ToOle(Color.FromArgb(192, 0, 0));
                    rTitle.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    // Thông tin HĐ & Ngày bán
                    Excel.Range rInfoHD = exSheet.Range["A5:G5"];
                    rInfoHD.MergeCells = true;
                    rInfoHD.Value = string.Format("Mã hóa đơn: {0}    |    Ngày bán: {1:dd/MM/yyyy HH:mm}", maHDB, ngayBan);
                    rInfoHD.Font.Italic = true;
                    rInfoHD.Font.Size = 10;
                    rInfoHD.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    // Thông tin khách hàng & nhân viên
                    exSheet.Cells[7, 1] = "Khách hàng:";
                    exSheet.Range["A7"].Font.Bold = true;
                    exSheet.Cells[7, 2] = tenKH;

                    exSheet.Cells[7, 5] = "Điện thoại:";
                    exSheet.Range["E7"].Font.Bold = true;
                    exSheet.Cells[7, 6] = dienThoaiKH;

                    exSheet.Cells[8, 1] = "Địa chỉ:";
                    exSheet.Range["A8"].Font.Bold = true;
                    Excel.Range rDiaChi = exSheet.Range["B8:G8"];
                    rDiaChi.MergeCells = true;
                    rDiaChi.Value = diaChiKH;

                    exSheet.Cells[9, 1] = "Nhân viên bán:";
                    exSheet.Range["A9"].Font.Bold = true;
                    exSheet.Cells[9, 2] = tenNV;

                    // Tiêu đề bảng chi tiết (Dòng 11)
                    int headerRow = 11;
                    string[] headers = new string[] { "STT", "Mã Tivi", "Tên Tivi", "Số lượng", "Đơn giá (VNĐ)", "Giảm giá (%)", "Thành tiền (VNĐ)" };
                    for (int i = 0; i < headers.Length; i++)
                    {
                        exSheet.Cells[headerRow, i + 1] = headers[i];
                    }

                    Excel.Range rHeader = exSheet.Range[exSheet.Cells[headerRow, 1], exSheet.Cells[headerRow, headers.Length]];
                    rHeader.Font.Bold = true;
                    rHeader.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(220, 230, 242));
                    rHeader.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    rHeader.RowHeight = 25;

                    // Dữ liệu chi tiết từ DataGridView
                    int dataStartRow = 12;
                    int currentRow = dataStartRow;

                    for (int r = 0; r < dgvChiTiet.Rows.Count; r++)
                    {
                        if (dgvChiTiet.Rows[r].IsNewRow) continue;

                        exSheet.Cells[currentRow, 1] = (r + 1).ToString();
                        ((Excel.Range)exSheet.Cells[currentRow, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        // Lấy dữ liệu theo tên cột của dgvChiTietHDB
                        string maTV = dgvChiTiet.Rows[r].Cells["colMaTV"].Value?.ToString() ?? "";
                        string tenTV = dgvChiTiet.Rows[r].Cells["colTenTV"].Value?.ToString() ?? "";
                        decimal sl = Convert.ToDecimal(dgvChiTiet.Rows[r].Cells["colSoLuong"].Value ?? 0);
                        decimal donGia = Convert.ToDecimal(dgvChiTiet.Rows[r].Cells["colDonGiaBan"].Value ?? 0);
                        decimal giamGia = Convert.ToDecimal(dgvChiTiet.Rows[r].Cells["colGiamGia"].Value ?? 0);
                        decimal thanhTien = Convert.ToDecimal(dgvChiTiet.Rows[r].Cells["colThanhTien"].Value ?? 0);

                        exSheet.Cells[currentRow, 2] = maTV;
                        exSheet.Cells[currentRow, 3] = tenTV;
                        exSheet.Cells[currentRow, 4] = sl;
                        exSheet.Cells[currentRow, 5] = donGia;
                        exSheet.Cells[currentRow, 6] = giamGia;
                        exSheet.Cells[currentRow, 7] = thanhTien;

                        ((Excel.Range)exSheet.Cells[currentRow, 4]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        ((Excel.Range)exSheet.Cells[currentRow, 5]).NumberFormat = "#,##0";
                        ((Excel.Range)exSheet.Cells[currentRow, 6]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        ((Excel.Range)exSheet.Cells[currentRow, 7]).NumberFormat = "#,##0";

                        currentRow++;
                    }

                    int lastDataRow = currentRow - 1;

                    // Dòng Tổng tiền
                    int totalRow = currentRow;
                    Excel.Range rLabelTong = exSheet.Range[exSheet.Cells[totalRow, 1], exSheet.Cells[totalRow, 6]];
                    rLabelTong.MergeCells = true;
                    rLabelTong.Value = "TỔNG TIỀN THANH TOÁN:";
                    rLabelTong.Font.Bold = true;
                    rLabelTong.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                    Excel.Range rValTong = (Excel.Range)exSheet.Cells[totalRow, 7];
                    rValTong.Value = tongTien;
                    rValTong.Font.Bold = true;
                    rValTong.Font.Size = 12;
                    rValTong.Font.Color = ColorTranslator.ToOle(Color.Red);
                    rValTong.NumberFormat = "#,##0";

                    // Kẻ viền bảng chi tiết + dòng tổng tiền
                    Excel.Range rTableAll = exSheet.Range[exSheet.Cells[headerRow, 1], exSheet.Cells[totalRow, headers.Length]];
                    rTableAll.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                    // Dòng Bằng chữ
                    int wordRow = totalRow + 1;
                    Excel.Range rBangChu = exSheet.Range[exSheet.Cells[wordRow, 1], exSheet.Cells[wordRow, headers.Length]];
                    rBangChu.MergeCells = true;
                    rBangChu.Value = string.IsNullOrEmpty(bangChu) ? "Số tiền viết bằng chữ: " + DatabaseHelper.ChuyenSoSangChu(tongTien) : "Số tiền viết bằng chữ: " + bangChu;
                    rBangChu.Font.Italic = true;
                    rBangChu.Font.Bold = true;

                    // Tự động căn dãn cột
                    exSheet.Columns.AutoFit();

                    // Kích hoạt & Lưu
                    exBook.Activate();
                    exBook.SaveAs(sfd.FileName);

                    DialogResult dr = MessageBox.Show(
                        "Xuất Hóa đơn bán ra Excel thành công!\nĐường dẫn: " + sfd.FileName + "\n\nBạn có muốn mở file ngay không?",
                        "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (dr == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất Hóa đơn ra Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (exBook != null)
                    {
                        exBook.Close(false);
                        Marshal.ReleaseComObject(exBook);
                    }
                    if (exApp != null)
                    {
                        exApp.Quit();
                        Marshal.ReleaseComObject(exApp);
                    }
                    if (exSheet != null) Marshal.ReleaseComObject(exSheet);

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
        }

        /// <summary>
        /// Xuất Hóa đơn nhập hàng chuyên nghiệp ra Excel
        /// </summary>
        public static void ExportHoaDonNhapToExcel(
            string maHDN,
            DateTime ngayNhap,
            string tenNV,
            string tenNCC,
            string diaChiNCC,
            string dienThoaiNCC,
            DataGridView dgvChiTiet,
            decimal tongTien,
            string bangChu)
        {
            if (dgvChiTiet == null || dgvChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Hóa đơn nhập chưa có sản phẩm nào để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Workbook (*.xlsx)|*.xlsx|Excel 97-2003 (*.xls)|*.xls";
                sfd.FileName = "HoaDonNhap_" + maHDN.Trim() + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                Excel.Application exApp = null;
                Excel.Workbook exBook = null;
                Excel.Worksheet exSheet = null;

                try
                {
                    exApp = new Excel.Application();
                    exBook = exApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                    exSheet = (Excel.Worksheet)exBook.Worksheets[1];
                    exSheet.Name = "HoaDonNhap";

                    // Thông tin cửa hàng
                    Excel.Range rCty = exSheet.Range["A1:D1"];
                    rCty.MergeCells = true;
                    rCty.Value = "CỬA HÀNG KINH DOANH TIVI UY TÍN";
                    rCty.Font.Bold = true;
                    rCty.Font.Size = 12;
                    rCty.Font.Color = ColorTranslator.ToOle(Color.FromArgb(16, 124, 65));

                    Excel.Range rDC = exSheet.Range["A2:D2"];
                    rDC.MergeCells = true;
                    rDC.Value = "Địa chỉ: Số 3 Cầu Giấy, Hà Nội - Hotline: 0988.123.456";
                    rDC.Font.Italic = true;
                    rDC.Font.Size = 10;

                    // Tiêu đề Hóa Đơn Nhập
                    Excel.Range rTitle = exSheet.Range["A4:G4"];
                    rTitle.MergeCells = true;
                    rTitle.Value = "HÓA ĐƠN NHẬP HÀNG";
                    rTitle.Font.Bold = true;
                    rTitle.Font.Size = 18;
                    rTitle.Font.Name = "Times New Roman";
                    rTitle.Font.Color = ColorTranslator.ToOle(Color.FromArgb(0, 51, 102));
                    rTitle.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    // Thông tin HĐ & Ngày nhập
                    Excel.Range rInfoHD = exSheet.Range["A5:G5"];
                    rInfoHD.MergeCells = true;
                    rInfoHD.Value = string.Format("Mã hóa đơn nhập: {0}    |    Ngày nhập: {1:dd/MM/yyyy HH:mm}", maHDN, ngayNhap);
                    rInfoHD.Font.Italic = true;
                    rInfoHD.Font.Size = 10;
                    rInfoHD.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    // Thông tin Nhà cung cấp & nhân viên
                    exSheet.Cells[7, 1] = "Nhà cung cấp:";
                    exSheet.Range["A7"].Font.Bold = true;
                    exSheet.Cells[7, 2] = tenNCC;

                    exSheet.Cells[7, 5] = "Điện thoại:";
                    exSheet.Range["E7"].Font.Bold = true;
                    exSheet.Cells[7, 6] = dienThoaiNCC;

                    exSheet.Cells[8, 1] = "Địa chỉ:";
                    exSheet.Range["A8"].Font.Bold = true;
                    Excel.Range rDiaChi = exSheet.Range["B8:G8"];
                    rDiaChi.MergeCells = true;
                    rDiaChi.Value = diaChiNCC;

                    exSheet.Cells[9, 1] = "Nhân viên nhận:";
                    exSheet.Range["A9"].Font.Bold = true;
                    exSheet.Cells[9, 2] = tenNV;

                    // Tiêu đề bảng chi tiết (Dòng 11)
                    int headerRow = 11;
                    string[] headers = new string[] { "STT", "Mã Tivi", "Tên Tivi", "Số lượng", "Đơn giá nhập (VNĐ)", "Giảm giá (%)", "Thành tiền (VNĐ)" };
                    for (int i = 0; i < headers.Length; i++)
                    {
                        exSheet.Cells[headerRow, i + 1] = headers[i];
                    }

                    Excel.Range rHeader = exSheet.Range[exSheet.Cells[headerRow, 1], exSheet.Cells[headerRow, headers.Length]];
                    rHeader.Font.Bold = true;
                    rHeader.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(220, 230, 242));
                    rHeader.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    rHeader.RowHeight = 25;

                    // Dữ liệu chi tiết từ DataGridView
                    int dataStartRow = 12;
                    int currentRow = dataStartRow;

                    for (int r = 0; r < dgvChiTiet.Rows.Count; r++)
                    {
                        if (dgvChiTiet.Rows[r].IsNewRow) continue;

                        exSheet.Cells[currentRow, 1] = (r + 1).ToString();
                        ((Excel.Range)exSheet.Cells[currentRow, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        string maTV = dgvChiTiet.Rows[r].Cells["colMaTV"].Value?.ToString() ?? "";
                        string tenTV = dgvChiTiet.Rows[r].Cells["colTenTV"].Value?.ToString() ?? "";
                        decimal sl = Convert.ToDecimal(dgvChiTiet.Rows[r].Cells["colSoLuong"].Value ?? 0);
                        decimal donGia = Convert.ToDecimal(dgvChiTiet.Rows[r].Cells["colDonGiaNhap"].Value ?? 0);
                        decimal giamGia = Convert.ToDecimal(dgvChiTiet.Rows[r].Cells["colGiamGia"].Value ?? 0);
                        decimal thanhTien = Convert.ToDecimal(dgvChiTiet.Rows[r].Cells["colThanhTien"].Value ?? 0);

                        exSheet.Cells[currentRow, 2] = maTV;
                        exSheet.Cells[currentRow, 3] = tenTV;
                        exSheet.Cells[currentRow, 4] = sl;
                        exSheet.Cells[currentRow, 5] = donGia;
                        exSheet.Cells[currentRow, 6] = giamGia;
                        exSheet.Cells[currentRow, 7] = thanhTien;

                        ((Excel.Range)exSheet.Cells[currentRow, 4]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        ((Excel.Range)exSheet.Cells[currentRow, 5]).NumberFormat = "#,##0";
                        ((Excel.Range)exSheet.Cells[currentRow, 6]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        ((Excel.Range)exSheet.Cells[currentRow, 7]).NumberFormat = "#,##0";

                        currentRow++;
                    }

                    int totalRow = currentRow;
                    Excel.Range rLabelTong = exSheet.Range[exSheet.Cells[totalRow, 1], exSheet.Cells[totalRow, 6]];
                    rLabelTong.MergeCells = true;
                    rLabelTong.Value = "TỔNG TIỀN THANH TOÁN:";
                    rLabelTong.Font.Bold = true;
                    rLabelTong.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                    Excel.Range rValTong = (Excel.Range)exSheet.Cells[totalRow, 7];
                    rValTong.Value = tongTien;
                    rValTong.Font.Bold = true;
                    rValTong.Font.Size = 12;
                    rValTong.Font.Color = ColorTranslator.ToOle(Color.FromArgb(0, 51, 102));
                    rValTong.NumberFormat = "#,##0";

                    // Kẻ viền bảng
                    Excel.Range rTableAll = exSheet.Range[exSheet.Cells[headerRow, 1], exSheet.Cells[totalRow, headers.Length]];
                    rTableAll.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                    // Dòng Bằng chữ
                    int wordRow = totalRow + 1;
                    Excel.Range rBangChu = exSheet.Range[exSheet.Cells[wordRow, 1], exSheet.Cells[wordRow, headers.Length]];
                    rBangChu.MergeCells = true;
                    rBangChu.Value = string.IsNullOrEmpty(bangChu) ? "Số tiền viết bằng chữ: " + DatabaseHelper.ChuyenSoSangChu(tongTien) : "Số tiền viết bằng chữ: " + bangChu;
                    rBangChu.Font.Italic = true;
                    rBangChu.Font.Bold = true;

                    exSheet.Columns.AutoFit();

                    exBook.Activate();
                    exBook.SaveAs(sfd.FileName);

                    DialogResult dr = MessageBox.Show(
                        "Xuất Hóa đơn nhập ra Excel thành công!\nĐường dẫn: " + sfd.FileName + "\n\nBạn có muốn mở file ngay không?",
                        "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (dr == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất Hóa đơn nhập ra Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (exBook != null)
                    {
                        exBook.Close(false);
                        Marshal.ReleaseComObject(exBook);
                    }
                    if (exApp != null)
                    {
                        exApp.Quit();
                        Marshal.ReleaseComObject(exApp);
                    }
                    if (exSheet != null) Marshal.ReleaseComObject(exSheet);

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
        }

        /// <summary>
        /// Phương thức hỗ trợ dự phòng xuất CSV
        /// </summary>
        public static void ExportToCSV(DataGridView dgv, string defaultFileName)
        {
            if (dgv == null || dgv.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV File (*.csv)|*.csv|All Files (*.*)|*.*";
                sfd.FileName = defaultFileName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StringBuilder sb = new StringBuilder();

                        var visibleCols = new List<DataGridViewColumn>();
                        for (int i = 0; i < dgv.Columns.Count; i++)
                        {
                            if (dgv.Columns[i].Visible) visibleCols.Add(dgv.Columns[i]);
                        }

                        // Header
                        for (int i = 0; i < visibleCols.Count; i++)
                        {
                            sb.Append("\"" + visibleCols[i].HeaderText.Replace("\"", "\"\"") + "\"");
                            if (i < visibleCols.Count - 1) sb.Append(",");
                        }
                        sb.AppendLine();

                        // Rows
                        foreach (DataGridViewRow row in dgv.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                for (int i = 0; i < visibleCols.Count; i++)
                                {
                                    string val = row.Cells[visibleCols[i].Index].Value != null ? row.Cells[visibleCols[i].Index].Value.ToString() : "";
                                    sb.Append("\"" + val.Replace("\"", "\"\"") + "\"");
                                    if (i < visibleCols.Count - 1) sb.Append(",");
                                }
                                sb.AppendLine();
                            }
                        }

                        File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                        MessageBox.Show("Xuất file thành công!\nĐường dẫn: " + sfd.FileName,
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
