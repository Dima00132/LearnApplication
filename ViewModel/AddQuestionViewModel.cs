using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnApplication.Model;
using LearnApplication.Model.Settings;
using LearnApplication.Model.Web;
using LearnApplication.Navigation;
using LearnApplication.Service.Interface;
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
        private Subject _category;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddQuestionCommand))]
        private string _question = string.Empty;

        [ObservableProperty]
        private string _answer = string.Empty;

        [ObservableProperty]
        private string _hyperlink = string.Empty;

        private readonly INavigationService _navigationService;
        public readonly ILocalDbService _localDbService;

        public readonly ISettingsApplication _settingsApplication;

        public AddQuestionViewModel(INavigationService navigationService, ILocalDbService localDbService,ISettingsApplication settingsApplication )
        {
            _navigationService = navigationService;
            _localDbService = localDbService;
            _settingsApplication = settingsApplication;
        }

        [RelayCommand(CanExecute = nameof(CheckQuestionEmpty))]
        public async Task AddQuestion()
        {
            if (!CheckNet.IsNullOrEmpty(Hyperlink) && !CheckNet.IsFormedUriString(Hyperlink))
            {
                Application.Current?.MainPage?.DisplayAlert("Connection error!", "Неверно указала ссылка на материал! Проверьте правильность ссылки.", "Ok");
                return;
            }

            var question = new CardQuestion(Question, _settingsApplication.GetNumberOfRepetitions(), Answer, Hyperlink);
            _category.AddQuestion(question);

            _localDbService.CreateAndUpdate(question, _category);
            //_localDbService.Create(question);
            //_localDbService.Update(_category);

            await _navigationService.NavigateBackUpdate();
        }

        public bool CheckQuestionEmpty() => !string.IsNullOrEmpty(Question);

        public override Task OnNavigatingTo(object? parameter, object? parameterSecond = null)
        {
            if (parameter is Subject learnCategory)
                _category = learnCategory;
            return base.OnNavigatingTo(parameter);
        }

        //public override Task OnUpdateDbService()
        //{
        //    _localDbService.Update(_category);
        //    return base.OnUpdateDbService();
        //}
    }
}
