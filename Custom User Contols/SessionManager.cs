using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using SGMOSOL.SCREENS;
using SGMOSOL.BAL;

namespace SGMOSOL.Custom_User_Contols
{
    public class SessionManager
    {
       
        int SessionTimeoutMilliseconds;
        private DateTime lastActivityTime;
        private Timer sessionTimer;
        private Form MDIForm;
        public SessionManager(Form frmMdi)
        {
            SessionTimeoutMilliseconds = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Timer"]);
            // Initialize last activity time
            lastActivityTime = DateTime.Now;
            MDIForm = frmMdi;
            // Initialize and configure the session timer
            sessionTimer = new Timer();
            sessionTimer.Interval = SessionTimeoutMilliseconds; // Check every second
            sessionTimer.Tick += SessionTimer_Tick;
        }

        public void StartTimer()
        {
            sessionTimer.Start();
        }

        public void ResetSession()
        {
            lastActivityTime = DateTime.Now;
        }

        private void SessionTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsedTime = DateTime.Now - lastActivityTime;
            if (elapsedTime.TotalMilliseconds >= SessionTimeoutMilliseconds)
            {
                sessionTimer.Stop();
                MessageBox.Show("Session has timed out. You will be redirected to the login page.");
                LoginBAL loginBAL = new LoginBAL();
                loginBAL.updateUser_Login_Details();
                loginBAL.DeleteUser_Login_details();
                if (MDIForm != null)
                {
                    MDIForm.Close();
                }
                MDI login = new MDI();
                login.WindowState = FormWindowState.Maximized;
                login.ShowDialog();
            }
            else
            {
                lastActivityTime = DateTime.Now;
            }
        }
    }
}
