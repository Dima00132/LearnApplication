using CommunityToolkit.Maui.Storage;
using LearnApplication.Model;
using LearnApplication.Model.Settings;
using LearnApplication.Navigation;
using LearnApplication.Service;
using LearnApplication.Service.Interface;
using LearnApplication.View;
using LearnApplication.ViewModel;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace LearnApplication
{
    public  class InitializingApplicationSettings(INavigationService navigationService,IDataService dataService)
    {
        private readonly INavigationService _navigationService = navigationService;
        private readonly IDataService _dataService = dataService;
        public void Start()
        {
            SetsThemeSettings();
            SetsAnimatedSettings();
        }
        private void SetsThemeSettings()
        {
            var theme = _dataService.Get(AddressesForSavingSettings.Theme, string.Empty).Result;
            SettingsApplication.SetApplicationTheme(theme);
        }

        private void SetsAnimatedSettings()
        {
            var animated = _dataService.Get(AddressesForSavingSettings.Animated, false).Result;
            _navigationService.IsAnimated = animated;
        }
    }


    public partial class App : Application
    {

        private readonly InitializingApplicationSettings  _initializing;
        public App(INavigationService navigationService,IDataService dataService)
        {
            _initializing = new InitializingApplicationSettings(navigationService, dataService);
            InitializeComponent();
            MainPage = new NavigationPage();
            navigationService.NavigateToMainPage();
        }

        protected override void OnStart()
        {
            _initializing.Start();
            base.OnStart();
        }
    }
}
