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
        [ObservableProperty]
        private ObservableCollection<LearnCategory> _categoryUnderStudys;

        public override Task OnNavigatingTo(object? parameter)
        {
            CategoryUnderStudys = parameter as ObservableCollection<LearnCategory>;

            return base.OnNavigatingTo(parameter);
        }


        private readonly INavigationService _navigationService;
        public MainViewModel( INavigationService navigationService)
        {
            //_categoryUnderStudys = categoryUnderStudys;
            _navigationService = navigationService;
             
        }

        public RelayCommand AddCommand => new RelayCommand(Add);
        async private void Add()
        {
            var subject = await App.Current?.MainPage?.DisplayPromptAsync("Тема", "Введите Название:", "OK", "Отмена");
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



            await _navigationService.NavigateByPage<TabbedLearnPage>(learnCategory);

        }
    }
}
