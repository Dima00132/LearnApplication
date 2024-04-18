using LearnApplication.ViewModel;

namespace LearnApplication.View;

public partial class WebPage : ContentPage
{
    private readonly WebViewModel _webViewModel;

    public WebPage(WebViewModel webViewModel)
	{
		BindingContext = _webViewModel = webViewModel;

        InitializeComponent();
	}
}