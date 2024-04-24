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
    internal class RoomMasterDAL
    {
        CommonFunctions cf = new CommonFunctions();
        System.Data.DataTable Dr = new System.Data.DataTable();
        public System.Data.DataSet GetDmgedRoomsForGrid(int intCtrMachId, int locID = 0)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            try
            {
                using (SqlCommand command = new SqlCommand("SP_GetDmgedRoomsForGrid", clsConnection.GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CtrMachId", intCtrMachId);
                    command.Parameters.AddWithValue("@LocID", locID);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return ds;
        }
        public System.Data.DataTable GetDrSublocations(long locationID, long modTypeID = 0)
        {
            using (SqlCommand command = new SqlCommand("SP_GetSublocations", clsConnection.GetConnection()))
            {
                try
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@LocationID", locationID);
                    command.Parameters.AddWithValue("@ModTypeID", modTypeID);
                    Dr = clsConnection.ExecuteReader(command);
                }
                catch (Exception ex)
                {
                    cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                }
                return Dr;
            }
        }
        public System.Data.DataTable GetDrRoomDetails1(string strRoomSrch = "", Int64 intDeptId = 0, int intAvailableStatus = 0, int Loc_ID = 0)
        {
            using (SqlCommand command = new SqlCommand("SP_GetRoomDetails", clsConnection.GetConnection()))
            {
                try
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@strRoomSrch", strRoomSrch);
                    command.Parameters.AddWithValue("@intDeptId", intDeptId);
                    command.Parameters.AddWithValue("@intAvailableStatus", intAvailableStatus);
                    command.Parameters.AddWithValue("@Loc_ID", Loc_ID);
                    Dr = clsConnection.ExecuteReader(command);
                }
                catch (Exception ex)
                {

                }
                return Dr;
            }
        }
        public System.Data.DataTable GetDrRoomDetails(int intDeptId = 0, int intActiveInactiveStatus = 0, int intAvailableStatus = 0, Int64 DeptID = 0, Int64 locId = 0)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("SP_GetRoomDetails", clsConnection.GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@intDeptId", DeptID);
                    command.Parameters.AddWithValue("@intAvailableStatus", intAvailableStatus);
                    command.Parameters.AddWithValue("@Loc_ID", locId);
                    command.Parameters.AddWithValue("@strRoomSrch", "");

                    Dr = clsConnection.ExecuteReader(command);
                }
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return Dr;
        }
        public System.Data.DataTable GetRent(long id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetRent", clsConnection.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", id);

                    Dr = clsConnection.ExecuteReader(cmd);
                }
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return Dr;
        }
        public System.Data.DataTable GetDaysForDonnerextend(long SerialNo, long CheckInId)
        {
            SqlCommand command = new SqlCommand("SP_GetDaysForDonnerextend", clsConnection.GetConnection());
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@SerialNo", SerialNo);
            command.Parameters.AddWithValue("@CheckInId", CheckInId);

            try
            {
                Dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return Dr;
        }
        public System.Data.DataTable GetDrAuthPersons()
        {
            using (SqlCommand command = new SqlCommand("SP_GetAuthPersons", clsConnection.GetConnection()))
            {
                try
                {
                    command.CommandType = CommandType.StoredProcedure;

                    Dr = clsConnection.ExecuteReader(command);
                }
                catch (Exception ex)
                {
                    cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                }
                return Dr;
            }
        }
        public System.Data.DataSet GetDonners(int bhaktid, int locId)
        {
            using (SqlCommand command = new SqlCommand("SP_GetDonners", clsConnection.GetConnection()))
            {
                System.Data.DataSet dataSet = new System.Data.DataSet();
                try
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@bhaktid", bhaktid);
                    command.Parameters.AddWithValue("@locId", locId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    adapter.Fill(dataSet);
                }
                catch (Exception ex)
                {
                    cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                }
                return dataSet;
            }
        }
        public System.Data.DataSet GetAnnadan(int bhaktid, int locId)
        {
            using (SqlCommand command = new SqlCommand("SP_GetAnnadan", clsConnection.GetConnection()))
            {
                System.Data.DataSet dataSet = new System.Data.DataSet();
                try
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@bhaktid", bhaktid);
                    command.Parameters.AddWithValue("@locId", locId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataSet);
                }
                catch (Exception ex)
                {
                    cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                }
                return dataSet;
            }
        }

        public System.Data.DataTable checkRoomAvailability(int RoomId)
        {
            using (SqlCommand command = new SqlCommand("SP_CheckRoomAvailability", clsConnection.GetConnection()))
            {
                try
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RoomId", RoomId);
                    Dr = clsConnection.ExecuteReader(command);
                }
                catch (Exception ex)
                {
                    cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                }
                return Dr;
            }
        }
        public System.Data.DataSet GetValidation(long id)
        {
            System.Data.DataSet ds = new System.Data.DataSet();

            try
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetValidation", clsConnection.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return null; // Error
            }
            return ds;
        }
        public System.Data.DataTable GetDrRoomIds(int donerId, long intDeptId = 0, short intAvailableStatus = 0)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetDrRoomIds", clsConnection.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DonerId", donerId);
                    cmd.Parameters.AddWithValue("@IntDeptId", intDeptId);
                    cmd.Parameters.AddWithValue("@IntAvailableStatus", intAvailableStatus);

                    Dr = clsConnection.ExecuteReader(cmd);
                }
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return Dr;
        }
        public System.Data.DataTable GetDaysForDonner(int donerId, int roomId)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetDaysForDonner", clsConnection.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoomId", roomId);
                    Dr = clsConnection.ExecuteReader(cmd);
                }
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return Dr;
        }
        public System.Data.DataTable GetDaysForAnnadan(int donerId)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("GetDaysForAnnadan", clsConnection.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DonerId", donerId);
                    Dr = clsConnection.ExecuteReader(cmd);
                }
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return Dr;
        }
        public int GetrDonnerRoomId(long checkInMstId)
        {
            int donnerRoomId = 0;
            try
            {
                using (SqlCommand command = new SqlCommand("SP_GetDonnerRoomId", clsConnection.GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CheckInMstId", checkInMstId);
                    command.ExecuteNonQuery();
                    Dr = clsConnection.ExecuteReader(command);
                    if (Dr.Rows.Count > 0)
                    {
                        donnerRoomId = Convert.ToInt32(Dr.Rows[0]["Donner_room_Id"]);
                    }
                }
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return donnerRoomId;
        }
        public System.Data.DataSet GetDataForGrid(Int64 intDeptId = 0, int Loc_ID = 0)
        {
            System.Data.DataSet ds = new System.Data.DataSet();

            SqlCommand command = new SqlCommand("SP_GetRoomDataForGrid", clsConnection.GetConnection());
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@intDeptId", intDeptId);
            command.Parameters.AddWithValue("@Loc_ID", Loc_ID);

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return ds;
        }
        public long updateRoomMst(int p1, int intActiveInactiveStatus = 0, int intAvailableStatus = 0)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateRoomMst", clsConnection.GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RoomId", p1);
                    command.Parameters.AddWithValue("@IntActiveInactiveStatus", intActiveInactiveStatus);
                    command.Parameters.AddWithValue("@IntAvailableStatus", intAvailableStatus);

                    return clsConnection.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                return -10;
            }
        }
        public long Insert(DamagedRooms objDamagedLkrs, int roomID)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("SP_InsertDamagedRoom", clsConnection.GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RoomID", roomID);
                    command.Parameters.AddWithValue("@Reason", objDamagedLkrs.Reason);
                    command.Parameters.AddWithValue("@Date", objDamagedLkrs.sDate);
                    command.Parameters.AddWithValue("@EnteredBy", objDamagedLkrs.EnteredBy);

                    return clsConnection.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                return -10;
            }
        }
        public long Delete(DamagedRooms objDamagedLkrs, int roomId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("SP_DeleteDamagedRoom", clsConnection.GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RoomId", roomId);

                    return clsConnection.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                return -10;
            }
        }
        public System.Data.DataSet GetChkOutWarnigGrid(int intCtrMachId, string strDate, int locId = 0)
        {
            System.Data.DataSet ds = new System.Data.DataSet();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_GetCheckOutWarningGrid", clsConnection.GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CtrMachId", intCtrMachId);
                    command.Parameters.AddWithValue("@Date", strDate);
                    command.Parameters.AddWithValue("@LocId", locId);

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
        public System.Data.DataSet GetMoreThan3Days(int intCtrMachId, string strDate, int LocID = 0)
        {
            System.Data.DataSet ds = new System.Data.DataSet();

            try
            {
                SqlCommand command = new SqlCommand("SP_GetRoomMoreThan3Days", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);
                command.Parameters.AddWithValue("@strDate", strDate);
                command.Parameters.AddWithValue("@LocID", LocID);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }

            return ds;
        }
        public System.Data.DataSet GetDataForFillingFromOnline(string id)
        {
            System.Data.DataSet ds = new System.Data.DataSet();

            try
            {
                SqlCommand command = new SqlCommand("SP_GetDataForFillingFromOnline", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", id);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return ds;
        }
        public System.Data.DataSet GetDsRoomDetails(Int64 intDeptId = 0, int intActiveInactiveStatus = 0, int intAvailableStatus = 0, int LocID = 0)
        {
            System.Data.DataSet ds = new System.Data.DataSet();

            try
            {
                SqlCommand command = new SqlCommand("SP_GetRoomListDetails", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@DeptId", intDeptId);
                command.Parameters.AddWithValue("@ActiveInactiveStatus", intActiveInactiveStatus);
                command.Parameters.AddWithValue("@AvailableStatus", intAvailableStatus);
                command.Parameters.AddWithValue("@LocId", LocID);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return ds;
        }
        public System.Data.DataTable GetDrRoomLDetails(Int64 intDeptId = 0, int intActiveInactiveStatus = 0 ,int intAvailableStatus = 0,int locid = 0)
        {
            try
            {
                    SqlCommand command = new SqlCommand("SP_GetRoomDetailsSummary", clsConnection.GetConnection());
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DeptId", intDeptId);
                    command.Parameters.AddWithValue("@LocId", locid);

                    Dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }

            return Dr;
        }
    }
}
