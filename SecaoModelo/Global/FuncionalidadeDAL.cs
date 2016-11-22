using System.Data.Objects;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Global
{
    public class FuncionalidadeDAL
    {
        /// <summary>
        /// Função para buscar objeto completo
        /// </summary>
        /// <param name="obj">objeto</param>
        /// <returns>objeto</returns>
        public GFUNCIONALIDADE Buscar(GFUNCIONALIDADE obj)
        {
            GFUNCIONALIDADE obj2 =
                Contexto.Atual.GFUNCIONALIDADE.SingleOrDefault(a => a.idFuncionalidade == obj.idFuncionalidade);

            Contexto.Atual.Refresh(RefreshMode.StoreWins, obj2);

            return obj2;
        }

        /// <summary>
        /// Retorna lista de objeto
        /// </summary>
        /// <returns></returns>
        public IQueryable<GFUNCIONALIDADE> BuscarLista()
        {
            IQueryable<GFUNCIONALIDADE> lista = from a in Contexto.Atual.GFUNCIONALIDADE
                                                select a;
            return lista;
        }
    }
}