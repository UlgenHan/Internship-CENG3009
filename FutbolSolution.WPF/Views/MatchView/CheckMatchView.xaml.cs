using FutbolSolution.WPF.ViewModels.MatchView;
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
    /// Interaction logic for CheckMatchView.xaml
    /// </summary>
    public partial class CheckMatchView : UserControl
    {
        public CheckMatchView(CheckMatchViewModel checkMatchViewModel)
        {
            InitializeComponent();
            DataContext = checkMatchViewModel;
        }
    }
}
