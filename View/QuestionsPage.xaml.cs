using LearnApplication.Model;
using LearnApplication.ViewModel;
using System.Collections.ObjectModel;

namespace LearnApplication.View;


[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class QuestionsPage : ContentPage
{
    public QuestionsPage()
    {
        InitializeComponent();
    }

    public QuestionsPage(QuestionsViewModel questionsViewModel) : base()
    {
        BindingContext = questionsViewModel;
    }
}