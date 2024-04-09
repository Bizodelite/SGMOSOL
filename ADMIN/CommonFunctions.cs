using System;

using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;
using System.Management;
using System.Windows.Forms;
using System.Security.RightsManagement;
using static System.Windows.Forms.AxHost;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Input;
using SGMOSOL.BAL;
using System.Collections;
using static SGMOSOL.ADMIN.CommonFunctions;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using System.Resources;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;



namespace SGMOSOL.ADMIN
{
    public class CommonFunctions
    {
        public static string SystemHDDModelNo;
        public static string SystemHDDSerialNo;
        public static string SystemMacID;

        public string DBServerName;
        //private byte[] key = new byte[0];
        //private byte[] IV = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        public static string PrjMsgBoxTitle = System.Configuration.ConfigurationManager.AppSettings["MSGBOX_TITLE"].ToString();
        //StringBuilder strSQL = new StringBuilder();
        long lngErrNum = 0;
        //clsConnection clsConnectionobj = new clsConnection();
        public enum eCurrencyType
        {
            Rupees,
            Dollars
        }
        public enum eAction
        {
            ActionView,
            ActionInsert,
            ActionUpdate,
            ActionDelete,
            ActionLocked
        }
        public enum eTokenDetail
        {
            StatusYes = 177,
            StatusNo = 178,
            Damaged = 191,
            Cheque = 7,
            Cash = 8,
            DD = 9,
            CreditCard = 44,
            NetTransfer = 181,
            StatusActive = 3,
            StatusInActive = 4,
            Swap = 202,
            // Amit MedicalPatientType 
            Bhakt = 203,
            Sevak = 204,
            Male = 205,
            Female = 206
        }
        public enum eModType
        {
            Bhojnalay = 1,
            Game = 2,
            Toytrain = 3,
            Entrygate = 4,
            Locker = 5,
            Dengi = 6,
            BhaktaNiwas = 7,
            BedSystem = 8,
            RoomMaint = 9,
            Collegemess = 10,
            Medical = 11
        }
        
        public static string fncEncode(string password)
        {
            string strDeCodePassword = "";
            for (int ctr = 0; ctr < password.Length; ctr++)
            {
                strDeCodePassword += (char)(password[ctr] + 50);
            }
            return strDeCodePassword;
        }
        public static string fncDecode(string password)
        {
            string strDeCodePassword = "";
            for (int ctr = 0; ctr < password.Length; ctr++)
            {
                strDeCodePassword += (char)(password[ctr] - 50);
            }
            return strDeCodePassword;
        }
        public enum eScreenID
        {
            Login = 100, 

            // --- Security Masters
            User = 1000,
            UserRole = 1100,
            UserRights = 1200,
            RoomLocked = 3210,
            InventoryAlloc = 3001,
            PrintReceipt = 3002,
            PrintReceiptSup = 3003,
            ReqToAdmin = 3004,
            DengiReceipt = 3103,
            DengiReceiptSup = 3105,
            StockDetails = 3005,
            StockReminder = 3006,
            NoteSlip = 3007,
            SaleItemReceipt = 3009,
            NonSalableConsume = 3010,
            Lockerchange = 3205,
            ChequeDengiReceiptVoucher = 1151,
            LockerCheckIn = 3201,
            LockerCheckOut = 3202,
            LockerCheckOutStar = 3207,
            AddDamagedLocker = 3208,
            LockerAvailable = 3203,
            Lockeroccupied = 3206,
            LockerInUse = 3204,
            LockerCheckoutWarning = 4001,
            LockerExtend = 4000,
            LockMoreThen3Day = 4002,
            lockeradvancevouchure = 4003,
            Attendence = 3012,
            CNCRegister = 3017,
            // ----MessReports
            PrasadWatap = 1092,
            UserWize = 1093,
            DailyDengi = 1094,
            ReceiptDetail = 1095,
            ToyTrainUserWise = 1098,
            ToyTrainDailyReceipt = 1099,
            EntryGateUserWise = 1101,
            EntryGateDailyReceipt = 1102,
            GameUserWise = 1104,
            GameDailyReceipt = 1105,
            DailyDengiReceiptRpt = 1106,
            DailyDengiReceipt = 1162,
            SaleItemReceiptsup = 3015,
            DepositReturn = 3013,
            BillReceipt = 3011,
            DepositReceipt = 3010,
            // -----
            EntryGateDet = 3100,
            ToyTrainDet = 3101,
            GameDet = 3102,
            EntryGateOpen = 3104,
            AbhishekRegister = 1161,
            lockercheckoutuserwise = 1129,
            lockercheckinuserwise = 1131,
            lockerreceiptdetail = 1168,
            dengireceiptdetail = 1172,
            lockerCheckInCheckOut = 1171,
            lockercheckoutdaily = 1130,
            lockercheckindaily = 1132,
            EntryGateReceiptDetail = 1128,
            DengiUserwise = 3016,
            // added santosh
            DengiBhetvastuUserwise = 10006,
            // bhakt niwas
            RoomAssetMaster = 3212,
            BedCheckIn = 5002,
            BedCheckOut = 5003,
            BedOccupied = 1407,
            RoomCheckOut = 1176,
            RoomCheckIn = 1175,
            RoomAvailable = 1177,
            OccupiedRoom = 1178,
            RoomChange = 1179,
            RoomOutOfOrder = 1180,
            AddDamagedRoom = 1181,
            RoomCheckOutStar = 1182,
            RoomCheckOutBefore = 6001,
            BNRoomCheckOutUserwise = 1187,
            BNRoomReceiptDetail = 1185,
            BNRoomAdvanceVoucher = 1188,
            BNDailyRoomCheckOut = 1186,
            BNUserwiseCashReport = 1193,
            RoomExtend = 1192,
            BEDRoomAdvanceVoucher = 1406,
            BNDailyRoomCheckIN = 4041,
            BNRoomCheckINUserwise = 4040,
            DailyVoucherEntryBhojnalay = 7001,
            DailyVoucherEntryToyTrain = 7002,
            DailyVoucherEntryGame = 7003,
            DailyVoucherEntryEntryGate = 7004,
            DailyVoucherEntryLocker = 7005,
            DailyVoucherEntryDengi = 7006,
            DailyVoucherEntryBhaktniwas = 7007,
            DailyVoucherEntryBed = 7008,
            DailyVoucherEntryMedical = 7013,
            DengiBhetVastu = 10005,
            // Added By: Amit 
            // For Purpose :MEDICAL
            MedicalReceiptEntry = 10001,
            MedicalReceiptEntryBhakt = 10002
        }
        public enum eReportID
        {
            PrintReceipt = 3001,
            NoteSlip = 3007,
            LockerCheckIn = 3201,
            LockerCheckOut = 3202,
            LockerCheckOutStar = 3207,
            SaleItemReceiptsup = 5002,
            SaleItemReceipt = 5001,
            lockerCheckInCheckOut = 1171,
            RoomCheckIn = 4201,
            RoomCheckIn1 = 9000,
            RoomCheckOut = 4202,
            RoomCheckOutStar = 4207,
            BNRoomAdvanceVoucher = 1188,
            BNUserwiseCashReport = 1193,
            BedCheckIn = 1219,
            DengiBhetVastu = 10005,
            // Added By: Amit 
            // For Purpose :MedicalReceiptPrint 
            // Date : 30/07/2018
            MedicalReceiptEntry = 10001,
            MedicalReceiptEntryBhakt = 10002,
            // Code Added 14/
            DengiAcknowledge = 10006
        }
        public enum eBhaktaType
        {
            Bhkta,
            Trip,
            Gorup
        }
        public enum ePrintReceipt
        {
            ProductN,
            ProductC,
            Advance,
            Nidhi,
            Qty,
            TotAdv,
            TotNidhi
        }
        //public enum eEntryGate
        //{
        //    Code,
        //    fDate,
        //    tDate,
        //    MinSerial,
        //    MaxSerial,
        //    TatalAmount,
        //    TatalTransCount,
        //    Status,
        //    Location,
        //    Counter,
        //    ReturnedQty,
        //    Guest,
        //    Stock
        //}
        string connectionString = Decrypt(ConfigurationManager.ConnectionStrings["strConnection"].ConnectionString, true);

