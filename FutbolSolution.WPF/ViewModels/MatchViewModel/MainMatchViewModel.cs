using FutbolSolution.Core.DTOs.MatchDTOs;
using FutbolSolution.Core.DTOs.RefereeDTOs;
using FutbolSolution.Core.DTOs.TeamDTOs;
using FutbolSolution.Core.Services;
using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.Utils;
using FutbolSolution.WPF.Views.MatchView;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Data;
using System.Windows.Input;

namespace FutbolSolution.WPF.ViewModels.MatchViewModel
{
    public class MainMatchViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<MatchHolder> _matches;
        private string _searchText;
        private string _selectedFilterOption;
        private ICollectionView _matchesView;
        private INavigationService _navigationService;
        private IMatchService _matchService;
        private IMatchStatisticsService _matchStatsService;
        private ITeamService _teamService;
        private IRefereeService _refereeService;

        public ObservableCollection<MatchHolder> Matches
        {
            get { return _matches; }
            set
            {
                _matches = value;
                OnPropertyChanged();
            }
        }

        public ICollectionView MatchesView
        {
            get { return _matchesView; }
            set
            {
                _matchesView = value;
                OnPropertyChanged();
            }
        }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        public string SelectedFilterOption
        {
            get { return _selectedFilterOption; }
            set
            {
                if (_selectedFilterOption != value)
                {
                    _selectedFilterOption = value;
                    OnPropertyChanged();
                    ApplyFilter();
                }
            }
        }
        
        public ObservableCollection<string> FilterOptions { get; set; }

        public ICommand UpdateCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand CheckCommand { get; private set; }
        public ICommand SelectFilterOptionCommand { get; private set; }

        public MainMatchViewModel(INavigationService navigationService,
            IMatchService matchService,IMatchStatisticsService matchStatisticsService,
            ITeamService teamService, IRefereeService refereeService)
        {
            _matchStatsService = matchStatisticsService;
            _navigationService = navigationService;
            _matchService = matchService;
            _teamService = teamService;
            _refereeService = refereeService;
            Matches = new ObservableCollection<MatchHolder>();
            FilterOptions = new ObservableCollection<string> { "Home Team", "Away Team", "Referee" }; // Example filter options
            InitializeData();
            SelectFilterOptionCommand = new RelayCommand<object>(SelectFilterOption);
            UpdateCommand = new RelayCommand<object>(UpdateMatch);
            DeleteCommand = new RelayCommand<object>(DeleteMatch);
            AddCommand = new RelayCommand<object>(AddMatch);
            CheckCommand = new RelayCommand<object>(CheckMatch);

            MatchesView = CollectionViewSource.GetDefaultView(Matches);
            MatchesView.Filter = FilterMatches;

            SelectedFilterOption = FilterOptions[0]; // Set default filter option
        }

        private void SelectFilterOption(object filterOption)
        {
            var filterOptionConverted = filterOption as string;
            SelectedFilterOption = filterOptionConverted;
        }

        private async void InitializeData()
        {
            var response = await _matchService.GetAll();
            var matchDtos = response.Data;
            foreach (MatchDTO match in matchDtos)
            {
                //Matches.Add(match);
                var homeTeamResponse = await _teamService.GetById((int)match.HomeTeamId); 
                var awayTeamResponse = await _teamService.GetById((int)match.AwayTeamId);
                var refereeResponse = await _refereeService.GetById((int)match.RefereeId);

                if(homeTeamResponse.IsSuccessful && awayTeamResponse.IsSuccessful && refereeResponse.IsSuccessful)
                {
                    var matchDateInstance = match.MatchDate.Value;
                    var MatchHolder = new MatchHolder();
                    MatchHolder.MatchID = match.MatchId;
                    MatchHolder.Stadium = match.Stadium;
                    MatchHolder.MatchDate = $"{matchDateInstance.Day}/{matchDateInstance.Month}/{matchDateInstance.Year}";
                    MatchHolder.Importance = match.Importance;
                    MatchHolder.WeatherConditions = match.WeatherConditions;
                    if (homeTeamResponse.StatusCode)
                    {
                        MatchHolder.HomeTeam =  (homeTeamResponse?.Data as TeamDTO).Name;
                    }
                    if (awayTeamResponse.StatusCode)
                    {
                        MatchHolder.AwayTeam = (awayTeamResponse?.Data as TeamDTO).Name;
                    }
                    if(refereeResponse.StatusCode)
                    {
                        var castedDTO = (refereeResponse.Data as RefereeDTO);
                        MatchHolder.Referee = $"{castedDTO.Name} {castedDTO.Surname}";
                    }
                    Matches.Add(MatchHolder);
                }
            }
        }
        
        private bool FilterMatches(object item)
        {
            if (item is MatchHolder match)
            {
                if (string.IsNullOrEmpty(SearchText)) return true;

                switch (SelectedFilterOption)
                {
                    case "Home Team":
                        return match.HomeTeam.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0;
                    case "Away Team":
                        return match.AwayTeam.ToString().IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0;
                    case "Referee":
                        return match.Referee.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0;
                    default:
                        return true;
                }
            }
            return false;
        }

        private void ApplyFilter()
        {
            MatchesView.Refresh();
        }

        private void UpdateMatch(object parameter)
        {
            var match = parameter as MatchHolder;
       
            _navigationService.NavigateTo<UpdateMatchView>(match);
            Refresh();
        }

        private async void DeleteMatch(object parameter)
        { 
            var matchHolder = parameter as MatchHolder;
            var matchResponse = await _matchService.GetById(matchHolder.MatchID);

            if(matchResponse.IsSuccessful && matchResponse.StatusCode)
            {
                var match = matchResponse.Data as MatchDTO;
                var matchStatEntity = await _matchStatsService.GetMatchStatsByMatchID(match.MatchId);
                if (matchStatEntity != null)
                {
                    await _matchStatsService.Delete(matchStatEntity.Data);
                    await _matchService.Delete(match);
                }
                Matches.Remove(matchHolder);
            }
        }

        private void CheckMatch(object parameter)
        {
           _navigationService.NavigateTo<CheckMatchView>(parameter);
        }

        private void AddMatch(object _)
        {
            _navigationService.NavigateTo<CreateMatchView>(null);
        }

        public void Refresh()
        {
            Matches.Clear();
            InitializeData();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    } 
}

