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
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddLearnQuestionCommand))]
        private string _question;

        [ObservableProperty]
        private string _answer;

        [ObservableProperty]
        private string _hyperlink;

        private ObservableCollection<LearnQuestion> _learnQuestions;

        private readonly INavigationService _navigationService;
        public AddQuestionViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Question = string.Empty;
            Answer = string.Empty;
            Hyperlink = string.Empty;
            _learnQuestions = [];


        }
        public override Task OnNavigatingTo(object? parameter)
        {
            if (parameter is ObservableCollection<LearnQuestion> questions)
                _learnQuestions = questions;
            return base.OnNavigatingTo(parameter);
        }


        [RelayCommand(CanExecute = nameof(CheckQuestionEmpty))]
        public async Task AddLearnQuestion()
        {
            _learnQuestions.Add(new LearnQuestion(Question, Answer, Hyperlink));
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
