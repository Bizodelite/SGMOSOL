using SGMOSOL.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGMOSOL.BAL
{
    public class ReqToAdminBAL
    {
        ReqToAdminDAL obj = new ReqToAdminDAL();
        public string getReqNumber()
        {
            return obj.getReqNumber();
        }
        public DataTable getItemCode()
        {
            return obj.getItemCode();
        }
        public string getItemName(int ItemId)
        {
            return obj.getItemName(ItemId);
        }
    }
}
