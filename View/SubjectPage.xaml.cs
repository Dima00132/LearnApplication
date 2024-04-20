
using LearnApplication.ViewModel;

namespace LearnApplication.View;


[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class SubjectPage : ContentPage
{
    //private readonly SubjectViewModel _subjectViewModel;

    public SubjectPage()
    {
        InitializeComponent();
    }

    //public SubjectPage(SubjectViewModel subjectViewModel ):base()
    //{
    //    BindingContext = _subjectViewModel= subjectViewModel;
     
    //}

    protected override void OnAppearing()
    {
        if (BindingContext is SubjectViewModel subjectViewModel)
            subjectViewModel.OnUpdate();
        base.OnAppearing();
    }
}