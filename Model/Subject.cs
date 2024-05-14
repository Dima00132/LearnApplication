using CommunityToolkit.Maui.Converters;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using LearnApplication.Model.Enum;
using LearnApplication.Model.Command;
using LearnApplication.Service;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Input;
using System.Xml.Serialization;
using LearnApplication.Model.Interface;



namespace LearnApplication.Model
{

    [Table("learn_subject")]
    public partial class Subject: ObservableObject, IUpdateDb
    {  
        [ObservableProperty]
        private string _subjectName; 
        
        [ObservableProperty]
        private double _memorizationPercentage;

        
        //[ObservableProperty]
        private double _repetitionsQuestionsCount;
        [Ignore]
        public double RepetitionsQuestionsCount
        {
            get => _repetitionsQuestionsCount;
            set
            {
                SetProperty(ref _repetitionsQuestionsCount, value);
            }
        }

        [Ignore]
        public EventHandler UpdateDbEvent { get; set; }

        [ObservableProperty]
        private double _knownCountLearn;

        [PrimaryKey,AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [Column("learn_id")]
        [ForeignKey(typeof(Learn))]
        public int LearnId { get; set; }

        private ObservableCollection<CardQuestion> _learnQuestions = [];

        [Column("questions")]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<CardQuestion> LearnQuestions 
        {
            get => _learnQuestions;
            set
            {
                SetProperty(ref _learnQuestions, value);
                InitializingQuestions();
            }
        }

        public DateTime LastActivity { get; set; }

        [ObservableProperty]
        public int _countQuestion;

        private bool _isStart = true;

        public Subject(){}
        public Subject(string subject)
        {
            SubjectName = subject;   
        }

        private void InitializingQuestions()
        {
            if (!_isStart)
                return;
            _isStart = false;
            _learnQuestions = LearnQuestions
                .OrderByDescending(x => x.Id)
                .Select(item =>
                {
                    BindingNotify(item, true);
                    item.RestartsTimer();
                    return item;
                })
                .ToObservableCollection();  
        }

        public IEnumerable<CardQuestion> FindsQuestionByRequest(string request)
        {
            var learnQuestions = LearnQuestions;
            var result = learnQuestions
                .Where(x => x.Question.Length >= request.Length)
                .Where(x => String.Compare(x.Question, 0, request, 0, request.Length, StringComparison.OrdinalIgnoreCase) == 0);
            return result;
        }

        public IReviewCard GetReviewQuestions(bool allOrUnknown = true)
        {

            var questions = LearnQuestions.OfType<ICardQuestion>().ToObservableCollection();
            return new ReviewQuestion(questions, allOrUnknown);
        }

        public void AddQuestion(CardQuestion question)
        {
            LearnQuestions.Insert(0, question);
            BindingNotify(question, true)
                .Card_PropertyChanged(question, new PropertyChangedEventArgs(nameof(LearnQuestions.Insert)));
        }
      
        public void RemoveQuestion(CardQuestion question)
        {
            LearnQuestions.Remove(question);
            BindingNotify(question, false)
                .Card_PropertyChanged(question, new PropertyChangedEventArgs(nameof(LearnQuestions.Remove)));
        }

        private Subject BindingNotify(INotifyPropertyChanged notifyProperty, bool subscriptionsOrHolidaysToEvents)
        {
            if (subscriptionsOrHolidaysToEvents)
                notifyProperty.PropertyChanged += Card_PropertyChanged;
            else
                notifyProperty.PropertyChanged -= Card_PropertyChanged;
            return this;
        }
        public void OrderQuestionByDescending<TResult>(Func<CardQuestion, TResult> funcSort)
            => LearnQuestions = LearnQuestions
            .OrderByDescending(funcSort)
            .ToObservableCollection();

      
        private void Card_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is IRepetition value)
                CommandManager.SetPropertyValue(e.PropertyName, value, this);
        }
    }
}
