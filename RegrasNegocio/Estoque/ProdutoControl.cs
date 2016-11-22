using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.AcessoDados.Estoque;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Estoque

{
    public class ProdutoControl
    {
        /// <summary>
        /// Instancia da classe produtoDAL
        /// </summary>
        private readonly ProdutoDAL dal = new ProdutoDAL();

        [Obsolete]
        public object ExecutaFuncao(EPRODUTO obj, Funcoes funcao, List<string> compl)
        {
            try
            {
                // Dependendo do parametro "funcao" será executada uma função diferente
                switch (funcao)
                {
                    case Funcoes.BuscarLista:
                        return BuscarLista();

                    case Funcoes.BuscarListaEspecifica:
                        return BuscarListaEspecifica(obj);

                    case Funcoes.Buscar:
                        return Buscar(obj);

                    case Funcoes.Adicionar:
                        return Criar(obj);

                    case Funcoes.BuscarItem:
                        return BuscarItem();

                    case Funcoes.BuscarUndMedida:
                        return BuscarUndMedida();

                    case Funcoes.Atualizar:
                        return Atualizar(obj);

                    case Funcoes.Cancelar:
                        return Desativar(obj);

                    case Funcoes.BuscarAdicional:
                        return BuscarAdicionais(obj);

                    case Funcoes.BuscarListaProdAdd:
                        return BuscarListaProdAdd(obj);

                    case Funcoes.BuscarPreco:
                        return BuscarPreco(obj);

                    case Funcoes.BuscarInsumo:
                        return BuscarInsumo(obj);

                    case Funcoes.BuscarCustoMedio:
                        return BuscarCustoMedio(obj);

                    case Funcoes.BuscarEstoque:
                        return BuscarEstoque(obj);

                    case Funcoes.Fechar:
                        dal.AtualizarBaseTemp();
                        return null;

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

        public ECTESTOQUE BuscarEstoque(EPRODUTO obj)
        {
            return dal.BuscarQuantidadeAtualEstoque(obj);
        }

        public EPRODUTO Buscar(EPRODUTO obj)
        {
            return dal.Buscar(obj);
        }

        public EPRODUTO Buscar(int idProduto)
        {
            return dal.Buscar(idProduto);
        }

        //Metodo que retorna a lista de Produtos

        public IQueryable<EPRODUTO> BuscarLista()
        {
            return dal.BuscarLista();
        }

        public IQueryable<EPRODUTO> BuscarListaEspecifica(EPRODUTO obj)
        {
            return dal.BuscarListaEspecifica(obj);
        }

        public bool Criar(EPRODUTO obj)
        {
            return dal.Criar(obj);
        }

        public IQueryable<ETIPOITEM> BuscarItem()
        {
            return dal.BuscarItem();
        }

        public IQueryable<EUNIDADE> BuscarUndMedida()
        {
            return dal.BuscarUndMedida();
        }

        public bool Atualizar(EPRODUTO obj)
        {
            return dal.Atualizar(obj);
        }

        public bool Desativar(EPRODUTO obj)
        {
            return dal.Desativar(obj);
        }

        public List<EPRODUTO> BuscarAdicionais(EPRODUTO obj)
        {
            return dal.BuscarAdicionais(obj);
        }

        public List<EPRODADD> BuscarListaProdAdd(EPRODUTO obj)
        {
            return dal.BuscarListaProdAdd(obj);
        }

        public ETABPRECO BuscarPreco(EPRODUTO obj)
        {
            return dal.BuscarPreco(obj);
        }

        public List<ERECURSO> BuscarInsumo(EPRODUTO obj)
        {
            return dal.BuscarInsumo(obj);
        }

        public ECTESTOQUE BuscarCustoMedio(EPRODUTO obj)
        {
            return dal.BuscarCustoMedio(obj);
        }
    }
}