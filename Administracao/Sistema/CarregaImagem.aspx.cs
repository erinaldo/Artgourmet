using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Administracao.Sistema
{
    public partial class CarregaImagem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                using (Contexto.Atual = new Global.Modelo.Restaurante())
                {
                    if(Convert.ToString(Request.QueryString["id"]) != "")
                    {
                        int id = Convert.ToInt32(Request.QueryString["id"]);

                        carregar(id);    
                    }
                }
                
            }
            else
            {

            }
            //carregar();
            //update(2);
            //add();
        }

        void carregar(int id)
        {
            
            try
            {
                GIMAGEM g = Contexto.Atual.GIMAGEM.SingleOrDefault(r => r.idImagem == id);

                MemoryStream imageStream = new MemoryStream();

                byte[] imageContent = g.dado.ToArray();

                imageStream.Position = 0;
                imageStream.Read(imageContent, 0, (int)imageStream.Length);
                Response.ContentType = g.mime;
                Response.BinaryWrite(imageContent);
                Response.End();
            }
            catch
            {
                // ERORR HANDLING INVALID PARAMETER, DB CONN, ETC.
            }
        }

        private byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

    }
}