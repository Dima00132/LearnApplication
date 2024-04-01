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
using LearnApplication.Service;



namespace LearnApplication.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly LocalDbService _localDbService;

        [ObservableProperty]
        private ObservableCollection<LearnCategory> _categoryUnderStudys;
        public MainViewModel( INavigationService navigationService, LocalDbService localDbService)
        {
            _categoryUnderStudys = [];
            _navigationService = navigationService;
            _localDbService = localDbService;

           CategoryUnderStudys = new ObservableCollection<LearnCategory>(localDbService.GetLearn());

        }


        public RelayCommand AddCommand => new(async() =>
        {
            var subject = await Application.Current?.MainPage?.DisplayPromptAsync("Тема", "Введите Название:", "OK", "Отмена");
            if (string.IsNullOrEmpty(subject))
                return;

            var learnCategory = new LearnCategory(subject);
            CategoryUnderStudys.Add(learnCategory);
            _localDbService.Create(learnCategory);
        });

        public RelayCommand DeleteFileDataCommand => new(async () =>
        {
            var subject = await Application.Current?.MainPage?.DisplayAlert("Предупреждение","Удалить файл данных ?", "Да", "Нет");
            if (!subject)
                return;
            _localDbService.DeleteFileData();
            CategoryUnderStudys = new ObservableCollection<LearnCategory>(_localDbService.GetLearn());
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
            {
                CategoryUnderStudys.Remove(learnCategory);
                _localDbService.Delete(learnCategory);
            }
        });

        //[RelayCommand]
        //void Delete(LearnCategory learnCategory)
        //{
        //    CategoryUnderStudys.Remove(learnCategory);
        //}

        public RelayCommand<LearnCategory> TapCommand
            => new(async (learnCategory) 
                => await _navigationService.NavigateByViewModel<TabbedLearnViewModel>(learnCategory.Id));

        //[RelayCommand]
        //async Task Tap(LearnCategory learnCategory)
        //{

        //    await _navigationService.NavigateByViewModel<TabbedLearnViewModel>(learnCategory);

        //   // await _navigationService.NavigateByPage<TabbedLearnPage>(learnCategory);

        //}       
        //public override Task OnNavigatingTo(object? parameter)
        //{
        //    if(parameter is ObservableCollection<LearnCategory> categor)
        //        CategoryUnderStudys = categor;
        //    return base.OnNavigatingTo(parameter);
        //}

    }
}
