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
    public sealed partial class QuestionsViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ILocalDbService _localDbService;
        private Category _category;

        [ObservableProperty]
        private ObservableCollection<СardQuestion> _learnQuestions = [];

        public QuestionsViewModel(INavigationService navigationService, ILocalDbService localDbService)
        {
            _navigationService = navigationService;
            _localDbService = localDbService;

        }


        //private void Initializes()
        //{

        //   // _category = _localDbService.GetById<Category>(_questId);
        //    LearnQuestions = _category.LearnQuestions;


        //}

        //public override Task OnUpdate()
        //{
        //    Initializes(); 
        //    return base.OnUpdate();
        //}



        //[ObservableProperty]
        //private Category _category;


        public RelayCommand AddCommand 
            => new(async () => await _navigationService.NavigateByPage<AddQuestionPage>(_category));
        public RelayCommand<СardQuestion> TapCommand
           => new(async (learnQuestion) => await _navigationService.NavigateByPage<SettingsPage>(learnQuestion));

        public RelayCommand<СardQuestion> DeleteCommand => new((learnQuestion) =>
        {
            if (learnQuestion is not null)
            {
                _category.RemoveQuestion(learnQuestion);
                _localDbService.Delete(learnQuestion);
            }
        });

        public override Task OnNavigatingTo(object? parameter, object? parameterSecond = null)
        {
            if (parameter is Category learnCategory)
            {
                _category = learnCategory;
                LearnQuestions = _category.LearnQuestions;
            }
            return base.OnNavigatingTo(parameter);
        }

        public override Task OnUpdateDbService()
        {
            _localDbService.Update(_category);
            return base.OnUpdateDbService();
        }
    }
}
