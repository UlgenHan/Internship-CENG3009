using FutbolSolution.Analyzer.LogisticRegression;
using FutbolSolution.Analyzer.Models;
using FutbolSolution.Core.DTOs.InjuresSuspensionDTOs;
using FutbolSolution.Core.DTOs.MatchDTOs;
using FutbolSolution.Core.DTOs.PlayerDTOs;
using FutbolSolution.Core.DTOs.RefereeDTOs;
using FutbolSolution.Core.DTOs.TeamDTOs;
using FutbolSolution.Core.Enums;
using FutbolSolution.Core.Mapper;
using FutbolSolution.Core.Models;
using FutbolSolution.Core.Services;
using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.Utils;
using FutbolSolution.WPF.Utils.HelperModels;
using FutbolSolution.WPF.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using static FutbolSolution.WPF.ViewModels.AnalysisViewModel.AnalysisViewModel;

namespace FutbolSolution.WPF.ViewModels.AnalysisViewModel
{
    public class AnalysisViewModel : INotifyPropertyChanged
    {

        private readonly ITeamService _teamService;
        private readonly IPlayerService _playerService;
        private readonly IPlayerStatsService _playerStatsService;
        private readonly IPlayerTeamLink _playerTeamLink;
        private readonly ITeamStatisticsService _teamStatisticsService;
        private readonly IRefereeService _refereeService;
        private readonly IMatchService _matchService;
        private readonly IMatchStatisticsService _matchStatisticsService;
        private readonly IInjuresSuspensionService _injuresSuspensionService;
        private readonly INavigationService _navigationService;
        private readonly IMapper<BaseTeamDTO, Team> _mapper;

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<PlayerHolder> _teamAFirstColumnPlayers;
        public ObservableCollection<PlayerHolder> TeamAFirstColumnPlayers
        {
            get { return _teamAFirstColumnPlayers; }
            set
            {
                _teamAFirstColumnPlayers = value;
                OnPropertyChanged(nameof(TeamAFirstColumnPlayers));
            }
        }

        private ObservableCollection<PlayerHolder> _teamASecondColumnPlayers;
        public ObservableCollection<PlayerHolder> TeamASecondColumnPlayers
        {
            get { return _teamASecondColumnPlayers; }
            set
            {
                _teamASecondColumnPlayers = value;
                OnPropertyChanged(nameof(TeamASecondColumnPlayers));
            }
        }

        private ObservableCollection<PlayerHolder> _teamBFirstColumnPlayers;
        public ObservableCollection<PlayerHolder> TeamBFirstColumnPlayers
        {
            get { return _teamBFirstColumnPlayers; }
            set
            {
                _teamBFirstColumnPlayers = value;
                OnPropertyChanged(nameof(TeamBFirstColumnPlayers));
            }
        }

        private ObservableCollection<PlayerHolder> _teamBSecondColumnPlayers;
        public ObservableCollection<PlayerHolder> TeamBSecondColumnPlayers
        {
            get { return _teamBSecondColumnPlayers; }
            set
            {
                _teamBSecondColumnPlayers = value;
                OnPropertyChanged(nameof(TeamBSecondColumnPlayers));
            }
        }

        private string _selectedRadioOption;

        public string SelectedRadioOption
        {
            get => _selectedRadioOption;
            set
            {
                if (_selectedRadioOption != value)
                {
                    _selectedRadioOption = value;
                    OnPropertyChanged(nameof(SelectedRadioOption));
                }
            }
        }

        // Team names
        public string TeamAName { get; set; }
        public string TeamBName { get; set; }

        // Team images
        public BitmapImage TeamAImage { get; set; }
        public BitmapImage TeamBImage { get; set; }


        // Selected player properties
        private int _selectedTeamAPlayerIndex;
        public int SelectedTeamAPlayerIndex
        {
            get => _selectedTeamAPlayerIndex;
            set
            {
                _selectedTeamAPlayerIndex = value;
                OnPropertyChanged(nameof(SelectedTeamAPlayerIndex));
                GetSelectedTeam(_selectedTeamAPlayerIndex, 1);
            }
        }

