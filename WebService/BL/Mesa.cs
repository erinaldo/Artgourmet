using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtebitGourmet.WebService.BL
{
    public partial class Mesa
    {
        public Mesa()
        {
            this.Items = new List<ItensMesa>();
        }

        public int Mesa_Numero { get; set; }
        public int Conta_ID { get; set; }
        public int Status_ID { get; set; }
        public string Status_Desc { get; set; }
        public int Pedidos_Qtd{ get; set; }
        public decimal Desconto { get; set; }
        public decimal Servico { get; set; }
        public bool Servico_Bool { get; set; }
        public decimal Sub_Total { get; set; }
        public int Pessoas_Qtd { get; set; }
        public decimal Total_Por_Pessoa { get; set; }
        public decimal Total { get; set; }


        public IList<ItensMesa> Items { get; set; }
    }
}
