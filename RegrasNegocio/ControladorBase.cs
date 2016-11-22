using System.Data;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio
{
    public class ControladorBase
    {
        public object PegaObjetoPorChave(EntityKey chave)
        {
            return Contexto.Atual.GetObjectByKey(chave);
        }
    }
}