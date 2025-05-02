using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;

namespace wv.GeneralFunction
{
    /// <summary>
    /// Summary description for eFormController
    /// </summary>
    public class GeneralFunctionController 
    {

        public GeneralFunctionController()
        {

        }

        public bool checkIsUnderMaintenance()
        {
            bool bCheckIsUnderMaintenance=false;

            if (ConfigurationManager.AppSettings["UnderMaintenance"] != null)
                if (ConfigurationManager.AppSettings["UnderMaintenance"] == "1")
                    bCheckIsUnderMaintenance = true;

            return bCheckIsUnderMaintenance;
        }

        public string getUnderMaintenanceMsg1()
        {
            string sGetUnderMaintenanceMsg1 = "";

            if (ConfigurationManager.AppSettings["UnderMaintenanceMsg1"] != null)
                if (ConfigurationManager.AppSettings["UnderMaintenanceMsg1"] != "")
                    sGetUnderMaintenanceMsg1 = ConfigurationManager.AppSettings["UnderMaintenanceMsg1"];

            return sGetUnderMaintenanceMsg1;
        }

        public string getUnderMaintenanceMsg2()
        {
            string sGetUnderMaintenanceMsg2 = "";

            if (ConfigurationManager.AppSettings["UnderMaintenanceMsg2"] != null)
                if (ConfigurationManager.AppSettings["UnderMaintenanceMsg2"] != "")
                    sGetUnderMaintenanceMsg2 = ConfigurationManager.AppSettings["UnderMaintenanceMsg2"];

            return sGetUnderMaintenanceMsg2;
        }

    }
}