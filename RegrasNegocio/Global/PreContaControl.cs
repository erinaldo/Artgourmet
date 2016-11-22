using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Artebit.Restaurante.Global.AcessoDados.Global;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Global
{
    public class PreContaControl : ControladorBase
    {
        private readonly PreContaDAL dal = new PreContaDAL();

        /// <summary>
        /// Função que gerencia qual o que será executado
        /// </summary>
        /// <param name="obj">objeto conta</param>
        /// <param name="funcao">nome da função a ser executada</param>
        /// <param name="compl">informações complementares</param>
        /// <returns></returns>
        [Obsolete]
        public object ExecutaFuncao(ACONTA obj, Funcoes funcao, List<string> compl)
        {
            try
            {
                // Dependendo do parametro "funcao" será executada uma função diferente
                switch (funcao)
                {
                    case Funcoes.Adicionar:
                        if (compl == null)
                        {
                            return Criar(obj, "0");
                        }
                        else
                        {
                            return Criar(obj, compl[0]);
                        }

                    case Funcoes.Atualizar:
                        if (compl == null)
                        {
                            return Atualizar(obj, "0");
                        }
                        else
                        {
                            return Atualizar(obj, compl[0]);
                        }

                    case Funcoes.Buscar:
                        return Buscar(obj);

                    case Funcoes.BuscarLista:
                        return BuscarLista();

                    case Funcoes.Transferir:
                        if (compl == null)
                        {
                            return Transferir(obj, "0");
                        }
                        else
                        {
                            return Transferir(obj, compl[0]);
                        }

                    case Funcoes.Fechar:
                        return FecharConta(obj);

                    case Funcoes.Imprimir:
                        return Imprimir(obj);

                    case Funcoes.Cancelar:
                        return Cancelar(obj);

                    case Funcoes.BuscaPorMesa:
                        return BuscarPorMesa(Convert.ToInt32(compl[0]));

                    case Funcoes.ValidaMesaAssociada:
                        return ValidarMesaAssociada(obj, compl[0]);

                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                using (var sw = new StreamWriter("C:\\logErroPreConta.txt", true))
                {
                    sw.WriteLine("Id Preconta: " + Convert.ToString(obj.idConta) + " - " + DateTime.Now.ToString());
                    sw.WriteLine("Usuário: " + Memoria.Codusuario);
                    sw.WriteLine("Ação: " + funcao.ToString());
                    foreach (ACONTITEM item in obj.ACONTITEM)
                    {
                        sw.WriteLine("Item " + item.nuItem + ": " + item.idProduto.ToString() + " - Status: " +
                                     item.idStatus.ToString() + " - Idpai: " + item.nuItemPai);
                    }
                }


                Excecao.TratarExcecao(ex);
                Memoria.MsgGlobal = ex.Message;
                return false;
                throw ex;
            }
        }

        public bool ValidarMesaAssociada(ACONTA obj, string nuMesa)
        {
            return dal.ValidarMesaAssociada(obj, nuMesa);
        }

        public bool Cancelar(ACONTA obj)
        {
            return dal.Cancelar(obj);
        }

        public bool JuntarMesa(ACONTA conta, int nuMesa)
        {
            return dal.JuntarMesa(conta, nuMesa);
        }

        public bool SepararMesa(ACONTA conta, int nuMesa)
        {
            return dal.SepararMesa(conta, nuMesa);
        }

        public bool Transferir(ACONTA obj, string contanova)
        {
            return dal.Transferir(obj, contanova);
        }

        public bool Transferir(ACONTA contaAtual, int destino)
        {
            return dal.Transferir(contaAtual, destino);
        }

        public bool Transferir(ACONTA contaAtual, int destino, List<ACONTITEM> itens)
        {
            return dal.Transferir(contaAtual, destino, itens);
        }

        public ACONTA BuscarPorMesa(int nuMesa)
        {
            return dal.BuscarPorMesa(nuMesa);
        }

        public ACONTA BuscarPorCartao(int nuCartao)
        {
            return dal.BuscarPorCartao(nuCartao);
        }

        public ACONTA BuscarPorId(int idConta)
        {
            return dal.BuscarPorId(idConta);
        }

        /// <summary>
        /// Função para criar
        /// </summary>
        /// <param name="obj">objeto conta</param>
        /// <returns>true ou false</returns>
        public Boolean Criar(ACONTA obj, string trans)
        {
            return dal.Criar(obj, trans);
        }

        public Boolean Criar(ACONTA obj)
        {
            return dal.Criar(obj);
        }

        /// <summary>
        /// Função para buscar
        /// </summary>
        /// <param name="obj">objeto conta</param>
        /// <returns>objeto conta</returns>
        public ACONTA Buscar(ACONTA obj)
        {
            return dal.Buscar(obj);
        }

        /// <summary>
        /// Função para buscar uma lista
        /// </summary>
        /// <returns>Lista de objetos</returns>
        public IQueryable<ACONTA> BuscarLista()
        {
            return dal.BuscarLista();
        }

        /// <summary>
        /// Função para atualizar
        /// </summary>
        /// <param name="obj">objeto conta</param>
        /// <returns>true ou false</returns>
        public bool Atualizar(ACONTA obj, string trans)
        {
            return dal.Atualizar(obj, trans);
        }

        /// <summary>
        /// Função para atualizar
        /// </summary>
        /// <param name="obj">objeto conta</param>
        /// <returns>true ou false</returns>
        public bool Atualizar(ACONTA obj)
        {
            return dal.Atualizar(obj);
        }

        public bool FecharConta(ACONTA obj)
        {
            return dal.FecharConta(obj);
        }

        public int Imprimir(ACONTA obj)
        {
            dal.ImprimirPreContaFila(obj);
            return 1;
        }
    }
}