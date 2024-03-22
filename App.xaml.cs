using LearnApplication.Navigation;
using LearnApplication.View;

namespace LearnApplication
{
    public partial class App : Application
    {
        

        public App(INavigationService navigationService)
        {
            InitializeComponent();


            InitializeComponent();
            MainPage = new NavigationPage();
            navigationService.NavigateToMainPage();
        }

    }
}
