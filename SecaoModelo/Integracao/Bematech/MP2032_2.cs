using System.Runtime.InteropServices;

namespace Artebit.Restaurante.Global.AcessoDados.Integracao.Bematech
{
    public class MP2032_2
    {
        [DllImport("MP2032_2.DLL")]
        public static extern int IniciaPorta(string porta);

        [DllImport("MP2032_2.DLL")]
        public static extern int FechaPorta();

        [DllImport("MP2032_2.DLL")]
        public static extern int BematechTX(string sTexto);

        [DllImport("MP2032_2.DLL")]
        public static extern int ComandoTX(string BufTrans, int TamBufTrans);

        [DllImport("MP2032_2.DLL")]
        public static extern int CaracterGrafico(string BufTrans, int TamBufTrans);

        [DllImport("MP2032_2.DLL")]
        public static extern int DocumentInserted();

        [DllImport("MP2032_2.DLL")]
        public static extern int Le_Status();

        [DllImport("MP2032_2.DLL")]
        public static extern int AutenticaDoc(string texto, int tempo);

        [DllImport("MP2032_2.DLL")]
        public static extern int Le_Status_Gaveta();

        [DllImport("MP2032_2.DLL")]
        public static extern int ConfiguraTamanhoExtrato(int NumeroLinhas);

        [DllImport("MP2032_2.DLL")]
        public static extern int HabilitaExtratoLongo(int Flag);

        [DllImport("MP2032_2.DLL")]
        public static extern int HabilitaEsperaImpressao(int Flag);

        [DllImport("MP2032_2.DLL")]
        public static extern int EsperaImpressao();

        [DllImport("MP2032_2.DLL")]
        public static extern int ConfiguraModeloImpressora(int ModeloImpressora);

        [DllImport("MP2032_2.DLL")]
        public static extern int AcionaGuilhotina(int Modo);

        [DllImport("MP2032_2.DLL")]
        public static extern int FormataTX(string BufTras, int TpoLtra, int Italic, int Sublin, int expand, int enfat);

        [DllImport("MP2032_2.DLL")]
        public static extern int HabilitaPresenterRetratil(int iFlag);

        [DllImport("MP2032_2.DLL")]
        public static extern int ProgramaPresenterRetratil(int iTempo);

        [DllImport("MP2032_2.DLL")]
        public static extern int VerificaPapelPresenter();

        [DllImport("MP2032_2.DLL")]
        public static extern int ConfiguraTaxaSerial(int Taxa);


        // Função para Configuração dos Códigos de Barras

        [DllImport("MP2032_2.DLL")]
        public static extern int ConfiguraCodigoBarras(int Altura, int Largura, int PosicaoCaracteres, int Fonte,
                                                       int Margem);


        // Funções para impressão dos códigos de barras

        [DllImport("MP2032_2.DLL")]
        public static extern int ImprimeCodigoBarrasUPCA(string Codigo);

        [DllImport("MP2032_2.DLL")]
        public static extern int ImprimeCodigoBarrasUPCE(string Codigo);

        [DllImport("MP2032_2.DLL")]
        public static extern int ImprimeCodigoBarrasEAN13(string Codigo);

        [DllImport("MP2032_2.DLL")]
        public static extern int ImprimeCodigoBarrasEAN8(string Codigo);

        [DllImport("MP2032_2.DLL")]
        public static extern int ImprimeCodigoBarrasCODE39(string Codigo);

        [DllImport("MP2032_2.DLL")]
        public static extern int ImprimeCodigoBarrasCODE93(string Codigo);

        [DllImport("MP2032_2.DLL")]
        public static extern int ImprimeCodigoBarrasCODE128(string Codigo);

        [DllImport("MP2032_2.DLL")]
        public static extern int ImprimeCodigoBarrasITF(string Codigo);

        [DllImport("MP2032_2.DLL")]
        public static extern int ImprimeCodigoBarrasCODABAR(string Codigo);

        [DllImport("MP2032_2.DLL")]
        public static extern int ImprimeCodigoBarrasISBN(string Codigo);

        [DllImport("MP2032_2.DLL")]
        public static extern int ImprimeCodigoBarrasMSI(string Codigo);

        [DllImport("MP2032_2.DLL")]
        public static extern int ImprimeCodigoBarrasPLESSEY(string Codigo);

        [DllImport("MP2032_2.DLL")]
        public static extern int ImprimeCodigoBarrasPDF417(int NivelCorrecaoErros, int Altura, int Largura, int Colunas,
                                                           string Codigo);

        [DllImport("MP2032_2.DLL")]
        public static extern int ImprimeBitmap(string name, int mode);

        [DllImport("MP2032_2.DLL")]
        public static extern int ImprimeBmpEspecial(string name, int xScale, int yScale, int angle);

        [DllImport("MP2032_2.DLL")]
        public static extern int AjustaLarguraPapel(int width);

        [DllImport("MP2032_2.DLL")]
        public static extern int SelectDithering(int mode);

        [DllImport("MP2032_2.DLL")]
        public static extern int PrinterReset();

        [DllImport("MP2032_2.DLL")]
        public static extern int LeituraStatusEstendido(byte[] A);

        [DllImport("MP2032_2.DLL")]
        public static extern int IoControl(int flag, bool mode);
    }
}