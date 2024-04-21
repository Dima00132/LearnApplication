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
using LearnApplication.Service.Interface;



namespace LearnApplication.ViewModel
{
    public sealed partial class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ILocalDbService _localDbService;

        private readonly Learn _learn;

        [ObservableProperty]
        private ObservableCollection<Category> _categoryUnderStudys;

        private bool _isStart = true;

        public MainViewModel(INavigationService navigationService, ILocalDbService localDbService)
        {
            _navigationService = navigationService;
            _localDbService = localDbService;
            _learn = localDbService.GetLearn();
        }


        public RelayCommand AddCommand => new(async () =>
        {
            var subject = await Application.Current?.MainPage?.DisplayPromptAsync("Тема", "Введите Название:", "OK", "Отмена");
            if (string.IsNullOrEmpty(subject))
                return;
            var learnCategory = new Category(subject);
            _learn.AddCategorie(learnCategory);
            _localDbService.Create(learnCategory);
        });

        public RelayCommand SettingsCommand => new(async () =>
        {
            await _navigationService.NavigateByViewModel<SettingsViewModel>();
        });

        public RelayCommand<Category> DeleteCommand => new((category) =>
        {
            if (category is not null)
            {
                _learn.Delete(category);
                _localDbService.Delete(category);
            }
        });

        public RelayCommand<Category> TapCommand=> new(async (category)=>
        {
            category.LastEntrance = DateTime.Now;
            await _navigationService.NavigateByViewModel<TabbedLearnViewModel>(category);
        });

        public override Task OnUpdateDbService()
        {
            _localDbService.Update(_learn);
            return base.OnUpdateDbService();
        }

        public override Task OnUpdate()
        {
            CategoryUnderStudys = _learn.GetSortedCategoriesByViewingTime(_isStart);
            _isStart = false;
            return base.OnUpdate();
        }
    }
}
