﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnApplication.Model;
using LearnApplication.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnApplication;
using LearnApplication.Navigation;
using LearnApplication.ViewModel.Base;



namespace LearnApplication.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<LearnCategory> _categoryUnderStudys;

        private bool _subject;

        private readonly INavigationService _navigationService;
        public MainViewModel(ObservableCollection<LearnCategory> categoryUnderStudys, INavigationService navigationService)
        {
            _categoryUnderStudys = categoryUnderStudys;
            _navigationService = navigationService;
             
        }

        public RelayCommand AddCommand => new RelayCommand(Add);
        async private void Add()
        {

            //await Shell.Current.GoToAsync($"{nameof(AddDishPage)}", true);
            //var _newName = await Shell.Current.DisplayPromptAsync("Логин", "Введите имя:", "OK", "Отмена");
            //var _newName = await Shell.Current.DisplayPromptAsync("Логин", "Введите имя:", "OK", "Отмена");
            //CategoryUnderStudys.AddLearnQuestion(new Dish() { Name = $"{_newName}" });

            var subject = await App.Current.MainPage.DisplayPromptAsync("Тема", "Введите Название:", "OK", "Отмена");
            if (string.IsNullOrEmpty(subject))
                return;
            CategoryUnderStudys.Add(new LearnCategory(subject));
        }

        [RelayCommand]
        void Delete(LearnCategory learnCategory)
        {
            CategoryUnderStudys.Remove(learnCategory);
        }

        [RelayCommand]
        async Task Tap(LearnCategory learnCategory)
        {



            await _navigationService.NavigateTo<TabbedLearnPage>(learnCategory);

        }
    }
}
