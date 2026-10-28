using UltimaMilla.Repartidor.Views;
namespace UltimaMilla.Repartidor
{
    public partial class AppShell : Shell
    {
        public AppShell(JornadaPage jornadaPage)
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ParadaDetallePage), typeof(ParadaDetallePage));
            JornadaShellContent.Content = jornadaPage;
        }
    }
}
