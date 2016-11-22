using System.Data;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Estoque
{
    public class UnidadeDAL
    {
        public IQueryable<EUNIDADE> BuscarLista()
        {
            // Busca a lista
            IQueryable<EUNIDADE> lista = from a in Contexto.Atual.EUNIDADE
                                         select a;
            return lista;
        }

        public bool Criar(EUNIDADE obj)
        {
            // Adicionar
            Contexto.Atual.AddToEUNIDADE(obj);
            Contexto.Atual.SaveChanges();
            return true;
        }

        public IQueryable<EUNIDADE> BuscarListaEspecifica(EUNIDADE obj)
        {
            // Busca a lista de mesas
            IQueryable<EUNIDADE> lista = from a in Contexto.Atual.EUNIDADE
                                         where a.codUndBase == obj.codUndBase
                                         select a;

            return lista;
        }

        public bool Atualizar(EUNIDADE obj)
        {
            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();
            return true;
        }

        public EUNIDADE Buscar(EUNIDADE obj)
        {
            EUNIDADE result = Contexto.Atual.EUNIDADE.SingleOrDefault(r => r.codUnd == obj.codUnd);
            return result;
        }
    }
}