using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnApplication.Model;
using LearnApplication.Navigation;
using LearnApplication.Service;
using LearnApplication.View;
using LearnApplication.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnApplication.ViewModel
{
    public partial class RepetitionOfUnknownsViewModel:ViewModelBase
    {
        [ObservableProperty]
        private ReviewQuestion _reviewQuestion;

        private INavigationService _navigationService;

        private readonly LocalDbService _localDbService;

        public RepetitionOfUnknownsViewModel(INavigationService navigationService, LocalDbService localDbService)
        {
            _navigationService = navigationService;
            // _localDbService = localDbService;
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


        public RelayCommand<СardQuestion> KnowCommand => new((learnQuestion) =>
        {
            if (!ReviewQuestion.IsQuestion)
                _navigationService.NavigateBack();
            if (learnQuestion is not null)
            {
                ReviewQuestion.DeleteQuestion(learnQuestion);

                // _localDbService.Update(learnQuestion);
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
            if (parameter is Category learnCategory)
            {
                //_id = id;
                ReviewQuestion = learnCategory.GetReviewQuestions();
                //Initializes();
            }

            return base.OnNavigatingTo(parameter);
        }
    }
}
