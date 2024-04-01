using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGMOSOL.ADMIN
{
    public static class UserInfo
    {
        private static int userId;
        private static string userName;
        private static string machine_id;
        private static int Location_id;
        private static int Company_ID;
        private static int dept_id;
        private static string CounterName;
        private static int ctr_mach_id;
        private static string MachineName;
        private static int FY_ID;
        private static DateTime FYstartdate;
        private static DateTime FYenddate;
        private static string Module;
        private static string Version;
        private static string ServerName;
        private static string reportpath;
        private static decimal bedCheckInMaxAmount;

        public static int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        public static string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public static string MachineId
        {
            get { return machine_id; }
            set { machine_id = value; }
        }
        public static int Loc_id
        {
            get { return Location_id; }
            set { Location_id = value; }
        }
        public static int CompanyID
        {
            get { return Company_ID; }
            set { Company_ID = value; }
        }
        public static int Dept_id
        {
            get
            {
                return dept_id;
            }
            set { dept_id = value; }
        }
        public static string Counter_Name
        {
            get { return CounterName; }
            set { CounterName = value; }
        }
        public static int ctrMachID
        {
            get { return ctr_mach_id; }
            set { ctr_mach_id = value; }
        }
        public static string Machine_Name
        {
            get { return MachineName; }
            set { MachineName = System.Environment.MachineName; }
        }
        public static int fy_id
        {
            get { return FY_ID; }
            set { FY_ID = value; }
        }
        public static DateTime FYStartDate
        {
            get { return FYstartdate; }
            set { FYstartdate = value; }
        }
        public static DateTime FYEndDate
        {
            get { return FYenddate; }
            set { FYenddate = value; }
        }
        public static string module
        {
            get { return Module; }
            set { Module = value; }
        }
        public static string version
        {
            get { return Version; }
            set { Version = value; }
        }
        public static string serverName
        {
            get { return ServerName; }
            set { ServerName = value; }
        }
        public static string ReportPath
        {
            get { return reportpath; }
            set { reportpath = value; }
        }
        public static decimal BedCheckInMaxAmount
        {
            get { return bedCheckInMaxAmount; }
            set { bedCheckInMaxAmount = value; }
        }
    }
}
