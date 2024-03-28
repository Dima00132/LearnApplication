using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


namespace LearnApplication.Model
{
    [Serializable]
    public partial class LearnCategory : ObservableObject
    {

        [ObservableProperty]
        private string _subject;

        [ObservableProperty]
        private int _testCountDontKnown = 0;
        public ObservableCollection<LearnQuestion> LearnQuestions { get;set; }

        public LearnCategory(){}
        public LearnCategory(string subject, ObservableCollection<LearnQuestion> learnQuestions = null)
        {
            var test = "Span определяет следующие свойства:\r\n\r\nBackgroundColorтип Color, представляющий цвет фона диапазона.\r\nCharacterSpacing, тип double, задает интервал между символами в отображаемом тексте.\r\nFontAttributes, типа FontAttributes, определяет стиль текста.\r\nFontAutoScalingEnabledboolТип , определяет, будет ли текст отражать параметры масштабирования, заданные в операционной системе. Значение по умолчанию этого свойства равно true.\r\nFontFamily, тип string, определяет семейство шрифтов.\r\nFontSize, тип double, определяет размер шрифта.\r\nLineHeightdoubleТип , указывает умножение, которое будет применяться к высоте строки по умолчанию при отображении текста.\r\nStyleтип Style, который является стилем, применяемым к диапазону.\r\nText, тип string, определяет текст, отображаемый в виде содержимого Spanобъекта .\r\nTextColor, типа Color, определяет цвет отображаемого текста.\r\nTextDecorations, тип TextDecorations, указывает текстовые украшения (подчеркивание и начерк), которые могут быть применены.\r\nTextTransformTextTransformТип , указывает регистр отображаемого текста.\r\nЭти свойства поддерживаются объектами BindableProperty, то есть эти свойства можно указывать в качестве целевых для привязки и стилизации данных.";

            Subject = subject;
            LearnQuestions = new ObservableCollection<LearnQuestion>() { new LearnQuestion(test, test), new LearnQuestion("ds2", "sd") , new LearnQuestion("ds3", "sd") , new LearnQuestion("ds4", "sd") };
            TestCountDontKnown =  (int)DointKnownCountLearn();
        }

        public double CountProgressLearn()
        {
            double count = LearnQuestions.Count;
            double Known = LearnQuestions.Count(x => x.IsKnown);
            return Known / count;
        }

        public double DointKnownCountLearn()=>LearnQuestions.Count - KnownCountLearn();
       

        public double KnownCountLearn()=>LearnQuestions.Count(x => x.IsKnown);
     

        public int CountQuestion()=>LearnQuestions.Count();

        public ReviewQuestion GetReviewQuestions(bool allOrUnknown = true)
        {
            var questions = new List<LearnQuestion>(LearnQuestions);
            return new ReviewQuestion(questions, allOrUnknown);
        }


        public void AddLearnQuestion(LearnQuestion learn)
        {
            if (learn is null)
                return;
            LearnQuestions.Add(learn);
            learn.Tick += _timer_Tick;
        }

        private void _timer_Tick(object? sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TestCountDontKnown = (int)DointKnownCountLearn();
            });
            
        }
    }
}
