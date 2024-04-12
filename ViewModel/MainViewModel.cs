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

        private readonly Learn _learn;

        [ObservableProperty]
        private ObservableCollection<Category> _categoryUnderStudys;
        public MainViewModel(INavigationService navigationService, LocalDbService localDbService)
        {
            _categoryUnderStudys = [];
            _navigationService = navigationService;
            _localDbService = localDbService;
            var t = localDbService.GetLearn();
            _learn = t; ;
            _learn.Start();
            CategoryUnderStudys = _learn.LearnCategories;
        }
        public override Task OnSaveDb()
        {
            _localDbService.Update(_learn);
            return base.OnSaveDb();
        }


        public RelayCommand AddCommand => new(async () =>
        {
            var subject = await Application.Current?.MainPage?.DisplayPromptAsync("Тема", "Введите Название:", "OK", "Отмена");
            if (string.IsNullOrEmpty(subject))
                return;

            var learnCategory = new Category(subject);
            _learn.AddCategorie(learnCategory);
           // CategoryUnderStudys.Add(learnCategory);
            _localDbService.Create(learnCategory);
            //_localDbService.Update(_learn);
        });

        public RelayCommand DeleteFileDataCommand => new(async () =>
        {
            var subject = await Application.Current?.MainPage?.DisplayAlert("Предупреждение", "Удалить файл данных ?", "Да", "Нет");
            if (!subject)
                return;
            _localDbService.DeleteFileData();
            //CategoryUnderStudys = new ObservableCollection<Category>(_localDbService.GetLearn());
        });

        //public RelayCommand AddCommand => new RelayCommand(Add);
        //async private void Add()
        //{
        //    var subject = await Application.Current?.MainPage?.DisplayPromptAsync("Тема", "Введите Название:", "OK", "Отмена");
        //    if (string.IsNullOrEmpty(subject))
        //        return;
        //    CategoryUnderStudys.Add(new Category(subject));
        //}

        public RelayCommand<Category> DeleteCommand => new((learnCategory) =>
        {
            if (learnCategory is not null)
            {
                _learn.Delete(learnCategory);
                //CategoryUnderStudys.Remove(learnCategory);
                _localDbService.Delete(learnCategory);
            }
        });

        //[RelayCommand]
        //void Delete(Category learnCategory)
        //{
        //    CategoryUnderStudys.Remove(learnCategory);
        //}

        public RelayCommand<Category> TapCommand
            => new(async (learnCategory)
                =>
                   {
                       
                        await _navigationService.NavigateByViewModel<TabbedLearnViewModel>(learnCategory);
                    });

        //[RelayCommand]
        //async Task Tap(Category learnCategory)
        //{

        //    await _navigationService.NavigateByViewModel<TabbedLearnViewModel>(learnCategory);

        //   // await _navigationService.NavigateByPage<TabbedLearnPage>(learnCategory);

        //}       
        //public override Task OnNavigatingTo(object? parameter)
        //{
        //    if(parameter is ObservableCollection<Category> categor)
        //        CategoryUnderStudys = categor;
        //    return base.OnNavigatingTo(parameter);
        //}

    }
}
