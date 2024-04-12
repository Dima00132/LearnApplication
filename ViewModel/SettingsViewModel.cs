﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnApplication.Model;
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
    public partial class SettingsViewModel :ViewModelBase
    {
    
        private СardQuestion _learnQuestion;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCangeCommand))]
        private string _question;

        [ObservableProperty]
        private string _answer;

        [ObservableProperty]
        private string _hyperlink;


        private readonly INavigationService _navigationService;
        private readonly LocalDbService _localDbService;

        public SettingsViewModel(INavigationService navigationService, LocalDbService localDbService)
        {
            _navigationService = navigationService;
            _localDbService = localDbService;
            Question = string.Empty;
            Answer = string.Empty;
            Hyperlink = string.Empty;
           // _learnQuestion = new СardQuestion();
        }


        public override Task OnNavigatingTo(object? parameter)
        {
            //if (parameter is int id)
            //{
            //    _learnQuestion = _localDbService.GetById<СardQuestion>(id);
            //    СardQuestion = _learnQuestion.СardQuestion;
            //    Answer = _learnQuestion.Answer;
            //    Hyperlink = _learnQuestion.Hyperlink;
            //}

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

            _learnQuestion.Change(new СardQuestion(Question, Answer, Hyperlink));
            //_localDbService.Update(_learnQuestion);
            _navigationService.NavigateBack();
           
        }
        public override Task OnSaveDb()
        {
            _localDbService.Update(_learnQuestion);
            return base.OnSaveDb();
        }

        public bool CheckQuestionEmpty() => !string.IsNullOrEmpty(Question);
    }
}
