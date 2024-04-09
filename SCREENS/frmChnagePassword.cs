using SGMOSOL.ADMIN;
using SGMOSOL.BAL;
using SGMOSOL.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGMOSOL.SCREENS
{
    public partial class frmChnagePassword : Form
    {
        LoginBAL login;
        CommonFunctions commonFunctions;
        bool OldUser = false;
        public frmChnagePassword(bool oldUser)
        {
            InitializeComponent();
            login = new LoginBAL();
            commonFunctions = new CommonFunctions();
            OldUser = oldUser;
        }

        private void btnchange_Click(object sender, EventArgs e)
        {
            string isError = null;
            if (lbloldpwderror.Text == "")
            {
                isError = commonFunctions.IsPasswordValid(txtNewPassword.Text);
                if (isError == "")
                {
                    string newPassword = CommonFunctions.Encrypt(txtNewPassword.Text, true);
                    lblError.Text = "";
                    if (txtOldPassword.Text != txtNewPassword.Text)
                    {
                        int status = login.updatePassword(txtUserName.Text, CommonFunctions.Encrypt(txtNewPassword.Text, true));
                        if (status == 1)
                        {
                            MDI mdiParentForm = Application.OpenForms.OfType<MDI>().FirstOrDefault();
                            if (mdiParentForm != null)
                            {
                                MessageBox.Show("Password Updated Successfully");
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Password Updated Successfully!!! Please Login First");
                                this.Hide();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Password can not be same as last password !!!");
                    }
                }
                else
                {
                    lblError.Text = isError;
                }
            }
        }
       
        private void frmChnagePassword_Load(object sender, EventArgs e)
        {
            txtUserName.Text = UserInfo.UserName;

        }
        public void getuserfromLogin(string uid)
        {
            if (txtUserName.Text == "")
            {
                txtUserName.Text = uid;
                this.Show();
            }
        }

        private void txtOldPassword_TextChanged(object sender, EventArgs e)
        {
            string lastPassword = "";

            if (OldUser == true)
            {
                lastPassword = login.GetPwdDetails(txtUserName.Text, true);
            }
            else
            {
                lastPassword = CommonFunctions.Decrypt(login.GetPwdDetails(txtUserName.Text, false), true);
            }


            if (lastPassword != txtOldPassword.Text)
            {
                lbloldpwderror.Text = "Incorrect last password !!!";
            }
            if (lastPassword == "")
            {
                string strlastPassword = login.GetPwdDetails(txtUserName.Text);
            }
            if (lastPassword == txtOldPassword.Text)
            {
                lbloldpwderror.Text = "";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
