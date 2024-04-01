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
    public partial class LearnCategory: IDataSelect
    {
        [PrimaryKey,AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [Column("subject")]
        public string Subject { get; set; }

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
            //CountDontKnown =  (int)DointKnownCountLearn;
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
        public double DointKnownCountLearn => LearnQuestions.Count - KnownCountLearn;

        public double KnownCountLearn => LearnQuestions.Count(x => x.IsKnown);

        public int CountQuestion => LearnQuestions.Count;

        public int CountDontKnown { get; private set; } = 0;

        public ReviewQuestion GetReviewQuestions(bool allOrUnknown = true)
        {
            var questions = new List<LearnQuestion>(LearnQuestions);
            return new ReviewQuestion(questions, allOrUnknown);
        }


        public void AddLearnQuestion(LearnQuestion learn)
        {
            if (learn is null)
                return;

            LearnQuestions.Add(learn);

            //if (localDbService is not null)
            //{
            //    localDbService.Create(learn);
            //    localDbService.Update(this);
            //}


            learn.TimerTick += Timer_Tick;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
               // CountDontKnown = (int)ProgressLearn.DointKnownCountLearn;
            });
            
        }
    }
}
