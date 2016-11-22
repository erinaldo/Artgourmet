using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.AcessoDados.Atendimento;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Atendimento
{
    public class AliquotaControl
    {
        private readonly AliquotaDAL dal = new AliquotaDAL();

        [Obsolete]
        public object ExecutaFuncao(AALIQUOTA obj, Funcoes funcoes, List<String> compl)
        {
            try
            {
                switch (funcoes)
                {
                    case Funcoes.Adicionar:
                        return Criar(obj);

                    case Funcoes.Atualizar:
                        return Atualizar(obj);

                    case Funcoes.Buscar:
                        return Buscar(obj);

                    case Funcoes.BuscarLista:
                        return BuscarLista();

                    case Funcoes.Cancelar:
                        return Excluir(obj);

                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                Excecao.TratarExcecao(ex);
                return null;
            }
        }

        public bool Excluir(AALIQUOTA obj)
        {
            return dal.Excluir(obj);
        }

        public IQueryable<AALIQUOTA> BuscarLista()
        {
            return dal.BuscarLista();
        }

        public AALIQUOTA Buscar(AALIQUOTA obj)
        {
            return dal.Buscar(obj);
        }

        public bool Atualizar(AALIQUOTA obj)
        {
            return dal.Atualizar(obj);
        }

        public bool Criar(AALIQUOTA obj)
        {
            return dal.Criar(obj);
        }
    }
}