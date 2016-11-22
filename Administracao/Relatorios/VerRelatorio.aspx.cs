using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artebit.Restaurante.Global.Modelo;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.WebFx;

namespace Artebit.Restaurante.Administracao.Relatorios
{
    public partial class VerRelatorio : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Contexto.AbrirContexto();

            StiWebViewerFxOptions.Toolbar.ShowOpenButton = false;

            if (Request.QueryString["id"] != null)
            {
                string rel = Convert.ToString(Request.QueryString["id"]);
             
                var rpt = new StiReport();
                
                rpt.Load(HttpContext.Current.Server.MapPath("~/Relatorios/MRT/" + rel + ".mrt"));

                rpt.Compile();

//                string conexao = (Contexto.Atual.Connection as System.Data.EntityClient.EntityConnection).StoreConnection.ConnectionString;

                //rpt.Dictionary.Databases.Clear();
                //rpt.Dictionary.Databases.Add(new StiSqlDatabase("Restaurante", conexao));
                
                reportViewer.Report = rpt;
            }

        }
    }
}