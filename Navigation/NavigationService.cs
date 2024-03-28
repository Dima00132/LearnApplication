
using LearnApplication.View;
using LearnApplication.ViewModel;
using LearnApplication.ViewModel.Base;
using Syncfusion.Maui.Core.Carousel;
using System.Diagnostics;


namespace LearnApplication.Navigation
{
    public class NavigationService : INavigationService
    {

        private Dictionary<Type, Type> viewModelView = new Dictionary<Type, Type>()
        {
            {typeof(AddQuestionViewModel),typeof(AddQuestionPage)},
            {typeof(QuestionsViewModel),typeof(QuestionsPage)},
            {typeof(RepetitionOfMaterialViewModel),typeof(RepetitionOfMaterialPage)},
            {typeof(SettingsViewModel),typeof(SettingsPage)},
            {typeof(SubjectPage),typeof(SubjectPage)},
            {typeof(TabbedLearnViewModel),typeof(TabbedPage)}
        };


        readonly IServiceProvider _services;
        protected INavigation Navigation
        {
            get
            {
                INavigation? navigation = Application.Current?.MainPage?.Navigation;
                if (navigation is not null)
                    return navigation;
                else
                {
                    if (Debugger.IsAttached)
                        Debugger.Break();
                    throw new Exception();
                }
            }
        }
        public NavigationService(IServiceProvider services)=> _services = services;

        public Task NavigateToMainPage(object? parameter = null)
                    => NavigateToPage<MainPage>(parameter);
        public Task NavigateByPage<T>(object? parameter = null) where T : Page
                => NavigateToPage<T>(parameter);


        public async Task NavigateByViewModel<T>(object? parameter = null) where T : ViewModelBase
        {
            
            if (viewModelView.ContainsKey(typeof(T)))
            {
                var typePage = viewModelView[typeof(T)];
                await NavigateToPage(typePage, parameter);
            }else
                throw new KeyNotFoundException($"Не найден тип в словаре {viewModelView}");
        }


        private async Task NavigateToPage(Type typePage,object? parameter = null) 
        {
            var toPage = ResolvePage(typePage);
            await InitializecircutPage(toPage, parameter);
        }

        private async Task NavigateToPage<T>(object? parameter = null) where T : Page
        {
            var toPage = ResolvePage<T>();
            await InitializecircutPage(toPage, parameter);
        }

        private async Task InitializecircutPage(Page toPage, object? parameter = null)
        {
            if (toPage is not null)
            {
                toPage.NavigatedTo += Page_NavigatedTo;
                var toViewModel = GetPageViewModelBase(toPage);
                if (toViewModel is not null)
                    await toViewModel.OnNavigatingTo(parameter);
                await Navigation.PushAsync(toPage, true);
                toPage.NavigatedFrom += Page_NavigatedFrom;
            }
            else
                throw new InvalidOperationException($"Unable to resolve type");
        }

        private async void Page_NavigatedFrom(object? sender, NavigatedFromEventArgs e)
        {
            bool isForwardNavigation = Navigation.NavigationStack.Count > 1
                && Navigation.NavigationStack[^2] == sender;
            if (sender is Page thisPage)
            {
                if (!isForwardNavigation)
                {
                    thisPage.NavigatedTo -= Page_NavigatedTo;
                    thisPage.NavigatedFrom -= Page_NavigatedFrom;
                }
                await CallNavigatedFrom(thisPage, isForwardNavigation);
            }
        }
        private Task CallNavigatedFrom(Page p, bool isForward)
        {
            var fromViewModel = GetPageViewModelBase(p);
            if (fromViewModel is not null)
                return fromViewModel.OnNavigatedFrom(isForward);
            return Task.CompletedTask;
        }
        private ViewModelBase GetPageViewModelBase<T>(T toPage) where T : Page
             =>toPage?.BindingContext as ViewModelBase;
        private async void Page_NavigatedTo(object? sender, NavigatedToEventArgs e)
            => await CallNavigatedTo(sender as Page);
        private  Task CallNavigatedTo(Page? page)
        {
            var fromViewModel = GetPageViewModelBase(page);
            if (fromViewModel is not null)
                return fromViewModel.OnNavigatedTo();
            return Task.CompletedTask;
        }


        private T? ResolvePage<T>() where T : Page
          => _services.GetService<T>();

        private Page ResolvePage(Type type)
            =>_services.GetService(type) as Page;
        

        public Task NavigateBack()
        {
            if (Navigation.NavigationStack.Count > 1)
                return Navigation.PopAsync();
            throw new InvalidOperationException("No pages to navigate back to!");
        }
    }
}
