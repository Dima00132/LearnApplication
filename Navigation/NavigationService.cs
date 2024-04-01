using LearnApplication.View;
using LearnApplication.ViewModel;
using LearnApplication.ViewModel.Base;
using System.Diagnostics;



namespace LearnApplication.Navigation
{
    public class NavigationService : INavigationService
    {

        private readonly Dictionary<Type, Type> viewModelView = new()
        {
            {typeof(AddQuestionViewModel),typeof(AddQuestionPage)},
            {typeof(QuestionsViewModel),typeof(QuestionsPage)},
            {typeof(RepetitionOfMaterialViewModel),typeof(RepetitionOfMaterialPage)},
            {typeof(SettingsViewModel),typeof(SettingsPage)},
            {typeof(SubjectPage),typeof(SubjectPage)},
            {typeof(TabbedLearnViewModel),typeof(TabbedLearnPage)}
        };


        readonly IServiceProvider _services;
        public static INavigation Navigation
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


        public Task NavigateByViewModel<T>(object? parameter = null) where T : ViewModelBase
        {
            
            if (viewModelView.ContainsKey(typeof(T)))
            {
                var typePage = viewModelView[typeof(T)];
                return NavigateToPage(typePage, parameter);
            }else
                throw new KeyNotFoundException($"Не найден тип в словаре {viewModelView}");
        }


        private async Task NavigateToPage(Type typePage,object? parameter = null) 
        {
            if(ResolvePage(typePage) is Page toPage)
                await InitializecircutPage(toPage, parameter);
        }

        private async Task NavigateToPage<T>(object? parameter = null) where T : Page
        {
            if (ResolvePage<T>() is T toPage)
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
                await NavigationService.Navigation.PushAsync(toPage, true);
                toPage.NavigatedFrom += Page_NavigatedFrom;
            }
            else
                throw new InvalidOperationException($"Unable to resolve type");
        }

        private async void Page_NavigatedFrom(object? sender, NavigatedFromEventArgs e)
        {
            bool isForwardNavigation = NavigationService.Navigation.NavigationStack.Count > 1
                && NavigationService.Navigation.NavigationStack[^2] == sender;
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
        private ViewModelBase GetPageViewModelBase(Page toPage)
        { 
            if(toPage?.BindingContext is ViewModelBase viewModel)
                return viewModel;
            throw new NullReferenceException($"Не найден BindingContext в Page {toPage?.GetType().FullName}");
        }
        private async void Page_NavigatedTo(object? sender, NavigatedToEventArgs e)
        { 
            if(sender is Page toPage)
             await CallNavigatedTo(toPage); 
        }
        private  Task CallNavigatedTo(Page page)
        {
            var fromViewModel = GetPageViewModelBase(page);
            if (fromViewModel is not null)
                return fromViewModel.OnNavigatedTo();
            return Task.CompletedTask;
        }


        private T? ResolvePage<T>() where T : Page
          => _services.GetService<T>();

        private object ResolvePage(Type type)
        {
            var service = _services.GetService(type);
            if (service is not null)
               return service;
            throw new NullReferenceException($"Не найден сервер в {_services.GetType().FullName}");
        }
        

        public Task NavigateBack()
        {
            if (NavigationService.Navigation.NavigationStack.Count > 1)
            {
                var page = NavigationService.Navigation.NavigationStack[^2];
                if (page?.BindingContext is ViewModelBase viewModel)
                    viewModel.OnUpdate();
                return NavigationService.Navigation.PopAsync();
            }
            throw new InvalidOperationException("No pages to navigate back to!");
        }
    }
}
