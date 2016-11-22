using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Compras
{
    public class EstoqueDAL
    {
        private List<GSISTEMA> lista = new List<GSISTEMA>();

        /// <summary>
        /// Funcao para buscar objeto movimento completo
        /// </summary>
        /// <param name="obj">objeto movimento</param>
        /// <param name="tpmov">tipo do movimento</param>
        /// <returns>objeto movimento</returns>
        public ECTESTOQUE Buscar(ECTESTOQUE obj)
        {
            // Busca o usuario
            ECTESTOQUE movimento =
                Contexto.Atual.ECTESTOQUE.ToList().SingleOrDefault(
                    a =>
                    a.idCtEstoque == obj.idCtEstoque);

            return movimento;
        }

        /// <summary>
        /// Fun��o para buscar uma lista de objetos
        /// </summary>
        /// <returns>lista de objetos</returns>
        public IQueryable<ECTESTOQUE> BuscarLista()
        {
            // Busca a lista
            IQueryable<ECTESTOQUE> lista = (from a in Contexto.Atual.ECTESTOQUE
                                            select a).Distinct();

            return lista;
        }

        /// <summary>
        /// Fun��o para criar novo estoque
        /// </summary>
        /// <param name="obj">objeto estoque</param>
        /// <returns>true ou false</returns>
        public bool Criar(ECTESTOQUE obj, List<string> compl)
        {
            return false;
        }
    }
}