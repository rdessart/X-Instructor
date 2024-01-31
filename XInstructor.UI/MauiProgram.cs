using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using XInstructor.Common.Services;
using XInstructor.Common.ViewModels;
using XInstructor.UI.ViewModels;
using XInstructor.UI.Views;

namespace XInstructor.UI
{
    public static class MauiProgram
    {
        public static MauiAppBuilder RegisterService(this MauiAppBuilder app) 
        {
            app.Services.AddSingleton<BeaconLocatorService>();
            app.Services.AddSingleton<UDPSimulatorService>();
            app.Services.AddSingleton<ClientManagerService>();
            return app;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder app) 
        {
            app.Services.AddSingleton<HomeViewModel>();
            app.Services.AddSingleton<WeatherViewModelMaui>();
            app.Services.AddSingleton<FailureViewModel>();
            app.Services.AddSingleton<RequestViewModel>();
            return app;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder app)
        {
            app.Services.AddTransient<HomePage>();
            app.Services.AddTransient<WeatherPage>();
            app.Services.AddTransient<FailurePage>();
            app.Services.AddTransient<RequestPage>();
            return app;
        }

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .RegisterService()
                .RegisterViewModels()
                .RegisterViews()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            var app = builder.Build();
            var udp = app.Services.GetRequiredService<UDPSimulatorService>();
            if(!udp.Initalize())
            {
                Debugger.Break();
            }
            else
            {
                udp.Start();
            }
            return app;
        }
    }
}
