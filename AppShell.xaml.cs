using LearnApplication.Model;
using LearnApplication.View;
using LearnApplication.ViewModel;
using System.Collections.ObjectModel;
using Syncfusion.Maui.Core.Hosting;
using Syncfusion.Maui.NavigationDrawer;


namespace LearnApplication
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {

            //var model = new ObservableCollection<LearnCategory>();
            //model.Add(new LearnCategory("c#"));
            //BindingContext = new MainViewModel(model);
            InitializeComponent();
            Routing.RegisterRoute(nameof(SubjectPage), typeof(SubjectPage));
            Routing.RegisterRoute(nameof(QuestionsPage), typeof(QuestionsPage));
            Routing.RegisterRoute(nameof(SettingsPage),typeof(SettingsPage));
            Routing.RegisterRoute(nameof(AddQuestionPage), typeof(AddQuestionPage));
            Routing.RegisterRoute(nameof(RepetitionOfMaterialPage), typeof(RepetitionOfMaterialPage));
     
            //Routing.RegisterRoute(nameof(TabbedPage), typeof(TabbedPage));
        }


    }
}
