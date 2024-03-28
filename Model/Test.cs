using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnApplication.Model
{
    public partial class Test2:ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Test> _tests;

        public Test2(ObservableCollection<Test> tests) 
        {
            Tests = tests;
        }

        public Test2()
        {
        }
    }


    public class Test
    {
        public Test()
        {
        }

        public Test(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
