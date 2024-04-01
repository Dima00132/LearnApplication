
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
}