using FutbolSolution.WPF.ViewModels.MatchViewModel;
using System.Windows.Controls;


namespace FutbolSolution.WPF.Views.MatchView
{
    /// <summary>
    /// Interaction logic for UpdateMatchView.xaml
    /// </summary>
    public partial class UpdateMatchView : UserControl
    {
        public UpdateMatchView(UpdateMatchViewModel updateMatchViewModel)
        {
            InitializeComponent();
            DataContext = updateMatchViewModel;
        }
    }
}
