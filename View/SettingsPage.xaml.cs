using LearnApplication.ViewModel;

namespace LearnApplication.View;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

    }
}