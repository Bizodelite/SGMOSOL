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
    internal class RoomChangeDAL
    {
        CommonFunctions cf = new CommonFunctions();
        System.Data.DataTable Dr = new System.Data.DataTable();
        RoomCheckInDAL RoomCheckInDALobj = new RoomCheckInDAL();
        public System.Data.DataTable GetDrRoomChangeMst(long lngLockerCheckInMstId = 0, string strDate = "", string lngSerialNo = "", long lngCtrMachId = 0, long lngComId = 0, long lngLocId = 0, long lngDeptId = 0, long lngFYId = 0, string strUserName = "")
        {
            SqlCommand command = new SqlCommand("SP_GetDrRoomChangeMst", clsConnection.GetConnection());
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@lngLockerCheckInMstId", lngLockerCheckInMstId);
            command.Parameters.AddWithValue("@strDate", strDate);
            command.Parameters.AddWithValue("@lngSerialNo", lngSerialNo);
            command.Parameters.AddWithValue("@lngComId", lngComId);
            command.Parameters.AddWithValue("@lngLocId", lngLocId);
            command.Parameters.AddWithValue("@lngFYId", lngFYId);

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
        public System.Data.DataTable findRoom(long CheckInMstId)
        {
            SqlCommand command = new SqlCommand("SP_findRoom", clsConnection.GetConnection());
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@CheckInMstId", CheckInMstId);
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
        public System.Data.DataTable GetDrRoomCheckInDet(long lngRoomCheckInMstId = 0, long lngCtrMachId = 0)
        {
            SqlCommand command = new SqlCommand("SP_GetDrRoomCheckChangeInDet", clsConnection.GetConnection());
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@lngRoomCheckInMstId", lngRoomCheckInMstId);
            command.Parameters.AddWithValue("@lngCtrMachId", lngCtrMachId);

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
    }
}
