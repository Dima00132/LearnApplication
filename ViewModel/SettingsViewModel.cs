using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnApplication.Extensions;
using LearnApplication.Model.Settings;
using LearnApplication.Navigation;
using LearnApplication.Service.Interface;
using LearnApplication.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnApplication.ViewModel
{
    public sealed class AddressesForSavingSettings
    {
        public const string Theme = "Settings_Theme";
        public const string Animated = "Settings_Animated";
    }

    public partial class SettingsViewModel:ViewModelBase
    {
        private readonly string _unspecified = "Тема системы";
        private readonly string _light = "Светлая";
        private readonly string _dark = "Темная";
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;

        [ObservableProperty]
        private string _theme;

       
        private bool _isAnimated;
        public bool IsAnimated
        {
            get => _isAnimated;
            set
            {
                if (_navigationService.IsAnimated != value)
                {
                    SettingsApplication.SetNavigationAnimated(_navigationService, value);
                    _dataService?.Save(AddressesForSavingSettings.Animated, value);
                }
                SetProperty(ref _isAnimated, value);
            }
        }


        public SettingsViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
            Theme =  SettingsApplication.GetApplicationTheme();
            IsAnimated = navigationService.IsAnimated;
        }

        public RelayCommand ApplicationThemeCommand => new (async () =>
        {
            var theme = await Application.Current?.MainPage?.DisplayActionSheet("Выберите тему", null, null, _unspecified, _light, _dark);
            if (string.IsNullOrEmpty(theme))
                return;

            SettingsApplication.SetApplicationTheme(theme);
            Theme = theme;
            _dataService?.Save(AddressesForSavingSettings.Theme, theme); 
        });

    }
}
