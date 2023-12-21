using Microsoft.Extensions.Logging;
using SubmissionTask.ClassLibrary.Interfaces;
using SubmissionTask.ClassLibrary.Services;
using SubmissionTask.Repositories;
using SubmissionTaskMaui.ViewModels;
using SubmissionTaskMaui.Views;

namespace SubmissionTaskMaui
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


            // Initierar services för dependency injection
            builder.Services.AddSingleton<IContactRepository, ContactRepository>();
            builder.Services.AddSingleton<IFileService, FileService>();

            builder.Services.AddSingleton<AddViewModel>();
            builder.Services.AddSingleton<AddPage>();

            builder.Services.AddSingleton<EditViewModel>();
            builder.Services.AddSingleton<EditPage>();

            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();


            builder.Logging.AddDebug();


            return builder.Build();
        }
    }
}