        private int _selectedTeamBPlayerIndex;
        public int SelectedTeamBPlayerIndex
        {
            get => _selectedTeamBPlayerIndex;
            set
            {
                _selectedTeamBPlayerIndex = value;
                OnPropertyChanged(nameof(SelectedTeamBPlayerIndex));
                GetSelectedTeam(_selectedTeamBPlayerIndex, 2);
            }
        }

        private int _selectedAlgorithmIndex;
        public int SelectedAlgorithmIndex
        {
            get => _selectedAlgorithmIndex;
            set
            {
                _selectedAlgorithmIndex = value;
                OnPropertyChanged(nameof(SelectedAlgorithmIndex));
            }
        }

        private int _selectedRefereeIndex;
        public int SelectedRefereeIndex
        {
            get => _selectedRefereeIndex;
            set
            {
                _selectedRefereeIndex = value;
                OnPropertyChanged(nameof(SelectedRefereeIndex));
            }
        }

        private ObservableCollection<TeamHolder> _comboBoxItemsTeam { get; set; }

        public ObservableCollection<TeamHolder> ComboBoxItemsTeam
        {
            get { return _comboBoxItemsTeam; }
            set
            {
                _comboBoxItemsTeam = value;
                OnPropertyChanged(nameof(ComboBoxItemsTeam));
            }
        }

        private ObservableCollection<string> _comboBoxItemsAlgorithm { get; set; }

        public ObservableCollection<string> ComboBoxItemsAlgorithm
        {
            get { return _comboBoxItemsAlgorithm; }
            set
            {
                _comboBoxItemsAlgorithm = value;
                OnPropertyChanged(nameof(ComboBoxItemsAlgorithm));
            }
        }

        private ObservableCollection<RefereeHolder> _comboBoxItemsReferee{ get; set; }

        public ObservableCollection<RefereeHolder> ComboBoxItemsReferee
        {
            get { return _comboBoxItemsReferee; }
            set
            {
                _comboBoxItemsReferee = value;
                OnPropertyChanged(nameof(ComboBoxItemsReferee));
            }
        }


        private TeamHolder _selectedTeamA;
        public TeamHolder SelectedTeamA
        {
            get { return _selectedTeamA; }
            set
            {
                _selectedTeamA = value;
                OnPropertyChanged(nameof(SelectedTeamA));
            }
        }

        private TeamHolder _selectedTeamB;
        public TeamHolder SelectedTeamB
        {
            get { return _selectedTeamB; }
            set
            {
                _selectedTeamB = value;
                OnPropertyChanged(nameof(SelectedTeamB));
            }
        }

        private List<PlayerHolder> InternalPlayerHolderTeamA { get; set; }
        private List<PlayerHolder> InternalPlayerHolderTeamB { get; set; }

        // Commands for buttons
        public ICommand StartMatchCommand { get; set; }
        public ICommand ResetCommand { get; set; }

        private string _homeResult;
        public string HomeResult
        {
            get { return _homeResult; }
            set
            {
                _homeResult = value;
                OnPropertyChanged(nameof(HomeResult));
            }
        }

        public AnalysisViewModel(ITeamService teamService, IPlayerService playerService,
            IPlayerTeamLink teamTeamLinkService,
            IMapper<BaseTeamDTO, Team> mapper,
            ITeamStatisticsService userStatisticsService,
            IRefereeService refereeService,
            IMatchService matchService,
            IMatchStatisticsService matchStatisticsService,
            IInjuresSuspensionService injuresSuspensionService,
            IPlayerStatsService playerStatsService,
            INavigationService navigationService
            )

