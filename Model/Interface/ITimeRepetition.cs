namespace LearnApplication.Model.Interface
{
    public interface ITimeRepetition
    {
        public DateTime DateTime { get; set; }
        void RestartsTimer();
    }
}
