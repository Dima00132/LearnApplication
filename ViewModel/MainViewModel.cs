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
           // CategoryUnderStudys = _learn.GetSortedCategoriesByViewingTime();
        }
        public override Task OnUpdateDbService()
        {
            //CategoryUnderStudys = _learn.GetSortedCategoriesByViewingTime(false);
            _localDbService.Update(_learn);
            return base.OnUpdateDbService();
        }

        public override Task OnUpdate()
        {
            CategoryUnderStudys = _learn.GetSortedCategoriesByViewingTime(_isStart);
            _isStart = false;
            return base.OnUpdate();
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

        public RelayCommand<Category> DeleteCommand => new((category) =>
        {
            if (category is not null)
            {
                _learn.Delete(category);
                _localDbService.Delete(category);
            }
        });

        //[RelayCommand]
        //void Delete(Category learnCategory)
        //{
        //    CategoryUnderStudys.Remove(learnCategory);
        //}

        public RelayCommand<Category> TapCommand=> new(async (category)
            =>
              {
                 category.LastEntrance = DateTime.Now;
                 await _navigationService.NavigateByViewModel<TabbedLearnViewModel>(category);
              });
    }
}
