using System;
using System.Data;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Global
{
    public class GrupoMesaDAL
    {
        /// <summary>
        /// Função para buscar objeto grupo mesa completo
        /// </summary>
        /// <param name="obj">objeto grupo mesa</param>
        /// <returns>objeto grupo  mesa</returns>
        public RGRUPOMESA Buscar(RGRUPOMESA obj)
        {
            // Busca o grupo 
            RGRUPOMESA grupo = Contexto.Atual.RGRUPOMESA.SingleOrDefault(a => a.idGrupo == obj.idGrupo
                                                                              &&
                                                                              (a.idEmpresa == Memoria.Empresa ||
                                                                               Memoria.Empresa == 0));

            return grupo;
        }

        /// <summary>
        /// Função para buscar uma lista de objetos grupo  mesa
        /// </summary>
        /// <returns>lista de objetos</returns>
        public IQueryable<RGRUPOMESA> BuscarLista()
        {
            // Busca o grupo 
            IQueryable<RGRUPOMESA> grupos = from a in Contexto.Atual.RGRUPOMESA
                                            where (a.idEmpresa == Memoria.Empresa || Memoria.Empresa == 0)
                                            select a;

            return grupos;
        }

        /// <summary>
        /// Função para buscar uma lista de objetos grupo  mesa com parametros
        /// </summary>
        /// <returns>lista de objetos</returns>
        public IQueryable<RGRUPOMESA> BuscarListaEspecifica(RGRUPOMESA obj)
        {
            // Busca o grupo 
            IQueryable<RGRUPOMESA> grupos = from a in Contexto.Atual.RGRUPOMESA
                                            where (a.idEmpresa == Memoria.Empresa || Memoria.Empresa == 0)
                                                  && (a.descricao == obj.descricao || obj.descricao == null)
                                                  && (a.dataAlteracao == obj.dataAlteracao || obj.dataAlteracao == null)
                                                  && (a.dataAlteracao == obj.dataAlteracao || obj.dataAlteracao == null)
                                            select a;

            return grupos;
        }

        /// <summary>
        /// Função para criar novo
        /// </summary>
        /// <param name="obj">objeto grupo  mesa</param>
        /// <returns>true ou false</returns>
        public bool Criar(RGRUPOMESA obj)
        {
            if (Memoria.Empresa != 0 && Memoria.Empresa != 0)
            {
                // atualiza a empresa e a filial
                obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);
                obj.idGrupo = Contexto.GerarId("RGRUPOMESA");

                // atualiza data de inclusão e alteração
                obj.dataInclusao = DateTime.Now;
                obj.dataAlteracao = DateTime.Now;

                // adiciona grupo  mesa
                Contexto.Atual.AddToRGRUPOMESA(obj);

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
        /// <param name="obj">objeto grupo mesa</param>
        /// <returns>true ou false</returns>
        public bool Atualizar(RGRUPOMESA obj)
        {
            // atualiza data de alteração
            obj.dataAlteracao = DateTime.Now;

            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();
            return true;
        }
    }
}