using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;
using Syncfusion.Maui.NavigationDrawer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LearnApplication.Model
{
    [Table("learn")]
    public partial class Learn : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        //[Column("categories")]
        //[OneToMany(CascadeOperations = CascadeOperation.All)]
        //public ObservableCollection<Category> Categories { get;  set; } = [];


        private ObservableCollection<Category> _categories = [];

        [Column("categories")]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        public ObservableCollection<Category> GetCategories()
        {
            RunsTimerCompletionChecks();
            return Categories;
        }

        public void MoveToStartingPosition(Category category)
        {
            category.LastActivity = DateTime.Now;
            Categories.Move(Categories.IndexOf(category), 0);
        }

        
        public void SortedCategoriesByViewingTime(bool sortById = true)
        {
            RunsTimerCompletionChecks();
            if (sortById)
                Categories = Categories.OrderByDescending(x => x.LastActivity).ToObservableCollection();
        }

        private void RunsTimerCompletionChecks()
        {
         
            foreach (var category in Categories)
                category.RestartsTheTimer();
        }
        public void AddCategorie(Category сategory)
        {
            if (сategory is null)
                return;
            Categories.Insert(0, сategory);
        }

        public void Delete(Category learnCategory)
        {
            if (learnCategory is not null)
                Categories.Remove(learnCategory);
        }

    }
}
