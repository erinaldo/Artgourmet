using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Artebit.Restaurante.Global.Modelo.Enum;
using Artebit.Restaurante.Global.Modelo.ModelLight;

//using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.Modelo
{
    public static class Memoria
    {
        private static string chavePublica =
            "C06B9CFE494783593B3A5C1BE532F57A504767DFE467E81F6DC6780D1CD860A6001D119EF1CA60B263915BE10CA1AA90C41A24AD90DCCBB9F7BF59EF469F47701E86C010F4BA376C7CA891ED079630C439C550B1842024210442ACB91092978A6297751F059547748F595333287C6A04BF23B55D03B3736EBB61ADBC324398B5";

        private static string chavePrivada =
            "F22968E7FC54DCB9BA332D48E9470551278012E88F439F9738756CF78628E4C674A92B8FCD7F55FA8F1BC8F89A657468C44257918B99C6AA4498BCFA17853923CB6A8911AFBD90408D6FD886EEC43A7D5E65DDDDF63169F6DC6C0ACAB0419E5BA64BD0D7C9B34B16DBED2D2D7A5A2299A3898156C0BCDBD247A3BB16FE584047";

        private static int codErroECF = 1;

        static Memoria()
        {
            //pega o tipo de base para verificar a forma de carregamento dos dados da filial e empresa
            string TipoBd = Convert.ToString(ConfigurationManager.AppSettings["TIPOBD"]);


            //se for local, carregará a informação de uma empresa e filial somente
            if (TipoBd == "Local")
            {
                Empresa = Contexto.Atual.GPARAMETROS.First().idEmpresa;
                Filial = Contexto.Atual.GPARAMETROS.First().idFilial;
                CodSistema = Convert.ToString(ConfigurationManager.AppSettings["CODSISTEMA"]);
            }

            IdECF = 1;
            IdCaixa = 1;
        }


        public static int? LogMesa { get; set; }

        public static int? LogMesaDestino { get; set; }

        public static int? LogContaDestino { get; set; }

        public static int? LogConta { get; set; }

        public static DateTime? LogData { get; set; }

        public static string LogCodUsuario { get; set; }


        public static List<Produto> Produtos { get; set; }

        public static string NomeVendedor { get; set; }

        public static int IdMonitor { get; set; }


        public static int IdCaixa { get; set; }

        public static int IdECF { get; set; }

        public static string ChavePrivada
        {
            get { return chavePrivada; }
            set { chavePrivada = value; }
        }


        public static string ChavePublica
        {
            get { return chavePublica; }
            set { chavePublica = value; }
        }

        public static string Comanda { get; set; }

        public static string MsgGlobal { get; set; }

        public static IEnumerable<GUSRFILMOD> ChuckNorris { get; set; }

        public static string CodSistema { get; set; }

        public static string Codusuario { get; set; }

        public static int? Perfil { get; set; }

        public static int? Vendedor { get; set; }

        public static int Empresa { get; set; }

        public static int Filial { get; set; }

        public static string Mesa { get; set; }

        public static string Botao { get; set; }

        public static List<int> FilialLista { get; set; }

        public static int WidthScreen { get; set; }
        public static int HeightScreen { get; set; }


        public static int CodErroECF
        {
            get { return codErroECF; }
            set { codErroECF = value; }
        }

        public static string LogAcao { get; set; }

        public static TipoConta TipoConta { get; set; }

        public static string Conta { get; set; }

        public static string Cliente { get; set; }

        public static bool RoundHouseKick(GUSUARIO us)
        {
            //pega a lista de sistemas
            string[] sistemas = Convert.ToString(ConfigurationManager.AppSettings["CODSISTEMA"]).Split(',');

            //pega o tipo de base para verificar a forma de carregamento dos dados da filial e empresa
            string TipoBd = Convert.ToString(ConfigurationManager.AppSettings["TIPOBD"]);

            if (TipoBd == "Local")
            {
                ChuckNorris =
                    Contexto.Atual.GUSRFILMOD.Where(
                        r =>
                        r.codUsuario == us.codusuario && sistemas.Contains(r.codSistema));

                if (ChuckNorris.Count() > 0)
                {
                    Perfil = ChuckNorris.First().idPerfil;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
    }
}