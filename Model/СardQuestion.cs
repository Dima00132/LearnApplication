using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnApplication.Model.Enum;
using LearnApplication.Model.Interface;
using LearnApplication.Model.Web;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.ComponentModel;
using static SQLite.SQLite3;

namespace LearnApplication.Model
{


    [Table("learn_question")]
    public partial class CardQuestion : ObservableObject, ICardQuestion
    {
        private readonly IDispatcherTimer DispatcherTimer = Application.Current?.Dispatcher.CreateTimer();
        public string NameCard => "Вопросы";

        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [Column("learn_category_id")]
        [ForeignKey(typeof(Subject))]
        public int LearnCategoryId { get; set; }

        private string _question;
        public string Question
        {
            get => _question;
            set
            {
                SetProperty(ref _question, value);
            }
        }

        private string _answer;
        public string Answer
        {
            get => _answer;
            set => SetProperty(ref _answer, value);
        }

        private UrlWebValid _hyperlink;
        [Column("Hyper_link")]
        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public UrlWebValid Hyperlink
        {
            get => _hyperlink;
            set => SetProperty(ref _hyperlink, value);
        }

        private bool _isKnown;
        public bool IsKnown
        {
            get => _isKnown;
            set
            {
                if (value != _isKnown)
                {
                    var propertyCommand = value ? NamePropertyCommand.Known : NamePropertyCommand.DontKnown;
                    OnPropertyChanged(propertyCommand.ToString());
                }
                SetProperty(ref _isKnown, value);
            }
        }

        private bool _isRepetitions;
        public bool IsRepetitions
        {
            get => _isRepetitions;
            set
            {
                if (value != _isRepetitions)
                {
                    var propertyCommand = value ? NamePropertyCommand.OnRepetition : NamePropertyCommand.OnWaitingRepeat;
                    OnPropertyChanged(propertyCommand.ToString());
                }
                SetProperty(ref _isRepetitions, value);
            }
        }

        public int _numberOfRepetitions;
        public int NumberOfRepetitions
        {
            get => _numberOfRepetitions;
            set
            {
                SetProperty(ref _numberOfRepetitions, value);
                OnPropertyChanged(nameof(NamePropertyCommand.RepetitionNumber));
            }
        }

        public DateTime DateTime { get; set; }

        private int _currentCountRepetitions;
        public int CurrentCountRepetitions
        {
            get => _currentCountRepetitions;
            set
            {
                SetProperty(ref _currentCountRepetitions, value);
                OnPropertyChanged(nameof(NamePropertyCommand.CountRepetitions));
            }
        }
        public Dictionary<int, NumberRepetition> RepetitionTimesDictionary { get; } = new()
        {
            [1] = NumberRepetition.First,[2] = NumberRepetition.Second,[3] = NumberRepetition.Third,
            [4] = NumberRepetition.Fourth,[5] = NumberRepetition.Fifth,[6] = NumberRepetition.Sixth
        };

        public CardQuestion(){}

        public CardQuestion(string question,int countRepetitions, string answer = "", string hyperlink = "" )
        {
            Question = question.Trim();
            Answer = answer.Trim();
            Hyperlink = new UrlWebValid(hyperlink.Replace(" ", string.Empty));
            //DispatcherTimer = Application.Current?.Dispatcher.CreateTimer();
            CurrentCountRepetitions = countRepetitions;
            _isRepetitions = true;
        }

        public CardQuestion ChangeQuestion(string question)
        {
            var questionTrim = question.Trim();
            if (questionTrim.Equals(Question))
                return this;
            Question = questionTrim;
            return this;
        }

        public CardQuestion ChangeAnswer(string answer)
        {
            var answerTrim = answer.Trim();
            if (answerTrim.Equals(Answer))
                return this;
            Answer = answerTrim;
            return this;
        }

        public CardQuestion ChangeHyperlink(string hyperlink)
        {
            var link = hyperlink.Replace(" ", string.Empty);
            if(link.Equals(Hyperlink.Url))
                return this;
            Hyperlink.Change(link);
            return this;
        }
        public double GetStudyProgress()
            =>NumberOfRepetitions / (CurrentCountRepetitions + 1.0);
        private TimeSpan GetCurrentTimeSpan()
        { 
            if (!RepetitionTimesDictionary.TryGetValue(NumberOfRepetitions, out NumberRepetition value))
                return new TimeSpan() ; 
            var timeHours = Convert.ToDouble(value);
            ////var timeSpan = TimeSpan.FromHours(timeHours);
            return TimeSpan.FromSeconds(timeHours);
        }

        private bool CheckingForTimerExpiration(TimeSpan currenrResald,TimeSpan oldTimeSpan)
        {
            if (currenrResald >= oldTimeSpan)
                return IsRepetitions = true;
            return false;
        }

        private bool CheckingCompletionOfTraining(int currentNumberOfRepetitions,int currentCountRepetitions)
        {
            if (currentNumberOfRepetitions > currentCountRepetitions)
                return IsKnown = true;
            return false;
        }
        public void RestartsTimer()
        {
            if (IsRepetitions | IsKnown)
                return;
            var currenrResald = DateTime.Now - DateTime;
            var oldTimeSpan = GetCurrentTimeSpan();
            if (CheckingForTimerExpiration(currenrResald, oldTimeSpan))
                return;
            var newTimeSpan = oldTimeSpan - currenrResald;
            StartTimer(newTimeSpan);
        }

        public void SetAsRepeated()
        {
            if (IsKnown)return;
            NumberOfRepetitions++;
            IsRepetitions = false;
            if (CheckingCompletionOfTraining(NumberOfRepetitions, CurrentCountRepetitions))
                return;
            //if (NumberOfRepetitions > CurrentCountRepetitions)
            //{
            //    IsKnown = true;
            //    return;
            //}
            DateTime = DateTime.Now;
            var timeSpan = GetCurrentTimeSpan();
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
                DateTime = DateTime.Now;
                IsRepetitions = true;
                IsKnown = false;
                DispatcherTimer.Tick -= Timer_Tick;
            });
        }
        public void ChangeNumberOfRepetitions(int numberOfRepetitions)
        { 
            CurrentCountRepetitions = numberOfRepetitions;
            if (CurrentCountRepetitions < NumberOfRepetitions)
                NumberOfRepetitions = CurrentCountRepetitions;
        }
    }  
}
