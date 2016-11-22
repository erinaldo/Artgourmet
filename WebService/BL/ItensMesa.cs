using System;
using System.Collections.Generic;
using System.Text;

namespace ArtebitGourmet.WebService.BL
{
    public partial class ItensMesa
    {
        public ItensMesa()
        {

        }

        public int Item_Num { get; set; }
        public int Produto_ID { get; set; }
        public string Produto_Desc { get; set; }
        public decimal Desconto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Preco { get; set; }
        public int Status_ID { get; set; }
        public string Status_Desc {get;set;}
        public int ItemPai_Num { get; set; }
        public bool Adicional { get; set; }
        public bool Opcao { get; set; }



    }
}
