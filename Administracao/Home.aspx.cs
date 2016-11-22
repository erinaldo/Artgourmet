using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;

namespace Artebit.Restaurante.Administracao
{
    public partial class Home : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MemoriaWeb.ValidaSessao();
        }
    }
}