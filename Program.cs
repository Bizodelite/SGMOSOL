using SGMOSOL.SCREENS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGMOSOL
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmLogin loginForm=new frmLogin();
            loginForm.WindowState = FormWindowState.Maximized;
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new MDI());
            }
            else {
                Application.Exit();
            }

        }
    }
}
