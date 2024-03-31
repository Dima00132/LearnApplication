using CommunityToolkit.Mvvm.ComponentModel;
using LearnApplication.Model.Enum;
using Microsoft.Maui.Dispatching;



namespace LearnApplication.Model
{
    [Serializable]
    public partial class LearnQuestion:ObservableObject
    {
        [ObservableProperty]
        private string _question;

        [ObservableProperty]
        private string _answer;
        [ObservableProperty]
        private string _hyperlink;

        [ObservableProperty]
        private bool _isKnown;

        [ObservableProperty]
        public int _numberOfRepetitions = 0;

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
        }

    }
}
