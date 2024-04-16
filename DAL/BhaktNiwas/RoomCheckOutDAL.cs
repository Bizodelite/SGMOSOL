using IDAutomation.Windows.Forms.LinearBarCode;
using Microsoft.Office.Interop.Excel;
using Microsoft.VisualBasic;
using SGMOSOL.ADMIN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using static SGMOSOL.BAL.BhaktNiwasBAL;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SGMOSOL.DAL.BhaktNiwas
{
    internal class RoomCheckOutDAL
    {
        CommonFunctions cf = new CommonFunctions();
        System.Data.DataTable Dr = new System.Data.DataTable();
        RoomCheckInDAL RoomCheckInDALobj = new RoomCheckInDAL();
        public System.Data.DataTable GetDrMaxSrNo(long ctrMachId = 0, long comId = 0, long locId = 0, long deptId = 0, long fyId = 0)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("SP_GetMaxRoomCheckOutSerialNo", clsConnection.GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CtrMachId", ctrMachId);
                    command.Parameters.AddWithValue("@ComId", comId);
                    command.Parameters.AddWithValue("@LocId", locId);
                    command.Parameters.AddWithValue("@DeptId", deptId);
                    command.Parameters.AddWithValue("@FYId", fyId);

                    Dr = clsConnection.ExecuteReader(command);
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
        public System.Data.DataTable GetDrRoomCheckOutMst(long roomCheckOutMstId = 0, DateTime? date = null, long serialNo = 0, long ctrMachId = 0, long comId = 0, long locId = 0, long deptId = 0, long fyId = 0, string userName = null)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("SP_GetRoomCheckOutMst", clsConnection.GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RoomCheckOutMstId", roomCheckOutMstId);
                    command.Parameters.AddWithValue("@Date", date);
                    command.Parameters.AddWithValue("@SerialNo", serialNo);
                    command.Parameters.AddWithValue("@CtrMachId", ctrMachId);
                    command.Parameters.AddWithValue("@ComId", comId);
                    command.Parameters.AddWithValue("@LocId", locId);
                    command.Parameters.AddWithValue("@DeptId", deptId);
                    command.Parameters.AddWithValue("@FYId", fyId);
                    command.Parameters.AddWithValue("@UserName", userName);

                    Dr = clsConnection.ExecuteReader(command);
                }
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }

            return Dr;
        }
        public System.Data.DataTable GetDrRoomCheckOutDet(long lngRoomCheckOutMstId = 0, bool blnAll = false, long lngCtrMachId = 0)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("SP_GetRoomCheckOutDet", clsConnection.GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the stored procedure
                    command.Parameters.AddWithValue("@lngRoomCheckOutMstId", lngRoomCheckOutMstId);
                    command.Parameters.AddWithValue("@blnAll", blnAll);
                    command.Parameters.AddWithValue("@lngCtrMachId", lngCtrMachId);

                    Dr = clsConnection.ExecuteReader(command);
                }
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return Dr;
        }
        public long Insert(RoomCheckOutMst RoomCheckOutMst, Collection coll, string strUserName, string strMacName, long lngSerialNo, DateTime EndteredOn)
        {
            System.Data.DataTable dr;
            long lngMstId = 0;
            long lngErrNo;
            try
            {
                clsConnection.glbTransaction = clsConnection.glbCon.BeginTransaction();

                //SetError("Inserting Into BN_ROOM_CHECK_OUT_MST_T ");
                lngErrNo = InsertMaster(RoomCheckOutMst, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }

                dr = GetDrRoomCheckOutMstId(strMacName);
                if (dr.Rows.Count > 0)
                {
                    lngMstId = Convert.ToInt64(dr.Rows[0]["CheckOutMstId"]);
                    lngSerialNo = Convert.ToInt64(dr.Rows[0]["SerialNo"]);
                    EndteredOn = Convert.ToDateTime(dr.Rows[0]["EnteredOn"]);
                }

                //SetError("Inserting Into BN_ROOM_CHECK_OUT_DET_T ");
                lngErrNo = InsertDetail(coll, lngMstId, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }
                lngErrNo = UpdateCheckInMstRent(RoomCheckOutMst, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }

                //SetError("Updating Room Status");
                lngErrNo = UpdateStatus(coll, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }

                if (RoomCheckOutMst.BhaktTypeId == 4)
                {
                    //SetError("Updating Days for Donner");
                    lngErrNo = UpdateDaysForDonner(RoomCheckOutMst.Days, RoomCheckOutMst.DnrRoomId);
                    if (lngErrNo < 0)
                    {
                        clsConnection.glbTransaction.Rollback();
                        return lngErrNo;
                    }
                }



                clsConnection.glbTransaction.Commit();
                return lngSerialNo;
            }
            catch (Exception ex)
            {
                clsConnection.glbTransaction.Rollback();
                //SetError(ex.ToString());
                return -1;
            }
        }
        public long InsertMaster(RoomCheckOutMst roomCheckOutMst, string userName, string machineName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("SP_InsertRoomCheckOutMst", clsConnection.GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ComId", roomCheckOutMst.ComId);
                    command.Parameters.AddWithValue("@LocId", roomCheckOutMst.LocId);
                    command.Parameters.AddWithValue("@DeptId", roomCheckOutMst.DeptId);
                    command.Parameters.AddWithValue("@CtrMachId", roomCheckOutMst.CtrMachId);
                    command.Parameters.AddWithValue("@FyId", roomCheckOutMst.FyId);
                    command.Parameters.AddWithValue("@OutDate", roomCheckOutMst.OutDate);
                    command.Parameters.AddWithValue("@OutTime", roomCheckOutMst.OutTime);
                    command.Parameters.AddWithValue("@CheckInMstId", roomCheckOutMst.CheckInMstId);
                    command.Parameters.AddWithValue("@Days", roomCheckOutMst.Days);
                    command.Parameters.AddWithValue("@NoOfRooms", roomCheckOutMst.NoOfRooms);
                    command.Parameters.AddWithValue("@Advance", roomCheckOutMst.Advance);
                    command.Parameters.AddWithValue("@Rent", roomCheckOutMst.Rent);
                    command.Parameters.AddWithValue("@Refund", roomCheckOutMst.Refund);
                    command.Parameters.AddWithValue("@UserId", roomCheckOutMst.UserId);
                    command.Parameters.AddWithValue("@ServerName", roomCheckOutMst.ServerName);
                    command.Parameters.AddWithValue("@EnteredBy", userName);
                    command.Parameters.AddWithValue("@ModifiedBy", userName);
                    command.Parameters.AddWithValue("@MachineName", machineName);

                    return clsConnection.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -10; // Error code
            }
        }
        public System.Data.DataTable GetDrRoomCheckOutMstId(string machineName = "")
        {
            try
            {
                using (SqlCommand command = new SqlCommand("SP_GetRoomCheckOutMstId", clsConnection.GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MachineName", machineName);

                    Dr = clsConnection.ExecuteReader(command);
                }
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return Dr;
        }
        public long InsertDetail(Collection coll, long roomCheckOutMstId, string userName, string machineName)
        {
            try
            {
                foreach (RoomCheckOutDet item in coll)
                {
                    using (SqlCommand command = new SqlCommand("SP_InsertRoomCheckOutDet", clsConnection.GetConnection()))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@RoomCheckOutMstId", roomCheckOutMstId);
                        command.Parameters.AddWithValue("@RoomId", item.LockerId);

                        long A = clsConnection.ExecuteNonQuery(command);
                        if (A > 0)
                        {
                            return A;
                        }
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -1; // Error
            }
        }
        public long UpdateCheckInMstRent(RoomCheckOutMst roomCheckOutMst, string userName, string machineName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateCheckInMstRent", clsConnection.GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Rent", roomCheckOutMst.Rent);
                    command.Parameters.AddWithValue("@Days", roomCheckOutMst.Days);
                    command.Parameters.AddWithValue("@ModifiedBy", userName);
                    command.Parameters.AddWithValue("@CheckInMstId", roomCheckOutMst.CheckInMstId);

                    return clsConnection.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -1; // Error
            }
        }
        public long UpdateDaysForDonner(int dnrRmnedDays, int roomId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("UpdateDaysForDonner", clsConnection.GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DnrRmnedDays", dnrRmnedDays);
                    command.Parameters.AddWithValue("@RoomId", roomId);
                    return clsConnection.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -1; // Error
            }
        }
        public long UpdateStatus(Collection Coll, string strUserName, string strMachineName)
        {
            try
            {
                foreach (RoomCheckOutDet item in Coll)
                {
                    using (SqlCommand cmd = new SqlCommand("SP_UpdateRoomStatus", clsConnection.GetConnection()))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@LockerId", item.LockerId);
                        cmd.Parameters.AddWithValue("@LockerAvailableStatus", item.LockerAvailableStatus);
                        cmd.Parameters.AddWithValue("@ModifiedBy", strUserName);
                        cmd.Parameters.AddWithValue("@RecordModifiedCount", item.LockerRecordModifiedCount);
                        long A = clsConnection.ExecuteNonQuery(cmd);
                        if (A > 0)
                        {
                            return A;
                        }
                    }
                }
                return 0; // Success
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -1; // Error
            }
        }
        public long Update(RoomCheckOutMst RoomCheckOutMst, Collection coll, Collection DelColl, string strUserName, string strMacName, long lngSerialNo)
        {
            Collection actualDelColl = new Collection();
            Collection actualInsColl = new Collection();

            Int16 ctr;
            Int16 ctr1;
            RoomCheckOutDet DelCheckOutDet = new RoomCheckOutDet();
            RoomCheckOutDet InsCheckOutDet = new RoomCheckOutDet();
            bool blnFound = false;

            // -----Making Delete Collection
            for (ctr = 1; ctr <= DelColl.Count; ctr++)
            {
                DelCheckOutDet = (RoomCheckOutDet)DelColl[ctr];
                blnFound = false;
                for (ctr1 = 1; ctr1 <= coll.Count; ctr1++)
                {
                    InsCheckOutDet = (RoomCheckOutDet)coll[ctr1];
                    if (DelCheckOutDet.LockerId == InsCheckOutDet.LockerId)
                    {
                        blnFound = true;
                        break;
                    }
                }
                if (!blnFound)
                    actualDelColl.Add(DelCheckOutDet);
            }

            // ----Making Insert Collection
            for (ctr = 1; ctr <= coll.Count; ctr++)
            {
                InsCheckOutDet = (RoomCheckOutDet)coll[ctr];
                blnFound = false;
                for (ctr1 = 1; ctr1 <= DelColl.Count; ctr1++)
                {
                    DelCheckOutDet = (RoomCheckOutDet)DelColl[ctr1];
                    if (DelCheckOutDet.LockerId == InsCheckOutDet.LockerId)
                    {
                        blnFound = true;
                        break;
                    }
                }
                if (!blnFound)
                    actualInsColl.Add(InsCheckOutDet);
            }


            try
            {
                clsConnection.glbTransaction = clsConnection.glbCon.BeginTransaction();

                long lngErrNo;
                if (DelColl.Count > 0)
                {
                    //SetError("Deleting LOCK_LOCKER_CHECK_OUT_DET_T ");
                    lngErrNo = Delete(actualDelColl, strUserName, strMacName);
                    if (lngErrNo < 0 & lngErrNo != -6)
                    {
                        clsConnection.glbTransaction.Rollback();
                        return lngErrNo;
                    }
                }

                //SetError("Inserting Into LOCK_LOCKER_CHECK_OUT_DET_T ");
                lngErrNo = InsertDetail(actualInsColl, RoomCheckOutMst.CheckOutMstId, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }

                //SetError("Updating Into LOCK_LOCKER_CHECK_OUT_MST_T ");
                lngErrNo = UpdateMaster(RoomCheckOutMst, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }

                //SetError("Updating Locker Status for Delete Coll");
                lngErrNo = UpdateStatus(actualDelColl, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }

                //SetError("Updating Locker Status for Insert Coll");
                lngErrNo = UpdateStatus(actualInsColl, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }

                clsConnection.glbTransaction.Commit();
                return 1;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.ToString());
                clsConnection.glbTransaction.Rollback();
                //SetError(ex.ToString());
                return -1;
            }
        }
        public long Delete(Collection coll, string userName, string machineName)
        {
            try
            {
                foreach (RoomCheckOutDet item in coll)
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteRoomCheckOutDet", clsConnection.GetConnection()))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameter to the stored procedure
                        command.Parameters.AddWithValue("@CheckOutDetId", item.CheckOutDetId);

                        long A = clsConnection.ExecuteNonQuery(command);
                        if (A > 0)
                        {
                            return A;
                        }
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version); Console.WriteLine("Error: " + ex.Message);
                return -1; // Error
            }
        }
        public long UpdateMaster(RoomCheckOutMst roomCheckOutMst, string userName, string machineName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateRoomCheckOutMst", clsConnection.GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Days", roomCheckOutMst.Days);
                    command.Parameters.AddWithValue("@NoOfRooms", roomCheckOutMst.NoOfRooms);
                    command.Parameters.AddWithValue("@Advance", roomCheckOutMst.Advance);
                    command.Parameters.AddWithValue("@Rent", roomCheckOutMst.Rent);
                    command.Parameters.AddWithValue("@Refund", roomCheckOutMst.Refund);
                    command.Parameters.AddWithValue("@ModifiedBy", userName);
                    command.Parameters.AddWithValue("@MachineName", machineName);
                    command.Parameters.AddWithValue("@RecordModifiedCount", roomCheckOutMst.RecordModifiedCount);
                    command.Parameters.AddWithValue("@CheckOutMstId", roomCheckOutMst.CheckOutMstId);

                    return clsConnection.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -1; // Error
            }
        }
    }
}
