using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.AcessoDados.Global;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Global
{
    public class JanelaControl : ControladorBase
    {
        private readonly JanelaDAL dal = new JanelaDAL();


        /// <summary>
        /// Função que gerencia qual o que será executado
        /// </summary>
        /// <param name="obj">objeto grupo mesa</param>
        /// <param name="funcao">nome da função a ser executada</param>
        /// <param name="compl">informações complementares</param>
        /// <returns></returns>
        [Obsolete]
        public object ExecutaFuncao(GJANELA obj, Funcoes funcao, List<string> compl)
        {
            try
            {
                // Dependendo do parametro "funcao" será executada uma função diferente
                switch (funcao)
                {
                    case Funcoes.Buscar:
                        return Buscar(obj);

                    case Funcoes.BuscarLista:
                        return BuscarLista();


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
        /// Função para buscar
        /// </summary>
        /// <param name="obj">objeto grupo mesa</param>
        /// <returns>objeto grupo mesa</returns>
        public GJANELA Buscar(GJANELA obj)
        {
            return dal.Buscar(obj);
        }

        /// <summary>
        /// Função para buscar uma lista
        /// </summary>
        /// <returns>Lista de objetos</returns>
        public IQueryable<GJANELA> BuscarLista()
        {
            return dal.BuscarLista();
        }
    }
}