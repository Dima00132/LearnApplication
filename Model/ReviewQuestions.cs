
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace LearnApplication.Model
{
    public partial class ReviewQuestion:ObservableObject
    {
        public ObservableCollection<СardQuestion> ReviewQuestions { get;set; }

        [ObservableProperty]
        public double _progress;

        private readonly double _countQuestions;
       // private readonly Category _learnCategory;
        private  double _knownQuestions;

        public bool IsQuestions { get => ReviewQuestions.Count >1; }


        private readonly bool _isAllOrUnknown;
        public ReviewQuestion(Category learnCategory, bool allOrUnknown = true)
        {
           // _learnCategory = learnCategory;
            _isAllOrUnknown = allOrUnknown;
            var reviewQuestions = _isAllOrUnknown ? learnCategory.LearnQuestions : learnCategory.LearnQuestions.Where(x => x.IsRepetitions & !x.IsKnown);
            ReviewQuestions = new ObservableCollection<СardQuestion>(reviewQuestions);
            _countQuestions = ReviewQuestions.Count;
            //Progress = allOrUnknown ? 0 : learnCategory.LearnQuestions.Count((x) => x.IsRepetitions) / _countQuestions;
            //Progress = 0;
            //_knownQuestions = 0;
        }

        public void MoveQuestionToEnd(СardQuestion learnQuestion)
        {
            ReviewQuestions.Remove(learnQuestion);
            ReviewQuestions.Add(learnQuestion);
        }

        public void DeleteQuestion(СardQuestion learnQuestion)
        {
            if (!_isAllOrUnknown)
            {
                learnQuestion.SetQuestionAsAlreadyKnown();
                //_knownQuestions++;
                Progress = _knownQuestions++ / _countQuestions;
            }

            ReviewQuestions.Remove(learnQuestion);
            ReviewQuestions.Add(learnQuestion);     
        }
    }
}
