using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ArtebitGourmet.WebService.BL
{
    [DataContract]
    public class Permissao
    {
        public Permissao()
        {

        }

        [DataMember]
        public int Permissao_ID { get; set; }
        [DataMember]
        public int Funcao_ID { get; set; }
        [DataMember]
        public int Janela_ID { get; set; }
        [DataMember]
        public int Perfil_ID { get; set; }

    }
}
