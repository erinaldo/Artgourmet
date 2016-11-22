using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ArtebitGourmet.WebService.BL
{
    [DataContract]
    public class Vendedor
    {
        public Vendedor()
        {
            this.Permissoes = new List<Permissao>();
        }


        [DataMember]
        public int Vendedor_ID { get; set; }
        [DataMember]
        public string Vendedor_Codigo { get; set; }
        [DataMember]
        public string Vendedor_Nome { get; set; }
        [DataMember]
        public string Usuario { get; set; }
        [DataMember]
        public int Perfil_ID { get; set; }


        [DataMember]
        public List<Permissao> Permissoes { get; set; }

        public IEnumerable<Permissao> PermissoesEnum { get; set; }




    }
}
