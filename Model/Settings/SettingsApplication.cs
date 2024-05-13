using LearnApplication.Extensions;
using LearnApplication.Navigation;
using LearnApplication.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnApplication.Model.Settings
{
    public interface ISettingsApplication
    {
        string GetApplicationTheme();
        int GetNumberOfRepetitions();
        void InstallApplicationTheme();
        void InstallNavigationAnimated();
       
        void SetApplicationTheme(string theme);
        void SetNavigationAnimated(bool animated);
        bool GetNavigationAnimated();
        void SetNumberOfRepetitions(Learn learn, int numberOfRepetitions);
    }

    public class SettingsApplication : ISettingsApplication
    {
        public const string Theme = "Settings_Theme";
        public const string Animated = "Settings_Animated";
        public const string Repetition = "Settings_Repetition";

        private readonly Dictionary<string, string> _nameTheme = new()
        {
            {"Тема системы","Unspecified"},{"Светлая","Light"},{"Темная","Dark"}
        };

        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;

        public SettingsApplication(INavigationService navigationService, IDataService dataService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
        }

        public void InstallApplicationTheme()
        {
            var theme = _dataService?.Get(Theme, _nameTheme["Тема системы"]).Result;
            var appTheme = _nameTheme[theme].ToEnum<AppTheme>();
            Application.Current.UserAppTheme = appTheme;
        }

        public void SetApplicationTheme(string theme)
        {
            if (string.IsNullOrEmpty(theme))
            {
                Application.Current.UserAppTheme = AppTheme.Unspecified;
                return;
            }

            var appTheme = _nameTheme[theme].ToEnum<AppTheme>();
            Application.Current.UserAppTheme = appTheme;

            _dataService?.Save(Theme, theme);
        }

        public string GetApplicationTheme()
        {
            var currentTheme = Application.Current.UserAppTheme.ToString();
            var theme = AppTheme.Unspecified.ToString();
            if (_nameTheme.ContainsValue(currentTheme))
                return _nameTheme.Where(x => x.Value == currentTheme).FirstOrDefault().Key;
            return theme;
        }

        
        public void InstallNavigationAnimated()
        {
            var animated = _dataService?.Get(Animated,false).Result;
            _navigationService.IsAnimated = animated.Value;
        }
        public void SetNavigationAnimated(bool animated)
        {
            _navigationService.IsAnimated = animated;
            _dataService?.Save(Animated, animated);
        }

        public bool GetNavigationAnimated()
        {
            return _navigationService.IsAnimated;
        }

        //public void InstallNumberOfRepetitions()
        //{
        //    var number = _dataService?.Get(Repetition, 4).Result;
        //    CardQuestion.CountRepetitions = number.Value;
        //}

        public int GetNumberOfRepetitions()
        {
            var number = _dataService?.Get(Repetition, 4).Result;
            return number.Value;
        }

        public void SetNumberOfRepetitions(Learn learn, int numberOfRepetitions)
        {
            foreach (var item in learn.Categories.SelectMany(x => x.LearnQuestions))
                item.ChangeNumberOfRepetitions(numberOfRepetitions);
            _dataService?.Save(Repetition, numberOfRepetitions);
            
        }
    }
}
