using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using SGMOSOL.ADMIN;
using SGMOSOL.BAL;
using SGMOSOL.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
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
        bool deskpwd = false;
        string strformType = "";
        public static string SystemHDDModelNo;
        public static string SystemHDDSerialNo;
        public static string SystemMacID;
        public frmChnagePassword(bool deskpwd = false, string formType = null)
        {
            InitializeComponent();
            login = new LoginBAL();
            commonFunctions = new CommonFunctions();
            deskpwd = deskpwd;
            this.AcceptButton = btnchange;
            strformType = formType;
        }

        private void btnchange_Click(object sender, EventArgs e)
        {
            string isError = null;
            int uID = 0;
            int ctrMachID = 0;
            DataTable dtuser = new DataTable();
            uID = login.getUserId(txtUserName.Text.Trim());
            dtuser = commonFunctions.getUserAllDetails(UserInfo.MachineId, uID);
            if (dtuser.Rows.Count > 0)
            {
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
                                status = login.InsertUser_PassWord_Logs(txtNewPassword.Text);
                                //MDI mdiParentForm = Application.OpenForms.OfType<MDI>().FirstOrDefault();
                                if (strformType == "Reset")
                                {
                                    MessageBox.Show("Password Updated Successfully!!! Please Login with new password!!!");
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Password Updated Successfully!!! Please Login with new password!!!");
                                    this.Close();
                                    LoginBAL loginBAL = new LoginBAL();
                                    // loginBAL.updateUser_Login_Details();
                                    loginBAL.DeleteUser_Login_details();
                                    frmLogin login = new frmLogin();
                                    login.WindowState = FormWindowState.Maximized;
                                    login.ShowDialog();
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
            else
            {
                MessageBox.Show("You dont have access for this machine");
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
            string strDesktopPwd = "";

            strDesktopPwd = commonFunctions.getDesktopPassword(txtUserName.Text);
            if (strDesktopPwd == "")
            {
                lastPassword = login.GetPwdDetails(txtUserName.Text, false);
            }
           
            else
            {
                lastPassword = CommonFunctions.Decrypt(login.GetPwdDetails(txtUserName.Text, true),true);
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

        private void frmChnagePassword_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