        {
             // Initialize player names
            _matchService = matchService;
            _teamStatisticsService = userStatisticsService;
            _refereeService = refereeService;
            _matchService = matchService;
            _matchStatisticsService = matchStatisticsService;
            _injuresSuspensionService = injuresSuspensionService;
            _playerService = playerService;
            _teamService = teamService;
            _playerTeamLink = teamTeamLinkService;
            _playerStatsService = playerStatsService;
            _mapper = mapper;
            _navigationService = navigationService;
            ComboBoxItemsTeam = new ObservableCollection<TeamHolder>();
            ComboBoxItemsAlgorithm = new ObservableCollection<string> { "Logistic Regression" };
            ComboBoxItemsReferee = new ObservableCollection<RefereeHolder>();
            StartMatchCommand = new RelayCommand<object>(StartMatch);
            ResetCommand = new RelayCommand<object>(Reset);

            // Set default selected players
            SelectedTeamAPlayerIndex = -1;
            SelectedTeamBPlayerIndex = -1;

            TeamAFirstColumnPlayers = new ObservableCollection<PlayerHolder>();
            TeamASecondColumnPlayers = new ObservableCollection<PlayerHolder>();
            TeamBFirstColumnPlayers = new ObservableCollection<PlayerHolder>();
            TeamBSecondColumnPlayers = new ObservableCollection<PlayerHolder>();
            InitiliazeTeam();
            InitiliazeReferee();

            InternalPlayerHolderTeamA = new List<PlayerHolder>();
            InternalPlayerHolderTeamB = new List<PlayerHolder>();

        }

        private async void InitiliazeTeam()
        {
            var teamBaseDTOs = await _teamService.GetAll();

            foreach (var team in teamBaseDTOs.Data)
            {
                var teamTeamDto = team as TeamDTO;

                ComboBoxItemsTeam.Add(new TeamHolder
                {
                    TeamId = teamTeamDto.TeamId,
                    Name = teamTeamDto.Name,
                    ImageData = teamTeamDto.TeamImage.ImageData
                });
            }
        }

        private async void InitiliazeReferee()
        {
            var refereeDtosResponse = await _refereeService.GetAll();
            foreach(var refereeDto in refereeDtosResponse.Data)
            {
                var convertedReferee = refereeDto as RefereeDTO;
                ComboBoxItemsReferee.Add(new RefereeHolder
                {
                    RefereeId = convertedReferee.RefereeId,
                    Name = convertedReferee.Name,
                    Bias = (decimal)convertedReferee.Bias,
                });
            }
        }


        private async void StartMatch(object obj)
        {
            // Logic for starting the match
            var selectedTeamHolderA = ComboBoxItemsTeam[SelectedTeamAPlayerIndex];
            var selectedTeamHolderB = ComboBoxItemsTeam[SelectedTeamBPlayerIndex];
            var selectedReferee = ComboBoxItemsReferee[SelectedRefereeIndex];
            var selectedOptionForRadioButton = SelectedRadioOption;

            if(selectedOptionForRadioButton != null && selectedOptionForRadioButton == "Option1")
            {
                //get from team home informations for regression ? 
                var homeTeam = await GetRegressionInformationForTeamMD(selectedTeamHolderA, selectedReferee);

                //get from team away informations for regression ? 
                var awayTeam = await GetRegressionInformationForTeamMD(selectedTeamHolderB, selectedReferee);

                var logisticRegressionCalculator = new NMHLogisticRegression(CoefficientHolder.Intercept,CoefficientHolder.BetaTeamStrength,
                    CoefficientHolder.BetaTeamStats,CoefficientHolder.BetaRefBias,CoefficientHolder.BetaMatchHistory);
                var homeTeamProbality = logisticRegressionCalculator.CalculateWinProbabilityWithHistory(homeTeam.teamPlayers,
                    homeTeam.teamStats, awayTeam.teamPlayers, awayTeam.teamStats,
                    selectedReferee.Bias, homeTeam.teamInjuriesSuspensions, awayTeam.teamInjuriesSuspensions,
                    homeTeam.MatchStats,awayTeam.MatchStats);
                HomeResult = homeTeamProbality.ToString();

            } else if(selectedOptionForRadioButton != null && selectedOptionForRadioButton == "Option2")
            {
                //get from team home informations for regression ? 
                var homeTeam = await GetRegressionInformationForTeamNMD(selectedTeamHolderA, selectedReferee);

                //get from team away informations for regression ? 
                var awayTeam = await GetRegressionInformationForTeamNMD(selectedTeamHolderB, selectedReferee);

                var logisticRegressionCalculator = new NMHLogisticRegression(CoefficientHolder.Intercept, CoefficientHolder.BetaTeamStrength,
                    CoefficientHolder.BetaTeamStats, CoefficientHolder.BetaRefBias, CoefficientHolder.BetaMatchHistory);

                var homeTeamProbality = logisticRegressionCalculator.CalculateWinProbability(homeTeam.teamPlayers,
                    homeTeam.teamStats, awayTeam.teamPlayers, awayTeam.teamStats,
                    selectedReferee.Bias, homeTeam.teamInjuriesSuspensions, awayTeam.teamInjuriesSuspensions);
                HomeResult = homeTeamProbality.ToString();
             
            }else
            {
                var messageBox = new DarkThemeMessageBox("You have to choose match data option!!", _navigationService);
                messageBox.ShowDialog();
            }
           
        }
       
