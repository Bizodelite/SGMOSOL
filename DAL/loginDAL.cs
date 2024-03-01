using SGMOSOL.ADMIN;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGMOSOL.DAL
{
    public class loginDAL
    {
        string connectionString = ConfigurationManager.ConnectionStrings["strConnection"].ConnectionString;
        CommonFunctions commonFunctions = new CommonFunctions();

        public string GetPwdDetails(string uid)
        {
            string pwd = "";
            string query = "select User_Login_Password from SEC_user_mst_t where User_Login_Name='" + uid + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        pwd = result.ToString();
                    }
                }
            }
            return pwd;
        }
        public string GetUserStatus(string uid)
        {
            string status = "";
            string query = "select User_Status from SEC_user_mst_t where User_Login_Name='" + uid + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        status = result.ToString();
                    }
                }
            }
            return status;
        }
        public int getUserId(string userName)
        {
            int userId = 0;
            string query = "  select User_Id from sec_user_mst_t where User_Login_Name='" + userName + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value && result != null && int.TryParse(result.ToString(), out int id))
                    {
                        userId = id;
                    }
                    connection.Close();
                }
            }
            return userId;
        }
        public string GetUserDetails(string userName)
        {
            string uName = "";
            string query = "select User_Login_Name from SEC_user_mst_t where User_Login_Name='" + userName + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        uName = result.ToString();
                    }
                }
            }
            return uName;
        }
        public int updatePassword(string uid, string pwd)
        {
            int status = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "Update SEC_user_mst_t set User_Login_Password='" + pwd + "' where User_Login_Name='" + uid + "'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    object result = command.ExecuteNonQuery();
                    if (result != DBNull.Value && result != null)
                    {
                        status = (int)result;
                    }
                }
            }
            return status;
        }
        public bool CheckDateTime()
        {
            bool status = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "select GETDATE()";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        DateTime dt = (DateTime)result;
                        DateTime dd = DateTime.Now;
                        if (dt.Year == dd.Year && dt.Month == dd.Month && dt.Day == dd.Day && dt.Hour == dd.Hour)
                            status = true;
                        else
                            status = false;
                    }
                }
            }
            return status;
        }
    }
}
