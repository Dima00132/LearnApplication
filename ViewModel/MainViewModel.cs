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
        //private ObservableCollection<Subject> _categoryUnderStudys;



        public MainViewModel(INavigationService navigationService, ILocalDbService localDbService)
        {
            _navigationService = navigationService;
            _localDbService = localDbService;
            Learn = localDbService.GetLearn();

            Learn.Categories.SetUpdateDbEvent(OnEventHandlerLearn)
                .SortedCategories((x) => x.LastActivity)
                .SetSubjectObservableCollection(Learn);
            //Learn.SortedCategories((x)=> x.LastActivity);
            //Learn.SetUpdateDbEvent(OnEventHandlerLearn);
       
            //Learn.SortedCategories((x) => x.LastActivity);
        }

        private void OnEventHandlerLearn(object? obj,EventArgs eventArgs)
        {
            if(obj is CardQuestion subject)
                _localDbService.Update(subject);
           
        }

        //private void Current_PageAppearing(object? sender, Page e)
        //{
        //    Learn.SortedCategories(_isStart);
        //    _isStart = false;
        //}

        public RelayCommand AddCommand => new(async () =>
        {
            var subject = await Application.Current?.MainPage?.DisplayPromptAsync("Тема", "Введите Название:", "OK", "Отмена");
            if (string.IsNullOrEmpty(subject))
                return;
            var category = new Subject(subject) { LastActivity = DateTime.Now,UpdateDbEvent = OnEventHandlerLearn };
   
            Learn.Add(category);
            _localDbService.CreateAndUpdate(category, Learn);

            //_localDbService.Create(learnCategory);
            //_localDbService.Update(Learn);
        });

        public RelayCommand SettingsCommand => new(async () =>
        {
            await _navigationService.NavigateByViewModel<SettingsViewModel>(Learn);
        });

        public RelayCommand<Subject> DeleteCommand => new((subject) =>
        {
            if (subject is not null)
            {
                subject.UpdateDbEvent -= OnEventHandlerLearn;
                Learn.Remove(subject);

                _localDbService.DeleteAndUpdate(subject, Learn);

    

                //_localDbService.RemoveQuestion(category);
                //_localDbService.Update(Learn);
            }
        });

        public RelayCommand<Subject> TapCommand=> new(async (subject) =>
        {
            Learn.MoveToStartingPosition(subject);
            await _navigationService.NavigateByViewModel<TabbedLearnViewModel>(subject);
        });

        //public override Task OnUpdateDbService()
        //{
        //   // _localDbService.Update(_learn);
        //    return base.OnUpdateDbService();
        //}

        //public override Task OnStart()
        //{
        //    Learn.SortedCategories(_isStart);
        //    _isStart = false;
        //    return base.OnStart();
        //}
    }
}
