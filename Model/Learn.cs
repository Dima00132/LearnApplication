using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnApplication.Model
{
    [Table("learn")]
    public class Learn
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [Column("categories")]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Category> LearnCategories { get;  set; } = [];

        public ObservableCollection<Category> GetCategories()
        {
            RunsTimerCompletionChecks();
            return LearnCategories;
        }

        public ObservableCollection<Category> GetSortedCategoriesByViewingTime(bool restartTimer = true)
        {
            if(restartTimer)
                RunsTimerCompletionChecks();
            var sortedByPlantTime = LearnCategories.OrderByDescending(x => x.LastEntrance);
            LearnCategories = new ObservableCollection<Category>(sortedByPlantTime);
            return LearnCategories;
        }

        private void RunsTimerCompletionChecks()
        {
            foreach (var category in LearnCategories)
                category.RestartsTheTimer();
        }
        public void AddCategorie(Category learnCategory)
        {
            if (learnCategory is not null)
                LearnCategories.Insert(0,learnCategory);
        }

        public void Delete(Category learnCategory)
        {
            if (learnCategory is not null)
                LearnCategories.Remove(learnCategory);
        }

    }
}
