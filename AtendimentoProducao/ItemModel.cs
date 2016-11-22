using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.AtendimentoProducao
{
    public class ItemModel : INotifyPropertyChanged
    {
        public ItemModel()
        {
            this.icone = "Img/verde.png";
        }

        private int idConta;

        public int IdConta
        {
            get { return idConta; }
            set { idConta = value; }
        }
        private int nuItem;

        public int NuItem
        {
            get { return nuItem; }
            set { nuItem = value; }
        }
        private int idStatus;

        public int IdStatus
        {
            get { return idStatus; }
            set 
            { 
                idStatus = value;

                OnPropertyChanged("IdStatus");
            }
        }
        private string produto;

        public string Produto
        {
            get { return produto; }
            set { produto = value; }
        }
        private decimal? quantidade;

        public decimal? Quantidade
        {
            get { return quantidade; }
            set { quantidade = value; }
        }
        private string vendedor;

        public string Vendedor
        {
            get { return vendedor; }
            set { vendedor = value; }
        }
        private IEnumerable<ACONTITEM> itens;

        public IEnumerable<ACONTITEM> Itens
        {
            get { return itens; }
            set { itens = value; }
        }

        private bool produzido;

        public bool Produzido
        {
            get { return produzido; }
            set
            {
                produzido = value;
                OnPropertyChanged("Produzido");
            }
        }

        private DateTime dataAlteracao;

        public DateTime DataAlteracao
        {
            get { return dataAlteracao; }
            set { dataAlteracao = value; }
        }

        public int? Mesa
        {
            get { return mesa; }
            set { mesa = value; }
        }

        public int? TempoPreparo
        {
            get { return tempoPreparo; }
            set
            {
                if(value == null)
                {
                    tempoPreparo = 0;
                }
                else
                {
                    tempoPreparo = value;
                }
            }
        }

        public ACONTITEM Item
        {
            get { return item; }
            set { item = value; }
        }

        public string Icone
        {
            get { return icone; }
            set 
            { 
                icone = value;

                OnPropertyChanged("Icone");
            }
        }

        private int? mesa;

        private int? tempoPreparo;

        private ACONTITEM item;

        private string icone;

        public void AtualizaStatus()
        {
            if (idStatus != 3)
            {
                TimeSpan tempoDecorrido = DateTime.Now.Subtract(item.horaInclusao.Value);

                double diferenca = tempoPreparo.Value - tempoDecorrido.TotalMinutes;

                if (diferenca <= 0)
                {
                    Icone = "Img/vermelho.png";
                }

                double percentual = (diferenca * 100) / ((double)tempoPreparo.Value == 0 ? 1 : (double)tempoPreparo.Value);

                if (percentual <= 30 && percentual > 0)
                {
                    Icone = "Img/amarelo.png";
                }
                else if (percentual < 0)
                {
                    Icone = "Img/vermelho.png";
                }
            }
            else
            {
                Icone = "Img/ok.png";
            }


        }

        // Declare the event
        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
