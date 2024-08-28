using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace FutbolSolution.WPF.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly Stack<NavigationEntry> _backStack = new Stack<NavigationEntry>();
        private readonly Stack<NavigationEntry> _forwardStack = new Stack<NavigationEntry>();
        private ContentControl _contentControl;
        private readonly IServiceProvider _serviceProvider;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public ContentControl ContentControl
        {
            get => _contentControl;
            set => _contentControl = value ?? throw new ArgumentNullException(nameof(value));
        }

        public void NavigateTo<TView>(object parameter = null) where TView : UserControl
        {
            var view = _serviceProvider.GetRequiredService<TView>();
            NavigateToView(view, parameter);
        }

        public void NavigateTo(Type userControlType, object parameter = null)
        {
            if (userControlType == null) throw new ArgumentNullException(nameof(userControlType));

            var view = (UserControl)_serviceProvider.GetRequiredService(userControlType);
            NavigateToView(view, parameter);
        }

        private void NavigateToView(UserControl view, object parameter)
        {
            if (parameter != null)
            {
                (view.DataContext as INavigable)?.OnNavigatedTo(parameter);
            }

            // Push the current view onto the back stack
            if (_contentControl.Content != null)
            {
                _backStack.Push(new NavigationEntry
                {
                    View = _contentControl.Content,
                    Parameter = null // You can modify this to hold the previous parameter if needed
                });
            }

            // Clear the forward stack
            _forwardStack.Clear();

            // Set the new view
            _contentControl.Content = view;
        }

        public bool CanGoBack => _backStack.Count > 0;

        public void GoBack()
        {
            if (CanGoBack)
            {
                var entry = _backStack.Pop();
                _forwardStack.Push(new NavigationEntry
                {
                    View = _contentControl.Content,
                    Parameter = null // You can modify this to hold the current parameter if needed
                });
                _contentControl.Content = entry.View;
                (entry.View as INavigable)?.OnNavigatedTo(entry.Parameter);

                // Call Refresh to refresh the view
                (entry.View as INavigable)?.Refresh(); // Ensure that the view can handle this
            }
        }

        public bool CanGoForward => _forwardStack.Count > 0;

        public void GoForward()
        {
            if (CanGoForward)
            {
                var entry = _forwardStack.Pop();
                _backStack.Push(new NavigationEntry
                {
                    View = _contentControl.Content,
                    Parameter = null // You can modify this to hold the current parameter if needed
                });
                _contentControl.Content = entry.View;
                (entry.View as INavigable)?.OnNavigatedTo(entry.Parameter);
            }
        }
    }
}
