using LearnApplication.ViewModel;

namespace LearnApplication.View;

public partial class AddQuestionPage : ContentPage
{

    public AddQuestionPage()
    {
        InitializeComponent();
  

    }
    public AddQuestionPage(AddQuestionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

    }
}