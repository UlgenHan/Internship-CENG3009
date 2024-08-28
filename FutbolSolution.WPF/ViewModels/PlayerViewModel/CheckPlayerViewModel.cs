using FutbolSolution.Core.DTOs.InjuresSuspensionDTOs;
using FutbolSolution.Core.DTOs.PlayerDTOs;
using FutbolSolution.Core.Services;
using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.Utils;
using FutbolSolution.WPF.Views.InjuresSuspensionView;
using FutbolSolution.WPF.Views.PlayerView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FutbolSolution.WPF.ViewModels.PlayerViewModel
{
    public class CheckPlayerViewModel : INotifyPropertyChanged, INavigable
    {
        private PlayerDTO _player;
        private PlayerStatsDTO _playerStats;
        private List<BaseInjuriesSuspensionsDTO> _injuriesSuspensions;
        private byte[] ImageArr { get; set; }
        public ObservableCollection<InjuriesSuspensionsDTO> Items { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateStatsCommand { get; set; }
        public ICommand AddInjures { get; set; }
        private INavigationService _navigationService;
        private IInjuresSuspensionService _injuresSuspensionService { get; set; }

        public string AgeDate {  get; set; }

        public PlayerDTO Player
        {
            get { return _player; }
            set
            {
                _player = value;
                OnPropertyChanged();
            }
        }

        public PlayerStatsDTO PlayerStats
        {
            get { return _playerStats; }
            set
            {
                _playerStats = value;
                OnPropertyChanged();
            }
        }

        public List<BaseInjuriesSuspensionsDTO> InjuriesSuspensions
        {
            get { return _injuriesSuspensions; }
            set
            {
                _injuriesSuspensions = value;
                OnPropertyChanged();
            }
        }

        public CheckPlayerViewModel(INavigationService navigationService,IInjuresSuspensionService injuresSuspensionService)
        {
            _injuresSuspensionService = injuresSuspensionService;
            _navigationService = navigationService;
            Items = new ObservableCollection<InjuriesSuspensionsDTO>();
            DeleteCommand = new RelayCommand<object>(DeleteItem);
            UpdateStatsCommand = new RelayCommand<object>(UpdateStatsNavigation);
            AddInjures = new RelayCommand<object>(AddInjuresNavigation);
            GetInjures();
        }

        private async void GetInjures()
        {
          var response = await _injuresSuspensionService.GetAll();
            InjuriesSuspensions = (List<BaseInjuriesSuspensionsDTO>)response.Data;
            foreach(var inj in  InjuriesSuspensions)
            {
                Items.Add((InjuriesSuspensionsDTO)inj);
            }
        }

        private void AddInjuresNavigation(object obj)
        {
            _navigationService.NavigateTo<CreateInjuresSuspensionView>(Player);
        }

        private void UpdateStatsNavigation(object obj)
        {
            _navigationService.NavigateTo<UpdatePlayerStatsView>(_playerStats);
        }

        private async void DeleteItem(object obj)
        {
            var injure = obj as InjuriesSuspensionsDTO;
            await _injuresSuspensionService.DeleteInjuresLinkByInjuriesSuspensionID(injure.InjurySuspensionId);
            await _injuresSuspensionService.Delete(injure);
            Items.Remove(injure);
            OnPropertyChanged();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public class Item
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnNavigatedTo(object parameter)
        {
            if (parameter == null)
            {
                return;
            }
            var navigationProperty = parameter.GetType().GetProperty("navigationServiceInstance");
            var playerProperty = parameter.GetType().GetProperty("playerDTO");
            var playerStatsProperty = parameter.GetType().GetProperty("playerStatsDTO");
            var injuriesSuspensionProperty = parameter.GetType().GetProperty("injuriesSuspensionDTOCollection");


            _navigationService = (INavigationService)navigationProperty.GetValue(parameter);
            if (playerProperty != null || playerStatsProperty != null || injuriesSuspensionProperty != null)
            {
                var player = playerProperty.GetValue(parameter);
                var playerStats = playerStatsProperty.GetValue(parameter);
                var injuriesSuspension = injuriesSuspensionProperty.GetValue(parameter);

                _player = (PlayerDTO)player;
                Console.WriteLine(_player.PlayerImage);
                _playerStats = (PlayerStatsDTO)playerStats;
                AgeDate = $"{_player.DateOfBirth.Day}/{_player.DateOfBirth.Month}/{_player.DateOfBirth.Year}";

            }
        }

        public void OnNavigatedFrom()
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
             GetInjures();
        }

    }
}
