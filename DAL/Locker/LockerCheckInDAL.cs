using Microsoft.Office.Interop.Excel;
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
    internal class LockerCheckInDAL
    {
        string connectionString = CommonFunctions.Decrypt(ConfigurationManager.ConnectionStrings["strConnection"].ConnectionString, true);
        CommonFunctions commonFunctions = new CommonFunctions();
        System.Text.StringBuilder strDML = new System.Text.StringBuilder();
        System.Text.StringBuilder strSQL = new System.Text.StringBuilder();
        long lngErrNum = 0;
        System.Data.DataTable Dr = new System.Data.DataTable();
        private void SetError(string str)
        {
            clsConnection.mErrorResult = clsConnection.mErrorResult + Microsoft.VisualBasic.Constants.vbNewLine + str;
        }
        public System.Data.DataTable GetDrMaxSrNo(long lngCtrMachId = 0, long lngComId = 0, long lngLocId = 0, long lngDeptId = 0, long lngFYId = 0)
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_GetMaxSerialNo", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@lngCtrMachId", lngCtrMachId);
                command.Parameters.AddWithValue("@lngComId", lngComId);
                command.Parameters.AddWithValue("@lngLocId", lngLocId);
                command.Parameters.AddWithValue("@lngDeptId", lngDeptId);
                command.Parameters.AddWithValue("@lngFYId", lngFYId);

                Dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                lngErrNum = -91;
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return Dr;
        }
        public int GetDaysLimit()
        {
            int EXPIRY_DAYS = 0;
            try
            {
                SqlCommand command = new SqlCommand("SP_GetExpiryDays", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                System.Data.DataTable reader = clsConnection.ExecuteReader(command);

                if (reader != null && reader.Rows.Count > 0)
                {
                    EXPIRY_DAYS = Convert.ToInt32(reader.Rows[0]["EXPIRY_DAYS"]);
                }

            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exception or logging
            }
            return EXPIRY_DAYS;
        }
        public int GetLockersLimit()
        {
            int MAX_LOCKERS = 0;
            try
            {
                SqlCommand command = new SqlCommand("SP_GetMaxLockers", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                using (System.Data.DataTable reader = clsConnection.ExecuteReader(command))
                {
                    if (reader != null && reader.Rows.Count > 0)
                    {
                        MAX_LOCKERS = Convert.ToInt32(reader.Rows[0]["MAX_LOCKERS"]);
                    }
                }
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exception or logging
            }
            return MAX_LOCKERS;
        }
        public long Insert(LockerCheckInMst LockerCheckInMst, Collection coll, string strUserName, string strMacName, long lngSerialNo, DateTime EndteredOn)
        {
            long lngMstId = 0;
            long lngErrNo = 0;
            try
            {
                clsConnection.glbTransaction = clsConnection.glbCon.BeginTransaction();


                SetError("Inserting Into LOCK_LOCKER_CHECK_IN_MST_T ");
                lngErrNo = InsertMaster(LockerCheckInMst, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }

                Dr = GetDrLockerCheckInMstId(strMacName);
                foreach (DataRow dr in Dr.Rows)
                {
                    lngMstId = Convert.ToInt64(dr["CheckInMstId"]);
                    lngSerialNo = Convert.ToInt64(dr["SerialNo"]);
                    EndteredOn = Convert.ToDateTime(dr["EnteredOn"]);
                    break;
                }
                //dr.Close();

                SetError("Inserting Into LOCKER_CHECK_IN_DET_T ");
                lngErrNo = InsertDetail(coll, lngMstId, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }

                SetError("Updating Locker Status");
                lngErrNo = UpdateStatus(coll, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }

                clsConnection.glbTransaction.Commit();
                return lngSerialNo;
            }
            catch (Exception ex)
            {
                //if (dr != null)
                //    dr.Close();
                commonFunctions.InsertErrorLog(ex.ToString(), UserInfo.module, UserInfo.version);
                return -1;
            }
        }
        public long InsertMaster(LockerCheckInMst LockerCheckInMst, string strUserName, string strMachineName)
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_InsertLockerCheckInMst", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ComId", LockerCheckInMst.ComId);
                command.Parameters.AddWithValue("@LocId", LockerCheckInMst.LocId);
                command.Parameters.AddWithValue("@DeptId", LockerCheckInMst.DeptId);
                command.Parameters.AddWithValue("@CtrMachId", LockerCheckInMst.CtrMachId);
                command.Parameters.AddWithValue("@FyId", LockerCheckInMst.FyId);
                command.Parameters.AddWithValue("@InDate", LockerCheckInMst.InDate);
                command.Parameters.AddWithValue("@InTime", LockerCheckInMst.InTime);
                command.Parameters.AddWithValue("@Name", LockerCheckInMst.Name);
                command.Parameters.AddWithValue("@Place", LockerCheckInMst.Place);
                command.Parameters.AddWithValue("@Days", LockerCheckInMst.Days);
                command.Parameters.AddWithValue("@NoOfLockers", LockerCheckInMst.NoOfLockers);
                command.Parameters.AddWithValue("@OutDate", LockerCheckInMst.OutDate);
                command.Parameters.AddWithValue("@OutTime", LockerCheckInMst.OutTime);
                command.Parameters.AddWithValue("@Advance", LockerCheckInMst.Advance);
                command.Parameters.AddWithValue("@Rent", LockerCheckInMst.Rent);
                command.Parameters.AddWithValue("@UserId", LockerCheckInMst.UserId);
                command.Parameters.AddWithValue("@ServerName", LockerCheckInMst.ServerName);
                command.Parameters.AddWithValue("@EnteredBy", strUserName);
                command.Parameters.AddWithValue("@ModifiedBy", strUserName);
                command.Parameters.AddWithValue("@MachineName", strMachineName);
                command.Parameters.AddWithValue("@MobNo", LockerCheckInMst.mob_no);

                return clsConnection.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -10; // Error
            }
        }

        public long InsertDetail(Collection coll, long lngLockerCheckInMstId, string strUserName, string strMachineName)
        {
            try
            {
                foreach (LockerCheckInDet item in coll)
                {
                    SqlCommand command = new SqlCommand("SP_InsertLockerCheckInDet", clsConnection.GetConnection());
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CheckInMstId", lngLockerCheckInMstId);
                    command.Parameters.AddWithValue("@LockerId", item.LockerId);

                    long A = clsConnection.ExecuteNonQuery(command);
                    if (A > 0)
                    {
                        return A;
                    }
                }
                return 0; // Success
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -10; // Error
            }
        }
        public long UpdateStatus(Collection Coll, string strUserName, string strMachineName)
        {
            try
            {
                foreach (LockerCheckInDet item in Coll)
                {
                    SqlCommand command = new SqlCommand("SP_UpdateLockerStatus", clsConnection.GetConnection());
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@LockerId", item.LockerId);
                    command.Parameters.AddWithValue("@AvailableStatus", item.LockerAvailableStatus);
                    command.Parameters.AddWithValue("@ModifiedBy", strUserName);
                    command.Parameters.AddWithValue("@RecordModifiedCount", item.LockerRecordModifiedCount);

                    long A = clsConnection.ExecuteNonQuery(command);
                    if (A > 0)
                    {
                        return A;
                    }
                }
                return 0; // Success
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -10; // Error
            }
        }

        public System.Data.DataTable GetDrLockerCheckInMstId(string strMachineName = "")
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_GetLockerCheckInMstId", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@MachineName", strMachineName);
                Dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                lngErrNum = -42205;
            }
            return Dr;
        }

        public long Update(LockerCheckInMst LockerCheckInMst, Collection coll, Collection DelColl, string strUserName, string strMacName, long lngSerialNo)
        {
            Collection actualDelColl = new Collection();
            Collection actualInsColl = new Collection();

            Int16 ctr;
            Int16 ctr1;
            LockerCheckInDet DelCheckInDet = new LockerCheckInDet();
            LockerCheckInDet InsCheckInDet = new LockerCheckInDet();
            bool blnFound = false;

            // -----Making Delete Collection
            for (ctr = 1; ctr <= DelColl.Count; ctr++)
            {
                DelCheckInDet = (LockerCheckInDet)DelColl[ctr];
                blnFound = false;
                for (ctr1 = 1; ctr1 <= coll.Count; ctr1++)
                {
                    InsCheckInDet = (LockerCheckInDet)coll[ctr1];
                    if (DelCheckInDet.LockerId == InsCheckInDet.LockerId)
                    {
                        blnFound = true;
                        break;
                    }
                }
                if (!blnFound)
                    actualDelColl.Add(DelCheckInDet);
            }

            // ----Making Insert Collection
            for (ctr = 1; ctr <= coll.Count; ctr++)
            {
                InsCheckInDet = (LockerCheckInDet)coll[ctr];
                blnFound = false;
                for (ctr1 = 1; ctr1 <= DelColl.Count; ctr1++)
                {
                    DelCheckInDet = (LockerCheckInDet)DelColl[ctr1];
                    if (DelCheckInDet.LockerId == InsCheckInDet.LockerId)
                    {
                        blnFound = true;
                        break;
                    }
                }
                if (!blnFound)
                    actualInsColl.Add(InsCheckInDet);
            }


            try
            {
                clsConnection.glbTransaction = clsConnection.glbCon.BeginTransaction();



                long lngErrNo;
                if (DelColl.Count > 0)
                {
                    SetError("Deleting from LOCK_LOCKER_CHECK_IN_DET_T ");
                    lngErrNo = DeleteDetail(actualDelColl, strUserName, strMacName);
                    if (lngErrNo < 0 & lngErrNo != -6)
                    {
                        clsConnection.glbTransaction.Rollback();
                        return lngErrNo;
                    }
                }

                SetError("Inserting Into LOCK_LOCKER_CHECK_IN_DET_T ");
                lngErrNo = InsertDetail(actualInsColl, LockerCheckInMst.CheckInMstId, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }

                SetError("Updating Into LOCK_LOCKER_CHECK_IN_MST_T ");
                lngErrNo = UpdateMaster(LockerCheckInMst, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }

                SetError("Updating Locker Status for Delete Coll");
                lngErrNo = UpdateStatus(actualDelColl, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }

                SetError("Updating Locker Status for Insert Coll");
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
                SetError(ex.ToString());
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -1;
            }
        }
        public long Insert1(LockerChangeMst LockerChangeMst, Collection coll, string strUserName, string strMacName, long lngSerialNo, DateTime EnteredOn)
        {
            long lngErrNo = 0;

            try
            {
                clsConnection.glbTransaction = clsConnection.glbCon.BeginTransaction();

                foreach (var item in coll)
                {
                    LockerCheckInDet lockerCheckInDet = (LockerCheckInDet)item;
                    LockerChangeMst.PrevLkrId = lockerCheckInDet.LockerId;
                    SqlCommand command = new SqlCommand("SP_InsertLockerChange", clsConnection.GetConnection());

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CheckInMstId", LockerChangeMst.CheckInMstId);
                    command.Parameters.AddWithValue("@PrevLockerId", LockerChangeMst.PrevLkrId);
                    command.Parameters.AddWithValue("@Reason", LockerChangeMst.Reason);
                    command.Parameters.AddWithValue("@UserId", LockerChangeMst.UserId);
                    command.Parameters.AddWithValue("@ServerName", LockerChangeMst.ServerName);
                    command.Parameters.AddWithValue("@EnteredBy", strUserName);
                    command.Parameters.AddWithValue("@ModifiedBy", strUserName);
                    command.Parameters.AddWithValue("@MachineName", strMacName);

                    lngErrNo = -88;
                    lngErrNo = clsConnection.ExecuteNonQuery(command);

                    if (lngErrNo < 0)
                    {
                        clsConnection.glbTransaction.Rollback();
                        return lngErrNo;
                    }
                }

                clsConnection.glbTransaction.Commit();
                return lngErrNo;
            }
            catch (Exception ex)
            {
                clsConnection.glbTransaction.Rollback();
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                SetError(ex.ToString());
                return -1;
            }
        }

        public long InsertRoomExtend(LockerCheckInMst RoomChangeMst, string strUserName, string strMacName)
        {
            long lngErrNo = 0;

            try
            {
                clsConnection.glbTransaction = clsConnection.glbCon.BeginTransaction();

                SqlCommand command = new SqlCommand("SP_InsertRoomExtend", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@LocId", RoomChangeMst.LocId);
                command.Parameters.AddWithValue("@DeptId", RoomChangeMst.DeptId);
                command.Parameters.AddWithValue("@CtrMachId", RoomChangeMst.CtrMachId);
                command.Parameters.AddWithValue("@CheckInMstId", RoomChangeMst.CheckInMstId);
                command.Parameters.AddWithValue("@ExtDate", RoomChangeMst.ExtDate);
                command.Parameters.AddWithValue("@ExtRent", RoomChangeMst.ExtRent);
                command.Parameters.AddWithValue("@ExtDay", RoomChangeMst.ExtDay);
                command.Parameters.AddWithValue("@EnteredBy", strUserName);

                lngErrNo = -88;
                lngErrNo = clsConnection.ExecuteNonQuery(command);

                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }

                clsConnection.glbTransaction.Commit();
                return lngErrNo;
            }
            catch (Exception ex)
            {
                clsConnection.glbTransaction.Rollback();
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                SetError(ex.ToString());
                return -1;
            }
        }


        public long DeleteDetail(Collection coll, string strUserName, string strMachineName)
        {
            try
            {
                for (int i = 1; i < coll.Count + 1; i++)
                {
                    SqlCommand command = new SqlCommand("SP_DeleteLockerDetail", clsConnection.GetConnection());
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CheckInMstId", ((LockerCheckInDet)coll[i]).CheckInMstId);
                    command.Parameters.AddWithValue("@LockerId", ((LockerCheckInDet)coll[i]).LockerId);

                    lngErrNum = -90;
                    lngErrNum = clsConnection.ExecuteNonQuery(command);

                    if (lngErrNum != 0)
                    {
                        lngErrNum = -1;
                        return lngErrNum;
                    }
                }
                return lngErrNum;
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                strDML.Length = 0;
                return -10;
            }
        }

        public long UpdateMaster(LockerCheckInMst LockerCheckInMst, string strUserName, string strMachineName)
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_UpdateLockerMaster", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CheckInMstId", LockerCheckInMst.CheckInMstId);
                command.Parameters.AddWithValue("@Name", LockerCheckInMst.Name);
                command.Parameters.AddWithValue("@Place", LockerCheckInMst.Place);
                command.Parameters.AddWithValue("@Days", LockerCheckInMst.Days);
                command.Parameters.AddWithValue("@NoOfLockers", LockerCheckInMst.NoOfLockers);
                command.Parameters.AddWithValue("@OutDate", LockerCheckInMst.OutDate);
                command.Parameters.AddWithValue("@OutTime", LockerCheckInMst.OutTime);
                command.Parameters.AddWithValue("@Advance", LockerCheckInMst.Advance);
                command.Parameters.AddWithValue("@Rent", LockerCheckInMst.Rent);
                command.Parameters.AddWithValue("@ModifiedBy", strUserName);
                command.Parameters.AddWithValue("@MachineName", strMachineName);
                command.Parameters.AddWithValue("@MobNo", LockerCheckInMst.mob_no);
                command.Parameters.AddWithValue("@RecordModifiedCount", LockerCheckInMst.RecordModifiedCount);

                lngErrNum = -89;
                lngErrNum = clsConnection.ExecuteNonQuery(command);
                return lngErrNum;
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -10;
            }
        }


        public System.Data.DataTable GetDrLockerCheckInMst(long lngLockerCheckInMstId = 0, string strDate = "", long lngSerialNo = 0, long lngCtrMachId = 0, long lngComId = 0, long lngLocId = 0, long lngDeptId = 0, long lngFYId = 0, string strUserName = "")
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetLockerCheckInMst", clsConnection.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@lngLockerCheckInMstId", lngLockerCheckInMstId);
                cmd.Parameters.AddWithValue("@strDate", strDate);
                cmd.Parameters.AddWithValue("@lngSerialNo", lngSerialNo);
                cmd.Parameters.AddWithValue("@lngComId", lngComId);
                cmd.Parameters.AddWithValue("@lngLocId", lngLocId);
                cmd.Parameters.AddWithValue("@lngDeptId", lngDeptId);

                Dr = clsConnection.ExecuteReader(cmd);
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                lngErrNum = -91;
            }
            return Dr;
        }


        public System.Data.DataTable GetDrLockerCheckInDet(long lngLockerCheckInMstId = 0, bool blnAll = false, long lngCtrMachId = 0)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetLockerCheckInDet", clsConnection.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@lngLockerCheckInMstId", lngLockerCheckInMstId);
                cmd.Parameters.AddWithValue("@blnAll", blnAll);
                cmd.Parameters.AddWithValue("@lngCtrMachId", lngCtrMachId);

                Dr = clsConnection.ExecuteReader(cmd);
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                lngErrNum = -91;
            }
            return Dr;
        }


    }
}
