
using LearnApplication.ViewModel;
using Microsoft.Maui.Controls;

namespace LearnApplication.View;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class TabbedLearnPage : TabbedPage
{
    private readonly TabbedLearnViewModel tabbedLearnViewModel;
    public TabbedLearnPage(TabbedLearnViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = tabbedLearnViewModel= viewModel;
    }

    //protected override void OnAppearing()
    //{
    //    tabbedLearnViewModel.OnNavigatingTo();
    //    base.OnAppearing();
    //}
}