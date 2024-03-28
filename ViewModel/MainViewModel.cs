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



namespace LearnApplication.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        
        [ObservableProperty]
        private ObservableCollection<LearnCategory> _categoryUnderStudys;
        public MainViewModel( INavigationService navigationService)
        {
            _categoryUnderStudys = [];
            _navigationService = navigationService;
    
        }


        public RelayCommand AddCommand => new(async() =>
        {
            var subject = await Application.Current?.MainPage?.DisplayPromptAsync("Тема", "Введите Название:", "OK", "Отмена");
            if (string.IsNullOrEmpty(subject))
                return;
            CategoryUnderStudys.Add(new LearnCategory(subject));
        });

        //public RelayCommand AddCommand => new RelayCommand(Add);
        //async private void Add()
        //{
        //    var subject = await Application.Current?.MainPage?.DisplayPromptAsync("Тема", "Введите Название:", "OK", "Отмена");
        //    if (string.IsNullOrEmpty(subject))
        //        return;
        //    CategoryUnderStudys.Add(new LearnCategory(subject));
        //}

        public RelayCommand<LearnCategory> DeleteCommand => new((learnCategory) =>
        {
            if (learnCategory is not null)
                CategoryUnderStudys.Remove(learnCategory);
        });

        //[RelayCommand]
        //void Delete(LearnCategory learnCategory)
        //{
        //    CategoryUnderStudys.Remove(learnCategory);
        //}

        public RelayCommand<LearnCategory> TapCommand => new(async (learnCategory) => await _navigationService.NavigateByViewModel<TabbedLearnViewModel>(learnCategory));

        //[RelayCommand]
        //async Task Tap(LearnCategory learnCategory)
        //{

        //    await _navigationService.NavigateByViewModel<TabbedLearnViewModel>(learnCategory);

        //   // await _navigationService.NavigateByPage<TabbedLearnPage>(learnCategory);

        //}       
        public override Task OnNavigatingTo(object? parameter)
        {
            if(parameter is ObservableCollection<LearnCategory> categor)
                CategoryUnderStudys = categor;
            return base.OnNavigatingTo(parameter);
        }

    }
}
