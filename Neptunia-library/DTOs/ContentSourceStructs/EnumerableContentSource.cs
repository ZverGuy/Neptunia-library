using System.Collections.Generic;
using Neptunia_library.Interfaces;

namespace Neptunia_library.DTOs.ContentSourceStructs
{
    public class EnumerableContentSource : IContentSource
    {
        public string ContentSourceName { get; set; }
        public string ContentName { get; set; }
        public string UrlToContentPage { get; set; }
        public string UrlToContent { get; set; }
        public Dictionary<string, string> ParserParameters { get; set; }
        public IEnumerable<ContentSourceUrl> Urls { get; set; }
    }
}