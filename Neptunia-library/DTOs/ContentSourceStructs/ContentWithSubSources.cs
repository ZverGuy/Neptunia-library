using System.Collections.Generic;
using Neptunia_library.Interfaces;
using Newtonsoft.Json;

namespace Neptunia_library.DTOs.ContentSourceStructs
{
    public class ContentWithSubSources : IContent
    {
        [JsonProperty("sourceName")]
        public string ContentSourceName { get; set; }
        [JsonProperty("name")]
        public string ContentName { get; set; }
        [JsonProperty("url")]
        public string UrlToContentPage { get; set; }
        [JsonProperty("additionalProps")]
        public Dictionary<string, string> ParserParameters { get; set; }
        [JsonProperty("subSources")]
        public List<SubContentSource> SubContentSources { get; set; }

        public ContentWithSubSources()
        {
            ParserParameters = new Dictionary<string, string>();
            SubContentSources = new List<SubContentSource>();
        }
    }
}