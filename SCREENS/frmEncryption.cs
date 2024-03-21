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
            lblEncryptValue.Text = strEncrypt;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
