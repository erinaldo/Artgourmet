using System;
using System.Data;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Global
{
    public class PerfilDAL
    {
        /// <summary>
        /// Função para buscar objeto perfil completo
        /// </summary>
        /// <param  name="obj">objeto perfil</param>
        /// <returns>objeto perfil</returns>
        public GPERFIL Buscar(GPERFIL obj)
        {
            // Busca o perfil
            IQueryable<GPERFIL> perfis = from a in Contexto.Atual.GPERFIL
                                         where
                                             (a.idPerfil == obj.idPerfil ||
                                              (a.descricao == obj.descricao && a.codSistema == obj.codSistema))
                                             &&
                                             (a.idEmpresa == Memoria.Empresa || Memoria.Empresa == 0)
                                         select a;

            if (perfis.Any())
            {
                return perfis.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Função para buscar objeto perfil completo
        /// </summary>
        /// <param name="idPerfil"> </param>
        /// <returns>objeto perfil</returns>
        public GPERFIL Buscar(int idPerfil)
        {
            // Busca o perfil
            return (from a in Contexto.Atual.GPERFIL
                    where
                        (a.idPerfil == idPerfil)
                        &&
                        (a.idEmpresa == Memoria.Empresa || Memoria.Empresa == 0)
                    select a).FirstOrDefault();
        }

        /// <summary>
        /// Função para buscar objeto perfil sem as ligações
        /// </summary>
        /// <param name="obj">objeto perfil</param>
        /// <returns>objeto perfil</returns>
        public GPERFIL BuscarVazio(GPERFIL obj)
        {
            // Busca o perfil
            GPERFIL perfil = Contexto.Atual.GPERFIL.ToList().SingleOrDefault(a => a.idPerfil == obj.idPerfil
                                                                                  &&
                                                                                  (a.idEmpresa == Memoria.Empresa ||
                                                                                   Memoria.Empresa == 0)
                );

            if (perfil != null)
            {
                for (int x = 0; x < perfil.GPERMISSAO.Count(); x++)
                {
                    perfil.GPERMISSAO.Remove((perfil.GPERMISSAO.ElementAt(x)));
                    x--;
                }
            }

            return perfil;
        }

        /// <summary>
        /// Função para buscar uma lista de objetos
        /// </summary>
        /// <returns>lista de objetos</returns>
        public IQueryable<GPERFIL> BuscarLista()
        {
            // Busca a lista
            IQueryable<GPERFIL> lista = (from a in Contexto.Atual.GPERFIL
                                         where (a.idEmpresa == Memoria.Empresa || Memoria.Empresa == 0)
                                         select a).Distinct();

            return lista;
        }

        public IQueryable<GPERFIL> BuscarListaEspecifica(GPERFIL obj)
        {
            IQueryable<GPERFIL> lista = from a in Contexto.Atual.GPERFIL
                                        where a.codSistema == obj.codSistema
                                        select a;
            return lista;
        }

        /// <summary>
        /// Função para criar novo
        /// </summary>
        /// <param name="obj">objeto perfil</param>
        /// <returns>true ou false</returns>
        public bool Criar(GPERFIL obj)
        {
            if (Memoria.Empresa != 0)
            {
                // atualiza a empresa e a filial
                obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);

                obj.idPerfil = Contexto.GerarId("GPERFIL");

                // atualiza data de inclusão e alteração
                obj.dataInclusao = DateTime.Now;
                obj.dataAlteracao = DateTime.Now;

                // adiciona perfil
                Contexto.Atual.AddToGPERFIL(obj);

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
        /// <param name="obj">objeto perfil</param>
        /// <returns>true ou false</returns>
        public bool Atualizar(GPERFIL obj)
        {
            // atualiza data de alteração
            obj.dataAlteracao = DateTime.Now;

            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();
            return true;
        }

        /// <summary>
        /// Função que verifica se o perfil tem permissão
        /// </summary>
        /// <param name="obj">objeto perfil</param>
        /// <param name="janela">janela atual</param>
        /// <param name="funcionalidade">funcionalidade que deseja saber a permissão</param>
        /// <returns>true ou false</returns>
        public bool Verificar(GPERFIL obj, string janela, int funcionalidade)
        {
            // Verifica se o perfil tem permissão da funcionalidade naquela tela
            if (
                obj.GPERMISSAO.Any(
                    p =>
                    p.idFuncionalidade == funcionalidade && p.GJANELA.nomeJanela == janela &&
                    p.GJANELA.codSistema == Memoria.CodSistema))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Função que troca o status para inativo
        /// </summary>
        /// <param name="obj">objeto perfil</param>
        /// <returns>true ou false</returns>
        public bool Cancelar(GPERFIL obj)
        {
            obj.ativo = false;
            return Atualizar(obj);
        }

        /// <summary>
        /// Função que verifica se o perfil tem acesso à janela
        /// </summary>
        /// <param name="obj">objeto perfil</param>
        /// <param name="sistema">sistema atual</param>
        /// <param name="janela">janela atual</param>
        /// <returns>true ou false</returns>
        public bool VerificarJanela(GPERFIL obj, string janela)
        {
            // Verifica se o perfil tem permissão da funcionalidade naquela tela
            if (obj.GPERMISSAO.ToList().Any(r => r.GJANELA.nomeJanela == janela))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Função para buscar objeto perfil do usuário de acordo com a empresa e a filial
        /// </summary>
        /// <param  name="obj">objeto perfil</param>
        /// <returns>objeto perfil</returns>
        public GPERFIL BuscarEspecifica()
        {
            // Busca o perfil
            IQueryable<GPERFIL> perfis = from a in Contexto.Atual.GPERFIL
                                         join b in Contexto.Atual.GUSRFILMOD on a.idPerfil equals b.idPerfil
                                         where (a.idEmpresa == Memoria.Empresa)
                                               &&
                                               (b.idEmpresa == Memoria.Empresa)
                                               &&
                                               (b.idFilial == Memoria.Filial)
                                               &&
                                               (b.codUsuario == Memoria.Codusuario)
                                               &&
                                               (b.codSistema == Memoria.CodSistema)
                                         select a;

            if (perfis.Count() > 0)
            {
                return perfis.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
    }
}