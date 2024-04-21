using LearnApplication.ViewModel;

namespace LearnApplication.View;

public partial class SettingsPage : ContentPage
{
    private readonly SettingsViewModel _settingsViewModel;

    public SettingsPage(SettingsViewModel settingsViewModel)
	{
		InitializeComponent();
        BindingContext = _settingsViewModel = settingsViewModel;
    }

    //protected override void OnDisappearing()
    //{
    //    _settingsViewModel.OnUpdateDbService();
    //    base.OnDisappearing();
    //}
}