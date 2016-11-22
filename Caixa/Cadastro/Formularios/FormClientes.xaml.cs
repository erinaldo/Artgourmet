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
    /// Interaction logic for FormClientes.xaml
    /// </summary>
    public partial class FormClientes : RadWindow
    {
        public GCLIFOR cliente;

        public FormClientes(GCLIFOR _cliente)
        {
            InitializeComponent();

            cliente = _cliente;

            CarregaInfo();
        }

        private void CarregaInfo()
        {
            if (cliente != null)
            {
                txtNome.Text = cliente.nomeFantasia;
                txtCPF.Value = cliente.cpfcpnj;
                ckbAtivo.IsChecked = cliente.ativo;

                GCLIFOREND end = cliente.GCLIFOREND.SingleOrDefault(r => r.tipoEndereco == 1);

                txtRua.Text = end.rua;
                txtNumero.Text = end.numero;
                txtBairro.Text = end.bairro;
                txtComplemento.Text = end.complemento;
                txtCidade.Text = end.cidade;
                txtUF.Text = end.uf;
                txtCEP.Value = end.cep;
                txtDDD.Value = end.ddd1;
                txtTelefone.Value = end.telefone1;
                txtEmail.Text = end.email;

                Header = "Atualização de Cliente";
            }
            else
            {
                Header = "Novo Cliente";
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            Funcoes func;
            var cliend = new GCLIFOREND();

            if (cliente == null)
            {
                func = Funcoes.Adicionar;
                cliente = new GCLIFOR();
                cliente.idClifor = Contexto.GerarId("GCLIFOR");
                cliente.tpClifor = 1; //Cliente

                cliend.idClifor = cliente.idClifor;
                cliente.idEmpresa = Memoria.Empresa;
            }
            else
            {
                func = Funcoes.Atualizar;
                cliend = cliente.GCLIFOREND.SingleOrDefault(r => r.tipoEndereco == 1);
            }

            cliente.nomeFantasia = txtNome.Text;
            cliente.cpfcpnj = Convert.ToString(txtCPF.Value);
            cliente.ativo = ckbAtivo.IsChecked;

            cliend.rua = txtRua.Text;
            cliend.numero = txtNumero.Text;
            cliend.bairro = txtBairro.Text;
            cliend.complemento = txtComplemento.Text;
            cliend.cidade = txtCidade.Text;
            cliend.uf = txtUF.Text;
            cliend.cep = txtCEP.Value.ToString();
            cliend.tipoEndereco = 1;
            cliend.email = txtEmail.Text;

            cliend.ddd1 = txtDDD.Value.ToString();
            cliend.telefone1 = txtTelefone.Value.ToString();

            if (func == Funcoes.Adicionar)
            {
                cliend.nuEndereco = 1;
                cliente.GCLIFOREND.Add(cliend);
            }


            var fornCtrl = new FornecedorControl();

            bool result = false;

            if (func == Funcoes.Adicionar)
            {
                result = fornCtrl.Criar(cliente);
            }
            else
            {
                result = fornCtrl.Atualizar(cliente);
            }

            if (result)
            {
                Alert("Dados enviados com sucesso.");
            }
            else
            {
                Alert("Erro ao enviar dados.");
            }

            Close();
        }
    }
}