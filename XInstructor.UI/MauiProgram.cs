using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using XInstructor.UI.ViewModels;
using XInstructor.UI.Views;

namespace XInstructor.UI
{
    public static class MauiProgram
    {
        public static MauiAppBuilder RegisterService(this MauiAppBuilder app) => app;

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder app) 
        {
            app.Services.AddSingleton<HomeViewModel>();
            app.Services.AddSingleton<WeatherViewModel>();
            app.Services.AddSingleton<FailureViewModel>();
            return app;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder app)
        {
            app.Services.AddTransient<HomePage>();
            app.Services.AddTransient<WeatherPage>();
            app.Services.AddTransient<FailurePage>();
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

            return builder.Build();
        }
    }
}
