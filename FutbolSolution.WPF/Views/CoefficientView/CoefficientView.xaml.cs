using FutbolSolution.WPF.ViewModels.CoefficientViewModel;
using System.Windows.Controls;


namespace FutbolSolution.WPF.Views.CoefficientView
{
    /// <summary>
    /// Interaction logic for CoefficientView.xaml
    /// </summary>
    public partial class CoefficientView : UserControl
    {
        public CoefficientView(CoefficientViewModel coefficientViewModel)
        {
            InitializeComponent();
            DataContext = coefficientViewModel;
        }
    }
}
