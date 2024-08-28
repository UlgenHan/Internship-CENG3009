using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.ViewModels.MatchViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FutbolSolution.WPF.Views.MatchView
{
    /// <summary>
    /// Interaction logic for MainMatchView.xaml
    /// </summary>
    public partial class MainMatchView : UserControl, INavigable
    {
        public MainMatchView(MainMatchViewModel mainMatchViewModel)
        {
            InitializeComponent();
            DataContext = mainMatchViewModel;
        }

        public void OnNavigatedFrom()
        {
           
        }

        public void OnNavigatedTo(object parameter)
        {
           
        }

        public void Refresh()
        {
            var viewModel = (MainMatchViewModel)DataContext;
            viewModel.Refresh();
        }
    }
}
