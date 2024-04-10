using CommunityToolkit.Mvvm.ComponentModel;
using LearnApplication.Model.Enum;
using LearnApplication.Service;
using Microsoft.Maui.Dispatching;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Xml.Serialization;

namespace LearnApplication.Model
{
   
    [Table("learn_question")]
    public  class LearnQuestion: IDataSelect
    {

        [PrimaryKey,AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [Column("learn_category_id")]
        [ForeignKey(typeof(LearnCategory))]
        public int LearnCategoryId { get; set; }

        [Column("question")]
        public string Question { get; set; }

        [Column("answer")]
        public string Answer { get; set; }
        [Column("hyper_link")]
        public string Hyperlink { get; set; }

        [Column("is_known")]
        public bool IsKnown { get; private set; } = false;

        [Column("is_repetitions")]
        public bool IsRepetitions { get; set; } = true;

        // [Column("DispatcherTimer")]
        public readonly IDispatcherTimer DispatcherTimer;

        [Column("NumberOfRepetitions")]
        public int NumberOfRepetitions { get; set; } = 0;


        //public event EventHandler? TimerTick;

        private readonly NumberRepetition[] _repetitions = 
        [
            NumberRepetition.Test,NumberRepetition.First,NumberRepetition.Second,NumberRepetition.Third
        ];

        public LearnQuestion():this(string.Empty, string.Empty)
        {
        }

        public LearnQuestion(string question, string answer ,string hyperlink = "")
        {
            Question = question;
            Answer = answer;
            Hyperlink = hyperlink;
            DispatcherTimer = Application.Current?.Dispatcher.CreateTimer();
            DispatcherTimer.Tick += Timer_Tick;
            //var dataTime = new DateTime().tim;
        }

       

        public void SetsQuestionAsAlreadyKnown(bool isStartTimer = true)
        {
            var timeHours = Convert.ToDouble(_repetitions[NumberOfRepetitions]);
            // var _dispatcher = Application.Current?.Dispatcher.CreateTimer();
            IsRepetitions = false;

            if (!isStartTimer & NumberOfRepetitions < 4)
            {
                IsKnown = true;
                return;
            }
           
            if (DispatcherTimer is not null)
            {

                // DispatcherTimer.Tick += Timer_Tick;

                DispatcherTimer.Interval = TimeSpan.FromSeconds(20);
                DispatcherTimer.Start();
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                NumberOfRepetitions++;
                IsRepetitions = true;
                //TimerTick?.Invoke(this,EventArgs.Empty);
                //_dispatcher.Stop();
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
