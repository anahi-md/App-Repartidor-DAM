using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UltimaMilla.Repartidor.Models;

namespace UltimaMilla.Repartidor.ViewModels;

public partial class JornadaViewModel : ObservableObject
{
    [ObservableProperty] private string _saludo = string.Empty;

    [ObservableProperty] private int _totalEntregas = 0;

    [ObservableProperty] private int _entregasCompletadas = 0;

    [ObservableProperty] private string _mensajeEstado = string.Empty;
    public ObservableCollection<Parada> Paradas { get; set; } = new();
    public JornadaViewModel()
    {
        FormarSaludo();
        CargarParadas();
    }
    [RelayCommand]

    private async Task RefrescaParadas()
    {
        await Task.Delay(1000); // Simula una operación de actualización
        CargarParadas(); // Recarga las paradas de ejemplo
        MensajeEstado = "¡Datos actualizados correctamente!";
    
        await Task.Delay(3000); // Espera 3 segundos antes de limpiar el mensaje
        MensajeEstado = string.Empty; // Limpia el mensaje de estado
    }

    private void CargarParadas()
    {
        Paradas.Clear();
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

        Paradas.Add(new Parada
        {
            Numero = 5,
            Cliente = "Eulalio de la Cruz",
            Direccion = "Colonia 888, Jalisco",
            Estado = "Entregada"
        });

        TotalEntregas = Paradas.Count;
        EntregasCompletadas = Paradas.Count(p => p.Estado == "Entregada");

    }

    private void FormarSaludo()
    {
        string repartidor = "Miguelito Contreras";
        string saludo = string.Empty;

        if (DateTime.Now.Hour < 12)
            saludo = "Buenos días ";
        else if (DateTime.Now.Hour < 20)
            saludo = "Buenas tardes ";
        else
            saludo = "Buenas noches ";
        Saludo = saludo + repartidor;
    }
}
