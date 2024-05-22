using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGMOSOL.DataModel
{
    public class dengiReceiptModel
    {
        public string Receipt_Id { get; set; }
        public DateTime dr_Date { get; set; }
        public int comp_ID { get; set; }
        public int locID { get; set; }
        public int Dept_Id { get; set; }
        public int Fy_ID { get; set; }
        public int ctrMacId { get; set; }
        public double serailId { get; set; }
        public int DengiId { get; set; }
        public string dengitype { get; set; }
        public decimal amount { get; set; }
        public int paymentTypeId { get; set; }
        public string type { get; set; }
        public string gotra { get; set; }
        public string Name { get; set; }
        public int gotraId { get; set; }
        public string contact { get; set; }
        public string Address { get; set; }
        public int DistId { get; set; }
        public string DISTRICT { get; set; }
        public string Taluka { get; set; }
        public string chqbankname { get; set; }
        public string chno { get; set; }
        public DateTime chqdate { get; set; }
        public string ddbankname { get; set; }
        public string ddno { get; set; }
        public DateTime dd_date { get; set; }
        public string netbankname { get; set; }
        public string netbankrefnumber { get; set; }
        public string cardbankname { get; set; }
        public string cardbankrefnumber { get; set; }
        public string EnteredBy { get; set; }
        public DateTime EnteredOn { get; set; }
        public string modifiedby { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string machinename { get; set; }
        public int userId { get; set; }
        public string serverName { get; set; }
        public int tidId { get; set; }
        public string Invoiceno { get; set; }
        public string PanNo { get; set; }
        public string PinCode { get; set; }
        public int countryId { get; set; }
        public string COUNTRY_NAME { get; set; }
        public int counter { get; set; }
        public int stateId { get; set; }
        public string STATE { get; set; }
        public string Doc_type { get; set; }
        public string Doc_Detail { get; set; }
        public string receiptFno { get; set; }
        public string receiptLNo { get; set; }
        public DateTime receiptFDate { get; set; }
        public DateTime ReceiptLDate { get; set; }
        public int IsDuplicate { get; set; }
        public string ScanImage { get; set; }
        public string Prefix { get; set; }
        public string Barcode { get; set; }
        public string TableName { get; set; }

    }
    public struct DengiErrorLog
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public string ReceiptNo { get; set; }
        public string LastName { get; set; }
        public double LastAmount { get; set; }
        public string LastReceiptNo { get; set; }
        public DateTime ErrorDate { get; set; }
        public string Mach_Id { get; set; }
        public string Username { get; set; }
    }
};
