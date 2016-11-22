using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ArtebitGourmet.WebService.BL
{
    [DataContract]
    public partial class ItensCardapio
    {
        public ItensCardapio()
        {
            this.Observacoes = new List<ItemObservacao>();
        }

        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int Item_ID { get; set; }
        [DataMember]
        public int? Grupo { get; set; }
        [DataMember]
        public int? LocalPreco { get; set; }
        [DataMember]
        public decimal? Preco { get; set; }

        [DataMember]
        public int?    PrdCat1_ID { get; set; }
        [DataMember]
        public string PrdCat1_Nome { get; set; }
        [DataMember]
        public string PrdCat1_Codigo { get; set; }
        [DataMember]
        public string PrdCat1_Cor { get; set; }

        [DataMember]
        public int?    PrdCat2_ID { get; set; }
        [DataMember]
        public string PrdCat2_Nome { get; set; }
        [DataMember]
        public string PrdCat2_Codigo { get; set; }
        [DataMember]
        public string PrdCat2_Cor { get; set; }

        [DataMember]
        public int?    PrdCat3_ID { get; set; }
        [DataMember]
        public string PrdCat3_Nome { get; set; }
        [DataMember]
        public string PrdCat3_Codigo { get; set; }
        [DataMember]
        public string PrdCat3_Cor { get; set; }

        [DataMember]
        public List<ItemObservacao> Observacoes { get; set; }

    }
}
