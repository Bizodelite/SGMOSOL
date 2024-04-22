using SGMOSOL.ADMIN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SGMOSOL.BAL.BhaktNiwasBAL;

namespace SGMOSOL.DAL.BhaktNiwas
{
    internal class RoomLockedDAL
    {
        CommonFunctions cf = new CommonFunctions();
        System.Data.DataTable Dr = new System.Data.DataTable();

        public System.Data.DataSet GetData(DateTime strDate)
        {
            System.Data.DataSet ds = new System.Data.DataSet();

            try
            {
                SqlCommand command = new SqlCommand("SP_GetRoomLockedRecords", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@strDate", strDate);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return ds;
        }
        public System.Data.DataSet GetConbobox(int intDeptId = 0, int intActiveInactiveStatus = 0, int intAvailableStatus = 0, Int64 DeptID = 0, Int64 locId = 0)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            try
            {
                using (SqlCommand command = new SqlCommand("SP_GetRoomDetails", clsConnection.GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@intDeptId", intDeptId);
                    command.Parameters.AddWithValue("@intAvailableStatus", intAvailableStatus);
                    command.Parameters.AddWithValue("@Loc_ID", locId);
                    command.Parameters.AddWithValue("@strRoomSrch", "");

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return ds;
        }
        public System.Data.DataSet GetDataforEdit(string strDate = "")
        {
            System.Data.DataSet ds = new System.Data.DataSet();

            try
            {
                SqlCommand command = new SqlCommand("SP_GetRoomLockedDataforEdit", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@strDate", strDate);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }

            return ds;
        }
        public long Insert(RoomLocked RoomLocked, string strUserName = null, string strMacName = null, DateTime EndteredOn = default(DateTime))
        {
            long i = InsertRoomLocked(RoomLocked, strUserName, strMacName, EndteredOn);
            if (i < 0)
            {
                return i;
            }
            return i;
        }
        public long Update(RoomLocked RoomLocked, string strID = null, string strMacName = null, DateTime EndteredOn = default(DateTime))
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_UpdateRoomLock", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@RoomLockId", RoomLocked.ROOM_LOCK_ID);
                command.Parameters.AddWithValue("@RoomId", RoomLocked.ROOM_ID);
                command.Parameters.AddWithValue("@ModifiedOn", EndteredOn);

                return clsConnection.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -10;
            }
        }
        public long InsertRoomLocked(RoomLocked roomLocked, string userName, string machineName, DateTime sDate)
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_InsertRoomLock", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@RoomId", roomLocked.ROOM_ID);
                command.Parameters.AddWithValue("@LockDate", roomLocked.LOCK_DATE);
                command.Parameters.AddWithValue("@DeptId", roomLocked.DEPT_ID);
                command.Parameters.AddWithValue("@LocId", roomLocked.LOC_ID);
                command.Parameters.AddWithValue("@EnteredBy", userName);
                command.Parameters.AddWithValue("@ModifiedBy", userName);
                command.Parameters.AddWithValue("@EnteredOn", sDate);
                command.Parameters.AddWithValue("@ModifiedOn", sDate);
                command.Parameters.AddWithValue("@BookingId", roomLocked.BookingID);

                return clsConnection.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -10;
            }
        }
    }
}
