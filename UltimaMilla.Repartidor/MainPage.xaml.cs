namespace UltimaMilla.Repartidor
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private void OnColorButtonClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (this.BackgroundColor == Colors.Black)
            {
                TituloTxt.TextColor = Colors.Black;
                DescripcionTxt.TextColor = Colors.Black ;
                DescripcionTxt.TextColor = Colors.Black;
                this.BackgroundColor = Colors.White;
                button.Text = "Modo Oscuro";
            }
            else
            {
                TituloTxt.TextColor = Colors.White;
                DescripcionTxt.TextColor = Colors.White;
                this.BackgroundColor = Colors.Black;
                
                button.Text = "Modo Claro";
            }
        }
    }
}
