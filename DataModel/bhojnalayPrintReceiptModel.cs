using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGMOSOL.DataModel
{
    public class bhojnalayPrintReceiptModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
        public int Quantity { get; set; }
        public decimal PRINT_MST_ID  { get; set; }
        public DateTime PR_Date { get; set; }
        public int DeptId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Taluka { get; set; }
        public string ItemCode { get; set;  }
        public int Cash { get; set; }
        public int Guest { get; set; }
        public int Change { get; set; }
        public string Remark { get; set; }
        public string DocType { get; set; }
        public string DocTypeDetail{ get; set; }
        public string SerialNo { get; set; }
        public string receiptFno { get;set; }
        public string receiptLNo { get; set; }
        public DateTime receiptFDate { get; set; }
        public DateTime ReceiptLDate { get;set; }

    }
}
