using System;
using System.Collections.Generic;
using Artebit.Restaurante.Global.AcessoDados.Caixa;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Caixa
{
    public class FormaPagtoControl
    {
        private readonly FormaPagtoDAL dal = new FormaPagtoDAL();

        /// <summary>
        /// Função que gerencia qual o que será executado
        /// </summary>
        /// <param name="obj">objeto</param>
        /// <param name="funcao">nome da função a ser executada</param>
        /// <param name="compl">informações complementares</param>
        /// <returns></returns>
        [Obsolete]
        public object ExecutaFuncao(AFORMAPGTO obj, Funcoes funcao, List<string> compl)
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

                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                Excecao.TratarExcecao(ex);
                Memoria.MsgGlobal = ex.Message;
                return false;
            }
        }

        public bool Atualizar(AFORMAPGTO obj)
        {
            return dal.Atualizar(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">objeto </param>
        /// <returns>True-> executado com sucesso, caso contrário-> False</returns>
        public bool Criar(AFORMAPGTO obj)
        {
            return dal.Criar(obj);
        }
    }
}