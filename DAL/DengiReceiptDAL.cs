﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;
using SGMOSOL.DataModel;
using SGMOSOL.ADMIN;
using SGMOSOL.DataSet;
namespace SGMOSOL.DAL
{
    public class DengiReceiptDAL
    {
        string connectionString = CommonFunctions.Decrypt(ConfigurationManager.ConnectionStrings["strConnection"].ConnectionString, true);
        CommonFunctions commonFunctions = new CommonFunctions();

        public DataTable getDengiReceipt(object data)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GET_DEN_DENGI_RECEIPT_DATA", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if (data is dengiReceiptModel obj)
                        {
                            string fdate = obj.receiptFDate.ToString("yyyy-MM-dd");
                            string ldate = obj.ReceiptLDate.ToString("yyyy-MM-dd");
                            command.Parameters.AddWithValue("@NAME", string.IsNullOrEmpty(obj.Name) ? DBNull.Value : (object)obj.Name);
                            command.Parameters.AddWithValue("@RECEIPT_F_NO", string.IsNullOrEmpty(obj.receiptFno) ? DBNull.Value : (object)obj.receiptFno);
                            command.Parameters.AddWithValue("@RECEIPT_L_NO", string.IsNullOrEmpty(obj.receiptLNo) ? DBNull.Value : (object)obj.receiptLNo);
                            command.Parameters.AddWithValue("@FDATE", fdate);
                            command.Parameters.AddWithValue("@LDATE", ldate);
                            command.Parameters.AddWithValue("@MOBILE_NUMBER", string.IsNullOrEmpty(obj.contact) ? DBNull.Value : (object)obj.contact);
                            //command.Parameters.AddWithValue("@DENGI_TYPE", obj.DengiId);
                            command.Parameters.AddWithValue("@LOC_ID", UserInfo.Loc_id);
                            command.Parameters.AddWithValue("@DEPT_ID", UserInfo.Dept_id);
                            command.Parameters.AddWithValue("@CTR_MACH_ID", UserInfo.ctrMachID);
                            command.Parameters.AddWithValue("@FY_ID", UserInfo.fy_id);
                            using (SqlDataAdapter da = new SqlDataAdapter(command))
                            {
                                da.Fill(dt);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dt;
        }

        public DataTable fetchDengiReceipt(string receiptID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GET_DENGI_RECEIPT_DATABYID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SERIAL_NO", receiptID);
                        command.Parameters.AddWithValue("@LOC_ID", UserInfo.Loc_id);
                        command.Parameters.AddWithValue("@DEPT_ID", UserInfo.Dept_id);
                        command.Parameters.AddWithValue("@CTR_MACH_ID", UserInfo.ctrMachID);
                        command.Parameters.AddWithValue("@FY_ID", UserInfo.fy_id);
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

        public DataTable GETTOTALAMOUNTBYPAYMENTID(DateTime fromDate, DateTime ToDate)
        {
            DengiTotalAmountReport ds = new DengiTotalAmountReport();
            try
            {
                string fdate = fromDate.ToString("yyyy-MM-dd");
                string ldate = ToDate.ToString("yyyy-MM-dd");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("GET_TOTAL_AMOUNT_BY_PAYMENTID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@FROMDATE", fdate);
                        command.Parameters.AddWithValue("@TODATE", ldate);
                        command.Parameters.AddWithValue("@LOC_ID", UserInfo.Loc_id);
                        command.Parameters.AddWithValue("@DEPT_ID", UserInfo.Dept_id);
                        command.Parameters.AddWithValue("@CTR_MACH_ID", UserInfo.ctrMachID);
                        command.Parameters.AddWithValue("@FY_ID", UserInfo.fy_id);
                        command.Parameters.AddWithValue("@USERID", UserInfo.UserId);
                        using (SqlDataAdapter da = new SqlDataAdapter(command))
                        {
                            da.Fill(ds, "TotalAmountByPaymentId");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);

            }
            return ds.Tables["TotalAmountByPaymentId"];
        }

        public DataTable GetLastInvoiceNumber()
        {
            DataTable dt = new DataTable();
            try
            {
               
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GET_LAST_INVOICE_BY_COUNTER", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@LOC_ID", UserInfo.Loc_id);
                        command.Parameters.AddWithValue("@DEPT_ID", UserInfo.Dept_id);
                        command.Parameters.AddWithValue("@CTR_MACH_ID", UserInfo.ctrMachID);
                        command.Parameters.AddWithValue("@FY_ID", UserInfo.fy_id);
                        using (SqlDataAdapter da = new SqlDataAdapter(command))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);

            }
            return dt;
        }



