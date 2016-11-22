using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Caixa.Relatorio.Model
{
    public class FechamentoPeriodoModelView
    {
        public FechamentoPeriodoModelView(DateTime inicio, DateTime fim)
        {
            Load(inicio, fim);

            DataInicial = inicio;
            DataFim = fim;
        }

        public DateTime DataInicial { get; set; }
        public DateTime DataFim { get; set; }

        public int TotalAtendimentos { get; set; }
        public int TotalCuponsCancelados { get; set; }
        public int TotalAtendimentoDelivery { get; set; }

        public DateTime MediaPermancencia { get; set; }

        public int TotalClientes { get; set; }
        public int TotalMesas { get; set; }
        public int TotalBalcao { get; set; }

        public decimal CartNaoFechadas { get; set; }
        public decimal ValorPorPessoa { get; set; }
        public decimal TicketMedioCupom { get; set; }

        public decimal TotalVendaProdutos { get; set; }
        public decimal TotalDescProdutos { get; set; }
        public decimal TotalServicoGarcons { get; set; }
        public decimal TotalServicosCozinha { get; set; }
        public decimal TotalServicosDelivery { get; set; }
        public decimal TotalServico { get; set; }
        public decimal TotalAdicionais { get; set; }
        public decimal TotalContravales { get; set; }
        public decimal TotalRepique { get; set; }
        public decimal TotalTroco { get; set; }

        public decimal TotalRecebidoFisico { get; set; }
        public decimal TotalVendidoCC { get; set; }
        public decimal TotalTaxaIngresso { get; set; }
        public decimal TotalDiferencaConsMin { get; set; }

        public decimal FundoDeCaixa { get; set; }

        public decimal FaturadoMesas { get; set; }
        public decimal FaturadoBalcao { get; set; }
        public decimal FaturadoDelivery { get; set; }

        public decimal TotalNotasCredito { get; set; }

        public decimal TotalVendas { get; set; }
        public decimal TotalPago { get; set; }

        public List<ARECEBIMENTO> Recebimentos { get; set; }
        public List<AFECHAMENTO> Fechamentos { get; set; }

        public void Load(DateTime inicio, DateTime fim)
        {
            IQueryable<ACUPOMECF> ll =
                Contexto.Atual.ACUPOMECF.Where(
                    r => r.dataEmitido >= inicio && r.dataEmitido <= fim && (r.cancelado != true || r.cancelado == null) && r.fiscal == true);

            List<ACUPOMECF> cupons = ll.ToList();

            TotalAtendimentos = cupons.Count(r => r.cancelado == null);
            TotalCuponsCancelados =
                Contexto.Atual.ACUPOMECF.Count(r => r.dataEmitido >= inicio && r.dataEmitido <= fim && r.cancelado == true);

            TotalAtendimentoDelivery = (from p in Contexto.Atual.ACONTA
                                        join h in ll on p.idConta equals h.idConta
                                        where p.tpConta == "D"
                                        select h).Count();

            int contadorrecebimentos = 0;

            Recebimentos = new List<ARECEBIMENTO>();

            foreach (ACUPOMECF acupomecf in cupons)
            {
                contadorrecebimentos += acupomecf.ARECEBIMENTO.Count();

                if (contadorrecebimentos > 0)
                {
                    break;
                }
            }

            if (contadorrecebimentos > 0)
            {
                long total =
                    cupons.Sum(
                        r =>
                        r.ARECEBIMENTO.Where(w => w.idConta != null).Sum(
                            p =>
                            new TimeSpan(r.dataEmitido == null
                                             ? 0
                                             : r.dataEmitido.Value.Subtract(p.ACONTA.dataInclusao).Ticks).
                                Ticks));
                if (total < 0)
                {
                    total = total*-1;
                }

                var dt = new DateTime(total/(TotalAtendimentos <= 0 ? 1 : TotalAtendimentos));

                MediaPermancencia = dt;

                TotalClientes = (from p in Contexto.Atual.ACONTA
                                 join h in ll on p.idConta equals h.idConta
                                 select p.pessoas).Sum();


                TotalMesas = TotalAtendimentos;
                TotalBalcao = (from p in Contexto.Atual.ACONTA
                               join h in ll on p.idConta equals h.idConta
                               where p.tpConta == "B"
                               select h).Count();

                TotalVendaProdutos = cupons.Sum(r => r.total).Value;

                CartNaoFechadas = 0;
                ValorPorPessoa = TotalVendaProdutos/(TotalClientes <= 0 ? 1 : TotalClientes);
                TicketMedioCupom = 0;

                TotalDescProdutos = (decimal) cupons.Sum(r => r.AITEMCUPOM.Sum(h => h.desconto));
                TotalServico = cupons.Sum(r => r.servico).Value;
                //TotalServicoGarcons = cupons.Where(r=> r.AITEMCUPOM
                TotalServicosCozinha = 0;
                TotalServicosDelivery = 0;
                TotalAdicionais = 0;
                TotalContravales = 0;
                TotalRepique = cupons.Sum(r=> r.repique);
                TotalTroco = (decimal) cupons.Sum(r => r.troco);

                TotalRecebidoFisico =
                    Convert.ToDecimal(Contexto.Atual.AFECHAMENTO.Where(a => a.idEmpresa == Memoria.Empresa
                                                                            && a.idFilial == Memoria.Filial
                                                                            && a.APERIODOFISCAL.dataInicio == inicio).
                                          Sum(b => b.valorFisico));

                Fechamentos = Contexto.Atual.AFECHAMENTO.Where(a => a.idEmpresa == Memoria.Empresa
                                                                    && a.idFilial == Memoria.Filial
                                                                    && a.APERIODOFISCAL.dataInicio == inicio).ToList();

                TotalVendidoCC = 0;
                TotalTaxaIngresso = 0;
                TotalDiferencaConsMin = 0;

                FundoDeCaixa = 0;

                FaturadoMesas = (from p in Contexto.Atual.ACONTA
                                 join h in ll on p.idConta equals h.idConta
                                 where p.tpConta == "M"
                                 select h).Sum(r => r.total + r.servico) ?? 0;

                FaturadoBalcao = (from p in Contexto.Atual.ACONTA
                                  join h in ll on p.idConta equals h.idConta
                                  where p.tpConta == "B"
                                  select h).Sum(r => r.total + r.servico) ?? 0;

                FaturadoDelivery = (from p in Contexto.Atual.ACONTA
                                    join h in ll on p.idConta equals h.idConta
                                    where p.tpConta == "D"
                                    select h).Sum(r => r.total + r.servico) ?? 0;

                TotalNotasCredito = 0;

                TotalVendas = FaturadoBalcao + FaturadoMesas + FaturadoDelivery;
                TotalPago = TotalVendaProdutos + TotalServico + TotalTroco + TotalRepique;


                Recebimentos = new List<ARECEBIMENTO>();

                foreach (ACUPOMECF ac in cupons)
                {
                    Recebimentos.AddRange(ac.ARECEBIMENTO.Where(w => w.idConta != null).ToList());
                }
            }
            else
            {
                Fechamentos = Contexto.Atual.AFECHAMENTO.Where(a => a.idEmpresa == Memoria.Empresa
                                                                    && a.idFilial == Memoria.Filial
                                                                    && a.APERIODOFISCAL.dataInicio == inicio).ToList();
            }
        }
    }
}