        private async Task<RegressionCalculationDataFrame>
            GetRegressionInformationForTeamNMD(TeamHolder selectedTeamHolder,RefereeHolder selectedRefereeHolder)
        {
            // Get Team Entity
            var selectedHomeTeamResponse = await _teamService.GetById(selectedTeamHolder.TeamId);
            var selectedHomeTeam = selectedHomeTeamResponse.Data;
            selectedHomeTeam = selectedHomeTeam ?? selectedHomeTeam as TeamDTO;

            //Get Team Stats
            var teamStatsResponse = await _teamStatisticsService.GetTeamStatsByTeamID(selectedTeamHolder.TeamId);
            var teamStats = teamStatsResponse.Data;
            var teamStatsConverted = teamStats as TeamWithStatisticsDTO;
            var nmhTeamDataFrame = new NMHTeamDataFrame
            {
                Draws = teamStatsConverted.Draws,
                GoalsConceded = teamStatsConverted.GoalsConceded,
                GoalsScored = teamStatsConverted.GoalsScored,
                Losses = teamStatsConverted.Losses,
                Wins = teamStatsConverted.Wins
            };
                
            
            //Get All Stats Players And Injures Stats 
            var playerStatsNMHDataFrame = new List<NMHPlayerDataFrame>();
            foreach (var playerHolder in InternalPlayerHolderTeamA)
            {
                var playerStatsResponse = await _playerStatsService.GetById(playerHolder.PlayerId);
                var playerStatsConverted = playerStatsResponse.Data as PlayerStatsDTO;
                playerStatsNMHDataFrame.Add(new NMHPlayerDataFrame { 
                    AerialDuelsWon = playerStatsConverted.AerialDuelsWon,
                    Assists = playerStatsConverted.Assists,
                    Clearances = playerStatsConverted.Clearances,
                    DribblesCompleted = playerStatsConverted.DribblesCompleted,
                    Goals = playerStatsConverted.Goals,
                    Interceptions = playerStatsConverted.Interceptions,
                    PassAccuracy = (decimal)playerStatsConverted.PassAccuracy,
                    Tackles = playerStatsConverted.Tackles,
                });
            }
            
            
            
            //Get Injures Information for each player ?? 
            var injuresDataHolderList = new Dictionary<int, List<NMHInjurySuspension>>();
            var counter = 0;
            foreach (var playerHolder in InternalPlayerHolderTeamA)
            {
                var injuresSuspensionDataFrameList = new List<NMHInjurySuspension>();
                var injureInformationResponse = await _injuresSuspensionService.GetInjuriesSuspensionsByPlayerID(playerHolder.PlayerId);
                if (injureInformationResponse != null)
                {
                    foreach (var item in injureInformationResponse.Data)
                    {
                        var injuresSuspensionDto = item as InjuriesSuspensionsDTO;
                        var suspensionListResponse = await _injuresSuspensionService.
                            GetInjuresLinkBySuspensionId(injuresSuspensionDto.InjurySuspensionId);
                        var suspensionList = suspensionListResponse.Data;

                        foreach(var suspensionLink in  suspensionList)
                        {
                            
                            switch(suspensionLink.Severity)
                            {
                                case 0:
                                    injuresSuspensionDataFrameList.Add(new NMHInjurySuspension
                                    {
                                        Type = injuresSuspensionDto.Type,
                                        InjureSeverity = InjureSeverityEnum.Light,
                                    });
                                    break;
                                case 1:
                                    injuresSuspensionDataFrameList.Add(new NMHInjurySuspension
                                    {
                                        Type = injuresSuspensionDto.Type,
                                        InjureSeverity = InjureSeverityEnum.Mid,
                                    });
                                    break;
                                case 2:
                                    injuresSuspensionDataFrameList.Add(new NMHInjurySuspension
                                    {
                                        Type = injuresSuspensionDto.Type,
                                        InjureSeverity = InjureSeverityEnum.Critical,
                                    });
                                    break;
                            }
                        }


                    }
                }
            
                injuresDataHolderList.Add(counter, injuresSuspensionDataFrameList);
                counter++;
            }
                
            return new RegressionCalculationDataFrame
            {
                teamPlayers = playerStatsNMHDataFrame,
                refereeBias = selectedRefereeHolder.Bias,
                teamInjuriesSuspensions = injuresDataHolderList,
                teamStats = nmhTeamDataFrame
            };
        }

