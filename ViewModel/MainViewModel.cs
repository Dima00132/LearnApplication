using CommunityToolkit.Mvvm.ComponentModel;
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

        [ObservableProperty]
        private  Learn _learn;
        
        //[ObservableProperty]
        //private ObservableCollection<Category> _categoryUnderStudys;

        private bool _isStart = true;

        public MainViewModel(INavigationService navigationService, ILocalDbService localDbService)
        {
            _navigationService = navigationService;
            _localDbService = localDbService;
            Learn = localDbService.GetLearn();
            Learn.SortedCategoriesByViewingTime(_isStart);
        }

        //private void Current_PageAppearing(object? sender, Page e)
        //{
        //    Learn.SortedCategoriesByViewingTime(_isStart);
        //    _isStart = false;
        //}

        public RelayCommand AddCommand => new(async () =>
        {
            var subject = await Application.Current?.MainPage?.DisplayPromptAsync("Тема", "Введите Название:", "OK", "Отмена");
            if (string.IsNullOrEmpty(subject))
                return;
            var category = new Category(subject) { LastActivity = DateTime.Now };
            Learn.AddCategorie(category);

            _localDbService.CreateAndUpdate(category, Learn);

            //_localDbService.Create(learnCategory);
            //_localDbService.Update(Learn);
        });

        public RelayCommand SettingsCommand => new(async () =>
        {
            await _navigationService.NavigateByViewModel<SettingsViewModel>(Learn);
        });

        public RelayCommand<Category> DeleteCommand => new((category) =>
        {
            if (category is not null)
            {
                Learn.Delete(category);

                _localDbService.DeleteAndUpdate(category, Learn);

    

                //_localDbService.Delete(category);
                //_localDbService.Update(Learn);
            }
        });

        public RelayCommand<Category> TapCommand=> new(async (category)=>
        {
            Learn.MoveToStartingPosition(category);
            await _navigationService.NavigateByViewModel<TabbedLearnViewModel>(category);
        });

        //public override Task OnUpdateDbService()
        //{
        //   // _localDbService.Update(_learn);
        //    return base.OnUpdateDbService();
        //}

        //public override Task OnStart()
        //{
        //    Learn.SortedCategoriesByViewingTime(_isStart);
        //    _isStart = false;
        //    return base.OnStart();
        //}
    }
}
