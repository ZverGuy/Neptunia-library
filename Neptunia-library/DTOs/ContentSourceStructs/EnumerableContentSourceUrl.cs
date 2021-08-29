#nullable enable
using System;
using System.Collections.Generic;

namespace Neptunia_library.DTOs.ContentSourceStructs
{
    public struct EnumerableContentSourceUrl
    {
        public string UrlToContent { get; set; }
        public bool ItsCached { get; set; }
        public Dictionary<string, object>? Cache { get; set; }
    }
}