using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGMOSOL.BAL
{
    public class clsItemData
    {
        private string sName;
        private int iID;

        private long sName2;
        private string sName3;

        public clsItemData()
        {
            sName = "";
            iID = 0;
            sName2 = 0;
        }

        public clsItemData(string Name, int ID)
        {
            sName = Name;
            iID = ID;
        }

        public clsItemData(string Name, int ID, long Name2)
        {
            sName = Name;
            iID = ID;
            sName2 = Name2;
        }

        public clsItemData(string Name, int ID, long Name2, string Name3)
        {
            sName = Name;
            iID = ID;
            sName2 = Name2;
            sName3 = Name3;
        }

        public void setName(string Name)
        {
            sName = Name;
        }

        public void setName2(long Name2)
        {
            sName2 = Name2;
        }

        public void setName3(string Name3)
        {
            sName3 = Name3;
        }

        public string Name
        {
            get
            {
                return sName;
            }
            set
            {
                sName = value;
            }
        }

        public int ItemData
        {
            get
            {
                return iID;
            }
            set
            {
                iID = value;
            }
        }

        public long ItemName2
        {
            get
            {
                return sName2;
            }
            set
            {
                sName2 = value;
            }
        }


        public string ItemName3
        {
            get
            {
                return sName3;
            }
            set
            {
                sName3 = value;
            }
        }
 
        public override string ToString()
        {
            return sName;
        }
    }
}