        DataTable dt = null;
        public DataTable getCountry()
        {
            dt = new DataTable();
            try
            {
                ; SqlCommand command = new SqlCommand("SP_GetCountries", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dt;
        }
        public DataTable getStateById(int countryId)
        {
            dt = new DataTable();
            try
            {
            SqlCommand command = new SqlCommand("SP_GetStatesByCountryId", clsConnection.GetConnection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CountryId", countryId);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
        }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dt;
        }
        public DataTable fillTID()
        {
            DataTable dt = new DataTable();
            try
            {
            SqlCommand command = new SqlCommand("SP_GetTIDByCounterId", clsConnection.GetConnection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CounterId", UserInfo.ctrMachID);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
        }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dt;
        }
        public DataTable getDisctrictbyStateId(int stateId)
        {
            DataTable dt = new DataTable();
            try
            {
            SqlCommand command = new SqlCommand("SP_GetDistrictsByStateId", clsConnection.GetConnection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@StateId", stateId);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
        }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dt;
        }
        public DataTable GetDocumentType()
        {
            DataTable dt = new DataTable();
            try
            {
            SqlCommand command = new SqlCommand("SP_GetDocumentTypes", clsConnection.GetConnection());
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
        }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dt;
        }
        public DataTable getPaymentMode()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("SELECT isnull(Token_Detail_Id,0) AS Token_Detail_Id");
                    stringBuilder.Append(", isnull(Token_Detail_Name, '') AS Token_Detail_Name");
                    stringBuilder.Append(", isnull(Token_Detail_code, '') AS Token_Detail_Name FROM");
                    stringBuilder.Append("  com_token_det_t");
                    stringBuilder.Append("  inner join PAYMENT_TYPE_MST on PAYMENT_TYPE=Token_Detail_Id ");
                    stringBuilder.Append(" where Token_Mst_Id = 6 and MODULE_ID=9 and STATUS=1 and COUNTER_ID=" + UserInfo.ctrMachID + " ");
                    SqlDataAdapter adapter = new SqlDataAdapter(stringBuilder.ToString(), connection);
                    connection.Close();
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dt;
        }
        public DataTable getDengiType()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetDengiTypes", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dt;
        }
        public DataTable getGotra()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetGotra", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dt;
        }
        public string ExtractDate(string input)
        {
            int valueIndex = input.IndexOf("Value:");

            if (valueIndex != -1)
            {
                string datePart = input.Substring(valueIndex + "Value:".Length).Trim();
                if (DateTime.TryParseExact(datePart, "dd-MM-yyyy HH:mm:ss", null, DateTimeStyles.None, out DateTime parsedDate))
                {
                    return parsedDate.ToString("yyyy-MM-dd");
                }
            }
            return string.Empty;
        }
        public DateTime? TryParseDateTime(string dateString)
        {
            DateTime parsedDateTime;

            if (DateTime.TryParse(dateString, out parsedDateTime))
            {
                if (IsValidSqlDateTime(parsedDateTime))
                {
                    return parsedDateTime;
                }
                else
                {
                    return null; // Set to null if the DateTime is outside the valid range
                }
            }
            else
            {
                return null; // Set to null if parsing fails
            }
        }
        static bool IsValidSqlDateTime(DateTime dateTime)
        {
            // Check if the DateTime is within the valid range for SQL Server's datetime type
            return (dateTime >= SqlDateTime.MinValue.Value) && (dateTime <= SqlDateTime.MaxValue.Value);
        }


        public string IsPasswordValid(string password)
        {
            StringBuilder sb = new StringBuilder();
            if (password.Length < 8)
            {
                sb.AppendLine("Password must be at least 8 characters long");
            }
            if (!password.Any(char.IsUpper))
            {
                sb.AppendLine("Password must contain at least one uppercase letter.");
            }
            if (!Regex.IsMatch(password, @"[!@#$%^&*()_+{}\[\]:;<>,.?~\\-]"))
            {
                sb.AppendLine("Password must contain at least one special character.");
            }
            if (!password.Any(char.IsDigit))
            {
                sb.AppendLine("Password must contain at least one numeric value.");
            }
            if (CheckPassWord(password))
            {
                sb.AppendLine("Entered password is in your last 3 entered password, Please enter new password.");
            }
            return sb.ToString();
        }
        public bool CheckPassWord(string password)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_CheckLast3PassWord", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", UserInfo.UserId);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    if (item["Password"].ToString() == password)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return false;
        }
        // Code added Robin
        public string words(double numbers)
        {
            int number = Convert.ToInt32(numbers);

            if (number == 0) return "Zero";
            if (number == -2147483648) return "Minus Two Hundred and Fourteen Crore Seventy Four Lakh Eighty Three Thousand Six Hundred and Forty Eight";
            int[] num = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (number < 0)
            {
                sb.Append("Minus ");
                number = -number;
            }
            string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };
            num[0] = number % 1000; // units
            num[1] = number / 1000;
            num[2] = number / 100000;
            num[1] = num[1] - 100 * num[2]; // thousands
            num[3] = number / 10000000; // crores
            num[2] = num[2] - 100 * num[3]; // lakhs
            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0) continue;
                u = num[i] % 10; // ones
                t = num[i] / 10;
                h = num[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (numbers > 99)
                    {
                        if (h > 0 || i == 0) sb.Append("and ");
                    };
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }

        //public DataTable isMachineAccess(int uid, int ctr_mach_id)
        //{
        //    dt = new DataTable();
        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();
        //            string query = "select * from SEC_USER_COUNTER_MST_T where USER_ID=" + uid + " and CTR_MACH_ID=" + ctr_mach_id + "";
        //            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
        //            connection.Close();
        //            adapter.Fill(dt);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);

