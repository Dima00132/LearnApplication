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
        private RepetitionOfEverythingViewModel _repetitionOfEverythingViewModel;
        [ObservableProperty]
        private RepetitionOfEverythingViewModel _repetitionOfUnknownsViewModel;

        //[ObservableProperty]
        //private RepetitionOfUnknownsViewModel _repetitionOfUnknownsViewModel;

        public TabbedRepetitionViewModel(RepetitionOfEverythingViewModel repetitionOfEverythingViewModel, RepetitionOfEverythingViewModel repetitionOfUnknownsViewModel)
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
           // RepetitionOfUnknownsViewModel?.OnUpdateDbService();
            return base.OnUpdateDbService();
        }

        public override Task OnNavigatingTo(object? parameter)
        {
            if (parameter is Category learnCategory)
            {
                RepetitionOfEverythingViewModel?.OnNavigatingTo(learnCategory);
                RepetitionOfUnknownsViewModel?.OnNavigatingTo(learnCategory);

            }
            return base.OnNavigatingTo(parameter);
        }

    }
}
