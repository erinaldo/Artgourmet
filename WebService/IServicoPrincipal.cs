using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ArtebitGourmet.WebService.BL;
using Artebit.Restaurante.Global.WebService.BL;

namespace ArtebitGourmet.WebService
{
    [ServiceContract(Namespace="")]
    public interface IServicoPrincipal
    {
        [WebInvoke(Method = "GET",
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare,
           UriTemplate = "pdv/vendedores")]
        List<Vendedor> GetVendedores();

        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "pdv/itenscardapio")]
        List<ItensCardapio> GetItensCardapio();

        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "pdv/mesas")]
        List<MesaLista> GetMesasLista();

        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "pdv/mesas/{id}")]
        Mesa GetMesa(string id);

        [WebInvoke(Method = "PUT",
         UriTemplate = "/pdv/mesas")]
        void ExecutaAcao(AcaoModel acao);

    }

}
