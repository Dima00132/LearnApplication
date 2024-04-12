using LearnApplication.ViewModel;

namespace LearnApplication.View;

public partial class AddQuestionPage : ContentPage
{

    public AddQuestionPage()
    {
        InitializeComponent();
  

    }
    AddQuestionViewModel addQuestionViewModel;
    public AddQuestionPage(AddQuestionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = addQuestionViewModel= viewModel;

    }

    protected override void OnDisappearing()
    {
        //addQuestionViewModel._localDbService.UpdateAll();
        base.OnDisappearing();
    }


}