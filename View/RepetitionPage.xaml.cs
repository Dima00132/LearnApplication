using LearnApplication.ViewModel;

namespace LearnApplication.View;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class RepetitionPage : ContentPage
{
    public RepetitionPage()
    {
        InitializeComponent();
    }

    public RepetitionPage(RepetitionViewModel viewModel):this()
	{
		BindingContext = viewModel;    
    }
}