using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace LearnApplication.Model
{
    [Serializable]
    public partial class ReviewQuestion
    {

      
        public ObservableCollection<LearnQuestion> ReviewQuestions { get;set; }

       
        public double Progress {get; set;}

        private readonly double _countQuestions;

        private  double _knownQuestions;

        public bool IsQuestion { get => ReviewQuestions.Count >1; }    

        public ReviewQuestion():this([])
        {
        }

        public ReviewQuestion(List<LearnQuestion> returnsQuestions,bool allOrUnknown = true)
        {
            var reviewQuestions = allOrUnknown ? returnsQuestions : returnsQuestions.Where(x => !x.IsKnown).ToList();
            ReviewQuestions = new ObservableCollection<LearnQuestion>(reviewQuestions);
            _countQuestions = returnsQuestions.Count;
            Progress = allOrUnknown ? 0 : returnsQuestions.Count((x) => x.IsKnown) / _countQuestions;
            _knownQuestions = allOrUnknown ? 0 : returnsQuestions.Count((x) => x.IsKnown);


        }

    

        public void MoveQuestionToEnd(LearnQuestion learnQuestion)
        {
            
            ////////////////// Добавить 
            ReviewQuestions.Remove(learnQuestion);
            ReviewQuestions.Add( learnQuestion); 
        }


        public  void DeleteQuestion(LearnQuestion learnQuestion)
        {
            learnQuestion.SetsQuestionAsAlreadyKnown();
            _knownQuestions++;
            Progress = _knownQuestions / _countQuestions;
            ReviewQuestions.Remove(learnQuestion); 
        }
    }
}
