using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnApplication.Model;
using LearnApplication.Navigation;
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
        
        private  LearnCategory _learnCategory;
        public RelayCommand RepeatAllQuestionsCommand 
        { get => new RelayCommand(() => _navigationService.NavigateByPage<RepetitionOfMaterialPage>(_learnCategory?.GetReviewQuestions()),
                () => _learnCategory?.LearnQuestions.Count != 0);  }
        public RelayCommand RepeatDontKnownQuestionsCommand 
        { get => new RelayCommand(() =>
            _navigationService.NavigateByPage<RepetitionOfMaterialPage>(_learnCategory?.GetReviewQuestions(false)),
            () => _learnCategory?.LearnQuestions.Count != 0 & _learnCategory?.DointKnownCountLearn() != 0); }

        [ObservableProperty]
        private double _progressLearn;

        [ObservableProperty]
        public int _learnCount;

        [ObservableProperty]
        private double _dointKnownCount;

        [ObservableProperty]
        private double _knownCount;


        private readonly INavigationService _navigationService;
        public SubjectViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            //RepeatAllQuestionsCommand = new RelayCommand(
            //    ()=>_navigationService.NavigateByPage<RepetitionOfMaterialPage>(_learnCategory?.GetReviewQuestions()),
            //    () => _learnCategory?.LearnQuestions.Count != 0);

            //RepeatDontKnownQuestionsCommand = new RelayCommand(()=> 
            //_navigationService.NavigateByPage<RepetitionOfMaterialPage>(_learnCategory?.GetReviewQuestions(false)),
            //()=> _learnCategory?.LearnQuestions.Count != 0 & _learnCategory?.DointKnownCountLearn() != 0);

        }

        public void InitializesFields()
        {
            if (_learnCategory is null)
                return;
           ProgressLearn = _learnCategory.CountProgressLearn();
            LearnCount = _learnCategory.CountQuestion();
            DointKnownCount = _learnCategory.TestCountDontKnown;
            KnownCount = _learnCategory.KnownCountLearn();
        }
        public override Task OnNavigatingTo(object? parameter)
        {
            _learnCategory = parameter as LearnCategory;
            InitializesFields();
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
