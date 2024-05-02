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
        private double _curentScrollY;

        public MainPage(MainViewModel mainViewModel)
        {
            BindingContext = _mainViewModel= mainViewModel;
            InitializeComponent();
            _curentScrollY = scrollView.ScrollY;

        }

        //protected override void OnAppearing()
        //{
        //    _mainViewModel.OnStart();
        //    base.OnAppearing();
        //}

        //protected override void OnDisappearing()
        //{
        //    _mainViewModel.OnUpdateDbService();
        //    base.OnDisappearing();
        //}

        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            var newScrollY = e.ScrollY;
            addFrame.IsVisible = newScrollY < _curentScrollY;
            _curentScrollY = newScrollY;
        }
    }

}
