using FutbolSolution.Core.DTOs.MatchDTOs;
using FutbolSolution.Core.DTOs.TeamDTOs;
using FutbolSolution.Core.Mapper;
using FutbolSolution.Core.Models;
using FutbolSolution.Core.Services;
using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.Utils;
using FutbolSolution.WPF.Views.TeamView;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows.Input;

namespace FutbolSolution.WPF.ViewModels.TeamViewModel
{
    public class MainTeamViewModel
    {
        private ObservableCollection<TeamDTO> _teams;
        private string _searchText;
        private string _selectedFilterOption;
        private ICollectionView _teamView;
        private IPlayerTeamLink _teamLinkService;
        private INavigationService _navigationService;
        private ITeamService _teamService;
        private ITeamStatisticsService _teamStatisticsService;
        private IMatchService _matchService;
        private IMatchStatisticsService _matchStatisticsService;
        private IMapper<BaseTeamDTO, TeamStatistics> _mapper;


        public ObservableCollection<TeamDTO> Teams
        {
            get { return _teams; }
            set
            {
                _teams = value;
                OnPropertyChanged();
            }
        }

        public ICollectionView TeamViews
        {
            get { return _teamView; }
            set
            {
                _teamView = value;
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
                    Console.WriteLine($"SelectedFilterOption changed to: {_selectedFilterOption}"); // Debugging output

                    OnPropertyChanged(); // Notify UI to update();
                    ApplyFilter();
                }

            }
        }
        public ICommand SelectFilterOptionCommand { get; private set; }


        public ObservableCollection<string> FilterOptions { get; private set; }

        public ICommand UpdateCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand CheckCommand { get; private set; }
        public ICommand AddCommand { get; private set; }

        public MainTeamViewModel(INavigationService navigationService, ITeamService teamService,
            ITeamStatisticsService teamStatisticsService, IMapper<BaseTeamDTO, TeamStatistics> mapper
           , IPlayerTeamLink teamLinkService, IMatchService matchService, IMatchStatisticsService matchStatisticsService)
        {
            _mapper = mapper;
            _teamStatisticsService = teamStatisticsService;
            _teamService = teamService;
            _navigationService = navigationService;
            _matchService = matchService;
            _matchStatisticsService = matchStatisticsService;
            Teams = new ObservableCollection<TeamDTO>();
            FilterOptions = new ObservableCollection<string> { "Name", "City", "Stadium" }; // Example filter options

            InitializeData();

            UpdateCommand = new RelayCommand<object>(UpdateTeam);
            DeleteCommand = new RelayCommand<object>(DeleteTeam);
            CheckCommand = new RelayCommand<object>(CheckTeam);
            AddCommand = new RelayCommand<object>(AddTeam);

            TeamViews = CollectionViewSource.GetDefaultView(Teams);
            TeamViews.Filter = FilterTeams;
            SelectFilterOptionCommand = new RelayCommand<object>(SelectFilterOption);
            SelectedFilterOption = FilterOptions[0]; // Set default filter option
            SelectedFilterOption = "Name";
            _teamLinkService = teamLinkService;
        }

        private void SelectFilterOption(object parameter)
        {
            var filterOption = parameter as string;
            SelectedFilterOption = filterOption;
        }

        private async void InitializeData()
        {
            var response = await _teamService.GetAll();
            var teamsDTO = response.Data;
            foreach (TeamDTO p in teamsDTO)
            {
                Teams.Add(p);
            }
        }

        private bool FilterTeams(object item)
        {
            if (item is TeamDTO team)
            {
                if (string.IsNullOrEmpty(SearchText)) return true;

                switch (SelectedFilterOption)
                {
                    case "Name":
                        return team.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0;
                    case "Stadium":
                        return team.Stadium.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0;
                    case "City":
                        return team.City.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0;
                    default:
                        return true;
                }
            }
            return false;
        }

        private void ApplyFilter()
        {
            TeamViews.Refresh();
        }

        private void UpdateTeam(object team)
        {
            _navigationService.NavigateTo<UpdateTeamView>(team);
        }

        private async void DeleteTeam(object parameter)
        {
            var team = parameter as TeamDTO;

            //Delete firslty team stats
            var statsEntity = await _teamStatisticsService.GetTeamStatsByTeamID(team.TeamId);
            if (statsEntity == null) return;
            //var convertedDto = _mapper.Map(statsEntity.Data, null);
            var deleteResponse = await _teamStatisticsService.Delete(statsEntity.Data);
            if (deleteResponse == null) return;

            //Delete team player link data
            var playerTeamLinkResponse = await _teamLinkService.FindByTeamID(team.TeamId);
            if (playerTeamLinkResponse.IsSuccessful)
            {
                var responseData = playerTeamLinkResponse.Data;
                foreach (var item in responseData)
                {
                    await _teamLinkService.Delete(item);
                }
            }

            //delete matches
            var getAllMatches = await _matchService.GetMatchesByTeamId(team.TeamId);
            foreach (var matchItem in getAllMatches.Data)
            {
                var matchStats = await _matchStatisticsService.GetMatchStatsByMatchID(matchItem.MatchId);
                var mappedStats = matchStats.Data as MatchStatsDTO;
                await _matchStatisticsService.Delete(mappedStats);
                await _matchService.Delete(matchItem);
            }


            await _teamService.Delete(team);
            Teams.Remove(team);
        }

        private async void CheckTeam(object parameter)
        {
            var teamdto = (TeamDTO)parameter;
            var teamStats = await _teamStatisticsService.GetTeamStatsByTeamID(teamdto.TeamId);
            if (teamStats.IsSuccessful)
            {
                var DtoTransportesLit = new
                {
                    teamDTO = teamdto,
                    teamStats = teamStats.Data,
                };
                _navigationService.NavigateTo<CheckTeamView>(DtoTransportesLit);
            }
        }

        private void AddTeam(object _)
        {
            _navigationService.NavigateTo<CreateTeamView>(null);
        }

        public void Refresh()
        {
            Teams.Clear();
            InitializeData();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
