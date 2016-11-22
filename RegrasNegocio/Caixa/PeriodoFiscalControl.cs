using System;
using System.Collections.Generic;
using Artebit.Restaurante.Global.AcessoDados.Caixa;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Caixa
{
    public class PeriodoFiscalControl : ControladorBase
    {
        private readonly PeriodoFiscalDAL dal = new PeriodoFiscalDAL();

        /// <summary>
        /// Função que gerencia qual o que será executado
        /// </summary>
        /// <param name="obj">objeto</param>
        /// <param name="funcao">nome da função a ser executada</param>
        /// <param name="compl">informações complementares</param>
        /// <returns></returns>
        [Obsolete]
        public object ExecutaFuncao(APERIODOFISCAL obj, Funcoes funcao, List<string> compl)
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

                    case Funcoes.BuscarAtual:
                        return BuscarAtual();

                    case Funcoes.Fechar:
                        return FecharAtual();

                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                Excecao.TratarExcecao(ex);
                Memoria.MsgGlobal = ex.Message;
                return false;
            }
        }

        public bool FecharAtual()
        {
            return dal.FecharPeriodoAtual();
        }

        public APERIODOFISCAL BuscarAtual()
        {
            return dal.PegarPeriodoAtivo();
        }

        public bool Atualizar(APERIODOFISCAL obj)
        {
            return dal.Atualizar(obj);
        }

        public bool Criar(APERIODOFISCAL obj)
        {
            return dal.Criar(obj);
        }
    }
}