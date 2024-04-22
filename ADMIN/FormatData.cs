using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGMOSOL.ADMIN
{
    static class FormatData
    {
        public static string FormatCharData(string strInput)
        {
            if (strInput == Constants.vbNullString)
                return "NULL,";
            else
                return "'" + Strings.Replace(strInput, "'", "''") + "'";
        }
        public static string FormatCharDataM(string strInput)
        {
            if (strInput == Constants.vbNullString)
                return "NULL,";
            else
                return "N'" + Strings.Replace(strInput, "'", "''") + "',";
        }

        public static string FormatDateData(string strInput)
        {
            if (strInput == Constants.vbNullString)
                return "NULL,";
            else
                // FormatDateData = "TO_DATE('" & strInput & "','DD/MM/YYYY')" & ","
                return "Convert(DateTime,'" + strInput + "',103),";
        }
        public static string FormatDateDataForQuery(string strInput)
        {
            if (strInput == Constants.vbNullString)
                return "NULL,";
            else
                // FormatDateData = "TO_DATE('" & strInput & "','DD/MM/YYYY')" & ","
                return "Convert(DateTime,'" + strInput + "',103)";
        }
        public static string FormatDateDataForQuery1(string strInput)
        {
            if (strInput == Constants.vbNullString)
                return "NULL,";
            else
                return "TO_DATE('" + strInput + "','DD/MM/YYYY')" + ",";
        }

        public static string FormatTimeData(string strInput)
        {
            if (strInput == Constants.vbNullString)
                return "NULL,";
            else
                // FormatDateData = "TO_DATE('" & strInput & "','DD/MM/YYYY')" & ","
                return "Convert(DateTime,'" + strInput + "',108),";
        }

        public static string FormatNumberData(double dblInput)
        {
            if (dblInput == -1)
                return "NULL,";
            else
                return System.Convert.ToString(dblInput) + ",";
        }

        public static string FormatNumberDataForZero(double dblInput)
        {
            if (dblInput == 0)
                return "NULL,";
            else
                return System.Convert.ToString(dblInput) + ",";
        }

        public static string FormatNumberDataActual(double dblInput)
        {
            return System.Convert.ToString(dblInput) + ",";
        }


        public static string fixQuotes(string theString)
        {
            return Strings.Replace(Strings.Trim(theString), "'", "''");
        }

        public static bool IsWildCardThere(string str)
        {
            if (Strings.InStr(str, "%") > 0 | Strings.InStr(str, "*") > 0 | Strings.InStr(str, "_") > 0)
                return true;
            else
                return false;
        }

        public static string ReplaceStarWithPer(string str)
        {
            return Strings.Replace(str, "*", "%");
        }

        public static double RoundIt(double Number, int NumDigitsAfterDecimal = 0, TriState IncludeLeadingDigit = Constants.vbUseDefault, TriState UseParensForNegativeNumbers = TriState.UseDefault, TriState GroupDigit = Constants.vbUseDefault)
        {
            double RoundIt;
            double strNumber = 0;
            try
            {
                if (Conversion.Val(Number) == Conversion.Val(""))
                    Number = 0;
                if (Number.ToString().IndexOf(".") > 0)
                {
                    if (Number < 0)
                        RoundIt = (-0.5) + Number * (Math.Pow(10, NumDigitsAfterDecimal));
                    else
                        RoundIt = 0.5 + Number * (Math.Pow(10, NumDigitsAfterDecimal));

                    if (RoundIt.ToString().IndexOf(".") > 0)
                        strNumber = Conversion.Val(RoundIt.ToString().Substring(1, RoundIt.ToString().IndexOf(".") - 1)) / (Math.Pow(10, NumDigitsAfterDecimal));
                    else
                        strNumber = RoundIt / (Math.Pow(10, NumDigitsAfterDecimal));
                }
                else
                    strNumber = Number;

                RoundIt = Convert.ToDouble(Strings.FormatNumber(strNumber, NumDigitsAfterDecimal, IncludeLeadingDigit, UseParensForNegativeNumbers, GroupDigit));

                if (Conversion.Val(RoundIt) == 0)
                    RoundIt = 0;
            }
            catch (Exception ex)
            {
                // SetError("RounIt : " & ex.ToString)
                RoundIt = 0;
            }
            return RoundIt;
        }

        public static string FormatDateToString(DateTime mDate)
        {
            // FormatDateToString = Microsoft.VisualBasic.Day(mDate) & "/" & Month(mDate) & "/" & Year(mDate)
            return Strings.Format(mDate.Day, "00") + "/" + Strings.Format(mDate.Month, "00") + "/" + Strings.Format(mDate.Year, "0000");
        }

        public static string FormatDateForMaxStringData(string strInput)
        {
            if (strInput == Constants.vbNullString)
                return "NULL,";
            else
                return "Convert(DateTime,SUBSTRING('" + strInput + "',0,11),121),";
        }

        public static string FormatCharDataCNC(string strInput)
        {
            if (strInput == Constants.vbNullString)
                return "NULL";
            else
                return "'" + Strings.Replace(strInput, "'", "''") + "',";
        }


        enum LookupValues
        {
            InwardIssue = 2092
        }
    }

}
