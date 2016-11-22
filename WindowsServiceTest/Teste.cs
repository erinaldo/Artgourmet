////Code written by Mark Middlemist - @delradie 
////Made available at http://delradiesdev.blogspot.com
////Interop details from http://pinvoke.net/
//using System;
//using System.Runtime.InteropServices;

//namespace DelradiesDev.PrinterStatus
//{
//    public class WinSpoolPrinterInfo
//    {
//        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
//        public static extern int OpenPrinter(string pPrinterName, out IntPtr phPrinter, ref PRINTER_DEFAULTS pDefault);

//        [DllImport("winspool.drv", SetLastError = true, CharSet = CharSet.Auto)]
//        public static extern bool GetPrinter(IntPtr hPrinter, Int32 dwLevel, IntPtr pPrinter, Int32 dwBuf, out Int32 dwNeeded);

//        [DllImport("winspool.drv", SetLastError = true)]
//        public static extern int ClosePrinter(IntPtr hPrinter);

//        [StructLayout(LayoutKind.Sequential)]
//        public struct PRINTER_DEFAULTS
//        {
//            public IntPtr pDatatype;
//            public IntPtr pDevMode;
//            public int DesiredAccess;
//        }

//        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
//        public struct PRINTER_INFO_2
//        {
//            [MarshalAs(UnmanagedType.LPTStr)]
//            public string pServerName;

//            [MarshalAs(UnmanagedType.LPTStr)]
//            public string pPrinterName;

//            [MarshalAs(UnmanagedType.LPTStr)]
//            public string pShareName;

//            [MarshalAs(UnmanagedType.LPTStr)]
//            public string pPortName;

//            [MarshalAs(UnmanagedType.LPTStr)]
//            public string pDriverName;

//            [MarshalAs(UnmanagedType.LPTStr)]
//            public string pComment;

//            [MarshalAs(UnmanagedType.LPTStr)]
//            public string pLocation;

//            public IntPtr pDevMode;

//            [MarshalAs(UnmanagedType.LPTStr)]
//            public string pSepFile;

//            [MarshalAs(UnmanagedType.LPTStr)]
//            public string pPrintProcessor;

//            [MarshalAs(UnmanagedType.LPTStr)]
//            public string pDatatype;

//            [MarshalAs(UnmanagedType.LPTStr)]
//            public string pParameters;

//            public IntPtr pSecurityDescriptor;
//            public uint Attributes;
//            public uint Priority;
//            public uint DefaultPriority;
//            public uint StartTime;
//            public uint UntilTime;
//            public uint Status;
//            public uint cJobs;
//            public uint AveragePPM;
//        }

//        public PRINTER_INFO_2? GetPrinterInfo(String printerName)
//        {
//            IntPtr pHandle;
//            PRINTER_DEFAULTS defaults = new PRINTER_DEFAULTS();
//            PRINTER_INFO_2? Info2 = null;

//            OpenPrinter(printerName, out pHandle, ref defaults);

//            Int32 cbNeeded = 0;

//            bool bRet = GetPrinter(pHandle, 2, IntPtr.Zero, 0, out cbNeeded);

//            if (cbNeeded > 0)
//            {
//                IntPtr pAddr = Marshal.AllocHGlobal((int)cbNeeded);

//                bRet = GetPrinter(pHandle, 2, pAddr, cbNeeded, out cbNeeded);

//                if (bRet)
//                {
//                    Info2 = (PRINTER_INFO_2)Marshal.PtrToStructure(pAddr, typeof(PRINTER_INFO_2));
//                }

//                Marshal.FreeHGlobal(pAddr);
//            }

//            ClosePrinter(pHandle);

//            return Info2;
//        }
//    }
//} 


using System;
using System.Collections;
using System.Management;

namespace WindowsServiceTest
{
    internal enum PrinterStatus
    {
        Other = 1,
        Unknown,
        Idle,
        Printing,
        Warmup,
        Stopped,
        printing,
        Offline
    }


    internal class Teste
    {
        public static void tttt()
        {
            PrinterStatus stat;

            if ((stat = GetPrinterStat("PRECONTA1")) != 0) // UNC or a local name
            {
                Console.WriteLine(stat);
            }
            else
            {
                Console.WriteLine("Failed to get status");
            }
        }

        private static PrinterStatus GetPrinterStat(string printerDevice)
        {
            PrinterStatus ret = 0;
            string path = "win32_printer.DeviceId='" + printerDevice + "'";
            using (var printer = new ManagementObject(path))
            {
                printer.Get();
                PropertyDataCollection printerProperties = printer.Properties;
                var st =
                    (PrinterStatus) Convert.ToInt32(printer.Properties["PrinterStatus"].Value);
                ret = st;
            }
            return ret;
        }

        public static Hashtable GetPrinterProperties()
        {
            var properties = new Hashtable();
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");

            //now loop thorugh all the objects in the searcher
            foreach (ManagementObject obj in searcher.Get())
            {
                string[] printerStatus =
                    {
                        "Other", "Unknown", "Idle", "Printing", "WarmUp", "Stopped Printing",
                        "Offline"
                    };
                string[] printerState =
                    {
                        "Paused", "Error", "Pending Deletion", "Paper Jam", "Paper Out", "Manual Feed", "Paper Problem",
                        "Offline", "IO Active", "Busy", "Printing",
                        "Output Bin Full", "Not Available", "Waiting", "Processing", "Initialization", "Warming Up",
                        "Toner Low", "No Toner", "Page Punt", "User Intervention Required",
                        "Out of Memory", "Door Open", "Server_Unknown", "Power Save"
                    };


                //now loop through all the properties
                foreach (PropertyData data in obj.Properties)
                {
                    //make sure we have the default printer
                    if ((bool) obj["Default"])
                    {
                        Console.WriteLine(string.Format("{0}: {1}", data.Name, data.Value));
                        /*    switch (data.Name.ToLower())
                            {
                                case "printerstate":
                                    properties.Add("State", printerState[Convert.ToInt32(data.Value)]);
                                    break;
                                case "printerstatus":
                                    properties.Add("Status", printerStatus[Convert.ToInt32(data.Value)]);
                                    break;
                            }*/
                    }
                }
            }

            return properties;
        }
    }
}