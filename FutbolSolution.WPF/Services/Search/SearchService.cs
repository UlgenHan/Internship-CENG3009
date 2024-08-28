using System.Collections.Generic;

namespace FutbolSolution.WPF.Services.Search
{
    public class SearchService : ISearchService
    {
    
       
        public IEnumerable<SearchResult> Search(string query)
        {
            return new List<SearchResult>();
        }
    }
}
