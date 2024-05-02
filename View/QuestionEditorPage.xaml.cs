using LearnApplication.ViewModel;
//using System.Windows.Froms;

namespace LearnApplication.View;

public partial class QuestionEditorPage : ContentPage
{
    private readonly QuestionEditorViewModel _settingsViewModel;
    private double _curentScrollY;

    public QuestionEditorPage(QuestionEditorViewModel settingsViewModel)
	{
		InitializeComponent();
		BindingContext = _settingsViewModel= settingsViewModel;
        _curentScrollY = scrollView.ScrollY;
        
    }

    protected override void OnDisappearing()
    {
        _settingsViewModel.OnUpdateDbService();
        base.OnDisappearing();
    }

    private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
    {
        var newScrollY = e.ScrollY;
        saveButton.IsVisible = newScrollY < _curentScrollY;
        _curentScrollY = newScrollY;
    }
}