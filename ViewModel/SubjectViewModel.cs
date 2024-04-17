using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnApplication.Model;
using LearnApplication.Navigation;
using LearnApplication.Service;
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
        private  Category _category;

        [ObservableProperty]
        private double _progressLearn;

        [ObservableProperty]
        public int _learnCount;

        [ObservableProperty]
        private double _repetitionsCount;

        [ObservableProperty]
        private double _knownCount;

        [ObservableProperty]
        private int _reviewQuestionCount;
        public RelayCommand RepeatAllQuestionsCommand => new(() =>
            _navigationService.NavigateByPage<TabbedRepetitionPage>(Category, true),
            () => Category?.CountQuestion != 0);
        public RelayCommand RepeatDontKnownQuestionsCommand => new(() =>
            _navigationService.NavigateByPage<TabbedRepetitionPage>(Category, false),
            () => Category?.RepetitionsCount != 0);

        public SubjectViewModel(INavigationService navigationService, ILocalDbService localDbService)
        {
            _navigationService = navigationService;
            _localDbService = localDbService;
        }

        public override Task OnUpdate()
        {
            InitializesFields();
            return base.OnUpdate();
        }

        public override Task OnUpdateDbService()
        {
            _localDbService.Update(Category);
            return base.OnUpdateDbService();
        }

        public void InitializesFields()
        {
            LearnCount = Category.CountQuestion;
            RepetitionsCount = Category.RepetitionsCount;
            KnownCount = Category.KnownCountLearn;
           
        }

        public override Task OnNavigatingTo(object? parameter, object? parameterSecond = null)
        {

            if (parameter is Category category)
            {
                //_primaryKeyId = id;
                Category = category;
               // _category.TimerTick += Timer_Tick;
                InitializesFields();
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
