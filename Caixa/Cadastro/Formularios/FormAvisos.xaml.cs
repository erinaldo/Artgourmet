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
    /// Interaction logic for FormAvisos.xaml
    /// </summary>
    public partial class FormAvisos
    {
        public FormAvisos()
        {
            InitializeComponent();
            DataContext = new AAVISOS {idEmpresa = 0, idFilial = 0};
        }

         //Formulário chamado quando se edita um usuário
        public FormAvisos(AAVISOS obj)
        {
            InitializeComponent();
            DataContext = obj;
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            var obj = DataContext as AAVISOS;

            if (obj.idEmpresa != 0)
                Contexto.Atual.SaveChanges();
            else
            {
                obj.idEmpresa = Memoria.Empresa;
                obj.idFilial = Memoria.Filial;
                obj.idAviso = Contexto.GerarId("AAVISOS");

                Contexto.Atual.AddToAAVISOS(obj);

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
