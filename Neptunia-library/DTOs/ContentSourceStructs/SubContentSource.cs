using System.Collections.Generic;
using Newtonsoft.Json;

namespace Neptunia_library.DTOs.ContentSourceStructs
{
    public class SubContentSource
    {
        [JsonProperty("subSourceName")]
        public string SubContentSourceName { get; set; }
        [JsonProperty("content")]
        public List<ContentSourceUrl> Urls { get; set; }

        public SubContentSource()
        {
            Urls = new List<ContentSourceUrl>();
        }
    }
}