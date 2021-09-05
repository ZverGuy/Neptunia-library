using System.Collections.Generic;

namespace Neptunia_library.Interfaces
{
    public interface IContentSource
    {
        string ContentSourceName { get; set; }
        string ContentName { get; set; }
        string UrlToContentPage { get; set; }
        Dictionary<string, string> ParserParameters { get; set; }
    }
}