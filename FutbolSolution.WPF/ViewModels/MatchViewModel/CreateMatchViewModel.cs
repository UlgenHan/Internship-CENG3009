using FutbolSolution.Core.DTOs.MatchDTOs;
using FutbolSolution.Core.DTOs.RefereeDTOs;
using FutbolSolution.Core.DTOs.TeamDTOs;
using FutbolSolution.Core.Models;
using FutbolSolution.Core.Services;
using FutbolSolution.Core.Validations;
using FutbolSolution.Service.Services;
using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.Utils;
using FutbolSolution.WPF.Utils.HelperModels;
using FutbolSolution.WPF.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FutbolSolution.WPF.ViewModels.MatchViewModel
{
    public class CreateMatchViewModel : INotifyPropertyChanged
    {
        private readonly IMatchService _matchService;
        private readonly MatchValidator _matchValidator;
        private INavigationService _navigationService;
        private readonly ITeamService _teamService;
        private readonly IRefereeService _refereeService;

        private Dictionary<string, int> teamLinkList;

        private ObservableCollection<string> _teamItems;
        private string _selectedHomeTeamItem;
        private string _SelectedAwayTeamItem;

        public ObservableCollection<string> TeamItems
        {
            get => _teamItems;
            set
            {
                _teamItems = value;
                OnPropertyChanged();
            }
        }

        public string SelectedAwayTeamItem
        {
            get => _SelectedAwayTeamItem;
            set
            {
                _SelectedAwayTeamItem = value;
                OnPropertyChanged();
            }
        }

        public string SelectedHomeTeamItem
        {
            get => _selectedHomeTeamItem;
            set
            {
                _selectedHomeTeamItem = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<RefereeHolder> _refereeItems;
        private string _selectedRefereeItems;

        public ObservableCollection<RefereeHolder> RefereeItems
        {
            get => _refereeItems;
            set
            {
                _refereeItems = value;
                OnPropertyChanged();
            }
        }
        private int _selectedRefereeIndex;
        public int SelectedRefereeIndex
        {
            get => _selectedRefereeIndex;
            set
            {
                _selectedRefereeIndex = value;
                OnPropertyChanged();
            }
        }

        private DateTime _matchDate;
        private string _homeTeam;
        private string _awayTeam;
        private string _stadium;
        private string _referee;
        private string _weatherConditions {  get; set; }
        private string _importance {  get; set; }

        public DateTime MatchDate
        {
            get => _matchDate;
            set
            {
                _matchDate = value;
                OnPropertyChanged();
            }
        }

        public string Importance
        {
            get => _importance;
            set
            {
                _importance = value;
                OnPropertyChanged();
            }
        }

        public string WeatherConditions
        {
            get => _weatherConditions;
            set
            {
                _weatherConditions = value;
                OnPropertyChanged();
            }
        }

        public string HomeTeam
        {
            get => _homeTeam;
            set
            {
                _homeTeam = value;
                OnPropertyChanged();
            }
        }

        public string AwayTeam
        {
            get => _awayTeam;
            set
            {
                _awayTeam = value;
                OnPropertyChanged();
            }
        }

        public string Stadium
        {
            get => _stadium;
            set
            {
                _stadium = value;
                OnPropertyChanged();
            }
        }

        public string Referee
        {
            get => _referee;
            set
            {
                _referee = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }

        public CreateMatchViewModel(IRefereeService refereeService, IMatchService matchService , INavigationService navigationService, ITeamService teamService)
        {
            _matchService = matchService;
            _navigationService = navigationService;
            _matchValidator = new MatchValidator(); // Initialize the validator
            SaveCommand = new RelayCommand<object>(Save);
            _teamService = teamService;
            _refereeService = refereeService;

            RefereeItems = new ObservableCollection<RefereeHolder>();
            TeamItems = new ObservableCollection<string>();
            teamLinkList = new Dictionary<string, int>();

            //Default Configuration for selection Indexes
            SelectedRefereeIndex = -1;

            //Get All team names from db
            InitTeam();
            InitReferee();
            
        }

        private async void InitTeam()
        {
            var teamEntitesResponse = await _teamService.GetAll();
            if (teamEntitesResponse.IsSuccessful)
            {
                var teamEntities = teamEntitesResponse.Data;
                foreach (var entity in teamEntities)
                {
                    var convertedEntity = entity as TeamDTO;
                    teamLinkList.Add(convertedEntity.Name, convertedEntity.TeamId);
                    TeamItems.Add(convertedEntity.Name);
                }
            }
        }

        private async void InitReferee()
        {
            var refereeResponse = await _refereeService.GetAll();
            if(refereeResponse.IsSuccessful && refereeResponse.StatusCode) 
            { 
              foreach(var item in refereeResponse.Data)
                {
                    var convertedItem = item as RefereeDTO;
                    RefereeItems.Add(new RefereeHolder
                    {
                        Name = $"{convertedItem.Name} {convertedItem.Surname}",
                        Bias = (decimal)convertedItem.Bias,
                        RefereeId = convertedItem.RefereeId,
                    });
                }  
            }
        }
        
        private async void Save(object parameter)
        {
            var matchDto = new MatchDTO
            {
                MatchDate = _matchDate,
                HomeTeamId = teamLinkList[SelectedHomeTeamItem],
                AwayTeamId = teamLinkList[SelectedAwayTeamItem],
                Stadium = _stadium,
                RefereeId = RefereeItems[SelectedRefereeIndex].RefereeId,
                WeatherConditions = _weatherConditions,
                Importance = _importance,
            };
            
            // Validate the matchDto
            var (isValid, validationMessages) = _matchValidator.Validate(new Match
            {
                MatchDate = matchDto.MatchDate,
                HomeTeamId = matchDto.HomeTeamId,
                AwayTeamId = matchDto.AwayTeamId,
                Stadium = matchDto.Stadium,
                RefereeId = matchDto.RefereeId,
                WeatherConditions = matchDto.WeatherConditions,
                Importance = matchDto.Importance,
            });

            if (!isValid)
            {
                // Collect all validation messages into a single string
                var errorMessages = string.Join(Environment.NewLine,
                    validationMessages.Select(m => $"{m.Key}: {m.Value}"));

               
                // Show the error messages in the custom message box
                var errorMessageBox = new DarkThemeMessageBox(errorMessages, _navigationService);
                errorMessageBox.ShowDialog();
                return; // Stop further processing if validation fails
            }

            try
            {
                await _matchService.Create(matchDto);

                // Show success message
                var successMessage = "Match created successfully!";
                var successMessageBox = new DarkThemeMessageBox(successMessage, _navigationService);
                successMessageBox.ShowDialog();

                // Clear fields after successful save
                MatchDate = DateTime.Now;
                HomeTeam = string.Empty;
                AwayTeam = string.Empty;
                Stadium = string.Empty;
                Referee = string.Empty;
                Importance = string.Empty;
                WeatherConditions = string.Empty;
            }
            catch (Exception ex)
            {
                var errorMessageBox = new DarkThemeMessageBox("An error occurred while saving the match: " + ex.Message, _navigationService);
                errorMessageBox.ShowDialog();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
