using LearnApplication.ViewModel;

namespace LearnApplication.View;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class TabbedRepetitionPage : TabbedPage
{
	public TabbedRepetitionPage(TabbedRepetitionViewModel tabbedRepetitionViewModel)
	{
		
		BindingContext = tabbedRepetitionViewModel;
        InitializeComponent();
    }
}