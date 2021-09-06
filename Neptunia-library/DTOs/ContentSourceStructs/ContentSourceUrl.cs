#nullable enable
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Neptunia_library.DTOs.ContentSourceStructs
{
    public class ContentSourceUrl
    {
        [JsonProperty("url")]
        public string UrlToContent { get; set; }
        [JsonProperty("itsCached")]
        public bool ItsCached { get; set; }
        [JsonProperty("cache")]
        public Dictionary<string, object>? Cache { get; set; }
    }
}