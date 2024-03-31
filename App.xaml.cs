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
        private readonly string Path = string.Empty;

        public App(INavigationService navigationService, IDataService dataService)
        {

            InitializeComponent();
            var navigation = new NavigationPage();
            MainPage = navigation;
            DataService = dataService;


            var nameFile = "SaveLearnCategorys.xml";
            //Path = $"D:\\{nameFile}";
            Path = nameFile;

            var g = new ObservableCollection<LearnCategory>()
            {
                new LearnCategory("dsfs")
                {
                    LearnQuestions = new ObservableCollection<LearnQuestion>()
                    {
                        new LearnQuestion("11","11")
                    }
                }
            };
            dataService.Save("SaveLearnCategorys", g);

            var t = dataService.Get<ObservableCollection<LearnCategory>>("SaveLearnCategorys", null);

            _learnCategories = XmlSerializationService.DeserializeFromXml<ObservableCollection<LearnCategory>>(Path);

            navigationService.NavigateToMainPage(_learnCategories);

           
        }

        public IDataService DataService { get; }

        private void MainPage_Disappearing(object? sender, EventArgs e)
        {
            XmlSerializationService.SerializeToXml(Path, _learnCategories);
        }

        private void Navigation_Disappearing(object? sender, EventArgs e)
        {
            XmlSerializationService.SerializeToXml(Path, _learnCategories);
        }

    }
}
