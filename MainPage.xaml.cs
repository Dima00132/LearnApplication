using LearnApplication.Model;
using LearnApplication.Service;
using LearnApplication.ViewModel;
using System.Collections.ObjectModel;

namespace LearnApplication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel _mainViewModel;

        public MainPage(MainViewModel mainViewModel)
        {
            BindingContext = _mainViewModel= mainViewModel;
            InitializeComponent();
            
        }

        protected override void OnDisappearing()
        {
            _mainViewModel.OnSaveDb();
            base.OnDisappearing();
        }

    }

}