        private async Task<RegressionCalculationDataFrame>
           GetRegressionInformationForTeamMD(TeamHolder selectedTeamHolder, RefereeHolder selectedRefereeHolder)
        {
            // Get Team Entity
            var selectedHomeTeamResponse = await _teamService.GetById(selectedTeamHolder.TeamId);
            var selectedHomeTeam = selectedHomeTeamResponse.Data;
            selectedHomeTeam = selectedHomeTeam ?? selectedHomeTeam as TeamDTO;

            //Get Team Stats
            var teamStatsResponse = await _teamStatisticsService.GetTeamStatsByTeamID(selectedTeamHolder.TeamId);
            var teamStats = teamStatsResponse.Data;
            var teamStatsConverted = teamStats as TeamWithStatisticsDTO;
            var nmhTeamDataFrame = new NMHTeamDataFrame
            {
                Draws = teamStatsConverted.Draws,
                GoalsConceded = teamStatsConverted.GoalsConceded,
                GoalsScored = teamStatsConverted.GoalsScored,
                Losses = teamStatsConverted.Losses,
                Wins = teamStatsConverted.Wins
            };


            //Get All Stats Players And Injures Stats 
            var playerStatsNMHDataFrame = new List<NMHPlayerDataFrame>();
            foreach (var playerHolder in InternalPlayerHolderTeamA)
            {
                var playerStatsResponse = await _playerStatsService.GetById(playerHolder.PlayerId);
                var playerStatsConverted = playerStatsResponse.Data as PlayerStatsDTO;
                playerStatsNMHDataFrame.Add(new NMHPlayerDataFrame
                {
                    AerialDuelsWon = playerStatsConverted.AerialDuelsWon,
                    Assists = playerStatsConverted.Assists,
                    Clearances = playerStatsConverted.Clearances,
                    DribblesCompleted = playerStatsConverted.DribblesCompleted,
                    Goals = playerStatsConverted.Goals,
                    Interceptions = playerStatsConverted.Interceptions,
                    PassAccuracy = (decimal)playerStatsConverted.PassAccuracy,
                    Tackles = playerStatsConverted.Tackles,
                });
            }



            //Get Injures Information for each player ?? 
            var injuresDataHolderList = new Dictionary<int, List<NMHInjurySuspension>>();
            var counter = 0;
            foreach (var playerHolder in InternalPlayerHolderTeamA)
            {
                var injuresSuspensionDataFrameList = new List<NMHInjurySuspension>();
                var injureInformationResponse = await _injuresSuspensionService.GetInjuriesSuspensionsByPlayerID(playerHolder.PlayerId);
                if (injureInformationResponse != null)
                {
                    foreach (var item in injureInformationResponse.Data)
                    {
                        var injuresSuspensionDto = item as InjuriesSuspensionsDTO;
                        var suspensionListResponse = await _injuresSuspensionService.
                            GetInjuresLinkBySuspensionId(injuresSuspensionDto.InjurySuspensionId);
                        var suspensionList = suspensionListResponse.Data;

                        foreach (var suspensionLink in suspensionList)
                        {

                            switch (suspensionLink.Severity)
                            {
                                case 0:
                                    injuresSuspensionDataFrameList.Add(new NMHInjurySuspension
                                    {
                                        Type = injuresSuspensionDto.Type,
                                        InjureSeverity = InjureSeverityEnum.Light,
                                    });
                                    break;
                                case 1:
                                    injuresSuspensionDataFrameList.Add(new NMHInjurySuspension
                                    {
                                        Type = injuresSuspensionDto.Type,
                                        InjureSeverity = InjureSeverityEnum.Mid,
                                    });
                                    break;
                                case 2:
                                    injuresSuspensionDataFrameList.Add(new NMHInjurySuspension
                                    {
                                        Type = injuresSuspensionDto.Type,
                                        InjureSeverity = InjureSeverityEnum.Critical,
                                    });
                                    break;
                            }
                        }


                    }
                }

                injuresDataHolderList.Add(counter, injuresSuspensionDataFrameList);
                counter++;
            }




            //get match data
            var matchStatsList = new List<MatchStatsDTO>();

            var matchDataResponse = await _matchService.GetMatchesByTeamId(selectedTeamHolder.TeamId);
            foreach(var match in matchDataResponse.Data)
            {
               var matchStatsDto =  await _matchStatisticsService.GetMatchStatsByMatchID(match.MatchId);
                var mappedStats = matchStatsDto.Data as MatchStatsDTO;
                matchStatsList.Add(mappedStats);
            }

            return new RegressionCalculationDataFrame
            {
                teamPlayers = playerStatsNMHDataFrame,
                refereeBias = selectedRefereeHolder.Bias,
                teamInjuriesSuspensions = injuresDataHolderList,
                teamStats = nmhTeamDataFrame,
                MatchStats = matchStatsList
            };
        }


