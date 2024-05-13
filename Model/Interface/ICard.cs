using System.ComponentModel;

namespace LearnApplication.Model.Interface
{
    public interface ICard : INotifyPropertyChanged, ICardDb
    {
        string NameCard { get; }
    }
}
