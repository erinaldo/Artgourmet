using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Artebit.Restaurante.Reserva
{
	public partial class Reserva : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            Global.RegrasNegocio.MemoriaWeb.ValidaSessao();
            
            
		}
	}
}