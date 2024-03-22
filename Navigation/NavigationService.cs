using LearnApplication.ViewModel.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnApplication.Navigation
{
    public class NavigationService : INavigationService
    {
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
                    //This is not good!
                    if (Debugger.IsAttached)
                        Debugger.Break();
                    throw new Exception();
                }
            }
        }
        public NavigationService(IServiceProvider services)=> _services = services;


        public Task NavigateToMainPage()
                    => NavigateToPage<MainPage>();
        public Task NavigateTo<T>(object parameter) where T : Page
                => NavigateToPage<T>(parameter);

        private async Task NavigateToPage<T>(object? parameter = null) where T : Page
        {
            var toPage = ResolvePage<T>();
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
                throw new InvalidOperationException($"Unable to resolve type {typeof(T).FullName}");
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

        public Task NavigateTo<T>() where T : Page
        => NavigateToPage<T>();
        private Task NavigateToPage<T>() where T : Page
        {
            var page = ResolvePage<T>();
            if (page is not null)
                return Navigation.PushAsync(page, true);
            throw new InvalidOperationException($"Unable to resolve type {typeof(T).FullName}");
        }
        private T? ResolvePage<T>() where T : Page
          => _services.GetService<T>();
        public Task NavigateBack()
        {


            if (Navigation.NavigationStack.Count > 1)
                return Navigation.PopAsync();
            throw new InvalidOperationException("No pages to navigate back to!");
        }


    }
}
