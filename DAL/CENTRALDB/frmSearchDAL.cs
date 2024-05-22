using SGMOSOL.ADMIN;
using SGMOSOL.DataModel;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGMOSOL.DataSet;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using CrystalDecisions.ReportAppServer.DataDefModel;
using System.IO;


namespace SGMOSOL.DAL.CENTRALDB
{
    public class frmSearchDAL
    {
        string connectionString = ConfigurationManager.ConnectionStrings["strConnectionCB"].ConnectionString;
        CommonFunctions commonFunctions = new CommonFunctions();
        DataTable dt = null;

        public DataTable checkRecordinCB(string tableParam, string searchType)
        
        {
            dt = new DataTable();
            string strTableName = null;
            string strQuery = null;
            if (tableParam.Length > 0)
            {

                if (tableParam.Length >= 3)
                {
                    string firstThreeDigits = tableParam.Substring(0, 3);
                    strTableName = firstThreeDigits + "Bhakt";
                }

            }
            if (searchType == "BARCODE")
            {
                strQuery = "select BHAKTID as [BHAKT ID],BARCODE, NAME, MOBILE, AADHARNO as [ADHAR NUMBER] from [" + strTableName + "] where BARCODE ='" + tableParam + "'";
            }
            if (searchType == "MOBILE")
            {
                strQuery = "select BHAKTID as [BHAKT ID],BARCODE, NAME, MOBILE, AADHARNO as [ADHAR NUMBER] from [" + strTableName + "] where MOBILE ='" + tableParam + "'";
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(strQuery, connection);
                    adapter.Fill(dt);
                    connection.Close();
                    DataColumn tableName = new DataColumn("BHAKT_TABLE", typeof(string));
                    dt.Columns.Add(tableName);
                    foreach (DataRow row in dt.Rows)
                    {
                        row["BHAKT_TABLE"] = strTableName;
                    }
                }
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dt;
        }
        public DataTable LoadTransaction(string tableName, string barcode)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("[SP_GETUSERDATA]", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TABLENAME", tableName);
                        command.Parameters.AddWithValue("@BARCODE", barcode);
                        using (SqlDataAdapter da = new SqlDataAdapter(command))
                        {
                            da.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);

            }
            return dataTable;
        }

        public string CreateAndInsertBarcode(object data)
        {
            string strBarcode = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("[SP_INSERTCENTRALDATA]", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        {
                            if (data is dengiReceiptModel obj)
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@PREFIX", obj.Prefix);
                                command.Parameters.AddWithValue("@ADHAR_NO", obj.Doc_Detail);
                                command.Parameters.AddWithValue("@MOBILE_NO", obj.contact);
                                command.Parameters.AddWithValue("@NAME", obj.Name);
                                command.Parameters.AddWithValue("@GOTRA",obj.gotra);
                                command.Parameters.AddWithValue("@DISTRICT", obj.gotra);
                                command.Parameters.AddWithValue("@TALUKA", obj.gotra);
                                command.Parameters.AddWithValue("@STATE", obj.STATE);
                                command.Parameters.AddWithValue("@PINCODE", obj.PinCode);
                                command.Parameters.AddWithValue("@ADDRESS", obj.Address);
                                command.Parameters.AddWithValue("@STATUS", 1);
                                command.Parameters.AddWithValue("@REMARK", null);
                                command.Parameters.AddWithValue("@ENTEREDBY", UserInfo.UserName);
                                command.Parameters.AddWithValue("@TABLENAME", obj.TableName);
                                SqlParameter barcodeParam = new SqlParameter("@BARCODE", SqlDbType.VarChar, 50);
                                barcodeParam.Direction = ParameterDirection.Output;
                                command.Parameters.Add(barcodeParam);
                               //connection.Open();
                                command.ExecuteNonQuery();
                                strBarcode = barcodeParam.Value.ToString();
                       ;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }

            return strBarcode;
        }
    }

}
