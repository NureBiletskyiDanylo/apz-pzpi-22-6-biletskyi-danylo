using MediStoS_Mobile.Models;
using MediStoS_MobileClient.Models;
using MediStoS_MobileClient.Services;
using Microsoft.Extensions.Logging;

namespace MediStoS_MobileClient
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddSingleton<WarehouseService>();
            builder.Services.AddTransient<WarehouseViewModel>();
            builder.Services.AddTransient<WarehousesPage>();
            builder.Services.AddTransient<WarehouseDetailViewModel>();
            builder.Services.AddTransient<WarehouseDetailPage>();

            return builder.Build();
        }
    }
}
