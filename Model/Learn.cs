using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public void Start()
        {
            foreach (var category in LearnCategories)
                category.StartTimer();
        }

        public void AddCategorie(Category learnCategory)
        {
            if (learnCategory is not null)
                LearnCategories.Add(learnCategory);
        }

        public void Delete(Category learnCategory)
        {
            if (learnCategory is not null)
                LearnCategories.Remove(learnCategory);
        }

    }
}
