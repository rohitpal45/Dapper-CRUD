using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DapperCRUD.Models
{
    public class CnSetting
    {
        public static string CnMain
        {
            get
            {
                string cn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                return cn;
            }
        }
    }
}