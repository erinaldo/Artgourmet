using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;

namespace ReservasWeb
{
    public partial class Captcha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Bitmap objBMP = new System.Drawing.Bitmap(110, 30);

            Graphics objGraphics = System.Drawing.Graphics.FromImage(objBMP);

            objGraphics.Clear(Color.SkyBlue);

            objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            // Fonte configurada para ser usada no texto do captcha

            Font objFont = new Font("Times New Roman", 18, FontStyle.Strikeout);

            string captchaValue = "";

            int[] valuesArray = new int[8];

            int x;

            //Cria o valor randomicamente e adiciona ao array

            Random autoRand = new Random();

            for (x = 0; x < 8; x++)
            {

                valuesArray[x] = System.Convert.ToInt32(autoRand.Next(0, 9));

                captchaValue += (valuesArray[x].ToString());

            }

            //Adiciona o valor gerado para o captcha na sessão

            //para ser validado posteriormente

            Session.Add("CaptchaValue", captchaValue);

            //Desenha a imagem com o nosso texto

            objGraphics.DrawString(captchaValue, objFont, Brushes.White, 3, 3);

            //Determina o tipo de conteúdo da imagem do captcha

            Response.ContentType = "image/GIF";

            //Salva em stream

            objBMP.Save(Response.OutputStream, ImageFormat.Gif);

            //Libera os objeto da memória pois os mesmos não são mais necessários

            objFont.Dispose();

            objGraphics.Dispose();

            objBMP.Dispose();
        }
    }
}