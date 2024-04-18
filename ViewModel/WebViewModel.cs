using CommunityToolkit.Mvvm.ComponentModel;
using LearnApplication.Navigation;
using LearnApplication.Service;
using LearnApplication.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnApplication.ViewModel
{
    public partial class WebViewModel:ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ILocalDbService _localDbService;

        [ObservableProperty]
        private UrlWebViewSource _urlWebViewSource;

        public WebViewModel(INavigationService navigationService, ILocalDbService localDbService)
        {
            _navigationService = navigationService;
            _localDbService = localDbService;
        }

        public override Task OnNavigatingTo(object? parameter, object? parameterSecond = null)
        {

            if (parameter is UrlWebViewSource urlWebView)
                UrlWebViewSource = urlWebView;
            return base.OnNavigatingTo(parameter, parameterSecond);
        }
    }
}
