using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Artebit.Restaurante.Global.Util;
using Telerik.Windows.Controls;

namespace Artebit.Restaurante.Caixa.Cadastro.Formularios
{
    /// <summary>
    /// Interaction logic for FormUsuario.xaml
    /// </summary>
    public partial class FormUsuario
    {
        private readonly bool _editar;
        private List<string> _codSistema;
        private UsuarioControl _control = new UsuarioControl();
        private List<CheckBox> _listaCheckBox;
        private List<RadComboBox> _listaComboBox;
        private GUSUARIO _usu = new GUSUARIO();

        //Formulário chamado quando adiciona um novo usuário
        public FormUsuario()
        {
            InitializeComponent();
            _usu.codusuario = Memoria.Codusuario;
            CarregaPerfil();
        }

        //Formulário chamado quando se edita um usuário
        public FormUsuario(GUSUARIO obj)
        {
            InitializeComponent();
            CarregaInfo(obj);
            _editar = true;
            CarregaPerfil();
        }

        public void CarregaInfo(GUSUARIO obj)
        {
            //Informações do usuário
            _usu = obj;
            txtboxCodUsuario.IsReadOnly = true;
            txtboxCodUsuario.Value = obj.codusuario;
            txtboxNome.Value = obj.nome;
            senha.Password = "";
            checkUsuario.IsChecked = obj.ativo;
        }

        public void CarregaPerfil()
        {
            // Limpar as listas
            _codSistema = new List<string>();
            _listaComboBox = new List<RadComboBox>();
            _listaCheckBox = new List<CheckBox>();
            _control = new UsuarioControl();

            //Buscar todos os sistemas que o usuário logado tem acesso
            var usulog = new GUSUARIO {codusuario = Memoria.Codusuario};
            IQueryable<GSISTEMA> listaSistemas = _control.BuscarSistemaPerfil(usulog);

            //Buscar todos os sistemas que o usuário selecionado na grid tem acesso.
            IQueryable<GUSRFILMOD> sisSelecionado = _control.BuscarSistemaPerfil2(_usu);

            //Montar a grid
            var rowgrid = new List<int>();
            for (int x = 0; x <= listaSistemas.Count(); x++)
            {
                var row = new RowDefinition {Height = new GridLength(35, GridUnitType.Pixel)};
                gridScroll.RowDefinitions.Add(row);
                rowgrid.Add(x);
            }

            //Adicionando o TextBlock, ComboBox e Checkbox na grid.
            int contRow = 0;
            foreach (GSISTEMA fil in listaSistemas)
            {
                // Textblock
                string codigoSistema = fil.codSistema;
                string nomeSistema = fil.descricao;
                var txtblock = new TextBlock
                                   {
                                       Text = nomeSistema,
                                       HorizontalAlignment = HorizontalAlignment.Center,
                                       VerticalAlignment = VerticalAlignment.Center
                                   };
                Grid.SetRow(txtblock, rowgrid[contRow]);
                Grid.SetColumn(txtblock, 0);
                gridScroll.Children.Add(txtblock);

                //Combobox
                var cbx = new RadComboBox
                              {
                                  Name = "ComboBox" + codigoSistema,
                                  HorizontalAlignment = HorizontalAlignment.Left,
                                  VerticalAlignment = VerticalAlignment.Center,
                                  Width = 194,
                                  Height = 25
                              };
                Grid.SetRow(cbx, rowgrid[contRow]);
                Grid.SetColumn(cbx, 1);
                cbx.SelectedValuePath = "descricao";
                cbx.DisplayMemberPath = "descricao";
                gridScroll.Children.Add(cbx);
                cbx.ItemsSource = CarregaComboBox(fil.codSistema);
                int selecionado = cbx.Items.Count;
                selecionado--;
                cbx.SelectedIndex = selecionado;
                _codSistema.Add(codigoSistema);
                _listaComboBox.Add(cbx);

                //Check Box ativo
                var check = new CheckBox();
                Grid.SetRow(check, rowgrid[contRow]);
                Grid.SetColumn(check, 3);
                check.HorizontalAlignment = HorizontalAlignment.Left;
                check.VerticalAlignment = VerticalAlignment.Center;
                check.Name = "CheckBox" + codigoSistema;
                gridScroll.Children.Add(check);
                _listaCheckBox.Add(check);
                contRow++;

                //Preenchendo os campos se o usuario for editar
                if (_editar)
                {
                    // Fazer um loop para verificar se o usário tem perfil para o sistema.
                    foreach (GUSRFILMOD guser in sisSelecionado)
                    {
                        if (guser.codSistema == fil.codSistema)
                        {
                            var perfil = new GPERFIL();
                            var ctrl = new PerfilControl();
                            perfil.idPerfil = Convert.ToInt32(guser.idPerfil);
                            perfil = ctrl.Buscar(perfil);
                            cbx.SelectedValue = perfil.descricao;
                            check.IsChecked = guser.supervisor;
                            txtboxCodUsuario.IsReadOnly = true;
                            break;
                        }
                        else
                        {
                            cbx.SelectedValue = "0";
                            cbx.SelectedIndex = selecionado;
                            check.IsChecked = false;
                        }
                    }
                }
            }
        }

