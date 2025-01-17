﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnApplication.Model;
using LearnApplication.Model.Web;
using LearnApplication.Navigation;
using LearnApplication.Service.Interface;
using LearnApplication.ViewModel.Base;


namespace LearnApplication.ViewModel
{
    public sealed partial class QuestionEditorViewModel :ViewModelBase
    {
    
        private CardQuestion _cardQuestion;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCangeCommand))]
        private string _question = string.Empty;

        [ObservableProperty]
        private string _answer = string.Empty;

        [ObservableProperty]
        private string _hyperlink = string.Empty;


        private readonly INavigationService _navigationService;
        private readonly ILocalDbService _localDbService;

        public QuestionEditorViewModel(INavigationService navigationService, ILocalDbService localDbService)
        {
            _navigationService = navigationService;
            _localDbService = localDbService;
        }


        public override Task OnNavigatingTo(object? parameter, object? parameterSecond = null)
        {
            if (parameter is CardQuestion learnQuestion)
            {
                _cardQuestion = learnQuestion;
               Question = _cardQuestion.Question;
                Answer = _cardQuestion.Answer;
                Hyperlink = _cardQuestion.Hyperlink.Url;
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

            

            _cardQuestion.ChangeQuestion(Question).ChangeAnswer(Answer).ChangeHyperlink(Hyperlink);

            _localDbService.Update(_cardQuestion.Hyperlink);
            _localDbService.Update(_cardQuestion);

            _navigationService.NavigateBackUpdate();  
        }
        public bool CheckQuestionEmpty() => !string.IsNullOrEmpty(Question);

        public override Task OnUpdateDbService()
        {
            //_localDbService.Update(_cardQuestion.Hyperlink);
            //_localDbService.Update(_cardQuestion);
            return base.OnUpdateDbService();
        }

        
    }
}
