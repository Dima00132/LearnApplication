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
            _repetitionOfEverythingViewModel?.OnUpdate();
            _repetitionOfUnknownsViewModel?.OnUpdate();
            return base.OnUpdate();
        }

        public override Task OnNavigatingTo(object? parameter)
        {
            if (parameter is LearnCategory learnCategory)
            {
                _repetitionOfEverythingViewModel?.OnNavigatingTo(learnCategory);
                _repetitionOfUnknownsViewModel?.OnNavigatingTo(learnCategory);

            }
            return base.OnNavigatingTo(parameter);
        }

    }
}
