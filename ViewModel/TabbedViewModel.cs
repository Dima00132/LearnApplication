using CommunityToolkit.Mvvm.ComponentModel;
using LearnApplication.Model;
using LearnApplication.Navigation;
using LearnApplication.View;
using LearnApplication.ViewModel.Base;


namespace LearnApplication.ViewModel
{
    public sealed partial class TabbedLearnViewModel :ViewModelBase
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

        public override Task OnUpdate()
        {
            SubjectViewModel?.OnUpdate();
            QuestionsViewModel?.OnUpdate();
            return base.OnUpdate();
        }

        public override Task OnUpdateDbService()
        {
            SubjectViewModel?.OnUpdateDbService();
            QuestionsViewModel?.OnUpdateDbService();
            return base.OnUpdateDbService();
        }

        public override Task OnNavigatingTo(object? parameter, object? parameterSecond = null)
        {
            if (parameter is Category learnCategory)
            {
                SubjectViewModel?.OnNavigatingTo(learnCategory);
                QuestionsViewModel?.OnNavigatingTo(learnCategory);

            }
            return base.OnNavigatingTo(parameter);
        }

    }
}
