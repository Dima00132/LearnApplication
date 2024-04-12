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
    public partial class LearnCategory:ObservableObject, IDataSelect
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
        public ObservableCollection<LearnQuestion> LearnQuestions { get; set; } = [];

        //[Column("progress_learn")]
        //public ProgressLearn ProgressLearn { get; set; }

        public LearnCategory():this(string.Empty)
        {}
        public LearnCategory(string subject)
        {
           
            Subject = subject;
            //ProgressLearn = new ProgressLearn(LearnQuestions);
            //CountDontKnown =  (int)DontKnownCountLearn;
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
            //var questions = new List<LearnQuestion>(LearnQuestions);
            return new ReviewQuestion(this, allOrUnknown);
        }

        public void AddLearnQuestion(LearnQuestion learn)
        {
            if (learn is null)
                return;

            LearnQuestions.Add(learn);
            //learn.DispatcherTimer.Tick += Timer_Tick;
        }

        public void StartTimer()
        {
            foreach (var item in LearnQuestions)
                item.RestartsTheTimer();
            
        }


        //public event EventHandler? TimerTick;

        //private void Timer_Tick(object? sender, EventArgs e)
        //{
        //    MainThread.BeginInvokeOnMainThread(() =>
        //    {
        //        var g = 0;
        //        TimerTick?.Invoke(sender, EventArgs.Empty);
        //       //CountDontKnown = (int)ProgressLearn.DontKnownCountLearn;
        //    });
            
        //}
    }
}
