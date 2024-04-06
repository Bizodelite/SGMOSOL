using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using SGMOSOL.ADMIN;
using SGMOSOL.BAL;
using SGMOSOL.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGMOSOL.SCREENS
{
    public partial class frmLogin : Form
    {
        LoginBAL login;
        CommonFunctions cm;
        public int Mach_ID;
        bool oldUser;
        public frmLogin()
        {
            login = new LoginBAL();
            cm = new CommonFunctions();
            InitializeComponent();
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            pnlLogin.Left = (this.ClientSize.Width - pnlLogin.Width) / 2;
            cm = new CommonFunctions();
            this.AcceptButton = btnLogin;
            lblmessage.Text = "";
            cm.GetMacId();
            UserInfo.version = Application.ProductVersion;
            this.Text = cm.getFormTitle() + " / " + UserInfo.version;
            UserInfo.module = "Login";
            txtUser.Focus();
        }

        public string getDesktopPassword(string userName)
        {
            string strDesktopPassword;
            strDesktopPassword = login.getDesktopPassword(userName);
            return strDesktopPassword;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string strValue = CommonFunctions.Encrypt(txtpwd.Text,true);
            string strValue1 = CommonFunctions.Decrypt(txtpwd.Text,true);
            getauthentication();
        }
       
        private void getauthentication()
        {
            string isUser = null;
            string isPwd = null;
            string Status = null;
            int activeUser = 0;

            int uID = 0;
            DataTable dt = new DataTable();
            DataTable dtuser = new DataTable();
            isUser = login.GetUserDetails(txtUser.Text);
            if (isUser != "")
            {
                if (getDesktopPassword(isUser) == "")
                {
                    lblmessage.Text = "Please reset your password!!!";
                    oldUser = true;
                    return;
                }
                isPwd = CommonFunctions.Decrypt(login.GetPwdDetails(isUser), true);
                if (isPwd == txtpwd.Text)
                {
                    if (login.CheckDateTime())
                    {
                        Status = login.GetUserStatus(txtUser.Text);
                        if (Status == "Y")
                        {
                            uID = login.getUserId(isUser);
                            DataTable dtActiveUser = new DataTable();
                            dtActiveUser = login.GetLoggedInUser(uID);
                            if (dtActiveUser.Rows.Count == 0)
                            {
                                UserInfo.UserId = uID;
                                dtuser = cm.getUserAllDetails(UserInfo.MachineId, uID);
                                if (dtuser.Rows.Count > 0)
                                {
                                    UserInfo.serverName = CommonFunctions.Decrypt(System.Configuration.ConfigurationManager.AppSettings["SERVER"].ToString(), true);
                                    foreach (DataRow row in dtuser.Rows)
                                    {
                                        UserInfo.Counter_Name = row["COUNTER_MACHINE_SHORT_NAME"].ToString();
                                        UserInfo.Loc_id = Convert.ToInt32(row["LOC_ID"]);
                                        UserInfo.Dept_id = Convert.ToInt32(row["DEPT_ID"]);
                                        UserInfo.ctrMachID = Convert.ToInt32(row["CTR_MACH_ID"]);
                                    }
                                }
                                else
                                {
                                    lblmessage.Text = "You not have right for Counter and Location";
                                    return;
                                }
                            }
                            else
                            {
                                string loggedCounterName = dtActiveUser.Rows[0]["COUNTER_NAME"].ToString();
                                lblmessage.Text = "User already loggedIn ON " + loggedCounterName + ", Please logout first !!!";
                                return;
                            }
                            UserInfo.UserName = isUser;
                            UserInfo.fy_id = cm.getFYID();
                            UserInfo.Machine_Name = System.Environment.MachineName;
                            lblmessage.Text = "Login Successfull";
                            login.InsertUser_Login_details();
                            LoginStatusMessage(true);

                        }
                        else
                        {
                            lblmessage.Text = "User is not Active User";
                        }
                    }
                    else
                    {
                        lblmessage.Text = "Login failure due to date time miss match";
                    }
                }
                else
                {
                    lblmessage.Text = "Wrong Password !!!";
                }
            }
            else { lblmessage.Text = "Invalid User !!!"; }
        }
        public bool LoginStatusMessage(bool status)
        {
            if (status)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
            return status;
        }


        private void lnkRestPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string isUser = null;
            if (txtUser.Text == "")
            {
                lblmessage.Text = "Please enter user name";
            }
            else
            {
                isUser = login.GetUserDetails(txtUser.Text);
                if (isUser == "")
                {
                    lblmessage.Text = "Invalid UserName";
                }
                else
                {
                    UserInfo.UserName = isUser;
                    frmChnagePassword frmchnagepassword = new frmChnagePassword(oldUser);
                    frmchnagepassword.Show();
                    // this.Close();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
