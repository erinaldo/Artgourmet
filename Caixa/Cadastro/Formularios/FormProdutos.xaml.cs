using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Artebit.Restaurante.Caixa.ModelView;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Atendimento;
using Artebit.Restaurante.Global.RegrasNegocio.Estoque;
using Telerik.Windows.Controls;

namespace Artebit.Restaurante.Caixa.Cadastro
{
    /// <summary>
    /// Interaction logic for FormMesas.xaml
    /// </summary>
    public partial class FormProdutos : RadWindow
    {
        private List<FidelidadeModel> listaFidelidade = new List<FidelidadeModel>();
        private EPRODUTO obj = new EPRODUTO();

        private ETABPRECO preco = new ETABPRECO();

        #region Carregamento

        public FormProdutos()
        {
            InitializeComponent();

            CarregaUnidadeMedida();
            CarregaAliquota();
            CarregaFidelidade();
        }

        public FormProdutos(EPRODUTO obj)
        {
            InitializeComponent();
            DataContext = obj;

            CarregaUnidadeMedida();
            CarregaInfo();
            CarregaFidelidade();
        }


        public void CarregaUnidadeMedida()
        {
            var control = new ProdutoControl();
            IQueryable<EUNIDADE> lista = control.BuscarUndMedida();

            cmbUndControle.ItemsSource = lista;
        }


        public void CarregaAliquota()
        {
            IQueryable<AALIQUOTA> lista = from aliquota in Contexto.Atual.AALIQUOTA
                                          select aliquota;

            var dados = from a in lista
                        select new
                                   {
                                       id = a.idAliquota,
                                       nome = a.aliquota
                                   };

            cmbAliquota.ItemsSource = dados;
        }


        public void CarregaFidelidade()
        {
            int idprd = Convert.ToInt32(obj.idProduto);

            var fid = new AFIDELIDADE();
            var control = new FidelidadeControl();

            fid.ativo = true;
            fid.tipo = 1;
            fid.idFidelidade = 0;

            IQueryable<AFIDELIDADE> lista = control.BuscarLista();


            IQueryable<FidelidadeModel> dados = from d in lista
                                                select new FidelidadeModel
                                                           {
                                                               IdFidelidade = d.idFidelidade,
                                                               Nome = d.nome,
                                                               Valor = d.APRDFIDELIDADE.FirstOrDefault(
                                                                   r =>
                                                                   r.idProduto == idprd &&
                                                                   r.idEmpresa == Memoria.Empresa).valorMoeda
                                                           };
            listaFidelidade = dados.ToList();
            gridFidelidade.ItemsSource = listaFidelidade;
        }


        // Carrega dados nos campos
        public void CarregaInfo()
        {
            CarregaAliquota();
            CarregaUnidadeMedida();

            // carrega dados nos campos
            obj = DataContext as EPRODUTO;

            if (obj != null)
            {
                #region Tab Informações

                txtCodigo.Value = obj.codigo;
                txtNome.Text = obj.nome;
                txtValidade.Value = obj.diasValidade;
                cmbUndControle.SelectedValue = obj.undControle;
                cmbAliquota.SelectedValue = obj.aliquota;
                checkAtivo.IsChecked = obj.ativo;

                var ct = new ProdutoControl();
                preco = ct.BuscarPreco(obj);

                if (preco != null)
                {
                    txtPreco1.Value = preco.preco1;
                    txtPreco2.Value = preco.preco2;
                    txtPreco3.Value = preco.preco3;
                }
                else
                {
                    preco = new ETABPRECO();
                    preco.ativo = true;
                    preco.dataAlteracao = DateTime.Now;
                    preco.idEmpresa = Convert.ToInt32(Memoria.Empresa);
                    preco.idFilial = Convert.ToInt32(Memoria.Filial);
                    preco.idProduto = obj.idProduto;
                    preco.idTabPreco = Contexto.GerarId("ETABPRECO");
                }

                switch (obj.tipoItem)
                {
                    case 1:
                        check1.IsChecked = true;
                        break;
                    case 2:
                        check2.IsChecked = true;
                        break;
                    case 3:
                        check3.IsChecked = true;
                        break;
                }

                switch (obj.tipoTributacao)
                {
                    case "T":
                        tipo1.IsChecked = true;
                        break;
                    case "S":
                        tipo3.IsChecked = true;
                        break;
                    case "I":
                        tipo2.IsChecked = true;
                        break;
                    case "N":
                        tipo4.IsChecked = true;
                        break;
                }

                #endregion

                #region Tab Detalhes

                if (obj.corPDV != "" && obj.corPDV != null)
                {
                    var color = (Color) ColorConverter.ConvertFromString("#" + obj.corPDV.Replace("#", ""));
                    var myBrush = new SolidColorBrush(color);
                    Retangulo.Fill = myBrush;
                }

                txtOrdemCardapio.Value = obj.ordemPDV;
                txtNomeResumido.Text = obj.nomeResumo;
                txtMobile.Text = obj.codigoPDV;

                #endregion
            }
            else
            {
                obj = new EPRODUTO();
            }
        }

