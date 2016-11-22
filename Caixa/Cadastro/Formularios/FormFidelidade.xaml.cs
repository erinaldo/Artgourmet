using System;
using System.Linq;
using System.Windows;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Atendimento;
using Telerik.Windows.Controls;

namespace Artebit.Restaurante.Caixa.Cadastro
{
    /// <summary>
    /// Interaction logic for FormFidelidade.xaml
    /// </summary>
    public partial class FormFidelidade : RadWindow
    {
        private AFIDELIDADE fid = new AFIDELIDADE();


        public FormFidelidade(AFIDELIDADE obj)
        {
            InitializeComponent();
            radioConsumo.IsChecked = true;
            fid = obj;
            CarregarInfo();
        }

        private void CarregarInfo()
        {
            if (fid != null)
            {
                txtMoeda.Text = fid.moeda;
                txtNome.Text = fid.nome;
                ckbAtivo.IsChecked = fid.ativo;
                txtPontos.Value = fid.valorPorReal;
                ckbDiaTodo.IsChecked = fid.diaTodo;
                txtHorarioIni.Value = fid.horarioInicial;
                txtHorarioFim.Value = fid.horarioFinal;
                //carrega o radio button
                switch (fid.tipo)
                {
                    case 1:
                        radioProduto.IsChecked = true;
                        break;
                    case 2:
                        radioConsumo.IsChecked = true;
                        break;
                    default:
                        radioConsumo.IsChecked = false;
                        radioProduto.IsChecked = false;
                        break;
                }

                //Carrega a grid produtos

                var dados = from a in fid.APRDFIDELIDADE
                            select new
                                       {
                                           codigo = a.idProduto,
                                           a.EPRODUTO.nome,
                                           valor = a.valorMoeda
                                       };

                gridProdutos.ItemsSource = dados;
                gridProdutos.Rebind();
                gridProdutos.Focus();
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            Funcoes func;
            if (fid == null)
            {
                func = Funcoes.Adicionar;
                fid = new AFIDELIDADE();
            }
            else
            {
                func = Funcoes.Atualizar;
            }

            fid.moeda = txtMoeda.Text;
            fid.nome = txtNome.Text;
            fid.ativo = ckbAtivo.IsChecked;
            fid.valorPorReal = Convert.ToDecimal(txtPontos.Value);
            //carrega o radio button
            if (radioProduto.IsChecked == true)
            {
                fid.tipo = 1;
            }
            else
            {
                if (radioConsumo.IsChecked == true)
                {
                    fid.tipo = 2;
                }
                else
                {
                    fid.tipo = null;
                }
            }

            fid.diaTodo = ckbDiaTodo.IsChecked;

            if (ckbDiaTodo.IsChecked == false)
            {
                //string dia = "01/01/2000 00:00:00";
                string horIni = txtHorarioIni.Value.ToString();
                string horFim = txtHorarioFim.Value.ToString();
                string dia = DateTime.Now.Date.ToString();
                dia = dia.Replace(" 00:00:00", "");
                fid.horarioInicial = Convert.ToDateTime(horIni.Replace(dia, "01/01/2000"));
                fid.horarioFinal = Convert.ToDateTime(horFim.Replace(dia, "01/01/2000"));
            }
            else
            {
                fid.horarioInicial = null;
                fid.horarioFinal = null;
            }


            var ctrl = new FidelidadeControl();

            bool result = false;

            if (func == Funcoes.Adicionar)
            {
                result = ctrl.Criar(fid);
            }
            else
            {
                result = ctrl.Atualizar(fid);
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

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            txtPontos.IsEnabled = false;
            label3.IsEnabled = false;
            RadTabProdutos.IsEnabled = true;
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            txtPontos.IsEnabled = true;
            label3.IsEnabled = true;
            RadTabProdutos.IsEnabled = false;
        }

        private void ckbDiaTodo_Click(object sender, RoutedEventArgs e)
        {
            if (ckbDiaTodo.IsChecked == true)
            {
                label5.IsEnabled = false;
                label6.IsEnabled = false;
                txtHorarioIni.IsEnabled = false;
                txtHorarioFim.IsEnabled = false;
            }
            else
            {
                label5.IsEnabled = true;
                label6.IsEnabled = true;
                txtHorarioIni.IsEnabled = true;
                txtHorarioFim.IsEnabled = true;
            }
        }
    }
}