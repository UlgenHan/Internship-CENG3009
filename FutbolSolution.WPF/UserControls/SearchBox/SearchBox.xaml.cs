using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.Services.Search;
using FutbolSolution.WPF.ViewModels.SearchViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FutbolSolution.WPF.UserControls.SearchBox
{
    /// <summary>
    /// Interaction logic for SearchBox.xaml
    /// </summary>
    public partial class SearchBox : UserControl
    {
        public SearchBox()
        {
            InitializeComponent();;
        }

        public ISearchService SearchService { get; set; }
        public INavigationService NavigationService { get; set; }

        // You might want to implement a method to initialize the DataContext
        public void Initialize(ISearchService searchService, INavigationService navigationService)
        {
            SearchService = searchService;
            NavigationService = navigationService;
            DataContext = new SearchViewModel(SearchService, NavigationService);
        }

        private void ResultListBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBox listBox && listBox.SelectedItem is SearchResult selectedResult)
            {
                // Set the SelectedResult in the ViewModel
                var viewModel = (SearchViewModel)DataContext;
                viewModel.SelectedResult = selectedResult;

                // Close the Popup after selection
                ResultsPopup.IsOpen = false;
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // Show the Popup when the TextBox is focused
            var viewModel = (SearchViewModel)DataContext;
            if (viewModel.ResultsVisible)
            {
                ResultsPopup.IsOpen = true;
            }
        }

        // Hide the Popup when the user clicks outside of it
        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            ResultsPopup.IsOpen = false;
        }

    }
}
