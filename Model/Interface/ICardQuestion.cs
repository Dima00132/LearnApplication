using LearnApplication.Model.Web;

namespace LearnApplication.Model.Interface
{
    public interface ICardQuestion : ICard, IRepetition, ITimeRepetition
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public UrlWebValid Hyperlink { get; set; }
        CardQuestion ChangeQuestion(string question);
        CardQuestion ChangeAnswer(string answer);
        CardQuestion ChangeHyperlink(string hyperlink);
    }
}
