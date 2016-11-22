using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Artebit.Restaurante.Global.Modelo;

namespace ReservasWeb
{
    public partial class CarregaImagem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);

                carregar(id);
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

        void update(int id)
        {
            // Convert System.Drawing.Image to a byte[]
            byte[] file_byte = ReadFile("c:\\imagem.png");
            // Create a System.Data.Linq.Binary - this is what an "image" column is mapped to


            GIMAGEM g = Contexto.Atual.GIMAGEM.SingleOrDefault(r => r.idImagem == id);

            g.dado = file_byte;
            //g.idEmpresa = 1;
            //g.idImagem = 1;
            g.extensao = "png";

            Contexto.Atual.SaveChanges();
        }

        private void add()
        {
            // Convert System.Drawing.Image to a byte[]
            byte[] file_byte = ReadFile("c:\\imagem.png");
            // Create a System.Data.Linq.Binary - this is what an "image" column is mapped to

            GIMAGEM g = new GIMAGEM();

            g.dado = file_byte;
            g.idEmpresa = 1;
            g.idImagem = 5;
            g.extensao = "png";

            Contexto.Atual.AddToGIMAGEM(g);
            Contexto.Atual.SaveChanges();

        }

        private byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        //Open file into a filestream and 
        //read data in a byte array.
        byte[] ReadFile(string sPath)
        {
            //Initialize byte array with a null value initially.
            byte[] data = null;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream(sPath, FileMode.Open,
            FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to 

            //supply number of bytes to read from file.
            //In this case we want to read entire file. 

            //So supplying total number of bytes.
            data = br.ReadBytes((int)numBytes);
            return data;
        }
    }
}