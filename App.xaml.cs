using LearnApplication.Model;
using LearnApplication.Navigation;
using LearnApplication.Service;
using LearnApplication.View;
using System.Collections.ObjectModel;

namespace LearnApplication
{
    public partial class App : Application
    {
        private readonly ObservableCollection<LearnCategory> _learnCategories;
        private readonly string Path = string.Empty;

        public App(INavigationService navigationService)
        {
            InitializeComponent();
            MainPage = new NavigationPage();
            var nameFile = "SaveLearnCategorys.xml";
            var Path = $"D:\\{nameFile}";

            _learnCategories = XmlSerializationService.DeserializeFromXml<ObservableCollection<LearnCategory>>(Path);

            navigationService.NavigateToMainPage(_learnCategories);

            MainPage.Disappearing += MainPage_Disappearing;
        }
        private void MainPage_Disappearing(object? sender, EventArgs e)
        {
            XmlSerializationService.SerializeToXml(Path, _learnCategories);
        }
    }
}
