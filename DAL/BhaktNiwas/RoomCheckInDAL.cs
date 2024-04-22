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
    internal class RoomCheckInDAL
    {
        CommonFunctions cf = new CommonFunctions();
        System.Data.DataTable Dr = new System.Data.DataTable();
        public System.Data.DataTable GetMaxSerialNo(int lngComId = 0, int lngLocId = 0, int lngDeptId = 0, int lngFYId = 0)
        {
            using (SqlCommand command = new SqlCommand("SP_GetRoomCheckInMaxSerialNo", clsConnection.GetConnection()))
            {
                try
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@lngComId", lngComId);
                    command.Parameters.AddWithValue("@lngLocId", lngLocId);
                    command.Parameters.AddWithValue("@lngDeptId", lngDeptId);
                    command.Parameters.AddWithValue("@lngFYId", lngFYId);
                    Dr = clsConnection.ExecuteReader(command);
                }
                catch (Exception ex)
                {
                    cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                }
                return Dr;
            }
        }
        public System.Data.DataTable GetLastEnterdName(int intCtrId)
        {
            using (SqlCommand command = new SqlCommand("SP_GetLastEnteredName", clsConnection.GetConnection()))
            {
                try
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@intCtrId", intCtrId);

                    Dr = clsConnection.ExecuteReader(command);
                }
                catch (Exception ex)
                {
                    cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                }
                return Dr;
            }
        }
        public System.Data.DataTable GetDaysLimit()
        {
            using (SqlCommand command = new SqlCommand("SP_GetRoomDaysLimit", clsConnection.GetConnection()))
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
        public System.Data.DataTable GetRoomLimit()
        {
            using (SqlCommand command = new SqlCommand("SP_GetRoomLimit", clsConnection.GetConnection()))
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
        public System.Data.DataTable GetDrRoomCheckInMst(Int64 lngRoomCheckInMstId = 0, string strDate = "", int lngSerialNo = 0, int lngCtrMachId = 0, int lngComId = 0, int lngLocId = 0, int lngDeptId = 0, int lngFYId = 0, string strUserName = "")
        {
            using (SqlCommand command = new SqlCommand("SP_GetDrRoomCheckInMst", clsConnection.GetConnection()))
            {
                try
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@lngRoomCheckInMstId", lngRoomCheckInMstId);
                    command.Parameters.AddWithValue("@strDate", strDate);
                    command.Parameters.AddWithValue("@lngSerialNo", lngSerialNo);
                    command.Parameters.AddWithValue("@lngCtrMachId", lngCtrMachId);
                    command.Parameters.AddWithValue("@lngComId", lngComId);
                    command.Parameters.AddWithValue("@lngLocId", lngLocId);
                    command.Parameters.AddWithValue("@lngDeptId", lngDeptId);
                    command.Parameters.AddWithValue("@lngFYId", lngFYId);
                    command.Parameters.AddWithValue("@strUserName", strUserName);
                    Dr = clsConnection.ExecuteReader(command);
                }
                catch (Exception ex)
                {
                    cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                }
                return Dr;
            }
        }
        public System.Data.DataTable GetDrRoomCheckInDet(long lngRoomCheckInMstId = 0, bool blnAll = false, long lngDeptId = 0)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetDrRoomCheckInDet", clsConnection.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@lngRoomCheckInMstId", lngRoomCheckInMstId);
                    cmd.Parameters.AddWithValue("@blnAll", blnAll);
                    cmd.Parameters.AddWithValue("@lngDeptId", lngDeptId);

                    Dr = clsConnection.ExecuteReader(cmd);
                }
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return Dr;
        }
        public long Insert(RoomCheckInMst RoomCheckInMst, Collection coll, string strUserName, string strMacName, long lngSerialNo, DateTime EndteredOn)
        {
            long lngMstId = 0;
            long lngErrNo;
            try
            {
                clsConnection.glbTransaction = clsConnection.glbCon.BeginTransaction();

                //SetError("Inserting Into BN_ROOM_CHECK_IN_MST_T ");
                lngErrNo = InsertMaster(RoomCheckInMst, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }

                Dr = GetDrRoomCheckInMstId(strMacName);
                if (Dr.Rows.Count > 0)
                {
                    lngMstId = Convert.ToInt64(Dr.Rows[0]["CheckInMstId"]);
                    lngSerialNo = Convert.ToInt64(Dr.Rows[0]["SerialNo"]);
                    EndteredOn = Convert.ToDateTime(Dr.Rows[0]["EnteredOn"]);
                }

                //SetError("Inserting Into BN_ROOM_CHECK_IN_MST_T_HISTORY");
                lngErrNo = InsertIntoHistoryMaster(RoomCheckInMst, strUserName, lngMstId, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }



                //SetError("Inserting Into BN_ROOM_CHECK_IN_DET_T ");
                lngErrNo = InsertDetails(coll, lngMstId, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }

                //SetError("Inserting Into BN_ROOM_CHECK_IN_DET_T_HISTORY ");
                lngErrNo = InsertIntoHistoryDetail(coll, lngMstId, strUserName, strMacName);
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
        public long InsertMaster(RoomCheckInMst roomCheckInMst, string strUserName, string strMachineName)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SP_InsertRoomCheckInMst", clsConnection.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ComId", roomCheckInMst.ComId);
                    cmd.Parameters.AddWithValue("@LocId", roomCheckInMst.LocId);
                    cmd.Parameters.AddWithValue("@DeptId", roomCheckInMst.DeptId);
                    cmd.Parameters.AddWithValue("@CtrMachId", roomCheckInMst.CtrMachId);
                    cmd.Parameters.AddWithValue("@SublocId", roomCheckInMst.Sublocid);
                    cmd.Parameters.AddWithValue("@Sublocation", roomCheckInMst.sublocn);
                    cmd.Parameters.AddWithValue("@DonnerRoomId", roomCheckInMst.DonnerRoomId);
                    cmd.Parameters.AddWithValue("@FyId", roomCheckInMst.FyId);
                    cmd.Parameters.AddWithValue("@InDate", roomCheckInMst.InDate);
                    cmd.Parameters.AddWithValue("@InTime", roomCheckInMst.InTime);
                    cmd.Parameters.AddWithValue("@AppNo", roomCheckInMst.AppNo);
                    cmd.Parameters.AddWithValue("@Name", roomCheckInMst.Name);
                    cmd.Parameters.AddWithValue("@Image", roomCheckInMst.Image);
                    cmd.Parameters.AddWithValue("@Place", roomCheckInMst.Place);
                    cmd.Parameters.AddWithValue("@VehicleNo", roomCheckInMst.VehicleNo);
                    cmd.Parameters.AddWithValue("@Days", roomCheckInMst.Days);
                    cmd.Parameters.AddWithValue("@NoOfRooms", roomCheckInMst.NoOfRooms);
                    cmd.Parameters.AddWithValue("@NoOfPersons", roomCheckInMst.NoOfPersons);
                    cmd.Parameters.AddWithValue("@OutDate", roomCheckInMst.OutDate);
                    cmd.Parameters.AddWithValue("@OutTime", roomCheckInMst.OutTime);
                    cmd.Parameters.AddWithValue("@Advance", roomCheckInMst.Advance);
                    cmd.Parameters.AddWithValue("@Rent", roomCheckInMst.Rent);
                    cmd.Parameters.AddWithValue("@UserId", roomCheckInMst.UserId);
                    cmd.Parameters.AddWithValue("@DonerId", roomCheckInMst.donerId);
                    cmd.Parameters.AddWithValue("@BhaktTypeId", roomCheckInMst.BhaktTypeId);
                    cmd.Parameters.AddWithValue("@AuthPersonId", roomCheckInMst.AuthPersonId);
                    cmd.Parameters.AddWithValue("@ServerName", roomCheckInMst.ServerName);
                    cmd.Parameters.AddWithValue("@EnteredBy", roomCheckInMst.EnteredBy);
                    cmd.Parameters.AddWithValue("@ModifiedBy", roomCheckInMst.ModifiedBy);
                    cmd.Parameters.AddWithValue("@MachineName", roomCheckInMst.MachineName);
                    cmd.Parameters.AddWithValue("@Remark", roomCheckInMst.Remark);
                    cmd.Parameters.AddWithValue("@MobNo", roomCheckInMst.mob_no);
                    cmd.Parameters.AddWithValue("@ScanImageName", roomCheckInMst.ScanDoc);
                    cmd.Parameters.AddWithValue("@IsOnline", roomCheckInMst.isOnline);
                    cmd.Parameters.AddWithValue("@PaymentType", roomCheckInMst.paymenttype);
                    cmd.Parameters.AddWithValue("@Tid", roomCheckInMst.tid);
                    cmd.Parameters.AddWithValue("@Invoice", roomCheckInMst.invoiceno);
                    cmd.Parameters.AddWithValue("@Barcode", roomCheckInMst.Barcode);
                    cmd.Parameters.AddWithValue("@BookingID", roomCheckInMst.BookingID);
                    cmd.Parameters.AddWithValue("@CountryID", roomCheckInMst.CountryId);
                    cmd.Parameters.AddWithValue("@CountryName", roomCheckInMst.Countryname);
                    cmd.Parameters.AddWithValue("@StateID", roomCheckInMst.Stateid);
                    cmd.Parameters.AddWithValue("@State", roomCheckInMst.statename);
                    cmd.Parameters.AddWithValue("@DistrictID", roomCheckInMst.Districtid);
                    cmd.Parameters.AddWithValue("@District", roomCheckInMst.DISTRICT);
                    cmd.Parameters.AddWithValue("@CHQBankName", roomCheckInMst.CHQ_BANK_NAME);
                    cmd.Parameters.AddWithValue("@CHQNo", roomCheckInMst.CHQ_NO);
                    cmd.Parameters.AddWithValue("@CHQDate", roomCheckInMst.CHQ_DATE);
                    cmd.Parameters.AddWithValue("@KYCDocType", roomCheckInMst.DOC_TYPE);
                    cmd.Parameters.AddWithValue("@KYCDocDetail", roomCheckInMst.DOC_DETAILS);

                    return clsConnection.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -10;
            }
        }
        public System.Data.DataTable GetDrRoomCheckInMstId(string strMachineName = "")
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetRoomCheckInMstId", clsConnection.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MachineName", strMachineName);

                    Dr = clsConnection.ExecuteReader(cmd);
                }
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return Dr;
        }
        public long InsertIntoHistoryMaster(RoomCheckInMst roomCheckInMst, string strUserName, long lngMstId, string strMachineName)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SP_InsertRoomCheckInMstHistory", clsConnection.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ComId", roomCheckInMst.ComId);
                    cmd.Parameters.AddWithValue("@LocId", roomCheckInMst.LocId);
                    cmd.Parameters.AddWithValue("@DeptId", roomCheckInMst.DeptId);
                    cmd.Parameters.AddWithValue("@CtrMachId", roomCheckInMst.CtrMachId);
                    cmd.Parameters.AddWithValue("@SublocId", roomCheckInMst.Sublocid);
                    cmd.Parameters.AddWithValue("@Sublocation", roomCheckInMst.sublocn);
                    cmd.Parameters.AddWithValue("@DonnerRoomId", roomCheckInMst.DonnerRoomId);
                    cmd.Parameters.AddWithValue("@FyId", roomCheckInMst.FyId);
                    cmd.Parameters.AddWithValue("@InDate", roomCheckInMst.InDate);
                    cmd.Parameters.AddWithValue("@InTime", roomCheckInMst.InTime);
                    cmd.Parameters.AddWithValue("@AppNo", roomCheckInMst.AppNo);
                    cmd.Parameters.AddWithValue("@Name", roomCheckInMst.Name);
                    cmd.Parameters.AddWithValue("@Image", roomCheckInMst.Image);
                    cmd.Parameters.AddWithValue("@Place", roomCheckInMst.Place);
                    cmd.Parameters.AddWithValue("@VehicleNo", roomCheckInMst.VehicleNo);
                    cmd.Parameters.AddWithValue("@Days", roomCheckInMst.Days);
                    cmd.Parameters.AddWithValue("@NoOfRooms", roomCheckInMst.NoOfRooms);
                    cmd.Parameters.AddWithValue("@NoOfPersons", roomCheckInMst.NoOfPersons);
                    cmd.Parameters.AddWithValue("@OutDate", roomCheckInMst.OutDate);
                    cmd.Parameters.AddWithValue("@OutTime", roomCheckInMst.OutTime);
                    cmd.Parameters.AddWithValue("@Advance", roomCheckInMst.Advance);
                    cmd.Parameters.AddWithValue("@Rent", roomCheckInMst.Rent);
                    cmd.Parameters.AddWithValue("@UserId", roomCheckInMst.UserId);
                    cmd.Parameters.AddWithValue("@DonerId", roomCheckInMst.donerId);
                    cmd.Parameters.AddWithValue("@BhaktTypeId", roomCheckInMst.BhaktTypeId);
                    cmd.Parameters.AddWithValue("@AuthPersonId", roomCheckInMst.AuthPersonId);
                    cmd.Parameters.AddWithValue("@ServerName", roomCheckInMst.ServerName);
                    cmd.Parameters.AddWithValue("@EnteredBy", roomCheckInMst.EnteredBy);
                    cmd.Parameters.AddWithValue("@ModifiedBy", roomCheckInMst.ModifiedBy);
                    cmd.Parameters.AddWithValue("@MachineName", roomCheckInMst.MachineName);
                    cmd.Parameters.AddWithValue("@Remark", roomCheckInMst.Remark);
                    cmd.Parameters.AddWithValue("@MobNo", roomCheckInMst.mob_no);
                    cmd.Parameters.AddWithValue("@ScanImageName", roomCheckInMst.ScanDoc);
                    cmd.Parameters.AddWithValue("@IsOnline", roomCheckInMst.isOnline);
                    cmd.Parameters.AddWithValue("@PaymentType", roomCheckInMst.paymenttype);
                    cmd.Parameters.AddWithValue("@Tid", roomCheckInMst.tid);
                    cmd.Parameters.AddWithValue("@Invoice", roomCheckInMst.invoiceno);
                    cmd.Parameters.AddWithValue("@Barcode", roomCheckInMst.Barcode);
                    cmd.Parameters.AddWithValue("@BookingID", roomCheckInMst.BookingID);
                    cmd.Parameters.AddWithValue("@CountryId", roomCheckInMst.CountryId);
                    cmd.Parameters.AddWithValue("@CountryName", roomCheckInMst.Countryname);
                    cmd.Parameters.AddWithValue("@StateId", roomCheckInMst.Stateid);
                    cmd.Parameters.AddWithValue("@State", roomCheckInMst.statename);
                    cmd.Parameters.AddWithValue("@DistrictId", roomCheckInMst.Districtid);
                    cmd.Parameters.AddWithValue("@District", roomCheckInMst.DISTRICT);
                    cmd.Parameters.AddWithValue("@ChqBankName", roomCheckInMst.CHQ_BANK_NAME);
                    cmd.Parameters.AddWithValue("@ChqNo", roomCheckInMst.CHQ_NO);
                    cmd.Parameters.AddWithValue("@IsDelete", 0);
                    cmd.Parameters.AddWithValue("@CheckInMstId", roomCheckInMst.CheckInMstId);
                    cmd.Parameters.AddWithValue("@ChqDate", roomCheckInMst.CHQ_DATE);

                    return clsConnection.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -10;
            }
        }
        public long InsertDetails(Collection coll, long lngRoomCheckInMstId, string strUserName, string strMachineName)
        {
            try
            {
                foreach (RoomCheckInDet item in coll)
                {
                    using (SqlCommand cmd = new SqlCommand("SP_InsertRoomCheckInDet", clsConnection.GetConnection()))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@RoomCheckInMstId", lngRoomCheckInMstId);
                        cmd.Parameters.AddWithValue("@LockerId", item.LockerId);

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
        public long InsertIntoHistoryDetail(Collection coll, long lngRoomCheckInMstId, string strUserName, string strMachineName)
        {
            try
            {
                foreach (RoomCheckInDet item in coll)
                {
                    using (SqlCommand cmd = new SqlCommand("SP_InsertRoomCheckInDetHistory", clsConnection.GetConnection()))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@RoomCheckInMstId", lngRoomCheckInMstId);
                        cmd.Parameters.AddWithValue("@LockerId", item.LockerId);
                        cmd.Parameters.AddWithValue("@EnteredBy", strUserName);
                        cmd.Parameters.AddWithValue("@MachineName", strMachineName);
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
        public long UpdateStatus(Collection Coll, string strUserName, string strMachineName)
        {
            try
            {
                foreach (RoomCheckInDet item in Coll)
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
        public long Update(RoomCheckInMst RoomCheckInMst, Collection coll, Collection DelColl, string strUserName, string strMacName, long lngSerialNo)
        {
            Collection actualDelColl = new Collection();
            Collection actualInsColl = new Collection();

            Int16 ctr;
            Int16 ctr1;
            RoomCheckInDet DelCheckInDet = new RoomCheckInDet();
            RoomCheckInDet InsCheckInDet = new RoomCheckInDet();
            bool blnFound = false;

            // -----Making Delete Collection
            for (ctr = 1; ctr <= DelColl.Count; ctr++)
            {
                DelCheckInDet = (RoomCheckInDet)DelColl[ctr];
                blnFound = false;
                for (ctr1 = 1; ctr1 <= coll.Count; ctr1++)
                {
                    InsCheckInDet = (RoomCheckInDet)coll[ctr1];
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
                InsCheckInDet = (RoomCheckInDet)coll[ctr];
                blnFound = false;
                for (ctr1 = 1; ctr1 <= DelColl.Count; ctr1++)
                {
                    DelCheckInDet = (RoomCheckInDet)DelColl[ctr1];
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
                    //SetError("Deleting from BN_ROOM_CHECK_IN_DET_T ");
                    lngErrNo = Delete(actualDelColl, strUserName, strMacName);
                    if (lngErrNo < 0 & lngErrNo != -6)
                    {
                        clsConnection.glbTransaction.Rollback();
                        return lngErrNo;
                    }
                }

                //SetError("Inserting Into BN_ROOM_CHECK_IN_DET_T ");
                lngErrNo = InsertDetails(actualInsColl, RoomCheckInMst.CheckInMstId, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }

                //SetError("Updating Into BN_ROOM_CHECK_IN_MST_T ");
                lngErrNo = UpdateChangemst(RoomCheckInMst, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }


                //SetError("Updating Room Status for Delete Coll");
                lngErrNo = UpdateStatus(actualDelColl, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }

                //SetError("Updating Room Status for Insert Coll");
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
                clsConnection.glbTransaction.Rollback();
                return -1;
            }
        }
        public long Delete(Collection coll, string strUserName, string strMachineName)
        {
            try
            {
                foreach (RoomCheckInDet item in coll)
                {
                    using (SqlCommand cmd = new SqlCommand("SP_DeleteRoomCheckInDet", clsConnection.GetConnection()))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CheckInMstId", item.CheckInMstId);
                        cmd.Parameters.AddWithValue("@LockerId", item.LockerId);
                        long A = clsConnection.ExecuteNonQuery(cmd);
                        if (A > 0)
                        {
                            return A;
                        }
                    }
                }
                return 0;//Success
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -1; // Error
            }
        }
        public long UpdateChangemst(RoomCheckInMst roomCheckInMst, string strUserName, string strMachineName)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SP_UpdateRoomCheckInMst", clsConnection.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CheckInMstId", roomCheckInMst.CheckInMstId);
                    cmd.Parameters.AddWithValue("@Name", roomCheckInMst.Name);
                    cmd.Parameters.AddWithValue("@BhaktTypeId", roomCheckInMst.BhaktTypeId);
                    cmd.Parameters.AddWithValue("@Sublocid", roomCheckInMst.Sublocid);
                    cmd.Parameters.AddWithValue("@Sublocn", roomCheckInMst.sublocn);
                    cmd.Parameters.AddWithValue("@Days", roomCheckInMst.Days);
                    cmd.Parameters.AddWithValue("@NoOfRooms", roomCheckInMst.NoOfRooms);
                    cmd.Parameters.AddWithValue("@OutDate", roomCheckInMst.OutDate);
                    cmd.Parameters.AddWithValue("@OutTime", roomCheckInMst.OutTime);
                    cmd.Parameters.AddWithValue("@Advance", roomCheckInMst.Advance);
                    cmd.Parameters.AddWithValue("@Rent", roomCheckInMst.Rent);
                    cmd.Parameters.AddWithValue("@ScanDoc", roomCheckInMst.ScanDoc);
                    cmd.Parameters.AddWithValue("@ModifiedBy", strUserName);
                    cmd.Parameters.AddWithValue("@MachineName", strMachineName);
                    cmd.Parameters.AddWithValue("@PaymentType", roomCheckInMst.paymenttype);
                    cmd.Parameters.AddWithValue("@Tid", roomCheckInMst.tid);
                    cmd.Parameters.AddWithValue("@CountryId", roomCheckInMst.CountryId);
                    cmd.Parameters.AddWithValue("@Countryname", roomCheckInMst.Countryname);
                    cmd.Parameters.AddWithValue("@StateId", roomCheckInMst.Stateid);
                    cmd.Parameters.AddWithValue("@Statename", roomCheckInMst.statename);
                    cmd.Parameters.AddWithValue("@DistrictId", roomCheckInMst.Districtid);
                    cmd.Parameters.AddWithValue("@District", roomCheckInMst.DISTRICT);
                    cmd.Parameters.AddWithValue("@ChqBankName", roomCheckInMst.CHQ_BANK_NAME);
                    cmd.Parameters.AddWithValue("@ChqNo", roomCheckInMst.CHQ_NO);
                    cmd.Parameters.AddWithValue("@ChqDate", roomCheckInMst.CHQ_DATE);
                    cmd.Parameters.AddWithValue("@MobNo", roomCheckInMst.mob_no);
                    cmd.Parameters.AddWithValue("@RecordModifiedCount", roomCheckInMst.RecordModifiedCount);
                    cmd.Parameters.AddWithValue("@IsOnline", roomCheckInMst.isOnline);

                    return clsConnection.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -1; // Error
            }
        }
        public long InsertRePrint(object CheckinID, string UserName, string MacineName, int userid)
        {

            // aaa
            long lngMstId;
            long lngErrNo;
            try
            {
                clsConnection.glbTransaction = clsConnection.glbCon.BeginTransaction();
                //SetError("Inserting Into  ");
                lngErrNo = InsertReprint(CheckinID, UserName, MacineName, userid);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }
                else
                {
                    clsConnection.glbTransaction.Commit();
                    return lngErrNo;
                }
            }
            catch (Exception ex)
            {
                clsConnection.glbTransaction.Rollback();
                //SetError(ex.ToString());
                return -1;
            }
        }
        public long InsertReprint(object checkInReceiptId, string userName, string machineName, int userId)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SP_InsertReprintCheckInReceipt", clsConnection.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CheckInReceiptId", checkInReceiptId);
                    cmd.Parameters.AddWithValue("@UserName", userName);
                    cmd.Parameters.AddWithValue("@MachineName", machineName);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    return clsConnection.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -1; // Error
            }
        }
        public long UpdateChange(RoomCheckInMst RoomCheckInMst, Collection coll, Collection DelColl, string strUserName, string strMacName, long lngSerialNo)
        {
            Collection actualDelColl = new Collection();
            Collection actualInsColl = new Collection();

            Int16 ctr;
            Int16 ctr1;
            RoomCheckInDet DelCheckInDet = new RoomCheckInDet();
            RoomCheckInDet InsCheckInDet = new RoomCheckInDet();
            bool blnFound = false;

            // -----Making Delete Collection
            for (ctr = 1; ctr <= DelColl.Count; ctr++)
            {
                DelCheckInDet = (RoomCheckInDet)DelColl[ctr];
                blnFound = false;
                for (ctr1 = 1; ctr1 <= coll.Count; ctr1++)
                {
                    InsCheckInDet = (RoomCheckInDet)coll[ctr1];
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
                InsCheckInDet = (RoomCheckInDet)coll[ctr];
                blnFound = false;
                for (ctr1 = 1; ctr1 <= DelColl.Count; ctr1++)
                {
                    DelCheckInDet = (RoomCheckInDet)DelColl[ctr1];
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
                    //SetError("Deleting from BN_ROOM_CHECK_IN_DET_T ");
                    lngErrNo = Delete(actualDelColl, strUserName, strMacName);
                    if (lngErrNo < 0 & lngErrNo != -6)
                    {
                        clsConnection.glbTransaction.Rollback();
                        return lngErrNo;
                    }
                }

                if (DelColl.Count > 0)
                {
                    //SetError("Update BN_ROOM_CHECK_IN_DET_T_HISTORY");
                    lngErrNo = UpdateRoomCheckInDetHistory(actualDelColl, strUserName, strMacName);
                    if (lngErrNo < 0 & lngErrNo != -6)
                    {
                        clsConnection.glbTransaction.Rollback();
                        return lngErrNo;
                    }
                }

                //SetError("Inserting Into BN_ROOM_CHECK_IN_DET_T ");
                lngErrNo = InsertDetails(actualInsColl, RoomCheckInMst.CheckInMstId, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }
                //SetError("Inserting Into BN_ROOM_CHECK_IN_DET_T_HISTORY ");
                lngErrNo = InsertIntoHistory(actualInsColl, RoomCheckInMst.CheckInMstId, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }

                //SetError("Updating Into BN_ROOM_CHECK_IN_MST_T ");
                lngErrNo = UpdateChangeRoomChanges(RoomCheckInMst, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }


                //SetError("Updating Room Status for Delete Coll");
                lngErrNo = UpdateStatus(actualDelColl, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }

                //SetError("Updating Room Status for Insert Coll");
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
                //MsgBox(ex.ToString());
                clsConnection.glbTransaction.Rollback();
                //SetError(ex.ToString());
                return -1;
            }
        }
        public long UpdateRoomCheckInDetHistory(Collection coll, string strUserName, string strMachineName)
        {
            foreach (RoomCheckInDet item in coll)
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateRoomCheckInDetHistory", clsConnection.GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CheckInMstIds", item.CheckInMstId);
                    command.Parameters.AddWithValue("@LockerIds", item.LockerId);
                    command.Parameters.AddWithValue("@UserName", strUserName);
                    command.Parameters.AddWithValue("@MachineName", strMachineName);

                    try
                    {
                        long A = clsConnection.ExecuteNonQuery(command);
                        if (A > 0)
                        {
                            return A;
                        }
                    }
                    catch (Exception ex)
                    {
                        return -90;
                    }
                }
            }
            return 0;
        }
        public long InsertIntoHistory(Collection coll, long lngRoomCheckInMstId, string strUserName, string strMachineName)
        {
            foreach (RoomCheckInDet item in coll)
            {
                using (SqlCommand command = new SqlCommand("SP_InsertIntoRoomHistory", clsConnection.GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RoomCheckInMstId", lngRoomCheckInMstId);
                    command.Parameters.AddWithValue("@LockerId", item.LockerId);
                    command.Parameters.AddWithValue("@UserName", strUserName);
                    command.Parameters.AddWithValue("@MachineName", strMachineName);

                    try
                    {
                        long A = clsConnection.ExecuteNonQuery(command);
                        if (A > 0)
                        {
                            return A;
                        }
                    }
                    catch (Exception ex)
                    {
                        return -1;
                    }
                }
            }
            return 0;
        }
        public long UpdateChangeRoomChanges(RoomCheckInMst roomCheckInMst, string strUserName, string strMachineName)
        {
            using (SqlCommand command = new SqlCommand("SP_UpdateChangeRoomChanges", clsConnection.GetConnection()))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CheckInMstId", roomCheckInMst.CheckInMstId);
                command.Parameters.AddWithValue("@Name", roomCheckInMst.Name);
                command.Parameters.AddWithValue("@Sublocid", roomCheckInMst.Sublocid);
                command.Parameters.AddWithValue("@Sublocation", roomCheckInMst.sublocn);
                command.Parameters.AddWithValue("@Days", roomCheckInMst.Days);
                command.Parameters.AddWithValue("@NoOfRooms", roomCheckInMst.NoOfRooms);
                command.Parameters.AddWithValue("@OutDate", roomCheckInMst.OutDate);
                command.Parameters.AddWithValue("@OutTime", roomCheckInMst.OutTime);
                command.Parameters.AddWithValue("@Advance", roomCheckInMst.Advance);
                command.Parameters.AddWithValue("@Rent", roomCheckInMst.Rent);
                command.Parameters.AddWithValue("@ScanDoc", roomCheckInMst.ScanDoc);
                command.Parameters.AddWithValue("@ModifiedBy", strUserName);
                command.Parameters.AddWithValue("@MachineName", strMachineName);
                command.Parameters.AddWithValue("@MobNo", roomCheckInMst.mob_no);
                command.Parameters.AddWithValue("@RecordModifiedCount", roomCheckInMst.RecordModifiedCount);

                try
                {
                    return clsConnection.ExecuteNonQuery(command);
                }
                catch (Exception ex)
                {
                    return -10;
                }
            }
        }
        public long Insert1(RoomChangeMst RoomChangeMst, Collection coll, string strUserName, string strMacName, long lngSerialNo, DateTime EndteredOn)
        {
            long lngErrNo = 0;
            try
            {
                clsConnection.glbTransaction = clsConnection.glbCon.BeginTransaction();
                for (var ctr = 1; ctr <= coll.Count; ctr++)
                {
                    RoomChangeMst.PrevLkrId = ((RoomCheckInDet)coll[ctr]).LockerId;
                    //SetError("Inserting Into BN_ROOM_CHANGE_T ");
                    lngErrNo = InsertRoomChange(RoomChangeMst, strUserName, strMacName);
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
                //SetError(ex.ToString());
                return -1;
            }
        }
        public long InsertRoomChange(RoomChangeMst roomChangeMst, string strUserName, string strMachineName)
        {
            using (SqlCommand command = new SqlCommand("SP_InsertRoomChange", clsConnection.GetConnection()))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CheckInMstId", roomChangeMst.CheckInMstId);
                command.Parameters.AddWithValue("@PrevRoomId", roomChangeMst.PrevLkrId);
                command.Parameters.AddWithValue("@Reason", roomChangeMst.Reason);
                command.Parameters.AddWithValue("@UserId", roomChangeMst.UserId);
                command.Parameters.AddWithValue("@ServerName", roomChangeMst.ServerName);
                command.Parameters.AddWithValue("@EnteredBy", strUserName);
                command.Parameters.AddWithValue("@ModifiedBy", strUserName);
                command.Parameters.AddWithValue("@MachineName", strMachineName);

                try
                {
                    return clsConnection.ExecuteNonQuery(command);
                }
                catch (Exception ex)
                {
                    return -10;
                }
            }
        }
        public long InsertRoomExtend(RoomCheckInMst RoomChangeMst, string strUserName, string strMacName)
        {
            long lngMstId;
            long lngErrNo;
            try
            {
                clsConnection.glbTransaction = clsConnection.glbCon.BeginTransaction();


                //SetError("Inserting Into BN_ROOM_EXTEND_T ");
                lngErrNo = InserRoomExtend(RoomChangeMst, strUserName, strMacName);
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
                //SetError(ex.ToString());
                return -1;
            }
        }
        public long InserRoomExtend(RoomCheckInMst roomCheckInMst, string strUserName,string strMacName)
        {
            using (SqlCommand command = new SqlCommand("SP_InsertRoomsExtend", clsConnection.GetConnection()))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@LocId", roomCheckInMst.LocId);
                command.Parameters.AddWithValue("@DeptId", roomCheckInMst.DeptId);
                command.Parameters.AddWithValue("@CtrMachId", roomCheckInMst.CtrMachId);
                command.Parameters.AddWithValue("@CheckInMstId", roomCheckInMst.CheckInMstId);
                command.Parameters.AddWithValue("@ExtendDate", roomCheckInMst.ExtDate);
                command.Parameters.AddWithValue("@Rent", roomCheckInMst.ExtRent);
                command.Parameters.AddWithValue("@Days", roomCheckInMst.ExtDay);
                command.Parameters.AddWithValue("@EnteredBy", strUserName);
                command.Parameters.AddWithValue("@ModifiedBy", strUserName);

                try
                {
                    return clsConnection.ExecuteNonQuery(command);
                }
                catch (Exception ex)
                {
                    return -10;
                }
            }
        }
        public long DeleteRoomLocked(string StrID)
        {
            long lngMstId;
            long lngErrNo;
            try
            {
                clsConnection.glbTransaction = clsConnection.glbCon.BeginTransaction();


                //SetError("Deleting Into BN_ROOM_CHANGE_T ");
                lngErrNo = deleteRoomLocked(StrID);
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
                //SetError(ex.ToString());
                return -1;
            }
        }
        public long deleteRoomLocked(string id)
        {
            try
            {
                    SqlCommand command = new SqlCommand("SP_DeleteRoomLocked", clsConnection.GetConnection());
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@BookingId", id);

                    return clsConnection.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                return -10;
            }
        }
    }
}
