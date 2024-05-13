using LearnApplication.Model.Interface;

namespace LearnApplication.Model.Command
{
    public sealed class RecalculationKnowCommand(Subject subject) : ICommand
    {
        private readonly Subject _subject = subject;
        public void Execute()
            => _subject.KnownCountLearn = _subject.LearnQuestions.Count(x => x.IsKnown);
    }
}
