using System;
using System.Windows;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Atendimento;
using Telerik.Windows.Controls;

namespace Artebit.Restaurante.Caixa.Cadastro
{
    /// <summary>
    /// Interaction logic for FormAliquota.xaml
    /// </summary>
    public partial class FormAliquota : RadWindow
    {
        private AALIQUOTA ali = new AALIQUOTA();

        public FormAliquota(AALIQUOTA obj)
        {
            InitializeComponent();
            ali = obj;
            CarregarInfo();
        }

        private void CarregarInfo()
        {
            if (ali != null)
            {
                txtAliquota.Text = ali.aliquota;
                txtALQ.Value = ali.valor;
                ckbAtivo.IsChecked = ali.ativo;

                //carrega o radio button
                switch (ali.tpImposto)
                {
                    case "ICMS":
                        radioICMS.IsChecked = true;
                        break;
                    case "ISS":
                        radioISS.IsChecked = true;
                        break;
                    default:
                        radioICMS.IsChecked = false;
                        radioISS.IsChecked = false;
                        break;
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            Funcoes func;
            if (ali == null)
            {
                func = Funcoes.Adicionar;
                ali = new AALIQUOTA();
            }
            else
            {
                func = Funcoes.Atualizar;
            }

            ali.aliquota = txtAliquota.Text;
            //ali.posicao = Convert.ToDouble(txtPosicao.Value);
            ali.valor = Convert.ToDecimal(txtALQ.Value);
            ali.ativo = ckbAtivo.IsChecked;

            //carrega o radio button
            if (radioICMS.IsChecked == true)
            {
                ali.tpImposto = "ICMS";
            }
            else
            {
                if (radioISS.IsChecked == true)
                {
                    ali.tpImposto = "ISS";
                }
                else
                {
                    ali.tpImposto = null;
                }
            }


            var ctrl = new AliquotaControl();

            bool result = false;

            if (func == Funcoes.Adicionar)
            {
                result = ctrl.Criar(ali);
            }
            else
            {
                result = ctrl.Atualizar(ali);
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