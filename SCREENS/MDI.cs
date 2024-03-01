using SGMOSOL.ADMIN;
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
    }
}
