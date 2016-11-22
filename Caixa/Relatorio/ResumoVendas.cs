using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Artebit.Restaurante.Global.Modelo;
using Telerik.Windows.Controls;
using System.Windows;

namespace Artebit.Restaurante.Caixa.Relatorio
{
    public abstract class ResumoVendas
    {
        public static string RetornaRelatorio(DateTime data)
        {
            var dados = new ResumoVendasModel();

            var sb = new StringBuilder();

            sb.Append("\n");
            sb.Append("\n");
            sb.Append(CentralizaTexto(Negrito("Relatorio"))); //negrito | centralizado
            sb.Append("========================================\n");

            sb.AppendFormat("Data Inicial: {0:dd/MM/yyyy HH:mm:ss}\n", dados.DataInicial);
            sb.AppendFormat("Data Final: {0:dd/MM/yyyy HH:mm:ss}\n", dados.DataFinal);
            sb.Append("\n");

            sb.Append(Negrito(CentralizaTexto("Resumo de Vendas"))); //negrito | centralizado
            sb.Append("========================================\n");
            sb.Append(AlinhaDireita("Total de Faturamento", string.Format("{0:c2}", dados.TotalFaturamento)));
            sb.Append(AlinhaDireita("Descontos", string.Format("{0:c2}", dados.Descontos)));
            sb.Append("---------------------------------------\n");
            sb.Append(Negrito(AlinhaDireita("Vendas Liq.", string.Format("{0:c2}", dados.VendasLiquidas))));
            sb.Append("\n");

            sb.Append(AlinhaDireita("Serviço", string.Format("{0:c2}", dados.Servico)));
            sb.Append("========================================\n");
            sb.Append(Negrito(AlinhaDireita("Total", string.Format("{0:c2}", dados.Total))));

            sb.Append("\n");

            sb.Append(Negrito(CentralizaTexto("Formas de Pagamento"))); //negrito | centralizado
            sb.Append("========================================\n");
            sb.Append("                                   Valor\n");

            if (dados.FormasPagamento.Any())
            {
                var itens = from h in dados.FormasPagamento
                            where h.idFormaPGTO != null
                            group h by new {h.AFORMAPGTO.idFormaPGTO}
                            into g
                            select new
                                       {
                                           g.First().AFORMAPGTO.descricao,
                                           total =
                                g.Sum(
                                    r =>
                                    r.AFORMAPGTO.tipo == "Di"
                                        ? (r.valorRecebido - r.ACUPOMECF.troco)
                                        : r.valorRecebido)
                                       };


                foreach (var d in itens)
                {
                    sb.Append(AlinhaDireita(d.descricao, string.Format("{0:c2}", d.total)));
                }


                sb.Append("---------------------------------------\n");
                sb.Append(Negrito(AlinhaDireita("Total", string.Format("{0:c2}", itens.Sum(r => r.total)))));
            }
            sb.Append("\n");

            sb.Append(Negrito(CentralizaTexto("Retiros Parciais"))); //negrito | centralizado
            sb.Append("========================================\n");
            sb.Append(AlinhaDireita("Valor", string.Format("{0:c2}", 0)));
            sb.Append("========================================\n");
            sb.Append(AlinhaDireita("Total", string.Format("{0:c2}", 0)));

            sb.Append(Negrito(CentralizaTexto("Dinheiro em Caixa"))); //negrito | centralizado
            sb.Append("========================================\n");
            sb.Append(AlinhaDireita("Pagos em Dinheiro", string.Format("{0:c2}", dados.DinheiroEmCaixa)));
            sb.Append("---------------------------------------\n");
            sb.Append(Negrito(AlinhaDireita("Total", string.Format("{0:c2}", dados.DinheiroEmCaixa)))); //negrito 

            sb.Append("\n");

            sb.Append(Negrito(CentralizaTexto("Estatísticas"))); //negrito | centralizado
            sb.Append("========================================\n");
            sb.Append(AlinhaDireita("# Mesas Fechadas", dados.MesasFechadas.ToString(CultureInfo.InvariantCulture)));
            sb.Append(AlinhaDireita("Média Consumo X Mesa", string.Format("{0:n2}", dados.MediaConsumoMesa)));
            sb.Append(AlinhaDireita("Tempo Permanencia", dados.TempoPermanencia));
            sb.Append(AlinhaDireita("# Pessoas Mesas Fechadas", dados.PessoasMesasFechadas.ToString(CultureInfo.InvariantCulture)));
            sb.Append(AlinhaDireita("Media Consumo Pessoas", string.Format("{0:n2}", dados.MediaConsumoPessoas)));

            sb.Append("\n");

            sb.Append(AlinhaDireita("# Mesas Abertas", dados.MesasAbertas.ToString(CultureInfo.InvariantCulture)));
            sb.Append(AlinhaDireita("Consumo Mesas Abertas", string.Format("{0:c2}", dados.ConsumoMesasAbertas)));
            sb.Append(AlinhaDireita("# Pessoas Mesas Abertas", dados.PessoasMesasAbertas.ToString(CultureInfo.InvariantCulture)));
            sb.Append(AlinhaDireita("Total Cancelamentos", string.Format("{0:c2}", dados.TotalCancelamentos)));
            sb.Append(AlinhaDireita("Total Descontos", string.Format("{0:c2}", dados.TotalDescontos)));
            sb.Append(AlinhaDireita("Total Servico Coletado", string.Format("{0:c2}", dados.TotalServicoColetado)));

            //Rodapé da impressão
            sb.Append(new string(' ', 30) + "\n");

            sb.Append(new string('-', 48) + "\n");

            sb.Append(new string(' ', 10) + "ARTEBIT GOURMET " + Sistema.Versao + "\n");

            sb.Append(new string(' ', 10) + "WWW.ARTEBIT.COM.BR" + "\n");


            return sb.ToString();
        }

