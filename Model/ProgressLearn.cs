using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.ObjectModel;


namespace LearnApplication.Model {

    [Table("ProgressLearn")]
    public class ProgressLearn
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Column("learn_question_id")]
        [ForeignKey(typeof(LearnQuestion))]
        public int LearnQuestionId { get; set; }

        private readonly ObservableCollection<LearnQuestion> _learnQuestions;
        public ProgressLearn(ObservableCollection<LearnQuestion> learnQuestions)
        {
            _learnQuestions = learnQuestions;
        }

        public ProgressLearn() : this([])
        {
        }

        public double CountProgress
        {
            get {
                double count = _learnQuestions.Count;
                double Known = _learnQuestions.Count(x => x.IsKnown);
                return Known / count;
            }
        }

        public double DointKnownCountLearn => _learnQuestions.Count - KnownCountLearn;


        public double KnownCountLearn => _learnQuestions.Count(x => x.IsKnown);


        public int CountQuestion => _learnQuestions.Count;
    }
}
