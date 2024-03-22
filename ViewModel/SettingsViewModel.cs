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
     
        private LearnQuestion _learnQuestiong;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCangeCommand))]
        private string _question;

        [ObservableProperty]
        private string _answer;

        [ObservableProperty]
        private string _hyperlink;

        [ObservableProperty]
        private bool _isKnown;


        private readonly INavigationService _navigationService;
        public SettingsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        public override Task OnNavigatingTo(object? parameter)
        {


            _learnQuestiong = parameter as LearnQuestion;
            Question = _learnQuestiong?.Question;
            Answer = _learnQuestiong?.Answer;
            return base.OnNavigatingTo(parameter);
        }


        [RelayCommand(CanExecute = nameof(CheckQuestionEmpty))]
        public void SaveCange()
        {

            _learnQuestiong.Change(new LearnQuestion(Question, Answer,Hyperlink,IsKnown));
            _navigationService.NavigateBack();
        }

        public bool CheckQuestionEmpty() => !string.IsNullOrEmpty(Question);
    }
}
