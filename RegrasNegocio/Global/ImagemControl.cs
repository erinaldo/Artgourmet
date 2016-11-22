using System;
using Artebit.Restaurante.Global.AcessoDados.Global;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Global
{
    public class ImagemControl
    {
        private readonly ImagemDAL dal = new ImagemDAL();

        [Obsolete]
        public object ExcecutaFuncao(GIMAGEM obj, Funcoes funcoes)
        {
            try
            {
                switch (funcoes)
                {
                    case Funcoes.Buscar:
                        return Buscar(obj);

                    case Funcoes.Cancelar:
                        return Cancelar(obj);

                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                Excecao.TratarExcecao(ex);
                return null;
            }
        }

        public GIMAGEM Buscar(GIMAGEM obj)
        {
            return dal.Buscar(obj);
        }

        public bool Cancelar(GIMAGEM obj)
        {
            return dal.Cancelar(obj);
        }
    }
}