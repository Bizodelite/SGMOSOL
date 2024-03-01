using SGMOSOL.DAL;
using System;
using System.Collections.Generic;
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
        public bool CheckDateTime()
        {
            return login.CheckDateTime();
        }
    }
}
