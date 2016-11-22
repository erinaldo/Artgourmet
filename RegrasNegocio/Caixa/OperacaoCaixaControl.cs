using System;
using System.Collections.Generic;
using Artebit.Restaurante.Global.AcessoDados.Caixa.ECF;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Caixa
{
    public class OperacaoCaixaControl : ControladorBase
    {
        private readonly OperacaoCaixaDAL dal = new OperacaoCaixaDAL();


        /// <summary>
        /// Função que gerencia qual o que será executado
        /// </summary>
        /// <param name="obj">objeto</param>
        /// <param name="funcao">nome da função a ser executada</param>
        /// <param name="compl">informações complementares</param>
        /// <returns></returns>
        [Obsolete]
        public object ExecutaFuncao(AOPCAIXA obj, Funcoes funcao, List<string> compl)
        {
            try
            {
                // Dependendo do parametro "funcao" será executada uma função diferente
                switch (funcao)
                {
                    case Funcoes.Adicionar:
                        return Criar(obj);

                    case Funcoes.BuscarItem:
                        return dal.GerarIdReducao();

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

        /// <summary>
        /// Cria uma nova operação de caixa
        /// </summary>
        /// <param name="obj">objeto operação Caixa</param>
        /// <returns>True-> executado com sucesso, caso contrário-> False</returns>
        public bool Criar(AOPCAIXA obj)
        {
            return dal.Criar(obj);
        }
    }
}