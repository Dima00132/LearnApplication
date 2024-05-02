using LearnApplication.ViewModel;

namespace LearnApplication.View;

public partial class AddQuestionPage : ContentPage
{
    private readonly AddQuestionViewModel _addQuestionViewModel;
    private double _curentScrollY;

    public AddQuestionPage()
    {
        InitializeComponent();
        _curentScrollY = scrollView.ScrollY;
    }
   
    public AddQuestionPage(AddQuestionViewModel viewModel):this()
	{
		BindingContext = _addQuestionViewModel= viewModel;
    }

    protected override void OnDisappearing()
    {
        _addQuestionViewModel.OnUpdateDbService();
        base.OnDisappearing();
    }

    private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
    {
        var newScrollY = e.ScrollY;
        saveButton.IsVisible = newScrollY < _curentScrollY;
        _curentScrollY = newScrollY;
    }
}