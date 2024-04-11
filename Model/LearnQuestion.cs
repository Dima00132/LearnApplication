using CommunityToolkit.Mvvm.ComponentModel;
using LearnApplication.Model.Enum;
using LearnApplication.Service;
using Microsoft.Maui.Dispatching;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Xml.Serialization;

namespace LearnApplication.Model
{
   
    [Table("learn_question")]
    public partial class LearnQuestion:ObservableObject, IDataSelect
    {

        [PrimaryKey,AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [Column("learn_category_id")]
        [ForeignKey(typeof(LearnCategory))]
        public int LearnCategoryId { get; set; }

        // [Column("question")]

        [ObservableProperty]
        private string _question;

        // [Column("answer")]

        [ObservableProperty]
        private string _answer;


        // [Column("hyper_link")]

        [ObservableProperty]
        private string _hyperlink;

        // [Column("is_known")]


        [ObservableProperty]
        private bool _isKnown  = false;

        // [Column("is_repetitions")]


        [ObservableProperty]
        private bool _isRepetitions  = true;

        // [Column("DispatcherTimer")]
        public readonly IDispatcherTimer DispatcherTimer;

        
        public DateTime DateTime { get;set; }

       
        public int NumberOfRepetitions { get; set; }


        //public event EventHandler? TimerTick;

        private readonly NumberRepetition[] _repetitions = 
        [
            NumberRepetition.Test,NumberRepetition.First,NumberRepetition.Second,NumberRepetition.Third
        ];

        //public  LocalDbService _localDbService { get; set; }
        //public  LearnCategory learnCategory { get; set; }

        public LearnQuestion():this(string.Empty, string.Empty)
        {
        }

        public LearnQuestion(string question, string answer , string hyperlink = "")
        {
            Question = question;
            Answer = answer;
            //_localDbService = localDbService;
            //this.learnCategory = learnCategory;
            Hyperlink = hyperlink;
            DispatcherTimer = Application.Current?.Dispatcher.CreateTimer();
       
            //var dataTime = new DateTime().tim;
        }

        public void RestartsTheTimer()
        {
            if (IsRepetitions)
                return;

            var carentDate = DateTime;
            var newDate = DateTime.Now;
            var resald = newDate - carentDate;
            var timeHours = Convert.ToDouble(_repetitions[NumberOfRepetitions]);
            var oldTimeSpan = TimeSpan.FromSeconds(timeHours);

            

            if (resald >= oldTimeSpan)
            {
                NumberOfRepetitions++;
                IsRepetitions = true;
                return;
            }
            var newTimeSpan = oldTimeSpan - resald;

            StartTimer(newTimeSpan);
            //if (DispatcherTimer is not null)
            //{

            //    // DispatcherTimer.Tick += Timer_Tick;

            //    DispatcherTimer.Interval = newTimeSpan;
            //    DispatcherTimer.Start();
            //}

        }

       

        public void SetsQuestionAsAlreadyKnown(bool isStartTimer = true)
        {
            if (!isStartTimer & NumberOfRepetitions < 4)
            {
                IsKnown = true;
                return;
            }
            DateTime = DateTime.Now;
            IsRepetitions = false;
            var timeHours = Convert.ToDouble(_repetitions[NumberOfRepetitions]);
            //var timeSpan = TimeSpan.FromHours(timeHours);
            var timeSpan = TimeSpan.FromSeconds(timeHours);
            StartTimer(timeSpan);

        }

        private void StartTimer(TimeSpan timeSpan)
        {
            if (DispatcherTimer is not null)
            {
                DispatcherTimer.Tick += Timer_Tick;
                DispatcherTimer.Interval = timeSpan;
                DispatcherTimer.Start();
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                NumberOfRepetitions++;
                IsRepetitions = true;
               
                DispatcherTimer.Tick -= Timer_Tick;
               //_localDbService.Update(learnCategory);
            });
            
        }

        public void Change(LearnQuestion learn)
        {
           
            Question = learn.Question;
            Answer = learn.Answer;
            Hyperlink = learn.Hyperlink;
        }

    }
}
