using CommunityToolkit.Mvvm.ComponentModel;
using LearnApplication.Model;
using LearnApplication.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnApplication.ViewModel
{
    public partial class TabbedRepetitionViewModel:ViewModelBase
    {
        [ObservableProperty]
        private RepetitionViewModel _repetitionOfEverythingViewModel;
        [ObservableProperty]
        private RepetitionViewModel _repetitionOfUnknownsViewModel;

        //[ObservableProperty]
        //private RepetitionOfUnknownsViewModel _repetitionOfUnknownsViewModel;

        public TabbedRepetitionViewModel(RepetitionViewModel repetitionOfEverythingViewModel, RepetitionViewModel repetitionOfUnknownsViewModel)
        {
            _repetitionOfEverythingViewModel = repetitionOfEverythingViewModel;
            _repetitionOfUnknownsViewModel = repetitionOfUnknownsViewModel;
        }

        public override Task OnUpdate()
        {
            RepetitionOfEverythingViewModel?.OnUpdate();
            RepetitionOfUnknownsViewModel?.OnUpdate();
            return base.OnUpdate();
        }

        public override Task OnUpdateDbService()
        {
            RepetitionOfEverythingViewModel?.OnUpdateDbService();
            RepetitionOfUnknownsViewModel?.OnUpdateDbService();
            return base.OnUpdateDbService();
        }

        public override Task OnNavigatingTo(object? parameter, object? parameterSecond = null)
        {
            if (parameter is Category learnCategory)
            {
                RepetitionOfEverythingViewModel?.OnNavigatingTo(learnCategory, parameterSecond);
                RepetitionOfUnknownsViewModel?.OnNavigatingTo(learnCategory, parameterSecond);

            }
            return base.OnNavigatingTo(parameter);
        }

    }
}
