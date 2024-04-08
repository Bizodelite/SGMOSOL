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
        public frmChnagePassword()
        {
            InitializeComponent();
            login = new LoginBAL();
            commonFunctions = new CommonFunctions();
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
                        int status = login.updatePassword(txtUserName.Text, newPassword);
                        if (status == 1)
                        {
                            status = login.InsertUser_PassWord_Logs(txtNewPassword.Text);
                            if (status == 0)
                            {
                                MessageBox.Show("Password Updated Successfully");
                                MDI mdiParentForm = Application.OpenForms.OfType<MDI>().FirstOrDefault();
                                //if (mdiParentForm != null)
                                //{
                                    this.Close();
                                //}
                                //else
                                //{
                                //    MDI home = new MDI();
                                //    home.Show();
                                //}
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
            string lastPassword = CommonFunctions.Decrypt(login.GetPwdDetails(txtUserName.Text),true);
            if (lastPassword != txtOldPassword.Text)
            {
                lbloldpwderror.Text = "Incorrect last password !!!";
            }
            else
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
