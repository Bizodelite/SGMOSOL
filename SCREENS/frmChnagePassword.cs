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
        bool OldUser = false;
        public frmChnagePassword(bool oldUser = false)
        {
            InitializeComponent();
            login = new LoginBAL();
            commonFunctions = new CommonFunctions();
            OldUser = oldUser;
            this.AcceptButton = btnchange;
        }

        private void btnchange_Click(object sender, EventArgs e)
        {
            string isError = null;
            int uID = 0;
            int ctrMachID = 0;
            DataTable dtuser = new DataTable();
            uID = login.getUserId(txtUserName.Text.Trim());
            dtuser = commonFunctions.getUserAllDetails(UserInfo.MachineId, uID);
            
            foreach (DataRow row in dtuser.Rows)
            {
              UserInfo.ctrMachID = Convert.ToInt32(row["CTR_MACH_ID"]);
            }
            if (lbloldpwderror.Text == "")
            {
                isError = commonFunctions.IsPasswordValid(txtNewPassword.Text);
                if (isError == "")
                {
                    string newPassword = CommonFunctions.Encrypt(txtNewPassword.Text, true);
                    lblError.Text = "";
                    if (txtOldPassword.Text != txtNewPassword.Text)
                    {
                        DataTable dtckMachineAccess = new DataTable();
                        dtckMachineAccess = login.chkmachneAccess(UserInfo.UserId);
                        if (dtckMachineAccess.Rows.Count > 0)
                        {
                            int status = login.updatePassword(txtUserName.Text, CommonFunctions.Encrypt(txtNewPassword.Text, true));
                            if (status == 1)
                            {
                                status = login.InsertUser_PassWord_Logs(txtNewPassword.Text);
                                if (status == 0)
                                {
                                    MessageBox.Show("Password Updated Successfully!!!");
                                    MDI mdiParentForm = Application.OpenForms.OfType<MDI>().FirstOrDefault();

                                    if (mdiParentForm != null)
                                    {
                                        this.Close();
                                        frmLogin loginForm = new frmLogin();
                                        loginForm.WindowState = FormWindowState.Maximized;
                                        loginForm.ShowDialog();
                                    }
                                    else { 
                                    
                                    }
                                   
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
                            MessageBox.Show("You dont have access for this machine");
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

        private void frmChnagePassword_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
