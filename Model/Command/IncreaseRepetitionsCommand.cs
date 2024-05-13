using LearnApplication.Model.Interface;
using System.Linq;

namespace LearnApplication.Model.Command
{


    public sealed class IncreaseRepetitionsCommand(Subject subject, ICardQuestion question =null ) : ICommand
    {
        private readonly Subject _subject = subject;
        private readonly ICardQuestion? _question = question;
        public void Execute()
        {
            _subject.RepetitionsQuestionsCount++;
            if ( question is not null)
                subject?.UpdateDbEvent?.Invoke(_question,EventArgs.Empty);
        }
        //public void Execute()
        //{
        //    _subject.RepetitionsQuestionsCount = _subject.LearnQuestions.Where(x => x.IsRepetitions).Count();
        //}
    }
}
