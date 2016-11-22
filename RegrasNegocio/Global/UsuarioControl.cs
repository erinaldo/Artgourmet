using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.AcessoDados.Global;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Global
{
    public class UsuarioControl : ControladorBase
    {
        private readonly UsuarioDAL dal = new UsuarioDAL();


        /// <summary>
        /// Função que gerencia qual o que será executado
        /// </summary>
        /// <param name="obj">objeto usuario</param>
        /// <param name="funcao">nome da função a ser executada</param>
        /// <param name="compl">informações complementares, 0 -> sistema</param>
        /// <returns></returns>
        [Obsolete]
        public object ExecutaFuncao(GUSUARIO obj, Funcoes funcao, List<string> compl)
        {
            try
            {
                // Dependendo do parametro "funcao" será executada uma função diferente
                switch (funcao)
                {
                    case Funcoes.Adicionar:
                        return Criar(obj);

                    case Funcoes.Atualizar:
                        return Atualizar(obj);

                    case Funcoes.Buscar:
                        return Buscar(obj);

                    case Funcoes.BuscarLista:
                        return BuscarLista();

                    case Funcoes.Verificar:
                        return Verificar(obj);

                    case Funcoes.Cancelar:
                        return Cancelar(obj);

                    case Funcoes.BuscaPerfil:
                        return BuscarPerfil(obj);

                    case Funcoes.BuscaSistemaPerfil:
                        return BuscarSistemaPerfil(obj);

                    case Funcoes.BuscaSistemaPerfil2:
                        return BuscarSistemaPerfil2(obj);

                    case Funcoes.ValidaSenha:
                        return ValidarSenha(obj);

                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                Excecao.TratarExcecao(ex);
                return null;
            }
        }

        public GPERFIL BuscarPerfil(GUSUARIO obj)
        {
            return dal.BuscarPerfil(obj);
        }

        /// <summary>
        /// Função para criar
        /// </summary>
        /// <param name="obj">objeto usuario</param>
        /// <returns>true ou false</returns>
        public Boolean Criar(GUSUARIO obj)
        {
            return dal.Criar(obj);
        }

        /// <summary>
        /// Função para buscar
        /// </summary>
        /// <param name="obj">objeto usuario</param>
        /// <returns>objeto usuário</returns>
        public GUSUARIO Buscar(GUSUARIO obj)
        {
            return dal.Buscar(obj);
        }

        /// <summary>
        /// Função para buscar uma lista
        /// </summary>
        /// <returns>Lista de objetos</returns>
        public IQueryable<GUSUARIO> BuscarLista()
        {
            return dal.BuscarLista();
        }

        /// <summary>
        /// Função para atualizar
        /// </summary>
        /// <param name="obj">objeto usuario</param>
        /// <returns>true ou false</returns>
        public bool Atualizar(GUSUARIO obj)
        {
            return dal.Atualizar(obj);
        }

        /// <summary>
        /// Função para verificar
        /// </summary>
        /// <param name="obj">objeto usuário</param>
        /// <param name="sistema">sistema que deseja entrar</param>
        /// <returns>objeto usuário</returns>
        public GUSUARIO Verificar(GUSUARIO obj)
        {
            return dal.Verificar(obj);
        }

        /// <summary>
        /// Função para mudar o status para inativo
        /// </summary>
        /// <param name="obj">obj usuário</param>
        /// <returns>true ou false</returns>
        public bool Cancelar(GUSUARIO obj)
        {
            return dal.Cancelar(obj);
        }

        public IQueryable<GSISTEMA> BuscarSistemaPerfil(GUSUARIO obj)
        {
            return dal.BuscarSistemaPerfil(obj);
        }

        public IQueryable<GUSRFILMOD> BuscarSistemaPerfil2(GUSUARIO obj)
        {
            return dal.BuscarSistemaPerfil2(obj);
        }

        public bool ValidarSenha(GUSUARIO obj)
        {
            return dal.ValidarSenha(obj);
        }
    }
}