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
        public string GetPwdDetails(string uid)
        {
            return login.GetPwdDetails(uid);
        }
        public string GetUserStatus(string uid)
        {
            return login.GetUserStatus(uid);
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
        public int InsertUser_PassWord_Logs(string pwd)
        {
            return login.InsertUser_PassWord_Logs(pwd);
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
