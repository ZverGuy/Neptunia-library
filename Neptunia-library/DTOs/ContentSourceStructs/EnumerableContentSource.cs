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
        public List<ContentSourceUrl> Urls { get; set; }

        public EnumerableContentSource()
        {
            ParserParameters = new Dictionary<string, string>();
            Urls = new List<ContentSourceUrl>();
        }
    }
}