using LearnApplication.Model.Command.Base;
using LearnApplication.Model.Command.Interface;

namespace LearnApplication.Model.Command
{
    public sealed class ChangeRepetitionNumberCommand(Subject subject) : SubjectStateChangeCommand, ICommand
    {
        private readonly Subject _subject = subject;
        public void Execute()
        {
            _subject.MemorizationPercentage = RereadStudyProgres(_subject);
        }
    }
}
