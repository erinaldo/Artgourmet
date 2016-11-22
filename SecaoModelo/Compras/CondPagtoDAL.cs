using System.Collections.Generic;
using System.Data;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Compras
{
    public class CondPagtoDAL
    {
        private List<GSISTEMA> lista = new List<GSISTEMA>();

        /// <summary>
        /// Função para buscar objeto condição de pagamento completo
        /// </summary>
        /// <param name="obj">objeto condição de pagamento</param>
        /// <returns>objeto condição de pagamento</returns>
        public CCONDPAGTO Buscar(CCONDPAGTO obj)
        {
            // Busca o usuário
            CCONDPAGTO condpagto =
                Contexto.Atual.CCONDPAGTO.ToList().SingleOrDefault(
                    a =>
                    a.idCondPagto == obj.idCondPagto);

            return condpagto;
        }

        /// <summary>
        /// Função para buscar uma lista de objetos
        /// </summary>
        /// <returns>lista de objetos</returns>
        public IQueryable<CCONDPAGTO> BuscarLista()
        {
            // Busca a lista
            IQueryable<CCONDPAGTO> lista = (from a in Contexto.Atual.CCONDPAGTO
                                            select a).Distinct();

            return lista;
        }

        /// <summary>
        /// Função para buscar uma lista de objetos
        /// </summary>
        /// <returns>lista de objetos</returns>
        public IQueryable<CMOVIMENTO> BuscarVazio()
        {
            // Busca a lista
            IQueryable<CMOVIMENTO> lista = from a in Contexto.Atual.CMOVIMENTO
                                           select a;

            return lista;
        }

        /// <summary>
        /// Função para criar novo
        /// </summary>
        /// <param name="obj">objeto movimento</param>
        /// <returns>true ou false</returns>
        public bool Criar(CCONDPAGTO obj)
        {
            if (Memoria.Empresa != 0)
            {
                obj.idEmpresa = Memoria.Empresa;

                if (obj.idCondPagto == 0)
                {
                    obj.idCondPagto = Contexto.GerarId("CCONDPAGTO");
                }

                // adiciona usuário
                Contexto.Atual.AddToCCONDPAGTO(obj);

                // salva alterações
                Contexto.Atual.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Função para buscar
        /// </summary>
        /// <param name="obj">objeto movimento</param>
        /// <returns>true ou false</returns>
        public bool Atualizar(CCONDPAGTO obj)
        {
            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();
            return true;
        }

        /// <summary>
        /// Função que troca o status para inativo
        /// </summary>
        /// <param name="obj">objeto movimento</param>
        /// <returns>true ou false</returns>
        //public bool Cancelar(CMOVIMENTO obj)
        //{
        //    obj.idStatus = "C";
        //    return Atualiza(obj);
        //}
    }
}