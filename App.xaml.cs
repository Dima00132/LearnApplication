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
        private readonly ObservableCollection<Category> _learnCategories;


        public App(INavigationService navigationService)
        {

            InitializeComponent();
            var navigation = new NavigationPage();
            MainPage = navigation;

           


            

            navigationService.NavigateToMainPage(_learnCategories);
        }

        //public  void Test()
        //{

        //    var g = new List<Category>()
        //    {
        //        new Category("dsfs")
        //        {
        //            LearnQuestions = new ob<СardQuestion>()
        //            {
        //                new СardQuestion("11","11"),
        //                new СardQuestion("1sfd1","11"),
        //                new СardQuestion("1sdfs1","11"),
        //                new СardQuestion("sdf1","11"),
        //                new СardQuestion("1dfs1","11"),
        //                new СardQuestion("fsdf11","11"),
        //                new СardQuestion("sfd11","11"),
        //                new СardQuestion("1sdfsd1","11"),
        //                new СardQuestion("fdssf11","11")
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
