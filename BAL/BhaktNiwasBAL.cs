using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGMOSOL.BAL
{
    internal class BhaktNiwasBAL
    {
        public struct RoomCheckInMst
        {
            public long CheckInMstId { get; set; }
            public int ComId { get; set; }
            public int LocId { get; set; }
            public int DeptId { get; set; }
            public long CtrMachId { get; set; }
            public int FyId { get; set; }
            public Int64 Sublocid { get; set; }
            public string sublocn { get; set; }
            public DateTime InDate { get; set; }
            public DateTime InTime { get; set; }
            public string Image { get; set; }
            public string mob_no { get; set; }
            public Int16 BhaktTypeId { get; set; }
            public string VehicleNo { get; set; }
            public int donerId { get; set; }
            public int DonnerRoomId { get; set; }
            public long SerialNo { get; set; }
            public Int32 AppNo { get; set; }
            public string Name { get; set; }
            public string Place { get; set; }
            public int Days { get; set; }
            public int NoOfRooms { get; set; }
            public int NoOfPersons { get; set; }
            public int AuthPersonId { get; set; }
            public string Remark { get; set; }
            public DateTime OutDate { get; set; }
            public DateTime OutTime { get; set; }
            public decimal Advance { get; set; }
            public decimal Rent { get; set; }
            public decimal ExtRent { get; set; }
            public int ExtDay { get; set; }
            public DateTime ExtDate { get; set; }
            public long RecordModifiedCount { get; set; }
            public int UserId { get; set; }
            public string ServerName { get; set; }
            public string EnteredBy { get; set; }
            public string EnteredOn { get; set; }
            public string ModifiedBy { get; set; }
            public string ModifiedOn { get; set; }
            public string MachineName { get; set; }
            public string ScanDoc { get; set; }
            public string isOnline { get; set; }
            public string BookingID { get; set; }
            public long tid { get; set; }
            public string invoiceno { get; set; }
            public long paymenttype { get; set; }
            public string Barcode { get; set; }
            public Int64 CountryId { get; set; }
            public string Countryname { get; set; }
            public Int64 Stateid { get; set; }
            public string statename { get; set; }
            public Int64 Districtid { get; set; }
            public string DISTRICT { get; set; }
            public string CHQ_BANK_NAME { get; set; }
            public string CHQ_NO { get; set; }
            public DateTime? CHQ_DATE { get; set; }
            // code added 26-07-2023 
            // check for kyc document 
            public string DOC_TYPE { get; set; }
            public string DOC_DETAILS { get; set; }
        }
        public struct RoomCheckInDet
        {
            public long CheckInDetId { get; set; }
            public long CheckInMstId { get; set; }
            public Int32 LockerId { get; set; }
            public Int32 givenlockers { get; set; }
            public Int32 LockerAvailableStatus { get; set; }
            public long LockerRecordModifiedCount { get; set; }
        }
        public struct RoomCheckOutMst
        {
            public long CheckOutMstId { get; set; }
            public int ComId { get; set; }
            public int LocId { get; set; }
            public int DeptId { get; set; }
            public long CtrMachId { get; set; }
            public int FyId { get; set; }
            public DateTime OutDate { get; set; }
            public DateTime OutTime { get; set; }
            public long SerialNo { get; set; }
            public long CheckInMstId { get; set; }
            public int BhaktTypeId { get; set; }
            public int DonerId { get; set; }
            public Int16 DnrAllowedDays { get; set; }
            public Int16 DnrRmnedDays { get; set; }
            public int DnrRoomId { get; set; }
            public int Days { get; set; }
            public int NoOfRooms { get; set; }
            public decimal Advance { get; set; }
            public decimal Rent { get; set; }
            public decimal Refund { get; set; }
            public long RecordModifiedCount { get; set; }
            public int UserId { get; set; }
            public string ServerName { get; set; }
            public string EnteredBy { get; set; }
            public string EnteredOn { get; set; }
            public string ModifiedBy { get; set; }
            public string ModifiedOn { get; set; }
            public string MachineName { get; set; }
        }
        public struct RoomCheckOutDet
        {
            public long CheckOutDetId { get; set; }
            public long CheckOutMstId { get; set; }
            public Int32 LockerId { get; set; }
            public Int32 LockerAvailableStatus { get; set; }
            public long LockerRecordModifiedCount { get; set; }
        }
        public struct RoomChangeMst
        {
            public long CheckInMstId { get; set; }
            public int ComId { get; set; }
            public int LocId { get; set; }
            public int DeptId { get; set; }
            public long CtrMachId { get; set; }
            public int FyId { get; set; }
            public int PrevLkrId { get; set; }
            public DateTime OutDate { get; set; }
            public DateTime OutTime { get; set; }
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
        public struct DamagedRooms
        {
            public Int32 LockerId { get; set; }
            public string Reason { get; set; }
            public DateTime sDate { get; set; }
            public string EnteredBy { get; set; }
        }
        public struct RoomLocked
        {
            public long ROOM_LOCK_ID { get; set; }
            public long ROOM_ID { get; set; }
            public DateTime LOCK_DATE { get; set; }
            public long DEPT_ID { get; set; }
            public long LOC_ID { get; set; }
            public string EnteredBy { get; set; }
            public string EnteredOn { get; set; }
            public string ModifiedBy { get; set; }
            public string ModifiedOn { get; set; }
            public long BookingID { get; set; }
        }
    }
}
