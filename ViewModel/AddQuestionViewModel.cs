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
    public sealed partial class AddQuestionViewModel:ViewModelBase
    {
        private Category _learnCategory;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddLearnQuestionCommand))]
        private string _question = string.Empty;

        [ObservableProperty]
        private string _answer = string.Empty;

        [ObservableProperty]
        private string _hyperlink = string.Empty;

        private readonly INavigationService _navigationService;
        public readonly ILocalDbService _localDbService;

        public AddQuestionViewModel(INavigationService navigationService, ILocalDbService localDbService)
        {
            _navigationService = navigationService;
            _localDbService = localDbService;
        }
     
        [RelayCommand(CanExecute = nameof(CheckQuestionEmpty))]
        public async Task AddLearnQuestion()
        {
            var question = new СardQuestion(Question, Answer,  Hyperlink);
            _learnCategory.AddQuestion(question);       
            _localDbService.Create(question);
            await _navigationService.NavigateBackUpdate();
        }

        public bool CheckQuestionEmpty() => !string.IsNullOrEmpty(Question);

        public override Task OnNavigatingTo(object? parameter, object? parameterSecond = null)
        {
            if (parameter is Category learnCategory)
                _learnCategory = learnCategory;
            return base.OnNavigatingTo(parameter);
        }

        public override Task OnUpdateDbService()
        {
            _localDbService.Update(_learnCategory);
            return base.OnUpdateDbService();
        }
    }
}
