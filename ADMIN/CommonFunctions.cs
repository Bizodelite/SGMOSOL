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



namespace SGMOSOL.ADMIN
{
    public class CommonFunctions
    {
        public string SystemHDDModelNo;
        public string SystemHDDSerialNo;
        public string SystemMacID;
        public string DBServerName;
        private byte[] key = new byte[0];
        private byte[] IV = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        enum CurrencyType
        {
            Rupees,
            Dollars
        }

        string connectionString = Decrypt(ConfigurationManager.ConnectionStrings["strConnection"].ConnectionString, true);

        DataTable dt = null;
        public DataTable getCountry()
        {
            dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = " SELECT CountryID, CountryName FROM Countries";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
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
        public DataTable GetDocumentType()
        {
            dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "select 0 as DocumentID,'Select'as DocumentName union all select Lookup_Value_Id as DocumentID,Lookup_Value_Name as DocumentName from COM_LOOKUP_VALUES_MST_V where Lookup_Name = 'Document Type'";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
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
        public DataTable getStateById(int countryId)
        {
            dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = " SELECT StateId, StateName FROM States where CountryId=" + countryId + "";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
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
        public DataTable fillTID()
        {
            dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "select tidNo,Tid from COM_TID_MST_T where counter_id=" + UserInfo.ctrMachID + "";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
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
        public DataTable getDisctrictbyStateId(int stateId)
        {
            dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "   select DistrictId, DistrictName from Districts where StateId=" + stateId + "";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
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
        public DataTable getPaymentMode()
        {
            dt = new DataTable();
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
                    stringBuilder.Append("  left join PAYMENT_TYPE_MST on PAYMENT_TYPE=Token_Detail_Id ");
                    stringBuilder.Append(" where Token_Mst_Id = 6 and MODULE_ID=1 and STATUS=1 ");
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
            dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append(" select mst.DENGI_MST_ID,");
                    stringBuilder.Append("mst.TYPE,isnull(mst.TYPE_M,'')");
                    stringBuilder.Append(" from DEN_DENGI_MASTER_T mst where DENGI_STATUS_ID=3");
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
        public DataTable getGotra()
        {
            dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("SELECT ISNULL(gotra_id,0) AS gotra_id");
                    stringBuilder.Append(",isnull(gotra_name,'') AS gotra_name");
                    stringBuilder.Append(" from tbl_gotra_master ");
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
                // Check if the parsed DateTime is within the valid range for SQL Server's datetime type
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
            return sb.ToString();
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

        public DataTable isMachineAccess(int uid, int ctr_mach_id)
        {
            dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "select * from SEC_USER_COUNTER_MST_T where USER_ID=" + uid + " and CTR_MACH_ID=" + ctr_mach_id + "";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
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
        public DataTable getUserAllDetails(string strMachineId, int uid = 0)
        {
            dt = new DataTable();
            try
            {
                string query;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    if (uid != 0)
                    {
                        query = "select * from SEC_USER_COUNTER_MST_V where MACHINE_ID='" + strMachineId + "' and User_Id=" + uid + "";
                    }
                    else
                    {
                        query = "select top 1 * from SEC_USER_COUNTER_MST_V where MACHINE_ID='" + strMachineId + "'";
                    }
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
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
        public int getFYID()
        {
            int fyId = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "select  FINANCIAL_YEAR_ID  From COM_FINYR_TO_COMP_MST_V  Where COMPANY_ID=9 and CURRENT_FINANCIAL_YEAR_STATUS ='Y'";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        fyId = Convert.ToInt32(result);
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);

            }
            return fyId;
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
        //public string Encrypt(string encryptval)
        //{
        //    try
        //    {
        //        key = System.Text.Encoding.UTF8.GetBytes("RSNGBP");
        //        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        //        byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptval);
        //        MemoryStream ms = new MemoryStream();
        //        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
        //        cs.Write(inputByteArray, 0, inputByteArray.Length);
        //        cs.FlushFinalBlock();
        //        return Convert.ToBase64String(ms.ToArray());
        //    }
        //    catch (Exception ex)
        //    {
        //        InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
        //        return "-1";
        //    }
        //}
        //public string Decrypt(string Outputval)
        //{
        //    try
        //    {
        //        var strfirsttwo = Outputval.Substring(0, 2);
        //        var RemainStr = Outputval.Remove(0, 2);
        //        byte[] inputByteArray = new byte[RemainStr.Length + 1];
        //        // Dim inputByteArray(keyval.Length) As Byte
        //        string keyvald = string.Concat("RSNGBP", "", strfirsttwo);
        //        key = System.Text.Encoding.UTF8.GetBytes(keyvald);
        //        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        //        inputByteArray = Convert.FromBase64String(RemainStr);
        //        MemoryStream ms = new MemoryStream();
        //        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
        //        cs.Write(inputByteArray, 0, inputByteArray.Length);
        //        cs.FlushFinalBlock();
        //        System.Text.Encoding encoding = System.Text.Encoding.UTF8;
        //        return encoding.GetString(ms.ToArray());
        //    }
        //    catch (Exception ex)
        //    {
        //        InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
        //        return "-1";
        //    }
        //}
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
            dt = new DataTable();
            try
            {
                string query;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    query = "SELECT Lookup_Value_Name,Lookup_Value_Order FROM COM_LOOKUP_VALUES_MST_V WHERE Lookup_Id=8";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
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
    }
}


