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
        private Category _learnCategory;
        private int _questId;

        public override Task OnNavigatingTo(object? parameter, object? parameterSecond = null)
        {
            //if (parameter is int id)
            //{
            //    _questId = id;
            //    Initializes();
            //}

            if (parameter is Category learnCategory)
            {
                _learnCategory = learnCategory;
                LearnQuestions = _learnCategory.LearnQuestions;
            }
            return base.OnNavigatingTo(parameter);
        }
        public override Task OnUpdateDbService()
        {
            _localDbService.Update(_learnCategory);
            return base.OnUpdateDbService();
        }
        private void Initializes()
        {

           // _category = _localDbService.GetById<Category>(_questId);
            LearnQuestions = _learnCategory.LearnQuestions;
            

        }

        //public override Task OnUpdate()
        //{
        //    Initializes(); 
        //    return base.OnUpdate();
        //}


        [ObservableProperty]
        private ObservableCollection<СardQuestion> _learnQuestions = new();

        //[ObservableProperty]
        //private Category _category;


        public RelayCommand AddCommand => new(async () => await _navigationService.NavigateByPage<AddQuestionPage>(_learnCategory));

        //async private void Add()
        //{
        //     await _navigationService.NavigateByPage<AddQuestionPage>(LearnQuestions.Add); 
        //}


        public RelayCommand<СardQuestion> DeleteCommand => new((learnQuestion) =>
        {
            if (learnQuestion is not null)
            {
                _learnCategory.RemoveQuestion(learnQuestion);
      /*          LearnQuestions.Remove(learnQuestion)*/;
                _localDbService.Delete(learnQuestion);
            }
        });

        //[RelayCommand]
        //public void Delete(СardQuestion learnQuestion)
        //{
        //    LearnQuestions.Remove(learnQuestion);
        //}


        public RelayCommand<СardQuestion> TapCommand 
            => new(async (learnQuestion) =>
            {
                await _navigationService.NavigateByPage<SettingsPage>(learnQuestion);
                
            });

        //[RelayCommand]
        //public async Task Tap(СardQuestion learnQuestion)
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
