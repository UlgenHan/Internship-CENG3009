using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.WPF.Services.Search
{
    public interface ISearchService
    {
        IEnumerable<SearchResult> Search(string query);
    }
}
