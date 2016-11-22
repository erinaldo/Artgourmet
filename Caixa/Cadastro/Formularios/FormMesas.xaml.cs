using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Microsoft.Win32;
using Telerik.Windows.Controls;
using Image = System.Drawing.Image;

namespace Artebit.Restaurante.Caixa.Cadastro
{
    /// <summary>
    /// Interaction logic for FormMesas.xaml
    /// </summary>
    public partial class FormMesas : RadWindow
    {
        private GMESA obj = new GMESA();

        public FormMesas()
        {
            InitializeComponent();

            gridStatus.ItemsSource = from p in Contexto.Atual.GSTATMESA
                                     select new
                                                {
                                                    id = p.idStatus,
                                                    p.descricao,
                                                    img = ""
                                                };
            CarregaSource();

            ObjectSet<GSTATMESA> status = Contexto.Atual.GSTATMESA;

            foreach (GSTATMESA v in status)
            {
                var im = new GIMGMESA();
                im.idEmpresa = Memoria.Empresa;
                im.idFilial = Memoria.Filial;
                im.idStatus = v.idStatus;
                im.idImagem = 1;

                obj.GIMGMESA.Add(im);
            }
            obj.idStatus = 2; //Livre
        }

        public FormMesas(GMESA obj)
        {
            InitializeComponent();
            DataContext = obj;


            gridStatus.ItemsSource = from p in Contexto.Atual.GIMGMESA
                                     where p.nuMesa == obj.nuMesa
                                     select new
                                                {
                                                    id = p.idStatus,
                                                    p.GSTATMESA.descricao,
                                                    img = p.GIMAGEM.dado
                                                };

            CarregaSource();
            CarregaInfo();
        }

        private void CarregaSource()
        {
            Lugares.ItemsSource = Contexto.Atual.RGRUPOMESA;

            gridImpressoras.ItemsSource = Contexto.Atual.AIMPRESSORA;
        }

        public void CarregaInfo()
        {
            // carrega dados nos campos
            obj = DataContext as GMESA;

            if (obj != null)
            {
                txtNuMesa.Value = obj.nuMesa;
                Lugares.SelectedItem = obj.RGRUPOMESA;
                txtObservacao.Text = obj.observacao;
                checkAtivo.IsChecked = obj.ativo;

                var lista = new List<AIMPRESSORA>();

                foreach (AIMPRESSMESA it in obj.AIMPRESSMESA)
                {
                    lista.Add(it.AIMPRESSORA);
                }

                gridImpressoras.Select(lista.AsEnumerable());
            }
            else
            {
                obj = new GMESA();
            }
        }

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
            obj.nuMesa = Convert.ToInt32(txtNuMesa.Value);
            obj.RGRUPOMESA = Lugares.SelectedItem as RGRUPOMESA;
            obj.ativo = checkAtivo.IsChecked ?? true;
            obj.observacao = txtObservacao.Text;

            obj.AIMPRESSMESA.Clear();


            foreach (AIMPRESSORA it in gridImpressoras.SelectedItems)
            {
                var ob = new AIMPRESSMESA();
                ob.idEmpresa = obj.idEmpresa;
                ob.idFilial = obj.idFilial;
                ob.nuMesa = obj.nuMesa;
                ob.idImpressora = it.idImpressora;

                obj.AIMPRESSMESA.Add(ob);
            }

            //Verifica se é inclusão ou alterção
            Funcoes acao;
            if (DataContext != null)
                acao = Funcoes.Atualizar;
            else
                acao = Funcoes.Adicionar;

            //Atualizando banco
            var control = new MesaControl();

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

        private void AlterarImagem_Click(object sender, RoutedEventArgs e)
        {
            var bt = (Button) sender;

            var i = bt.Parent.FindChildByType<System.Windows.Controls.Image>();
            int id = Convert.ToInt32(bt.CommandParameter);

            GIMGMESA gi = obj.GIMGMESA.Where(r => r.idStatus == id).FirstOrDefault();

            var img = new GIMAGEM();
            img.idEmpresa = Memoria.Empresa;
            img.idImagem = Contexto.GerarId("GIMAGEM");

            string path = AdicionaImagem(ref img);

            if (path != null)
            {
                if (i != null)
                {
                    i.Source = new BitmapImage(new Uri(path));
                }

                gi.GIMAGEM = img;
            }
        }

        private byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }

        //Open file into a filestream and 
        //read data in a byte array.
        private byte[] ReadFile(string sPath)
        {
            //Initialize byte array with a null value initially.
            byte[] data = null;

            //Use FileInfo object to get file size.
            var fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            var fStream = new FileStream(sPath, FileMode.Open,
                                         FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            var br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to 

            //supply number of bytes to read from file.
            //In this case we want to read entire file. 

            //So supplying total number of bytes.
            data = br.ReadBytes((int) numBytes);
            return data;
        }

        private string AdicionaImagem(ref GIMAGEM img)
        {
            Stream FileOpenStream = null;
            var FileBox = new OpenFileDialog();
            FileBox.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            FileBox.Filter = "Pictures (*.jpg;*.jpeg;*.gif;*.png)|*.jpg;*.jpeg;*.gif;*.png|" +
                             "All Files (*.*)|*.*";
            FileBox.FilterIndex = 1;
            FileBox.Multiselect = false;
            bool? FileSelected = FileBox.ShowDialog();

            if (FileSelected.HasValue)
            {
                if (FileSelected.Value)
                {
                    FileOpenStream = FileBox.OpenFile();

                    if (FileOpenStream != null)
                    {
                        byte[] ByteArray;
                        using (var br = new BinaryReader(FileOpenStream))
                        {
                            ByteArray = br.ReadBytes(Convert.ToInt32(FileOpenStream.Length));
                        }

                        img.dado = ByteArray;
                        img.extensao = "." + FileBox.FileName.Split('.').Last().ToLower();
                        img.mime = "." + FileBox.FileName.Split('.').Last().ToLower();
                        return FileBox.FileName;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}