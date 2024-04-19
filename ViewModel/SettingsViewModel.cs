using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnApplication.Model;
using LearnApplication.Model.Web;
using LearnApplication.Navigation;
using LearnApplication.Service;
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
    public sealed partial class SettingsViewModel :ViewModelBase
    {
    
        private СardQuestion _learnQuestion;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCangeCommand))]
        private string _question = string.Empty;

        [ObservableProperty]
        private string _answer = string.Empty;

        [ObservableProperty]
        private string _hyperlink = string.Empty;


        private readonly INavigationService _navigationService;
        private readonly ILocalDbService _localDbService;

        public SettingsViewModel(INavigationService navigationService, ILocalDbService localDbService)
        {
            _navigationService = navigationService;
            _localDbService = localDbService;
        }


        public override Task OnNavigatingTo(object? parameter, object? parameterSecond = null)
        {
            if (parameter is СardQuestion learnQuestion)
            {
                _learnQuestion = learnQuestion;
               Question = _learnQuestion.Question;
                Answer = _learnQuestion.Answer;
                Hyperlink = _learnQuestion.Hyperlink;
            }
            return base.OnNavigatingTo(parameter);
        }


        [RelayCommand(CanExecute = nameof(CheckQuestionEmpty))]
        public void SaveCange()
        {
            if (!CheckNet.IsNullOrEmpty(Hyperlink) && !CheckNet.IsFormedUriString(Hyperlink))
            {
                Application.Current?.MainPage?.DisplayAlert("Connection error!", "Неверно указала ссылка на материал! Проверьте правильность ссылки.", "Ok");
                return;
            }

            _learnQuestion.Change(Question, Answer, Hyperlink);
            _navigationService.NavigateBackUpdate();  
        }
        public bool CheckQuestionEmpty() => !string.IsNullOrEmpty(Question);

        public override Task OnUpdateDbService()
        {
            _localDbService.Update(_learnQuestion);
            return base.OnUpdateDbService();
        }

        
    }
}
