namespace DividirConta
{
    public partial class MainPage : ContentPage
    {
        #region Variáveis
        private decimal conta;
        private int gorjeta;
        private int numPessoas = 1;
        #endregion

        #region Encapsulamento
        public int NumPessoas { get => numPessoas; set => numPessoas = value; }
        public int Gorjeta { get => gorjeta; set => gorjeta = value; }
        public decimal Conta { get => conta; set => conta = value; }
        #endregion

        public MainPage()
        {
            InitializeComponent();
        }

        private async void txtConta_Completed(object sender, EventArgs e)
        {
            try
            {
                Conta = decimal.Parse(txtConta.Text);
            } catch (FormatException)
            {
                await DisplayAlert("Erro", "Formato Inválido", "OK");
                ResetAllValues();
            } finally
            {
                TotalConta();
            }
        }

        private void TotalConta()
        {
            decimal totalGorjeta = (Conta * Gorjeta) / 100;

            decimal gorjetaPorPessoa = (totalGorjeta / NumPessoas);
            lblGorjeta.Text = $"{gorjetaPorPessoa:c}";

            decimal subTotal = (Conta / NumPessoas);
            lblSubTotal.Text = $"{subTotal:c}";

            decimal totalPorPessoa = (Conta + totalGorjeta) / NumPessoas;
            lblTotal.Text = $"{totalPorPessoa:c}";
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if(sender is Button)
            {
                Button btn = (Button)sender;
                int percent = int.Parse(btn.Text.Replace("%", ""));
                sldGorjeta.Value = percent;
            }
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Gorjeta = (int)e.NewValue;
            lblGorjetaPer.Text = $"Gorjeta: {Gorjeta}%";
            TotalConta();
        }

        private void Button_Clicked_Menos(object sender, EventArgs e)
        {
            if(NumPessoas > 1)
            {
                NumPessoas--;
            }
            lblPessoas.Text = NumPessoas.ToString();
            TotalConta();
        }
        private void Button_Clicked_Mais(object sender, EventArgs e)
        {
            NumPessoas++;
            lblPessoas.Text = NumPessoas.ToString();
            TotalConta();
        }

        private void Button_Clicked_Iniciar(object sender, EventArgs e)
        {
            ResetAllValues();

            TotalConta();
        }
    
        private void ResetAllValues()
        {
            Conta = 0;
            Gorjeta = 0;
            NumPessoas = 1;

            txtConta.Text = "";
            lblPessoas.Text = "1";
            sldGorjeta.Value = 0;
        }
    }
}
