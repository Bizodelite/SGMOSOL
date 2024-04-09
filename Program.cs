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

            // Show the login form
            frmLogin loginForm = new frmLogin();
            loginForm.WindowState = FormWindowState.Maximized;

            // Show the MDI form if login is successful
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // If login is successful, start the MDI parent form
                Application.Run(new MDI());
            }
            else
            {
                // If login fails or the login form is closed, exit the application
                Application.Exit();
            }
        }
    }
}
