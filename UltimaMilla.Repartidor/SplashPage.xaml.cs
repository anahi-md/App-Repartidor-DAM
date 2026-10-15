using Microsoft.Extensions.DependencyInjection;
namespace UltimaMilla.Repartidor;



public partial class SplashPage : ContentPage

{

    // Tiempo que el splash permanece visible, en milisegundos. 

    // Súbelo o bájalo según qué tan pesados sean tus recursos iniciales. 

    private const int SplashDurationMs = 2000;



    public SplashPage()

    {

        InitializeComponent();

    }



    protected override async void OnAppearing()

    {

        base.OnAppearing();



        await Task.Delay(SplashDurationMs);



        // Application.Current.MainPage está obsoleto desde .NET 8. 

        // El reemplazo vigente es Windows[0].Page. 

        if (Application.Current is not null && Application.Current.Windows.Count > 0)
        {
            var servicios = IPlatformApplication.Current?.Services;

            if (servicios is not null)
            {
                var appShell = servicios.GetRequiredService<AppShell>();
                Application.Current.Windows[0].Page = appShell;
            }
        }

    }
}