using LearnApplication.ViewModel;

namespace LearnApplication.View;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class RepetitionOfEverythingPage : ContentPage
{
    public RepetitionOfEverythingPage()
    {
        InitializeComponent();
    }

    public RepetitionOfEverythingPage(RepetitionOfEverythingViewModel viewModel):this()
	{
		BindingContext = viewModel;    
    }
}