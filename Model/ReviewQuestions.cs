
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace LearnApplication.Model
{
    [Serializable]
    public partial class ReviewQuestion
    {

      
        public ObservableCollection<LearnQuestion> ReviewQuestions { get;set; }
        private Stack<LearnQuestion> _questions;

       
        public double Progress {get; set;}

        private readonly double _countQuestions;
        private readonly LearnCategory _learnCategory;
        private  double _knownQuestions;

        public bool IsQuestion { get => ReviewQuestions.Count >1; }    

        //public ReviewQuestion():this()
        //{
        //}

        //public ReviewQuestion(List<LearnQuestion> returnsQuestions,bool allOrUnknown = true)
        //{
        //    var reviewQuestions = allOrUnknown ? returnsQuestions : returnsQuestions.Where(x => !x.IsKnown).ToList();
        //    //_questions = new Stack<LearnQuestion>(returnsQuestions);
        //    ReviewQuestions = new ObservableCollection<LearnQuestion>(returnsQuestions);
        //    _countQuestions = returnsQuestions.Count;
        //    Progress = allOrUnknown ? 0 : returnsQuestions.Count((x) => x.IsKnown) / _countQuestions;
        //    _knownQuestions = allOrUnknown ? 0 : returnsQuestions.Count((x) => x.IsKnown);


        //}
        public ReviewQuestion(LearnCategory learnCategory, bool allOrUnknown = true)
        {
            _learnCategory = learnCategory;
            var reviewQuestions = allOrUnknown ? learnCategory.LearnQuestions.ToList() : learnCategory.LearnQuestions.Where(x => x.IsRepetitions).ToList();
            //_questions = new Stack<LearnQuestion>(returnsQuestions);
            ReviewQuestions = new ObservableCollection<LearnQuestion>(reviewQuestions);
            _countQuestions = learnCategory.LearnQuestions.Count;
            Progress = allOrUnknown ? 0 : learnCategory.LearnQuestions.Count((x) => x.IsRepetitions) / _countQuestions;
            _knownQuestions = allOrUnknown ? 0 : learnCategory.LearnQuestions.Count((x) => x.IsRepetitions);
        

        }

        public void MoveQuestionToEnd(LearnQuestion learnQuestion)
        {

            ////////////////// Добавить 
            
            //_questions.Push(learnQuestion);
            ReviewQuestions.Remove(learnQuestion);
            ReviewQuestions.Add(learnQuestion);

        }

        //private bool Updata()
        //{

        //}


        public void DeleteQuestion(LearnQuestion learnQuestion)
        {
            learnQuestion.SetsQuestionAsAlreadyKnown();
            _knownQuestions++;
            Progress = _knownQuestions / _countQuestions;
            ReviewQuestions.Remove(learnQuestion);
            ReviewQuestions.Add(learnQuestion);
           


        }
    }
}
