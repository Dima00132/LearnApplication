using LearnApplication.Model;
using LearnApplication.Service;
using LearnApplication.ViewModel;
using System.Collections.ObjectModel;

namespace LearnApplication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
  
        public MainPage(MainViewModel viewModel)
        {
            BindingContext =viewModel;
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            var g = 2;
            base.OnDisappearing();
        }
    }

}
