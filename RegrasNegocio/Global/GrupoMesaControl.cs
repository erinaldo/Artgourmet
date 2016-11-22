using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.AcessoDados.Global;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Global
{
    public class GrupoMesaControl : ControladorBase
    {
        private readonly GrupoMesaDAL dal = new GrupoMesaDAL();


        /// <summary>
        /// Função que gerencia qual o que será executado
        /// </summary>
        /// <param name="obj">objeto grupo mesa</param>
        /// <param name="funcao">nome da função a ser executada</param>
        /// <param name="compl">informações complementares</param>
        /// <returns></returns>
        [Obsolete]
        public object ExecutaFuncao(RGRUPOMESA obj, Funcoes funcao, List<string> compl)
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
        /// <param name="obj">objeto grupo mesa</param>
        /// <returns>true ou false</returns>
        public Boolean Criar(RGRUPOMESA obj)
        {
            return dal.Criar(obj);
        }

        /// <summary>
        /// Função para buscar
        /// </summary>
        /// <param name="obj">objeto grupo mesa</param>
        /// <returns>objeto grupo mesa</returns>
        public RGRUPOMESA Buscar(RGRUPOMESA obj)
        {
            return dal.Buscar(obj);
        }

        /// <summary>
        /// Função para buscar uma lista
        /// </summary>
        /// <returns>Lista de objetos</returns>
        public IQueryable<RGRUPOMESA> BuscarLista()
        {
            return dal.BuscarLista();
        }

        /// <summary>
        /// Função para buscar uma lista com parametros
        /// </summary>
        /// <param name="obj">objeto grupo mesa</param>
        /// <returns>lista de objetos</returns>
        public IQueryable<RGRUPOMESA> BuscarListaEspecifica(RGRUPOMESA obj)
        {
            return dal.BuscarListaEspecifica(obj);
        }

        /// <summary>
        /// Função para atualizar
        /// </summary>
        /// <param name="obj">objeto grupo mesa</param>
        /// <returns>true ou false</returns>
        public bool Atualizar(RGRUPOMESA obj)
        {
            return dal.Atualizar(obj);
        }
    }
}