using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnApplication.Extensions;
using LearnApplication.Model;
using LearnApplication.Model.Settings;
using LearnApplication.Navigation;
using LearnApplication.Service.Interface;
using LearnApplication.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnApplication.ViewModel
{
    //public sealed class AddressesForSavingSettings
    //{
    //    public const string Theme = "Settings_Theme";
    //    public const string Animated = "Settings_Animated";
    //    public const string Repetition = "Settings_Repetition";
    //}

    public partial class SettingsViewModel:ViewModelBase
    {
        private readonly string _unspecified = "Тема системы";
        private readonly string _light = "Светлая";
        private readonly string _dark = "Темная";
     
        private readonly SettingsApplication _settingsApplication;
        [ObservableProperty]
        private string _theme;

        [ObservableProperty]
        private int _numberOfRepetitions;

        private bool _isAnimated;
        public bool IsAnimated
        {
            get => _isAnimated;
            set
            {
                _settingsApplication.SetNavigationAnimated(value);
                SetProperty(ref _isAnimated, value);
            }
        }


        public SettingsViewModel(SettingsApplication settingsApplication)
        {
            //_navigationService = navigationService;
            //_dataService = dataService;
            Theme = settingsApplication.GetApplicationTheme();
            NumberOfRepetitions = settingsApplication.GetNumberOfRepetitions();
            IsAnimated = settingsApplication.GetNavigationAnimated();
            _settingsApplication = settingsApplication;
        }
        private Learn _learm;

        public override Task OnNavigatingTo(object? parameter, object? parameterSecond = null)
        {
            if(parameter is Learn learn)
            {
                _learm = learn;
            }
            return base.OnNavigatingTo(parameter, parameterSecond);
        }
        public RelayCommand PerformSearchCommand => new( () =>
        {
            _settingsApplication.SetNumberOfRepetitions(_learm, NumberOfRepetitions);
        });

        public RelayCommand ApplicationLanguageCommand => new(async () =>
        {
            await Application.Current?.MainPage?.
            DisplayAlert("Уведомление", "На данном этапе развития приложения добавлент только Русский язык! В будущем возможно будут доступны другие языки.", "ОK");
        });

        public RelayCommand ApplicationThemeCommand => new (async () =>
        {
            var theme = await Application.Current?.MainPage?.DisplayActionSheet("Выберите тему", null, null, _unspecified, _light, _dark);
            if (string.IsNullOrEmpty(theme))
                return;
            Theme = theme;
            _settingsApplication.SetApplicationTheme(theme);
            
            //_dataService?.Save(AddressesForSavingSettings.Theme, theme); 
        });

    }
}
