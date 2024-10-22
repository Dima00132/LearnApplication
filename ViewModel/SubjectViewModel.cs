﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnApplication.Model;
using LearnApplication.Navigation;
using LearnApplication.Service.Interface;
using LearnApplication.View;
using LearnApplication.ViewModel.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace LearnApplication.ViewModel
{

    public sealed partial class SubjectViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ILocalDbService _localDbService;

        [ObservableProperty]
        private  Subject _category;


        //[ObservableProperty]
        //private double _progressLearn;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RepeatAllQuestionsCommand))]
        public int _learnCount;

        //[ObservableProperty]
        //[NotifyCanExecuteChangedFor(nameof(RepeatDontKnownQuestionsCommand))]

        [ObservableProperty]
        private double _knownCount;


        //public RelayCommand RepeatAllQuestionsCommand => new(() =>
        //    _navigationService.NavigateByPage<TabbedRepetitionPage>(Subject, true),
        //    () => Subject?.CountQuestion != 0);

        //public RelayCommand RepeatDontKnownQuestionsCommand => new(() =>
        //   _navigationService.NavigateByPage<TabbedRepetitionPage>(Subject, false),
        //   () => Subject?.RepetitionsQuestionsCount != 0);


        [RelayCommand(CanExecute = nameof(CheckCountQuestion))]
        public void RepeatAllQuestions()
            => _navigationService.NavigateByPage<RepetitionPage>(Category, true);
        public bool CheckCountQuestion() => Category?.CountQuestion != 0;




        [RelayCommand(CanExecute = nameof(CheckRepetitionsCount))]
        public void RepeatDontKnownQuestions()
            => _navigationService.NavigateByPage<RepetitionPage>(Category, false);
        public bool CheckRepetitionsCount() => Category?.RepetitionsQuestionsCount != 0;


        public RelayCommand AddQuestionCommand 
            => new(async () => await _navigationService.NavigateByPage<AddQuestionPage>(Category));


        public SubjectViewModel(INavigationService navigationService, ILocalDbService localDbService)
        {
            _navigationService = navigationService;
            _localDbService = localDbService;
            
        }

        public override Task OnUpdate()
        {

            //LearnCount = Subject.CountQuestion;
            //RepetitionsCount = Subject.RepetitionsQuestionsCount;
            //KnownCount = Subject.KnownCountLearn;
            return base.OnUpdate();
        }

        public override Task OnUpdateDbService()
        {
            _localDbService.Update(Category);
            return base.OnUpdateDbService();
        }

        //public void InitializesFields()
        //{
        //    LearnCount = Subject.CountQuestion;
        //    RepetitionsQuestionsCount = Subject.RepetitionsQuestionsCount;
        //    KnownCount = Subject.KnownCountLearn;
           
        //}

        public override Task OnNavigatingTo(object? parameter, object? parameterSecond = null)
        {

            if (parameter is Subject category)
            {
                //_primaryKeyId = id;
                Category = category;
               // RepetitionsCount = category.RepetitionsQuestionsCount;

               // _category.TimerTick += Timer_Tick;
               // InitializesFields();
            }
            return base.OnNavigatingTo(parameter);
        }

        //public RelayCommand AddCommand => new RelayCommand(Add);
        //private void Add()
        //{

        //    _navigationService.NavigateByPage<AddQuestionPage>(_category);

        //}


    

        //[RelayCommand]
        //async Task RepeatQuestions()
        //{
        //    await _navigationService.NavigateByPage<RepetitionPage>(_category);
        //}

       // public bool CheckCountQuestions() => _category?.LearnQuestions.Count != 0;

    }
}
