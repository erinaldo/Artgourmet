using System;
using System.Linq;
using System.Windows;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio.Global;

namespace Artebit.Restaurante.Caixa.Caixas.Janelas
{
    /// <summary>
    /// Interaction logic for Transferir.xaml
    /// </summary>
    public partial class SepararMesa
    {
        public string Resultado = "";
        private ACONTA _conta = new ACONTA();

        public SepararMesa()
        {
            InitializeComponent();

            txtNuMesa.Focus();
        }

        public void CarregarInfo()
        {
            _conta = DataContext as ACONTA;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (txtNuMesa.Text != "")
            {
                if (Convert.ToInt32(txtNuMesa.Text) != _conta.nuMesa)
                {
                    var preconta = new PreContaControl();

                    if (preconta.ValidarMesaAssociada(_conta, txtNuMesa.Text))
                    {
                        int numesa = Convert.ToInt32(txtNuMesa.Text);

                        if (numesa == _conta.nuMesa)
                        {
                            var associacao = _conta.AASSOCIACAO.FirstOrDefault();
                            if (associacao != null)
                            {
                                int? novamesa = associacao.nuMesa;

                                if (novamesa == null)
                                {
                                    Alert("Mesa única, não pode separa. ");
                                    Close();
                                }
                                _conta.nuMesa = novamesa;

                                foreach (AASSOCIACAO nnn in _conta.AASSOCIACAO.Where(a => a.nuMesa == novamesa))
                                {
                                    _conta.AASSOCIACAO.Remove(nnn);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            foreach (AASSOCIACAO nnn in _conta.AASSOCIACAO.Where(a => a.nuMesa == numesa))
                            {
                                _conta.AASSOCIACAO.Remove(nnn);
                                break;
                            }
                        }


                        Memoria.LogAcao = "Separar da mesa: " + txtNuMesa.Text;
                        bool result = preconta.Atualizar(_conta);

                        if (result)
                        {
                            var c = new MesaControl();
                            var mesa = new GMESA {nuMesa = Convert.ToInt32(txtNuMesa.Text)};
                            mesa = c.Buscar(mesa);

                            mesa.idStatus = 2;

                            result = c.Atualizar(mesa);

                            if (result)
                            {
                                Resultado = txtNuMesa.Text;
                                Close();
                            }
                            else
                            {
                                Alert("Erro ao atualizar status da mesa. ");
                                Close();
                            }
                        }
                        else
                        {
                            Alert("Erro ao separar mesa. ");
                            Close();
                        }
                    }
                    else
                    {
                        Alert("Mesa inválida.");
                        Close();
                    }
                }
                else
                {
                    Alert("Seleciona uma mesa diferente da que você está.");
                    Close();
                }
            }
            else
            {
                Alert("Selecione uma mesa.");
                Close();
            }
        }

        //private void btnOK_Click(object sender, RoutedEventArgs e)
        //{
        //    if (txtNuMesa.Text != "")
        //    {
        //        if (Convert.ToInt32(txtNuMesa.Text) != conta.nuMesa)
        //        {
        //            PreConta preconta = new PreConta();

        //            List<string> compl = new List<string>();

        //            compl.Add(txtNuMesa.Text);

        //            if ((bool)preconta.ExecutaFuncao(conta, Funcoes.ValidaMesaAssociada, compl))
        //            {

        //                int numesa = Convert.ToInt32(txtNuMesa.Text);

        //                if (numesa == conta.nuMesa)
        //                {
        //                    int? novamesa = conta.AMESASASSOCIADAS.FirstOrDefault().nuMesa;

        //                    if (novamesa == null)
        //                    {
        //                        RadWindow.Alert("Mesa única, não pode separa. ");
        //                        return;
        //                    }
        //                    conta.nuMesa = novamesa;

        //                    foreach (AMESASASSOCIADAS nnn in conta.AMESASASSOCIADAS.Where(a => a.nuMesa == novamesa))
        //                    {
        //                        conta.AMESASASSOCIADAS.Remove(nnn);
        //                        break;
        //                    }

        //                }
        //                else
        //                {
        //                    foreach (AMESASASSOCIADAS nnn in conta.AMESASASSOCIADAS.Where(a => a.nuMesa == numesa))
        //                    {
        //                        conta.AMESASASSOCIADAS.Remove(nnn);
        //                        break;
        //                    }
        //                }


        //                Memoria.LogAcao = "Separar da mesa: " + txtNuMesa.Text;
        //                bool result = (bool) preconta.ExecutaFuncao(conta, Global.RegrasNegocio.Funcoes.Atualizar, null);

        //                if (result)
        //                {
        //                    Mesa c = new Mesa();
        //                    GMESA mesa = new GMESA();
        //                    mesa.nuMesa = Convert.ToInt32(txtNuMesa.Text);
        //                    mesa = (GMESA) c.ExecutaFuncao(mesa, Global.RegrasNegocio.Funcoes.Buscar, null, null);

        //                    mesa.idStatus = 2;

        //                    result = (bool) c.ExecutaFuncao(mesa, Global.RegrasNegocio.Funcoes.Atualizar, null, null);

        //                    if (result)
        //                    {
        //                        Resultado = txtNuMesa.Text;
        //                        this.Close();
        //                    }
        //                    else
        //                    {
        //                        RadWindow.Alert("Erro ao atualizar status da mesa. ");
        //                    }
        //                }
        //                else
        //                {
        //                    RadWindow.Alert("Erro ao separar mesa. ");
        //                }
        //            } else
        //            {
        //                RadWindow.Alert("Mesa inválida.");
        //            }
        //        } else
        //        {
        //            RadWindow.Alert("Seleciona uma mesa diferente da que você está.");
        //        }
        //    } else
        //    {
        //        RadWindow.Alert("Slecione uma mesa.");
        //    }
        //}

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}