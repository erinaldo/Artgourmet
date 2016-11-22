using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Global
{
    public class ImagemDAL
    {
        public GIMAGEM Buscar(GIMAGEM obj)
        {
            if (Memoria.Empresa != 0 && Memoria.Empresa != 0)
            {
                // Busca
                GIMAGEM vend = Contexto.Atual.GIMAGEM.SingleOrDefault(a => a.idImagem == obj.idImagem
                                                                           &&
                                                                           (a.idEmpresa == Memoria.Empresa ||
                                                                            Memoria.Empresa == 0));

                return vend;
            }
            else
            {
                return null;
            }
        }

        public bool Cancelar(GIMAGEM obj)
        {
            Contexto.Atual.DeleteObject(obj);
            Contexto.Atual.SaveChanges();
            return true;
        }
    }
}