using UltimaMilla.Repartidor.Views;
namespace UltimaMilla.Repartidor
{
    public partial class AppShell : Shell
    {
        public AppShell(JornadaPage jornadaPage)
        {
            InitializeComponent();

            JornadaShellContent.Content = jornadaPage;
        }
    }
}
