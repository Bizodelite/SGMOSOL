using SGMOSOL.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGMOSOL.BAL
{
    public class BhojnalayPrintReceiptBAL
    {
        BhojnalayPrintReceiptDAL da = new BhojnalayPrintReceiptDAL();
        public DataTable getItemCode()
        {
            return da.getItemCode();
        }
        public DataTable getItemCodeAssignToCounter()
        {
            return da.getItemCodeAssignToCounter();
        }
        public DataTable getItemName()
        {
            return da.getItemName();
        }
        public string getItemName(string itemType, string itemValue)
        {
            return da.getItemName(itemType, itemValue);
        }
        public decimal getItemPrice(int itemId)
        {
            return da.getItemPrice(itemId);
        }
        public int getMasterReceiptNumber()
        {
            return da.getMasterReceiptNumber();
        }
        public int InsertBhojnalayReceipt(object data)
        {
            return da.InsertBhojnalayPrintReceiptMasterData(data);
        }
        public int InsertMessItemData(object data)
        {
            return da.InsertMessItemData(data);
        }
        public int InsertRquToAdmin_DET(object data)
        {
            return da.InsertRquToAdmin_DET(data);
        }
        public int getItemIdbyItemName(string ItemName)
        {
            return da.getItemIdbyItemName(ItemName);
        }
        public DataTable getAllData(object data)
        {
            return da.getAllData(data);
        }
        public DataTable getDataByReceiptID(string ReceiptID)
        {
            return da.getDataByReceiptID(ReceiptID);
        }
        public DataTable getItemDetailbyMasterId(string ReceiptID)
        {
            return da.getItemDataByMasterId(ReceiptID);
        }
        public DataTable getMessItemDataForReport(string Receipt_ID)
        {
            return da.getMessItemDataForReport(Receipt_ID);
        }
        public string getReqNumber()
        {
            return da.getReqNumber();
        }
        //public DataTable getItemCode()
        //{
        //    return obj.getItemCode();
        //}
        public string getItemName(int ItemId)
        {
            return da.getItemName(ItemId);
        }
        public int InsertReqToAdmin_MST(object data)
        {
            return da.InsertReqToAdmin_MST(data);
        }
    }
}
