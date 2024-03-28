using CommunityToolkit.Mvvm.ComponentModel;
using LearnApplication.Navigation;
using LearnApplication.View;
using LearnApplication.ViewModel.Base;


namespace LearnApplication.ViewModel
{
    public partial class TabbedLearnViewModel :ViewModelBase
    {
        [ObservableProperty]
        private SubjectViewModel _subjectViewModel;

        [ObservableProperty]
        private QuestionsViewModel _questionsViewModel;

        public TabbedLearnViewModel (SubjectViewModel subjectViewModel, QuestionsViewModel questViewModel)
        {
            SubjectViewModel = subjectViewModel;
            QuestionsViewModel = questViewModel;
        }

        public override Task OnNavigatingTo(object? parameter)
        {
            _subjectViewModel.OnNavigatingTo(parameter);
            _questionsViewModel.OnNavigatingTo(parameter);
            return base.OnNavigatingTo(parameter);
        }

    }
}
