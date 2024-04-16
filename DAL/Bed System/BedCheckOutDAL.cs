using Microsoft.VisualBasic;
using SGMOSOL.ADMIN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIA;
using static SGMOSOL.BAL.BadBAL;

namespace SGMOSOL.DAL
{
    internal class BedCheckOutDAL
    {
        CommonFunctions cf = new CommonFunctions();

        public DataTable GetMaxSerialNumber(long lngComId = 0, long lngLocId = 0, long lngDeptId = 0, long lngFYId = 0)
        {
            DataTable dr = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_BedGetMaxSerialNumber", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ComId", lngComId);
                command.Parameters.AddWithValue("@LocId", lngLocId);
                command.Parameters.AddWithValue("@DeptId", lngDeptId);
                command.Parameters.AddWithValue("@FYId", lngFYId);

                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dr;
        }
        public DataTable GetDsBedCheckInMstBarcode(long lngLockerCheckInMstId = 0, string strDate = "", string lngSerialNo = "", long lngCtrMachId = 0, long lngComId = 0, long lngLocId = 0, long lngDeptId = 0, long lngFYId = 0, string strUserName = "")
        {
            DataTable dr = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetBedCheckInMstBarcode", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LockerCheckInMstId", lngLockerCheckInMstId);
                command.Parameters.AddWithValue("@Date", strDate);
                command.Parameters.AddWithValue("@SerialNo", lngSerialNo);
                command.Parameters.AddWithValue("@ComId", lngComId);
                command.Parameters.AddWithValue("@LocId", lngLocId);
                command.Parameters.AddWithValue("@FYId", lngFYId);

                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dr;
        }
        public DataTable GetDrPrintRcptDet(long lngPrintRcptMstId = 0, int intUserId = 0)
        {
            DataTable dr = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetBEDPrintRcptDet", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@PrintRcptMstId", lngPrintRcptMstId);
                command.Parameters.AddWithValue("@UserId", intUserId);
                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dr;
        }
        public DataTable GetDrBedCheckOutMst(long lngBedCheckOutMstId = 0, string strDate = "", long lngSerialNo = 0, long lngComId = 0, long lngLocId = 0, long lngDeptId = 0, long lngFYId = 0)
        {
            DataTable dr = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetBedCheckOutMst", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@BedCheckOutMstId", lngBedCheckOutMstId);
                command.Parameters.AddWithValue("@Date", strDate);
                command.Parameters.AddWithValue("@SerialNo", lngSerialNo);
                command.Parameters.AddWithValue("@ComId", lngComId);
                command.Parameters.AddWithValue("@LocId", lngLocId);
                command.Parameters.AddWithValue("@DeptId", lngDeptId);
                command.Parameters.AddWithValue("@FYId", lngFYId);
                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dr;
        }
        public DataTable GetDrPrintRcptDetOut(long lngPrintRcptMstId = 0)
        {
            DataTable dr = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetPrintRcptDetOut", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PrintRcptMstId", lngPrintRcptMstId);
                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dr;
        }
        public long Insert(BedCheckInMst RoomCheckInMst, Collection coll, string strUserName, string strMacName, long lngSerialNo, DateTime EndteredOn)
        {
            DataTable dr;
            long lngMstId = 0;
            long lngErrNo;
            try
            {
                clsConnection.glbTransaction = clsConnection.glbCon.BeginTransaction();

                lngErrNo = InsertMaster(RoomCheckInMst, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }

                dr = GetDrBedCheckInMstId(strMacName);
                if (dr.Rows.Count > 0)
                {
                    lngMstId = Convert.ToInt64(dr.Rows[0]["CheckInMstId"]);
                    lngSerialNo = Convert.ToInt64(dr.Rows[0]["SerialNo"]);
                    EndteredOn = Convert.ToDateTime(dr.Rows[0]["EnteredOn"]);
                }
                //dr.Close();

                lngErrNo = InsertDetail(coll, lngMstId, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }

                clsConnection.glbTransaction.Commit();
                // Insert = lngErrNo
                return lngSerialNo;
            }
            catch (Exception ex)
            {
                clsConnection.glbTransaction.Rollback();
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -1;
            }
        }
        public long InsertMaster(BedCheckInMst RoomCheckInMst, string strUserName, string strMachineName)
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_InsertBedCheckOutMst", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ComId", RoomCheckInMst.ComId);
                command.Parameters.AddWithValue("@LocId", RoomCheckInMst.LocId);
                command.Parameters.AddWithValue("@DeptId", RoomCheckInMst.DeptId);
                command.Parameters.AddWithValue("@CtrMachId", RoomCheckInMst.CtrMachId);
                command.Parameters.AddWithValue("@FyId", RoomCheckInMst.FyId);
                command.Parameters.AddWithValue("@OutDate", RoomCheckInMst.OutDate);
                command.Parameters.AddWithValue("@OutTime", RoomCheckInMst.OutTime);
                command.Parameters.AddWithValue("@CheckInMstId", RoomCheckInMst.CheckInMstId);
                command.Parameters.AddWithValue("@BhaktTypeId", RoomCheckInMst.BhaktTypeId);
                command.Parameters.AddWithValue("@Days", RoomCheckInMst.Days);
                command.Parameters.AddWithValue("@NoOfBeds", RoomCheckInMst.NoOfBeds);
                command.Parameters.AddWithValue("@Advance", RoomCheckInMst.Advance);
                command.Parameters.AddWithValue("@Rent", RoomCheckInMst.Rent);
                command.Parameters.AddWithValue("@Refund", RoomCheckInMst.Refund);
                command.Parameters.AddWithValue("@UserId", RoomCheckInMst.UserId);
                command.Parameters.AddWithValue("@ServerName", RoomCheckInMst.ServerName);
                command.Parameters.AddWithValue("@EnteredBy", strUserName);
                command.Parameters.AddWithValue("@ModifiedBy", strUserName);
                command.Parameters.AddWithValue("@MachineName", strMachineName);

                return clsConnection.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -10; // Error code
            }
        }
        public DataTable GetDrBedCheckInMstId(string strMachineName = null)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetBedCheckInMstId", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                if (!string.IsNullOrEmpty(strMachineName))
                {
                    command.Parameters.AddWithValue("@MachineName", strMachineName);
                }
                else
                {
                    command.Parameters.AddWithValue("@MachineName", DBNull.Value);
                }

                dt = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dt;
        }
        public long InsertDetail(Collection coll, long lngPrintRcptMstId, string strUserName, string strMachineName)
        {
            try
            {
                foreach (BedCheckInDet item in coll)
                {
                    SqlCommand command = new SqlCommand("SP_InsertBedCheckOutDet", clsConnection.GetConnection());
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PrintRcptMstId", lngPrintRcptMstId);
                    command.Parameters.AddWithValue("@ProdId", item.ProdId);
                    command.Parameters.AddWithValue("@Rent", item.Rent);
                    command.Parameters.AddWithValue("@Qty", item.Qty);
                    command.Parameters.AddWithValue("@Advance", item.Advance);
                    command.Parameters.AddWithValue("@TotalRent", item.TotalRent);
                    command.Parameters.AddWithValue("@TotalAdv", item.TotalAdv);

                    long A = clsConnection.ExecuteNonQuery(command);
                    if (A > 0)
                    {
                        return A;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -1;
            }
        }
        public long Update(BedCheckInMst RoomCheckInMst, Collection coll, Collection DelColl, string strUserName, string strMacName, long lngSerialNo)
        {
            Collection actualDelColl = new Collection();
            Collection actualInsColl = new Collection();

            Int16 ctr;
            Int16 ctr1;
            BedCheckInDet DelCheckInDet = new BedCheckInDet();
            BedCheckInDet InsCheckInDet = new BedCheckInDet();
            bool blnFound = false;

            // -----Making Delete Collection
            for (ctr = 1; ctr <= DelColl.Count; ctr++)
            {
                DelCheckInDet = (BedCheckInDet)DelColl[ctr];
                blnFound = false;
                for (ctr1 = 1; ctr1 <= coll.Count; ctr1++)
                {
                    InsCheckInDet = (BedCheckInDet)coll[ctr1];
                    if (DelCheckInDet.ProdId == InsCheckInDet.ProdId)
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
                InsCheckInDet = (BedCheckInDet)coll[ctr];
                blnFound = false;
                for (ctr1 = 1; ctr1 <= DelColl.Count; ctr1++)
                {
                    DelCheckInDet = (BedCheckInDet)DelColl[ctr1];
                    if (DelCheckInDet.ProdId == InsCheckInDet.ProdId)
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
                    lngErrNo = Delete(actualDelColl, strUserName, strMacName);
                    if (lngErrNo < 0 & lngErrNo != -6)
                    {
                        clsConnection.glbTransaction.Rollback();
                        return lngErrNo;
                    }
                }

                lngErrNo = InsertDetail(actualInsColl, RoomCheckInMst.CheckInMstId, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }

                lngErrNo = UpdateChangemst(RoomCheckInMst, strUserName, strMacName);
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
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -1;
            }
        }
        public long Delete(Collection coll, string strUserName, string strMachineName)
        {
            try
            {
                foreach (BedCheckInDet item in coll)
                {
                    SqlCommand command = new SqlCommand("SP_DeleteBedCheckOutDet", clsConnection.GetConnection());
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CheckOutDetId", item.CheckInDetId);
                    long A = clsConnection.ExecuteNonQuery(command);
                    if (A > 0)
                    {
                        return A;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -1;
            }
        }
        public long UpdateChangemst(BedCheckInMst roomCheckInMst, string strUserName, string strMachineName)
        {
            long lngErrNum = 0;
            try
            {
                SqlCommand command = new SqlCommand("SP_UpdateBedCheckInMst", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CheckInMstId", roomCheckInMst.CheckInMstId);
                command.Parameters.AddWithValue("@Name", roomCheckInMst.Name);
                command.Parameters.AddWithValue("@Place", roomCheckInMst.Place);
                command.Parameters.AddWithValue("@Days", roomCheckInMst.Days);
                command.Parameters.AddWithValue("@NoOfPersons", roomCheckInMst.NoOfPersons);
                command.Parameters.AddWithValue("@NoOfBeds", roomCheckInMst.NoOfBeds);
                command.Parameters.AddWithValue("@OutDate", roomCheckInMst.OutDate);
                command.Parameters.AddWithValue("@OutTime", roomCheckInMst.OutTime);
                command.Parameters.AddWithValue("@Advance", roomCheckInMst.Advance);
                command.Parameters.AddWithValue("@Rent", roomCheckInMst.Rent);
                command.Parameters.AddWithValue("@ModifiedBy", strUserName);
                command.Parameters.AddWithValue("@MachineName", strMachineName);
                command.Parameters.AddWithValue("@MobNo", roomCheckInMst.mob_no);
                command.Parameters.AddWithValue("@RecordModifiedCount", roomCheckInMst.RecordModifiedCount);

                return clsConnection.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                lngErrNum = -10;
            }
            return lngErrNum;
        }
        public DataTable GetDrOcc(int intUserid = 0)
        {
            DataTable dr = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetOccupancyData", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", intUserid);

                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dr;
        }

        public long UpdateOutofOrder(int OutOFOrderQuantity, int ID)
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_UpdateOutofOrder", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@OutOFOrderQuantity", OutOFOrderQuantity);
                command.Parameters.AddWithValue("@ID", ID);
                return clsConnection.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -1;
            }
        }

    }
}
