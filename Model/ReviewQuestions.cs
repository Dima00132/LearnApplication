using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace LearnApplication.Model
{
    public partial class ReviewQuestion : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<LearnQuestion> _reviewQuestions;
 

        public bool IsQuestion { get => ReviewQuestions.Count >1; }

        public ReviewQuestion(ObservableCollection<LearnQuestion> returnsQuestions)
        {
            ReviewQuestions = returnsQuestions;
        }

        public void MoveQuestionToEnd(LearnQuestion learnQuestion)
        {
            ////////////
            DeleteQuestion(learnQuestion);
            ReviewQuestions.Add( learnQuestion); 
        }


        public void DeleteQuestion(LearnQuestion learnQuestion)
        {
            learnQuestion.IsKnown = true;
            ReviewQuestions.Remove(learnQuestion); 
        }
    }
}
