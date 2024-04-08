using SGMOSOL.ADMIN;
using SGMOSOL.DataModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SGMOSOL.BAL.LockerBAL;

namespace SGMOSOL.DAL
{
    public class loginDAL
    {
        string connectionString = CommonFunctions.Decrypt(ConfigurationManager.ConnectionStrings["strConnection"].ConnectionString,true);
        CommonFunctions commonFunctions = new CommonFunctions();

        public string GetPwdDetails(string uid)
        {
            string pwd = "";
            string query = "select LOGIN_PASSWORD from SEC_user_mst_t where User_Login_Name='" + uid + "'";
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
                string query = "Update SEC_user_mst_t set LOGIN_PASSWORD='" + pwd + "' where User_Login_Name='" + uid + "'";
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
        public int InsertUser_PassWord_Logs(string pwd)
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_InsertPasswordLog", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UserID", UserInfo.UserId);
                command.Parameters.AddWithValue("@Password", pwd);

                return Convert.ToInt32(clsConnection.ExecuteNonQuery(command));
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -10; // Error
            }
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
        public int InsertUser_Login_details()
        {
            int status = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("DML_SEC_LOGIN_DETAILS", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@FLAG", "I");
                        command.Parameters.AddWithValue("@USER_ID", UserInfo.UserId);
                        command.Parameters.AddWithValue("@LOGGED_OUT_TIME", null);
                        command.Parameters.AddWithValue("@LOGIN_TYPE", null);
                        command.Parameters.AddWithValue("@ENTERED_BY", UserInfo.UserName);
                        command.Parameters.AddWithValue("@CounterName", UserInfo.Counter_Name);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);

            }
            return status;
        }

        public int UpdateUser_Login_details()
        {
            int status = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("DML_SEC_LOGIN_DETAILS", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@FLAG", "U");
                        command.Parameters.AddWithValue("@USER_ID", UserInfo.UserId);
                        command.Parameters.AddWithValue("@LOGGED_OUT_TIME", DateTime.Now);
                        command.Parameters.AddWithValue("@LOGIN_TYPE", null);
                        command.Parameters.AddWithValue("@ENTERED_BY", UserInfo.UserName);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);

            }
            return status;
        }
        public int DeleteUser_Login_details()
        {
            int status = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("DML_SEC_LOGIN_DETAILS", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@FLAG", "D");
                        command.Parameters.AddWithValue("@USER_ID", UserInfo.UserId);
                        command.Parameters.AddWithValue("@LOGGED_OUT_TIME", null);
                        command.Parameters.AddWithValue("@LOGIN_TYPE", null);
                        command.Parameters.AddWithValue("@ENTERED_BY", UserInfo.UserName);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);

            }
            return status;
        }

        public DataTable GetLoggedInUser(int uid)
        {
            int Id = 0;
            try
            {
                string query = "select USER_ID from SEC_ACTIVE_LOGIN_DETAILS where USER_ID=" + uid + "";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                        {
                            Id = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch(Exception ex) { }
            return Id;
        }
    }
}
