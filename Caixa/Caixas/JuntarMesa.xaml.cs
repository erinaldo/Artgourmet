using System;
using System.Windows;
using Artebit.Restaurante.Global.AcessoDados.Global;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio.Global;

namespace Artebit.Restaurante.Caixa.Caixas
{
    /// <summary>
    /// Interaction logic for Transferir.xaml
    /// </summary>
    public partial class JuntarMesa
    {
        public string Resultado = "";
        private ACONTA _conta = new ACONTA();

        public JuntarMesa()
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
            var preconta = new PreContaControl();

            if (txtNuMesa.Text != "")
            {
                if (Convert.ToInt32(txtNuMesa.Text) != _conta.nuMesa)
                {
                    var mes = new AASSOCIACAO
                                  {
                                      idEmpresa = Memoria.Empresa,
                                      idFilial = Memoria.Filial,
                                      nuMesa = Convert.ToInt32(txtNuMesa.Text),
                                      idConta = _conta.idConta
                                  };


                    var mm = new MesaControl();
                    if (mes.nuMesa != null)
                    {
                        var mmm = new GMESA {nuMesa = mes.nuMesa.Value};
                        mmm = mm.Buscar(mmm);

                        // Valida se a mesa está Ocupada ou Livre
                        if (mmm.idStatus == 1 || mmm.idStatus == 2)
                        {

                            if (! preconta.ValidarMesaAssociada(_conta, txtNuMesa.Text))
                            {
                                #region Validação da conta da mesa

                                bool valida = mm.ValidarConta(mmm);

                                if (valida)
                                {
                                    var preconta2 = new PreContaControl();
                                    var conta2 = new ACONTA {nuMesa = Convert.ToInt32(txtNuMesa.Text)};

                                    var pqp =
                                        new PreContaDAL();

                                    conta2 = pqp.BuscarByMesa(conta2);

                                    //conta2.nuMesaAnterior = conta2.nuMesa;
                                    conta2.nuMesa = _conta.nuMesa;

                                    int pessoaconta2 = conta2.pessoas;

                                    int totalpessoa = _conta.pessoas + pessoaconta2;


                                    bool result2 = conta2.nuMesa != null && preconta2.Transferir(conta2, conta2.nuMesa.Value);

                                    if (!result2)
                                    {
                                        Alert("Erro ao juntar mesa. ");
                                        Close();
                                    }

                                    _conta.pessoas = totalpessoa;
                                }

                                #endregion

                                _conta.AASSOCIACAO.Add(mes);

                                Memoria.LogAcao = "Juntar com a mesa: " + txtNuMesa.Text;
                                bool result = preconta.Atualizar(_conta);

                                if (result)
                                {
                                    var c = new MesaControl();
                                    var mesa2 = new GMESA {nuMesa = Convert.ToInt32(txtNuMesa.Text)};
                                    mesa2 = c.Buscar(mesa2);

                                    mesa2.idStatus = 1;

                                    result = c.Atualizar(mesa2);

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
                                    Alert("Erro ao juntar mesa. ");
                                    Close();
                                }
                            }
                            else
                            {
                                Alert("Favor selecionar outra mesa!");
                                Close();
                            }
                        }
                        else
                        {
                            Alert("Mesa inválida.");
                            Close();
                        }
                    }
                }
                else
                {
                    Alert("Favor selecionar outra mesa!");
                    Close();
                }
            }
            else
            {
                Alert("Selecione uma mesa.");
            }
        }

        //private void btnOK_Click(object sender, RoutedEventArgs e)
        //{
        //    PreConta preconta = new PreConta();

        //    if (Convert.ToInt32(txtNuMesa.Text) != conta.nuMesa)
        //    {
        //        int? totalpessoa = 0;

        //        AMESASASSOCIADAS mes = new AMESASASSOCIADAS();
        //        mes.idEmpresa = Memoria.Empresa.Value;
        //        mes.idFilial = Memoria.Filial.Value;
        //        mes.nuMesa = Convert.ToInt32(txtNuMesa.Text);
        //        mes.idConta = conta.idConta;

        //        Mesa mm = new Mesa();
        //        GMESA mmm = new GMESA();
        //        mmm.nuMesa = mes.nuMesa;
        //        mmm = (GMESA) mm.ExecutaFuncao(mmm, Global.RegrasNegocio.Funcoes.Buscar, null, null);

        //        bool valida = (bool) mm.ExecutaFuncao(mmm, Global.RegrasNegocio.Funcoes.ValidaConta, null, null);

        //        if (valida == true)
        //        {
        //            PreConta preconta2 = new PreConta();
        //            ACONTA conta2 = new ACONTA();
        //            conta2.nuMesa = Convert.ToInt32(txtNuMesa.Text);

        //            Artebit.Restaurante.Global.AcessoDados.Global.PreContaDAL pqp =
        //                new Artebit.Restaurante.Global.AcessoDados.Global.PreContaDAL();

        //            conta2 = pqp.BuscaByMesa(conta2);

        //            int? pessoaconta2 = 0;

        //            if (conta2.pessoas != null)
        //            {
        //                pessoaconta2 = conta2.pessoas;
        //            }

        //            totalpessoa = conta.pessoas + pessoaconta2;

        //            conta2.nuMesaAnterior = conta2.nuMesa;
        //            conta2.nuMesa = conta.nuMesa;

        //            GMESA mesa = new GMESA();
        //            Mesa cmesa = new Mesa();
        //            mesa.nuMesa = Convert.ToInt32(txtNuMesa.Text);

        //            bool result2 = false;

        //            result2 = (bool) preconta2.ExecutaFuncao(conta2, Global.RegrasNegocio.Funcoes.Transferir, null);

        //            if (!result2)
        //            {
        //                RadWindow.Alert("Erro ao juntar mesa. ");
        //            }

        //            conta.pessoas = totalpessoa;
        //        }

        //        conta.AMESASASSOCIADAS.Add(mes);

        //        bool result = (bool) preconta.ExecutaFuncao(conta, Global.RegrasNegocio.Funcoes.Atualizar, null);

        //        if (result)
        //        {
        //            Mesa c = new Mesa();
        //            GMESA mesa = new GMESA();
        //            mesa.nuMesa = Convert.ToInt32(txtNuMesa.Text);
        //            mesa = (GMESA) c.ExecutaFuncao(mesa, Global.RegrasNegocio.Funcoes.Buscar, null, null);

        //            mesa.idStatus = 1;

        //            result = (bool) c.ExecutaFuncao(mesa, Global.RegrasNegocio.Funcoes.Atualizar, null, null);

        //            if (result)
        //            {
        //                Resultado = txtNuMesa.Text;
        //                this.Close();
        //            }
        //            else
        //            {
        //                RadWindow.Alert("Erro ao atualizar status da mesa. " + Memoria.MsgGlobal);
        //            }
        //        }
        //        else
        //        {
        //            RadWindow.Alert("Erro ao juntar mesa. " + Memoria.MsgGlobal);
        //        }
        //    } else
        //    {
        //        RadWindow.Alert("Favor selecionar outra mesa!");
        //    }

        //}

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}