using LearnApplication.Model;
using LearnApplication.ViewModel;
using System.Collections.ObjectModel;

namespace LearnApplication.View;


[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class QuestionsPage : ContentPage
{
    
    private double _curentScrollY;
    public QuestionsPage()
    {
        InitializeComponent();
        _curentScrollY = scrollView.ScrollY;
    }


    public QuestionsPage(QuestionsViewModel questionsViewModel) : base()
    {
        BindingContext = questionsViewModel;
  
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    private void scrollView_Scrolled(object sender, ScrolledEventArgs e)
    {
        var newScrollY = e.ScrollY;
        addFrame.IsVisible = newScrollY < _curentScrollY;
        _curentScrollY = newScrollY;
    }
}