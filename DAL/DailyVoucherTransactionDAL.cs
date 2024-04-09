using Microsoft.VisualBasic;
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
    public class DailyVoucherTransactionDAL
    {
        long lngErrNum = 0;
        long lngErrNum1 = 0;
        System.Text.StringBuilder strSQL = new System.Text.StringBuilder();
        System.Text.StringBuilder strDML = new System.Text.StringBuilder();
        System.Text.StringBuilder strDML1 = new System.Text.StringBuilder();
        SqlConnection mCnn = new SqlConnection();
        public DataTable GetDrPendingTransactionByCTR_MAC_ID(string strDate, Int64 intCtrMachId, Int64 intLocId, int intFY_ID, Int16 intModID)
        {
            DataTable dr = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetPendingTransactionsByCTR_MAC_ID", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@strDate", strDate);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);
                command.Parameters.AddWithValue("@intLocId", intLocId);
                command.Parameters.AddWithValue("@intFY_ID", intFY_ID);
                command.Parameters.AddWithValue("@intModID", intModID);

                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                lngErrNum = -91;
            }
            return dr;
        }


        public DataTable GetDrPendingTransaction(string strDate, Int64 intCtrMachId, Int64 intLocId, int intFY_ID, Int16 intModID)
        {
            DataTable dr = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetPendingTransactions", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@strDate", strDate);
                command.Parameters.AddWithValue("@intLocId", intLocId);
                command.Parameters.AddWithValue("@intFY_ID", intFY_ID);
                command.Parameters.AddWithValue("@intModID", intModID);

                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                lngErrNum = -91;
            }
            return dr;
        }


        public DataTable GetDrDailyVoucherBed(string strDate, Int64 intLocId, int intFyId, Int16 intModType, int toSerialNo)
        {
            DataTable dr = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDailyVoucherBed", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@strDate", strDate);
                command.Parameters.AddWithValue("@intLocId", intLocId);
                command.Parameters.AddWithValue("@intFyId", intFyId);
                command.Parameters.AddWithValue("@intModType", intModType);
                command.Parameters.AddWithValue("@toSerialNo", toSerialNo);

                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                lngErrNum = -91;
            }
            return dr;
        }

        public DataTable GetDrDailyVoucherBhaktniwas(string strDate, Int64 intLocId, int intFyId, Int16 intModType, int toSerialNo)
        {
            DataTable dr = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDailyVoucherBhaktniwas", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@strDate", strDate);
                command.Parameters.AddWithValue("@intLocId", intLocId);
                command.Parameters.AddWithValue("@intFyId", intFyId);
                command.Parameters.AddWithValue("@intModType", intModType);
                command.Parameters.AddWithValue("@toSerialNo", toSerialNo);

                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                lngErrNum = -91;
            }
            return dr;
        }

        public DataTable GetDrDailyVoucherBhojnalay(string strDate, Int64 intCtrMachId, Int64 intFyId, Int16 intModType, int toSerialNo)
        {
            DataTable dr = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDailyVoucherBhojnalay", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@strDate", strDate);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);
                command.Parameters.AddWithValue("@intFyId", intFyId);
                command.Parameters.AddWithValue("@intModType", intModType);
                command.Parameters.AddWithValue("@toSerialNo", toSerialNo);

                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                lngErrNum = -91;
            }
            return dr;
        }

        public DataTable GetDrDailyVoucherGame(string strDate, Int64 intCtrMachId, Int64 intFyId, Int16 intModType, int toSerialNo)
        {
            DataTable dr = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDailyVoucherGame", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@strDate", strDate);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);
                command.Parameters.AddWithValue("@intFyId", intFyId);
                command.Parameters.AddWithValue("@intModType", intModType);
                command.Parameters.AddWithValue("@toSerialNo", toSerialNo);

                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                lngErrNum = -91;
            }
            return dr;
        }

        public DataTable GetDrDailyVoucherToyTrain(string strDate, Int64 intCtrMachId, Int64 intFyId, Int16 intModType, int toSerialNo)
        {
            DataTable dr = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDailyVoucherToyTrain", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@strDate", strDate);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);
                command.Parameters.AddWithValue("@intFyId", intFyId);
                command.Parameters.AddWithValue("@intModType", intModType);
                command.Parameters.AddWithValue("@toSerialNo", toSerialNo);

                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                lngErrNum = -91;
            }
                return dr;
        }

        public DataTable GetDrDailyVoucherLocker(string strDate, Int64 intLocId, Int64 intFyId, Int16 intModType, int toSerialNo)
        {
            DataTable dr = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDailyVoucherLocker", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@strDate", strDate);
                command.Parameters.AddWithValue("@intLocId", intLocId);
                command.Parameters.AddWithValue("@intFyId", intFyId);
                command.Parameters.AddWithValue("@intModType", intModType);
                command.Parameters.AddWithValue("@toSerialNo", toSerialNo);

                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                lngErrNum = -91;
            }
                return dr;
        }

        public DataTable GetDrDailyVoucherDengi(string strDate, Int64 intLocId, Int64 intFyId, Int16 intModType, int toSerialNo)
        {
            DataTable dr = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDailyVoucherDengi", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@strDate", strDate);
                command.Parameters.AddWithValue("@intLocId", intLocId);
                command.Parameters.AddWithValue("@intFyId", intFyId);
                command.Parameters.AddWithValue("@intModType", intModType);
                command.Parameters.AddWithValue("@toSerialNo", toSerialNo);

                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                lngErrNum = -91;
            }
                return dr;
        }

        public DataTable GetDrDailyVoucherEntryGate(string strDate, Int64 intLocId, int intFyId, Int16 intModType, int toSerialNo)
        {
            DataTable dr = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDailyVoucherEntryGate", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@strDate", strDate);
                command.Parameters.AddWithValue("@intLocId", intLocId);
                command.Parameters.AddWithValue("@intFyId", intFyId);
                command.Parameters.AddWithValue("@intModType", intModType);
                command.Parameters.AddWithValue("@toSerialNo", toSerialNo);

                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                lngErrNum = -91;
            }
                return dr;
        }

        public DataTable GetDrDailyVoucherMedical(string strDate, Int64 intCtrMachId, int intFyId, Int16 intModType, int toSerialNo)
        {
            DataTable dr = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDailyVoucherMedical", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@strDate", strDate);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);
                command.Parameters.AddWithValue("@intFyId", intFyId);
                command.Parameters.AddWithValue("@intModType", intModType);
                command.Parameters.AddWithValue("@toSerialNo", toSerialNo);

                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                lngErrNum = -91;
            }
                return dr;
        }

        public long InsertMst(Collection coll, string strUserName, string strMachineName, Int64 intCtrMachId, Int64 LocId, Int64 DeptId, int intUserId, int intCompanyId, int intFyId, Int16 intModId, string intCounterName, string strToDate, Int16 IsExcel
)
        {

            // Dim count As Integer = Convert.ToInt32(idArrayMstIds.Length)
            long SUC = -1;
            int ERR;
            long SUC1 = -1;
            int ERR1;
            try
            {
                for (int I = 1; I < coll.Count + 1; I++)
                {
                    SqlCommand cmd = new SqlCommand();
                    strDML.Length = 0;
                    strDML.Append("STP_INS_UPD_DailyVoucherTransaction");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@FROM_DATE", SqlDbType.DateTime).Value = (((DailyVoucherTransaction)coll[I]).fDate);
                    cmd.Parameters.Add("@TO_DATE", SqlDbType.DateTime).Value = (((DailyVoucherTransaction)coll[I]).TDate);
                    cmd.Parameters.Add("@MINSERIALNO", SqlDbType.Int).Value = Convert.ToInt32(((DailyVoucherTransaction)coll[I]).MinSerialNo);
                    cmd.Parameters.Add("@MAXSERIALNO", SqlDbType.Int).Value = Convert.ToInt32(((DailyVoucherTransaction)coll[I]).MaxSerialNo);
                    cmd.Parameters.Add("@TOTALAMOUNT", SqlDbType.Int).Value = Convert.ToInt32(((DailyVoucherTransaction)coll[I]).TotalAmount);
                    cmd.Parameters.Add("@TOTALRECEIPT", SqlDbType.Int).Value = Convert.ToInt32(((DailyVoucherTransaction)coll[I]).TotalReceipt);
                    cmd.Parameters.Add("@STATUS", SqlDbType.Int).Value = Convert.ToInt32(((DailyVoucherTransaction)coll[I]).Status);
                    cmd.Parameters.Add("@LOCATIONNAME", SqlDbType.VarChar).Value = (((DailyVoucherTransaction)coll[I]).LocationName);
                    cmd.Parameters.Add("@COM_ID", SqlDbType.Int).Value = intCompanyId;
                    cmd.Parameters.Add("@LOC_ID", SqlDbType.Int).Value = LocId;
                    cmd.Parameters.Add("@DEPT_ID", SqlDbType.Int).Value = DeptId;
                    cmd.Parameters.Add("@MOD_ID", SqlDbType.Int).Value = intModId;
                    cmd.Parameters.Add("@MOD_NAME", SqlDbType.VarChar).Value = (((DailyVoucherTransaction)coll[I]).ModName);
                    cmd.Parameters.Add("@ENTERED_BY", SqlDbType.VarChar, 50).Value = strUserName;
                    cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar, 50).Value = strUserName;
                    cmd.Parameters.Add("@MACHINE_NAME", SqlDbType.VarChar, 30).Value = strMachineName;
                    cmd.Parameters.Add("@CTR_MACH_ID", SqlDbType.VarChar, 30).Value = intCtrMachId;
                    cmd.Parameters.Add("@CTR_NAME", SqlDbType.VarChar, 50).Value = intCounterName;
                    cmd.Parameters.Add("@USER_ID", SqlDbType.VarChar, 30).Value = intUserId;
                    cmd.Parameters.Add("@FY_ID", SqlDbType.VarChar, 30).Value = intFyId;
                    cmd.Parameters.Add("@IsExcel", SqlDbType.Int).Value = IsExcel;

                    SqlParameter param1 = new SqlParameter("@SUCCEED", SqlDbType.Int);
                    param1.Value = 0;
                    param1.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param1);
                    SqlParameter param2 = new SqlParameter("@FAILURE", SqlDbType.Int);
                    param2.Value = 0;
                    param2.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param2);
                    lngErrNum = clsConnection.DMLStoredProc(strDML.ToString(), cmd);
                    SUC = Convert.ToInt64(cmd.Parameters["@SUCCEED"].Value);
                    ERR = Convert.ToInt32(cmd.Parameters["@FAILURE"].Value);
                    if (SUC > 0)
                        lngErrNum = SUC;
                    else
                        lngErrNum = -1;
                }

                if (IsExcel == 0)
                {

                    // Push data into ERPDB
                    SqlCommand cmd1 = new SqlCommand();
                    strDML1.Length = 0;
                    strDML1.Append("SP_PUSH_DAILY_VOUCHER_TRANSACTION");
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add("@TO_DATE", SqlDbType.VarChar, 50).Value = strToDate;
                    SqlParameter param3 = new SqlParameter("@SUCCEED", SqlDbType.Int);
                    param3.Value = 0;
                    param3.Direction = ParameterDirection.Output;
                    cmd1.Parameters.Add(param3);
                    SqlParameter param4 = new SqlParameter("@FAILURE", SqlDbType.Int);
                    param4.Value = 0;
                    param4.Direction = ParameterDirection.Output;
                    cmd1.Parameters.Add(param4);
                    lngErrNum1 = clsConnection.DMLStoredProc(strDML1.ToString(), cmd1);
                    SUC1 = Convert.ToInt64(cmd1.Parameters["@SUCCEED"].Value);
                    ERR1 = Convert.ToInt32(cmd1.Parameters["@FAILURE"].Value);
                    return lngErrNum1;
                }
                else
                    return lngErrNum1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public DataTable GetDrDailyVoucherData(long DailyId)
        {
            DataTable dr = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDailyVoucherData", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DailyId", DailyId);

                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                lngErrNum = -91;
            }
                return dr;
        }

        public long UpdateStatusDailyVoucher(Int64 DailyVou_ID)
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_UpdateStatusDailyVoucher", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DailyVou_ID", DailyVou_ID);

                return clsConnection.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                return -90; // Update error code, handle exceptions properly
            }
        }


    }
}
