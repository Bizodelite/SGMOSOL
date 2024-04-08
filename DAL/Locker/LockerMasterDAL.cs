using Microsoft.VisualBasic;
using SGMOSOL.ADMIN;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SGMOSOL.BAL.LockerBAL;

namespace SGMOSOL.DAL
{
    internal class LockerMasterDAL
    {
        string connectionString = CommonFunctions.Decrypt(ConfigurationManager.ConnectionStrings["strConnection"].ConnectionString, true);
        CommonFunctions commonFunctions = new CommonFunctions();
        System.Text.StringBuilder strDML = new System.Text.StringBuilder();
        System.Text.StringBuilder strSQL = new System.Text.StringBuilder(); 
        SqlConnection mCnn = new SqlConnection();
        clsConnection mDsCon = new clsConnection();
        long lngErrNum = 0;
        DataTable dr = new DataTable();
        private void SetError(string str)
        {
            clsConnection.mErrorResult = clsConnection.mErrorResult + Constants.vbNewLine + str;
        }
        public System.Data.DataSet GetDsLockerDetails(int ctrMachId = 0, short activeInactiveStatus = 0, short availableStatus = 0)
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDsLockerDetails", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CtrMachId", ctrMachId);
                command.Parameters.AddWithValue("@ActiveInactiveStatus", activeInactiveStatus);
                command.Parameters.AddWithValue("@AvailableStatus", availableStatus);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                System.Data.DataSet ds = new System.Data.DataSet();
                adapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exception
                lngErrNum = -91;
                return null;
            }
        }

        public System.Data.DataSet GetDataForGrid(int ctrMachId = 0)
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDataForGrid", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CtrMachId", ctrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                System.Data.DataSet ds = new System.Data.DataSet();
                adapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exception
                lngErrNum = -91;
                return null;
            }
        }
        
        public DataTable GetDrLockerDetails(int ctrMachId = 0, int activeInactiveStatus = 0, int availableStatus = 0, string str = "")
        {
            try
            {
                str = str + "%";
                SqlCommand command = new SqlCommand("SP_GetDrLockerDetails", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CtrMachId", ctrMachId);
                command.Parameters.AddWithValue("@ActiveInactiveStatus", activeInactiveStatus);
                command.Parameters.AddWithValue("@AvailableStatus", availableStatus);
                command.Parameters.AddWithValue("@Str", str);

                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                lngErrNum = -91;
            }
                return dr;
        }

        public DataTable GetDrLockerTariff()
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDrLockerTariff", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                lngErrNum = -91;
            }
                return dr;
        }

        public System.Data.DataSet GetDmgedLkrsForGrid(object intCtrMachId)
        {
                System.Data.DataSet ds = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDmgedLkrsForGrid", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                lngErrNum = -91;
                return ds;
            }
        }

        public System.Data.DataSet GetChkOutWarnigGrid(int intCtrMachId, string strDate)
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_GetMoreThan3Days", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CtrMachId", intCtrMachId);
                command.Parameters.AddWithValue("@CallVal", 1);

                System.Data.DataSet ds = new System.Data.DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                lngErrNum = -91;
                return null;
            }
        }

        public System.Data.DataSet GetMoreThan3Days(int intCtrMachId, string strDate)
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_GetMoreThan3Days", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CtrMachId", intCtrMachId);
                command.Parameters.AddWithValue("@CallVal", 0);

                System.Data.DataSet ds = new System.Data.DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                lngErrNum = -91;
                return null;
            }
        }

        public long UpdateLockerMst(int p1, int intActiveInactiveStatus = 0, int intAvailableStatus = 0)
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_UpdateLockerMst", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@LockerId", p1);
                command.Parameters.AddWithValue("@ActiveInactiveStatus", intActiveInactiveStatus);
                command.Parameters.AddWithValue("@AvailableStatus", intAvailableStatus);

                return clsConnection.ExecuteNonQuery(command); // Since it's an UPDATE operation
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -1; // Error
            }
        }

        public long Insert(DamagedLockers objDamagedLkrs)
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_InsertDamagedLocker", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@LockerId", objDamagedLkrs.LockerId);
                command.Parameters.AddWithValue("@Reason", objDamagedLkrs.Reason);
                command.Parameters.AddWithValue("@Date", objDamagedLkrs.sDate);

                return clsConnection.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -1; // Error
            }
        }

        public long Delete(DamagedLockers objDamagedLkrs)
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_DeleteDamagedLocker", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@LockerId", objDamagedLkrs.LockerId);

                return clsConnection.ExecuteNonQuery(command); // Success
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -1; // Error
            }
        }

    }
}
