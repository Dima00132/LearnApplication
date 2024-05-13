using LearnApplication.Model.Interface;

namespace LearnApplication.Model.Command
{
    public sealed class RecalculationRepetitionsCommand(Subject subject) : ICommand
    {
        private readonly Subject _subject = subject;
        public void Execute()
            => _subject.RepetitionsQuestionsCount = _subject.LearnQuestions.Count(x=>x.IsRepetitions);
    }
}
