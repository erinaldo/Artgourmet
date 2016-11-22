using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Estoque
{
    public class ProdutoDAL
    {
        public IQueryable<EPRODUTO> BuscarLista()
        {
            // Busca a lista de produtos
            IQueryable<EPRODUTO> produtos = from a in Contexto.Atual.EPRODUTO
                                            where (a.idEmpresa == Memoria.Empresa || Memoria.Empresa == 0)
                                            select a;

            return produtos;
        }

        public IQueryable<EPRODUTO> BuscarListaEspecifica(EPRODUTO obj)
        {
            // Busca a lista de mesas
            IQueryable<EPRODUTO> produtos = from a in Contexto.Atual.EPRODUTO
                                            where
                                                ((obj.nome.Contains(a.codigo) || obj.nome.Contains(a.nome)) ||
                                                 (obj.nome == null || obj.nome == "") || (a.nome.Contains(obj.nome))
                                                 && (a.idEmpresa == Memoria.Empresa || Memoria.Empresa == 0)
                                                 && (a.ativo == true))
                                            select a;

            return produtos;
        }

        public EPRODUTO Buscar(EPRODUTO obj)
        {
            EPRODUTO prod =
                Contexto.Atual.EPRODUTO.SingleOrDefault(
                    r => r.idEmpresa == obj.idEmpresa && r.idProduto == obj.idProduto);

            return prod;
        }

        public EPRODUTO Buscar(int idProduto)
        {
            EPRODUTO prod =
                Contexto.Atual.EPRODUTO.SingleOrDefault(
                    r => r.idEmpresa == Memoria.Empresa && r.idProduto == idProduto);

            return prod;
        }

        public bool Criar(EPRODUTO obj)
        {
            obj.ativo = true;

            //Atualizando as data
            obj.dataAlteracao = DateTime.Now;
            obj.dataCriacao = DateTime.Now;
            obj.usuarioCriacao = Memoria.Codusuario;

            //Alterado o ID do produto adicional
            foreach (EPRODADD p in obj.EPRODADD)
            {
                p.idProduto = obj.idProduto;
            }


            //Alterando o ID do recurso(produto adiconado em ficha técnica)
            foreach (ERECURSO re in obj.ERECURSO)
            {
                re.idProdPai = obj.idProduto;
            }


            //Alterando o ID do preço
            foreach (ETABPRECO preco in obj.ETABPRECO)
            {
                preco.idTabPreco = Contexto.GerarId("ETABPRECO");
                preco.dataAlteracao = DateTime.Now;
                preco.idProduto = obj.idProduto;
            }

            CriarListaInsumosMestre(ref obj);

            // adiciona produto
            Contexto.Atual.AddToEPRODUTO(obj);

            // salva alterações
            Contexto.Atual.SaveChanges();

            return true;
        }

        public IQueryable<ETIPOITEM> BuscarItem()
        {
            IQueryable<ETIPOITEM> itens = from a in Contexto.Atual.ETIPOITEM
                                          select a;
            return itens;
        }

        public IQueryable<EUNIDADE> BuscarUndMedida()
        {
            IQueryable<EUNIDADE> itens = from a in Contexto.Atual.EUNIDADE
                                         select a;
            return itens;
        }

        public bool Atualizar(EPRODUTO obj)
        {
            //CriaListaInsumosMestre(ref obj);

            obj.dataAlteracao = DateTime.Now;
            obj.usuarioAlteracao = Memoria.Codusuario;
            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();
            return true;
        }

        public bool Desativar(EPRODUTO obj)
        {
            obj.ativo = false;
            return Atualizar(obj);
        }

        public List<EPRODUTO> BuscarAdicionais(EPRODUTO obj)
        {
            IQueryable<EPRODADD> itens = from e in Contexto.Atual.EPRODADD
                                         where (e.idProduto == obj.idProduto)
                                         select e;

            var lista = new List<EPRODUTO>();

            foreach (EPRODADD iten in itens)
            {
                var produto = new EPRODUTO();
                produto.idProduto = iten.idPrdAdd;
                produto.idEmpresa = iten.idEmpresa;
                produto = Buscar(produto);
                lista.Add(produto);
            }

            return lista;
        }

        public List<EPRODADD> BuscarListaProdAdd(EPRODUTO obj)
        {
            IQueryable<EPRODADD> lista = from proAdd in Contexto.Atual.EPRODADD
                                         where (obj.idProduto == proAdd.idProduto)
                                         select proAdd;


            //Convertendo em lista, pois na interface soh trabalha com lista
            List<EPRODADD> returLista = lista.ToList();
            return returLista;
        }

        public ETABPRECO BuscarPreco(EPRODUTO obj)
        {
            ETABPRECO preco = Contexto.Atual.ETABPRECO.SingleOrDefault(
                r => r.idEmpresa == obj.idEmpresa &&
                     r.idProduto == obj.idProduto &&
                     r.ativo
                );

            return preco;
        }

        public ECTESTOQUE BuscarCustoMedio(EPRODUTO obj)
        {
            ECTESTOQUE preco = Contexto.Atual.ECTESTOQUE.Where(r => r.idProduto == obj.idProduto &&
                                                                    r.idEmpresa == obj.idEmpresa).AsEnumerable().Last();

            return preco;
        }


        public List<ERECURSO> BuscarInsumo(EPRODUTO obj)
        {
            IQueryable<ERECURSO> lista = from a in Contexto.Atual.ERECURSO
                                         where (obj.idProduto == a.idProdPai && obj.idEmpresa == a.idEmpresa)
                                         select a;

            //Convertendo a Iqueryable em List
            List<ERECURSO> listaRetorno = lista.ToList();
            return listaRetorno;
        }


        public ECTESTOQUE BuscarQuantidadeAtualEstoque(EPRODUTO obj)
        {
            IQueryable<ECTESTOQUE> ret =
                Contexto.Atual.ECTESTOQUE.Where(r => r.idEmpresa == Memoria.Empresa && r.idProduto == obj.idProduto);

            if (ret.Count() > 0)
            {
                return ret.OrderByDescending(r => r.idCtEstoque).First();
            }
            else
            {
                return null;
            }
        }

        private void CriarListaInsumosMestre(ref EPRODUTO obj)
        {
            var lista = new List<EPRDLISTA>();

            TratarListaInsumos(obj.idProduto, obj.idProduto, 1, ref lista);

            obj.EPRDLISTA.Clear();

            foreach (EPRDLISTA item in lista)
            {
                obj.EPRDLISTA.Add(item);
            }
        }

        private void CriarListaInsumosMestre(EPRODUTO obj)
        {
            var lista = new List<EPRDLISTA>();

            TratarListaInsumos(obj.idProduto, obj.idProduto, 1, ref lista);

            obj.EPRDLISTA.Clear();

            int cont = 1;
            foreach (EPRDLISTA item in lista.Distinct())
            {
                item.NroItem = cont;
                obj.EPRDLISTA.Add(item);
                cont++;
            }
        }

        private void TratarListaInsumos(int idProdutoRaiz, int idProduto, decimal? quantidade, ref List<EPRDLISTA> lista)
        {
            EPRODUTO prod =
                Contexto.Atual.EPRODUTO.SingleOrDefault(r => r.idEmpresa == Memoria.Empresa && r.idProduto == idProduto);

            if (prod != null)
            {
                foreach (ERECURSO item in prod.ERECURSO)
                {
                    var p = new EPRDLISTA();

                    p.IdEmpresa = item.idEmpresa;
                    p.IdProduto = idProdutoRaiz;

                    p.IdInsumo = item.idProdFilho;

                    p.Qtde = item.quantidade.Value*quantidade.Value;
                    p.CodUnd = item.codUnd;

                    lista.Add(p);

                    TratarListaInsumos(idProdutoRaiz, item.idProdFilho, p.Qtde, ref lista);
                }
            }
        }

        public void AtualizarBaseTemp()
        {
            Contexto.Atual = new Modelo.Restaurante();
            foreach (EPRODUTO prd in Contexto.Atual.EPRODUTO.Where(r => r.ERECURSO.Count > 0))
            {
                CriarListaInsumosMestre(prd);
                Contexto.Atual.ObjectStateManager.ChangeObjectState(prd, EntityState.Modified);
            }

            Contexto.Atual.SaveChanges();
        }
    }
}