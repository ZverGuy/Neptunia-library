using System.Collections.Generic;
using Neptunia_library.DTOs;

namespace Neptunia_library.Interfaces
{
    public interface ISearchEngine
    {
        IEnumerable<SearchEngineResult> GetSearchResults(string searchquery, IEnumerable<IContentProvider> sites);
    }
}