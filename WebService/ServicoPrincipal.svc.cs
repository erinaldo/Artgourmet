using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Estoque;
using Artebit.Restaurante.Global.WebService.BL;
using ArtebitGourmet.WebService.BL;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Artebit.Restaurante.Global.Modelo.Enum;


namespace ArtebitGourmet.WebService
{
    public class ServicoPrincipal : IServicoPrincipal
    {

        List<BL.Vendedor> IServicoPrincipal.GetVendedores()
        {
            List<Vendedor> vendedores = (from p in Contexto.Atual.GVENDEDOR
                               join h in Contexto.Atual.GUSRFILMOD on p.codUsuario equals h.codUsuario
                               where h.codSistema == "M"
                               select new BL.Vendedor()
                                          {
                                              Vendedor_ID = p.idVen,
                                              Vendedor_Codigo = p.codigo,
                                              Usuario = p.codUsuario,
                                              Vendedor_Nome = p.nome,
                                              Perfil_ID = h.idPerfil.Value,
                                              PermissoesEnum = (from g in Contexto.Atual.GPERMISSAO
                                                                where g.GPERFIL.Any(r => r.idPerfil == h.idPerfil.Value)
                                                                select new BL.Permissao()
                                                                           {
                                                                               Funcao_ID = g.idFuncionalidade,
                                                                               Janela_ID = g.idJanela,
                                                                               Perfil_ID = h.idPerfil.Value,
                                                                               Permissao_ID = g.idPermissao
                                                                           }).AsEnumerable()
                                          }).ToList();

            foreach (var v in vendedores)
            {
                v.Permissoes = v.PermissoesEnum.ToList();
            }

            return vendedores.ToList();
        }

        public List<BL.ItensCardapio> GetItensCardapio()
        {
            //Memoria.Empresa = 1;
            //Memoria.Filial = 1;

            //List<BL.ItensCardapio> itens = new List<BL.ItensCardapio>();
            
            //ECARDAPIOITEM item = new ECARDAPIOITEM();
            //CardapioItem cardControl = new CardapioItem();
            //Cardapio Control = new Cardapio();
            //IQueryable<ECARDAPIOITEM> itensCardapio = null;

            //item.idCardapio = (int)Control.ExecutaFuncao(null, Funcoes.BuscarAtual, null);
            
            //itensCardapio =
            //    (IQueryable<ECARDAPIOITEM>)cardControl.ExecutaFuncao(item, Funcoes.BuscarListaEspecifica, null);

            //itensCardapio = itensCardapio.Where(i => i.idCardapio == item.idCardapio);

            //itens = (from p in itensCardapio
            //         select new BL.ItensCardapio()
            //         {
            //             Item_ID = p.idItemCard,
            //             Grupo = p.grupo,
            //             LocalPreco = p.localPreco,

            //             PrdCat1_ID = p.idPrdCat1,
            //             PrdCat1_Codigo = p.EPRODUTO != null ? p.EPRODUTO.codigo : null,
            //             PrdCat1_Cor = p.EPRODUTO != null ? p.EPRODUTO.corPDV: null,
            //             PrdCat1_Nome = p.EPRODUTO != null ? p.EPRODUTO.nomeResumo ?? p.EPRODUTO.nome : null,

            //             PrdCat2_ID = p.idPrdCat2,
            //             PrdCat2_Codigo = p.EPRODUTO2 != null ? p.EPRODUTO2.codigo : null,
            //             PrdCat2_Cor = p.EPRODUTO2 != null ? p.EPRODUTO2.corPDV : null,
            //             PrdCat2_Nome = p.EPRODUTO2 != null ? p.EPRODUTO2.nomeResumo ?? p.EPRODUTO2.nome : null,

            //             PrdCat3_ID = p.idPrdCat3,
            //             PrdCat3_Codigo = p.EPRODUTO1 != null ? p.EPRODUTO1.codigo : null,
            //             PrdCat3_Cor = p.EPRODUTO1 != null ? p.EPRODUTO1.corPDV : null,
            //             PrdCat3_Nome = p.EPRODUTO1 != null ? p.EPRODUTO1.nomeResumo ?? p.EPRODUTO1.nome : null,


            //             Preco = (from h in Contexto.Atual.ETABPRECO
            //                      where (h.idProduto == p.idPrdCat1 && p.localPreco == 1)
            //                      || (h.idProduto == p.idPrdCat2 && p.localPreco == 2)
            //                      || (h.idProduto == p.idPrdCat3 && p.localPreco == 3)
            //                      select h.preco1).FirstOrDefault()
            //         }).ToList();

            //foreach (var i in itens)
            //{
            //    switch (i.LocalPreco)
            //    {
            //        case 1:
            //            i.Observacoes = (from p in Contexto.Atual.EOBSERVACAO
            //                             where p.EPRODUTO.Any(r => r.idProduto == i.PrdCat1_ID)
            //                             select new BL.ItemObservacao
            //                             {
            //                                  Observacao_ID = p.idObs,
            //                                  Observacao_Desc = p.descricao,
            //                                  Produto_ID = i.PrdCat1_ID
            //                             }).ToList();
            //            break;

            //        case 2:
            //            i.Observacoes = (from p in Contexto.Atual.EOBSERVACAO
            //                             where p.EPRODUTO.Any(r => r.idProduto == i.PrdCat2_ID)
            //                             select new BL.ItemObservacao
            //                             {
            //                                 Observacao_ID = p.idObs,
            //                                 Observacao_Desc = p.descricao,
            //                                 Produto_ID = i.PrdCat2_ID
            //                             }).ToList();
            //            break;

            //        case 3:
            //            i.Observacoes = (from p in Contexto.Atual.EOBSERVACAO
            //                             where p.EPRODUTO.Any(r => r.idProduto == i.PrdCat3_ID)
            //                             select new BL.ItemObservacao
            //                             {
            //                                 Observacao_ID = p.idObs,
            //                                 Observacao_Desc = p.descricao,
            //                                 Produto_ID = i.PrdCat3_ID
            //                             }).ToList();
            //            break;

            //        default:
            //            break;
            //    }
            //}

            //return itens;

            return null;
        }

