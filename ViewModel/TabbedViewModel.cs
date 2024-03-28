using CommunityToolkit.Mvvm.ComponentModel;
using LearnApplication.Model;
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
            if (parameter is LearnCategory learnCategory)
            {
                SubjectViewModel?.OnNavigatingTo(learnCategory);
                QuestionsViewModel?.OnNavigatingTo(learnCategory.LearnQuestions);
                
            }
            return base.OnNavigatingTo(parameter);
        }

    }
}
