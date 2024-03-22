using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGMOSOL.Custom_User_Contols
{
    public partial class InputBox : Form
    {
        public InputBox()
        {
            InitializeComponent();
        }
        public string MessageText
        {
            get { return lblAlert.Text; }
            set { lblAlert.Text = value; }
        }
        public string keyValue
        {
            get { return txtkey.Text; }
            set { txtkey.Text = value; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtkey.Text == "")
            {
                lblerr.Text = "Please Enter Key";
            }
            if (txtkey.Text != "159")
            {
                lblerr.Text = "Wrong Key !!!";
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void InputBox_Load(object sender, EventArgs e)
        {

        }
    }
}
