using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.AcessoDados.Global;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Global
{
    public class EmpresaControl : ControladorBase
    {
        private readonly EmpresaDAL dal = new EmpresaDAL();

        /// <summary>
        /// Função que gerencia qual o que será executado
        /// </summary>
        /// <param name="obj">objeto grupo mesa</param>
        /// <param name="funcao">nome da função a ser executada</param>
        /// <param name="compl">informações complementares</param>
        /// <returns></returns>
        [Obsolete]
        public object ExecutaFuncao(GEMPRESA obj, Funcoes funcao, List<string> compl)
        {
            try
            {
                // Dependendo do parametro "funcao" será executada uma função diferente
                switch (funcao)
                {
                    case Funcoes.BuscarAtual:
                        return BuscarAtual();
                    case Funcoes.BuscarLista:
                        return BuscarLista();

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
        /// Função para buscar a empresa atual
        /// </summary>
        /// <returns>Objeto Empresa</returns>
        public GEMPRESA BuscarAtual()
        {
            return dal.BuscarAtual();
        }

        public IQueryable<GEMPRESA> BuscarLista()
        {
            return dal.BuscarLista();
        }
    }
}