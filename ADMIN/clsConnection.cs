using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Resources;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;

namespace SGMOSOL.ADMIN
{
    public class clsConnection
    {
        public static string mErrorResult;
        public static string mErrorMessage;
        // Public gSystem As SystemStatus

        public static SqlConnection glbCon = new SqlConnection();
        public static System.Data.SqlClient.SqlTransaction glbTransaction;
        public static OleDbConnection glbACon = new OleDbConnection();
        public static OleDbTransaction glbATransaction;
        public static int lngErrNum;
        public static string glbAccessPath;
        public static SqlConnection glbConImg = new SqlConnection();
        private CommonFunctions cf = new CommonFunctions();

        public static SqlConnection GetConnection()
        {
            SqlConnection GetConnection;
            try
            {
                if (glbCon.State == ConnectionState.Open)
                    GetConnection = glbCon;
                else
                {
                    string StrConnectionStr;
                    //StrConnectionStr = "USER ID=" + mStrUID + ";PASSWORD=" + mStrPWD + ";INITIAL CATALOG=" + mStrSOURCE + ";DATA SOURCE=" + mStrSERVER + ";";
                    StrConnectionStr = CommonFunctions.Decrypt(ConfigurationManager.ConnectionStrings["strConnection"].ConnectionString, true);
                    glbCon = new SqlConnection();
                    glbCon.ConnectionString = StrConnectionStr;
                    glbCon.Open();
                    GetConnection = glbCon;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Connection Failed.", MsgBoxStyle.OkOnly, "Error");
                GetConnection = null/* TODO Change to default(_) if this is not a reference type */;
            }
            return GetConnection;
        }

        public static long ExecuteNonQuery(SqlCommand objCmd)
        {
            try
            {
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Connection = glbCon;
                objCmd.Transaction = glbTransaction;
                lngErrNum = objCmd.ExecuteNonQuery();
                if (lngErrNum == 0)
                    return -6;
                else
                    return 0;
            }
            catch (SqlException ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -(ex.Number);

            }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -1;
            }
        }
        public static long ExecuteScalar(SqlCommand objCmd)
        {
            try
            {
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Connection = glbCon;
                objCmd.Transaction = glbTransaction;
                object result = objCmd.ExecuteScalar();
                if (result != null)
                {
                    lngErrNum = Convert.ToInt32(result);
                }
                else
                {
                    lngErrNum = -1;
                }
            }
            catch (SqlException ex)
            {
                lngErrNum = -(ex.Number);
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            catch (Exception ex)
            {
                lngErrNum = -1;
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return lngErrNum;
        }
        public static System.Data.DataTable ExecuteReader(SqlCommand ObjCmd)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();
            try
            {
                ObjCmd.Transaction = glbTransaction;
                SqlDataReader ObjDr = ObjCmd.ExecuteReader();
                dataTable.Load(ObjDr);
            }
            catch (Exception e)
            {
                InsertErrorLog(e.Message, UserInfo.module, UserInfo.version);
            }
            return dataTable;
        }
        public static void InsertErrorLog(string errorMsg, string module, string version)
        {
            try
            {
                SqlCommand command = new SqlCommand("DML_ERROR_LOG", GetConnection());
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Loc_Id", UserInfo.Loc_id);
                        command.Parameters.AddWithValue("@Dept_Id", UserInfo.Dept_id);
                        command.Parameters.AddWithValue("@Ctr_mac_Id", UserInfo.ctrMachID);
                        command.Parameters.AddWithValue("@User_Id", UserInfo.UserId);
                        command.Parameters.AddWithValue("@Error_msg", errorMsg);
                        command.Parameters.AddWithValue("@Module", module);
                        command.Parameters.AddWithValue("@Version", version);
                        ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }
        
        public static long DMLStoredProc(string strCmd, SqlCommand objCmd)
        {
            // Dim objCmd As New SqlClient.SqlCommand()
            try
            {
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Connection = glbCon;
                objCmd.Transaction = glbTransaction;
                objCmd.CommandText = strCmd;
                lngErrNum = objCmd.ExecuteNonQuery();
                if (lngErrNum == 0)
                    lngErrNum = -6;
                else
                    lngErrNum = 0;
            }
            catch (SqlException ex)
            {
                lngErrNum = -(ex.Number);
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            catch (Exception ex)
            {
                lngErrNum = -1;
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return lngErrNum;
        }

    }

}
