using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGMOSOL.BAL
{
    internal class BadBAL
    {
        public class BedCheckInMst
        {
            public long CheckOutMstId { get; set; }
            public long CheckInMstId { get; set; }
            public int ComId { get; set; }
            public int LocId { get; set; }
            public int DeptId { get; set; }
            public long CtrMachId { get; set; }
            public int FyId { get; set; }
            public string CheckIn_ID { get; set; }

            public DateTime InDate { get; set; }
            public DateTime InTime { get; set; }
            public string mob_no { get; set; }
            public Int16 BhaktTypeId { get; set; }

            public long SerialNo { get; set; }
            public string ScanDoc { get; set; }
            public string Image { get; set; }
            public string Name { get; set; }
            public string Place { get; set; }
            public int Days { get; set; }
            public int NoOfRooms { get; set; }
            public int NoOfPersons { get; set; }
            public double NoOfBeds { get; set; }
            public string Remark { get; set; }
            public DateTime OutDate { get; set; }
            public DateTime OutTime { get; set; }
            public double Advance { get; set; }
            public double Refund { get; set; }
            public double Rent { get; set; }
            public long RecordModifiedCount { get; set; }
            public int UserId { get; set; }
            public string ServerName { get; set; }
            public string EnteredBy { get; set; }
            public string EnteredOn { get; set; }
            public string ModifiedBy { get; set; }
            public string ModifiedOn { get; set; }
            public string MachineName { get; set; }
            public string Barcode { get; set; }

            public string ImagePath { get; set; }
        }
        public struct BedCheckInDet
        {
            public long CheckOutMstId { get; set; }
            public long CheckOutDetId { get; set; }
            public long CheckInDetId { get; set; }
            public long CheckInMstId { get; set; }
            public Int32 ProdId { get; set; }
            public Int32 Qty { get; set; }
            public double Rent { get; set; }
            public double Advance { get; set; }
            public double TotalRent { get; set; }
            public double TotalAdv { get; set; }
            public long LockerRecordModifiedCount { get; set; }
        }
    }
}
