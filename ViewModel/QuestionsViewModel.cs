using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnApplication.Model;
using LearnApplication.Navigation;
using LearnApplication.View;
using LearnApplication.ViewModel.Base;
using Microsoft.Maui.Animations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LearnApplication.ViewModel
{ 
    public partial class QuestionsViewModel : ViewModelBase
    {
        public override Task OnNavigatingTo(object? parameter)
        {
            if(parameter is ObservableCollection<LearnQuestion> learnQuestions)
                LearnQuestions = learnQuestions;
            return base.OnNavigatingTo(parameter);
        }



        [ObservableProperty]
        private ObservableCollection<LearnQuestion> _learnQuestions = new();

        //[ObservableProperty]
        //private LearnCategory _learnCategory;


        public RelayCommand AddCommand => new(async () => await _navigationService.NavigateByPage<AddQuestionPage>(LearnQuestions));

        //async private void Add()
        //{
        //     await _navigationService.NavigateByPage<AddQuestionPage>(LearnQuestions.Add); 
        //}


        public RelayCommand<LearnQuestion> DeleteCommand => new((learnQuestion) =>
        {
            if(learnQuestion is not null)
                LearnQuestions.Remove(learnQuestion);
        });

        //[RelayCommand]
        //public void Delete(LearnQuestion learnQuestion)
        //{
        //    LearnQuestions.Remove(learnQuestion);
        //}


        public RelayCommand<LearnQuestion> TapCommand => new(async (learnQuestion) => await _navigationService.NavigateByPage<SettingsPage>(learnQuestion));

        //[RelayCommand]
        //public async Task Tap(LearnQuestion learnQuestion)
        //{
        //    await _navigationService.NavigateByPage<SettingsPage>(learnQuestion);  
        //}


        private readonly INavigationService _navigationService;
        public QuestionsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
