using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


namespace LearnApplication.Model
{
    [Serializable]
    public partial class LearnCategory : ObservableObject
    {

        [ObservableProperty]
        private string _subject;

        [ObservableProperty]
        private int _testCountDontKnown = 0;


        public ObservableCollection<LearnQuestion> LearnQuestions { get; set; } = [];

        public LearnCategory():this(string.Empty)
        {}
        public LearnCategory(string subject)
        {
            Subject = subject;
            TestCountDontKnown =  (int)DointKnownCountLearn();
        }

        public double CountProgressLearn()
        {
            double count = LearnQuestions.Count;
            double Known = LearnQuestions.Count(x => x.IsKnown);
            return Known / count;
        }

        public double DointKnownCountLearn()=>LearnQuestions.Count - KnownCountLearn();
       

        public double KnownCountLearn()=>LearnQuestions.Count(x => x.IsKnown);
     

        public int CountQuestion()=>LearnQuestions.Count;

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
            learn.TimerTick += Timer_Tick;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TestCountDontKnown = (int)DointKnownCountLearn();
            });
            
        }
    }
}
