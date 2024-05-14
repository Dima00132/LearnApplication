using LearnApplication.Model.Command.Base;
using LearnApplication.Model.Command.Interface;
using LearnApplication.Model.Interface;

namespace LearnApplication.Model.Command
{
    public sealed class InsertCommand(Subject subject, IRepetition? question) : SubjectStateChangeCommand, ICommand
    {
        private readonly Subject _subject = subject;
        private readonly IRepetition? _question = question;
        public void Execute()
        {
            _subject.CountQuestion++;
            _question.IsRepetitions = true;
            _subject.MemorizationPercentage += AddsProgress(_subject.CountQuestion, _question);
            
        }
    }
}
