using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UltimaMilla.Repartidor.Models;
using UltimaMilla.Repartidor.Services;

namespace UltimaMilla.Repartidor.ViewModels;

public partial class JornadaViewModel : ObservableObject
{
    private readonly ParadasDatabase _paradasDatabase;
    [ObservableProperty] private string _saludo = string.Empty;

    [ObservableProperty] private int _totalEntregas = 0;

    [ObservableProperty] private int _entregasCompletadas = 0;

    [ObservableProperty] private string _mensajeEstado = string.Empty;
    [ObservableProperty] private Parada? _paradaSeleccionada;
    [ObservableProperty] private bool _isRefrescando = false;
    public ObservableCollection<Parada> Paradas { get; set; } = new();
    public JornadaViewModel(ParadasDatabase paradasDatabase)
    {
        _paradasDatabase = paradasDatabase;
        FormarSaludo();
    }
    [RelayCommand]

    private async Task RefrescaParadas()
    {
        try
        {
            IsRefrescando = true;
#if DEBUG
            await Task.Delay(1000); // Simula una espera de 1 segundos
#endif
            var paradas = await _paradasDatabase.GetParadasAsync();

            if (paradas.Count == 0)
            {
                MensajeEstado = "No hay paradas disponibles.";
                await Task.Delay(2000); // Espera 2 segundos antes de limpiar el mensaje
                await InventaParadas();
                paradas = await _paradasDatabase.GetParadasAsync();
            }

            Paradas.Clear();
            foreach (var parada in paradas)
                Paradas.Add(parada);

            TotalEntregas = Paradas.Count;
            EntregasCompletadas = Paradas.Count(p => p.Estado == "Entregada");
            MensajeEstado = "!Datos actualizados correctamente¡";
        }
        catch (Exception ex)
        {
            MensajeEstado = $"Error al actualizar los datos: {ex.Message}";
        }
        finally
        {
            IsRefrescando = false;

        }

        await Task.Delay(2000); // Espera 2 segundos antes de limpiar el mensaje
        MensajeEstado = string.Empty;
    }

    private async Task InventaParadas()
    {
        await _paradasDatabase.SaveParadaAsync(new Parada
        {
            Numero = 1,
            Cliente = "Farmacia San Juan",
            Direccion = "Av. Hidalgo 123",
            Estado = "Pendiente",
            RepartidorId = 1
        });
        await _paradasDatabase.SaveParadaAsync(new Parada
        {
            Numero = 2,
            Cliente = "Abarrotes La Esquina",
            Direccion = "Calle Morelos 45",
            Estado = "Entregada",
            RepartidorId = 1
        });
        await _paradasDatabase.SaveParadaAsync(new Parada
        {
            Numero = 3,
            Cliente = "Consultorio Dra. Ruiz",
            Direccion = "Blvd. Constitucion 890",
            Estado = "Pendiente",
            RepartidorId = 1
        });
        await _paradasDatabase.SaveParadaAsync(new Parada
        {
            Numero = 4,
            Cliente = "Don Panchito Perez",
            Direccion = "Ciudada Universitaria, Sistemas 890",
            Estado = "Pendiente",
            RepartidorId = 1
        });
        await _paradasDatabase.SaveParadaAsync(new Parada
        {
            Numero = 5,
            Cliente = "Eulalio de la Cruz",
            Direccion = "Colonia 888, Jalisco",
            Estado = "Entregada",
            RepartidorId = 1
        });
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
