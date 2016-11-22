using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Caixa.ECF
{
    public class OperacaoCaixaDAL
    {
        /// <summary>
        /// Cria uma nova operação de caixa
        /// </summary>
        /// <param name="obj">objeto a ser criado</param>
        /// <returns>True-> executado com sucesso, caso contrário-> False</returns>
        public bool Criar(AOPCAIXA obj)
        {
            obj.idEmpresa = Memoria.Empresa;
            obj.idFilial = Memoria.Filial;

            obj.codUsuario = Memoria.Codusuario;


            obj.idOpCaixa = Contexto.GerarId("AOPCAIXA");

            foreach (ACUPOMECF cupom in obj.ACUPOMECF)
            {
                cupom.idOpCaixa = obj.idOpCaixa;
                cupom.idCupomECF = Contexto.GerarId("ACUPOMECF");

                foreach (ARECEBIMENTO receb in cupom.ARECEBIMENTO)
                {
                    receb.idEmpresa = Memoria.Empresa;
                    receb.idFilial = Memoria.Filial;
                    receb.idRecebimento = Contexto.GerarId("ARECEBIMENTO");
                    receb.idCupomECF = cupom.idCupomECF;
                    //int? id = receb.idFormaPGTO;
                    receb.idConta = cupom.idConta;
                    
                    //ACONTA connta = Contexto.Atual.ACONTA.SingleOrDefault(c => c.idConta == receb.idConta
                    //                                                           && c.idFilial == Memoria.Filial
                    //                                                           && c.idEmpresa == Memoria.Empresa);

                    //receb.ACONTA = connta;

                    //receb.idFormaPGTO = id;
                }

                foreach (AITEMCUPOM item in cupom.AITEMCUPOM)
                {
                    item.idCupomECF = cupom.idCupomECF;
                    item.idOpCaixa = obj.idOpCaixa;
                }
            }

            Contexto.Atual.AddToAOPCAIXA(obj);

            Contexto.Atual.SaveChanges();

            return true;
        }

        public int GerarIdReducao()
        {
            return Contexto.GerarId("AREDUCOES");
        }
    }
}