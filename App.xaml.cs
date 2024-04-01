using CommunityToolkit.Maui.Storage;
using LearnApplication.Model;
using LearnApplication.Navigation;
using LearnApplication.Service;
using LearnApplication.View;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace LearnApplication
{
    public partial class App : Application
    {
        private readonly ObservableCollection<LearnCategory> _learnCategories;


        public App(INavigationService navigationService)
        {

            InitializeComponent();
            var navigation = new NavigationPage();
            MainPage = navigation;

           


            

            navigationService.NavigateToMainPage(_learnCategories);
        }

        //public  void Test()
        //{

        //    var g = new List<LearnCategory>()
        //    {
        //        new LearnCategory("dsfs")
        //        {
        //            LearnQuestions = new ob<LearnQuestion>()
        //            {
        //                new LearnQuestion("11","11"),
        //                new LearnQuestion("1sfd1","11"),
        //                new LearnQuestion("1sdfs1","11"),
        //                new LearnQuestion("sdf1","11"),
        //                new LearnQuestion("1dfs1","11"),
        //                new LearnQuestion("fsdf11","11"),
        //                new LearnQuestion("sfd11","11"),
        //                new LearnQuestion("1sdfsd1","11"),
        //                new LearnQuestion("fdssf11","11")
        //            }
        //        }
        //    };
           
        //    LocalDbService.Create(g.First());

      
        //    var learn = LocalDbService.GetLearn();
        //}

        public IDataService DataService { get; }
        public LocalDbService LocalDbService { get; }
    }
}
