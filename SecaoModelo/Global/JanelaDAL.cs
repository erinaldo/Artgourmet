using System.Data.Objects;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Global
{
    public class JanelaDAL
    {
        /// <summary>
        /// Função para buscar objeto completo
        /// </summary>
        /// <param name="obj">objeto</param>
        /// <returns>objeto</returns>
        public GJANELA Buscar(GJANELA obj)
        {
            GJANELA obj2 = Contexto.Atual.GJANELA.SingleOrDefault(a => a.idJanela == obj.idJanela);

            Contexto.Atual.Refresh(RefreshMode.StoreWins, obj2);

            return obj2;
        }

        /// <summary>
        /// Retorna lista de objeto
        /// </summary>
        /// <returns></returns>
        public IQueryable<GJANELA> BuscarLista()
        {
            IQueryable<GJANELA> lista = from a in Contexto.Atual.GJANELA
                                        select a;
            return lista;
        }
    }
}