using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.ViewModels.PlayerViewModel;
using System.Windows.Controls;


namespace FutbolSolution.WPF.Views.PlayerView
{
    /// <summary>
    /// Interaction logic for CheckPlayerView.xaml
    /// </summary>
    public partial class CheckPlayerView : UserControl, INavigable
    {
        public CheckPlayerView(CheckPlayerViewModel checkPlayerViewModel)
        {
            InitializeComponent();
            DataContext = checkPlayerViewModel;
        }

        public void OnNavigatedFrom()
        {
           
        }

        public void OnNavigatedTo(object parameter)
        {
            
        }

        public void Refresh()
        {
            var viewModel = DataContext as CheckPlayerViewModel;
            if (viewModel != null)
            {
                viewModel.Refresh();
            }
        }
    }
}
