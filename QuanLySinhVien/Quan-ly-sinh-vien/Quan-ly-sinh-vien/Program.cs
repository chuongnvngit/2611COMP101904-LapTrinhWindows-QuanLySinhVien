using System;
using System.Windows.Forms;

namespace Quan_ly_sinh_vien
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new FrmDangNhap());
        }
    }
}