        private IEnumerable<GPERFIL> CarregaComboBox(String sistema)
        {
            //Buscar os perfis para carregar no combobox
            var controlador = new PerfilControl();
            IQueryable<GPERFIL> lista = controlador.BuscarLista();

            var perfil = new GPERFIL {descricao = "Selecione", idPerfil = 0, codSistema = sistema};

            List<GPERFIL> list = lista.Where(l => l.codSistema == sistema).ToList();
            list.Add(perfil);

            return list;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            SalvarDados();
        }

        private bool validaDados()
        {
            if (Convert.ToString(txtboxCodUsuario.Value) == "")
            {
                Alert("Informe um código de usuário.");
                return false;
            }


            if (Convert.ToString(txtboxNome.Value) == "")
            {
                Alert("Informe um nome de usuário.");
                return false;
            }

            if (Convert.ToString(senha.Password) == "")
            {
                Alert("Informe a senha do usuário.");
                return false;
            }

            return true;
        }

        private void SalvarDados()
        {
            Funcoes acao = _editar ? Funcoes.Atualizar : Funcoes.Adicionar;

            if (validaDados())
            {
                //Recebendo os dados digitados do usuario
                _usu.codusuario = Convert.ToString(txtboxCodUsuario.Value);
                _usu.nome = Convert.ToString(txtboxNome.Value);
                if (!string.IsNullOrEmpty(senha.Password))
                {
                    _usu.senha = Criptografia.GerarSHA1(Criptografia.GerarMD5(Convert.ToString(senha.Password)));
                }
                _usu.ativo = checkUsuario.IsChecked;

                //Recebendo os informações dos perfis
                var listaPerfil = new List<GUSRFILMOD>();

                //Se o perfil for preenchido ele vai ser adicionado em uma lista, caso contrário o perfil será excluído.
                for (int i = 0; i < _codSistema.Count; i++)
                {
                    if (Convert.ToString(_listaComboBox[i].SelectedValue) != "Selecione")
                    {
                        var mod = new GUSRFILMOD
                                      {
                                          idEmpresa = Convert.ToInt32(Memoria.Empresa),
                                          codUsuario = _usu.codusuario,
                                          idFilial = Convert.ToInt32(Memoria.Filial),
                                          codSistema = Convert.ToString(_codSistema[i]),
                                          supervisor = Convert.ToBoolean(_listaCheckBox[i].IsChecked)
                                      };
                        var busca = new PerfilControl();
                        var obj = new GPERFIL
                                      {
                                          descricao = Convert.ToString(_listaComboBox[i].SelectedValue),
                                          codSistema = _codSistema[i]
                                      };
                        obj = busca.Buscar(obj);
                        mod.idPerfil = obj.idPerfil;
                        listaPerfil.Add(mod);
                    }
                    else
                    {
                        GUSRFILMOD tt = _usu.GUSRFILMOD.SingleOrDefault(r => r.codSistema == _codSistema[i]
                                                                            && r.codUsuario == _usu.codusuario);

                        if (tt != null)
                        {
                            _usu.GUSRFILMOD.Remove(tt);
                        }
                    }
                }

                //Adicionando os perfils ao usuário.
                foreach (GUSRFILMOD gup in listaPerfil)
                {
                    GUSRFILMOD tt = _usu.GUSRFILMOD.SingleOrDefault(r => r.codSistema == gup.codSistema);

                    if (tt != null)
                    {
                        _usu.GUSRFILMOD.Remove(tt);
                    }

                    _usu.GUSRFILMOD.Add(gup);
                }

                //Atualizando ou adicionando usuario
                bool resultUsuario = acao == Funcoes.Adicionar ? _control.Criar(_usu) : _control.Atualizar(_usu);

                if (!resultUsuario)
                    Alert("Verifique os dados digitados e tente novamente");
                else
                    Close();
            }
        }
    }
}