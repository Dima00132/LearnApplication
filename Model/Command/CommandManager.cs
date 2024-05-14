using LearnApplication.Model.Enum;
using LearnApplication.Model.Interface;
using System.Net.Http.Headers;

namespace LearnApplication.Model.Command
{


            //    [NamePropertyCommand.OnRepetition] = (rep, sub) => new IncreaseRepetitionsCommand(sub).Execute(),
            //[NamePropertyCommand.OnWaitingRepeat] = (rep, sub) => new DecreaseRepetitionsCommand(sub).Execute(),
            //[NamePropertyCommand.RecalculationRepetitions] = (rep, sub) => new RecalculationRepetitionsCommand(sub).Execute(),
            //[NamePropertyCommand.RecalculationKnow] = (rep, sub) => new RecalculationKnowCommand(sub).Execute(),
            //[NamePropertyCommand.Known] = (rep, sub) => new IncreaseKnowCountCommand(sub).Execute(),
            //[NamePropertyCommand.DontKnown] = (rep, sub) => new DecreaseKnowCountCommand(sub).Execute(),

    public static class CommandManager
    {
        private static Dictionary<NamePropertyCommand, Action<IRepetition, Subject>> _command = new()
        {
            [NamePropertyCommand.RepetitionNumber] = (rep, sub) =>new ChangeRepetitionNumberCommand(sub).Execute(),
            [NamePropertyCommand.Insert] = (rep, sub) => new InsertCommand(sub, rep).Execute(),
            [NamePropertyCommand.Remove] = (rep, sub) => new RemoveCommand(sub, rep).Execute()
        };

        private static Dictionary<NamePropertyCommand, Action<ICardQuestion, Subject>> _commandWithUpdateDb = new()
        {
            [NamePropertyCommand.OnRepetition] = (x, y) => new IncreaseRepetitionsCommand(y, x).Execute(),
            [NamePropertyCommand.OnWaitingRepeat] = (x, y) => new DecreaseRepetitionsCommand(y, x).Execute(),
            [NamePropertyCommand.Known] = (x, y) => new IncreaseKnowCountCommand(y, x).Execute(),
            [NamePropertyCommand.DontKnown] = (x, y) => new DecreaseKnowCountCommand(y, x).Execute(),
 
        };


        //private static void SetPropertyValueWithUpdateDb(NamePropertyCommand propertyCommand, ICardQuestion question, Subject category)
        //{
        //    _commandWithUpdateDb[propertyCommand].Invoke(question, category);
        //}

        private static NamePropertyCommand GetPropertyCommand(string nameProperty)
            => (NamePropertyCommand)System.Enum.Parse(typeof(NamePropertyCommand), nameProperty, true);

        private static bool CheckIfPropertyExists(string nameProperty)
            => !System.Enum.IsDefined(typeof(NamePropertyCommand), nameProperty);




        public static void SetPropertyValue(string nameProperty, IRepetition questionRepetition, Subject category)
        {
            if (CheckIfPropertyExists(nameProperty))
                return;
            var propertyCommand = GetPropertyCommand(nameProperty);

            if ( _commandWithUpdateDb.ContainsKey(propertyCommand) && questionRepetition is ICardQuestion question)
            {
                _commandWithUpdateDb[propertyCommand].Invoke(question, category);
                return;
            }

            if (_command.ContainsKey(propertyCommand))
                _command[propertyCommand].Invoke(questionRepetition, category);
        }

        public static void AddPropertyCommand(NamePropertyCommand nameCommand, Action<IRepetition, Subject> action)
            => _command.Add(nameCommand, action);

        //private static double RereadStudyProgres(Subject category)
        //{
        //    if (category.LearnQuestions.Count == 0)
        //        return 0;
        //    double learningProgress = category.LearnQuestions.Sum(x => x.GetStudyProgress());
        //    return CalculatesStudyPercentage(learningProgress, category.LearnQuestions.Count);
        //}

        //private static double CalculatesStudyPercentage(double learningProgress, double countQuestions)
        //{
        //    var percentage = learningProgress * 100.0 / countQuestions;
        //    var percentageInTenths = Math.Round(percentage, 1);
        //    return percentageInTenths;
        //}
        //private static double AddsProgress(int countQuestions, IRepetition question)
        //{
        //    double learningProgress = question.GetStudyProgress();
        //    return CalculatesStudyPercentage(learningProgress, countQuestions);
        //}
    }
}