        #endregion

        #region Botões de ação

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            SalvarDados();
            Close();
        }

        private void SalvarDados()
        {
            //Verifica se é inclusão ou alterção
            Funcoes acao;
            if (DataContext != null)
                acao = Funcoes.Atualizar;
            else
            {
                acao = Funcoes.Adicionar;
                obj.idEmpresa = Memoria.Empresa;
                obj.idProduto = Contexto.GerarId("EPRODUTO");
            }

            #region Tab Informações

            obj.codigo = Convert.ToString(txtCodigo.Value);
            obj.nome = txtNome.Text;
            obj.diasValidade = Convert.ToInt32(txtValidade.Value);
            obj.ativo = checkAtivo.IsChecked;

            if (cmbUndControle.SelectedValue != null)
                obj.undControle = Convert.ToString(cmbUndControle.SelectedValue);

            if (cmbAliquota.SelectedValue != null)
                obj.aliquota = Convert.ToInt32(cmbAliquota.SelectedValue);

            if (check1.IsChecked == true)
                obj.tipoItem = 1;
            else if (check2.IsChecked == true)
                obj.tipoItem = 2;
            else if (check3.IsChecked == true)
                obj.tipoItem = 3;

            if (tipo1.IsChecked == true)
                obj.tipoTributacao = "T";
            else if (tipo2.IsChecked == true)
                obj.tipoTributacao = "I";
            else if (tipo3.IsChecked == true)
                obj.tipoTributacao = "S";
            else if (tipo4.IsChecked == true)
                obj.tipoTributacao = "N";

            if (txtPreco1.Value != null && Convert.ToString(txtPreco1.Value) != "")
                preco.preco1 = Convert.ToDecimal(txtPreco1.Value);

            if (txtPreco2.Value != null && Convert.ToString(txtPreco2.Value) != "")
                preco.preco2 = Convert.ToDecimal(txtPreco2.Value);

            if (txtPreco3.Value != null && Convert.ToString(txtPreco3.Value) != "")
                preco.preco3 = Convert.ToDecimal(txtPreco3.Value);

            obj.ETABPRECO.Add(preco);

            #endregion

            #region Tab Detalhes

            if (Retangulo.Fill.ToString() == "#00FFFFFF")
                obj.corPDV = null;
            else
                obj.corPDV = Convert.ToString(Retangulo.Fill).Replace("#", "").Substring(2, 6);


            obj.ordemPDV = Convert.ToInt32(txtOrdemCardapio.Value);
            obj.nomeResumo = txtNomeResumido.Text;
            obj.codigoPDV = txtMobile.Text;

            #endregion

            #region Fidelidade

            //Se o preço da fidelidade for alterado atualizará a lista.
            foreach (FidelidadeModel a in gridFidelidade.Items)
            {
                foreach (FidelidadeModel f in listaFidelidade)
                {
                    if (a.IdFidelidade != f.IdFidelidade)
                    {
                        var fid = new FidelidadeModel();

                        fid.IdFidelidade = a.IdFidelidade;
                        fid.Nome = a.Nome;
                        fid.Valor = a.Valor;

                        listaFidelidade.Add(fid);
                    }
                }
            }


            obj.APRDFIDELIDADE.Clear();
            foreach (FidelidadeModel a in listaFidelidade)
            {
                var fid = new APRDFIDELIDADE();

                fid.idEmpresa = Memoria.Empresa;
                fid.idProduto = obj.idProduto;
                fid.idFidelidade = a.IdFidelidade;
                fid.idFilial = Memoria.Filial;
                fid.valorMoeda = a.Valor;

                obj.APRDFIDELIDADE.Add(fid);
            }

            #endregion

            //Atualizando banco
            var control = new ProdutoControl();

            bool result = false;

            if (acao == Funcoes.Adicionar)
            {
                result = control.Criar(obj);
            }
            else
            {
                result = control.Atualizar(obj);
            }

            if (!result)
                Alert("Verifique os dados digitados e tente novamente");
        }

        #endregion

        //Metodo para mudar a cor do retangulo quando escolhe uma cor
        private void Selector_SelectedColorChanged(object sender, EventArgs e)
        {
            var myBrush = new SolidColorBrush(Selector.SelectedColor);
            Retangulo.Fill = myBrush;
        }
    }
}