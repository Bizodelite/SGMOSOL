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
    internal class LockerChangeDAL
    {
        string connectionString = CommonFunctions.Decrypt(ConfigurationManager.ConnectionStrings["strConnection"].ConnectionString, true);
        CommonFunctions commonFunctions = new CommonFunctions();
        System.Text.StringBuilder strDML = new System.Text.StringBuilder();
        System.Text.StringBuilder strSQL = new System.Text.StringBuilder();
        SqlConnection mCnn = new SqlConnection();
        clsConnection mDsCon = new clsConnection();
        long lngErrNum = 0;
        DataTable dr = new DataTable();
        
        public DataTable FindLocker(long checkInMstId)
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_FindLocker", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CheckInMstId", checkInMstId);

                 dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dr;
        }

        public DataTable GetDrLockerChangeMst(long lockerCheckInMstId = 0, string date = "", long serialNo = 0, long ctrMachId = 0, int comId = 0, int locId = 0, int deptId = 0, long fyId = 0)
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDrLockerChangeMst", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@LockerCheckInMstId", lockerCheckInMstId);
                command.Parameters.AddWithValue("@Date", date);
                command.Parameters.AddWithValue("@SerialNo", serialNo);
                command.Parameters.AddWithValue("@CtrMachId", ctrMachId);
                command.Parameters.AddWithValue("@ComId", comId);
                command.Parameters.AddWithValue("@LocId", locId);
                command.Parameters.AddWithValue("@DeptId", deptId);
                command.Parameters.AddWithValue("@FYId", fyId);

                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dr;
        }
        public DataTable GetDrLockerCheckInDet(long lockerCheckInMstId = 0, long ctrMachId = 0)
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDrLockerCheckInDet", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@LockerCheckInMstId", lockerCheckInMstId);
                command.Parameters.AddWithValue("@CtrMachId", ctrMachId);

                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                lngErrNum = -91;
            }
            return dr;
        }


    }
}
