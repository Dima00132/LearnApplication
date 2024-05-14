using LearnApplication.Model.Interface;

namespace LearnApplication.Model.Command.Base
{
    public abstract class SubjectStateChangeCommand
    {
        protected double CalculatesStudyPercentage(double learningProgress, double countQuestions)
        {
            var percentage = learningProgress * 100.0 / countQuestions;
            var percentageInTenths = Math.Round(percentage, 1);
            return percentageInTenths;
        }
        protected double AddsProgress(int countQuestions, IRepetition question)
        {
            double learningProgress = question.GetStudyProgress();
            return CalculatesStudyPercentage(learningProgress, countQuestions);
        }

        protected double RereadStudyProgres(Subject category)
        {
            if (category.LearnQuestions.Count == 0)
                return 0;
            double learningProgress = category.LearnQuestions.Sum(x => x.GetStudyProgress());
            return CalculatesStudyPercentage(learningProgress, category.LearnQuestions.Count);
        }
    }
}
