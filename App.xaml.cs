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

        public App(INavigationService navigationService)
        {
            InitializeComponent();
            MainPage = new NavigationPage();
            _learnCategories = XmlSerializationService.DeserializeFromXml<ObservableCollection<LearnCategory>>("SaveLearnCategorys.xml");

            navigationService.NavigateToMainPage(_learnCategories);

            MainPage.Disappearing += MainPage_Disappearing;
        }
        private void MainPage_Disappearing(object? sender, EventArgs e)
        {
            XmlSerializationService.SerializeToXml("SaveLearnCategorys.xml", _learnCategories);
        }
    }
}
