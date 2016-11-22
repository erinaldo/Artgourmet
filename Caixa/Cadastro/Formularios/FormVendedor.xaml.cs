using System;
using System.Linq;
using System.Windows;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Telerik.Windows.Controls;

namespace Artebit.Restaurante.Caixa.Cadastro
{
    /// <summary>
    /// Interaction logic for CadastrarVendedorJanela.xaml
    /// </summary>
    public partial class FormVendedor : RadWindow
    {
        private GVENDEDOR vend = new GVENDEDOR();

        public FormVendedor()
        {
            InitializeComponent();
            CarregarComboBox();
        }

        public FormVendedor(GVENDEDOR obj)
        {
            InitializeComponent();
            DataContext = obj;
            CarregarComboBox();
            CarregarInfo();
        }

        private void CarregarComboBox()
        {
            var control = new UsuarioControl();
            IQueryable<GUSUARIO> lista = control.BuscarLista();
            comboCodigoUsu.ItemsSource = lista;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void CarregarInfo()
        {
            //Carregando dados do DataContext
            vend = DataContext as GVENDEDOR;

            if (vend != null)
            {
                comboCodigoUsu.SelectedValue = vend.codUsuario;
                txboxCodigo.Password = vend.codigo;
                txboxNome.Value = vend.nome;
                txboxComissao.Value = vend.comissao;
                checkAtivo.IsChecked = vend.ativo;
            }
        }

        private bool validaDados()
        {
            if (Convert.ToString(comboCodigoUsu.SelectedValue) == "")
            {
                Alert("Informe o usuário.");
                return false;
            }
            if (Convert.ToString(txboxCodigo.Password) == "")
            {
                Alert("Informe o código");
                return false;
            }
            if (Convert.ToString(txboxNome.Value) == "")
            {
                Alert("Informe o nome.");
                return false;
            }
            if (
                Contexto.Atual.GVENDEDOR.Any(
                    r =>
                    r.idEmpresa == Memoria.Empresa && r.idFilial == Memoria.Filial && r.codigo == txboxCodigo.Password))
            {
                Alert("Código inválido.");
                return false;
            }

            return true;
        }

        private void SalvarDados()
        {
            if (validaDados())
            {
                var controlador = new VendedorControl();
                //List<string> compl = null;

                if (DataContext == null)
                    vend = new GVENDEDOR();

                vend.codUsuario = Convert.ToString(comboCodigoUsu.SelectedValue);
                vend.codigo = Convert.ToString(txboxCodigo.Password);
                vend.comissao = Convert.ToDouble(txboxComissao.Value);
                vend.nome = Convert.ToString(txboxNome.Value);
                vend.ativo = Convert.ToBoolean(checkAtivo.IsChecked);

                //Verificando se é para adicionar ou editar
                Funcoes acao;

                if (DataContext == null)
                    acao = Funcoes.Adicionar;
                else
                    acao = Funcoes.Atualizar;


                bool result = false;

                if (acao == Funcoes.Adicionar)
                {
                    result = controlador.Criar(vend);
                }
                else
                {
                    controlador.Atualizar(vend);
                }

                if (!result)
                    Alert("Verifique os dados digitados e tente novamente");
                else
                    Close();
            }
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            SalvarDados();
        }
    }
}