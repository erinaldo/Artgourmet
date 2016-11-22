using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Artebit.Restaurante.Global.Modelo;
using Telerik.Windows.Controls;

namespace Artebit.Restaurante.Caixa.Cadastro.Formularios
{
    /// <summary>
    /// Interaction logic for FormPerfil.xaml
    /// </summary>
    public partial class FormPerfil
    {
        //Formulário chamado quando adiciona um novo usuário
        public FormPerfil()
        {
            InitializeComponent();

            cbSistema.ItemsSource = Contexto.Atual.GSISTEMA;

            var obj = new GPERFIL {idEmpresa = 0};

            DataContext = obj;
        }

        //Formulário chamado quando se edita um usuário
        public FormPerfil(GPERFIL obj)
        {
            InitializeComponent();
            DataContext = obj;

            cbSistema.ItemsSource = Contexto.Atual.GSISTEMA;
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            var obj = DataContext as GPERFIL;

            if(obj.idEmpresa != 0)
                Contexto.Atual.SaveChanges();
            else
            {
                obj.idEmpresa = Memoria.Empresa;
                obj.idPerfil = Contexto.GerarId("GPERFIL");
                obj.dataInclusao = DateTime.Now;
                obj.dataAlteracao = DateTime.Now;

                Contexto.Atual.AddToGPERFIL(obj);

                Contexto.Atual.SaveChanges();
            }

            Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

      
    }
}
