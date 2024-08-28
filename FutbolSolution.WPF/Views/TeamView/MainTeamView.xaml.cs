using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.ViewModels.TeamViewModel;
using System.Windows.Controls;

namespace FutbolSolution.WPF.Views.TeamView
{
    /// <summary>
    /// Interaction logic for MainTeamView.xaml
    /// </summary>
    public partial class MainTeamView : UserControl, INavigable
    {
        public MainTeamView(MainTeamViewModel mainTeamViewModel)
        {
            InitializeComponent();
            DataContext = mainTeamViewModel;
        }

        public void OnNavigatedFrom()
        {
           
        }

        public void OnNavigatedTo(object parameter)
        {
            
        }

        public void Refresh()
        {
           var viewModel = DataContext as MainTeamViewModel;
            viewModel?.Refresh();
        }
    }
}
