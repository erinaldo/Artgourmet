using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.AcessoDados.Global;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Global
{
    public class PerfilControl : ControladorBase
    {
        private readonly PerfilDAL dal = new PerfilDAL();


        /// <summary>
        /// Função que gerencia qual o que será executado
        /// </summary>
        /// <param name="obj">objeto perfil</param>
        /// <param name="funcao">nome da função a ser executada</param>
        /// <param name="compl">informações complementares, 0 -> sistema, 1 -> janela, 2 -. funcionalidade</param>
        /// <returns></returns>
        [Obsolete]
        public object ExecutaFuncao(GPERFIL obj, Funcoes funcao, List<string> compl)
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

                    case Funcoes.BuscarItem:
                        return BuscarItem();

                    case Funcoes.BuscarVazio:
                        return BuscarVazio(obj);

                    case Funcoes.BuscarLista:
                        return BuscarLista();

                    case Funcoes.Verificar:
                        return Verificar(obj, compl[1], Convert.ToInt32(compl[2]));

                    case Funcoes.VerificarJanela:
                        return VerificarJanela(obj, compl[0]);

                    case Funcoes.Cancelar:
                        return cancela(obj);

                    case Funcoes.BuscarListaEspecifica:
                        return BuscarListaEspecifica(obj);

                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                Excecao.TratarExcecao(ex);
                return null;
            }
        }

        /// <summary>
        /// Função para criar
        /// </summary>
        /// <param name="obj">objeto perfil</param>
        /// <returns>true ou false</returns>
        public Boolean Criar(GPERFIL obj)
        {
            return dal.Criar(obj);
        }

        /// <summary>
        /// Função para buscar
        /// </summary>
        /// <param name="obj">objeto perfil</param>
        /// <returns>objeto perfil</returns>
        public GPERFIL Buscar(GPERFIL obj)
        {
            return dal.Buscar(obj);
        }

        /// <summary>
        /// Função para buscar
        /// </summary>
        /// <param name="idPerfil"> </param>
        /// <returns>objeto perfil</returns>
        public GPERFIL Buscar(int idPerfil)
        {
            return dal.Buscar(idPerfil);
        }

        /// <summary>
        /// Função para buscar o perfil do usuário na empresa e filial
        /// </summary>
        /// <param name="obj">objeto perfil</param>
        /// <returns>objeto perfil</returns>
        public GPERFIL BuscarItem()
        {
            return dal.BuscarEspecifica();
        }

        /// <summary>
        /// Função para buscar uma lista
        /// </summary>
        /// <returns>Lista de objetos</returns>
        public IQueryable<GPERFIL> BuscarLista()
        {
            return dal.BuscarLista();
        }

        public IQueryable<GPERFIL> BuscarListaEspecifica(GPERFIL obj)
        {
            return dal.BuscarListaEspecifica(obj);
        }

        /// <summary>
        /// Função para buscar objeto sem as ligações vazio
        /// </summary>
        /// <param name="obj">objeto perfil</param>
        /// <returns>objeto perfil</returns>
        public GPERFIL BuscarVazio(GPERFIL obj)
        {
            return dal.BuscarVazio(obj);
        }

        /// <summary>
        /// Função para atualizar
        /// </summary>
        /// <param name="obj">objeto perfil</param>
        /// <returns>true ou false</returns>
        public bool Atualizar(GPERFIL obj)
        {
            return dal.Atualizar(obj);
        }

        /// <summary>
        /// Função para verificar permissão
        /// </summary>
        /// <param name="obj">objeto perfil</param>
        /// <param name="sistema">sistema atual</param>
        /// <param name="janela">janela atual</param>
        /// <param name="funcionalidade">funcionalidade que deseja saber a permissão</param>
        /// <returns>true ou false</returns>
        public bool Verificar(GPERFIL obj, string janela, int funcionalidade)
        {
            return dal.Verificar(obj, janela, funcionalidade);
        }

        /// <summary>
        /// Função para verificar se tem acesso à janela
        /// </summary>
        /// <param name="obj">objeto perfil</param>
        /// <param name="sistema">sistema atual</param>
        /// <param name="janela">janela atual</param>
        /// <returns>true ou false</returns>
        public bool VerificarJanela(GPERFIL obj, string janela)
        {
            if (obj != null)
            {
                return dal.VerificarJanela(obj, janela);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Função para mudar o status para inativo
        /// </summary>
        /// <param name="obj">obj perfil</param>
        /// <returns>true ou false</returns>
        public bool cancela(GPERFIL obj)
        {
            return dal.Cancelar(obj);
        }
    }
}