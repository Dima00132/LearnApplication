using LearnApplication.ViewModel;

namespace LearnApplication.View;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class RepetitionOfMaterialPage : ContentPage
{
	public RepetitionOfMaterialPage(RepetitionOfMaterialViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        
    }

    //private void CarouselView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
    //{
    //   // carouselView.ScrollTo(0,0,ScrollToPosition.Start,false);
    //}

}