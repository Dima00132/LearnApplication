using CommunityToolkit.Mvvm.ComponentModel;


namespace LearnApplication.Model
{
    [Serializable]
    public partial class ProgressLearn : ObservableObject
    {
        private readonly List<LearnQuestion> _learnQuestions;

        private double _countProgressLearn;
        public double CountProgressLearn
        {
            get => _countProgressLearn;
            set
            {
                _countProgressLearn = CountProgress();
                SetProperty(ref _countProgressLearn, value);
            }
        }

        public ProgressLearn(List<LearnQuestion> learnQuestions)
        {
            _learnQuestions = learnQuestions;
        }

        public ProgressLearn()
        {
        }

        private double CountProgress()
        {
            double count = _learnQuestions.Count;
            double Known = _learnQuestions.Count(x => x.IsKnown);
            return Known / count;
        }

        public double DointKnownCountLearn()
        {
            return _learnQuestions.Count - KnownCountLearn();
        }

        public double KnownCountLearn()
        {
            return _learnQuestions.Count(x => x.IsKnown);
        }

        public int CountQuestion()
        {
            return _learnQuestions.Count();
        }


    }
}
