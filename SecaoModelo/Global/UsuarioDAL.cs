using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Global
{
    public class UsuarioDAL
    {
        private List<GSISTEMA> lista = new List<GSISTEMA>();

        /// <summary>
        /// Função para buscar objeto usuário completo
        /// </summary>
        /// <param name="obj">objeto usuário</param>
        /// <returns>objeto usuário</returns>
        public GUSUARIO Buscar(GUSUARIO obj)
        {
            // Busca o usuário
            GUSUARIO usuario = Contexto.Atual.GUSUARIO.SingleOrDefault(a => a.codusuario == obj.codusuario);

            return usuario;
        }

        /// <summary>
        /// Função para buscar uma lista de objetos
        /// </summary>
        /// <returns>lista de objetos</returns>
        public IQueryable<GUSUARIO> BuscarLista()
        {
            // Busca a lista
            IQueryable<GUSUARIO> lista = (from a in Contexto.Atual.GUSUARIO
                                          select a).Distinct();

            return lista;
        }

        /// <summary>
        /// Função para criar novo
        /// </summary>
        /// <param name="obj">objeto usuário</param>
        /// <returns>true ou false</returns>
        public bool Criar(GUSUARIO obj)
        {
            if (Memoria.Empresa != 0)
            {
                // atualiza data de inclusão e alteração
                obj.dataInclusao = DateTime.Now;
                obj.dataAlteracao = DateTime.Now;

                // adiciona usuário
                Contexto.Atual.AddToGUSUARIO(obj);

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
        /// <param name="obj">objeto usuário</param>
        /// <returns>true ou false</returns>
        public bool Atualizar(GUSUARIO obj)
        {
            // atualiza data de alteração
            obj.dataAlteracao = DateTime.Now;

            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();
            return true;
        }

        /// <summary>
        /// Função que verifica existência de usuário
        /// </summary>
        /// <param name="obj">objeto usuário</param>
        /// <param name="sistema">módulo que deseja acessar</param>
        /// <returns>objeto usuário</returns>
        public GUSUARIO Verificar(GUSUARIO obj)
        {
            // Busca usuário
            GUSUARIO usuario = Contexto.Atual.GUSUARIO.SingleOrDefault(a => a.codusuario == obj.codusuario
                                                                            && a.ativo == true
                                                                            && a.senha == obj.senha);
            if (usuario != null)
            {
                if (usuario.GUSRFILMOD.Any(a => a.codSistema == Memoria.CodSistema))
                    return usuario;
                else
                    return null;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Função que troca o status para inativo
        /// </summary>
        /// <param name="obj">objeto usuário</param>
        /// <returns>true ou false</returns>
        public bool Cancelar(GUSUARIO obj)
        {
            obj.ativo = false;
            return Atualizar(obj);
        }

        /// <summary>
        /// Busca o perfil do usuário
        /// </summary>
        /// <param name="obj">Objeto GUSUARIO</param>
        /// <returns>um objeto GPERFIL</returns>
        public GPERFIL BuscarPerfil(GUSUARIO obj)
        {
            GUSRFILMOD filmod = obj.GUSRFILMOD.ToList().SingleOrDefault(r => r.idEmpresa == Memoria.Empresa
                                                                             && r.codUsuario == obj.codusuario
                                                                             && r.idFilial == Memoria.Filial
                                                                             && r.codSistema == Memoria.CodSistema);


            return filmod.GPERFIL;
        }

        /// <summary>
        /// Faz uma busca no banco e retorna todos os sitemas que o usuário tem acesso.
        /// </summary>
        /// <param name="obj">GUSUÁRIO</param>
        /// <returns>Lista de objetos GSISTEMA.</returns>
        public IQueryable<GSISTEMA> BuscarSistemaPerfil(GUSUARIO obj)
        {
            IQueryable<GSISTEMA> filmod = from a in Contexto.Atual.GUSRFILMOD
                                          join s in Contexto.Atual.GSISTEMA on a.codSistema equals s.codSistema
                                          where (a.idEmpresa == Memoria.Empresa || Memoria.Empresa == 0)
                                                && a.codUsuario == obj.codusuario
                                                && (a.idFilial == Memoria.Filial || Memoria.Filial == 0)
                                          select s;
            return filmod;
        }

        /// <summary>
        /// Faz uma busca no banco e retorna todos os sitemas que o usuário tem acesso.
        /// </summary>
        /// <param name="obj">GUSUÁRIO</param>
        /// <returns>Lista de objetos GSISTEMA.</returns>
        public IQueryable<GUSRFILMOD> BuscarSistemaPerfil2(GUSUARIO obj)
        {
            IQueryable<GUSRFILMOD> filmod = from a in Contexto.Atual.GUSRFILMOD
                                            join s in Contexto.Atual.GSISTEMA on a.codSistema equals s.codSistema
                                            where (a.idEmpresa == Memoria.Empresa || Memoria.Empresa == 0)
                                                  && a.codUsuario == obj.codusuario
                                                  && (a.idFilial == Memoria.Filial || Memoria.Filial == 0)
                                            select a;
            return filmod;
        }

        /// <summary>
        /// Função que valida a senha do usuário, para saber se o mesmo precisa ou não alterar a senha
        /// </summary>
        /// <param name="obj">objeto usuário</param>
        /// <returns>returna TRUE, para senha válida, ou FALSE, para senha expirada</returns>
        public bool ValidarSenha(GUSUARIO obj)
        {
            if (obj.dataUpdSenha != null)
            {
                GPERFIL perfil =
                    (from p in Contexto.Atual.GPERFIL select p).Where(r => r.idPerfil == Memoria.Perfil.Value).
                        FirstOrDefault();

                TimeSpan diff = Convert.ToDateTime(obj.dataUpdSenha) - DateTime.Now;

                if (perfil.valSenha != null && perfil.valSenha != 0)
                {
                    if (diff.Days >= perfil.valSenha)
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}