        /// <summary>
        /// Imprime relatorio da data informada
        /// </summary>
        /// <param name="data"></param>
        public static void ImprimeRelatorio(DateTime data)
        {
        }

        /// <summary>
        /// Imprime relatorio de vendas consumadas no período em aberto. Caso nao tenha periodo em aberto, gera do dia atual
        /// </summary>
        public static void ImprimeRelatorio()
        {
            string resultado = RetornaRelatorio(DateTime.Now);

            Impressoras.Fiscal.ECF.ImpressoraFiscal.OperacaoNaoFiscal.ImprimirRelatorio(resultado);

        }

        /// <summary>
        /// Imprime relatorio de vendas consumadas no período em aberto. Caso nao tenha periodo em aberto, gera do dia atual
        /// </summary>
        public static void ImprimeRelatorio(RadBusyIndicator indicator)
        {
            try
            {
                string resultado = RetornaRelatorio(DateTime.Now);

                Impressoras.Fiscal.ECF.ImpressoraFiscal.OperacaoNaoFiscal.ImprimirRelatorio(resultado);

                Application.Current.Dispatcher.Invoke(new Action(() =>
                                                                 indicator.IsBusy = false));
            }
            catch (Exception ex)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                                                                 RadWindow.Alert(ex.Message)));

                Application.Current.Dispatcher.Invoke(new Action(() =>
                                                                indicator.IsBusy = false));
            }          
        }


        private static string CentralizaTexto(string texto)
        {
            return new string(' ', (40 - texto.Length)/2) + texto + "\n";
        }

        private static string AlinhaDireita(string textoEsquerda, string textoDireita)
        {
            return textoEsquerda + new string(' ', 40 - (textoEsquerda.Length + textoDireita.Length)) + textoDireita +
                   "\n";
        }

        private static string Negrito(string texto)
        {
            return (char) 27 + ((char) 69 + texto) + (char) 27 + (char) 70;
        }
    }

    public class ResumoVendasModel
    {
        public DateTime DataFinal;
        public DateTime DataInicial;

        public ResumoVendasModel()
        {
            APERIODOFISCAL periodo = Contexto.Atual.APERIODOFISCAL.FirstOrDefault(r => r.status == false);


            DataInicial = DateTime.Now.Date.AddHours(5);
            DataFinal = DataInicial.AddDays(1);

            if (DateTime.Now.Hour < 5)
            {
                DataInicial = DateTime.Now.AddDays(-1).AddHours(5);
                DataFinal = DataInicial.AddDays(1);
            }

            if (periodo != null)
            {
                DataInicial = periodo.dataInicio;
                DataFinal = DateTime.Now;
            }


            //PEGA OS VALORES VENDIDOS ATÉ O MOMENTO
            List<ACUPOMECF> cupons = (from p in Contexto.Atual.ACUPOMECF
                                      join h in Contexto.Atual.ACONTA on p.idConta equals h.idConta
                                      where
                                          (p.dataEmitido >= DataInicial && p.dataEmitido <= DataFinal) &&
                                          (p.cancelado != true || p.cancelado == null)
                                          && h.tpConta == "M"
                                      select p).ToList();

            // where (p.dataEmitido >= DataInicial && p.dataEmitido <= DataFinal)  && p.canceladoECF == null)
            // .Where(r => (r.dataEmitido >= DataInicial && r.dataEmitido <= DataFinal)  && r.canceladoECF == null).ToList();

            List<ACONTA> contasFechadas = (from p in Contexto.Atual.ACONTA
                                           join h in Contexto.Atual.ACUPOMECF on p.idConta equals h.idConta
                                           where
                                               (h.dataEmitido >= DataInicial && h.dataEmitido <= DataFinal) &&
                                               (h.cancelado != true || h.cancelado == null)
                                               && p.tpConta == "M"
                                           select p).ToList();

            List<ACONTA> contasAbertas = (from p in Contexto.Atual.ACONTA
                                          where (p.idStatus == 1 || p.idStatus == 3)
                                                && p.tpConta == "M" && p.nuMesa != null
                                          select p).ToList();

            #region PEGA OS VALORES VENDIDOS NAS MESAS ABERTAS

            IQueryable<ACONTA> contas = (from p in Contexto.Atual.ACONTA
                                         where (p.idStatus == 1 || p.idStatus == 3)
                                               && p.tpConta == "M" && p.nuMesa != null
                                         select p);

            foreach (ACONTA c in contas)
            {
                decimal? totalConta = 0;

                foreach (ACONTITEM it in c.ACONTITEM.Where(r => r.idStatus != 2))
                {
                    decimal? totalItem = (it.preco*it.quantidade) - it.desconto;
                    if (totalItem <= 0)
                        totalItem = Convert.ToDecimal("0,01");

                    totalConta += totalItem;
                }

                if (totalConta - c.desconto <= 0)
                {
                    ConsumoMesasAbertas += Convert.ToDecimal("0,01");
                }
                else
                {
                    ConsumoMesasAbertas += (totalConta - (c.desconto ?? 0)) ?? 0;
                }
            }

            #endregion

            //CALCULA O TOTAL DE FATURAMENTO = VALORES MESAS ABERTAS + VALORES MESAS FECHADAS
            TotalFaturamento = ((decimal) cupons.Sum(r => r.total)) + ConsumoMesasAbertas;

            #region BUSCA O TOTAL DE DESCONTOS DAS MESAS ABERTAS

            foreach (ACONTA c in contas)
            {
                decimal? totalDescontos = c.ACONTITEM.Where(r => r.idStatus != 2).Aggregate<ACONTITEM, decimal?>(0, (current, it) => current + (it.desconto ?? 0));

                Descontos = (totalDescontos + c.desconto) ?? 0;
            }

            #endregion

            #region BUSCA O TOTAL DE DESCONTOS DAS MESAS FECHADAS

            foreach (ACUPOMECF ac in cupons)
            {
                foreach (AITEMCUPOM ait in ac.AITEMCUPOM)
                {
                    Descontos += ait.desconto ?? 0;
                }

                Descontos += ac.desconto ?? 0;
            }

            #endregion

            VendasLiquidas = TotalFaturamento - Descontos;
            Servico = Convert.ToDecimal("0,1")*VendasLiquidas;
            Total = VendasLiquidas + Servico;

            FormasPagamento = (from p in Contexto.Atual.ARECEBIMENTO
                               where
                                   p.ACUPOMECF != null &&
                                   (p.ACUPOMECF.dataEmitido >= DataInicial && p.ACUPOMECF.dataEmitido <= DataFinal) &&
                                   (p.ACUPOMECF.cancelado != true || p.ACUPOMECF.cancelado == null)
                               select p).ToList();

            DinheiroEmCaixa =
                FormasPagamento.Where(r => r.idFormaPGTO == 3).Sum(
                    r =>r.valorRecebido - r.ACUPOMECF.troco
                    ).Value; //Dinheiro


            MesasFechadas = contasFechadas.Count;
            MediaConsumoMesa = ((decimal) cupons.Sum(r => r.total))/(MesasFechadas <= 0 ? 1 : MesasFechadas);

            double totalHoras = 0;
            foreach (ACONTA co in contasFechadas)
            {
                var cupom = cupons.FirstOrDefault(r => r.idConta == co.idConta && (r.cancelado != true || r.cancelado == null));
                if (cupom != null)
                {
                    if (cupom.dataEmitido != null)
                    {
                        DateTime dataFechamento =
                            cupom.dataEmitido.Value;

                        TimeSpan diferenca = co.dataInclusao.Subtract(dataFechamento);

                        totalHoras += diferenca.TotalHours;
                    }
                }
            }

            if (totalHoras < 0)
            {
                totalHoras = -1*totalHoras;
            }

            TempoPermanencia = string.Format("{0:n2} Horas", totalHoras/(MesasFechadas <= 0 ? 1 : MesasFechadas));


            PessoasMesasFechadas = contasFechadas.Sum(r => r.pessoas);

            PessoasMesasFechadas = PessoasMesasFechadas == 0 ? 1 : PessoasMesasFechadas;

            MediaConsumoPessoas = ((decimal) cupons.Sum(r => r.total))/PessoasMesasFechadas;

            MesasAbertas = contasAbertas.Count();

            PessoasMesasAbertas = contasAbertas.Sum(r => r.pessoas);

            TotalCancelamentos = contasAbertas.Sum(r => r.ACONTITEM.Where(t => t.idStatus == 2).Sum(s => s.preco));
            TotalDescontos = (decimal) contasAbertas.Sum(r => r.desconto + r.ACONTITEM.Sum(t => t.desconto));
            TotalServicoColetado = Convert.ToDecimal("0,1")*ConsumoMesasAbertas;
        }

        public decimal TotalFaturamento { get; set; }
        public decimal Descontos { get; set; }
        public decimal VendasLiquidas { get; set; }
        public decimal Servico { get; set; }
        public decimal Total { get; set; }

        public List<ARECEBIMENTO> FormasPagamento { get; set; }
        public decimal DinheiroEmCaixa { get; set; }

        public int MesasFechadas { get; set; }
        public decimal MediaConsumoMesa { get; set; }
        public string TempoPermanencia { get; set; }
        public int PessoasMesasFechadas { get; set; }
        public decimal MediaConsumoPessoas { get; set; }

        public int MesasAbertas { get; set; }
        public decimal ConsumoMesasAbertas { get; set; }
        public int PessoasMesasAbertas { get; set; }
        public decimal TotalCancelamentos { get; set; }
        public decimal TotalDescontos { get; set; }
        public decimal TotalServicoColetado { get; set; }
    }
}