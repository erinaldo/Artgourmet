namespace Artebit.Restaurante.Caixa.ModelView
{
    public class ItemConta
    {
        public int nuItem { get; set; }
        public string codigo { get; set; }
        public string nome { get; set; }
        public string unidade { get; set; }
        public decimal quantidade { get; set; }
        public decimal? preco { get; set; }
        public decimal? desconto { get; set; }
        public string status { get; set; }
        public decimal? total { get; set; }
        public string vendedor { get; set; }
    }
}