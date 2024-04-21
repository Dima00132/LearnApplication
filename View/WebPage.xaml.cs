using LearnApplication.ViewModel;

namespace LearnApplication.View;

public partial class WebPage : ContentPage
{
    

    public WebPage(WebViewModel webViewModel)
	{
		BindingContext  = webViewModel;

        InitializeComponent();
	}
}