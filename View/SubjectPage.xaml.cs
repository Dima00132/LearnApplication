
using LearnApplication.ViewModel;

namespace LearnApplication.View;


[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class SubjectPage : ContentPage
{
    public SubjectPage()
    {
        InitializeComponent();
    }

    public SubjectPage(SubjectViewModel subjectViewModel ):base()
    {
        BindingContext = subjectViewModel;
    }

    protected override void OnAppearing()
    {
        if(BindingContext is SubjectViewModel subjectViewModel)
            subjectViewModel.InitializesFields();
        base.OnAppearing();
    }

}