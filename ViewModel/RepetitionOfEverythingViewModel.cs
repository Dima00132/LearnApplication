using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnApplication.Model;
using LearnApplication.Navigation;
using LearnApplication.Service;
using LearnApplication.View;
using LearnApplication.ViewModel.Base;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Internals;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LearnApplication.ViewModel
{
    public partial class RepetitionOfEverythingViewModel : ViewModelBase
    {   
        [ObservableProperty]
        private ReviewQuestion _reviewQuestion; 

        private INavigationService _navigationService;
        private Category _learnCategory;
        private readonly LocalDbService _localDbService;

        public RepetitionOfEverythingViewModel(INavigationService navigationService,LocalDbService localDbService)
        {
            _navigationService = navigationService;
           _localDbService = localDbService;
            //_reviewQuestion = new();
        }

        public RelayCommand<СardQuestion> SettingsCommand => new((learnQuestion) => _navigationService.NavigateByPage<SettingsPage>(learnQuestion));
        //private void Settings(СardQuestion learnQuestion)
        //{

        //    _navigationService.NavigateByPage<SettingsPage>(learnQuestion);

        //}


        public RelayCommand<СardQuestion> DontKnowCommand => new((learnQuestion) =>
        {
            if (learnQuestion is not null)
            {
                ReviewQuestion.MoveQuestionToEnd(learnQuestion);
            }
        });

        //public void DontKnow(СardQuestion learnQuestion)
        //{
        //    ReviewQuestion.MoveQuestionToEnd(learnQuestion);

        //}

        public override Task OnSaveDb()
        {
            _localDbService.Update(_learnCategory);
            return base.OnSaveDb();
        }

        public RelayCommand<СardQuestion> KnowCommand => new((learnQuestion) =>
        {
            if (!ReviewQuestion.IsQuestion)
                _navigationService.NavigateBack();
            if (learnQuestion is not null)
            {
               ReviewQuestion.DeleteQuestion(learnQuestion);
               
               //_localDbService.Update(_learnCategory);
            }
        });


        //public void Know(СardQuestion learnQuestion)
        //{
        //    if (!ReviewQuestion.IsQuestion)
        //        _navigationService.NavigateBack();
        //    ReviewQuestion.DeleteQuestion(learnQuestion);
        //}
        private void Initializes(СardQuestion learnQuestion)
        {
            
           // ReviewQuestion = _localDbService.GetById<Category>(_id).GetReviewQuestions();
        }

        public override Task OnUpdate()
        {
            //Initializes();
            return base.OnUpdate();
        }

        public override Task OnNavigatingTo(object? parameter)
        {
            if(parameter is Category learnCategory)
            {
                _learnCategory = learnCategory;
                ReviewQuestion = learnCategory.GetReviewQuestions();
               //Initializes();
            }
    
            return base.OnNavigatingTo(parameter);
        }
    }
}
