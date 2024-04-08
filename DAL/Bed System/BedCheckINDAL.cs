using Microsoft.VisualBasic;
using SGMOSOL.ADMIN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SGMOSOL.BAL.BadBAL;

namespace SGMOSOL.DAL
{
    internal class BedCheckINDAL
    {
        CommonFunctions cf = new CommonFunctions();
        private void SetError(string str)
        {
            clsConnection.mErrorResult = clsConnection.mErrorResult + Microsoft.VisualBasic.Constants.vbNewLine + str;
        }
        public DataTable GetDrMaxSrNo(long lngCtrMachId = 0, long lngComId = 0, long lngLocId = 0, long lngDeptId = 0, long lngFYId = 0)
        {
            DataTable dr = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_BEDGetMaxSerialNo", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@lngComId", lngComId);
                command.Parameters.AddWithValue("@lngLocId", lngLocId);
                command.Parameters.AddWithValue("@lngDeptId", lngDeptId);
                command.Parameters.AddWithValue("@lngFYId", lngFYId);

                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dr;
        }
        public DataTable GetDsBedCheckInMst(long lngLockerCheckInMstId = 0, long lngComId = 0, long lngLocId = 0, long lngFYId = 0, string strDate = "", long lngSerialNo = 0)
        {
            DataTable dr = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetBedCheckInMst", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@lngLockerCheckInMstId", lngLockerCheckInMstId);
                command.Parameters.AddWithValue("@lngComId", lngComId);
                command.Parameters.AddWithValue("@lngLocId", lngLocId);
                command.Parameters.AddWithValue("@lngFYId", lngFYId);
                command.Parameters.AddWithValue("@strDate", strDate);
                command.Parameters.AddWithValue("@lngSerialNo", lngSerialNo);

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
                SqlCommand command = new SqlCommand("SP_GetPrintRcptDet", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@lngPrintRcptMstId", (object)lngPrintRcptMstId ?? DBNull.Value);
                command.Parameters.AddWithValue("@intUserId", (object)intUserId ?? DBNull.Value);

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
            long lngErrNo = 0;
            try
            {
                clsConnection.glbTransaction = clsConnection.glbCon.BeginTransaction();

                SetError("Inserting Into BED_CHECK_IN_MST_T ");
                lngErrNo = InsertMaster(RoomCheckInMst, strUserName, strMacName);
                if (lngErrNo < 0)
                {
                    clsConnection.glbTransaction.Rollback();
                    return lngErrNo;
                }

                dr = GetDrRoomCheckInMstId(strMacName);
                if (dr.Rows.Count > 0)
                {
                    lngMstId = Convert.ToInt64(dr.Rows[0]["CheckInMstId"]);
                    lngSerialNo = Convert.ToInt64(dr.Rows[0]["SerialNo"]);
                    EndteredOn = Convert.ToDateTime(dr.Rows[0]["EnteredOn"]);
                }
                //dr.Close();

                SetError("Inserting Into BN_ROOM_CHECK_IN_DET_T ");
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
                //if (!dr == null)
                //    dr.Close();
                clsConnection.glbTransaction.Rollback();
                SetError(ex.ToString());
                return -1;
            }
        }
        public long InsertMaster(BedCheckInMst roomCheckInMst, string userName, string machineName)
        {
            try
            {

                SqlCommand command = new SqlCommand("SP_InsertBedCheckInMst", clsConnection.GetConnection());

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ComId", roomCheckInMst.ComId);
                command.Parameters.AddWithValue("@LocId", roomCheckInMst.LocId);
                command.Parameters.AddWithValue("@DeptId", roomCheckInMst.DeptId);
                command.Parameters.AddWithValue("@CtrMachId", roomCheckInMst.CtrMachId);
                command.Parameters.AddWithValue("@FyId", roomCheckInMst.FyId);
                command.Parameters.AddWithValue("@InDate", roomCheckInMst.InDate);
                command.Parameters.AddWithValue("@InTime", roomCheckInMst.InTime);
                command.Parameters.AddWithValue("@Name", roomCheckInMst.Name);
                command.Parameters.AddWithValue("@Image", roomCheckInMst.Image);
                command.Parameters.AddWithValue("@Place", roomCheckInMst.Place);
                command.Parameters.AddWithValue("@BhaktTypeId", roomCheckInMst.BhaktTypeId);
                command.Parameters.AddWithValue("@Days", roomCheckInMst.Days);
                command.Parameters.AddWithValue("@NoOfBeds", roomCheckInMst.NoOfBeds);
                command.Parameters.AddWithValue("@NoOfPersons", roomCheckInMst.NoOfPersons);
                command.Parameters.AddWithValue("@OutDate", roomCheckInMst.OutDate);
                command.Parameters.AddWithValue("@OutTime", roomCheckInMst.OutTime);
                command.Parameters.AddWithValue("@Advance", roomCheckInMst.Advance);
                command.Parameters.AddWithValue("@Rent", roomCheckInMst.Rent);
                command.Parameters.AddWithValue("@UserId", roomCheckInMst.UserId);
                command.Parameters.AddWithValue("@ScanDoc", roomCheckInMst.ScanDoc);
                command.Parameters.AddWithValue("@ServerName", roomCheckInMst.ServerName);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@MachineName", machineName);
                command.Parameters.AddWithValue("@Barcode", roomCheckInMst.Barcode);
                command.Parameters.AddWithValue("@MobNo", roomCheckInMst.mob_no);
                command.Parameters.AddWithValue("@ImagePath", roomCheckInMst.ImagePath);
                command.Parameters.AddWithValue("@CheckInId", roomCheckInMst.CheckIn_ID);

                return clsConnection.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -10;
            }
        }
        public DataTable GetDrRoomCheckInMstId(string machineName = null)
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_GetRoomCheckInMst", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(machineName))
                {
                    command.Parameters.AddWithValue("@MachineName", machineName);
                }
                else
                {
                    command.Parameters.AddWithValue("@MachineName", DBNull.Value);
                }

                return clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return new DataTable();
            }
        }
        public long InsertDetail(Collection coll, long printRcptMstId, string strUserName, string strMachineName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("your_connection_string"))
                {
                    connection.Open();
                    foreach (BedCheckInDet item in coll)
                    {
                        SqlCommand command = new SqlCommand("SP_InsertBedCheckInDet", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PrintRcptMstId", printRcptMstId);
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
                }
                return 0; // Success
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return -1; // Error
            }
        }
    }
}
