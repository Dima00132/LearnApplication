using LearnApplication.ViewModel;

namespace LearnApplication.View;

public partial class AddQuestionPage : ContentPage
{
    private readonly AddQuestionViewModel _addQuestionViewModel;
    public AddQuestionPage()
    {
        InitializeComponent();
    }
   
   
    public AddQuestionPage(AddQuestionViewModel viewModel):this()
	{
		BindingContext = _addQuestionViewModel= viewModel;
    }

    protected override void OnDisappearing()
    {
        _addQuestionViewModel.OnSaveDb();
        base.OnDisappearing();
    }


}