using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnApplication.Model;
using LearnApplication.Model.Web;
using LearnApplication.Navigation;
using LearnApplication.Service.Interface;
using LearnApplication.View;
using LearnApplication.ViewModel.Base;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Internals;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LearnApplication.ViewModel
{
    public sealed partial class RepetitionViewModel : ViewModelBase
    {   
        [ObservableProperty]
        private ReviewQuestion _reviewQuestion;

        [ObservableProperty]
        private bool _isVisibleLink;

   
        //[ObservableProperty]
        //[NotifyCanExecuteChangedFor(nameof(ChecksLinkCommand))]
        //private string _isWorkingLinkImage;

        private INavigationService _navigationService;
        private Category _learnCategory;
        private readonly ILocalDbService _localDbService;

        public RepetitionViewModel(INavigationService navigationService, ILocalDbService localDbService)
        {
            _navigationService = navigationService;
           _localDbService = localDbService;
          
        }

        public RelayCommand<CardQuestion> SettingsCommand => new((learnQuestion) => _navigationService.NavigateByPage<QuestionEditorPage>(learnQuestion));

        

        public RelayCommand<CardQuestion> DontKnowCommand => new((learnQuestion) =>
        {
            if (learnQuestion is not null)
            {
                ReviewQuestion.MoveQuestionToEnd(learnQuestion);
            }
     
        });

        //public override Task OnUpdateDbService()
        //{
        //    _localDbService.Update(_learnCategory);
        //    return base.OnUpdateDbService();
        //}

        public RelayCommand<CardQuestion> KnowCommand => new((question) =>
        {
            if (!ReviewQuestion.IsQuestions)
                _navigationService.NavigateBackUpdate();
            if (question is not null)
            {
               ReviewQuestion.DeleteQuestion(question);
               
               _localDbService.Update(question);
                _localDbService.Update(_learnCategory);
            }
        });








        [RelayCommand()]
        public void ChecksLink(CardQuestion question)
        {
            IsVisibleLink = !question.Hyperlink.IsNullOrEmpty;
        }



        [RelayCommand]
        public void LinkToAdditionalMaterial(CardQuestion question)
        {
            if (!question.Hyperlink.IsUrlValid) 
            { 
                Application.Current?.MainPage?.DisplayAlert("Connection error!", "Неверно указала ссылка на материал! Проверьте правильность ссылки.", "Ok");
                return;
            }
            _navigationService.NavigateByPage<WebPage>(question.Hyperlink.GetUrlWebViewSource());
        }

        //public bool IsCurentLink(CardQuestion question)
        //{

        //    if (string.IsNullOrEmpty(question?.Hyperlink.Url))
        //    {
        //        IsVisibleLink = false;
        //    }
        //    return true;
        //}

        //public RelayCommand<CardQuestion> LinkToAdditionalMaterialCommand => new((question) => 
        //{

        //    var urlWeb = question?.GetUrlWebView();
        //    //if (!urlWeb.IsUrlValid)
        //    //{
        //    //    Application.Current?.MainPage?.DisplayAlert("Connection error!", "Неверно указала ссылка на материал! Проверьте правильность ссылки.", "Ok");
        //    //    return;
        //    //}
        //    _navigationService.NavigateByPage<WebPage>(urlWeb.GetUrlWebViewSource());
        //},
        //    (question) => 
        //{
        //    var urlWeb = question?.GetUrlWebView();

        //    if (!urlWeb.IsUrlValid)
        //        IsWorkingLinkImage = "image_not_working_link.png";
        //    IsWorkingLinkImage = "image_working_link.png";

        //     var f = !CheckNet.IsNullOrEmpty(question.Hyperlink);

        //    return urlWeb.IsUrlValid;
        //});

        
        public override Task OnNavigatingTo(object? parameterFirst, object? parameterSecond)
        {
            if(parameterFirst is Category learnCategory)
            {
                _learnCategory = learnCategory;
                if(parameterSecond is bool allOrUnknown)
                    ReviewQuestion = learnCategory.GetReviewQuestions(allOrUnknown);
            }
            return base.OnNavigatingTo(parameterFirst, parameterSecond);
        }
    }
}
