using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using Artebit.Restaurante.Caixa.Relatorio.Model;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Caixa.Relatorio
{
    /// <summary>
    /// Interaction logic for FechamentoPeriodo.xaml
    /// </summary>
    public partial class FechamentoPeriodo
    {
        public string Resultado = "";

        public FechamentoPeriodo()
        {
            InitializeComponent();
        }

        public void CarregaRelatorio(DateTime inicio, DateTime fim)
        {
            var obj = new FechamentoPeriodoModelView(inicio, fim);


            var sb = new StringBuilder();
            sb.Append("\n");
            sb.Append("\n");

            sb.Append(Negrito(CentralizaTexto("FECHAMENTO DE CAIXA\n")));
            sb.Append("\n");
            sb.Append("========================================\n");
            sb.AppendFormat("Data desde:  {0:dd/MM/yyyy}  até  {1:dd/MM/yyyy}\n", obj.DataInicial, obj.DataFim);
            //sb.Append("Período:  0                 Caixa(s): Caixa Principal\n");
            sb.AppendFormat("Hora:  {0:HH:mm:ss}    Operador: \"{1}\"\n", DateTime.Now, Memoria.Codusuario);
            sb.Append("========================================\n");
            sb.Append("\n");
            sb.AppendFormat("Total Atendimento............: {0:0000}\n", obj.TotalAtendimentos);
            sb.AppendFormat("Total Coupons Cancelados.....: {0:0000}\n", obj.TotalCuponsCancelados);
            sb.AppendFormat("Total Atend. Delivery........: {0:0000}\n", obj.TotalAtendimentoDelivery);
            sb.Append("\n");
            sb.AppendFormat("Média Permanência............:{0:HH:mm:ss}\n", obj.MediaPermancencia);
            sb.Append("\n");
            sb.AppendFormat("Total Clientes...............: {0:0000}\n", obj.TotalClientes);
            sb.AppendFormat("Total Mesas..................: {0:0000}\n", obj.TotalMesas);
            sb.AppendFormat("Total Balcão.................: {0:0000}\n", obj.TotalBalcao);
            sb.Append("\n");
            //sb.AppendFormat("Cart. não fechados...........: {0:n2}\n",obj.CartNaoFechadas);
            sb.AppendFormat("Valor x pessoa...............: {0:n2}\n", obj.ValorPorPessoa);
            sb.AppendFormat("Ticket médio por coupon......: {0:n2}\n", obj.TicketMedioCupom);
            sb.AppendFormat("\n");
            sb.AppendFormat("Total Venda de produtos......: {0:n2}\n", obj.TotalVendaProdutos);
            sb.AppendFormat("Total Desc. em produtos......: {0:n2}\n", obj.TotalDescProdutos);
            sb.AppendFormat("Total Serviço................: {0:n2}\n", obj.TotalServico);
            //sb.AppendFormat("Total serviços garçons.......: {0:n2}\n",obj.TotalServicoGarcons);
            //sb.AppendFormat("Total serviços cozinha.......: {0:n2}\n",obj.TotalServicosCozinha);
            //sb.AppendFormat("Total serviços delivery......: {0:n2}\n",obj.TotalServicosDelivery);
            //sb.AppendFormat("Total Adicionais.............: {0:n2}\n",obj.TotalAdicionais);
            //sb.AppendFormat("Total Contravales............: {0:n2}\n",obj.TotalContravales);
            sb.AppendFormat("Total Repique................: {0:n2}\n", obj.TotalRepique);
            sb.AppendFormat("Total Troco..................: {0:n2}\n", obj.TotalTroco);
            sb.Append("\n");
            sb.AppendFormat("Total Recebido Físico........: {0:n2}\n", obj.TotalRecebidoFisico);
            //sb.AppendFormat("Total Vendido C.C............: {0:n2}\n",obj.TotalVendidoCC);
            //sb.AppendFormat("Total Taxas ingresso.........: {0:n2}\n",obj.TotalTaxaIngresso);
            //sb.AppendFormat("Total Diferença Cons. Mínima.: {0:n2}\n",obj.TotalDiferencaConsMin);
            sb.Append("\n");
            sb.AppendFormat("Fundo de Caixa...............: {0:n2}\n", obj.FundoDeCaixa);
            sb.Append("\n");
            sb.AppendFormat("Faturado Mesas...............: {0:n2}\n", obj.FaturadoMesas);
            sb.AppendFormat("Faturado Balcão..............: {0:n2}\n", obj.FaturadoBalcao);
            sb.AppendFormat("Faturado Delivery............: {0:n2}\n", obj.FaturadoDelivery);
            sb.Append("\n");
            sb.AppendFormat("Total Notas de Crédito.......: {0:n2}\n", obj.TotalNotasCredito);
            sb.Append("\n");
            sb.AppendFormat(Negrito("TOTAL VENDAS.................: {0:n2}\n"), obj.TotalVendas);
            sb.AppendFormat(Negrito("TOTAL PAGO...................: {0:n2}\n"), obj.TotalPago);


            sb.Append("\n");

            sb.Append(Negrito("DETALHE DE RECEBIMENTOS\n"));
            sb.Append("-----------------------------------------\n");
            sb.Append("TIPO               VL SISTEMA   VL FISICO   DIF\n");
            sb.Append("-----------------------------------------\n");

            decimal totalVlSistema = 0;
            decimal totalVlFisico = 0;
            decimal totalDiferenca = 0;
            if (obj.Fechamentos.Any())
            {
                var itens = from h in obj.Fechamentos
                            group h by new {h.idTipoRecebimento}
                            into g let fechamento = g.FirstOrDefault() where fechamento != null select new
                                       {
                                           g.First().AFORMAPGTO.descricao,
                                           fechamento.valorFisico,
                                           fechamento.valorSistema
                                       };

                foreach (var k in itens)
                {
                    string nome = k.descricao.Length > 18
                                      ? k.descricao.Substring(0, 18)
                                      : k.descricao + new string(' ', 19 - k.descricao.Length);

                    string valorSistema = String.Format("{0:0.00}", k.valorSistema);
                    valorSistema += new string(' ', 9 - valorSistema.Length);

                    totalVlSistema += k.valorSistema ?? 0;

                    string valorFisico = String.Format("{0:0.00}", k.valorFisico);
                    valorFisico += new string(' ', 9 - valorFisico.Length);

                    totalVlFisico += k.valorFisico ?? 0;

                    string valorDif = String.Format("{0:0.00}", k.valorFisico - k.valorSistema);
                    valorDif += new string(' ', 9 - valorDif.Length);

                    sb.Append(AlinhaDireita(nome, valorSistema + "  " + valorFisico + " " + valorDif));
                }

                totalDiferenca = totalVlFisico - totalVlSistema;
            }

            sb.Append("-----------------------------------------\n");
            string sTotalSistema = String.Format("{0:0.00}", totalVlSistema);
            sTotalSistema += new string(' ', 9 - sTotalSistema.Length);

            string sTotalFisico = String.Format("{0:0.00}", totalVlFisico);
            sTotalFisico += new string(' ', 9 - sTotalFisico.Length);

            string sTotalDifer = String.Format("{0:0.00}", totalDiferenca);
            sTotalDifer += new string(' ', 9 - sTotalDifer.Length);

            sb.Append(Negrito(AlinhaDireita("TOTAL.", sTotalSistema + "  " + sTotalFisico + " " + sTotalDifer)));
            sb.Append("-----------------------------------------\n");

            sb.Append("\n");

            Resultado = sb.ToString();
            txtRelatorio.Text = Resultado;
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (dtFinal.SelectedDate != null)
            {
                DateTime datafinal = dtFinal.SelectedDate.Value;

                String horIni = Convert.ToDateTime(txtHorIni.Value.ToString()).ToString("HH:mm:ss");
                String horFim = Convert.ToDateTime(txtHorFim.Value.ToString()).ToString("HH:mm:ss");

                DateTime datainicial = Convert.ToDateTime(datafinal.ToString(CultureInfo.InvariantCulture).Replace("00:00:00", horIni));
                datafinal = Convert.ToDateTime(datafinal.ToString(CultureInfo.InvariantCulture).Replace("00:00:00", horFim));

                CarregaRelatorio(datainicial, datafinal);
            }
        }

        private void btImprimir_Click(object sender, RoutedEventArgs e)
        {
            string resultado = Resultado;
            Impressoras.Fiscal.ECF.ImpressoraFiscal.OperacaoNaoFiscal.ImprimirRelatorio(resultado);
        }

        public int Imprimir()
        {
            string resultado = Resultado;

            Impressoras.Fiscal.ECF.ImpressoraFiscal.OperacaoNaoFiscal.ImprimirRelatorio(resultado, 2);

            return 1;
        }

        private string AlinhaDireita(string textoEsquerda, string textoDireita)
        {
            return textoEsquerda + new string(' ', 50 - (textoEsquerda.Length + textoDireita.Length)) + textoDireita +
                   "\n";
        }

        private string Negrito(string texto)
        {
            return (char) 27 + ((char) 69 + texto) + (char) 27 + (char) 70;
        }

        private string CentralizaTexto(string texto)
        {
            return new string(' ', (40 - texto.Length)/2) + texto + "\n";
        }
    }
}