        //    }
        //    return dt;
        //}
        public DataTable getUserAllDetails(string strMachineId, int uid = 0)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetUserDetails", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MachineId", strMachineId);
                if (uid != 0)
                {
                    command.Parameters.AddWithValue("@UserId", uid);
                }
                else
                {
                    command.Parameters.AddWithValue("@UserId", DBNull.Value);
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dt;
        }
        public string getFormTitle()
        {
            DataTable dt = new DataTable();
            string formTitle = "";
            try
            {
                dt = getUserAllDetails(UserInfo.MachineId);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        formTitle = row["LOC_FNAME"].ToString() + " / " + row["Loc_Name"].ToString() + " / " + row["Department_Name"].ToString() + " / " + row["COUNTER_MACHINE_TITLE"] + "";
                    }
                }
            }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return formTitle;
        }
        public DataTable getFYDetail()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetFinancialYearDetails", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dt;
        }

        public bool IsValidPan(string panNumber)
        {
            string pattern = @"^[A-Z]{5}[0-9]{4}[A-Z]{1}$";
            return Regex.IsMatch(panNumber, pattern);
        }
        public bool IsValidAadhar(string aadharNumber)
        {
            string pattern = @"^\d{12}$";
            return Regex.IsMatch(aadharNumber, pattern);
        }
        public bool IsValidPassport(string passportNumber)
        {
            string pattern = @"^[A-Z]{1}\d{7}$";
            return Regex.IsMatch(passportNumber, pattern);
        }
        public bool IsValidLicenseNumber(string licenseNumber)
        {
            string pattern = @"^[A-Z]{2}[0-9]{13}$";
            return Regex.IsMatch(licenseNumber, pattern);
        }
        public bool IsValidVoterId(string voterIdNumber)
        {
            string pattern = @"^[A-Z]{3}\d{7}$";
            return Regex.IsMatch(voterIdNumber, pattern);
        }
        public void InsertErrorLog(string errorMsg, string module, string version)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("DML_ERROR_LOG", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Loc_Id", UserInfo.Loc_id);
                        command.Parameters.AddWithValue("@Dept_Id", UserInfo.Dept_id);
                        command.Parameters.AddWithValue("@Ctr_mac_Id", UserInfo.ctrMachID);
                        command.Parameters.AddWithValue("@User_Id", UserInfo.UserId);
                        command.Parameters.AddWithValue("@Error_msg", errorMsg);
                        command.Parameters.AddWithValue("@Module", module);
                        command.Parameters.AddWithValue("@Version", version);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }

        public void GetMacId()
        {
            int count = 0;
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                if ((bool)mo["IPEnabled"] == true)
                {
                    if (count == 0)
                    {
                        SystemHDDModelNo = mo["MacAddress"].ToString();
                        SystemHDDModelNo = SystemHDDModelNo.Replace(":", "-");
                    }
                    else if (count == 1)
                    {
                        SystemHDDSerialNo = mo["MacAddress"].ToString();
                        SystemHDDSerialNo = SystemHDDSerialNo.Replace(":", "-");
                    }
                    else if (count == 2)
                    {
                        SystemMacID = mo["MacAddress"].ToString();
                        SystemMacID = SystemMacID.Replace(":", "-");
                        break;
                    }
                    count++;
                }
            }
            if (SystemHDDModelNo != "")
            {
                UserInfo.MachineId = SystemHDDModelNo;
            }
            else if (SystemHDDSerialNo != "")
            {
                UserInfo.MachineId = SystemHDDSerialNo;
            }
            else
            {
                UserInfo.MachineId = SystemMacID;
            }
        }

        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            string result;
            try
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(toEncrypt);
                string s = "RSNGBP";
                byte[] key;
                if (useHashing)
                {
                    System.Security.Cryptography.MD5CryptoServiceProvider mD5CryptoServiceProvider = new System.Security.Cryptography.MD5CryptoServiceProvider();
                    key = mD5CryptoServiceProvider.ComputeHash(System.Text.Encoding.UTF8.GetBytes(s));
                    mD5CryptoServiceProvider.Clear();
                }
                else
                {
                    key = System.Text.Encoding.UTF8.GetBytes(s);
                }
                System.Security.Cryptography.TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new System.Security.Cryptography.TripleDESCryptoServiceProvider();
                tripleDESCryptoServiceProvider.Key = key;
                tripleDESCryptoServiceProvider.Mode = System.Security.Cryptography.CipherMode.ECB;
                tripleDESCryptoServiceProvider.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                System.Security.Cryptography.ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateEncryptor();
                byte[] array = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
                tripleDESCryptoServiceProvider.Clear();
                result = System.Convert.ToBase64String(array, 0, array.Length);
            }
            catch
            {
                result = toEncrypt;
            }
            return result;
        }

        public static string Decrypt(string cipherString, bool useHashing)
        {
            string @string;
            try
            {
                byte[] array = System.Convert.FromBase64String(cipherString);
                string s = "RSNGBP";
                byte[] key;
                if (useHashing)
                {
                    System.Security.Cryptography.MD5CryptoServiceProvider mD5CryptoServiceProvider = new System.Security.Cryptography.MD5CryptoServiceProvider();
                    key = mD5CryptoServiceProvider.ComputeHash(System.Text.Encoding.UTF8.GetBytes(s));
                    mD5CryptoServiceProvider.Clear();
                }
                else
                {
                    key = System.Text.Encoding.UTF8.GetBytes(s);
                }
                System.Security.Cryptography.TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new System.Security.Cryptography.TripleDESCryptoServiceProvider();
                tripleDESCryptoServiceProvider.Key = key;
                tripleDESCryptoServiceProvider.Mode = System.Security.Cryptography.CipherMode.ECB;
                tripleDESCryptoServiceProvider.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                System.Security.Cryptography.ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateDecryptor();
                byte[] bytes = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
                tripleDESCryptoServiceProvider.Clear();
                @string = System.Text.Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                @string = cipherString;
            }
            return @string;
        }

        public DataTable getCurrency()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SP_GetCurrency", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return dt;
        }
        public bool fncSetDateAndRange(DateTimePicker dtpDate)
        {
            DateTime sCurDate;
            bool sBlnFlag;

            try
            {
                sCurDate = GetComSysdate();
                if (sCurDate >= UserInfo.FYStartDate & sCurDate <= UserInfo.FYEndDate)
                {
                    dtpDate.MinDate = UserInfo.FYStartDate;
                    dtpDate.MaxDate = sCurDate;
                    dtpDate.Value = sCurDate;
                }
                else
                {
                    dtpDate.MinDate = UserInfo.FYStartDate;
                    dtpDate.MaxDate = UserInfo.FYEndDate;
                    if (sCurDate < UserInfo.FYStartDate)
                        dtpDate.Value = UserInfo.FYStartDate;
                    else
                        dtpDate.Value = UserInfo.FYEndDate;
                }
                sBlnFlag = true;
            }
            catch (Exception ex)
            {
                InsertErrorLog("fncSetDateAndRange: " + ex.ToString(), UserInfo.module, UserInfo.version);
                sBlnFlag = false;
            }
            return sBlnFlag;
        }
        public bool fncSysTime(DateTimePicker dtpDate)
        {
            DateTime sCurTime;
            bool sBlnFlag;

            try
            {
                sCurTime = GetComSysTime();
                dtpDate.Value = sCurTime;

                sBlnFlag = true;
            }
            catch (Exception ex)
            {
                InsertErrorLog("fncSetDateAndRange : " + ex.ToString(), UserInfo.module, UserInfo.version);
                sBlnFlag = false;
            }
            return sBlnFlag;
        }
        public DateTime GetComSysdate(string strUserName = "")
        {
            DataTable dt = new DataTable();
            DateTime sysDate = DateTime.MinValue;

            try
            {
                SqlCommand command = new SqlCommand("SP_GetSystemDate", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);

                if (dt.Rows.Count > 0 && dt.Rows[0]["SyDate"] != DBNull.Value)
                {
                    sysDate = Convert.ToDateTime(dt.Rows[0]["SyDate"]);
                }
            }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }

            return sysDate;
        }
        public DateTime GetComSysTime(string strUserName = "")
        {
            DataTable dt = new DataTable();
            DateTime sysTime = DateTime.MinValue;

            try
            {
                SqlCommand command = new SqlCommand("SP_GetSystemTime", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);

                if (dt.Rows.Count > 0 && dt.Rows[0]["SysTime"] != DBNull.Value)
                {
                    sysTime = Convert.ToDateTime(dt.Rows[0]["SysTime"]);
                }
            }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }

            return sysTime;
        }
        public void FillListBox(ListBox lsb, DataTable myReader, string cboAttribute, string lsbIndex = "", string Name2 = "")
        {
            try
            {
                lsb.Items.Clear();
                {
                    int i = 0;
                    var withBlock = myReader;
                    //while (myReader.Read())
                    foreach (DataRow myReaderitem in myReader.Rows)
                    {
                        if (lsbIndex == "")
                            lsb.Items.Add(withBlock.Rows[i][cboAttribute]);
                        else if (Name2 == "")
                            lsb.Items.Add(new clsItemData(withBlock.Rows[i][cboAttribute].ToString(), Convert.ToInt32(myReaderitem[lsbIndex])));
                        else
                            lsb.Items.Add(new clsItemData(withBlock.Rows[i][cboAttribute].ToString(), Convert.ToInt32(myReaderitem[lsbIndex]), Convert.ToInt64(myReaderitem[Name2])));
                        i++;
                    }
                    //withBlock.Close();
                }
            }
            catch (Exception ex)
            {
                //if (myReader != null)
                //    myReader.Close();

                SetError("FillListBox : " + lsb.Name + Constants.vbCrLf + ex.ToString());
            }
        }

        public System.Data.DataTable GetDrCounterMachId(int intUserId, string strHddModelNo, string strHddSerialNo, string strMacId, int mod_type, int CtrMachId = 0)
        {
            System.Data.DataTable mReader = new DataTable();

            try
            {
                SqlCommand command = new SqlCommand("SP_GetUserCounterMachineDetails", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                // Add parameters
                command.Parameters.AddWithValue("@UserId", intUserId);
                command.Parameters.AddWithValue("@HddModelNo", strHddModelNo);
                command.Parameters.AddWithValue("@HddSerialNo", (object)strHddSerialNo ?? DBNull.Value);
                command.Parameters.AddWithValue("@MacId", (object)strMacId ?? DBNull.Value);
                command.Parameters.AddWithValue("@ModType", mod_type);
                command.Parameters.AddWithValue("@CtrMachId", CtrMachId);
                mReader = clsConnection.ExecuteReader(command);
                if (mReader.Rows.Count == 0)
                {
                    mReader.Rows.Add(new Object[] { "", "",0,0,"","","",0 });
                }
            }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return mReader;
        }
        public static void setCursor(Form Frm, bool mouDefault = true)
        {
            if (mouDefault)
                Frm.Cursor = Cursors.Default;
            else
                Frm.Cursor = Cursors.WaitCursor;
        }
        public static string FormatDateToString(DateTime mDate)
        {
            // FormatDateToString = Microsoft.VisualBasic.Day(mDate) & "/" & Month(mDate) & "/" & Year(mDate)
            //return String.Format(mDate.Day.ToString(), "00") + "/" + String.Format(mDate.Month.ToString(), "00") + "/" + String.Format(mDate.Year.ToString(), "0000");
            return mDate.Year.ToString("0000") + "-" + mDate.Day.ToString("00") + "-" + mDate.Month.ToString("00");
        }
        public int lsbItemData(ListBox lsb, int intIndex)
        {
            try
            {
                clsItemData mItemData;
                mItemData = (clsItemData)lsb.Items[intIndex];
                return mItemData.ItemData;
            }
            catch (Exception ex)
            {
                InsertErrorLog("lsbItemData : " + lsb.Name + ex.ToString(), UserInfo.module, UserInfo.version);
                return -1;
            }
        }
        public string lsbItemName(ListBox lsb, int intIndex)
        {
            try
            {
                clsItemData mItemName;
                mItemName = (clsItemData)lsb.Items[intIndex];
                return mItemName.Name;
            }
            catch (Exception ex)
            {
                InsertErrorLog("lsbItemName : " + lsb.Name + ex.ToString(), UserInfo.module, UserInfo.version);
                return "";
            }
        }

        public long lsbItemName2(ListBox lsb, int intIndex)
        {
            try
            {
                clsItemData mItemName;
                mItemName = (clsItemData)lsb.Items[intIndex];
                return mItemName.ItemName2;
            }
            catch (Exception ex)
            {
                InsertErrorLog("lsbItemName : " + lsb.Name + ex.ToString(), UserInfo.module, UserInfo.version);
                return -1;
            }
        }
        public string GetErrorMessage(string[] colAray, long Message_Id)
        {
            int i = 0;
            string strMessage = "";
            System.Data.DataSet DstError = new System.Data.DataSet();


            try
            {
                // DstError.ReadXml(Mid(System.AppDomain.CurrentDomain.BaseDirectory, 1, Len(System.AppDomain.CurrentDomain.BaseDirectory) - 4) & "ErrorMsg.xml")
                DstError.ReadXml(Application.StartupPath + "\\ErrorMsg.xml");

                //string[] COL;
                foreach (DataRow DrError in DstError.Tables[0].Rows)
                {
                    if (Convert.ToInt32(DrError[0]) == Message_Id)
                    {
                        strMessage = DrError[1].ToString();
                        for (i = 0; i <= colAray.GetUpperBound(0); i++)
                            strMessage = strMessage.Replace("[PARAM" + (i + 1) + "]", colAray[i]);
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                InsertErrorLog("GetErrorMessage : " + ex.ToString(), UserInfo.module, UserInfo.version);
                strMessage = "";
            }
            return strMessage;
        }
        public DataTable GetDsSearchDataNew(string StrTableName, string strFromDate, string strToDate, string strName, int intCtrMachId, long intLocId, long intUserId)
        {
            DataTable dataTable = new DataTable();

            try
            {
                SqlCommand command = new SqlCommand("SP_GetSearchDataNew", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@TableName", StrTableName);
                command.Parameters.AddWithValue("@FromDate", strFromDate);
                command.Parameters.AddWithValue("@ToDate", strToDate);
                command.Parameters.AddWithValue("@Name", strName);
                command.Parameters.AddWithValue("@CtrMachId", intCtrMachId);
                command.Parameters.AddWithValue("@LocId", intLocId);
                command.Parameters.AddWithValue("@UserId", intUserId);
                dataTable = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle or log the exception
            }

            return dataTable;
        }
        public void LockUnLockGrid(int intStartRow, int intEndRow, int intStartCol, int intEndCol, DataGridView fpsGrid, bool blnLock, bool blnColor = false)
        {
            int intCol;
            int intRow;
            try
            {
                {
                    var withBlock = fpsGrid;
                    for (intRow = intStartRow; intRow <= intEndRow; intRow++)
                    {
                        if (blnColor == true)
                            withBlock.Rows[intRow].DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                        else
                            withBlock.Rows[intRow].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                        for (intCol = intStartCol; intCol <= intEndCol; intCol++)
                            withBlock.Rows[intRow].Cells[intCol].ReadOnly = blnLock;
                        //withBlock.Rows[intRow].Cells[intCol].Locked = blnLock;
                    }
                }
            }
            catch (Exception ex)
            {
                InsertErrorLog("LockUnLockGrid : " + ex.ToString(), UserInfo.module, UserInfo.version);
            }
        }
        public string getNumbersInWords(decimal Numbers, eCurrencyType WhichCurrency = eCurrencyType.Rupees, bool blnInMillion = false)
        {
            string getNumbersInWords = "";
            string strNumerals = "";
            string strDecimals = "";
            string strIntPart = "";
            string strDecPart = "";
            int intDecPos = 0;
            string strAnd = "";
            WhichCurrency = eCurrencyType.Rupees;

            intDecPos = Numbers.ToString().IndexOf(".");

            if (intDecPos > 0)
            {
                strIntPart = Numbers.ToString().Substring(1, intDecPos - 1);
                strDecPart = Numbers.ToString().Substring(intDecPos + 1, 2);
                if (strDecPart.Length == 1)
                    strDecPart = strDecPart + "0";
            }
            else
                strIntPart = Numbers.ToString().Substring(Numbers.ToString().Length);

            if ((WhichCurrency == eCurrencyType.Rupees | WhichCurrency == eCurrencyType.Dollars) & blnInMillion == false)
            {
                if (strIntPart != "" && Convert.ToDouble(strIntPart) != 0)
                {
                    strNumerals = NumToWordInRupees(Convert.ToDouble(strIntPart));
                    if (strNumerals == "Error")
                    {
                    }
                    else
                        // strNumerals = "Rupees " & NumToWordInRupees(Val(strIntPart))
                        strNumerals = NumToWordInRupees(Convert.ToDouble(strIntPart));
                }

                if (strDecPart != "" && Convert.ToDouble(strDecPart) != 0)
                {
                    if (strDecimals == "Error")
                    {
                    }
                    else if (WhichCurrency == eCurrencyType.Rupees)
                        strDecimals = NumToWordInRupees(Convert.ToDouble(strDecPart)) + " Paise";
                    else
                        strDecimals = NumToWordInDollars(Convert.ToDouble(strDecPart)) + " Cents";
                }

                if (strNumerals != "" & strDecimals != "")
                    strAnd = " And ";

                if (strNumerals == "" & strDecimals == "" & strAnd == "")
                    getNumbersInWords = "Rupees " + " Zero ";
                else if (strNumerals == "Error" | strDecimals == "Error")
                    getNumbersInWords = "Invalid Number";
                else
                    getNumbersInWords = strNumerals + strAnd + strDecimals + " Only.";
            }
            else if (WhichCurrency == eCurrencyType.Dollars & blnInMillion == true)
            {
                if (Convert.ToDouble(strIntPart) != 0)
                {
                    strNumerals = NumToWordInDollars(Convert.ToDouble(strIntPart));
                    if (strNumerals == "Error")
                    {
                    }
                    else
                        // strNumerals = "Dollars " & NumToWordInDollars(Val(strIntPart))
                        strNumerals = NumToWordInDollars(Convert.ToDouble(strIntPart));
                }

                if (Convert.ToDouble(strDecPart) != 0)
                {
                    if (strDecimals == "Error")
                    {
                    }
                    else
                        strDecimals = NumToWordInDollars(Convert.ToDouble(strDecPart)) + " Cents";
                }

                if (strNumerals != "" & strDecimals != "")
                    strAnd = " And ";

                if (strNumerals == "" & strDecimals == "" & strAnd == "")
                    getNumbersInWords = "Dollars " + " Zero ";
                else if (strNumerals == "Error" | strDecimals == "Error")
                    getNumbersInWords = "Invalid Number";
                else
                    getNumbersInWords = strNumerals + strAnd + strDecimals + " Only.";
            }

            return getNumbersInWords;
        }
        private string NumToWordInRupees(double numstr)
        {
            string NumToWordInRupee = "";
            string tempStr = "";
            string newstr = "";

            if (numstr == 0)
            {
                return "Zero";
            }

            if (numstr < 0)
            {
                return "Error";
            }

            if (numstr >= Math.Pow(10, 11))
            {
                return "Error";
            }

            if (numstr >= Math.Pow(10, 7))
            {
                newstr = NumToWordInRupees((numstr / Math.Pow(10, 7)));
                numstr = Math.Round(((numstr / Math.Pow(10, 7)) - (numstr / Math.Pow(10, 7))) * Math.Pow(10, 7), 0);
                if (newstr.Trim() == "One")
                    tempStr = tempStr + newstr + "Crore ";
                else
                    tempStr = tempStr + newstr + "Crores, ";
            }

            if (numstr >= Math.Pow(10, 5))
            {
                newstr = NumToWordInRupees((numstr / Math.Pow(10, 5)));
                numstr = Math.Round(((numstr / Math.Pow(10, 5)) - (numstr / Math.Pow(10, 5))) * Math.Pow(10, 5), 0);
                if (newstr.Trim() == "One")
                    tempStr = tempStr + newstr + "Lakh ";
                else
                    tempStr = tempStr + newstr + "Lakhs, ";
            }

            if (numstr >= Math.Pow(10, 3))
            {
                newstr = NumToWordInRupees((numstr / Math.Pow(10, 3)));
                numstr = Math.Round(((numstr / Math.Pow(10, 3)) - (numstr / Math.Pow(10, 3))) * Math.Pow(10, 3), 0);
                if (numstr == 0)
                    tempStr = tempStr + newstr + "Thousand ";
                else
                    tempStr = tempStr + newstr + "Thousand ";
            }

            if (numstr >= Math.Pow(10, 2))
            {
                newstr = NumToWordInRupees((numstr / Math.Pow(10, 2)));
                numstr = Math.Round(((numstr / Math.Pow(10, 2)) - (numstr / Math.Pow(10, 2))) * Math.Pow(10, 2), 0);
                if (numstr == 0)
                    tempStr = tempStr + newstr + "Hundred ";
                else
                    // tempstr = tempstr & newstr & "Hundred And "
                    tempStr = tempStr + newstr + "Hundred ";
            }

            if (numstr >= 20)
            {
                switch (Convert.ToInt32(numstr / (double)10))
                {
                    case 2:
                        {
                            tempStr = tempStr + "Twenty ";
                            break;
                        }

                    case 3:
                        {
                            tempStr = tempStr + "Thirty ";
                            break;
                        }

                    case 4:
                        {
                            tempStr = tempStr + "Forty ";
                            break;
                        }

                    case 5:
                        {
                            tempStr = tempStr + "Fifty ";
                            break;
                        }

                    case 6:
                        {
                            tempStr = tempStr + "Sixty ";
                            break;
                        }

                    case 7:
                        {
                            tempStr = tempStr + "Seventy ";
                            break;
                        }

                    case 8:
                        {
                            tempStr = tempStr + "Eighty ";
                            break;
                        }

                    case 9:
                        {
                            tempStr = tempStr + "Ninety ";
                            break;
                        }
                }
                numstr = ((numstr / (double)10) - (numstr / (double)10)) * 10;
            }

            if (numstr > 0)
            {
                switch (numstr)
                {
                    case 1:
                        {
                            tempStr = tempStr + "One ";
                            break;
                        }

                    case 2:
                        {
                            tempStr = tempStr + "Two ";
                            break;
                        }

                    case 3:
                        {
                            tempStr = tempStr + "Three ";
                            break;
                        }

                    case 4:
                        {
                            tempStr = tempStr + "Four ";
                            break;
                        }

                    case 5:
                        {
                            tempStr = tempStr + "Five ";
                            break;
                        }

                    case 6:
                        {
                            tempStr = tempStr + "Six ";
                            break;
                        }

                    case 7:
                        {
                            tempStr = tempStr + "Seven ";
                            break;
                        }

                    case 8:
                        {
                            tempStr = tempStr + "Eight ";
                            break;
                        }

                    case 9:
                        {
                            tempStr = tempStr + "Nine ";
                            break;
                        }

                    case 10:
                        {
                            tempStr = tempStr + "Ten ";
                            break;
                        }

                    case 11:
                        {
                            tempStr = tempStr + "Eleven ";
                            break;
                        }

                    case 12:
                        {
                            tempStr = tempStr + "Twelve ";
                            break;
                        }

                    case 13:
                        {
                            tempStr = tempStr + "Thirteen ";
                            break;
                        }

                    case 14:
                        {
                            tempStr = tempStr + "Fourteen ";
                            break;
                        }

                    case 15:
                        {
                            tempStr = tempStr + "Fifteen ";
                            break;
                        }

                    case 16:
                        {
                            tempStr = tempStr + "Sixteen ";
                            break;
                        }

                    case 17:
                        {
                            tempStr = tempStr + "Seventeen ";
                            break;
                        }

                    case 18:
                        {
                            tempStr = tempStr + "Eighteen ";
                            break;
                        }

                    case 19:
                        {
                            tempStr = tempStr + "Nineteen ";
                            break;
                        }
                }
                // tempStr = tempStr & "Rupees"
                numstr = ((numstr / (double)10) - (numstr / (double)10)) * 10;
            }

            NumToWordInRupee = tempStr;
            return NumToWordInRupee;
        }

        private string NumToWordInDollars(double numstr)
        {
            string NumToWordInDollar = "";
            string tempStr = "";
            string newstr;

            if (numstr == 0)
            {
                return "Zero";
            }

            if (numstr < 0)
            {
                return "Error";
            }

            if (numstr >= Math.Pow(10, 11))
            {
                return "Error";
            }

            if (numstr >= Math.Pow(10, 9))
            {
                newstr = NumToWordInDollars((numstr / Math.Pow(10, 9)));
                numstr = ((numstr / Math.Pow(10, 9)) - (numstr / Math.Pow(10, 9))) * Math.Pow(10, 9);
                if (newstr.Trim() == "One")
                    tempStr = tempStr + newstr + "Billion ";
                else
                    tempStr = tempStr + newstr + "Billion, ";
            }

            if (numstr >= Math.Pow(10, 6))
            {
                newstr = NumToWordInDollars((numstr / Math.Pow(10, 6)));
                numstr = ((numstr / Math.Pow(10, 6)) - (numstr / Math.Pow(10, 6))) * Math.Pow(10, 6);
                if (newstr.Trim() == "One")
                    tempStr = tempStr + newstr + "Million ";
                else
                    tempStr = tempStr + newstr + "Million, ";
            }

            if (numstr >= Math.Pow(10, 3))
            {
                newstr = NumToWordInDollars((numstr / Math.Pow(10, 3)));
                numstr = ((numstr / Math.Pow(10, 3)) - (numstr / Math.Pow(10, 3))) * Math.Pow(10, 3);
                if (numstr == 0)
                    tempStr = tempStr + newstr + "Thousand ";
                else
                    tempStr = tempStr + newstr + "Thousand, ";
            }

            if (numstr >= Math.Pow(10, 2))
            {
                newstr = NumToWordInDollars((numstr / Math.Pow(10, 2)));
                numstr = ((numstr / Math.Pow(10, 2)) - (numstr / Math.Pow(10, 2))) * Math.Pow(10, 2);
                if (numstr == 0)
                    tempStr = tempStr + newstr + "Hundred ";
                else
                    // tempstr = tempstr & newstr & "Hundred And "
                    tempStr = tempStr + newstr + "Hundred ";
            }

            if (numstr >= 20)
            {
                switch (numstr / (double)10)
                {
                    case 2:
                        {
                            tempStr = tempStr + "Twenty ";
                            break;
                        }

                    case 3:
                        {
                            tempStr = tempStr + "Thirty ";
                            break;
                        }

                    case 4:
                        {
                            tempStr = tempStr + "Forty ";
                            break;
                        }

                    case 5:
                        {
                            tempStr = tempStr + "Fifty ";
                            break;
                        }

                    case 6:
                        {
                            tempStr = tempStr + "Sixty ";
                            break;
                        }

                    case 7:
                        {
                            tempStr = tempStr + "Seventy ";
                            break;
                        }

                    case 8:
                        {
                            tempStr = tempStr + "Eighty ";
                            break;
                        }

                    case 9:
                        {
                            tempStr = tempStr + "Ninety ";
                            break;
                        }
                }
                numstr = ((numstr / (double)10) - (numstr / (double)10)) * 10;
            }

            if (numstr > 0)
            {
                switch (numstr)
                {
                    case 1:
                        {
                            tempStr = tempStr + "One ";
                            break;
                        }

                    case 2:
                        {
                            tempStr = tempStr + "Two ";
                            break;
                        }

                    case 3:
                        {
                            tempStr = tempStr + "Three ";
                            break;
                        }

                    case 4:
                        {
                            tempStr = tempStr + "Four ";
                            break;
                        }

                    case 5:
                        {
                            tempStr = tempStr + "Five ";
                            break;
                        }

                    case 6:
                        {
                            tempStr = tempStr + "Six ";
                            break;
                        }

                    case 7:
                        {
                            tempStr = tempStr + "Seven ";
                            break;
                        }

                    case 8:
                        {
                            tempStr = tempStr + "Eight ";
                            break;
                        }

                    case 9:
                        {
                            tempStr = tempStr + "Nine ";
                            break;
                        }

                    case 10:
                        {
                            tempStr = tempStr + "Ten ";
                            break;
                        }

                    case 11:
                        {
                            tempStr = tempStr + "Eleven ";
                            break;
                        }

                    case 12:
                        {
                            tempStr = tempStr + "Twelve ";
                            break;
                        }

                    case 13:
                        {
                            tempStr = tempStr + "Thirteen ";
                            break;
                        }

                    case 14:
                        {
                            tempStr = tempStr + "Fourteen ";
                            break;
                        }

                    case 15:
                        {
                            tempStr = tempStr + "Fifteen ";
                            break;
                        }

                    case 16:
                        {
                            tempStr = tempStr + "Sixteen ";
                            break;
                        }

                    case 17:
                        {
                            tempStr = tempStr + "Seventeen ";
                            break;
                        }

                    case 18:
                        {
                            tempStr = tempStr + "Eighteen ";
                            break;
                        }

                    case 19:
                        {
                            tempStr = tempStr + "Nineteen ";
                            break;
                        }
                }
                numstr = ((numstr / (double)10) - (numstr / (double)10)) * 10;
            }

            NumToWordInDollar = tempStr;
            return NumToWordInDollar;
        }
        public void subLockForm(bool EnaDis, ArrayList objCtl, bool GridColor = false)
        {
            short sIndex;
            for (sIndex = 0; sIndex <= objCtl.Count - 1; sIndex++)
            {
                if (objCtl[sIndex].ToString() == "TextBox")
                {
                    ((System.Windows.Forms.TextBox)objCtl[sIndex]).Enabled = !EnaDis;
                    // CType(objCtl(sIndex), TextBox).ReadOnly = EnaDis
                    ((System.Windows.Forms.TextBox)objCtl[sIndex]).BackColor = System.Drawing.Color.White;
                }
                else if (objCtl[sIndex].ToString() == "ComboBox")
                {
                    ((System.Windows.Forms.ComboBox)objCtl[sIndex]).Enabled = !EnaDis;
                    ((System.Windows.Forms.ComboBox)objCtl[sIndex]).BackColor = System.Drawing.Color.White;
                }
                else if (objCtl[sIndex].ToString() == "CheckBox")
                    ((CheckBox)objCtl[sIndex]).Enabled = !EnaDis;
                else if (objCtl[sIndex].ToString() == "RadioButton")
                    ((RadioButton)objCtl[sIndex]).Enabled = !EnaDis;
                else if (objCtl[sIndex].ToString() == "DateTimePicker")
                    ((DateTimePicker)objCtl[sIndex]).Enabled = !EnaDis;
                else if (objCtl[sIndex].ToString() == "NumericUpDown")
                    ((NumericUpDown)objCtl[sIndex]).Enabled = !EnaDis;
            }
        }
        public ArrayList setControlsonForm(Form objContainer, ArrayList arrList, ArrayList buttonList)
        {
            //Control Ctl;
            foreach (Control Ctl in objContainer.Controls)
            {
                if (Information.TypeName(Ctl) != "Label")
                {
                    //if (Ctl.HasChildren() & TypeName(Ctl) != "FpSpread")
                    //    setControlsonForm(ref Ctl, ref arrList, ref buttonList);
                    //else 
                    if (Information.TypeName(Ctl) == "Button")
                        buttonList.Add(Ctl);
                    else if (Information.TypeName(objContainer) == "NumericUpDown")
                        arrList.Add(objContainer);
                    else
                        arrList.Add(Ctl);
                }
            }
            return arrList;
        }
        public System.Data.DataTable GetUserTotalAction(int intUserId = 0, long lngScreenId = 0, string strUserName = "", long LngActionId = 0)
        {
            System.Data.DataTable RstUserTotalAction = null;

            try
            {
                SqlCommand command = new SqlCommand("SP_GetUserTotalAction", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UserId", intUserId);
                command.Parameters.AddWithValue("@ScreenId", lngScreenId);
                command.Parameters.AddWithValue("@ActionId", LngActionId);
                RstUserTotalAction = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                lngErrNum = -23904;
                // Handle or log the exception
            }

            return RstUserTotalAction;
        }

        public bool SetUserScreenActions(Form Form, long Login_ID, long ScreenID, ArrayList frmCtl = null, Hashtable objHash = null, bool mBlnEdit = false, bool blnRBkDataEny = false)
        {
            try
            {
                ResourceManager myManager = new ResourceManager("SGMOSOL.ResourceMain", Form.GetType().Assembly);
                Form.Location = new System.Drawing.Point(0, 0);
                Form.KeyPreview = true;
                System.Drawing.Icon myIcon;
                myIcon = (System.Drawing.Icon)myManager.GetObject("gajanan-icon-iso-format");
                Form.Icon = myIcon;


                bool blnRecordFound;
                System.Data.DataTable rstAction;
                Form MyFrom;
                MyFrom = Form;
                rstAction = GetUserTotalAction(System.Convert.ToInt32(Login_ID), ScreenID);
                blnRecordFound = false;
                objHash = new Hashtable();
                objHash.Add("ViewButton", false);
                objHash.Add("PrintButton", true);
                objHash.Add("ExportButton", false);

                //while (rstAction.Read())
                foreach (DataRow dr in rstAction.Rows)
                {
                    blnRecordFound = true;
                    switch (dr["ActionId"])
                    {
                        case 1:
                            {
                                // New
                                SetAction("BTNNEW", MyFrom, frmCtl);
                                break;
                            }

                        case 9:
                            {
                                // Find
                                SetAction("BTNSEARCH", MyFrom, frmCtl);
                                break;
                            }

                        case 2:
                            {
                                // Save
                                SetAction("BTNSAVE", MyFrom, frmCtl);
                                break;
                            }

                        case 5:
                            {
                                // Index
                                SetAction("BTNINDEX", MyFrom, frmCtl);
                                break;
                            }

                        case 7:
                            {
                                // View
                                objHash["ViewButton"] = true;
                                SetAction("BTNVIEW", MyFrom, frmCtl);
                                break;
                            }

                        case 8:
                            {
                                // Print
                                objHash["PrintButton"] = true;
                                SetAction("BTNPRINT", MyFrom, frmCtl);
                                break;
                            }

                        //case 9:
                        //    {
                        //        // Transfer
                        //        SetAction("BTNMOVE", MyFrom, frmCtl);
                        //        break;
                        //    }

                        case 11:
                            {
                                blnRBkDataEny = true;
                                break;
                            }
                    }
                }
                //rstAction.Close();
                if (blnRecordFound == false)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                SetError("SetUserScreenActions : " + ex.ToString());
                return false;
            }
        }
        private void SetError(string str)
        {
            InsertErrorLog(str, UserInfo.module, UserInfo.version);
            clsConnection.mErrorResult = clsConnection.mErrorResult + Constants.vbNewLine + str;
        }
        private void SetAction(string caption, Form MyFrom, ArrayList frmCtl = null)
        {
            foreach (Control C in MyFrom.Controls)
            {
                if (C is System.Windows.Forms.Button)
                {
                    if (Strings.UCase(C.Name) == Strings.UCase(caption))
                    {
                        C.Enabled = true;
                        break;
                    }
                }
            }
            if (frmCtl is null)
                return;
            Int16 loopInt;
            for (loopInt = 0; loopInt <= frmCtl.Count - 1; loopInt++)
            {
                if (frmCtl[loopInt] is System.Windows.Forms.Button)
                {
                    if (Strings.UCase(((Control)frmCtl[loopInt]).Name) == Strings.UCase(caption))
                    {
                        ((Control)frmCtl[loopInt]).Enabled = true;
                        break;
                    }
                }
            }
        }
        public static string DateDiff(DateInterval Type, DateTime FromDate, DateTime ToDate)
        {
            string DateDiff = "";
            if (Type == DateInterval.Day)
            {
                DateDiff = (ToDate - FromDate).Days.ToString();
            }
            return DateDiff;
        }
        public void FillCombo(System.Windows.Forms.ComboBox cbo, DataTable myReader, string cboAttribute, string cboIndex = "", string Name2 = "", string Name3 = "")
        {
            try
            {
                cbo.BeginUpdate();
                cbo.Items.Clear();
                {
                    var withBlock = myReader;
                    int i = 0;
                    //while (myReader.Read())
                    foreach (DataRow myReaderitem in myReader.Rows)
                    {
                        if (cboIndex == "")
                            cbo.Items.Add(withBlock.Rows[i][cboAttribute]);
                        else if (Name2 == "")
                            cbo.Items.Add(new clsItemData(withBlock.Rows[i][cboAttribute].ToString(), Convert.ToInt32(myReaderitem[cboIndex])));
                        else if (Name3 == "")
                            cbo.Items.Add(new clsItemData(withBlock.Rows[i][cboAttribute].ToString(), Convert.ToInt32(myReaderitem[cboIndex]), Convert.ToInt64(myReaderitem[Name2])));
                        else
                            cbo.Items.Add(new clsItemData(withBlock.Rows[i][cboAttribute].ToString(), Convert.ToInt32(myReaderitem[cboIndex]), Convert.ToInt64(myReaderitem[Name2]), myReaderitem[Name3].ToString()));
                        i++;
                    }
                }
                cbo.EndUpdate();
                //myReader.Close();
            }
            catch (Exception ex)
            {
                //if (myReader != null)
                //    myReader.Close();
                SetError("FillCombo : " + cbo.Name + Constants.vbCrLf + ex.ToString());
            }
        }
        public long cmbItemdata(System.Windows.Forms.ComboBox cbo, int intIndex)
        {
            try
            {
                clsItemData mItemData;
                if (intIndex == -1)
                {
                    return 0;
                }
                mItemData = (clsItemData)cbo.Items[intIndex];
                return mItemData.ItemData;
            }
            catch (Exception ex)
            {
                SetError("cmbItemdata : " + cbo.Name + Constants.vbCrLf + ex.ToString());
                return 0;
            }
        }

        public string cmbItemName(System.Windows.Forms.ComboBox cbo, int intIndex)
        {
            try
            {
                clsItemData mItemData;
                if (intIndex == -1)
                {
                    return "";
                }
                mItemData = (clsItemData)cbo.Items[intIndex];
                return mItemData.Name;
            }
            catch (Exception ex)
            {
                SetError("cmbItemdata2 : " + cbo.Name + Constants.vbCrLf + ex.ToString());
                return null;
            }
        }
        public long cmbItemName2(System.Windows.Forms.ComboBox cbo, int intIndex)
        {
            try
            {
                clsItemData mItemData;
                mItemData = (clsItemData)cbo.Items[intIndex];
                return mItemData.ItemName2;
            }
            catch (Exception ex)
            {
                SetError("cmbItemdata2 : " + cbo.Name + Constants.vbCrLf + ex.ToString());
                return 0;
            }
        }


        // Get Itemdata of ComboBox. Parameters : ComboBox, Selected Index 
        public long cmbItemName3(System.Windows.Forms.ComboBox cbo, int intIndex)
        {
            try
            {
                clsItemData mItemData;
                mItemData = (clsItemData)cbo.Items[intIndex];
                return Convert.ToInt64(mItemData.ItemName3);
            }
            catch (Exception ex)
            {
                SetError("cmbItemdata3 : " + cbo.Name + Constants.vbCrLf + ex.ToString());
                return 0;
            }
        }
        public System.Data.DataSet GetDsPaymentType2(int[] Typet)
        {
            System.Data.DataSet ds = new System.Data.DataSet();

            try
            {
                SqlCommand command = new SqlCommand("SP_GetDsPaymentType2", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                // Create DataTable for Typet array
                DataTable dtTypet = new DataTable();
                dtTypet.Columns.Add("value", typeof(int));
                foreach (int value in Typet)
                {
                    dtTypet.Rows.Add(value);
                }

                // Add parameter for the array
                SqlParameter parameter = command.Parameters.AddWithValue("@TypetTable", dtTypet);
                parameter.SqlDbType = SqlDbType.Structured;
                parameter.TypeName = "dbo.IntArrayType";

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                lngErrNum = -91;
                // Handle or log the exception
            }

            return ds;
        }
        public void FillComboWithDataSet(System.Windows.Forms.ComboBox cbo, DataTable objDTable, string SortOrderField, string cboAttribute, string cboIndex = "", string Name2 = "", string strFilter = "")
        {
            try
            {
                DataView objDataView;
                if (strFilter != "")
                    objDataView = new DataView(objDTable, strFilter, SortOrderField, DataViewRowState.CurrentRows);
                else
                {
                    objDataView = new DataView(objDTable);
                    objDataView.Sort = SortOrderField;
                }
                cbo.BeginUpdate();
                cbo.Items.Clear();
                foreach (DataRowView dvRow in objDataView)
                {
                    if (cboIndex == "")
                        cbo.Items.Add(dvRow[cboAttribute]);
                    else if (Name2 == "")
                        cbo.Items.Add(new clsItemData(dvRow[cboAttribute].ToString(), Convert.ToInt32(dvRow[cboIndex])));
                    else
                        cbo.Items.Add(new clsItemData(dvRow[cboAttribute].ToString(), Convert.ToInt32(dvRow[cboIndex]), Convert.ToInt64(dvRow[Name2])));
                }
                cbo.EndUpdate();
            }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }
        public System.Data.DataTable GetDrSublocation(int ModType, Int64 Location_ID = 0)
        {
            System.Data.DataTable mReader = null;

            try
            {
                SqlCommand command = new SqlCommand("SP_GetDrSublocation", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.AddWithValue("@ModType", ModType);
                command.Parameters.AddWithValue("@LocationID", Location_ID);

                mReader = clsConnection.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                // Handle or log the exception
            }

            return mReader;
        }
        public System.Data.DataSet GetDsProductMenu(int intUserId = 0)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetProductMenu", clsConnection.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@intUserId", intUserId);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return ds;
        }
        public void FillComboWithDataSet(System.Windows.Forms.ListBox cbo, DataTable objDTable, string SortOrderField, string cboAttribute, string cboIndex = "", string Name2 = "", string strFilter = "")
        {
            try
            {
                DataView objDataView;

                if (strFilter != "")
                    objDataView = new DataView(objDTable, strFilter, SortOrderField, DataViewRowState.CurrentRows);
                else
                {
                    objDataView = new DataView(objDTable);
                    objDataView.Sort = SortOrderField;
                }
                cbo.BeginUpdate();
                cbo.Items.Clear();
                foreach (DataRowView dvRow in objDataView)
                {
                    if (cboIndex == "")
                        cbo.Items.Add(dvRow[cboAttribute]);
                    else if (Name2 == "")
                        cbo.Items.Add(new clsItemData(dvRow[cboAttribute].ToString(), Convert.ToInt32(dvRow[cboIndex])));
                    else
                        cbo.Items.Add(new clsItemData(dvRow[cboAttribute].ToString(), Convert.ToInt32(dvRow[cboIndex]), Convert.ToInt64(dvRow[Name2])));
                }
                cbo.EndUpdate();
            }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }
        public string GetComSysPrintTimeRpt(string strUserName = "")
        {
            string sysTime = string.Empty;
            try
            {
                SqlCommand command = new SqlCommand("SP_GetSysTime", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                DataTable dr = clsConnection.ExecuteReader(command);
                if (dr.Rows.Count > 0)
                {
                    sysTime = dr.Rows[0]["SysTime"].ToString();
                }
            }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return sysTime;
        }
        public static decimal getBedCheckInMaxAmount()
        {
            try
            {
                SqlCommand command = new SqlCommand("SP_getBedCheckInMaxAmount", clsConnection.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                DataTable dr = clsConnection.ExecuteReader(command);
                if (dr.Rows.Count > 0)
                {
                    return Convert.ToDecimal(dr.Rows[0]["Max_Amount"]);
                }
            }
            catch (Exception ex)
            {
                clsConnection.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            return 0;
        }
       
    }
}


