using System.Collections.Generic;
using Neptunia_library.Interfaces;

namespace Neptunia_library.DTOs.ContentSourceStructs
{
    public struct ContentSourceWithSubSources : IContentSource
    {
        public string ContentSourceName { get; set; }
        public Dictionary<string, string> ParserParameters { get; set; }
    }
}