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
    public partial class RepetitionOfMaterialViewModel : ViewModelBase
    {   
        [ObservableProperty]
        private ReviewQuestion _reviewQuestion; 

        private INavigationService _navigationService;
        private int _id;
        private readonly LocalDbService _localDbService;

        public RepetitionOfMaterialViewModel(INavigationService navigationService,LocalDbService localDbService)
        {
            _navigationService = navigationService;
            _localDbService = localDbService;
            _reviewQuestion = new();
        }

        public RelayCommand<LearnQuestion> SettingsCommand => new((learnQuestion) => _navigationService.NavigateByPage<SettingsPage>(learnQuestion.Id));
        //private void Settings(LearnQuestion learnQuestion)
        //{

        //    _navigationService.NavigateByPage<SettingsPage>(learnQuestion);

        //}


        public RelayCommand<LearnQuestion> DontKnowCommand => new((learnQuestion) =>
        {
            if (learnQuestion is not null)
            {
                ReviewQuestion.MoveQuestionToEnd(learnQuestion);
            }
        });

        //public void DontKnow(LearnQuestion learnQuestion)
        //{
        //    ReviewQuestion.MoveQuestionToEnd(learnQuestion);

        //}





        public RelayCommand<LearnQuestion> KnowCommand => new((learnQuestion) =>
        {
            if (!ReviewQuestion.IsQuestion)
                _navigationService.NavigateBack();
            if (learnQuestion is not null)
            {
                ReviewQuestion.DeleteQuestion(learnQuestion);
                _localDbService.Update(learnQuestion);
            }
        });


        //public void Know(LearnQuestion learnQuestion)
        //{
        //    if (!ReviewQuestion.IsQuestion)
        //        _navigationService.NavigateBack();
        //    ReviewQuestion.DeleteQuestion(learnQuestion);
        //}
        private void Initializes()
        {
            ReviewQuestion = _localDbService.GetById<LearnCategory>(_id).GetReviewQuestions();
        }

        public override Task OnUpdate()
        {
            Initializes();
            return base.OnUpdate();
        }

        public override Task OnNavigatingTo(object? parameter)
        {
            if(parameter is int id)
            {
                _id = id;
                Initializes();
            }
    
            return base.OnNavigatingTo(parameter);
        }
    }
}
