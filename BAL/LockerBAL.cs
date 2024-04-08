using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGMOSOL.BAL
{
    internal class LockerBAL
    {
        public struct LockerCheckInMst
        {
            public long CheckInMstId { get; set; }
            public int ComId { get; set; }
            public int LocId { get; set; }
            public int DeptId { get; set; }
            public long CtrMachId { get; set; }
            public int FyId { get; set; }
            public DateTime InDate { get; set; }
            public DateTime InTime { get; set; }
            public string mob_no { get; set; }
            public long SerialNo { get; set; }
            public Int32 AppNo { get; set; }
            public double ExtRent { get; set; }
            public int ExtDay { get; set; }
            public string ExtDate { get; set; }
            public string Name { get; set; }
            public string Place { get; set; }
            public int Days { get; set; }
            public int NoOfLockers { get; set; }
            public DateTime OutDate { get; set; }
            public DateTime OutTime { get; set; }
            public double Advance { get; set; }
            public double Rent { get; set; }
            public long RecordModifiedCount { get; set; }
            public int UserId { get; set; }
            public string ServerName { get; set; }
            public string EnteredBy { get; set; }
            public string EnteredOn { get; set; }
            public string ModifiedBy { get; set; }
            public string ModifiedOn { get; set; }
            public string MachineName { get; set; }
        }
        public struct LockerCheckInDet
        {
            public long CheckInDetId { get; set; }
            public long CheckInMstId { get; set; }
            public int LockerId { get; set; }
            public Int32 givenlockers { get; set; }
            public Int32 LockerAvailableStatus { get; set; }
            public long LockerRecordModifiedCount { get; set; }
        }
        public struct LockerCheckInPrintModel
        {
            public Int64 CHECK_IN_MST_ID { get; set; }
            public string LOC_SH_NAME { get; set; }
            public string DEPT_SH_NAME { get; set; }
            public string COUNTER { get; set; }
            public DateTime IN_DATE { get; set; }
            public DateTime IN_TIME { get; set; }
            public Int64 SERIAL_NO { get; set; }
            public string NAME { get; set; }
            public string PLACE { get; set; }
            public string MOB_NO { get; set; }
            public int DAYS { get; set; }
            public string NO_OF_LOCKERS { get; set; }
            public DateTime OUT_DATE { get; set; }
            public DateTime OUT_TIME { get; set; }
            public double ADVANCE { get; set; }
            public double RENT { get; set; }
            public string USER_NAME { get; set; }
            public string SERVER_NAME { get; set; }
            public string MACHINE_NAME { get; set; }
            public string AMT_IN_WORDS { get; set; }
            public string LOCKER_NAME { get; set; }
        }
        public struct LockerCheckOutMst
        {
            public long CheckOutMstId { get; set; }
            public int ComId { get; set; }
            public long LocId { get; set; }
            public int DeptId { get; set; }
            public long CtrMachId { get; set; }
            public int FyId { get; set; }
            public DateTime OutDate { get; set; }
            public DateTime OutTime { get; set; }
            public long SerialNo { get; set; }
            public long CheckInMstId { get; set; }
            public int Days { get; set; }
            public int NoOfLockers { get; set; }
            public double Advance { get; set; }
            public double Rent { get; set; }
            public double Refund { get; set; }
            public long RecordModifiedCount { get; set; }
            public int UserId { get; set; }
            public string ServerName { get; set; }
            public string EnteredBy { get; set; }
            public string EnteredOn { get; set; }
            public string ModifiedBy { get; set; }
            public string ModifiedOn { get; set; }
            public string MachineName { get; set; }
        }
        public struct LockerCheckOutDet
        {
            public long CheckOutDetId { get; set; }
            public long CheckOutMstId { get; set; }
            public Int32 LockerId { get; set; }
            public Int32 LockerAvailableStatus { get; set; }
            public long LockerRecordModifiedCount { get; set; }
        }
        public struct LockerChangeMst
        {
            public long CheckInMstId { get; set; }
            public int ComId { get; set; }
            public int LocId { get; set; }
            public int DeptId { get; set; }
            public long CtrMachId { get; set; }
            public int FyId { get; set; }
            public int PrevLkrId { get; set; }
            public string OutDate { get; set; }
            public string OutTime { get; set; }
            public long SerialNo { get; set; }
            public string Reason { get; set; }
            public int UserId { get; set; }
            public string ServerName { get; set; }
            public string EnteredBy { get; set; }
            public string EnteredOn { get; set; }
            public string ModifiedBy { get; set; }
            public string ModifiedOn { get; set; }
            public string MachineName { get; set; }
        }
        public struct DailyVoucherTransaction
        {
            public DateTime fDate { get; set; }
            public DateTime TDate { get; set; }
            public long MinSerialNo { get; set; }
            public long MaxSerialNo { get; set; }
            public double TotalAmount { get; set; }
            public double TotalReceipt { get; set; }
            public string Status { get; set; }
            public string LocationName { get; set; }
            public string ModName { get; set; }
            public string DeptName { get; set; }
            public string CtrName { get; set; }
        }
        public struct MessDayStockAccDet
        {
            public long DayStkAccDetId { get; set; }
            public long DayStkAccMstId { get; set; }
            public long ItemId { get; set; }
            public double DamagedQty { get; set; }
            public double ReturnedQty { get; set; }
        }
        public struct DamagedLockers
        {
            public Int32 LockerId { get; set; }
            public string Reason { get; set; }
            public string sDate { get; set; }
        }

    }
}
