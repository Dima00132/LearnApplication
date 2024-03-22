using LearnApplication.Model;
using LearnApplication.View;
using LearnApplication.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using Syncfusion.Maui.Core.Hosting;
using LearnApplication.Navigation;



namespace LearnApplication
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

            var model = new ObservableCollection<LearnCategory>();
            model.Add(new LearnCategory("c#"));

            //builder.Services.AddSingleton<IDataService, DataService>();
            builder.Services.AddTransient<MainPage>().AddTransient<MainViewModel>().AddTransient((x)=> model);
            builder.Services.AddSingleton<INavigationService,NavigationService>();


            //var model = new ObservableCollection<LearnCategory>();
            //model.Add(new LearnCategory("c#"));

            //builder.Services.AddSingleton<MainPage>().AddSingleton<MainViewModel>().AddTransient((x)=> model);

            builder.Services.AddTransient<TabbedLearnPage>();
            builder.Services.AddTransient<TabbedLearnViewModel >();


            builder.Services.AddTransient<SubjectPage>();
            builder.Services.AddTransient<SubjectViewModel>();


            builder.Services.AddTransient<QuestionsPage>();
            builder.Services.AddTransient<QuestionsViewModel>();



            builder.Services.AddTransient<SettingsPage>();
            builder.Services.AddTransient<SettingsViewModel>();

            builder.Services.AddTransient<AddQuestionPage>();
            builder.Services.AddTransient<AddQuestionViewModel>();

            builder.Services.AddTransient<RepetitionOfMaterialPage>();
            builder.Services.AddTransient<RepetitionOfMaterialViewModel>();



            





#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
