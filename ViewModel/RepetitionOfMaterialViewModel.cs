using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnApplication.Model;
using LearnApplication.Navigation;
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
        private LearnCategory _learnCategory;

        private ReviewQuestion _questionsToReview;

        //[ObservableProperty]
        //private string _question;

        //[ObservableProperty]
        //private string _answer;

        //[ObservableProperty]
        //private string _hyperlink;

        private INavigationService _navigationService;

        public RepetitionOfMaterialViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

        }

        public RelayCommand<LearnQuestion> SettingsCommand => new RelayCommand<LearnQuestion>(Settings);
        private void Settings(LearnQuestion learnQuestion)
        {


            _navigationService.NavigateTo<SettingsPage>(learnQuestion);
    
        }


        public RelayCommand<LearnQuestion> DontKnowCommand { get => new RelayCommand<LearnQuestion>((x) => DontKnow(x)); }

        public void DontKnow(LearnQuestion learnQuestion)
        {
            ReviewQuestion.MoveQuestionToEnd(learnQuestion);
           
        }





        public RelayCommand<LearnQuestion> KnowCommand { get => new  RelayCommand<LearnQuestion>((x)=> Know(x)); }


        public void Know(LearnQuestion learnQuestion)
        {
            if (!ReviewQuestion.IsQuestion)
                _navigationService.NavigateBack();
            ReviewQuestion.DeleteQuestion(learnQuestion);
        }
        


        [ObservableProperty]
        private ReviewQuestion _reviewQuestion;

        //private ObservableCollection<LearnQuestion> _learnQuestions = new();

        public override Task OnNavigatingTo(object? parameter)
        {
            ReviewQuestion = parameter as ReviewQuestion;
            return base.OnNavigatingTo(parameter);
        }
        //public void ApplyQueryAttributes(IDictionary<string, object> query)
        //{
        //    _learnCategory = (LearnCategory)query[nameof(LearnCategory)];
        //    var create = (bool)query["CreateListQuestions"];
        //    _questionsToReview = _learnCategory.ReturnsQuestionForRepetition();
        //    InitializesFields(_questionsToReview.GetQuestion());
        //}
    }
}
