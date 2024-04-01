using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnApplication.Model;
using LearnApplication.Navigation;
using LearnApplication.Service;
using LearnApplication.View;
using LearnApplication.ViewModel.Base;
using Microsoft.Maui.Animations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LearnApplication.ViewModel
{ 
    public partial class QuestionsViewModel : ViewModelBase
    {
        private LearnCategory _learnCategory;
        private int _questId;

        public override Task OnNavigatingTo(object? parameter)
        {
            if (parameter is int id)
            {
                _questId = id;
                Initializes();
            }
            return base.OnNavigatingTo(parameter);
        }

        private void Initializes()
        {

            _learnCategory = _localDbService.GetById<LearnCategory>(_questId);
            LearnQuestions = _learnCategory.LearnQuestions;
            

        }

        public override Task OnUpdate()
        {
            Initializes(); 
            return base.OnUpdate();
        }


        [ObservableProperty]
        private ObservableCollection<LearnQuestion> _learnQuestions = new();

        //[ObservableProperty]
        //private LearnCategory _learnCategory;


        public RelayCommand AddCommand => new(async () => await _navigationService.NavigateByPage<AddQuestionPage>(_learnCategory.Id));

        //async private void Add()
        //{
        //     await _navigationService.NavigateByPage<AddQuestionPage>(LearnQuestions.Add); 
        //}


        public RelayCommand<LearnQuestion> DeleteCommand => new((learnQuestion) =>
        {
            if (learnQuestion is not null)
            {
                LearnQuestions.Remove(learnQuestion);
                _localDbService.Delete(learnQuestion);
            }
        });

        //[RelayCommand]
        //public void Delete(LearnQuestion learnQuestion)
        //{
        //    LearnQuestions.Remove(learnQuestion);
        //}


        public RelayCommand<LearnQuestion> TapCommand 
            => new(async (learnQuestion) =>
            {
                await _navigationService.NavigateByPage<SettingsPage>(learnQuestion.Id);
                
            });

        //[RelayCommand]
        //public async Task Tap(LearnQuestion learnQuestion)
        //{
        //    await _navigationService.NavigateByPage<SettingsPage>(learnQuestion);  
        //}


        private readonly INavigationService _navigationService;
        private readonly LocalDbService _localDbService;

        public QuestionsViewModel(INavigationService navigationService, LocalDbService localDbService)
        {
            _navigationService = navigationService;
            _localDbService = localDbService;
            
        }
    }
}
