using System;
using System.Windows.Forms;
using tv_store.Modules._04_HeThong_BaoCao;

namespace tv_store
{
    internal static class Program
    {
        /// <summary>
        /// Điểm khởi chạy ứng dụng.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (frmDangNhap fLogin = new frmDangNhap())
            {
                if (fLogin.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new frmMain());
                }
            }
        }
    }
}
