using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UltimaMilla.Repartidor.Models;
using UltimaMilla.Repartidor.Services;

namespace UltimaMilla.Repartidor.ViewModels;
[QueryProperty(nameof(ParadaId), "ParadaId")]
public partial class ParadaDetalleViewModel : ObservableObject
{
    private readonly ParadasDatabase _paradasDatabase;
    [ObservableProperty] private int _paradaId;
    [ObservableProperty] private Parada? _parada;
    [ObservableProperty] private string _razonFallo = string.Empty;
    [ObservableProperty] private string _mensajeEstado = string.Empty;
    [ObservableProperty] private bool _isGuardando;
    //Opcionales PROPIOS de 6.1. Puede que choquen adelante. CUIDADO
    public bool EsPendiente => Parada?.Estado == "Pendiente";
    public bool EsFallida => Parada?.Estado == "Fallida";
    //----
    public ParadaDetalleViewModel(ParadasDatabase paradasDatabase)
    {
        _paradasDatabase = paradasDatabase;
    }

    // Los hooks On<Propiedad>Changed no pueden ser async — por eso 
    // disparamos la carga real como una tarea aparte ("fire and forget").
    partial void OnParadaIdChanged(int value)
    {
        _ = CargarAsync();
    }

    private async Task CargarAsync()
    {
        Parada = await _paradasDatabase.GetParadaAsync(new Parada { Id = ParadaId});
    }

    [RelayCommand]
    private async Task MarcarFallidaAsync()
    {
        if (Parada is null)
            return;

        if (string.IsNullOrWhiteSpace(RazonFallo))
        {
            MensajeEstado = "Escribe una razón antes de marcar el fallo.";
            return;
        }

        if (IsGuardando)
            return;

        IsGuardando = true;

        try
        {
            Parada.Estado = "Fallida";
            Parada.RazonFallo = RazonFallo;
            Parada.ModificadoEn = DateTime.Now;
            await _paradasDatabase.SaveParadaAsync(Parada);
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            MensajeEstado = $"Error al guardar:{ex.Message}";
        }
        finally
        {
            IsGuardando = false;
        }
    }

    [RelayCommand]
    private async Task MarcarEntregadaAsync()
    {
        if (Parada is null || Parada.Estado == "Entregada")
            return;

        if (Parada.Estado == "Fallida")
        {
            return;
        }

        //Se muestra primero la confirmacion porque para que cargarlo si aun esta decidiendo
        bool confirmar = await Shell.Current.DisplayAlert("Confirmar", "¿Marcar como entregada?", "Sí", "No");

        if (!confirmar)
            return;

        if (IsGuardando)
            return;

        IsGuardando = true;

        try
        {
            Parada.Estado = "Entregada";
            Parada.ModificadoEn = DateTime.Now;

            await _paradasDatabase.SaveParadaAsync(Parada);
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            MensajeEstado = $"Error al guardar: {ex.Message}";
        }
        finally
        {
            IsGuardando = false;
        }
    }

        [RelayCommand]
    private async Task ReintentarEntregaAsync()
    {
        if (Parada is null)
            return;

        if (Parada.Estado == "Entregada")
            return;

        if(IsGuardando)
            return;

        IsGuardando = true;

        try
        {
            Parada.Estado = "Pendiente";
            Parada.RazonFallo = string.Empty;
            Parada.ModificadoEn = DateTime.Now;
            await _paradasDatabase.SaveParadaAsync(Parada);
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            MensajeEstado = $"Error al guardar: {ex.Message}";
        }
        finally
        {
            IsGuardando = false;
        }
        {

        }
        
    }
}

