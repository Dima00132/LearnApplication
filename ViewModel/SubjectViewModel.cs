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

    public partial class SubjectViewModel : ViewModelBase
    {
        [ObservableProperty]
        private  LearnCategory _learnCategory;
        public RelayCommand RepeatAllQuestionsCommand => new(() => 
            _navigationService.NavigateByPage<RepetitionOfMaterialPage>(_learnCategory?.Id),
             () => LearnCategory?.LearnQuestions.Count != 0);
        public RelayCommand RepeatDontKnownQuestionsCommand => new(() =>
            _navigationService.NavigateByPage<RepetitionOfMaterialPage>(_learnCategory?.Id),
            () => LearnCategory?.LearnQuestions.Count != 0 & LearnCategory?.DointKnownCountLearn != 0);

        [ObservableProperty]
        private double _progressLearn;

        [ObservableProperty]
        public int _learnCount;

        [ObservableProperty]
        private double _dontKnownCount;

        [ObservableProperty]
        private double _knownCount;

        [ObservableProperty]
        private int _reviewQuestionCount;




        private readonly INavigationService _navigationService;
        private readonly LocalDbService _localDbService;

        public SubjectViewModel(INavigationService navigationService, LocalDbService localDbService)
        {
            _navigationService = navigationService;
            _localDbService = localDbService;
        }

        public override Task OnUpdate()
        {
            InitializesFields();
            return base.OnUpdate();
        }

        public void InitializesFields()
        {
            LearnCategory = _localDbService.GetById<LearnCategory>(_primaryKeyId);
           // ProgressLearns = _learnCategory.ProgressLearn;

            //ProgressLearn = _learnCategory.CountProgressLearn;
            //LearnCount = _learnCategory.CountQuestion;
            //DontKnownCount = _learnCategory.DointKnownCountLearn;
            //KnownCount = _learnCategory.KnownCountLearn;
            
        }

        private int _primaryKeyId;

        public override Task OnNavigatingTo(object? parameter)
        {

            if (parameter is int id)
            {
                _primaryKeyId = id;
                InitializesFields();
            }
            return base.OnNavigatingTo(parameter);
        }

        //public RelayCommand AddCommand => new RelayCommand(Add);
        //private void Add()
        //{

        //    _navigationService.NavigateByPage<AddQuestionPage>(_learnCategory);

        //}


    

        //[RelayCommand]
        //async Task RepeatQuestions()
        //{
        //    await _navigationService.NavigateByPage<RepetitionOfMaterialPage>(_learnCategory);
        //}

       // public bool CheckCountQuestions() => _learnCategory?.LearnQuestions.Count != 0;

    }
}
