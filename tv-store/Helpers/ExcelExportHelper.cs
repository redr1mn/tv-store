using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace tv_store.Helpers
{
    public static class ExcelExportHelper
    {
        public static void ExportToCSV(DataGridView dgv, string defaultFileName)
        {
            if (dgv.Rows.Count == 0)
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

                        var visibleCols = new System.Collections.Generic.List<DataGridViewColumn>();
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
