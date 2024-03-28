using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnApplication.Model;
using LearnApplication.Navigation;
using LearnApplication.View;
using LearnApplication.ViewModel.Base;
using Microsoft.Maui.Animations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LearnApplication.ViewModel
{
    public partial class SettingsViewModel :ViewModelBase
    {
    
        private LearnQuestion _learnQuestion;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCangeCommand))]
        private string _question;

        [ObservableProperty]
        private string _answer;

        [ObservableProperty]
        private string _hyperlink;


        private readonly INavigationService _navigationService;
        public SettingsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Question = string.Empty;
            Answer = string.Empty;
            Hyperlink = string.Empty;
            _learnQuestion = new LearnQuestion();
        }


        public override Task OnNavigatingTo(object? parameter)
        {
            if (parameter is LearnQuestion learnQuestion)
            {
                _learnQuestion = learnQuestion;
                Question = learnQuestion.Question;
                Answer = learnQuestion.Answer;
            }
            return base.OnNavigatingTo(parameter);
        }


        [RelayCommand(CanExecute = nameof(CheckQuestionEmpty))]
        public void SaveCange()
        {

            _learnQuestion.Change(new LearnQuestion(Question, Answer,Hyperlink));
            _navigationService.NavigateBack();
        }

        public bool CheckQuestionEmpty() => !string.IsNullOrEmpty(Question);
    }
}
