using FutbolSolution.WPF.ViewModels.RefereeViewModel;
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

namespace FutbolSolution.WPF.Views.RefereeView
{
    /// <summary>
    /// Interaction logic for CreateRefereeView.xaml
    /// </summary>
    public partial class CreateRefereeView : UserControl
    {
        public CreateRefereeView(CreateRefereeViewModel createRefereeViewModel)
        {
            InitializeComponent();
            DataContext = createRefereeViewModel;
        }
    }
}
