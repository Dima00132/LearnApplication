
using LearnApplication.ViewModel;
using Microsoft.Maui.Controls;

namespace LearnApplication.View;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class TabbedLearnPage : TabbedPage
{
    private readonly TabbedLearnViewModel _tabbedLearnViewModel;
    public TabbedLearnPage(TabbedLearnViewModel tabbedLearnViewModel)
	{
		InitializeComponent();
        BindingContext = _tabbedLearnViewModel= tabbedLearnViewModel;
    }

    protected override void OnAppearing()
    {
        _tabbedLearnViewModel.OnUpdate();
        base.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        _tabbedLearnViewModel.OnUpdateDbService();
        base.OnDisappearing();
    }
}