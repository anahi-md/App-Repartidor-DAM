using UltimaMilla.Repartidor.ViewModels;
using UltimaMilla.Repartidor.Models;

namespace UltimaMilla.Repartidor.Views;

public partial class JornadaPage : ContentPage
{
    public JornadaPage()
	{
		InitializeComponent();
        BindingContext = new JornadaViewModel();
        FormateaFecha();
    }
    private void FormateaFecha()
	{
		FechaLabel.Text = DateTime.Now.ToString("dddd d 'de' MMMM");

	}

    /* El TapGestureRecognizer va con el Grid raid del DataTemplate, no en el CollectionView
    * así cada fila tiene su propio manejador y ender te da directo el elemento con el
    * BindingContext de esa fila especifica */
    private async void OnParadaTapped(object sender, TappedEventArgs e)
    {
        if (sender is VisualElement { BindingContext: Parada parada })
        {
            await DisplayAlertAsync("Parada", $"Cliente: {parada.Cliente}", "Ok");
        }
    }
}