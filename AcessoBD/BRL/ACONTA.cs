using System;
using System.Linq;

namespace Artebit.Restaurante.Global.Modelo
{
    public partial class ACONTA
    {
        public decimal Total { get; set; }
        public decimal TotalGeral { get; set; }

        public decimal Servico { get; set; }
        public decimal TotalServico { get; set; }
        public decimal TotalDesconto { get; set; }
        public decimal TotalConta { get; set; }
        public decimal SubTotal { get; set; }

        public int TotalItens { get; set; }

        public void CalcularTotais()
        {
            //pega o total da conta dos itens que não foram cancelados
            Total =
                ACONTITEM.Where(r => r.idStatus != 2 && r.idStatus != 5).Sum(
                    r => (r.quantidade * r.preco) - r.desconto).Value;

            //pega o total da conta sem os descontos dados nos itens
            TotalGeral =
                ACONTITEM.Where(r => r.idStatus != 2 && r.idStatus != 5).Sum(r => (r.quantidade * r.preco));

            //se o desconto for maior que o total da conta, o total vai ser 1 centavo
            if (desconto >= Total)
            {
                Total = Convert.ToDecimal(0.01);
            }
            else
            {
                //se o desconto for menor, subtrai o desconto do total
                Total = Total - Convert.ToDecimal(desconto);
            }

            //taxa de serviço
            Servico = Convert.ToDecimal("0,1");

            //total do servico
            TotalServico = (Servico * TotalGeral);

            //total de descontos
            TotalDesconto =
                ACONTITEM.Where(r => r.idStatus != 2 && r.idStatus != 5).Sum(r => r.desconto).Value;
            TotalDesconto = TotalDesconto + Convert.ToDecimal(desconto);

            //se a conta não estiver com serviço, o mesmo será 0
            if (!servico)
            {
                TotalServico = 0;
            }

            TotalConta = Total + TotalServico;

            SubTotal = TotalGeral + TotalServico;

            TotalItens = ACONTITEM.Count(r => r.idStatus != 2 && r.idStatus != 5);
        }
    }
}