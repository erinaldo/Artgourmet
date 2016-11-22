using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace ArtebitGourmet.WebService.BL
{
    [DataContract]
    public partial class ItemObservacao
    {
        public ItemObservacao()
        {

        }

        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int? Produto_ID { get; set; }
        [DataMember]
        public int Observacao_ID { get; set; }
        [DataMember]
        public string Observacao_Desc { get; set; }
    }
}
