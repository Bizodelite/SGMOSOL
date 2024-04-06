using SGMOSOL.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGMOSOL.BAL
{

    public class LoginBAL
    {
        loginDAL login = new loginDAL();
        public string GetPwdDetails(string uid, bool? olduser=null)
        {
            return login.GetPwdDetails(uid, olduser);
        }
        public string GetUserStatus(string uid)
        {
            return login.GetUserStatus(uid);
        }
        public string getDesktopPassword(string uid)
        {
            return login.getDesktopPassword(uid);
        }
        public int getUserId(string userName)
        {
            return login.getUserId(userName);
        }
        public string GetUserDetails(string uid)
        {
            return login.GetUserDetails(uid);
        }
        public int updatePassword(string uid, string pwd)
        {
            return login.updatePassword(uid, pwd);
        }
        public bool CheckDateTime()
        {
            return login.CheckDateTime();
        }
        public int InsertUser_Login_details()
        {
            return login.InsertUser_Login_details();
        }
        public int updateUser_Login_Details()
        {
            return login.UpdateUser_Login_details();
        }
        public int DeleteUser_Login_details()
        {
            return login.DeleteUser_Login_details();
        }
        public DataTable GetLoggedInUser(int uid)
        {
            return login.GetLoggedInUser(uid);
        }
    }
}
