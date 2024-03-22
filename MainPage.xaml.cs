using LearnApplication.Model;
using LearnApplication.ViewModel;
using System.Collections.ObjectModel;

namespace LearnApplication
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel viewModel)
        {
            BindingContext = viewModel;
            InitializeComponent();
        }
    }

}
