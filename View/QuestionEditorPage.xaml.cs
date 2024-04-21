using LearnApplication.ViewModel;

namespace LearnApplication.View;

public partial class QuestionEditorPage : ContentPage
{
    private readonly QuestionEditorViewModel _settingsViewModel;

    public QuestionEditorPage(QuestionEditorViewModel settingsViewModel)
	{
		InitializeComponent();
		BindingContext = _settingsViewModel= settingsViewModel;
        
    }

    protected override void OnDisappearing()
    {
        _settingsViewModel.OnUpdateDbService();
        base.OnDisappearing();
    }
}