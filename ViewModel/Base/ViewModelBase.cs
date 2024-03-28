using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnApplication.ViewModel.Base
{
    public abstract class ViewModelBase : ObservableObject
    {
        public virtual Task OnNavigatingTo(object? parameter)
             => Task.CompletedTask;
        public virtual Task OnNavigatingTo(object? parameterFirst, object? parameterSecond)
             => Task.CompletedTask;
        public virtual Task OnNavigatedFrom(bool isForwardNavigation)
            => Task.CompletedTask;
        public virtual Task OnNavigatedTo()
            => Task.CompletedTask;

    }
}
