using System;
using System.Collections.Generic;
using Neptunia_library.Interfaces;

namespace Neptunia_library.DTOs.ContentSourceStructs
{
    public struct SimpleContentSource : IContentSource
    {
        public string ContentSourceName { get; set; }
        public Dictionary<string, string> ParserParameters { get; set; }
        public string UrlToContent { get; set; }
        public bool ItsCached { get; set; }
        public ValueTuple<string, object> Cache { get; set; }
    }
}