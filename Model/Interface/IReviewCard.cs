using System.Collections.ObjectModel;
using System.ComponentModel;


namespace LearnApplication.Model.Interface
{
    public interface IReviewCard :INotifyPropertyChanged
    {
        ObservableCollection<ICardQuestion> ReviewQuestions { get; set; }
        double CountQuestions { get; set; }
        double Progress { get; set; }
        double KnownQuestions { get; set; }
        bool IsQuestions { get; }
        void MoveToEnd(ICardQuestion learnQuestion);
        void Delete(ICardQuestion learnQuestion);
    }
}
