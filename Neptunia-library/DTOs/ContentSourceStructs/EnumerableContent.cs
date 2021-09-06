using System.Collections.Generic;
using Neptunia_library.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Neptunia_library.DTOs.ContentSourceStructs
{
    public class EnumerableContent : IContent
    {
        [JsonProperty("sourceName")]
        public string ContentSourceName { get; set; }
        
        [JsonProperty("name")]
        public string ContentName { get; set; }
        
        [JsonProperty("url")]
        public string UrlToContentPage { get; set; }
        
        [JsonProperty("additionalProps")]
        public Dictionary<string, string> ParserParameters { get; set; }
        [JsonProperty("content")]
        public List<ContentSourceUrl> Urls { get; set; }

        public EnumerableContent()
        {
            ParserParameters = new Dictionary<string, string>();
            Urls = new List<ContentSourceUrl>();
        }
    }
}