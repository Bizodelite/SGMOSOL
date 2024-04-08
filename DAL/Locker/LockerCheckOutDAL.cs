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
using System.Windows.Forms;
using static SGMOSOL.BAL.LockerBAL;

namespace SGMOSOL.DAL
{
    internal class LockerCheckOutDAL
    {
        long lngErrNum = 0;
        SqlCommand mCmd = new SqlCommand();
        SqlConnection mCnn = new SqlConnection();
        CommonFunctions cf = new CommonFunctions();
        System.Text.StringBuilder strDML = new System.Text.StringBuilder();
        System.Text.StringBuilder strSQL = new System.Text.StringBuilder();
        DataTable dr = new DataTable();

        public DataTable GetDrMaxSrNo(long lngCtrMachId = 0, long lngComId = 0, long lngLocId = 0, long lngDeptId = 0, long lngFYId = 0)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetMaxSrNo", clsConnection.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@lngCtrMachId", lngCtrMachId);
                cmd.Parameters.AddWithValue("@lngComId", lngComId);
                cmd.Parameters.AddWithValue("@lngLocId", lngLocId);
                cmd.Parameters.AddWithValue("@lngDeptId", lngDeptId);
                cmd.Parameters.AddWithValue("@lngFYId", lngFYId);

                dr = clsConnection.ExecuteReader(cmd);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                lngErrNum = -91;
            }
            return dr;
        }

        public long Insert(LockerCheckOutMst LockerCheckOutMst, Collection coll, string strUserName, string strMacName, long lngSerialNo, DateTime EndteredOn)
        {
            long lngMstId = 0;
            long lngErrNo = 0;
            try
            {
                clsConnection.glbTransaction = clsConnection.glbCon.BeginTransaction();

                SetError("Inserting Into LOCK_LOCKER_CHECK_OUT_MST_T ");
                lngErrNo = InsertMaster(LockerCheckOutMst, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }

                dr = GetDrLockerCheckOutMstId(strMacName);
                foreach (DataRow dritem in dr.Rows)
                {
                    lngMstId = Convert.ToInt64(dritem["CheckOutMstId"]);
                    lngSerialNo = Convert.ToInt64(dritem["SerialNo"]);
                    EndteredOn = Convert.ToDateTime(dritem["EnteredOn"]);
                }
                //dr.Close();

                SetError("Inserting Into LOCKER_CHECK_OUT_DET_T ");
                lngErrNo = InsertDetail(coll, lngMstId, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }
                lngErrNo = UpdateCheckInMstRent(LockerCheckOutMst, strUserName, strMacName);
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
                clsConnection.glbTransaction.Rollback();
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                SetError(ex.ToString());
                return -1;
            }
        }
        public long InsertMaster(LockerCheckOutMst LockerCheckOutMst, string strUserName, string strMachineName)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_InsertLockerCheckOutMst", clsConnection.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ComId", LockerCheckOutMst.ComId);
                cmd.Parameters.AddWithValue("@LocId", LockerCheckOutMst.LocId);
                cmd.Parameters.AddWithValue("@DeptId", LockerCheckOutMst.DeptId);
                cmd.Parameters.AddWithValue("@CtrMachId", LockerCheckOutMst.CtrMachId);
                cmd.Parameters.AddWithValue("@FyId", LockerCheckOutMst.FyId);
                cmd.Parameters.AddWithValue("@OutDate", LockerCheckOutMst.OutDate);
                cmd.Parameters.AddWithValue("@OutTime", LockerCheckOutMst.OutTime);
                cmd.Parameters.AddWithValue("@CheckInMstId", LockerCheckOutMst.CheckInMstId);
                cmd.Parameters.AddWithValue("@Days", LockerCheckOutMst.Days);
                cmd.Parameters.AddWithValue("@NoOfLockers", LockerCheckOutMst.NoOfLockers);
                cmd.Parameters.AddWithValue("@Advance", LockerCheckOutMst.Advance);
                cmd.Parameters.AddWithValue("@Rent", LockerCheckOutMst.Rent);
                cmd.Parameters.AddWithValue("@Refund", LockerCheckOutMst.Refund);
                cmd.Parameters.AddWithValue("@UserId", LockerCheckOutMst.UserId);
                cmd.Parameters.AddWithValue("@ServerName", LockerCheckOutMst.ServerName);
                cmd.Parameters.AddWithValue("@EnteredBy", strUserName);
                cmd.Parameters.AddWithValue("@ModifiedBy", strUserName);
                cmd.Parameters.AddWithValue("@MachineName", strMachineName);


                return clsConnection.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -10; // Error occurred
            }
        }

        public long InsertDetail(Collection coll, long lngLockerCheckInMstId, string strUserName, string strMachineName)
        {
            try
            {
                foreach (LockerCheckOutDet item in coll)
                {
                    SqlCommand command = new SqlCommand("SP_InsertLockerCheckOutDet", clsConnection.GetConnection());
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CheckOutMstId", lngLockerCheckInMstId);
                    command.Parameters.AddWithValue("@LockerId", item.LockerId);

                    long A = clsConnection.ExecuteNonQuery(command);
                    if (A != 0)
                    {
                        return A;
                    }
                }
                return 0; // Success
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -10; // Error
            }
        }
        public long UpdateCheckInMstRent(LockerCheckOutMst RoomCheckOutMst, string strUserName, string strMachineName)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_UpdateLockerCheckInMstRent", clsConnection.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CheckInMstId", RoomCheckOutMst.CheckInMstId);
                cmd.Parameters.AddWithValue("@Rent", RoomCheckOutMst.Rent);
                cmd.Parameters.AddWithValue("@Days", RoomCheckOutMst.Days);
                cmd.Parameters.AddWithValue("@ModifiedBy", strUserName);

                return clsConnection.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -10; // Error occurred
            }
        }
        public long UpdateStatus(Collection Coll, string strUserName, string strMachineName)
        {
            try
            {
                foreach (LockerCheckOutDet item in Coll)
                {
                    SqlCommand cmd = new SqlCommand("SP_UpdateLockerStatus", clsConnection.GetConnection());
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@LockerId", item.LockerId);
                    cmd.Parameters.AddWithValue("@AvailableStatus", item.LockerAvailableStatus);
                    cmd.Parameters.AddWithValue("@ModifiedBy", strUserName);
                    cmd.Parameters.AddWithValue("@RecordModifiedCount", item.LockerRecordModifiedCount);

                    long A = clsConnection.ExecuteNonQuery(cmd);
                    if (A != 0)
                    {
                        return A;
                    }
                }
                return 0; // Success
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -10;
            }
        }

        public DataTable GetDrLockerCheckOutMstId(string strMachineName = "")
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_GetLockerCheckOutMstId", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@MachineName", string.IsNullOrEmpty(strMachineName) ? DBNull.Value : (object)strMachineName);

                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dr;
        }



        public long Update(LockerCheckOutMst LockerCheckOutMst, Collection coll, Collection DelColl, string strUserName, string strMacName, long lngSerialNo)
        {
            Collection actualDelColl = new Collection();
            Collection actualInsColl = new Collection();

            Int16 ctr;
            Int16 ctr1;
            LockerCheckOutDet DelCheckOutDet = new LockerCheckOutDet();
            LockerCheckOutDet InsCheckOutDet = new LockerCheckOutDet();
            bool blnFound = false;

            // -----Making Delete Collection
            for (ctr = 1; ctr <= DelColl.Count + 1; ctr++)
            {
                DelCheckOutDet = (LockerCheckOutDet)DelColl[ctr];
                blnFound = false;
                for (ctr1 = 1; ctr1 <= coll.Count + 1; ctr1++)
                {
                    InsCheckOutDet = (LockerCheckOutDet)coll[ctr1];
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
            for (ctr = 1; ctr <= coll.Count + 1; ctr++)
            {
                InsCheckOutDet = (LockerCheckOutDet)coll[ctr];
                blnFound = false;
                for (ctr1 = 1; ctr1 <= DelColl.Count + 1; ctr1++)
                {
                    DelCheckOutDet = (LockerCheckOutDet)DelColl[ctr1];
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
                    SetError("Deleting LOCK_LOCKER_CHECK_OUT_DET_T ");
                    lngErrNo = DeleteDetail(actualDelColl, strUserName, strMacName);
                    if (lngErrNo < 0 & lngErrNo != -6)
                    {
                        clsConnection.glbTransaction.Rollback();
                        return lngErrNo;
                    }
                }

                SetError("Inserting Into LOCK_LOCKER_CHECK_OUT_DET_T ");
                lngErrNo = InsertDetail(actualInsColl, LockerCheckOutMst.CheckOutMstId, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }

                SetError("Updating Into LOCK_LOCKER_CHECK_OUT_MST_T ");
                lngErrNo = UpdateMaster(LockerCheckOutMst, strUserName, strMacName);
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
                return -1;
            }
        }
        //public long DeleteMaster(LockerCheckOutMst LockerCheckOutMst, string strUserName, string strMachineName)
        //{
        //    try
        //    {
        //        strDML.Append("DELETE FROM LOCK_LOCKER_CHECK_OUT_MST_T");
        //        strDML.Append(" WHERE ");
        //        strDML.Append("CHECK_OUT_MST_ID = " + LockerCheckOutMst.CheckOutMstId);
        //        lngErrNum = -90;
        //        lngErrNum = clsConnection.DML(strDML.ToString());
        //        strDML.Length = 0;
        //        return lngErrNum;
        //    }
        //    catch (Exception ex)
        //    {
        //        return -10;
        //    }
        //}
        public long DeleteDetail(Collection coll, string strUserName, string strMachineName)
        {
            try
            {
                foreach (LockerCheckOutDet item in coll)
                {
                    // Assuming clsConnection.ExecuteSP executes the stored procedure
                    //int result = clsConnection.ExecuteSP("DeleteLockerCheckOutDet", new SqlParameter("@CheckOutDetId", item.CheckOutDetId));

                    SqlCommand cmd = new SqlCommand("SP_DeleteLockerCheckOutDet", clsConnection.GetConnection());
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CheckOutDetId", item.CheckOutDetId);

                    long result = clsConnection.ExecuteNonQuery(cmd);
                    if (result != 0)
                    {
                        return result;
                    }
                }
                return 0; // Success
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exception if necessary
                return -1;
            }
        }
        public long UpdateMaster(LockerCheckOutMst lockerCheckOutMst, string strUserName, string strMachineName)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_UpdateLockerCheckOutMst", clsConnection.GetConnection());
                cmd.Parameters.AddWithValue("@CheckOutMstId", lockerCheckOutMst.CheckOutMstId);
                cmd.Parameters.AddWithValue("@Days", lockerCheckOutMst.Days);
                cmd.Parameters.AddWithValue("@NoOfLockers", lockerCheckOutMst.NoOfLockers);
                cmd.Parameters.AddWithValue("@Advance", lockerCheckOutMst.Advance);
                cmd.Parameters.AddWithValue("@Rent", lockerCheckOutMst.Rent);
                cmd.Parameters.AddWithValue("@Refund", lockerCheckOutMst.Refund);
                cmd.Parameters.AddWithValue("@ModifiedBy", strUserName);
                cmd.Parameters.AddWithValue("@MachineName", strMachineName);
                cmd.Parameters.AddWithValue("@RecordModifiedCount", lockerCheckOutMst.RecordModifiedCount);
                long result = clsConnection.ExecuteNonQuery(cmd);
                return result;
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exception if necessary
                return -10;
            }
        }

        //public long Delete(LockerCheckOutMst LockerCheckOutMst, Collection coll, string strUserName, string strMacName, ref long lngSerialNo)
        //{
        //    long lngErrNo = 0;
        //    string strType = "";
        //    try
        //    {
        //        clsConnection.glbTransaction = clsConnection.glbCon.BeginTransaction();

        //        SetError("Deleting LOCK_LOCKER_CHECK_OUT_DET_T ");
        //        lngErrNo = DeleteDetail(coll, strUserName, strMacName);
        //        if (lngErrNo < 0 & lngErrNo != -6)
        //        {
        //            clsConnection.glbTransaction.Rollback();
        //            return lngErrNo;
        //        }

        //        SetError("Deleting Into LOCK_LOCKER_CHECK_OUT_MST_T ");
        //        lngErrNo = DeleteMaster(LockerCheckOutMst, strUserName, strMacName);
        //        if (lngErrNo < 0)
        //        {
        //            clsConnection.glbTransaction.Rollback();
        //            return lngErrNo;
        //        }


        //        clsConnection.glbTransaction.Commit();
        //        return lngErrNo;
        //    }
        //    catch (Exception ex)
        //    {
        //        clsConnection.glbTransaction.Rollback();
        //        SetError(ex.ToString());
        //        return -1;
        //    }
        //}
        public long UpdateDays_Rent(object checkInMstId, long countedDays, decimal rent)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_UpdateLockerCheckInMst", clsConnection.GetConnection());
                cmd.Parameters.AddWithValue("@CheckInMstId", checkInMstId);
                cmd.Parameters.AddWithValue("@Days", countedDays);
                cmd.Parameters.AddWithValue("@Rent", rent);
                long result = clsConnection.ExecuteNonQuery(cmd);
                return result;
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exception if necessary
                return -10;
            }
        }

        public DataTable GetDrLockerCheckOutMst(long lngLockerCheckOutMstId = 0, string strDate = "", long lngSerialNo = 0, long lngComId = 0, long lngLocId = 0, long lngDeptId = 0, long lngFYId = 0, string strUserName = "")
        {
            try
            {
                SqlCommand command = new SqlCommand("GetLockerCheckOutMst", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@LockerCheckOutMstId", lngLockerCheckOutMstId);
                command.Parameters.AddWithValue("@Date", string.IsNullOrEmpty(strDate) ? (object)DBNull.Value : DateTime.Parse(strDate));
                command.Parameters.AddWithValue("@SerialNo", lngSerialNo == 0 ? (object)DBNull.Value : (object)lngSerialNo);
                command.Parameters.AddWithValue("@ComId", lngComId == 0 ? (object)DBNull.Value : (object)lngComId);
                command.Parameters.AddWithValue("@LocId", lngLocId == 0 ? (object)DBNull.Value : (object)lngLocId);
                command.Parameters.AddWithValue("@DeptId", lngDeptId == 0 ? (object)DBNull.Value : (object)lngDeptId);
                command.Parameters.AddWithValue("@FYId", lngFYId == 0 ? (object)DBNull.Value : (object)lngFYId);
                command.Parameters.AddWithValue("@UserName", string.IsNullOrEmpty(strUserName) ? (object)DBNull.Value : (object)strUserName);

                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dr;
        }
        public DataTable GetDrLockerCheckOutDet(long lngLockerCheckOutMstId = 0, bool blnAll = false, long lngCtrMachId = 0)
        {
            try
            {
                SqlCommand command = new SqlCommand("GetLockerCheckOutDet", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@LockerCheckOutMstId", lngLockerCheckOutMstId == 0 ? (object)DBNull.Value : (object)lngLockerCheckOutMstId);

                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dr;
        }

        public DataTable GetDrLockerCheckInMst(long lngLockerCheckInMstId = 0, string strDate = "", long lngSerialNo = 0, long lngCtrMachId = 0, long lngComId = 0, long lngLocId = 0, long lngDeptId = 0, long lngFYId = 0, string strUserName = "")
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_GetLockerCheckInMstForOut", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@LockerCheckInMstId", lngLockerCheckInMstId == 0 ? (object)DBNull.Value : (object)lngLockerCheckInMstId);
                command.Parameters.AddWithValue("@Date", string.IsNullOrEmpty(strDate) ? (object)DBNull.Value : DateTime.Parse(strDate));
                command.Parameters.AddWithValue("@SerialNo", lngSerialNo == 0 ? (object)DBNull.Value : (object)lngSerialNo);
                command.Parameters.AddWithValue("@ComId", lngComId == 0 ? (object)DBNull.Value : (object)lngComId);
                command.Parameters.AddWithValue("@LocId", lngLocId == 0 ? (object)DBNull.Value : (object)lngLocId);
                command.Parameters.AddWithValue("@DeptId", lngDeptId == 0 ? (object)DBNull.Value : (object)lngDeptId);
                command.Parameters.AddWithValue("@UserName", string.IsNullOrEmpty(strUserName) ? (object)DBNull.Value : (object)strUserName);

                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dr;
        }

        public DataTable GetDrLockerCheckInDet(long lngLockerCheckInMstId = 0, long lngCtrMachId = 0)
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_GetLockerCheckInDetForDetail", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@LockerCheckInMstId", lngLockerCheckInMstId == 0 ? (object)DBNull.Value : (object)lngLockerCheckInMstId);
                command.Parameters.AddWithValue("@CtrMachId", lngCtrMachId == 0 ? (object)DBNull.Value : (object)lngCtrMachId);

                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dr;
        }


        private void SetError(string str)
        {
            clsConnection.mErrorResult = clsConnection.mErrorResult + Constants.vbNewLine + str;
        }
    }
}
