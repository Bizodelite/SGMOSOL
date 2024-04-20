using SGMOSOL.ADMIN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGMOSOL.SCREENS
{
    public partial class frmEncryption : Form
    {
        public frmEncryption()
        {
            InitializeComponent();

        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            string strEncrypt = CommonFunctions.Encrypt(txtEncrypt.Text, true);
            lblEncrypted.Text = strEncrypt;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEncryption_Load(object sender, EventArgs e)
        {

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lblEncrypted.Text);
            btnCopy.Text = "Copied";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            btnCopy.Text = "Copy";
            lblEncrypted.Text = "";
            txtEncrypt.Text = "";
            Clipboard.Clear();
        }

        private void btnDec_Click(object sender, EventArgs e)
        {
            string strDecrypt = CommonFunctions.Decrypt(txtDecrypt.Text, true);
            lblDecrypted.Text = strDecrypt;
        }

        private void btncpydec_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lblDecrypted.Text);
            btncpydec.Text = "Copied";
        } 
    }
}