        public DataTable getDengiReceiptDataForReport(string receiptID)
        {
            DengiReceiptDataSet ds = new DengiReceiptDataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "select * from DEN_Dengi_Receipt_View where DENGI_RECEIPT_ID =" + receiptID + " and Location_Id=" + UserInfo.Loc_id + " and Department_Id=" + UserInfo.Dept_id + " and CTR_MACH_ID=" + UserInfo.ctrMachID + "";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(ds, "DEN_Dengi_Receipt_View");
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return ds.Tables["DEN_Dengi_Receipt_View"];
        }
        public int DupPrintInsert(object DengiReceiptId)
        {
            int status = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("INSERT INTO [dbo].[DEN_REPRINT_DENGI_RCPT_T] ([DENGI_RECEIPT_ID],[ENTERED_BY],[ENTERED_ON],[MACHINE_NAME],[USER_ID])" +
                        " VALUES (@DENGI_RECEIPT_ID,@ENTERED_BY,(GETDATE()),@MACHINE_NAME,@USER_ID)", connection))
                    {
                        command.CommandType = CommandType.Text;

                        command.Parameters.AddWithValue("@DENGI_RECEIPT_ID", DengiReceiptId);
                        command.Parameters.AddWithValue("@ENTERED_BY", UserInfo.UserName);
                        command.Parameters.AddWithValue("@MACHINE_NAME", UserInfo.Counter_Name);
                        command.Parameters.AddWithValue("@USER_ID", UserInfo.UserId);
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
        public int DupPrintDeclaration(object DengiReceiptId)
        {
            int status = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("INSERT INTO [dbo].[DEN_DECLARATION_DENGI_RCPT_T] ([DENGI_RECEIPT_ID],[ENTERED_BY],[ENTERED_ON],[MACHINE_NAME],[USER_ID])" +
                        " VALUES (@DENGI_RECEIPT_ID,@ENTERED_BY,(GETDATE()),@MACHINE_NAME,@USER_ID)", connection))
                    {
                        command.CommandType = CommandType.Text;

                        command.Parameters.AddWithValue("@DENGI_RECEIPT_ID", DengiReceiptId);
                        command.Parameters.AddWithValue("@ENTERED_BY", UserInfo.UserName);
                        command.Parameters.AddWithValue("@MACHINE_NAME", UserInfo.Counter_Name);
                        command.Parameters.AddWithValue("@USER_ID", UserInfo.UserId);
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
        public int InsertDengiReceipt(object data)
        {
            int status = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("InsertDengiReceipt", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if (data is dengiReceiptModel obj)
                        {
                            command.Parameters.AddWithValue("@DR_DATE", obj.dr_Date);
                            command.Parameters.AddWithValue("@Com_Id", UserInfo.CompanyID);
                            command.Parameters.AddWithValue("@Loc_Id", UserInfo.Loc_id);
                            command.Parameters.AddWithValue("@Dept_Id", UserInfo.Dept_id);
                            command.Parameters.AddWithValue("@Fy_Id", UserInfo.fy_id);
                            command.Parameters.AddWithValue("@Ctr_mac_Id", UserInfo.ctrMachID);
                            // command.Parameters.AddWithValue("@SERIAL_NO", obj.serailId);
                            command.Parameters.AddWithValue("@Dengi_Id", obj.DengiId);
                            command.Parameters.AddWithValue("@amount", obj.amount);
                            command.Parameters.AddWithValue("@payment_type_Id", obj.paymentTypeId);
                            command.Parameters.AddWithValue("@Name", obj.Name);
                            command.Parameters.AddWithValue("@Gotra_Id", obj.gotraId);
                            if (obj.gotra != "Select")
                                command.Parameters.AddWithValue("@Gotra_name", obj.gotra);
                            else
                                command.Parameters.AddWithValue("@Gotra_name", null);
                            command.Parameters.AddWithValue("@Contact", obj.contact);
                            command.Parameters.AddWithValue("@Address", obj.Address);
                            command.Parameters.AddWithValue("@DistrictId", obj.DistId);
                            command.Parameters.AddWithValue("@DISTRICT", obj.DISTRICT);
                            command.Parameters.AddWithValue("@Taluka", obj.Taluka);
                            command.Parameters.AddWithValue("@ChqBankName", obj.chqbankname);
                            command.Parameters.AddWithValue("@ChqNo", obj.chno);
                            DateTime? chqDate = obj.chqdate;
                            if (chqDate.HasValue && obj.chno != "")
                                command.Parameters.AddWithValue("@ChqDate", obj.chqdate);
                            else
                                command.Parameters.AddWithValue("@ChqDate", null);
                            command.Parameters.AddWithValue("@DDBankName", obj.ddbankname);
                            command.Parameters.AddWithValue("@DDNo", obj.ddno);
                            DateTime? dd_date = obj.dd_date;
                            if (dd_date.HasValue && obj.ddno != "")
                                command.Parameters.AddWithValue("@DDDate", obj.dd_date);
                            else
                                command.Parameters.AddWithValue("@DDDate", null);
                            command.Parameters.AddWithValue("@NetBankName", obj.netbankname);
                            command.Parameters.AddWithValue("@NetBankReferenceNo", obj.netbankrefnumber);
                            command.Parameters.AddWithValue("@CardBankName", obj.cardbankname);
                            command.Parameters.AddWithValue("@CardBankReferenceName", obj.cardbankrefnumber);
                            command.Parameters.AddWithValue("@EnteredBy", UserInfo.UserName);

                            command.Parameters.AddWithValue("@ModifiedBy", UserInfo.UserName);
                            command.Parameters.AddWithValue("@ModifiedOn", obj.dr_Date);
                            command.Parameters.AddWithValue("@MachineName", UserInfo.Machine_Name);
                            command.Parameters.AddWithValue("@UserId", UserInfo.UserId);
                            command.Parameters.AddWithValue("@SERVERNAME", UserInfo.serverName);
                            command.Parameters.AddWithValue("@CurrencyType", "Indian Rupees");
                            command.Parameters.AddWithValue("@TidId", obj.tidId);
                            command.Parameters.AddWithValue("@InvoiceNo", obj.Invoiceno);
                            command.Parameters.AddWithValue("@StateId", obj.stateId);
                            command.Parameters.AddWithValue("@STATE", obj.STATE);
                            command.Parameters.AddWithValue("@CountryId", obj.countryId);
                            command.Parameters.AddWithValue("@COUNTRY_NAME", obj.COUNTRY_NAME);
                            command.Parameters.AddWithValue("@PanCard", obj.PanNo);
                            command.Parameters.AddWithValue("@Pincode", obj.PinCode);
                            if (obj.Doc_type != "Select")
                                command.Parameters.AddWithValue("@Doc_Type", obj.Doc_type);
                            else
                                command.Parameters.AddWithValue("@Doc_Type", null);
                            command.Parameters.AddWithValue("@Doc_Detail", obj.Doc_Detail);
                            command.Parameters.AddWithValue("@IsDuplicate", obj.IsDuplicate);
                            command.Parameters.AddWithValue("@ScanImage", obj.ScanImage);
                            command.Parameters.AddWithValue("@Barcode", obj.Barcode);
                            SqlParameter idParam = new SqlParameter("@Receipt_ID", SqlDbType.Decimal);
                            idParam.Direction = ParameterDirection.Output;
                            command.Parameters.Add(idParam);
                            int rowsAffected = command.ExecuteNonQuery();
                            //  command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                status = Convert.ToInt32(command.Parameters["@Receipt_ID"].Value);
                            }
                            else
                            {
                                status = rowsAffected;
                                MessageBox.Show("Can not insert this record due to date mismatch!!! Please check date.");
                            }
                        }
                        //  MessageBox.Show("Dengi Receipt saved Successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }

            return status;
        }
        public int getDenigNumber()
        {
            int dengiId = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT MAX(SERIAL_NO) FROM DEN_DENGI_RECEIPT_MST_T WHERE DEPT_ID=" + UserInfo.Dept_id + " AND LOC_ID=" + UserInfo.Loc_id + " and FY_ID=" + UserInfo.fy_id + " and CTR_MACH_ID=" + UserInfo.ctrMachID + "";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value && result != null && int.TryParse(result.ToString(), out int maxId))
                        {
                            dengiId = maxId + 1;
                        }
                        if (result.ToString() == "")
                        {
                            dengiId = 1;
                        }
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dengiId;
        }
        public string ExtractDate(string input)
        {
            int valueIndex = input.IndexOf("Value:");

            if (valueIndex != -1)
            {
                string datePart = input.Substring(valueIndex + "Value:".Length).Trim();
                if (DateTime.TryParseExact(datePart, "dd-MM-yyyy HH:mm:ss", null, DateTimeStyles.None, out DateTime parsedDate))
                {
                    return parsedDate.ToString("yyyy-MM-dd");
                }
            }
            return string.Empty;
        }
        public bool checkIsDuplicate(string mobile)
        {
            bool isDuplicate = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT CONTACT FROM DEN_DENGI_RECEIPT_MST_T WHERE CONTACT='" + mobile + "'";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                        {
                            isDuplicate = true;
                        }
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);

            }
            return isDuplicate;
        }
        public DataTable getLastEnteredRecord(int ctr_Mach_id)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT top 1 Name,ADDRESS,AMOUNT, DOC_TYPE, DOC_DETAIL, CONTACT, TALUKA, PINCODE,COUNTRY_ID,STATE_ID,DISTRICT_ID FROM DEN_DENGI_RECEIPT_MST_T where CTR_MACH_ID = " + ctr_Mach_id + " AND LOC_ID=" + UserInfo.Loc_id + " AND DEPT_ID=" + UserInfo.Dept_id + " order by DENGI_RECEIPT_ID desc";
                    SqlDataAdapter da = new SqlDataAdapter(query, connection);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);

            }
            return dt;
        }
        public DataTable getAmountLimits(string DengiType)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = " select * from DEN_DENGI_MASTER_T where type='" + DengiType + "'";
                    SqlDataAdapter da = new SqlDataAdapter(query, connection);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dt;
        }

        public DataTable GetLastEnterdNameAmountSerial()//int intCtrId, int COM_ID, int LOC_ID, int FY_ID
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT top 1 cast(CAST(AMOUNT as decimal(12,2)) as float)as AMOUNT,NAME as LastEnteredName, ISNULL(SERIAL_NO,0) as SERIAL_NO FROM DEN_DENGI_RECEIPT_MST_T where CTR_MACH_ID  = " + UserInfo.ctrMachID + " and COM_ID = 9 and LOC_ID = " + UserInfo.Loc_id + " and FY_ID= " + UserInfo.fy_id + " order by DENGI_RECEIPT_ID desc";
                    SqlDataAdapter da = new SqlDataAdapter(query, connection);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dt;
        }



        public int InsertError(DengiErrorLog DengiError)
        {
            int status = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("InsertDengiReceipt_LOG", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if (DengiError is DengiErrorLog obj)
                        {
                            command.Parameters.AddWithValue("@UserName", obj.Name);
                            command.Parameters.AddWithValue("@Amount", obj.Amount);
                            command.Parameters.AddWithValue("@Serial_no", obj.ReceiptNo);
                            command.Parameters.AddWithValue("@LastUserName", obj.LastName);
                            command.Parameters.AddWithValue("@LastAmount", obj.LastAmount);
                            command.Parameters.AddWithValue("@LastSerial_no", obj.LastReceiptNo);
                            command.Parameters.AddWithValue("@CreatedBy", UserInfo.UserName);
                            command.Parameters.AddWithValue("@MACId", obj.Mach_Id);

                            SqlParameter idParam = new SqlParameter("@ID", SqlDbType.Decimal);
                            idParam.Direction = ParameterDirection.Output;
                            command.Parameters.Add(idParam);
                            command.ExecuteNonQuery();
                            status = Convert.ToInt32(command.Parameters["@ID"].Value);
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
    }
}
