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
        [Column("hyperlink")]
        public string Hyperlink { get; set; }

        [Column("isKnown")]
        public bool IsKnown { get; set; }

        public int NumberOfRepetitions { get; set; } = 0;

        public event EventHandler? TimerTick;

        private readonly NumberRepetition[] _repetitions = 
        [
            NumberRepetition.Test,NumberRepetition.First,NumberRepetition.Second,NumberRepetition.Third
        ];

        public LearnQuestion():this(string.Empty, string.Empty)
        {
        }

        public LearnQuestion(string question, string answer ,string hyperlink = "", bool isKnown = false)
        {
            Question = question;
            Answer = answer;
            Hyperlink = hyperlink;
            IsKnown = isKnown;
            //var dataTime = new DateTime().tim;
        }

       

        public void SetsQuestionAsAlreadyKnown(bool isStartTimer = true)
        {
            IsKnown = true;
            var timeHours = Convert.ToDouble(_repetitions[NumberOfRepetitions]);
            var _dispatcher = Application.Current?.Dispatcher.CreateTimer();

            if (!isStartTimer & NumberOfRepetitions < 4)
                return;
           
            if (_dispatcher is not null)
            {
                NumberOfRepetitions++;
                _dispatcher.Tick += Timer_Tick;
                _dispatcher.Interval = TimeSpan.FromSeconds(20);
                _dispatcher.Start();
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                IsKnown = false;
                TimerTick?.Invoke(this,EventArgs.Empty);
                //_dispatcher.Stop();
            });
            
        }

        public void Change(LearnQuestion learn)
        {
           
            Question = learn.Question;
            Answer = learn.Answer;
            Hyperlink = learn.Hyperlink;
            IsKnown = learn.IsKnown;

            //if (localDbService is null)
            //    return;

            //localDbService.Update(this);
        }

    }
}
