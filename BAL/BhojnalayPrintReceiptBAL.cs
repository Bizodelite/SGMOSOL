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
        public DataTable getItemName()
        {
            return da.getItemName();
        }
        public int getItemID(string itemType, string itemValue)
        {
            return da.getItemID(itemType, itemValue);
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
            return da.getMessItemDataForReport
                (Receipt_ID);
        }
    }
}
