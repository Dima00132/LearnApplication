using CommunityToolkit.Maui.Converters;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using LearnApplication.Service;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace LearnApplication.Model
{

    [Table("learn_category")]
    public partial class Category:ObservableObject
    {
        [PrimaryKey,AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [ObservableProperty]
        private string _subject;

        [Column("learn_id")]
        [ForeignKey(typeof(Learn))]
        public int LearnId { get; set; }

        [Column("count_dont_known")]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<СardQuestion> LearnQuestions { get; set; } = [];

        public DateTime LastActivity { get; set; }

        public Category():this(string.Empty)
        {}
        public Category(string subject)
        {
            Subject = subject;
        }

        public string MemorizationPercentage
        {
            get
            {
                if (LearnQuestions.Count == 0)
                    return "0";
                double know = LearnQuestions.Sum(x=>x.GetLearningProgress());
                double count = LearnQuestions.Count;


                var percentage = know * 100.0 / count;
                var percentageStr = Math.Round(percentage,1).ToString();

            
                return percentageStr;
            }
            set { }
        }


        //public double RepetitionsQuestionsCount => LearnQuestions.Count(x=>x.IsRepetitions & !x.IsKnown);
        public double RepetitionsQuestionsCount => LearnQuestions.Count(x => x.IsRepetitions);
        public double KnownCountLearn => LearnQuestions.Count(x => x.IsKnown);

        //public double KnownCountLearn => LearnQuestions.Count(x => x.IsKnown & x.NumberOfRepetitions > x.CountRepetitions);

        public int CountQuestion => LearnQuestions.Count;

        public int CountDontKnown { get; private set; } = 0;

        public IEnumerable<СardQuestion> FindsQuestionByRequest(string request)
        {
            var learnQuestions = LearnQuestions;
            var result = learnQuestions
                .Where(x => x.Question.Length >= request.Length)
                .Where(x => String.Compare(x.Question, 0, request, 0, request.Length, StringComparison.OrdinalIgnoreCase) == 0);
            return result;
        }

        public ReviewQuestion GetReviewQuestions(bool allOrUnknown = true)
        {
            return new ReviewQuestion(this, allOrUnknown);
        }

        public void AddQuestion(СardQuestion question)
        {
            if (question is null)
                return;
            LearnQuestions.Insert(0,question);
        }

        public void RemoveQuestion(СardQuestion question)
        {
            if (question is null)
                return;
            LearnQuestions.Remove(question);
        }
        
        private void SortDescendingById()
        {
            LearnQuestions = LearnQuestions.OrderByDescending(x => x.Id).ToObservableCollection();
        }

        public void RestartsTheTimer(bool sortById = true)
        {
            if (sortById)
                SortDescendingById();
            foreach (var item in LearnQuestions)
                item.RestartsTimer();
           
        }
    }
}
