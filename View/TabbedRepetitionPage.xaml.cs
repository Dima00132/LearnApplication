using LearnApplication.ViewModel;

namespace LearnApplication.View;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class TabbedRepetitionPage : TabbedPage
{
    private readonly TabbedRepetitionViewModel _tabbedRepetitionViewModel;

    public TabbedRepetitionPage(TabbedRepetitionViewModel tabbedRepetitionViewModel)
	{
		
		BindingContext = _tabbedRepetitionViewModel = tabbedRepetitionViewModel;
        InitializeComponent();
     
    }

    protected override void OnDisappearing()
    {
        _tabbedRepetitionViewModel?.OnSaveDb();
        base.OnDisappearing();
    }
}