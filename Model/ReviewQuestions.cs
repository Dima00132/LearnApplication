
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace LearnApplication.Model
{
    public partial class ReviewQuestion:ObservableObject
    {
        public ObservableCollection<СardQuestion> ReviewQuestions { get;set; }

        [ObservableProperty]
        public double _progress;

        [ObservableProperty]
        private  double _countQuestions;
        // private readonly Category _category;

        [ObservableProperty]
        private  double _knownQuestions;

        //[ObservableProperty]
        //private int _thereAreStillQuestions;

        public bool IsQuestions { get => ReviewQuestions.Count >1; }

        private readonly Category _category;
        private readonly bool _isAllOrUnknown;
        public ReviewQuestion(Category learnCategory, bool allOrUnknown = true)
        {
            _category = learnCategory;
            _isAllOrUnknown = allOrUnknown;
            ReviewQuestions = _isAllOrUnknown ? learnCategory.LearnQuestions : learnCategory.LearnQuestions
                .Where(x => x.IsRepetitions & !x.IsKnown)
                .ToObservableCollection();
           
            CountQuestions = ReviewQuestions.Count;
            //Progress = allOrUnknown ? 0 : learnCategory.LearnQuestions.Count((x) => x.IsRepetitions) / _countQuestions;
            //Progress = 0;
            KnownQuestions = 0;
        }

        public void FindWordsOnRepeat()
        {

           
            
            var questions = _category.LearnQuestions.Where(x => x.IsRepetitions & !x.IsKnown).ToObservableCollection();

            var curentCount = ReviewQuestions.Count;
            var newCount = questions.Count;

            if (curentCount == newCount)
                return ;

            var difference =  Math.Abs(newCount + curentCount);
            CountQuestions +=  difference;
            //ThereAreStillQuestions += difference;

            ReviewQuestions = questions;
     
        }

        public void MoveQuestionToEnd(СardQuestion learnQuestion)
        {
   
            if (!_isAllOrUnknown)
                FindWordsOnRepeat();
  
            //if (isNewReviewQuestions)
            //{
            //    ReviewQuestions.Move(ReviewQuestions.IndexOf(learnQuestion), ReviewQuestions.Count);
            //    return;
            //}

            ReviewQuestions.Remove(learnQuestion);
            ReviewQuestions.Insert(ReviewQuestions.Count, learnQuestion);
           // ReviewQuestions.Move(ReviewQuestions.IndexOf(learnQuestion), ReviewQuestions.Count - 1);
   
           
            //ReviewQuestions.Add(learnQuestion);
        }

        public void DeleteQuestion(СardQuestion learnQuestion)
        {

            if (!_isAllOrUnknown)
            {
                FindWordsOnRepeat();
                learnQuestion.SetQuestionAsAlreadyKnown();

                
                Progress = ++KnownQuestions / CountQuestions;
            }

            ReviewQuestions.Remove(learnQuestion);
           // ReviewQuestions.Add(learnQuestion);     
        }
    }
}
