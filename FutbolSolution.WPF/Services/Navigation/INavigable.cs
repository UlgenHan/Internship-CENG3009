using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.WPF.Services.Navigation
{
    public interface INavigable
    {
        void OnNavigatedTo(object parameter);
        void OnNavigatedFrom(); //  handle cleanup when navigating away
        void Refresh();
    }
}
