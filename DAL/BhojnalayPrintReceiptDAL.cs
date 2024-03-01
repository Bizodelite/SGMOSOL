using SGMOSOL.ADMIN;
using SGMOSOL.DataModel;
using SGMOSOL.DataSet.MessItemDSTableAdapters;
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

namespace SGMOSOL.DAL
{

    public class BhojnalayPrintReceiptDAL
    {
        string connectionString = ConfigurationManager.ConnectionStrings["strConnection"].ConnectionString;
        CommonFunctions commonFunctions = new CommonFunctions();
        DataTable dt = null;
        public DataTable getItemCode()
        {
            dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ITEM_ID,ITEM_CODE FROM BK_MESS_ITEM_MST_T_BK";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(dt);
                connection.Close();
            }
            return dt;
        }
        public DataTable getItemName()
        {
            dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ITEM_ID,ITEM_TITLE FROM BK_MESS_ITEM_MST_T_BK";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(dt);
                connection.Close();
            }
            return dt;
        }
        public int getItemID(string itemType, string itemValue)
        {
            int itemID = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ITEM_ID FROM BK_MESS_ITEM_MST_T_BK where " + itemType + "='" + itemValue + "'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value && result != null && int.TryParse(result.ToString(), out int ID))
                    {
                        itemID = ID;
                    }
                    connection.Close();
                }
            }
            return itemID;
        }
        public decimal getItemPrice(int itemID)
        {
            decimal Price = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT PRICE FROM BK_MESS_ITEM_MST_T_BK where ITEM_ID=" + itemID + "";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        Price = Convert.ToDecimal(result);
                    }
                    connection.Close();
                }
            }
            return Price;
        }
        public int getMasterReceiptNumber()
        {
            int ReceiptNumber = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT MAX(PRINT_RECEIPT_MST_ID) FROM MESS_PRINT_RECEIPT_MST_T";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value && result != null && int.TryParse(result.ToString(), out int maxId))
                    {
                        ReceiptNumber = maxId + 1;
                    }
                    connection.Close();
                }
            }
            return ReceiptNumber;
        }
        public int InsertBhojnalayPrintReceiptMasterData(object data)
        {
            int status = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_DML_MESS_PRINT_RECEIPT_MST", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if (data is bhojnalayPrintReceiptModel obj)
                        {
                            // command.Parameters.AddWithValue("@PRINT_MST_ID", obj.PrintReceiptMSTID);
                            command.Parameters.AddWithValue("@FLAG", "I");
                            command.Parameters.AddWithValue("@PR_DATE", obj.PR_Date);
                            command.Parameters.AddWithValue("@COM_ID", 9);
                            command.Parameters.AddWithValue("@LOC_ID", UserInfo.Loc_id);
                            command.Parameters.AddWithValue("@DEPT_ID", UserInfo.Dept_id);
                            command.Parameters.AddWithValue("@FY_ID", 11);
                            command.Parameters.AddWithValue("@USER_ID", UserInfo.UserId);
                            command.Parameters.AddWithValue("@CTR_MACH_ID", UserInfo.ctrMachID);
                            command.Parameters.AddWithValue("@GUEST", 0);
                            command.Parameters.AddWithValue("@AMOUNT", obj.TotalAmount);
                            command.Parameters.AddWithValue("@CASH", 0);
                            command.Parameters.AddWithValue("@CHANGE", 0);
                            command.Parameters.AddWithValue("@REMARKS", null);
                            command.Parameters.AddWithValue("@ENTERED_BY", UserInfo.UserName);
                            command.Parameters.AddWithValue("@MODIFIED_BY", "");
                            command.Parameters.AddWithValue("@MACHINE_NAME", "");
                            command.Parameters.AddWithValue("@SERVER_NAME", "");
                            command.Parameters.AddWithValue("@ITEM_NAME", obj.ItemName);
                            command.Parameters.AddWithValue("@NAME", obj.Name);
                            command.Parameters.AddWithValue("@ADDRESS", obj.Address);
                            command.Parameters.AddWithValue("@MOBILE", obj.Mobile);
                            command.Parameters.AddWithValue("@TALUKA", obj.Taluka);
                            command.Parameters.AddWithValue("@DOC_TYPE", obj.DocType);
                            command.Parameters.AddWithValue("@DOC_DETAIL", obj.DocTypeDetail);
                            SqlParameter idParam = new SqlParameter("@SUCCEED", SqlDbType.Int);
                            idParam.Direction = ParameterDirection.Output;
                            command.Parameters.Add(idParam);
                            command.ExecuteNonQuery();
                            status = Convert.ToInt32(command.Parameters["@SUCCEED"].Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return status;
        }

        public int InsertMessItemData(object data)
        {
            int status = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_DML_MESS_PRINT_RECEIPT_DET", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if (data is bhojnalayPrintReceiptModel obj)
                        {
                            // command.Parameters.AddWithValue("@PRINT_MST_ID", obj.PrintReceiptMSTID);
                            command.Parameters.AddWithValue("@PRINT_RECEIPT_MST_ID", obj.PRINT_MST_ID);
                            command.Parameters.AddWithValue("@ITEM_ID", obj.ItemId);
                            command.Parameters.AddWithValue("@PRICE", obj.Price);
                            command.Parameters.AddWithValue("@QTY", obj.Quantity);
                            command.Parameters.AddWithValue("@AMOUNT", obj.Amount);
                            command.Parameters.AddWithValue("@PR_DATE", obj.PR_Date);
                            command.Parameters.AddWithValue("@DEPT_ID", 29);
                            command.Parameters.AddWithValue("@GUEST1", 0);
                            SqlParameter idParam = new SqlParameter("@PRINT_RECEIPT_ID_DET", SqlDbType.Int);
                            idParam.Direction = ParameterDirection.Output;
                            command.Parameters.Add(idParam);
                            command.ExecuteNonQuery();
                            status = Convert.ToInt32(command.Parameters["@PRINT_RECEIPT_ID_DET"].Value);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return status;
        }

        public int getItemIdbyItemName(string ItemName)
        {
            int ItemId = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "select Item_Id from BK_MESS_ITEM_MST_T_BK where ITEM_TITLE='" + ItemName + "'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        ItemId = Convert.ToInt32(result);
                    }
                    connection.Close();
                }
            }
            return ItemId;
        }

        public DataTable getAllData(object data)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_GET_MESS_PRINT_RECEIPT_DATA", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (data is bhojnalayPrintReceiptModel obj)
                    {
                        string fdate = obj.receiptFDate.ToString("yyyy-MM-dd");
                        string ldate = obj.ReceiptLDate.ToString("yyyy-MM-dd");
                        command.Parameters.AddWithValue("@NAME", string.IsNullOrEmpty(obj.Name) ? DBNull.Value : (object)obj.Name);
                        command.Parameters.AddWithValue("@RECEIPT_F_NO", string.IsNullOrEmpty(obj.receiptFno) ? DBNull.Value : (object)obj.receiptFno);
                        command.Parameters.AddWithValue("@RECEIPT_L_NO", string.IsNullOrEmpty(obj.receiptLNo) ? DBNull.Value : (object)obj.receiptLNo);
                        command.Parameters.AddWithValue("@FDATE", fdate);
                        command.Parameters.AddWithValue("@LDATE", ldate);
                        command.Parameters.AddWithValue("@LOC_ID", UserInfo.Loc_id);
                        command.Parameters.AddWithValue("@DEPT_ID", UserInfo.Dept_id);
                        command.Parameters.AddWithValue("@CTR_MACH_ID", UserInfo.ctrMachID);
                        using (SqlDataAdapter da = new SqlDataAdapter(command))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            return dt;
        }
        public DataTable getDataByReceiptID(string Receipt_ID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_GET_MESS_PRINT_RECEIPT_ITEM_DATA", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@FLAG", "MASTER");
                    command.Parameters.AddWithValue("@PRINT_RECEIPT_MST_ID", Receipt_ID);
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable getItemDataByMasterId(string Receipt_ID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_GET_MESS_PRINT_RECEIPT_ITEM_DATA", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@FLAG", "ITEM");
                    command.Parameters.AddWithValue("@PRINT_RECEIPT_MST_ID", Receipt_ID);
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable getMessData_itemWise(string ReceiptId, int ctr_id, int user_id)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "select * from MESS_ITEM_RECEIPT_DATA_V where PRINT_RECEIPT_MST_ID='" + ReceiptId + "' and User_Id=" + user_id + " and CTR_MACH_ID=" + ctr_id + "";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(dt);
                connection.Close();
            }
            return dt;
        }
        public DataTable getMessItemDataForReport(string receiptID)
        {
            MessItemDS ds = new MessItemDS();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "select * from [MESS_ITEM_RECEIPT_DATA_V] where PRINT_RECEIPT_MST_ID='" + receiptID + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(ds, "MESS_ITEM_RECEIPT_DATA_V");
                connection.Close();
            }
            return ds.Tables["MESS_ITEM_RECEIPT_DATA_V"];
        }
    }
}
