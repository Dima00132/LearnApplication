using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnApplication.Model;
using LearnApplication.Navigation;
using LearnApplication.Service;
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


        private Category _learnCategory;

        // private ObservableCollection<СardQuestion> _learnQuestions;

        private readonly INavigationService _navigationService;
        public readonly LocalDbService _localDbService;

        public AddQuestionViewModel(INavigationService navigationService,LocalDbService localDbService)
        {
            _navigationService = navigationService;
            _localDbService = localDbService;
            Question = string.Empty;
            Answer = string.Empty;
            Hyperlink = string.Empty;
            //_learnQuestions = [];


        }
     
        public override Task OnNavigatingTo(object? parameter)
        {
            //if (parameter is int id)
            //{
            //    _learnCategory = _localDbService.GetById<Category>(id);
            //    //_learnQuestions = new ObservableCollection<СardQuestion>(value.LearnQuestions);
            //}

            if (parameter is Category learnCategory)
            {
                _learnCategory = learnCategory;
                //_learnQuestions = new ObservableCollection<СardQuestion>(value.LearnQuestions);
            }
            return base.OnNavigatingTo(parameter);
        }

        public override Task OnSaveDb()
        {
            _localDbService.Update(_learnCategory);
            return base.OnSaveDb();
        }


        [RelayCommand(CanExecute = nameof(CheckQuestionEmpty))]
        public async Task AddLearnQuestion()
        {
            var question = new СardQuestion(Question, Answer,  Hyperlink);
            _learnCategory.AddLearnQuestion(question);
            
            _localDbService.Create(question);
            //_localDbService.Update(_learnCategory); 

            //LearnQuestions.AddLearnQuestion(new СardQuestion(СardQuestion, Answer, Hyperlink, IsKnown));
            //_learnQuestions.Add(new СardQuestion(СardQuestion, Answer, Hyperlink, IsKnown));

            await _navigationService.NavigateBack();

            //await Shell.Current.GoToAsync("../TabbedTestPage");
            //MessagingCenter.Send<AddQuestionViewModel, Category>(this, nameof(SubjectPage), _learnQuestions);
            //MessagingCenter.Send<AddQuestionViewModel, Category>(this, nameof(QuestionsViewModel), _learnQuestions);
        }

        public bool CheckQuestionEmpty() => !string.IsNullOrEmpty(Question);
    }
}
