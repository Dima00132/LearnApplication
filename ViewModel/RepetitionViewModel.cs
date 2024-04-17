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
    public sealed partial class RepetitionViewModel : ViewModelBase
    {   
        [ObservableProperty]
        private ReviewQuestion _reviewQuestion; 

        private INavigationService _navigationService;
        private Category _learnCategory;
        private readonly ILocalDbService _localDbService;

        public RepetitionViewModel(INavigationService navigationService, ILocalDbService localDbService)
        {
            _navigationService = navigationService;
           _localDbService = localDbService;
        }

        public RelayCommand<СardQuestion> SettingsCommand => new((learnQuestion) => _navigationService.NavigateByPage<SettingsPage>(learnQuestion));


        public RelayCommand<СardQuestion> DontKnowCommand => new((learnQuestion) =>
        {
            if (learnQuestion is not null)
            {
                ReviewQuestion.MoveQuestionToEnd(learnQuestion);
            }
        });

        public override Task OnUpdateDbService()
        {
            _localDbService.Update(_learnCategory);
            return base.OnUpdateDbService();
        }

        public RelayCommand<СardQuestion> KnowCommand => new((learnQuestion) =>
        {
            if (!ReviewQuestion.IsQuestions)
                _navigationService.NavigateBackUpdate();
            if (learnQuestion is not null)
            {
               ReviewQuestion.DeleteQuestion(learnQuestion);
               
               _localDbService.Update(learnQuestion);
            }
        });


        //public override Task OnUpdate()
        //{
        //    //Initializes();
        //    return base.OnUpdate();
        //}

        public override Task OnNavigatingTo(object? parameterFirst, object? parameterSecond)
        {
            if(parameterFirst is Category learnCategory)
            {
                _learnCategory = learnCategory;
                if(parameterSecond is bool allOrUnknown)
                    ReviewQuestion = learnCategory.GetReviewQuestions(allOrUnknown);
            }
            return base.OnNavigatingTo(parameterFirst, parameterSecond);
        }
    }
}
