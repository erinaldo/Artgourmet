using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artebit.Restaurante.Global.WebService.BL
{
    public class AcaoModel
    {
        public int NuMesaOrigem { get; set; }
        public int NuMesaDestino { get; set; }
        public decimal Desconto { get; set; }
        public int NuPessoas { get; set; }
        public Acao AcaoExecutar { get; set; }

        public enum Acao
        {
            Transferir = 1,
            FecharConta = 2,
            DescontoConta = 3,
            DescontoItens = 4,
            CancelarConta = 5,
            Bloqueio = 6,
            Gorjeta = 7,
            Pessoas = 8
        }
    }
}