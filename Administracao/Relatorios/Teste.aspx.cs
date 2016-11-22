using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artebit.Restaurante.Global.Modelo;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;

namespace Artebit.Restaurante.Administracao.Relatorios
{
    public partial class Teste : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StiReport rpt = new StiReport();
            string path = HttpContext.Current.Server.MapPath("~/Relatorios/MRT/Descontos.mrt");

            
            rpt.Load(path);

            string conexao = (Contexto.Atual.Connection as System.Data.EntityClient.EntityConnection).StoreConnection.ConnectionString;
            
            rpt.Dictionary.Databases.Clear();
            rpt.Dictionary.Databases.Add(new StiSqlDatabase("Restaurante", conexao));


            StiWebViewerFx1.Report = rpt;
        }
    }
}