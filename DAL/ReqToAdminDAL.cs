using SGMOSOL.ADMIN;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SGMOSOL.DAL
{
    public class ReqToAdminDAL
    {
        string connectionString = CommonFunctions.Decrypt(ConfigurationManager.ConnectionStrings["strConnection"].ConnectionString, true);
        CommonFunctions commonFunctions = new CommonFunctions();
        public string getReqNumber()
        {
            string strReqNumber = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT MAX(SERIAL_NO) FROM MESS_REQ_TO_ADMIN_MST_T WHERE CTR_MACH_ID=" + UserInfo.ctrMachID + " AND FY_ID=" + UserInfo.fy_id + " AND LOC_ID=" + UserInfo.Loc_id + "";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                        {
                            strReqNumber = result.ToString();
                        }
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);

            }
            return strReqNumber;
        
        }
        public DataTable getItemCode()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ITEM_ID,ITEM_CODE FROM MESS_ITEM_MST_T WHERE ACTIVE=1 ";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(dt);
                connection.Close();
            }
            return dt;
        }
        public string getItemName(int ItemId)
        {
            string strItemName = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "  SELECT ITEM_TITLE FROM  MESS_ITEM_MST_T WHERE ITEM_ID=" + ItemId + "";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                        {
                            strItemName = result.ToString();
                        }
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);

            }
            return strItemName;

        }

    }
}
