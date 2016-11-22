using Artebit.Restaurante.Global.Modelo.Extensions;

namespace Artebit.Restaurante.Global.Modelo.Enum
{
    public enum TipoConta
    {
        [StringValue("D")] Delivery,
        [StringValue("M")] Mesa,
        [StringValue("C")] Cartao,
        [StringValue("B")] Balcao
    }
}