using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Neptunia_library.Enums;
using Neptunia_library.Interfaces;
using Newtonsoft.Json;

namespace Neptunia_library.DTOs
{
    public class ParsedContent
    {
        [JsonProperty("contentType")]
        public string? ContentType { get; private set; }
        
        [JsonProperty("name")]
        public string Name { get; private set; }
        
        [JsonProperty("description")]
        public string Description { get; private set; }
        
        [JsonProperty("userScore")]
        public string UserScore { get; private set; }
        
        [JsonProperty("url")]
        public string UrlFromDataBaseProvider { get; private set; }
        
        [JsonProperty("sources")]
        public IEnumerable<IContent> ContentSources { get; private set; }

        internal ParsedContent(string name, string description, string userScore, string urlFromDataBaseProvider,
            IEnumerable<IContent> contentSources, [AllowNull] string contentType = default)
        {
            Name = name;
            Description = description;
            UserScore = userScore;
            UrlFromDataBaseProvider = urlFromDataBaseProvider;
            ContentSources = contentSources;
            ContentType = contentType;
            
            if (ContentType == default)
            {
                ContentType  = "other";
            }
        }

    }
}