
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using LearnApplication.Model.Interface;
using System;
using System.Collections.ObjectModel;


namespace LearnApplication.Model
{

    public partial class ReviewQuestion:ObservableObject, IReviewCard
    {
        //public ObservableCollection<CardQuestion> ReviewQuestions { get;set; }


        private ObservableCollection<ICardQuestion> _reviewQuestions;
        public ObservableCollection<ICardQuestion> ReviewQuestions
        {
            get => _reviewQuestions;
            set => SetProperty(ref _reviewQuestions, value);

        }

        private double _progress;
        public double Progress
        {
            get => _progress;
            set => SetProperty(ref _progress, value);
        }

        private  double _countQuestions;
        public double CountQuestions
        {
            get => _countQuestions;
            set => SetProperty(ref _countQuestions, value);
        }

        private  double _knownQuestions;
        public double KnownQuestions
        {
            get => _knownQuestions;
            set => SetProperty(ref _knownQuestions, value);
        }

        public bool IsQuestions { get => ReviewQuestions.Count >1; }

        //private readonly Subject _subject;
        private readonly bool _isAllOrUnknown;
        private readonly ObservableCollection<ICardQuestion> source = [];
        public ReviewQuestion(ObservableCollection<ICardQuestion> questions, bool allOrUnknown = true)
        {
            source = questions;
            _isAllOrUnknown = allOrUnknown;
            //ReviewQuestions = _isAllOrUnknown ? subject.LearnQuestions : subject.LearnQuestions
            //    .Where(x => x.OnRepetition & !x.Known)
            //    .ToObservableCollection();

            ReviewQuestions = _isAllOrUnknown ? questions : GetReviewQuestions();
            CountQuestions = ReviewQuestions.Count;
            KnownQuestions = 0;
        }

        private ObservableCollection<ICardQuestion> GetReviewQuestions()
        {
            var random = new Random();
            return source
                .Where(x => x.IsRepetitions)
                .ToObservableCollection();
        }

        private void FindWordsOnRepeat()
        {



            // questions = _subject.LearnQuestions.Where(x => x.OnRepetition & !x.Known).ToObservableCollection();
            var questions = GetReviewQuestions();

            var curentCount = ReviewQuestions.Count;
            var newCount = questions.Count;

            if (curentCount == newCount)
                return ;

            var difference =  Math.Abs(newCount + curentCount);
            CountQuestions +=  difference;
            //ThereAreStillQuestions += difference;

            ReviewQuestions = questions;
     
        }

        public void MoveToEnd(ICardQuestion learnQuestion)
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

        public void Delete(ICardQuestion learnQuestion)
        {

            if (!_isAllOrUnknown)
            {
                FindWordsOnRepeat();
                learnQuestion.SetAsRepeated();
                Progress = ++KnownQuestions / CountQuestions;
            }

            ReviewQuestions.Remove(learnQuestion);  
        }
    }
}
