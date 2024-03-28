using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnApplication.Navigation
{
    public interface INavigationService
    {
        Task NavigateToMainPage(object parameter = null);
        Task NavigateTo<T>(object parameter) where T : Page;
        //Task NavigateTo<T>() where T : Page;
        Task NavigateBack();
    }
}
