﻿using LearnApplication.Extensions;
using LearnApplication.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnApplication.Model.Settings
{
    public static class SettingsApplication
    {

        private readonly static Dictionary<string, string> _nameTheme = new()
        {
            {"Тема системы","Unspecified"},{"Светлая","Light"},{"Темная","Dark"}
        };

        public static void SetApplicationTheme(string theme)
        {
            if (string.IsNullOrEmpty(theme))
            {
                Application.Current.UserAppTheme = AppTheme.Unspecified;
                return;
            }

            var appTheme = _nameTheme[theme].ToEnum<AppTheme>();
            Application.Current.UserAppTheme = appTheme;
        }

        public static string GetApplicationTheme()
        {
            var currentTheme = Application.Current.UserAppTheme.ToString();
            var theme = AppTheme.Unspecified.ToString();
            if (_nameTheme.ContainsValue(currentTheme))
                return _nameTheme.Where(x => x.Value == currentTheme).FirstOrDefault().Key;
            return theme;
        }


        public static void SetNavigationAnimated(INavigationService navigationService,bool animated)
        {
            navigationService.IsAnimated = animated;
        }  
    }
}