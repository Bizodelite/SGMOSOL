using SGMOSOL.ADMIN;
using SGMOSOL.BAL;
using SGMOSOL.SCREENS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGMOSOL
{
    public partial class MDI : Form
    {
        frmUserDengi frmuserDengi;
        frmDengiReceipt frmDengiReceip;
        frmChnagePassword frmChnagePassword;
        frmBhojnalayaPrintReceipt frmbhojnalayaPrintReceipt;
        CommonFunctions cm;
        public MDI()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            cm = new CommonFunctions();
            this.Text = cm.getFormTitle()+" / "+Application.ProductVersion;
        }

        private void MDI_Load(object sender, EventArgs e)
        {

        }

        private void dengiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDengiReceip = new frmDengiReceipt();
            frmuserDengi = new frmUserDengi();
            frmDengiReceip.StartPosition = FormStartPosition.CenterParent;
            frmDengiReceip.MdiParent = this;
            frmDengiReceip.WindowState = FormWindowState.Maximized;
            frmDengiReceip.Show();
            //frmDengiReceip.Show();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChnagePassword = new frmChnagePassword();
            frmChnagePassword.StartPosition = FormStartPosition.CenterParent;
            frmChnagePassword.MdiParent = this;
            frmChnagePassword.WindowState = FormWindowState.Maximized;
            frmChnagePassword.Show();
        }

        private void printReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmbhojnalayaPrintReceipt = new frmBhojnalayaPrintReceipt();
            frmbhojnalayaPrintReceipt.StartPosition = FormStartPosition.CenterParent;
            frmbhojnalayaPrintReceipt.MdiParent = this;
            frmbhojnalayaPrintReceipt.WindowState = FormWindowState.Maximized;
            frmbhojnalayaPrintReceipt.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginBAL loginBAL = new LoginBAL();
            loginBAL.updateUser_Login_Details();
            loginBAL.DeleteUser_Login_details();
            Application.Exit();
            System.Diagnostics.Process.Start(Application.ExecutablePath);
        }

        private void calculationFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCalculation frm = new frmCalculation();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void MDI_FormClosing(object sender, FormClosingEventArgs e)
        {
            LoginBAL loginBAL = new LoginBAL();
            loginBAL.updateUser_Login_Details();
            loginBAL.DeleteUser_Login_details();
           // Application.Exit();
           //s System.Diagnostics.Process.Start(Application.ExecutablePath);
        }
    }
}
