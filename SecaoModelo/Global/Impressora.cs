using System.Drawing;
using System.Drawing.Printing;

namespace Artebit.Restaurante.Global.Util
{
    public class Impressora
    {
        public string[] lista = null;

        public void ImprimeTexto(string texto, string nomeImpressora)
        {
            //Create a PrintDocument object 
            var PrintDoc = new PrintDocument();

            //Set PrinterName as the selected printer in the printers list 
            PrintDoc.PrinterSettings.PrinterName = nomeImpressora;

            //lista = texto.Split('*');

            //Add PrintPage event handler 
            PrintDoc.PrintPage += print_PrintPage;

            //Print the document 
            PrintDoc.Print();
        }

        protected void print_PrintPage(object sender, PrintPageEventArgs ppev)
        {
            //Get the Graphics object 
            Graphics g = ppev.Graphics;

            //Create a font verdana with size 14 
            var font = new Font("Arial", 12);


            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;

            //Read margins from PrintPageEventArgs 
            float leftMargin = 0;
            float topMargin = 1;
            //string line = null;

            //Calculate the lines per page on the basis of the height of the 
            //page and the height of the font 
            linesPerPage = ppev.MarginBounds.Height/font.GetHeight(g);

            font = new Font("Arial", 11, FontStyle.Regular);

            //Draw text 
            for (int i = 0; i < 50; i++)
            {
                yPos = topMargin + (count*font.GetHeight(g));

                g.DrawString("teste teste teste teste teste", font, Brushes.Black, leftMargin, yPos, new StringFormat());

                count++;
            }
        }
    }
}