using LearnApplication.Model.Enum;
using LearnApplication.Model.Interface;

namespace LearnApplication.Model.Command
{
    public static class CommandManager
    {
        private static Dictionary<NamePropertyCommand, Action<IRepetition, Subject>> _command = new()
        {
            [NamePropertyCommand.OnRepetition] = (x, y) =>new IncreaseRepetitionsCommand(y).Execute(),
            [NamePropertyCommand.OnWaitingRepeat] = (x, y) =>new DecreaseRepetitionsCommand(y).Execute(),
            [NamePropertyCommand.RecalculationRepetitions] = (x,y) => new RecalculationRepetitionsCommand(y).Execute(),
            [NamePropertyCommand.RecalculationKnow] = (x, y) => new RecalculationKnowCommand(y).Execute(),
            [NamePropertyCommand.Known] = (x, y) =>new IncreaseKnowCountCommand(y).Execute(),
            [NamePropertyCommand.DontKnown] = (x, y) =>new DecreaseKnowCountCommand(y).Execute(),
            [NamePropertyCommand.CountRepetitions ] = (x, y) => y.MemorizationPercentage = RereadStudyProgres(y),
            [NamePropertyCommand.RepetitionNumber] = (x, y) => y.MemorizationPercentage = RereadStudyProgres(y),
            [NamePropertyCommand.Insert] = (x, y) => y.MemorizationPercentage += AddsProgress(y.LearnQuestions.Count, x),
            [NamePropertyCommand.Remove] = (x, y) => y.MemorizationPercentage -= AddsProgress(y.LearnQuestions.Count, x)
        };

        private static Dictionary<NamePropertyCommand, Action<ICardQuestion, Subject>> _commandWithUpdateDb = new()
        {
            [NamePropertyCommand.OnRepetition] = (x, y) => new IncreaseRepetitionsCommand(y, x).Execute(),
            [NamePropertyCommand.OnWaitingRepeat] = (x, y) => new DecreaseRepetitionsCommand(y, x).Execute(),
            [NamePropertyCommand.Known] = (x, y) => new IncreaseKnowCountCommand(y, x).Execute(),
            [NamePropertyCommand.DontKnown] = (x, y) => new DecreaseKnowCountCommand(y, x).Execute(),
 
        };


        private static void SetPropertyValueWithUpdateDb(NamePropertyCommand propertyCommand, ICardQuestion question, Subject category)
        {
            _commandWithUpdateDb[propertyCommand].Invoke(question, category);
        }

        private static NamePropertyCommand GetPropertyCommand(string nameProperty)
            => (NamePropertyCommand)System.Enum.Parse(typeof(NamePropertyCommand), nameProperty, true);

        private static bool CheckIfPropertyExists(string nameProperty)
            => !System.Enum.IsDefined(typeof(NamePropertyCommand), nameProperty);




        public static void SetPropertyValue(string nameProperty, IRepetition questionRepetition, Subject category,bool updateDb = true)
        {
            if (CheckIfPropertyExists(nameProperty))
                return;
            var propertyCommand = GetPropertyCommand(nameProperty);

            if (updateDb & _commandWithUpdateDb.ContainsKey(propertyCommand))
            {
                var question = questionRepetition as ICardQuestion;
                SetPropertyValueWithUpdateDb(propertyCommand, question, category);
                return;
            }


           _command[propertyCommand].Invoke(questionRepetition, category);
        }

        public static void AddPropertyCommand(NamePropertyCommand nameCommand, Action<IRepetition, Subject> action)
            => _command.Add(nameCommand, action);

        private static double RereadStudyProgres(Subject category)
        {
            if (category.LearnQuestions.Count == 0)
                return 0;
            double learningProgress = category.LearnQuestions.Sum(x => x.GetStudyProgress());
            return CalculatesStudyPercentage(learningProgress, category.LearnQuestions.Count);
        }

        private static double CalculatesStudyPercentage(double learningProgress, double countQuestions)
        {
            var percentage = learningProgress * 100.0 / countQuestions;
            var percentageInTenths = Math.Round(percentage, 1);
            return percentageInTenths;
        }
        private static double AddsProgress(int countQuestions, IRepetition question)
        {
            double learningProgress = question.GetStudyProgress();
            return CalculatesStudyPercentage(learningProgress, countQuestions);
        }
    }
}
