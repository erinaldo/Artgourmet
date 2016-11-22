using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Estoque
{
    public class TabPrecoDAL
    {
        public List<ETABPRECO> BuscarLista(ETABPRECO obj)
        {
            IQueryable<ETABPRECO> lista = from a in Contexto.Atual.ETABPRECO
                                          where (a.idProduto == obj.idProduto &&
                                                 (a.idEmpresa == obj.idEmpresa || a.idEmpresa == Memoria.Empresa))
                                          select a;


            List<ETABPRECO> listaRetorno = lista.ToList();

            return listaRetorno;
        }

        public ETABPRECO Buscar(ETABPRECO obj)
        {
            ETABPRECO prod = Contexto.Atual.ETABPRECO.SingleOrDefault(r => r.idTabPreco == obj.idTabPreco);

            return prod;
        }
    }
}