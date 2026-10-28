using UltimaMilla.Repartidor.Views;
using UltimaMilla.Repartidor.ViewModels;
using Microsoft.Extensions.Logging;
using UltimaMilla.Repartidor.Services;

namespace UltimaMilla.Repartidor
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddTransient<JornadaPage>();
            builder.Services.AddTransient<JornadaViewModel>();
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddSingleton<SesionActual>();
            builder.Services.AddSingleton<ParadasDatabase>();
            builder.Services.AddTransient<ParadaDetallePage>();
            builder.Services.AddTransient<ParadaDetalleViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
