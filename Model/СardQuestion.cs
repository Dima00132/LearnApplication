using CommunityToolkit.Mvvm.ComponentModel;
using LearnApplication.Model.Enum;
using LearnApplication.Service;
using Microsoft.Maui.Dispatching;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Net;
using System.Xml.Serialization;
using static System.Net.WebRequestMethods;

namespace LearnApplication.Model
{

    public sealed class UrlWebValid
    {
        private readonly UrlWebViewSource _urlWebViewSource;
        public readonly bool IsUrlValid;

        public UrlWebValid(UrlWebViewSource urlWebViewSource)
        {
            IsUrlValid = CheckNet.Check(urlWebViewSource.Url);
            _urlWebViewSource = urlWebViewSource;
        }

        public UrlWebViewSource GetUrl()
        {
            if (!IsUrlValid)
                _urlWebViewSource.Url = "https://www.exai.com/blog/400-bad-request-error";
            return _urlWebViewSource;
        }
    }

    [Table("learn_question")]
    public partial class СardQuestion:ObservableObject
    {
        [PrimaryKey,AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [Column("learn_category_id")]
        [ForeignKey(typeof(Category))]
        public int LearnCategoryId { get; set; }

        [ObservableProperty]
        private string _question;

        [ObservableProperty]
        private string _answer;

        [ObservableProperty]
        private string _hyperlink;

        [ObservableProperty]
        private bool _isKnown;

        [ObservableProperty]
        private bool _isRepetitions  = true;

        [ObservableProperty]
        public int _numberOfRepetitions;

        public readonly IDispatcherTimer DispatcherTimer;
        public DateTime DateTime { get;set; }

        private readonly NumberRepetition[] _repetitionTimes = 
        [
            NumberRepetition.Test,NumberRepetition.First,NumberRepetition.Second,NumberRepetition.Third
        ];

        public СardQuestion():this(string.Empty, string.Empty)
        {
        }

        public СardQuestion(string question, string answer = "", string hyperlink = "")
        {
            Question = question;
            Answer = answer;
            Hyperlink = hyperlink;
            DispatcherTimer = Application.Current?.Dispatcher.CreateTimer();
        }

        public double GetLearningProgress()
        {
            return NumberOfRepetitions / (double)_repetitionTimes.Length;
        }

        public void RestartsTimer()
        {
            if (IsRepetitions)
                return;
            var resald = DateTime.Now - DateTime;
            var timeHours = Convert.ToDouble(_repetitionTimes[NumberOfRepetitions]);
            var oldTimeSpan = TimeSpan.FromSeconds(timeHours);

            if (resald >= oldTimeSpan)
            {
                NumberOfRepetitions++;
                IsRepetitions = true;
                return;
            }
            var newTimeSpan = oldTimeSpan - resald;

            StartTimer(newTimeSpan);
        }

        public void SetQuestionAsAlreadyKnown(bool isStartTimer = true)
        {
            IsRepetitions = false;
            if (!isStartTimer | NumberOfRepetitions == 4)
            {
                IsKnown = true;
                return;
            }
            DateTime = DateTime.Now;
       
            var timeHours = Convert.ToDouble(_repetitionTimes[NumberOfRepetitions]);
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

        public void Change(string question,string answer = "", string hyperlink = "")
        { 
            Question = question;
            Answer = answer;
            Hyperlink = hyperlink;
        }

        public UrlWebValid GetUrlWebView()
        {
            var url = new UrlWebViewSource()
            {
                Url = Hyperlink 
            };
            return new UrlWebValid(url);
        }
        private void Timer_Tick(object? sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                NumberOfRepetitions++;
                IsRepetitions = true;
                DispatcherTimer.Tick -= Timer_Tick;
            });
        }

    }
}
