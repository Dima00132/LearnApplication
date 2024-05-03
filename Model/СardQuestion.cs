using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnApplication.Model.Enum;
using LearnApplication.Model.Web;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace LearnApplication.Model
{
    [Table("learn_question")]
    public partial class СardQuestion : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [Column("learn_category_id")]
        [ForeignKey(typeof(Category))]
        public int LearnCategoryId { get; set; }

        [ObservableProperty]
        private string _question;

        [ObservableProperty]
        private string _answer;

        private UrlWebValid _hyperlink;

        [Column("Hyper_link")]
        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public UrlWebValid Hyperlink
        {
            get => _hyperlink;
            set => SetProperty(ref _hyperlink, value);
        }

        [ObservableProperty]
        private bool _isKnown;

        [ObservableProperty]
        private bool _isRepetitions = true;

        [ObservableProperty]
        public int _numberOfRepetitions;

        public readonly IDispatcherTimer DispatcherTimer;

        public DateTime DateTime { get; set; }

        private readonly NumberRepetition[] _repetitionTimes =
        [
            NumberRepetition.First,NumberRepetition.Second,NumberRepetition.Third,NumberRepetition.Fourth
        ];

        public СardQuestion() : this(string.Empty, string.Empty)
        {
        }

        public СardQuestion(string question, string answer = "", string hyperlink = "")
        {
            Question = question.Trim();
            Answer = answer.Trim();
            Hyperlink = new UrlWebValid(hyperlink.Replace(" ", string.Empty));
            DispatcherTimer = Application.Current?.Dispatcher.CreateTimer();
        }


        public double GetLearningProgress()
        {
            return NumberOfRepetitions / (_repetitionTimes.Length + 1.0);
        }

        public void RestartsTimer()
        {
            if (IsRepetitions || IsKnown)
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
            if (IsKnown)
                return;

            IsRepetitions = false;
            if (!isStartTimer | NumberOfRepetitions >= _repetitionTimes.Length)
            {
                IsKnown = true;
                NumberOfRepetitions++;
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
            Question = question.Trim();
            Answer = answer.Trim();
            Hyperlink.Change(hyperlink.Replace(" ", string.Empty));    
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
