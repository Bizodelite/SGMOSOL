using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGMOSOL.ADMIN
{
    public class SystemModel
    {
        private static SystemModel _instance;

        public static SystemModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SystemModel();
                }
                return _instance;
            }
        }
        public string sessionUser { get; set; }

        public static SystemModel _Computer_Name;
        public static SystemModel Computer_Name
        {
            get
            {
                if (_Computer_Name == null)
                {
                    _Computer_Name = new SystemModel();
                }
                return _Computer_Name;
            }
        }
        public string Comp_Name { get; set; }
    }
}