        private void Reset(object obj)
        {
            // Logic for resetting the match or form
        }

        private async void GetSelectedTeam(int selectedTeamIndex, int teamOptionIndex)
        {
            //find related team 
            if (_comboBoxItemsTeam.Count == 0)
                return;

            var selectedTeam = _comboBoxItemsTeam[selectedTeamIndex];
            if (teamOptionIndex == 1)
            {
                SelectedTeamA = selectedTeam;
            }
            else
            {
                SelectedTeamB = selectedTeam;
            }

            var allPlayerTeamLinkEntities = await _playerTeamLink.GetAll();
            if (allPlayerTeamLinkEntities.IsSuccessful)
            {
                var responseData = allPlayerTeamLinkEntities.Data;
                GetPlayerDTOOfTeam(responseData, selectedTeam.TeamId, teamOptionIndex);
            }
        }


        private async void GetPlayerDTOOfTeam(IEnumerable<PlayerTeamLinkDTO> playerTeamLinkDTOs, int selectedTeamId,
           int teamOptionIndex)
        {
            var playerList = new List<BasePlayerDTO>();
            var requiredData = playerTeamLinkDTOs.Where(playerTeamLink => playerTeamLink.TeamId == selectedTeamId);
            if (requiredData.Any())
            {
                foreach (var playerlinks in requiredData)
                {
                    var playerServiceResponse = await _playerService.GetById(playerlinks.PlayerId);
                    playerList.Add(playerServiceResponse.Data);
                }
            }
            SetPlayerOfTeam(playerList, teamOptionIndex);

        }


        private void SetPlayerOfTeam(List<BasePlayerDTO> playerList,
                int teamOptionIndex)
        {
            var playerHolderList = new List<PlayerHolder>();
            foreach (var playerBaseDTO in playerList)
            {
                var playerDto = playerBaseDTO as PlayerDTO;
                playerHolderList.Add(new PlayerHolder {
                    PlayerId=playerDto.Id ,
                    Name = playerDto.Name, 
                    ImageData = playerDto.PlayerImage.ImageData });
            }

            if (teamOptionIndex == 1)
            {
                InternalPlayerHolderTeamA.AddRange(playerHolderList);
                TeamAFirstColumnPlayers = new ObservableCollection<PlayerHolder>(playerHolderList.Take(6));
                TeamASecondColumnPlayers = new ObservableCollection<PlayerHolder>(playerHolderList.Skip(6).Take(5));
            }
            else
            {
                InternalPlayerHolderTeamB.AddRange(playerHolderList);
                TeamBFirstColumnPlayers = new ObservableCollection<PlayerHolder>(playerHolderList.Take(6));
                TeamBSecondColumnPlayers = new ObservableCollection<PlayerHolder>(playerHolderList.Skip(6).Take(5));
            }

        }


        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
