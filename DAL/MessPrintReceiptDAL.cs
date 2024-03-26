using CrystalDecisions.Shared;
using SGMOSOL.ADMIN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using static System.Windows.Forms.AxHost;

namespace SGMOSOL.DAL.Locker
{
    internal class MessPrintReceiptDAL
    {
        long lngErrNum = 0;
        StringBuilder strSQL = new StringBuilder();
        StringBuilder strSQL2 = new StringBuilder();
        StringBuilder strSQL3 = new StringBuilder();
        clsConnection mDsCon = new clsConnection();
        DataTable dr = new DataTable();
        CommonFunctions cf = new CommonFunctions();
        public System.Data.DataSet GetDsDailyDengi(string strDate, Int64 intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDailyDengi", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@strDate", strDate);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                lngErrNum = -91;
            }
                return DS;
        }

        public DataTable GetDrDailyDengi_UserNames(string strDate, Int64 intCtrMachId)
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDailyDengiUserNames", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@strDate", strDate);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);

                dr = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                lngErrNum = -91;
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
                return dr;
        }

        public System.Data.DataSet GetDsPrasadWatap(string strFDate, string strTDate, Int64 intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetPrasadWatapData", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@strFDate", strFDate);
                command.Parameters.AddWithValue("@strTDate", strTDate);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exception
            }

            return DS;
        }

        public System.Data.DataSet GetDsReceiptDetail(string strFDate, string strTDate, Int64 intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetReceiptDetailData", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@strFDate", strFDate);
                command.Parameters.AddWithValue("@strTDate", strTDate);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exception
            }

            return DS;
        }

        public System.Data.DataSet GetDsEntryGateUserWise(string strFDate, string strTDate, Int64 intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetEntryGateUserWiseData", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@strFDate", strFDate);
                command.Parameters.AddWithValue("@strTDate", strTDate);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exception
            }

            return DS;
        }

        public System.Data.DataSet GetDsEntryGateDailyReceipt(string strFDate, Int64 intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetEntryGateDailyReceiptData", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@strFDate", strFDate);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exception
            }

            return DS;
        }


        public System.Data.DataSet GetDsToyTrainUserWise(string strFDate, string strTDate, Int64 intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetToyTrainUserWiseData", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@strFDate", strFDate);
                command.Parameters.AddWithValue("@strTDate", strTDate);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exception
            }

            return DS;
        }

        public System.Data.DataSet GetDsGameUserWise(string strFDate, string strTDate, Int64 intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetGameUserWiseData", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@strFDate", strFDate);
                command.Parameters.AddWithValue("@strTDate", strTDate);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exception
            }

            return DS;
        }

        public System.Data.DataSet GetDsToyTrainDailyReceipt(string strFDate, Int64 intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetToyTrainDailyReceiptData", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@strFDate", strFDate);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exception
            }
            return DS;
        }
        public System.Data.DataSet GetDengiUserwise(string strFromDate, string strToDate, string strFromTime, string strToTime, Int64 intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDengiUserwiseData", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@strFromDate", strFromDate);
                command.Parameters.AddWithValue("@strToDate", strToDate);
                command.Parameters.AddWithValue("@strFromTime", strFromTime);
                command.Parameters.AddWithValue("@strToTime", strToTime);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exception
            }
            return DS;
        }
        public System.Data.DataSet GetDsDengiReceiptDetail(string strFDate, string strTDate, Int64 intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDengiReceiptDetailData", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@strFromDate", strFDate);
                command.Parameters.AddWithValue("@strToDate", strTDate);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                lngErrNum = -91;
            }
            return DS;
        }

        public System.Data.DataSet GetDsGameDailyReceipt(string strFDate, Int64 intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetGameDailyReceiptData", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@strFDate", strFDate);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                lngErrNum = -91;
            }
            return DS;
        }
        public System.Data.DataSet GetDsDailyDengiMess(string strDate, Int64 intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDailyDengiMessData", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@strDate", strDate);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                lngErrNum = -91;
            }
            return DS;
        }
        public System.Data.DataSet GetDsEntryGateReceiptDetail(string strFromDate, string strToDate, Int64 intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetEntryGateReceiptDetail", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@FromDate", strFromDate);
                command.Parameters.AddWithValue("@ToDate", strToDate);
                command.Parameters.AddWithValue("@CtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exceptions
                lngErrNum = -91;
            }
            return DS;
        }
        public System.Data.DataSet GetChequeReptDetail(string strDate, Int64 intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetChequeReptDetail", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Date", strDate);
                command.Parameters.AddWithValue("@CtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exceptions
                lngErrNum = -91;
            }
            return DS;
        }

        public System.Data.DataSet GetDengiVoucherDetails(string strFromDate, string strToDate, Int64 intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDengiVoucherDetails", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@FromDate", strFromDate);
                command.Parameters.AddWithValue("@ToDate", strToDate);
                command.Parameters.AddWithValue("@CtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exceptions
                lngErrNum = -91;
            }
            return DS;
        }

        public System.Data.DataSet GetDsDailyDengiReceipt(string strFDate, Int64 intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDailyDengiReceiptDetails", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@FDate", strFDate);
                command.Parameters.AddWithValue("@CtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exceptions
                lngErrNum = -91;
            }
            return DS;
        }


        public System.Data.DataSet GetDsckeckoutLockeruserwise(string strFromDate, string strToDate, string strFromTime, string strToTime, Int64 intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetCheckoutLockerUserWise", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@FromDate", strFromDate);
                command.Parameters.AddWithValue("@ToDate", strToDate);
                command.Parameters.AddWithValue("@FromTime", strFromTime);
                command.Parameters.AddWithValue("@ToTime", strToTime);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exceptions
                lngErrNum = -91;
            }
            return DS;
        }

        public System.Data.DataSet GetDschkinLockeruserwise(string strFromDate, string strToDate, string strFromTime, string strToTime, Int64 intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetCheckinLockerUserWise", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@FromDate", strFromDate);
                command.Parameters.AddWithValue("@ToDate", strToDate);
                command.Parameters.AddWithValue("@FromTime", strFromTime);
                command.Parameters.AddWithValue("@ToTime", strToTime);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exceptions
                lngErrNum = -91;
            }
            return DS;
        }


        public System.Data.DataSet GetDslockerreceiptdetail(string strFromDate, string strToDate, Int64 intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetLockerReceiptDetail", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@FromDate", strFromDate);
                command.Parameters.AddWithValue("@ToDate", strToDate);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exceptions
                lngErrNum = -91;
            }
            return DS;
        }


        public System.Data.DataSet GetDsLockerCheckInCheckOut(string strFromDate, string strToDate, Int64 intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetLockerCheckInCheckOut", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@FromDate", strFromDate);
                command.Parameters.AddWithValue("@ToDate", strToDate);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exceptions
                lngErrNum = -91;
            }
            return DS;
        }

        public System.Data.DataSet GetDsDailyLockerCheckOut(string strFromDate, Int64 intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDailyLockerCheckOut", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@FromDate", strFromDate);
                command.Parameters.AddWithValue("@CtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exceptions
                lngErrNum = -91;
            }
            return DS;
        }


        public System.Data.DataSet GetDsDailyLockerCheckIn(string strFromDate, Int64 intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDailyLockerCheckIn", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@FromDate", strFromDate);
                command.Parameters.AddWithValue("@CtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exceptions
                lngErrNum = -91;
            }
            return DS;
        }


        public System.Data.DataSet GetDsRoomReceiptDetail(string strFromDate, string strToDate, Int64 intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetRoomReceiptDetail", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@FromDate", strFromDate);
                command.Parameters.AddWithValue("@ToDate", strToDate);
                command.Parameters.AddWithValue("@CtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exceptions
                lngErrNum = -91;
            }
            return DS;
        }

        public System.Data.DataSet GetDsDailyRoomCheckOut(string strFromDate, Int64 intCtrMachId, Int64 intsublocind, Int64 intlocid, Int64 payid, string Username)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDailyRoomCheckOut", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@FromDate", strFromDate);
                command.Parameters.AddWithValue("@Sublocind", intsublocind);
                command.Parameters.AddWithValue("@Locid", intlocid);
                command.Parameters.AddWithValue("@Payid", payid);
                command.Parameters.AddWithValue("@CtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exceptions
                lngErrNum = -91;
            }
            return DS;
        }


        public System.Data.DataSet GetDsDailyRoomCheckIn(string strFromDate, Int64 intCtrMachId, Int64 intsublocind, Int64 intlocid, Int64 payid)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDailyRoomCheckIn", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@FromDate", strFromDate);
                command.Parameters.AddWithValue("@Sublocind", intsublocind);
                command.Parameters.AddWithValue("@Locid", intlocid);
                command.Parameters.AddWithValue("@Payid", payid);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exceptions
                lngErrNum = -91;
            }
            return DS;
        }


        public System.Data.DataSet GetDsLockerAdvanceVouchers(string strFromDate, string strToDate, string strFromTime, string strToTime, Int64 intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetLockerAdvanceVouchers", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@FromDate", strFromDate);
                command.Parameters.AddWithValue("@ToDate", strToDate);
                command.Parameters.AddWithValue("@FromTime", strFromTime);
                command.Parameters.AddWithValue("@ToTime", strToTime);
                command.Parameters.AddWithValue("@CtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exceptions
                lngErrNum = -91;
            }
            return DS;
        }

        public System.Data.DataSet GetDsRoomCheckOutUserwise(string strFromDate, string strToDate, string strFromTime, string strToTime, long intCtrMachId, long intlocid, long ptype)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetRoomCheckOutUserwise", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@strFromDate", strFromDate);
                command.Parameters.AddWithValue("@strToDate", strToDate);
                command.Parameters.AddWithValue("@strFromTime", strFromTime);
                command.Parameters.AddWithValue("@strToTime", strToTime);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);
                command.Parameters.AddWithValue("@intlocid", intlocid);
                command.Parameters.AddWithValue("@ptype", ptype);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exception
            }
            return DS;
        }


        public System.Data.DataSet GetDsRoomCheckInUserwise(string strFromDate, string strToDate, string strFromTime, string strToTime, long intCtrMachId, long intlocid, long ptype)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetRoomCheckInUserwise", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@strFromDate", strFromDate);
                command.Parameters.AddWithValue("@strToDate", strToDate);
                command.Parameters.AddWithValue("@strFromTime", strFromTime);
                command.Parameters.AddWithValue("@strToTime", strToTime);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);
                command.Parameters.AddWithValue("@intlocid", intlocid);
                command.Parameters.AddWithValue("@ptype", ptype);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exception
            }
            return DS;
        }


        public System.Data.DataSet GetDsRoomAdvanceVouchers(string strFromDate, string strToDate, string strFromTime, string strToTime, long intCtrMachId, long Locid, long pid)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetRoomAdvanceVouchers", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@strFromDate", strFromDate);
                command.Parameters.AddWithValue("@strToDate", strToDate);
                command.Parameters.AddWithValue("@strFromTime", strFromTime);
                command.Parameters.AddWithValue("@strToTime", strToTime);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);
                command.Parameters.AddWithValue("@Locid", Locid);
                command.Parameters.AddWithValue("@pid", pid);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exception
            }
            return DS;
        }

        public System.Data.DataSet GetDsBedAdvanceVouchers(string strFromDate, string strToDate, string strFromTime, string strToTime, long intCtrMachId, long payid)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetBedAdvanceVouchers", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@strFromDate", strFromDate);
                command.Parameters.AddWithValue("@strToDate", strToDate);
                command.Parameters.AddWithValue("@strFromTime", strFromTime);
                command.Parameters.AddWithValue("@strToTime", strToTime);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);
                command.Parameters.AddWithValue("@payid", payid);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exception
            }
            return DS;
        }


        public System.Data.DataSet GetDsBNUserwiseCashReport(string strFromDate, string strToDate, string strFromTime, string strToTime, long intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetBNUserwiseCashReport", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@strFromDate", strFromDate);
                command.Parameters.AddWithValue("@strToDate", strToDate);
                command.Parameters.AddWithValue("@strFromTime", strFromTime);
                command.Parameters.AddWithValue("@strToTime", strToTime);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exception
            }
            return DS;
        }

        public System.Data.DataSet GetDengiBhetVastuUserwise(string strFromDate, string strToDate, string strFromTime, string strToTime, long intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDengiBhetVastuUserwise", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@strFromDate", strFromDate);
                command.Parameters.AddWithValue("@strToDate", strToDate);
                command.Parameters.AddWithValue("@strFromTime", strFromTime);
                command.Parameters.AddWithValue("@strToTime", strToTime);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exception
            }
            return DS;
        }

        public System.Data.DataSet GetDsAnnadanRec(string strFDate, string strTDate, long intCtrMachId)
        {
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetAnnadanRec", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@strFDate", strFDate);
                command.Parameters.AddWithValue("@strTDate", strTDate);
                command.Parameters.AddWithValue("@intCtrMachId", intCtrMachId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(DS);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle exception
            }
            return DS;
        }


    }
}
