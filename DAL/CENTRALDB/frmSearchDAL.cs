using SGMOSOL.ADMIN;
using SGMOSOL.DataModel;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGMOSOL.DataSet;
using System.Windows.Forms;

namespace SGMOSOL.DAL.CENTRALDB
{
    public class frmSearchDAL
    {
        string connectionString = CommonFunctions.Decrypt(ConfigurationManager.ConnectionStrings["strConnectionCBs"].ConnectionString, true);
        CommonFunctions commonFunctions = new CommonFunctions();
        DataTable dt = null;

        public DataTable checkRecordinCB(string mob=null, string Bardcode)
        { 
            DataTable dataTable = new DataTable();
            return dataTable;
        
        }
    }
  
}
