using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Estoque
{
    public class CardapioItemDAL
    {
        /// <summary>
        /// Função para buscar apenas um objeto completo
        /// </summary>
        /// <param name="obj">objeto</param>
        /// <returns>objeto</returns>
        public ECARDAPIOITEM BuscarUm(ECARDAPIOITEM obj)
        {
            if (Memoria.Empresa != 0 && Memoria.Empresa != 0)
            {
                // Busca
                ECARDAPIOITEM obj2 = Contexto.Atual.ECARDAPIOITEM.FirstOrDefault(a => (a.idCardapio == obj.idCardapio ||
                                                                                       obj.idCardapio == 0)
                                                                                      //&& 
                                                                                      //  (a.idPrdCat1 == obj.idPrdCat1 ||
                                                                                      //  obj.idPrdCat1 == null)
                                                                                      //&&
                                                                                      //  (a.idPrdCat2 == obj.idPrdCat2 ||
                                                                                      //  obj.idPrdCat2 == null)
                                                                                      //&&
                                                                                      //  (a.idPrdCat3 == obj.idPrdCat3 ||
                                                                                      //  obj.idPrdCat3 == null)
                                                                                      //&&
                                                                                      //  (a.localPreco == obj.localPreco ||
                                                                                      //  obj.localPreco == 0)
                                                                                      &&
                                                                                      (a.nuPreco == obj.nuPreco ||
                                                                                       obj.nuPreco == null)
                                                                                      &&
                                                                                      (a.idEmpresa == Memoria.Empresa ||
                                                                                       Memoria.Empresa == 0)
                                                                                      &&
                                                                                      (a.idFilial == Memoria.Filial ||
                                                                                       Memoria.Filial == 0)
                    );

                return obj2;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Função para buscar uma lista de objetos mesa com parametros
        /// </summary>
        /// <returns>lista de objetos</returns>
        public IQueryable<ECARDAPIOITEM> BuscarListaEspecifica(ECARDAPIOITEM obj)
        {
            // Busca o mesa
            IQueryable<ECARDAPIOITEM> cardapio = from a in Contexto.Atual.ECARDAPIOITEM
                                                 where (a.idEmpresa == Memoria.Empresa)
                                                       && (a.idFilial == Memoria.Filial)
                                                 //      && (a.grupo == obj.grupo || obj.grupo == null)
                                                 //      && (a.idPrdCat1 == obj.idPrdCat1 || obj.idPrdCat1 == null)
                                                 //      && (a.idPrdCat2 == obj.idPrdCat2 || obj.idPrdCat2 == null)
                                                 //      && (a.idPrdCat3 == obj.idPrdCat3 || obj.idPrdCat3 == null)
                                                       && (a.idCardapio == obj.idCardapio || a.idCardapio == -1 )
                                                 select a;

            return cardapio;
        }

        /// <summary>
        /// Função para buscar uma lista de objetos mesa com parametros
        /// </summary>
        /// <returns>lista de objetos</returns>
        public List<ECARDAPIOITEM> BuscarItensCardapio(int idCardapio)
        {
            // Busca o mesa
            var lista = (from a in Contexto.Atual.ECARDAPIOITEM

                         where (a.idEmpresa == Memoria.Empresa)
                               && (a.idFilial == Memoria.Filial)
                               && (a.idCardapio == idCardapio)
                               && a.usaPreco == true
                               && a.ativo == true
                         select new
                         {
                             a.idItemCard,
                             a.idItemPai,
                             a.idProduto,
                             a.grupo,
                             a.posicao,
                             a.descricao,
                             a.cor,
                             a.usaPreco,
                             a.corFonte,
                             a.nuPreco
                         }).ToList();


            return (from a in lista
                    join b in Memoria.Produtos on a.idProduto equals b.idProduto
                    select new ECARDAPIOITEM()
                    {
                        idItemCard = a.idItemCard,
                        idItemPai = a.idItemPai,
                        idProduto = a.idProduto,
                        grupo = a.posicao,
                        descricao = a.descricao,
                        DescricaoPrd = b.nome,
                        CodigoPrd = b.codigo,
                        cor = a.cor,
                        usaPreco = a.usaPreco,
                        corFonte = a.corFonte,
                        nuPreco = a.nuPreco,
                        ProdutoLight = b
                    }).ToList();
        }


        public decimal BuscarPrecoItem(ECARDAPIOITEM obj)
        {
            //obj = BuscaUm(obj);

            int idPrdPreco = 0;

            if (obj != null)
            {
                ////localizar local do preco
                //switch (obj.localPreco)
                //{
                //    case 1:
                //        idPrdPreco = obj.idPrdCat1.Value;
                //        break;

                //    case 2:
                //        idPrdPreco = obj.idPrdCat2.Value;
                //        break;

                //    case 3:
                //        idPrdPreco = obj.idPrdCat3.Value;
                //        break;

                //    default:
                //        break;
                //};


                //Busca produto o qual o preco esta associado
                var prodDal = new ProdutoDAL();

                //pegar tabela de preco
                ETABPRECO tabPreco = Contexto.Atual.ETABPRECO.FirstOrDefault(r => r.idProduto == idPrdPreco && r.ativo);

                if (tabPreco != null)
                {
                    //pegar preco de acordo com o numero
                    switch (obj.nuPreco)
                    {
                        case 1:
                            return tabPreco.preco1;

                        case 2:
                            return tabPreco.preco2;

                        case 3:
                            return tabPreco.preco3;

                        default:
                            return 0;
                    }
                    ;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public int BuscarIdProdutoItem(ECARDAPIOITEM obj)
        {
            int idPrdPreco = 0;

            //localizar local do preco
            //switch (obj.localPreco)
            //{
            //    case 1:
            //        idPrdPreco = obj.idPrdCat1.Value;
            //        break;

            //    case 2:
            //        idPrdPreco = obj.idPrdCat2.Value;
            //        break;

            //    case 3:
            //        idPrdPreco = obj.idPrdCat3.Value;
            //        break;

            //    default:
            //        break;
            //};

            return idPrdPreco;
        }

        /// <summary>
        /// Busca o cardapio atual
        /// </summary>
        public void BuscarAtual()
        {
            bool? _segunda = null;
            bool? _terca = null;
            bool? _quarta = null;
            bool? _quinta = null;
            bool? _sexta = null;
            bool? _sabado = null;
            bool? _domingo = null;

            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    _segunda = true;
                    break;

                case DayOfWeek.Tuesday:
                    _terca = true;
                    break;

                case DayOfWeek.Wednesday:
                    _quarta = true;
                    break;

                case DayOfWeek.Thursday:
                    _quinta = true;
                    break;

                case DayOfWeek.Friday:
                    _sexta = true;
                    break;

                case DayOfWeek.Saturday:
                    _sabado = true;
                    break;

                case DayOfWeek.Sunday:
                    _domingo = true;
                    break;
            }

            DateTime diaatual = Convert.ToDateTime("01/01/2000 " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":00");

            ECARDAPIO cardapio = Contexto.Atual.ECARDAPIO.SingleOrDefault(c => c.ativo == true
                                                                               && (
                                                                                      (c.segunda == _segunda)
                                                                                      ||
                                                                                      (c.terca == _terca)
                                                                                      ||
                                                                                      (c.quarta == _quarta)
                                                                                      ||
                                                                                      (c.quinta == _quinta)
                                                                                      ||
                                                                                      (c.sexta == _sexta)
                                                                                      ||
                                                                                      (c.sabado == _sabado)
                                                                                      ||
                                                                                      (c.domingo == _domingo)
                                                                                  )
                                                                               && (
                                                                                      c.diaTodo == true
                                                                                      ||
                                                                                      (
                                                                                          c.horInicio <= diaatual &&
                                                                                          c.horFinal >= diaatual
                                                                                      )
                                                                                  )
                );
        }
    }
}