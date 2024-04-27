using LearnApplication.ViewModel;

namespace LearnApplication.View;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class RepetitionPage : ContentPage
{

    private readonly RepetitionViewModel _viewModel;
    public RepetitionPage()
    {
        InitializeComponent();
    }

    public RepetitionPage(RepetitionViewModel viewModel):this()
	{
		BindingContext = _viewModel= viewModel;
        
    }

    

    protected override void OnDisappearing()
    {
        _viewModel?.OnUpdateDbService();
        base.OnDisappearing();
    }
}
