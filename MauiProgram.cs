using LearnApplication.Model;
using LearnApplication.View;
using LearnApplication.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using Syncfusion.Maui.Core.Hosting;
using LearnApplication.Navigation;
using LearnApplication.Service;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;





namespace LearnApplication
{

    public static class ServicesExtensions
    {
        public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<LocalDbService>();

            builder.Services.AddSingleton<MainPage>().AddSingleton<MainViewModel>();

            builder.Services.AddSingleton<INavigationService, NavigationService>();

            builder.Services.AddTransient<TabbedLearnPage>().AddTransient<TabbedLearnViewModel>();

            builder.Services.AddTransient<SubjectPage>().AddTransient<SubjectViewModel>();

            builder.Services.AddTransient<QuestionsPage>().AddTransient<QuestionsViewModel>();

            builder.Services.AddTransient<SettingsPage>().AddTransient<SettingsViewModel>();

            builder.Services.AddTransient<AddQuestionPage>().AddTransient<AddQuestionViewModel>();

            builder.Services.AddTransient<RepetitionOfEverythingPage>().AddTransient<RepetitionOfEverythingViewModel>();
            builder.Services.AddTransient<RepetitionOfUnknownsPage>().AddTransient<RepetitionOfUnknownsViewModel>();

            builder.Services.AddTransient<TabbedRepetitionPage>().AddTransient<TabbedRepetitionViewModel>();
            return builder;
        }
    }


    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
            .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureServices()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


            //builder.Services.AddSingleton<LocalDbService>();

            //builder.Services.AddSingleton<MainPage>().AddSingleton<MainViewModel>();

            //builder.Services.AddSingleton<INavigationService,NavigationService>();

            //builder.Services.AddTransient<TabbedLearnPage>().AddTransient<TabbedLearnViewModel >();

            //builder.Services.AddTransient<SubjectPage>().AddTransient<SubjectViewModel>();

            //builder.Services.AddTransient<QuestionsPage>().AddTransient<QuestionsViewModel>();

            //builder.Services.AddTransient<SettingsPage>().AddTransient<SettingsViewModel>();

            //builder.Services.AddTransient<AddQuestionPage>().AddTransient<AddQuestionViewModel>();

            //builder.Services.AddTransient<RepetitionOfEverythingPage>().AddTransient<RepetitionOfEverythingViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
