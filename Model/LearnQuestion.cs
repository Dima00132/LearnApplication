using CommunityToolkit.Mvvm.ComponentModel;


namespace LearnApplication.Model
{
    public partial class LearnQuestion:ObservableObject
    {
        [ObservableProperty]
        private string _question;

        [ObservableProperty]
        private string _answer;
        [ObservableProperty]
        private string _hyperlink;

        [ObservableProperty]
        private bool _isKnown;


        public LearnQuestion(string question, string answer ,string hyperlink = "", bool isKnown = false)
        {
            Question = question;
            Answer = answer;
            Hyperlink = hyperlink;
            IsKnown = isKnown;
        }

        public void Change(LearnQuestion learn)
        {
            Question = learn.Question;
            Answer = learn.Answer;
            Hyperlink = learn.Hyperlink;
            IsKnown = learn.IsKnown;
        }

    }
}
