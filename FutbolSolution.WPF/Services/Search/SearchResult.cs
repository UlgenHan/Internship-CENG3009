using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FutbolSolution.WPF.Services.Search
{
    public class SearchResult
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Type UserControlType { get; set; }
    }
}
