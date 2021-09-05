using System;
using System.Collections.Generic;
using Neptunia_library.Interfaces;

namespace Neptunia_library.DTOs.ContentSourceStructs
{
    public class SimpleContentSource : IContentSource
    {
        public string ContentSourceName { get; set; }
        public string ContentName { get; set; }
        public string UrlToContentPage { get; set; }
        public Dictionary<string, string> ParserParameters { get; set; }
        public ContentSourceUrl Source;
    }
}