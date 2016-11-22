using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Caixa.Relatorio
{
    public class PreConta
    {
        public string RetornaRelatorio(ACONTA conta)
        {
            #region Calculo dos totais

            //pega o total da conta dos itens que não foram cancelados
            decimal total =
                conta.ACONTITEM.Where(r => r.idStatus != 2 && r.idStatus != 5).Sum(
                    r => (r.quantidade*r.preco) - r.desconto).Value;
            decimal totalGeral1 =
                conta.ACONTITEM.Where(r => r.idStatus != 2 && r.idStatus != 5).Sum(r => (r.quantidade*r.preco));

            Decimal totalDesc;

            if (total < conta.desconto)
            {
                totalDesc = Convert.ToDecimal(0.01);
            }
            else
            {
                totalDesc = total - Convert.ToDecimal(conta.desconto);
            }

            //taxa de serviço
            decimal servico = Convert.ToDecimal("0,1");

            //total do servico
            decimal totalServico = (servico*totalGeral1);

            //se não tiver serviço, o mesmo será igual a 0
            if (!conta.servico)
            {
                totalServico = 0;
            }

            //total de descontos
            //decimal totalDesconto = conta.ACONTITEM.Where(r => r.idStatus != 2 && r.idStatus != 5).Sum(r => r.desconto).Value;

            //quantidade de itens
            decimal quantidadeItens = conta.ACONTITEM.Count(r => (r.idStatus != 2 && r.idStatus != 5) && r.opcao == false);

            //permanencia
            string permanencia = string.Format("{0:hh\\:mm\\:ss}", DateTime.Now.Subtract(conta.dataInclusao));

            //totalgeral = total + servico
            decimal totalGeral = (totalServico + totalDesc);

            //total por pessoa
            string totalPessoa = string.Format("{0:c}", totalGeral/(conta.pessoas == 0 ? 1 : conta.pessoas));

            #endregion

            var sb = new StringBuilder();

            sb.Append("\n");
            sb.Append("\n");
            sb.Append(CentralizaTexto(Negrito("CUPOM NÃO FISCAL"))); //negrito | centralizado
            sb.Append("========================================\n");

            sb.AppendFormat("VENDEDOR: " + VerificaVendedor(conta) + "\n");
            sb.AppendFormat("PRECONTA NRO: " + conta.idConta + "\n");
            sb.AppendFormat("Data: {0:dd/MM/yyyy}    Hora: {0:HH:mm}\n", DateTime.Now);
            //sb.AppendFormat("Hora: {0:HH:mm}\n", DateTime.Now);
            sb.Append(CentralizaTexto(Negrito("MESA/CARTÃO: " + conta.nuMesa)));

            #region Header da tabela de produtos

            sb.Append("\n");
            sb.Append(new string('-', 48) + "\n");
            string tituloTabela = " QTDE     PRODUTO              P.UNIT   VALOR";
            sb.Append(tituloTabela + "\n");
            sb.Append(new string('-', 48) + "\n");

            #endregion

            var itens = from p in conta.ACONTITEM
                        where (p.idStatus != 2 && p.idStatus != 5) && p.opcao == false
                        group p by new {p.idProduto, p.preco, p.desconto, p.EPRODUTO.undVenda, p.EPRODUTO.codigo}
                        into g
                        select new
                                   {
                                       g.First().EPRODUTO.codigo,
                                       g.First().EPRODUTO.nome,
                                       desconto = g.Sum(r => r.desconto),
                                       g.First().preco,
                                       quantidade = g.Sum(r => r.quantidade),
                                       unidade = g.First().EPRODUTO.undVenda,
                                       nuItens = (from j in g
                                                  select j.nuItem).ToList()
                                   };

            #region Produtos

            string msg;

            foreach (var ac in itens)
            {
                //formata a quantidade
                msg = string.Format("{0:0}", ac.quantidade);
                string linha = " " + msg + new string(' ', 8 - msg.Length);

                //formata o nome do produto
                string nomeProduto = ac.nome.ToUpper();
                if (nomeProduto.Length > 21)
                {
                    nomeProduto = nomeProduto.Substring(0, 21);
                }
                linha += nomeProduto + new string(' ', 22 - nomeProduto.Length);

                //formata o preco total do item
                decimal? totalOpcionais = conta.ACONTITEM.Where(r => r.nuItemPai != null && r.opcao).Where(
                    r => r.nuItemPai == ac.nuItens.First()).Sum(r => r.quantidade*r.preco);

                decimal? totalOpcionaisGeral = conta.ACONTITEM.Where(r => r.nuItemPai != null && r.opcao).Where(
                    r => r.nuItemPai != null && ac.nuItens.Contains(r.nuItemPai.Value)).Sum(r => r.quantidade*r.preco);

                //formata o preco unitario
                msg = string.Format("{0:0.00}", ac.preco + totalOpcionais);
                linha += msg + new string(' ', 9 - msg.Length);

                decimal? totalItem = ((ac.preco*ac.quantidade) - ac.desconto) + totalOpcionaisGeral;
                msg = " " + string.Format("{0:0.00}", totalItem);
                linha += msg + new string(' ', 7 - msg.Length);

                //manda linha para a impressora
                sb.Append(linha + "\n");
                //retorno = MP2032.FormataTX(linha + "\n", 2, 0, 0, 0, 0);

                if (ac.desconto != 0)
                {
                    linha = new string(' ', 9) + "DESCONTO: " + string.Format("{0:0.00}", ac.desconto);
                    sb.Append(linha + "\n");
                    //retorno = MP2032.FormataTX(linha + "\n", 2, 0, 0, 0, 0);
                }
            }

            #endregion

            sb.Append(new string('-', 48) + "\n");

            #region Totais

            string total1 = new string(' ', 10) + "TOTAL PRODUTOS : " + string.Format("{0:c}", total);
            sb.Append(total1 + "\n");
            //retorno = MP2032.FormataTX(total1 + "\n", 2, 0, 0, 0, 0);

            string total3 = new string(' ', 10) + "DESCONTO: " + string.Format("{0:c}", conta.desconto);
            sb.Append(total3 + "\n");
            //retorno = MP2032.FormataTX(total3 + "\n", 2, 0, 0, 0, 0);

            string total4 = new string(' ', 10) + "SERVIÇO: " + string.Format("{0:c}", totalServico);
            sb.Append(total4 + "\n");
            //retorno = MP2032.FormataTX(total4 + "\n", 2, 0, 0, 0, 0);

            string total2 = "TOTAL: " + string.Format("{0:c}", totalGeral);
            sb.Append(CentralizaTexto(Negrito(total2)));
            //retorno = MP2032.FormataTX(total2 + "\n", 2, 0, 0, 1, 0);
            sb.Append(new string(' ', 48) + "\n");
            //retorno = MP2032.BematechTX(new string(' ', 40) + "\n");

            #endregion

            #region Rodapé de totais

            msg = "QUANTIDADE DE ITENS: " + quantidadeItens.ToString(CultureInfo.InvariantCulture);
            sb.Append(msg + "\n");
            //retorno = MP2032.BematechTX(msg + "\n");

            msg = "TOTAL DE PESSOAS: " + conta.pessoas.ToString(CultureInfo.InvariantCulture);
            sb.Append(msg + "\n");
            //retorno = MP2032.BematechTX(msg + "\n");

            msg = "TOTAL POR PESSOA: " + totalPessoa;
            sb.Append(msg + "\n");
            //retorno = MP2032.BematechTX(msg + "\n");

            msg = "PERMANÊNCIA: " + permanencia;
            sb.Append(msg + "\n");
            //retorno = MP2032.BematechTX(msg + "\n");

            #endregion

            //Rodapé da impressão
            sb.Append(new string(' ', 48) + "\n");
            //retorno = MP2032.BematechTX(new string(' ', 40) + "\n");
            sb.Append(new string('-', 48) + "\n");
            //retorno = MP2032.BematechTX(new string('-', 40) + "\n");
            sb.Append(new string(' ', 10) + "ARTEBIT GOURMET " + Sistema.Versao + "\n");
            //retorno = MP2032.BematechTX(new string(' ', 10) + "ARTEBIT GOURMET 1.0" + "\n");
            sb.Append(new string(' ', 10) + "WWW.ARTEBIT.COM.BR" + "\n");
            //retorno = MP2032.BematechTX(new string(' ', 10) + "WWW.ARTEBIT.COM.BR" + "\n");

            return sb.ToString();
        }

        /// <summary>
        /// Imprime relatorio da data informada
        /// </summary>
        /// <param name="data"></param>
        public static void ImprimeRelatorio(DateTime data)
        {
        }

        public string VerificaVendedor(ACONTA obj)
        {
            string str = "";

            //double Num;
            //bool isNum = double.TryParse(obj.vendedorAbertura, out Num);

            //if (isNum)
            //{
            //    str = Contexto.Atual.GVENDEDOR.FirstOrDefault(r => r.idVen == Num).nome;
            //}
            //else
            //{
            //    str = obj.vendedor;
            //}

            return str;
        }

        /// <summary>
        /// Imprime relatorio de vendas consumadas no período em aberto. Caso nao tenha periodo em aberto, gera do dia atual
        /// </summary>
        public void ImprimeRelatorio(ACONTA conta)
        {
            string resultado = RetornaRelatorio(conta);

            Impressoras.Fiscal.ECF.ImpressoraFiscal.OperacaoNaoFiscal.ImprimirRelatorio(resultado);
        }


        private static string CentralizaTexto(string texto)
        {
            return new string(' ', (40 - texto.Length)/2) + texto + "\n";
        }

/*
        private static string AlinhaDireita(string textoEsquerda, string textoDireita)
        {
            return textoEsquerda + new string(' ', 40 - (textoEsquerda.Length + textoDireita.Length)) + textoDireita +
                   "\n";
        }
*/

        private static string Negrito(string texto)
        {
            return (char) 27 + ((char) 69 + texto) + (char) 27 + (char) 70;
        }
    }
}