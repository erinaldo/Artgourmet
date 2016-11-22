using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.AcessoDados.Compras;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Compras
{
    public class CondPagtoControl : ControladorBase
    {
        private readonly CondPagtoDAL dal = new CondPagtoDAL();


        /// <summary>
        /// Função que gerencia qual o que será executado
        /// </summary>
        /// <param name="obj">objeto usuario</param>
        /// <param name="funcao">nome da função a ser executada</param>
        /// <param name="compl">informações complementares, 0 -> tipo de movimento : para a função Buscar()</param>
        /// <param name="compl">informações complementares, 0 -> idEmpres; 1 -> idFilial; 2 -> idMov : para a função Criar()</param>
        /// <returns></returns>
        [Obsolete]
        public object ExecutaFuncao(CCONDPAGTO obj, Funcoes funcao, List<string> compl)
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

                        //case Funcoes.Cancelar:
                        //    return cancela(obj);

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

        /// <summary>
        /// Função para criar
        /// </summary>
        /// <param name="obj">objeto movimento</param>
        /// <returns>true ou false</returns>
        public Boolean Criar(CCONDPAGTO obj)
        {
            return dal.Criar(obj);
        }

        /// <summary>
        /// Função para buscar
        /// </summary>
        /// <param name="obj">objeto usuario</param>
        /// <returns>objeto usuário</returns>
        public CCONDPAGTO Buscar(CCONDPAGTO obj)
        {
            return dal.Buscar(obj);
        }

        /// <summary>
        /// Função para buscar uma lista
        /// </summary>
        /// <returns>Lista de objetos</returns>
        public IQueryable<CCONDPAGTO> BuscarLista()
        {
            return dal.BuscarLista();
        }

        /// <summary>
        /// Função para atualizar
        /// </summary>
        /// <param name="obj">objeto usuario</param>
        /// <returns>true ou false</returns>
        public bool Atualizar(CCONDPAGTO obj)
        {
            return dal.Atualizar(obj);
        }

        /// <summary>
        /// Função para mudar o status para cancelado
        /// </summary>
        /// <param name="obj">obj movimento</param>
        /// <returns>true ou false</returns>
        //private bool cancela(CMOVIMENTO obj)
        //{
        //    return dal.Cancelar(obj);
        //}
    }
}