namespace LearnApplication.Model.Enum
{
    [Flags]
    public enum NamePropertyCommand
    {
        OnRepetition,
        RepetitionNumber,
        Known,
        CountRepetitions,
        Insert,
        Remove,
        OnWaitingRepeat,
        DontKnown,
        RecalculationRepetitions,
        RecalculationKnow,
    }
}
