using FutbolSolution.Core.DTOs.PlayerDTOs;
using FutbolSolution.WPF.Services.Navigation;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using FutbolSolution.WPF.Utils;
using System.Runtime.CompilerServices;
using FutbolSolution.Core.Services;
using FutbolSolution.WPF.Views.PlayerView;

namespace FutbolSolution.WPF.ViewModels.PlayerViewModel
{
    public class MainPlayerViewModel: INotifyPropertyChanged
    {
        private ObservableCollection<PlayerDTO> _players;
        private string _searchText;
        private string _selectedFilterOption;
        private ICollectionView _playersView;
        private INavigationService _navigationService;
        private IPlayerService _playerService;
        private IPlayerStatsService _playerStatsService;
        private IInjuresSuspensionService _injuresSuspensionService;
        private IPlayerTeamLink _playerTeamLinkService;

        public ObservableCollection<PlayerDTO> Players
        {
            get { return _players; }
            set
            {
                _players = value;
                OnPropertyChanged();
            }
        }

        public ICollectionView PlayersView
        {
            get { return _playersView; }
            set
            {
                _playersView = value;
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


        public ObservableCollection<string> FilterOptions { get; set; }

        public ICommand UpdateCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand CheckCommand { get; private set; }
        public ICommand AddCommand { get; private set; }

        public MainPlayerViewModel(INavigationService navigationService, IPlayerService playerService,
            IPlayerStatsService playerStatsService, IInjuresSuspensionService injuresSuspensionService,
            IPlayerTeamLink playerTeamLinkService)
        {
            _navigationService = navigationService;
            Players = new ObservableCollection<PlayerDTO>();
            FilterOptions = new ObservableCollection<string> { "Name", "Position", "Team" }; // Example filter options
            _playerService = playerService;
            InitializeData();

            UpdateCommand = new RelayCommand<object>(UpdatePlayer);
            DeleteCommand = new RelayCommand<object>(DeletePlayer);
            CheckCommand = new RelayCommand<object>(CheckPlayer);
            AddCommand = new RelayCommand<object>(AddPlayer);

            PlayersView = CollectionViewSource.GetDefaultView(Players);
            PlayersView.Filter = FilterPlayers;
            SelectFilterOptionCommand = new RelayCommand<object>(SelectFilterOption);

            SelectedFilterOption = FilterOptions[0]; // Set default filter option
            SelectedFilterOption = "Name";
            _playerStatsService = playerStatsService;
            _injuresSuspensionService = injuresSuspensionService;
            _playerTeamLinkService = playerTeamLinkService;
        }

        private void SelectFilterOption(object filterOption)
        {
            var filterOptionConverted = filterOption as string;
            Console.WriteLine($"Selected filter option received: {filterOption}"); // Debugging output
            SelectedFilterOption = filterOptionConverted;
            Console.WriteLine($"After the selection : {SelectedFilterOption}");
        }

        private async void InitializeData()
        {
            var response = await _playerService.GetAll();
            var playersDtos = response.Data;
            foreach (PlayerDTO p in playersDtos)
            {
                Players.Add(p);
            }
        }

        private bool FilterPlayers(object item)
        {
            if (item is PlayerDTO player)
            {
                if (string.IsNullOrEmpty(SearchText)) return true;

                switch (SelectedFilterOption)
                {
                    case "Name":
                        return player.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0;
                    case "Position":
                        return player.Position.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0;
                    case "Team":
                        return player.CurrentClub.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0;
                    default:
                        return true;
                }
            }
            return false;
        }

        private void ApplyFilter()
        {
            PlayersView.Refresh();
        }

        private void UpdatePlayer(object player)
        {
            _navigationService.NavigateTo<UpdatePlayerView>(player);
        }

        private async void DeletePlayer(object parameter)
        {
            //delete injures link
          
            var player = parameter as PlayerDTO;
            //delete injures
            await _injuresSuspensionService.DeleteInjuryByPlayerID(player.Id);

            //delete stats 
            var playerStatEntity = await _playerStatsService.GetById(player.PlayerStatsId);
            await _playerStatsService.Delete(playerStatEntity.Data);

            //delete playerteamlink
            var teamlink = await _playerTeamLinkService.FindByPlayerID(player.Id);
            await _playerTeamLinkService.Delete(teamlink.Data);

            //delete player 
            var respose = await _playerService.Delete(player);
            if (respose.StatusCode)
            {
                _players.Remove(player);
            }
        }

        private async void CheckPlayer(object parameter)
        {
            var player = parameter as PlayerDTO;
            var playerStatsResponse = await _playerStatsService.GetById(player.PlayerStatsId);

            var playerInjuresResponse = await _injuresSuspensionService.GetInjuriesSuspensionsByPlayerID(player.Id);

            var DtoTransportesLit = new
            {
                navigationServiceInstance = _navigationService,
                playerDTO = player,
                playerStatsDTO = playerStatsResponse.Data,
                injuriesSuspensionDTOCollection = playerInjuresResponse.Data
            };
            _navigationService.NavigateTo<CheckPlayerView>(DtoTransportesLit);
        }

        private void AddPlayer(object _)
        {
            _navigationService.NavigateTo<CreatePlayerView>(null);
        }

        public void Refresh()
        {
            Players.Clear();
            InitializeData();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