        public List<MesaLista> GetMesasLista()
        {
            return (from p in Contexto.Atual.GMESA
                    where p.ativo
                    select new MesaLista()
                               {
                                   NuMesa = p.nuMesa,
                                   Status = p.idStatus
                               }).ToList();
        }

        public Mesa GetMesa(string id)
        {
            Contexto.AbrirContexto();

            int nuMesa = Convert.ToInt32(id);
            var conta = (from p in Contexto.Atual.ACONTA
                         where p.nuMesa == nuMesa
                               && (p.idStatus == 1 || p.idStatus == 3)
                               && p.tpConta == "M"
                         select p).FirstOrDefault();

            if (conta != null)
            {
                var mesa = new Mesa
                               {
                                   Mesa_Numero = nuMesa,
                                   Conta_ID = conta.idConta,
                                   Status_ID = conta.idStatus,
                                   Status_Desc = conta.ASTATCONTA.descricao,
                                   Desconto = conta.desconto ?? 0,
                               };

                conta.CalcularTotais();

                mesa.Pedidos_Qtd = conta.TotalItens;
                mesa.Servico = conta.TotalServico;
                mesa.Servico_Bool = conta.servico;
                mesa.Sub_Total = conta.SubTotal;
                mesa.Pessoas_Qtd = conta.pessoas;
                mesa.Total_Por_Pessoa = conta.TotalConta/conta.pessoas;
                mesa.Total = conta.TotalConta;

                mesa.Items = (from h in conta.ACONTITEM
                              where h.idStatus != 2 && h.idStatus != 5
                              select new ItensMesa
                                         {
                                             Item_Num = h.nuItem,
                                             Produto_ID = h.idProduto,
                                             Produto_Desc = h.EPRODUTO.nome,
                                             Desconto = h.desconto ?? 0,
                                             Quantidade = h.quantidade,
                                             Preco = h.preco,
                                             Status_ID = h.idStatus ?? 0,
                                             Status_Desc = h.ASTATCONTITEM.descricao,
                                             ItemPai_Num = h.nuItemPai ?? 0,
                                             Adicional = h.adicional,
                                             Opcao = h.opcao

                                         }).ToList();

                return mesa;
            }
            else
            {
                return new Mesa
                           {
                               Pedidos_Qtd = 0,
                               Servico = 0,
                               Servico_Bool = false,
                               Sub_Total = 0,
                               Pessoas_Qtd = 1,
                               Total_Por_Pessoa = 0,
                               Total = 0,
                               Mesa_Numero = nuMesa,
                               Conta_ID = 0,
                               Status_ID = 0,
                               Status_Desc = "Livre",
                               Desconto = 0

                           };
            }

        }

