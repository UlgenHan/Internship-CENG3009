using FutbolSolution.Core.DTOs.MatchDTOs;
using FutbolSolution.Core.DTOs.RefereeDTOs;
using FutbolSolution.Core.Services;
using FutbolSolution.Service.Services;
using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.Utils;
using FutbolSolution.WPF.Views.RefereeView;
using FutbolSolution.WPF.Windows;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows.Input;

namespace FutbolSolution.WPF.ViewModels.RefereeViewModel
{
    public class MainRefereeViewModel : INotifyPropertyChanged, INavigable
    {
        // Required Attributes
        private ObservableCollection<RefereeDTO> _referees;
        private string _searchText;
        private string _selectedFilterOption;
        private ICollectionView _refereesView;
        private readonly INavigationService _navigationService;
        private readonly IRefereeService _refereeService;
        private readonly IMatchService _matchService;
        private readonly IMatchStatisticsService _matchStatisticsService;


        //Definition of Properties
        public ObservableCollection<RefereeDTO> Referees
        {
            get { return _referees; }
            set
            {
                _referees = value;
                OnPropertyChanged();
            }
        }

        public ICollectionView RefereesView
        {
            get { return _refereesView; }
            set
            {
                _refereesView = value;
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

                    OnPropertyChanged(); // Notify UI to update
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

        public MainRefereeViewModel(INavigationService navigationService, IRefereeService refereeService,
            IMatchService matchService, IMatchStatisticsService matchStatisticsService)
        {
            _navigationService = navigationService;
            _refereeService = refereeService;
            _matchService = matchService;
            _matchStatisticsService = matchStatisticsService;

            Referees = new ObservableCollection<RefereeDTO>();


            // I am giving literal filter names , I need to handle it from outside of this document
            // Not Factored
            FilterOptions = new ObservableCollection<string> { "Name", "Surname", "Experience" }; 
            InitializeData();

            //Command Definition made , but due to I defined it as generic , I have problems like object parameter passing ??
            //Not Factored
            UpdateCommand = new RelayCommand<RefereeDTO>(UpdateReferee);
            DeleteCommand = new RelayCommand<RefereeDTO>(DeleteReferee);
            AddCommand = new RelayCommand<object>(AddReferee);


            //Default Configuration Snippet , Maybe I can do it inside a init function.(Not Sure)
            //Not Factored
            RefereesView = CollectionViewSource.GetDefaultView(Referees);
            RefereesView.Filter = FilterReferees;
            SelectFilterOptionCommand = new RelayCommand<object>(SelectFilterOption);
            SelectedFilterOption = FilterOptions[0]; // Set default filter option
            SelectedFilterOption = "Name";
        }

        private void SelectFilterOption(object filterOption)
        {
            var filterOptionConverted = filterOption as string;
            SelectedFilterOption = filterOptionConverted;
        }

        // For DataGrid Initialize Purpose 
        private async void InitializeData()
        {
            var response = await _refereeService.GetAll();
            var refereeDtos = response.Data;
            foreach (RefereeDTO r in refereeDtos)
            {
                Referees.Add(r);
            }
        }
        
        private bool FilterReferees(object item)
        {
            if (item is RefereeDTO referee)
            {
                if (string.IsNullOrEmpty(SearchText)) return true;

                switch (SelectedFilterOption)
                {
                    case "Name":
                        return referee.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0;
                    case "Surname":
                        return referee.Surname.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0;
                    case "Experience":
                        return referee.ExperienceYears.ToString().IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0;
                    default:
                        return true;
                }
            }
            return false;
        }

        private void ApplyFilter()
        {
            RefereesView.Refresh();
        }

        private void UpdateReferee(object parameter)
        {
            var referee = (RefereeDTO)parameter;

            _navigationService.NavigateTo<UpdateRefereeView>(referee);
        }

        private async void DeleteReferee(object parameter)
        {

            var referee = (RefereeDTO)parameter;
            

            //related data ==>> Match data should be also deleted,
            var matchResponse = await _matchService.GetMatchesByRefereeId(referee.RefereeId);
                                
            if(matchResponse.IsSuccessful && matchResponse.StatusCode)
            {
                foreach(var item in matchResponse.Data)
                {
                    //MatchStats also should be deleted
                    var matchStatsResponse = await _matchStatisticsService.GetMatchStatsByMatchID(item.MatchId);
                    var matchStats = matchStatsResponse.Data as MatchStatsDTO;

                    await _matchStatisticsService.Delete(matchStats);
                    await _matchService.Delete(item);
                }
            }

            var response = await _refereeService.Delete(referee);
            if (response.StatusCode)
            {
                _referees.Remove(referee);
            }
        }

        private void AddReferee(object _)
        {
            _navigationService.NavigateTo<CreateRefereeView>(null);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnNavigatedTo(object parameter)
        {
            //Due to interface problem ?? I didn't find a new solution
        }

        public void OnNavigatedFrom()
        {
            //Due to interface problem ?? I didn't find a new solution
        }

        public void Refresh()
        {
            Referees.Clear();
            InitializeData();
        }
    }
}
