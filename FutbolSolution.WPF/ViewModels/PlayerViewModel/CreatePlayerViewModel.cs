using FutbolSolution.Core.DTOs.PlayerDTOs;
using FutbolSolution.Core.DTOs.TeamDTOs;
using FutbolSolution.Core.Models;
using FutbolSolution.Core.Repositories;
using FutbolSolution.Core.Services;
using FutbolSolution.Core.Validations; // Add this line
using FutbolSolution.Service.Services;
using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.Utils;
using FutbolSolution.WPF.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FutbolSolution.WPF.ViewModels.PlayerViewModel
{
    public class CreatePlayerViewModel : INotifyPropertyChanged
    {
        private readonly IPlayerService _playerService;
        private readonly PlayerValidator _playerValidator;
        private readonly ITeamService _teamService;
        private readonly INavigationService _navigationService;
        private readonly IPlayerTeamLink _playerTeamLinkService;
        private Dictionary<string, int> teamLinkList;

        private ObservableCollection<string> _teamItems;
        private string _selectedTeamItem;

        public ObservableCollection<string> TeamItems
        {
            get => _teamItems;
            set
            {
                _teamItems = value;
                OnPropertyChanged();
            }
        }

        public string SelectedTeamItem
        {
            get => _selectedTeamItem;
            set
            {
                _selectedTeamItem = value;
                OnPropertyChanged();
            }
        }


        private string name;
        private string surname;
        private int age;
        private DateTime dateOfBirth;
        private string nationality;
        private string position;
        private string currentClub;
        private double height;
        private double weight;
        private string preferredFoot;
        private int playerStatsId;
        private byte[] playerImage;
        private bool isFileUploaded;

        // Add a property for validation messages
        private string validationMessage;
        public string ValidationMessage
        {
            get => validationMessage;
            set { validationMessage = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }

        public string Surname
        {
            get => surname;
            set { surname = value; OnPropertyChanged(); }
        }

        public int Age
        {
            get => age;
            set { age = value; OnPropertyChanged(); }
        }

        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set { dateOfBirth = value; OnPropertyChanged(); }
        }

        public string Nationality
        {
            get => nationality;
            set { nationality = value; OnPropertyChanged(); }
        }

        public string Position
        {
            get => position;
            set { position = value; OnPropertyChanged(); }
        }

        public string CurrentClub
        {
            get => currentClub;
            set { currentClub = value; OnPropertyChanged(); }
        }

        public double Height
        {
            get => height;
            set { height = value; OnPropertyChanged(); }
        }

        public double Weight
        {
            get => weight;
            set { weight = value; OnPropertyChanged(); }
        }

        public string PreferredFoot
        {
            get => preferredFoot;
            set { preferredFoot = value; OnPropertyChanged(); }
        }

        public int PlayerStatsId
        {
            get => playerStatsId;
            set { playerStatsId = value; OnPropertyChanged(); }
        }

        public byte[] PlayerImage
        {
            get => playerImage;
            set { playerImage = value; OnPropertyChanged(); }
        }

        public bool IsFileUploaded
        {
            get => isFileUploaded;
            set { isFileUploaded = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }

        public CreatePlayerViewModel(IPlayerService playerService,INavigationService navigationService,
            ITeamService teamService, IPlayerTeamLink playerTeamLinkService)
        {
            _playerTeamLinkService = playerTeamLinkService;
            _playerService = playerService;
            _playerValidator = new PlayerValidator(); // Instantiate the validator
            _navigationService = navigationService;
            SaveCommand = new RelayCommand<object>(SavePlayer, CanSavePlayer);
            teamLinkList = new Dictionary<string, int>();
            TeamItems = new ObservableCollection<string>();
            _teamService = teamService;
            GetTeamLink();
        }


        private async void GetTeamLink()
        {
            var teamEntitesResponse = await _teamService.GetAll();
            if(teamEntitesResponse.IsSuccessful)
            {
                var teamEntities = teamEntitesResponse.Data;
                foreach(var entity in teamEntities)
                {
                    var convertedEntity = entity as TeamDTO;
                    teamLinkList.Add(convertedEntity.Name, convertedEntity.TeamId);
                    TeamItems.Add(convertedEntity.Name);
                }
            }
        }

        private bool CanSavePlayer(object parameter)
        {
            // You can implement additional checks here if needed
            return true;
        }

        private async void SavePlayer(object parameter)
        {
            var playerDto = new PlayerDTO
            {
                Name = this.Name,
                Surname = this.Surname,
                Age = this.Age,
                CurrentClub = SelectedTeamItem,
                DateOfBirth = this.DateOfBirth,
                Height = (decimal)this.Height,
                Weight = (decimal)this.Weight,
                Nationality = this.Nationality,
                Position = this.Position,
                PreferredFoot = this.PreferredFoot,
                PlayerStatsId = this.PlayerStatsId
            };

            // Validate the playerDto
            var (isValid, validationMessages) = _playerValidator.Validate(new Player
            {
                Name = playerDto.Name,
                Surname = playerDto.Surname,
                Age = playerDto.Age,
                DateOfBirth = playerDto.DateOfBirth,
                Nationality = playerDto.Nationality,
                Position = playerDto.Position,
                Height = playerDto.Height,
                Weight = playerDto.Weight,
                PreferredFoot = playerDto.PreferredFoot
            });

            if (!isValid || !IsFileUploaded || String.IsNullOrEmpty(SelectedTeamItem))
            {
                // Collect all validation messages into a single string
                var errorMessages = string.Join(Environment.NewLine,
                    validationMessages.Select(m => $"{m.Key}: {m.Value}"));

                if (!IsFileUploaded)
                {
                    errorMessages += Environment.NewLine;
                    errorMessages += "Player image not uploaded!";
                }

                if (String.IsNullOrEmpty(SelectedTeamItem))
                {
                    errorMessages += Environment.NewLine;
                    errorMessages += "Player team not selected!";
                }

                // Show the error messages in the custom message box
                var errorMessageBox = new DarkThemeMessageBox(errorMessages, _navigationService);
                errorMessageBox.ShowDialog();
                return; // Stop further processing if validation fails
            }

            try
            {
                playerDto.PlayerImage = new PlayerImage { ImageData = playerImage };            
                var saveResponse = await _playerService.Create(playerDto);

                if (saveResponse.IsSuccessful)
                {

                    var selectedTeamId = teamLinkList[SelectedTeamItem];
                    await _playerTeamLinkService.Create(
                        new PlayerTeamLinkDTO
                        {
                            TeamId = selectedTeamId,
                            PlayerId = saveResponse.Data.Id
                        });
                }

                // Show success message
                var successMessage = "Player created successfully!";
                var successMessageBox = new DarkThemeMessageBox(successMessage, _navigationService);
                successMessageBox.ShowDialog();

                // Clear fields after successful save
                Name = string.Empty;
                Surname = string.Empty;
                Age = 0;
                CurrentClub = string.Empty;
                DateOfBirth = DateTime.Now; // Set to a default value if needed
                Height = 0;
                Weight = 0;
                Nationality = string.Empty;
                Position = string.Empty;
                PreferredFoot = string.Empty;
                PlayerStatsId = 0;
                PlayerImage = null; // Clear the image if needed
            }
            catch (Exception ex)
            {
                var errorMessageBox = new DarkThemeMessageBox("An error occurred while saving the player: " + ex.Message, _navigationService);
                errorMessageBox.ShowDialog();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
