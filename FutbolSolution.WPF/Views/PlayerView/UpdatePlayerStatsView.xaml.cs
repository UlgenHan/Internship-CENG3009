using FutbolSolution.WPF.ViewModels.PlayerViewModel;
using System.Windows.Controls;


namespace FutbolSolution.WPF.Views.PlayerView
{
    /// <summary>
    /// Interaction logic for UpdatePlayerStatsView.xaml
    /// </summary>
    public partial class UpdatePlayerStatsView : UserControl
    {
        public UpdatePlayerStatsView(UpdatePlayerStatsViewModel updatePlayerStatsViewModel)
        {
            InitializeComponent();
            DataContext = updatePlayerStatsViewModel;
        }
    }
}
