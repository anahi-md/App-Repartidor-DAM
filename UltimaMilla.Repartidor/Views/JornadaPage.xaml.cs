using System.Collections.ObjectModel;
using UltimaMilla.Repartidor.Models;

namespace UltimaMilla.Repartidor.Views;

public partial class JornadaPage : ContentPage
{
	public ObservableCollection<Parada> Paradas { get; set; } = new();
    public JornadaPage()
	{
		InitializeComponent();
        MostrarDatosDeHoy();
        ParadasView.ItemsSource = Paradas;
    }

    private async void OnUpdateClicked(object sender, EventArgs e)
    {
        await Task.Delay(1000); // Simula una operación de actualización
        CargarParadasDeEjemplo(); // Recarga las paradas de ejemplo
        ParadasView.ItemsSource = Paradas;
        await DisplayAlertAsync("Actualización", "Se han actualizado las paradas.", "Aceptar");
    }

    private void MostrarDatosDeHoy()
	{
		string repartidor = "Miguelito Contreras";
		string saludo = string.Empty;
		int entregasDeHoy = 8;
		int entregasCompletadas = 5;

		FechaLabel.Text = DateTime.Now.ToString("dddd d 'de' MMMM");

		if (DateTime.Now.Hour < 12)
			saludo = "Buenos días ";
		else if (DateTime.Now.Hour < 20)
			saludo = "Buenas tardes ";
		else
			saludo = "Buenas noches ";
		SaludoLabel.Text = saludo + repartidor;

		TotalLabel.Text = entregasDeHoy.ToString();
		ComplLabel.Text = entregasCompletadas.ToString();
	}

	private async void OnParadaTapped(object sender, TappedEventArgs e)
    {
        if (sender is Border border && border.BindingContext is Parada parada)
        {
            await DisplayAlertAsync(
                "Detalle de la Parada", parada.Cliente, "Aceptar");
        }
    }

    private void CargarParadasDeEjemplo()
	{
		Paradas.Add(new Parada
		{
			Numero = 1,
			Cliente = "Farmacia San Juan",
			Direccion = "Av. Hidalgo 123",
			Estado = "Pendiente"
		});
        Paradas.Add(new Parada
        {
            Numero = 2,
            Cliente = "Abarrotes La Esquina",
            Direccion = "Calle Morelos 45",
            Estado = "Entregada"
        });
        Paradas.Add(new Parada
        {
            Numero = 3,
            Cliente = "Consultorio Dra. Ruiz",
            Direccion = "Blvd. Constitucion 890",
            Estado = "Pendiente"
        });
        Paradas.Add(new Parada
        {
            Numero = 4,
            Cliente = "Don Panchito Perez",
            Direccion = "Ciudada Universitaria, Sistemas 890",
            Estado = "Pendiente"
        });
    }
}