namespace WindowsServiceTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var p = new Processo();
            p.GerenciaFila(1);
            p.GerenciaFila(2);

            //string printerName = "PRECONTA1";
            //string query = string.Format("SELECT * from Win32_Printer WHERE Name LIKE '%{0}'", printerName);
            //ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            //ManagementObjectCollection coll = searcher.Get();

            //foreach (ManagementObject printer in coll)
            //{
            //    foreach (PropertyData property in printer.Properties)
            //    {
            //        Console.WriteLine(string.Format("{0}: {1}", property.Name, property.Value));
            //    }
            //}
        }
    }
}