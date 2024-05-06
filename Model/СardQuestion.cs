using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnApplication.Model.Enum;
using LearnApplication.Model.Web;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace LearnApplication.Model
{



    [Table("learn_question")]
    public partial class CardQuestion : ObservableObject
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

        //public static int CountRepetitions { get; set; } = 4;

        private int _countRepetitions;
        public int CountRepetitions
        {
            get => _countRepetitions;
            set
            {
                var maxCountRepetitions = _repetitionTimes.Length;
                if (value > maxCountRepetitions | value < 2)
                {
                    _countRepetitions = maxCountRepetitions;
                    return;
                }
                _countRepetitions = value;
                SetProperty(ref _countRepetitions, value);
            }
        }

        private readonly NumberRepetition[] _repetitionTimes =
        [
            NumberRepetition.First,NumberRepetition.Second,NumberRepetition.Third,
            NumberRepetition.Fourth , NumberRepetition.Fifth , NumberRepetition.Sixth
        ];
        
        public CardQuestion() : this(string.Empty,4, string.Empty)
        {
        }

        public CardQuestion(string question,int countRepetitions, string answer = "", string hyperlink = "" )
        {
            Question = question.Trim();
            Answer = answer.Trim();
            Hyperlink = new UrlWebValid(hyperlink.Replace(" ", string.Empty));
            DispatcherTimer = Application.Current?.Dispatcher.CreateTimer();
            CountRepetitions = countRepetitions;
        }


        public double GetLearningProgress()
        {
            return NumberOfRepetitions / (CountRepetitions + 1.0);
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

        public void SetQuestionAsAlreadyKnown()
        {
            if (IsKnown)
                return;

            NumberOfRepetitions++;
            IsRepetitions = false;
            if (NumberOfRepetitions > CountRepetitions)
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
