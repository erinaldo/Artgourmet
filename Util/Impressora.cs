using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Printing;

namespace Artebit.Restaurante.Global.Util
{
    public class Impressora
    {
        private static ManagementScope oManagementScope;
        //Adds the Printer
        public static bool AddPrinter(string sPrinterName)
        {
            try
            {
                oManagementScope = new ManagementScope(ManagementPath.DefaultPath);
                oManagementScope.Connect();

                var oPrinterClass = new ManagementClass(new ManagementPath("Win32_Printer"), null);
                ManagementBaseObject oInputParameters = oPrinterClass.GetMethodParameters("AddPrinterConnection");

                oInputParameters.SetPropertyValue("Name", sPrinterName);

                oPrinterClass.InvokeMethod("AddPrinterConnection", oInputParameters, null);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Deletes the printer
        public static bool DeletePrinter(string sPrinterName)
        {
            oManagementScope = new ManagementScope(ManagementPath.DefaultPath);
            oManagementScope.Connect();

            var oSelectQuery = new SelectQuery();
            oSelectQuery.QueryString = @"SELECT * FROM Win32_Printer WHERE Name = '" +
                                       sPrinterName.Replace("\\", "\\\\") + "'";

            var oObjectSearcher = new ManagementObjectSearcher(oManagementScope, oSelectQuery);
            ManagementObjectCollection oObjectCollection = oObjectSearcher.Get();

            if (oObjectCollection.Count != 0)
            {
                foreach (ManagementObject oItem in oObjectCollection)
                {
                    oItem.Delete();
                    return true;
                }
            }
            return false;
        }

        //Renames the printer
        public static void RenamePrinter(string sPrinterName, string newName)
        {
            oManagementScope = new ManagementScope(ManagementPath.DefaultPath);
            oManagementScope.Connect();

            var oSelectQuery = new SelectQuery();
            oSelectQuery.QueryString = @"SELECT * FROM Win32_Printer WHERE Name = '" +
                                       sPrinterName.Replace("\\", "\\\\") + "'";

            var oObjectSearcher = new ManagementObjectSearcher(oManagementScope, oSelectQuery);
            ManagementObjectCollection oObjectCollection = oObjectSearcher.Get();

            if (oObjectCollection.Count != 0)
            {
                foreach (ManagementObject oItem in oObjectCollection)
                {
                    oItem.InvokeMethod("RenamePrinter", new object[] {newName});
                    return;
                }
            }
        }

        //Sets the printer as Default
        public static void SetDefaultPrinter(string sPrinterName)
        {
            oManagementScope = new ManagementScope(ManagementPath.DefaultPath);
            oManagementScope.Connect();

            var oSelectQuery = new SelectQuery();
            oSelectQuery.QueryString = @"SELECT * FROM Win32_Printer WHERE Name = '" +
                                       sPrinterName.Replace("\\", "\\\\") + "'";

            var oObjectSearcher = new ManagementObjectSearcher(oManagementScope, oSelectQuery);
            ManagementObjectCollection oObjectCollection = oObjectSearcher.Get();

            if (oObjectCollection.Count != 0)
            {
                foreach (ManagementObject oItem in oObjectCollection)
                {
                    oItem.InvokeMethod("SetDefaultPrinter", new object[] {sPrinterName});
                    return;
                }
            }
        }

        //Gets the printer information
        public static void GetPrinterInfo(string sPrinterName)
        {
            oManagementScope = new ManagementScope(ManagementPath.DefaultPath);
            oManagementScope.Connect();

            var oSelectQuery = new SelectQuery();
            oSelectQuery.QueryString = @"SELECT * FROM Win32_Printer WHERE Name = '" +
                                       sPrinterName.Replace("\\", "\\\\") + "'";

            var oObjectSearcher = new ManagementObjectSearcher(oManagementScope, @oSelectQuery);
            ManagementObjectCollection oObjectCollection = oObjectSearcher.Get();

            foreach (ManagementObject oItem in oObjectCollection)
            {
                Console.WriteLine("Name : " + oItem["Name"]);
                Console.WriteLine("PortName : " + oItem["PortName"]);
                Console.WriteLine("DriverName : " + oItem["DriverName"]);
                Console.WriteLine("DeviceID : " + oItem["DeviceID"]);
                Console.WriteLine("Shared : " + oItem["Shared"]);
                Console.WriteLine("---------------------------------------------------------------");
            }
        }

        //Checks whether a printer is installed
        public bool IsPrinterInstalled(string sPrinterName)
        {
            oManagementScope = new ManagementScope(ManagementPath.DefaultPath);
            oManagementScope.Connect();

            var oSelectQuery = new SelectQuery();
            oSelectQuery.QueryString = @"SELECT * FROM Win32_Printer WHERE Name = '" +
                                       sPrinterName.Replace("\\", "\\\\") + "'";

            var oObjectSearcher = new ManagementObjectSearcher(oManagementScope, oSelectQuery);
            ManagementObjectCollection oObjectCollection = oObjectSearcher.Get();

            return oObjectCollection.Count > 0;
        }


        /// <summary>
        /// method to populate a generic list holding all the available printers on a network
        /// </summary>
        /// <returns></returns>
        public static List<string> GetInstalledPrinters()
        {
            var printerList = new List<string>();

            try
            {
                //set the scope of this search to the local machine
                var scope = new ManagementScope(ManagementPath.DefaultPath);
                //connect to the machine
                scope.Connect();

                //build the SelectQuery to pull from Win32_Printer
                var query = new SelectQuery("select * from Win32_Printer");

                var m = new ManagementClass("Win32_Printer");

                var obj = new ManagementObjectSearcher(scope, query);

                //now loop through what is found and populate our Generic list with
                //the names of all installed printers on the local machine
                using (ManagementObjectCollection printers = m.GetInstances())
                    foreach (ManagementObject printer in printers)
                        printerList.Add(printer["Name"].ToString());

                printerList.Sort();

                return printerList;
            }
            catch
            {
                return null;
            }
        }


        public static List<PrintQueue> GetPrinters()
        {
            var localPrintServer = new PrintServer();

            PrintQueueCollection printQueues =
                localPrintServer.GetPrintQueues(new[]
                                                    {
                                                        EnumeratedPrintQueueTypes.Local,
                                                        EnumeratedPrintQueueTypes.Connections
                                                    });

            List<PrintQueue> printerList = (from printer in printQueues
                                            select printer).ToList();

            return printerList;
        }
    }
}