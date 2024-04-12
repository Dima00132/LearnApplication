
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace LearnApplication.Model
{
    [Serializable]
    public partial class ReviewQuestion
    {

      
        public ObservableCollection<СardQuestion> ReviewQuestions { get;set; }
        private Stack<СardQuestion> _questions;

       
        public double Progress {get; set;}

        private readonly double _countQuestions;
        private readonly Category _learnCategory;
        private  double _knownQuestions;

        public bool IsQuestion { get => ReviewQuestions.Count >1; }    

        //public ReviewQuestion():this()
        //{
        //}

        //public ReviewQuestion(List<СardQuestion> returnsQuestions,bool allOrUnknown = true)
        //{
        //    var reviewQuestions = allOrUnknown ? returnsQuestions : returnsQuestions.Where(x => !x.IsKnown).ToList();
        //    //_questions = new Stack<СardQuestion>(returnsQuestions);
        //    ReviewQuestions = new ObservableCollection<СardQuestion>(returnsQuestions);
        //    _countQuestions = returnsQuestions.Count;
        //    Progress = allOrUnknown ? 0 : returnsQuestions.Count((x) => x.IsKnown) / _countQuestions;
        //    _knownQuestions = allOrUnknown ? 0 : returnsQuestions.Count((x) => x.IsKnown);


        //}
        public ReviewQuestion(Category learnCategory, bool allOrUnknown = true)
        {
            _learnCategory = learnCategory;
            var reviewQuestions = allOrUnknown ? learnCategory.LearnQuestions : learnCategory.LearnQuestions.Where(x => x.IsRepetitions);
            //_questions = new Stack<СardQuestion>(returnsQuestions);
            ReviewQuestions = new ObservableCollection<СardQuestion>(reviewQuestions);
            _countQuestions = learnCategory.LearnQuestions.Count;
            //Progress = allOrUnknown ? 0 : learnCategory.LearnQuestions.Count((x) => x.IsRepetitions) / _countQuestions;
            Progress = 0;
            _knownQuestions = 0;
        

        }

        public void MoveQuestionToEnd(СardQuestion learnQuestion)
        {

            ////////////////// Добавить 
            
            //_questions.Push(learnQuestion);
            ReviewQuestions.Remove(learnQuestion);
            ReviewQuestions.Add(learnQuestion);

        }

        //private bool Updata()
        //{

        //}


        public void DeleteQuestion(СardQuestion learnQuestion)
        {
            learnQuestion.SetsQuestionAsAlreadyKnown();
            _knownQuestions++;
            Progress = _knownQuestions / _countQuestions;
            ReviewQuestions.Remove(learnQuestion);
            ReviewQuestions.Add(learnQuestion);
           


        }
    }
}
