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
        frmCalculation frmCalculation;
        CommonFunctions cm;
        public MDI()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            cm = new CommonFunctions();
            this.Text = cm.getFormTitle() + " / " + Application.ProductVersion;
        }

        private void MDI_Load(object sender, EventArgs e)
        {

        }

        private void dengiToolStripMenuItem_Click(object sender, EventArgs e)
        {

            
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChnagePassword = Application.OpenForms.OfType<frmChnagePassword>().FirstOrDefault();
            if (frmChnagePassword == null)
            {
                frmChnagePassword = new frmChnagePassword();
                frmChnagePassword.StartPosition = FormStartPosition.CenterParent;
                frmChnagePassword.MdiParent = this;
                frmChnagePassword.WindowState = FormWindowState.Maximized;
                frmChnagePassword.Show();
            }
            frmuserDengi = Application.OpenForms.OfType<frmUserDengi>().FirstOrDefault();
            if (frmuserDengi != null)
            {
                frmuserDengi.Close();
            }
        }

        private void printReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmbhojnalayaPrintReceipt = Application.OpenForms.OfType<frmBhojnalayaPrintReceipt>().FirstOrDefault();
            if (frmbhojnalayaPrintReceipt == null)
            {
                frmbhojnalayaPrintReceipt = new frmBhojnalayaPrintReceipt();
                frmbhojnalayaPrintReceipt.StartPosition = FormStartPosition.CenterParent;
                frmbhojnalayaPrintReceipt.MdiParent = this;
                frmbhojnalayaPrintReceipt.WindowState = FormWindowState.Maximized;
                frmbhojnalayaPrintReceipt.Show();
            }
            frmuserDengi = Application.OpenForms.OfType<frmUserDengi>().FirstOrDefault();
            if (frmuserDengi == null)
            {
                frmuserDengi = new frmUserDengi();
               frmuserDengi.WindowState = FormWindowState.Minimized;
                frmuserDengi.Show();
            }
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
            frmCalculation = Application.OpenForms.OfType<frmCalculation>().FirstOrDefault();
            if (frmCalculation == null)
            {
                frmCalculation = new frmCalculation();
                frmCalculation.StartPosition = FormStartPosition.CenterParent;
                frmCalculation.MdiParent = this;
                frmCalculation.WindowState = FormWindowState.Maximized;
                frmCalculation.Show();
            }
            frmuserDengi = Application.OpenForms.OfType<frmUserDengi>().FirstOrDefault();
            if (frmuserDengi != null)
            { 
                frmuserDengi.Close();
            }
        }

        private void MDI_FormClosing(object sender, FormClosingEventArgs e)
        {
            LoginBAL loginBAL = new LoginBAL();
            loginBAL.updateUser_Login_Details();
            loginBAL.DeleteUser_Login_details();
            // Application.Exit();
            //s System.Diagnostics.Process.Start(Application.ExecutablePath);
        }

        private void encryptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void encryptionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmEncryption frm = new frmEncryption();
            frm.Show();
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void totalDengiReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void totalDengiReportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDengiReceip = Application.OpenForms.OfType<frmDengiReceipt>().FirstOrDefault();
            if (frmDengiReceip == null)
            {
                frmDengiReceip = new frmDengiReceipt();
                frmDengiReceip.StartPosition = FormStartPosition.CenterParent;
                frmDengiReceip.MdiParent = this;
                frmDengiReceip.WindowState = FormWindowState.Maximized;
                frmDengiReceip.Show();
            }
            frmuserDengi = Application.OpenForms.OfType<frmUserDengi>().FirstOrDefault();
            if (frmuserDengi == null)
            {
                frmuserDengi = new frmUserDengi();
                frmuserDengi.WindowState = FormWindowState.Minimized;
                frmuserDengi.Show();
            }
           
        }

        private void totalDengiReportToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmTotalDengiReport frm = new frmTotalDengiReport();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }
    }
}
