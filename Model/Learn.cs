using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;
using Syncfusion.Maui.NavigationDrawer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static SQLite.SQLite3;

namespace LearnApplication.Model
{
    public interface IObservable
    {
        void Add(IObserver o);
        void Remove(IObserver o);
        void Notify();
    }

    public interface IObserver
    {
        void Update();
    }
    public interface ILearn:IObservable
    {
        ObservableCollection<Subject> Categories { get; set; }
        ObservableCollection<Subject> GetCategories();
        void MoveToPosition(Subject category);



    }

    public interface IUpdateDb
    {
        EventHandler UpdateDbEvent { get; set; }
    }

    public static class LearnExtensions
    {
        public static IEnumerable<Subject> SortedCategories<TResult>(this IEnumerable<Subject> enumerator, Func<Subject, TResult> funcSort)
        {
            foreach (var item in enumerator.OrderByDescending(funcSort))
                yield return item;
        }

        public static IEnumerable<Subject> SetUpdateDbEvent(this IEnumerable<Subject> enumerator, EventHandler action)
        {
            foreach (var item in enumerator)
            {
                item.UpdateDbEvent += action;
                yield return item;
            }
        }
        public static void SetSubjectObservableCollection(this IEnumerable<Subject> enumerator, Learn  learn)
        {
            learn.Categories = enumerator.ToObservableCollection();
        }

    }


    [Table("learn")]
    public partial class Learn : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        private ObservableCollection<Subject> _categories = [];

        [Column("categories")]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Subject> Categories
        {
            get => _categories;
            set
            {
                SetProperty(ref _categories, value); 
            }
        }

        public ObservableCollection<Subject> GetCategories()
        {
            //RunsTimerCompletionChecks();
            return Categories;
        }

        public void MoveToStartingPosition(Subject category)
        {
            category.LastActivity = DateTime.Now;
            Categories.Move(Categories.IndexOf(category), 0);
        }


        public void SortedCategories<TResult>(Func<Subject, TResult> funcSort)
        {
            Categories = Categories.OrderByDescending(funcSort).ToObservableCollection();
        }

        public void SetUpdateDbEvent(EventHandler action)
        {
            foreach (var item in Categories)
                item.UpdateDbEvent += action;
        }

        //private void RunsTimerCompletionChecks()
        //{

        //    foreach (var category in Categories)
        //        category.SortAndRestartComponentsCardQuestion(x => x.Id);
        //}
        public void Add(Subject сategory)
        {
            if (сategory is null)
                return;
            Categories.Insert(0, сategory);
        }

        public void Remove(Subject learnCategory)
        {
            if (learnCategory is not null)
                Categories.Remove(learnCategory);
        }

    }
}
