using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artebit.Restaurante.Global.Modelo.ModelLight
{
    public class Recurso
    {
        private decimal valorUnitario;

        public int IdEmpresa { get; set; }
        public int IdProduto { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Quantidade { get; set; }
        public string Unidade { get; set; }
        public decimal ValorUnitario { 
            get { return valorUnitario; }

            set
            {
                valorUnitario = value;
                ValorTotal = Quantidade*valorUnitario;
            }
        }
        public decimal ValorTotal { get; set; }
    }
}