        public void ExecutaAcao(AcaoModel acao)
        {
            try
            {
                var ctrl = new PreContaControl();
                var cmesa = new MesaControl();
                Memoria.Empresa = 1;
                Memoria.Filial = 1;
                Memoria.TipoConta = TipoConta.Mesa;

                var conta = ctrl.BuscarPorMesa(acao.NuMesaOrigem);

                switch (acao.AcaoExecutar)
                {
                    case AcaoModel.Acao.Transferir:
                        ctrl.Transferir(conta, acao.NuMesaDestino);
                        break;
                    case AcaoModel.Acao.FecharConta:
                        conta.idStatus = 3;
                        conta.GMESA.idStatus = 4;

                        foreach (AASSOCIACAO am in conta.AASSOCIACAO)
                        {
                            var m = new GMESA();
                            if (am.nuMesa != null) m.nuMesa = am.nuMesa.Value;
                            m = cmesa.Buscar(m);
                            m.idStatus = 4;

                            cmesa.AtualizarStatus(m);
                        }

                        Contexto.Atual.SaveChanges();

                        ctrl.Imprimir(conta);
                        break;
                    case AcaoModel.Acao.Pessoas:
                        conta.pessoas = acao.NuPessoas;
                        Contexto.Atual.SaveChanges();
                        break;
                    case AcaoModel.Acao.DescontoConta:
                        conta.desconto = acao.Desconto;
                        Contexto.Atual.SaveChanges();
                        break;
                    case AcaoModel.Acao.Gorjeta:
                        conta.servico = !conta.servico;
                        Contexto.Atual.SaveChanges();
                        break;
                    case AcaoModel.Acao.CancelarConta:
                        ctrl.Cancelar(conta);
                        break;
                    case AcaoModel.Acao.DescontoItens:
                        break;
                    case AcaoModel.Acao.Bloqueio:
                        if (conta.idStatus == 3)//DESBLOQUEIO
                        {
                            conta.idStatus = 1;
                            conta.GMESA.idStatus = 1;
                            ctrl.Atualizar(conta);

                            foreach (AASSOCIACAO am in conta.AASSOCIACAO)
                            {
                                var m = new GMESA();
                                if (am.nuMesa != null) m.nuMesa = am.nuMesa.Value;
                                m = cmesa.Buscar(m);
                                m.idStatus = 1;

                                cmesa.Atualizar(m);
                            }

                            Contexto.Atual.SaveChanges();
                        }
                        else
                        {//BLOQUEIA
                            conta.idStatus = 3;
                            conta.GMESA.idStatus = 4;

                            foreach (AASSOCIACAO am in conta.AASSOCIACAO)
                            {
                                var m = new GMESA();
                                if (am.nuMesa != null) m.nuMesa = am.nuMesa.Value;
                                m = cmesa.Buscar(m);
                                m.idStatus = 4;

                                cmesa.AtualizarStatus(m);
                            }

                            Contexto.Atual.SaveChanges();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter r = new StreamWriter(@"c:\Teste\teste.txt",true))
                {
                    r.WriteLine("--------");
                    r.WriteLine(ex.ToString());
                    r.WriteLine("--------");
                    r.WriteLine(acao.NuMesaDestino);
                }
            }

        }
    }
}
