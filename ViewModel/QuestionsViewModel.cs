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
            var learnCategory = parameter as LearnCategory;
            InitializesFields(learnCategory);
            return base.OnNavigatingTo(parameter);
        }

    

        [ObservableProperty]
        private ObservableCollection<LearnQuestion> _learnQuestions;

        [ObservableProperty]
        private LearnCategory _learnCategory;


        public RelayCommand AddCommand { get=> new RelayCommand(async () => await _navigationService.NavigateTo<AddQuestionPage>(LearnQuestions.Add)); }
        
        async private void Add()
        {
             await _navigationService.NavigateTo<AddQuestionPage>(LearnQuestions.Add); 
        }


        [RelayCommand]
        public void Delete(LearnQuestion learnQuestion)
        {
            LearnQuestions.Remove(learnQuestion);
        }


        [RelayCommand]
        public async Task Tap(LearnQuestion learnQuestion)
        {
            await _navigationService.NavigateTo<SettingsPage>(learnQuestion);  
        }


        private readonly INavigationService _navigationService;
        public QuestionsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
           
        }

        private void InitializesFields(LearnCategory learnCategory)
        {
            LearnCategory = learnCategory;
     
            LearnQuestions = learnCategory.LearnQuestions;


        }

    }
}
