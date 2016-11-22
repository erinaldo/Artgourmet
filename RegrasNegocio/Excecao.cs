using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;

namespace Artebit.Restaurante.Global.RegrasNegocio
{
    public class Excecao
    {
        public static void TratarExcecao(Exception ex)
        {
            string nome = "logErros-" + DateTime.Now.ToString("dd_MM_yyyy");
            StreamWriter vWriter;
            if (HttpContext.Current != null)
            {
                vWriter = new StreamWriter(HttpContext.Current.Server.MapPath("~/Log/" + nome + ".txt"), true);
            }
            else
            {
                string pathdll = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                string caminhoLog = pathdll; //.Replace("\\bin\\Debug", "\\Log\\");
                Console.WriteLine(caminhoLog);
                vWriter = new StreamWriter(caminhoLog + nome + ".txt", true, Encoding.ASCII);
            }


            vWriter.WriteLine("--------------------------------- " + DateTime.Now.ToString() +
                              "--------------------------------- ");
            vWriter.WriteLine(ex.ToString());
            vWriter.WriteLine("------------------------------------------------------");
            vWriter.WriteLine("");
            vWriter.WriteLine("");
            vWriter.Flush();
            vWriter.Close();
        }
    }
}