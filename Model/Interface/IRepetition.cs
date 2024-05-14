using LearnApplication.Model.Enum;

namespace LearnApplication.Model.Interface
{
    public interface IRepetition
    {
        bool IsKnown { get; set; }
        bool IsRepetitions { get; set; }
        int NumberOfRepetitions { get; set; }
        int CurrentCountRepetitions { get; }
        Dictionary<int, NumberRepetition> RepetitionTimesDictionary { get; }
        void SetAsRepeated();
        double GetStudyProgress();
    }
}
