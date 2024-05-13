using LearnApplication.Model.Enum;

namespace LearnApplication.Model.Interface
{
    public interface IRepetition
    {
        bool IsKnown { get; set; }
        bool IsRepetitions { get; set; }
        int NumberOfRepetitions { get; set; }
        int CurrentCountRepetitions { get; set; }
        Dictionary<int, NumberRepetition> RepetitionTimesDictionary { get; }
        void ChangeNumberOfRepetitions(int numberOfRepetitions);
        void SetAsRepeated();
        double GetStudyProgress();
    }
}
