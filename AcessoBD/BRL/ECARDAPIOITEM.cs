using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artebit.Restaurante.Global.Modelo
{
    public partial class ECARDAPIOITEM
    {
        public string DescricaoPrd { get; set; }
        public string CodigoPrd { get; set; }
        public ModelLight.Produto ProdutoLight { get; set; }

    }
}
