using FutbolSolution.WPF.Services.Search;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FutbolSolution.WPF.Utils;
using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.Views;
using FutbolSolution.WPF.ViewModels.PlayerViewModel;
using FutbolSolution.WPF.Views.PlayerView;
using FutbolSolution.WPF.Views.TeamView;
using FutbolSolution.WPF.Views.RefereeView;
using FutbolSolution.WPF.Views.MatchView;
using FutbolSolution.WPF.Views.AnalysisView;
using FutbolSolution.WPF.Views.CoefficientView;

namespace FutbolSolution.WPF.ViewModels.SearchViewModel
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        private readonly ISearchService _searchService;
        private ObservableCollection<string> _searchItems;
        private string _query;
        private ObservableCollection<SearchResult> _results;
        private bool _resultsVisible;

        public ObservableCollection<string> SearchItems
        {
            get { return _searchItems; }
            set
            {
                _searchItems = value;
                OnPropertyChanged();
            }
        }

        public string Query
        {
            get => _query;
            set
            {
                _query = value;
                OnPropertyChanged();
                Search(null);
            }
        }

        public ObservableCollection<SearchResult> Results
        {
            get => _results;
            set
            {
                _results = value;
                OnPropertyChanged();
                ResultsVisible = _results != null && _results.Count > 0;
            }
        }

        public bool ResultsVisible
        {
            get => _resultsVisible;
            set
            {
                _resultsVisible = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchCommand { get; }

        public SearchViewModel(ISearchService searchService, INavigationService navigationService)
        {
            _searchService = searchService;
            SearchCommand = new RelayCommand<object>(Search);
            SearchItems = new ObservableCollection<string>
            {
                "Create Player",
                "Players",
                "Player Operations",
                "Create Team",
                "Teams",
                "Team Operation",
                "Create Referee",
                "Refeeres",
                "Referee Operation",
                "Match",
                "Match Operation",
                "Create Match",
                "Simulation",
                "Configuration",
                "Main Page"
            };
            _navigationService = navigationService;
            Results = new ObservableCollection<SearchResult>();
        }

        private SearchResult _selectedResult;
        public SearchResult SelectedResult
        {
            get => _selectedResult;
            set
            {
                if (_selectedResult != value)
                {
                    _selectedResult = value;
                    OnPropertyChanged();

                    if (_selectedResult != null)
                    {
                        NavigateToDetail(_selectedResult);
                        CloseResults(); // Close results after selection
                    }
                }
            }
        }

        private void CloseResults()
        {
            Results.Clear();
            ResultsVisible = false; // Hide the results
        }

        private void NavigateToDetail(SearchResult result)
        {
            if (result.UserControlType != null)
            {
                _navigationService.NavigateTo(result.UserControlType);
            }
        }

        private void Search(object parameter)
        {
            if (string.IsNullOrWhiteSpace(Query))
            {
                Results.Clear();
                ResultsVisible = false;
                SelectedResult = null; // Clear selected result when there's no query
                return;
            }

            // Filter the search items based on the query
            var filteredResults = SearchItems
                .Where(item => item.IndexOf(Query, StringComparison.OrdinalIgnoreCase) >= 0)
                .Select(item => new SearchResult
                {
                    Title = item,
                    Description = $"Search result for {item}",
                    UserControlType = GetUserControlForTitle(item) // Fetch corresponding UserControl type
                })
                .ToList();

            Results = new ObservableCollection<SearchResult>(filteredResults);

            // Show or hide the popup based on results
            ResultsVisible = Results.Count > 0;
        }

        private Type GetUserControlForTitle(string title)
        {
            // Map title to corresponding UserControl type
            switch (title)
            {
                case "Create Player":
                    return typeof(CreatePlayerView);
                case "Players":
                    return typeof(MainPlayerView);
                case "Player Operations":
                    return typeof(MainPlayerView);
                case "Create Team":
                    return typeof(CreateTeamView);
                case "Teams":
                    return typeof(MainTeamView);
                case "Team Operation":
                    return typeof(MainTeamView);
                case "Create Referee":
                    return typeof(CreateRefereeView);
                case "Refeeres":
                    return typeof(MainRefereeView);
                case "Referee Operation":
                    return typeof(MainRefereeView);
                case "Create Match":
                    return typeof(CreateMatchView);
                case "Match Operation":
                    return typeof(MainMatchView);
                case "Match":
                    return typeof(MainMatchView);
                case "Simulation":
                    return typeof(AnalysisView);
                case "Configuration":
                    return typeof(CoefficientView);

                default:
                    return null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
