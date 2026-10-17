using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace tv_store.Helpers
{
    public static class ExcelExportHelper
    {
        /// <summary>
        /// Xuất các cột đang hiển thị trên bảng dữ liệu sang Excel.
        /// Cần Microsoft Excel được cài trên máy để tạo workbook qua COM.
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
                    exApp = new Excel.Application();
                    exBook = exApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                    exSheet = (Excel.Worksheet)exBook.Worksheets[1];
                    exSheet.Name = string.IsNullOrEmpty(sheetName) ? "Sheet1" : sheetName;
                    exRange = (Excel.Range)exSheet.Cells[1, 1];

                    // Bỏ cột ẩn để file xuất chỉ chứa dữ liệu người dùng đang xem.
                    var visibleCols = new List<DataGridViewColumn>();
                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        if (col.Visible) visibleCols.Add(col);
                    }

                    int totalCols = visibleCols.Count + 1; // +1 cho cột STT

                    exRange.Range["A1:D1"].MergeCells = true;
                    exRange.Range["A1"].Value = "CỬA HÀNG KINH DOANH TIVI";
                    exRange.Range["A1"].Font.Bold = true;
                    exRange.Range["A1"].Font.Size = 11;
                    exRange.Range["A1"].Font.Color = ColorTranslator.ToOle(Color.FromArgb(16, 124, 65));

                    exRange.Range["A2:D2"].MergeCells = true;
                    exRange.Range["A2"].Value = "Địa chỉ: Hà Nội - Hotline: 0988.123.456";
                    exRange.Range["A2"].Font.Italic = true;
                    exRange.Range["A2"].Font.Size = 10;

                    Excel.Range rTitle = exSheet.Range[exSheet.Cells[4, 1], exSheet.Cells[4, totalCols]];
                    rTitle.MergeCells = true;
                    rTitle.Value = tieuDeBaoCao.ToUpper();
                    rTitle.Font.Bold = true;
                    rTitle.Font.Size = 16;
                    rTitle.Font.Name = "Times New Roman";
                    rTitle.Font.Color = ColorTranslator.ToOle(Color.FromArgb(192, 0, 0));
                    rTitle.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    Excel.Range rDate = exSheet.Range[exSheet.Cells[5, 1], exSheet.Cells[5, totalCols]];
                    rDate.MergeCells = true;
                    rDate.Value = "Thời gian xuất: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    rDate.Font.Italic = true;
                    rDate.Font.Size = 10;
                    rDate.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

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
                            // Ghi các cột số thành giá trị số để Excel còn tính tổng và sắp xếp được.
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

                    Excel.Range rAllTable = exSheet.Range[exSheet.Cells[startRow, 1], exSheet.Cells[lastDataRow, totalCols]];
                    rAllTable.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                    exSheet.Columns.AutoFit();

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
                    // Đóng workbook và phiên Excel do hàm tạo, kể cả khi xuất file thất bại.
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

        public static void ExportHoaDonBanToExcel(string maHDB, DateTime ngayBan, string tenNV,
            string tenKH, string diaChiKH, string dienThoaiKH, DataGridView dgvChiTiet, decimal tongTien, string bangChu)
        {
            ExportHoaDonToExcel(true, maHDB, ngayBan, tenNV, tenKH, diaChiKH, dienThoaiKH, dgvChiTiet, tongTien, bangChu);
        }

        public static void ExportHoaDonNhapToExcel(string maHDN, DateTime ngayNhap, string tenNV,
            string tenNCC, string diaChiNCC, string dienThoaiNCC, DataGridView dgvChiTiet, decimal tongTien, string bangChu)
        {
            ExportHoaDonToExcel(false, maHDN, ngayNhap, tenNV, tenNCC, diaChiNCC, dienThoaiNCC, dgvChiTiet, tongTien, bangChu);
        }

        // Hai loại hóa đơn dùng cùng bố cục, chỉ đổi tiêu đề và nhãn đối tác.
        private static void ExportHoaDonToExcel(
            bool hoaDonBan,
            string maHoaDon,
            DateTime ngayLap,
            string tenNV,
            string tenDoiTac,
            string diaChi,
            string dienThoai,
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
                sfd.FileName = (hoaDonBan ? "HoaDonBan_" : "HoaDonNhap_") + maHoaDon.Trim() + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

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
                    exSheet.Name = hoaDonBan ? "HoaDonBan" : "HoaDonNhap";

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

                    Excel.Range rTitle = exSheet.Range["A4:G4"];
                    rTitle.MergeCells = true;
                    rTitle.Value = hoaDonBan ? "HÓA ĐƠN BÁN HÀNG" : "HÓA ĐƠN NHẬP HÀNG";
                    rTitle.Font.Bold = true;
                    rTitle.Font.Size = 18;
                    rTitle.Font.Name = "Times New Roman";
                    rTitle.Font.Color = ColorTranslator.ToOle(Color.FromArgb(0, 51, 102));
                    rTitle.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    Excel.Range rInfoHD = exSheet.Range["A5:G5"];
                    rInfoHD.MergeCells = true;
                    rInfoHD.Value = string.Format("Mã hóa đơn: {0}    |    Ngày lập: {1:dd/MM/yyyy HH:mm}", maHoaDon, ngayLap);
                    rInfoHD.Font.Italic = true;
                    rInfoHD.Font.Size = 10;
                    rInfoHD.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    exSheet.Cells[7, 1] = hoaDonBan ? "Khách hàng:" : "Nhà cung cấp:";
                    exSheet.Range["A7"].Font.Bold = true;
                    exSheet.Cells[7, 2] = tenDoiTac;

                    exSheet.Cells[7, 5] = "Điện thoại:";
                    exSheet.Range["E7"].Font.Bold = true;
                    exSheet.Cells[7, 6] = dienThoai;

                    exSheet.Cells[8, 1] = "Địa chỉ:";
                    exSheet.Range["A8"].Font.Bold = true;
                    Excel.Range rDiaChi = exSheet.Range["B8:G8"];
                    rDiaChi.MergeCells = true;
                    rDiaChi.Value = diaChi;

                    exSheet.Cells[9, 1] = hoaDonBan ? "Nhân viên bán:" : "Nhân viên nhận:";
                    exSheet.Range["A9"].Font.Bold = true;
                    exSheet.Cells[9, 2] = tenNV;

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

                    int dataStartRow = 12;
                    int currentRow = dataStartRow;

                    for (int r = 0; r < dgvChiTiet.Rows.Count; r++)
                    {
                        if (dgvChiTiet.Rows[r].IsNewRow) continue;

                        exSheet.Cells[currentRow, 1] = (r + 1).ToString();
                        ((Excel.Range)exSheet.Cells[currentRow, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        string maTV = dgvChiTiet.Rows[r].Cells["MaTV"].Value?.ToString() ?? "";
                        string tenTV = dgvChiTiet.Rows[r].Cells["TenTV"].Value?.ToString() ?? "";
                        decimal sl = Convert.ToDecimal(dgvChiTiet.Rows[r].Cells["SoLuong"].Value ?? 0);
                        decimal donGia = Convert.ToDecimal(dgvChiTiet.Rows[r].Cells["DonGia"].Value ?? 0);
                        decimal giamGia = Convert.ToDecimal(dgvChiTiet.Rows[r].Cells["GiamGia"].Value ?? 0);
                        decimal thanhTien = Convert.ToDecimal(dgvChiTiet.Rows[r].Cells["ThanhTien"].Value ?? 0);

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
                    rValTong.Font.Color = ColorTranslator.ToOle(Color.Red);
                    rValTong.NumberFormat = "#,##0";

                    Excel.Range rTableAll = exSheet.Range[exSheet.Cells[headerRow, 1], exSheet.Cells[totalRow, headers.Length]];
                    rTableAll.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

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
                        "Xuất hóa đơn ra Excel thành công!\nĐường dẫn: " + sfd.FileName + "\n\nBạn có muốn mở file ngay không?",
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
                    // Chỉ đóng phiên Excel được tạo ở đây; không dùng lại phiên đang mở của người dùng.
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

    }
}
