using CommunityToolkit.Maui.Converters;
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

        public DateTime LastEntrance { get; set; }

        //[Column("progress_learn")]
        //public ProgressLearn ProgressLearn { get; set; }

        public Category():this(string.Empty)
        {}
        public Category(string subject)
        {
           
            Subject = subject;
            //ProgressLearn = new ProgressLearn(LearnQuestions);
            //CountDontKnown =  (int)DontKnownCountLearn;
        }

        public string MemorizationPercentage
        {
            get
            {
                if (LearnQuestions.Count == 0)
                    return "0";
                double know = LearnQuestions.Count(x => x.IsKnown);
                double count = LearnQuestions.Count;


                var percentage = know * 100.0 / count;
                var percentageStr = Math.Round(percentage,1).ToString();

               // var percentage = LearnQuestions.Count == 0? 0 :(LearnQuestions.Count(x => x.IsKnown) * 100) / LearnQuestions.Count;
                return percentageStr;
            }
            set { }
        }


        public double CountProgressLearn
        {
            get
            {
                double count = LearnQuestions.Count;
                double Known = LearnQuestions.Count(x => x.IsKnown);
                return Known / count;
            }
        }
        public double DontKnownCountLearn => LearnQuestions.Count - KnownCountLearn;

        public double RepetitionsCount => LearnQuestions.Count(x=>x.IsRepetitions);

        public double KnownCountLearn => LearnQuestions.Count(x => x.IsKnown & x.NumberOfRepetitions == 3);

        public int CountQuestion => LearnQuestions.Count;

        public int CountDontKnown { get; private set; } = 0;

        public ReviewQuestion GetReviewQuestions(bool allOrUnknown = true)
        {
            //var questions = new List<СardQuestion>(LearnQuestions);
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

        public void RestartsTheTimer()
        {
            foreach (var item in LearnQuestions)
                item.RestartsTimer();
            
        }
    }
}
