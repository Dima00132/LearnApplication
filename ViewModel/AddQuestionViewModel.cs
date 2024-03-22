using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnApplication.Model;
using LearnApplication.Navigation;
using LearnApplication.View;
using LearnApplication.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LearnApplication.ViewModel
{
    public partial class AddQuestionViewModel:ViewModelBase
    {
        //[ObservableProperty]
        //private LearnCategory _learnQuestions;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddLearnQuestionCommand))]
        private string _question;

        [ObservableProperty]
        private string _answer;

        [ObservableProperty]
        private string _hyperlink;

        [ObservableProperty]
        private bool _isKnown;

        //[ObservableProperty]
        //private ObservableCollection<LearnQuestion> _learnQuestions;
        //[ObservableProperty]
        private Action<LearnQuestion> _actionAddLearnQuestion;

        private readonly INavigationService _navigationService;
        public AddQuestionViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public override Task OnNavigatingTo(object? parameter)
        {
            _actionAddLearnQuestion = parameter as Action<LearnQuestion>;
            // LearnQuestions = parameter as LearnCategory;
          //_learnQuestions = parameter as List<LearnQuestion>;
            return base.OnNavigatingTo(parameter);
        }


        [RelayCommand(CanExecute = nameof(CheckQuestionEmpty))]
        public async void   AddLearnQuestion()
        {
           _actionAddLearnQuestion?.Invoke(new LearnQuestion(Question, Answer, Hyperlink, IsKnown));
            //LearnQuestions.AddLearnQuestion(new LearnQuestion(Question, Answer, Hyperlink, IsKnown));
            //_learnQuestions.Add(new LearnQuestion(Question, Answer, Hyperlink, IsKnown));

            await _navigationService.NavigateBack();

            //await Shell.Current.GoToAsync("../TabbedTestPage");
            //MessagingCenter.Send<AddQuestionViewModel, LearnCategory>(this, nameof(SubjectPage), _learnQuestions);
            //MessagingCenter.Send<AddQuestionViewModel, LearnCategory>(this, nameof(QuestionsViewModel), _learnQuestions);
        }

        public bool CheckQuestionEmpty() => !string.IsNullOrEmpty(Question);
    }
}
