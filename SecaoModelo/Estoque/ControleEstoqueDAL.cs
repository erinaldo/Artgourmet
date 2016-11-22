using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Estoque
{
    public class ControleEstoqueDAL
    {
        public IQueryable<ECTESTOQUE> BuscarLista(ECTESTOQUE obj)
        {
            IQueryable<ECTESTOQUE> lista = from a in Contexto.Atual.ECTESTOQUE
                                           where obj.idEmpresa == a.idEmpresa && obj.idProduto == a.idProduto
                                           select a;

            return lista;
        }
    }
}