using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Neptunia_library.Enums;
using Neptunia_library.Interfaces;

namespace Neptunia_library.DTOs
{
    public struct ParsedContent
    {
        public ContentTypeEnum? ContentType { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string UserScore { get; private set; }
        public string UrlFromDataBaseProvider { get; private set; }
        public IEnumerable<IContentSource> ContentSources { get; private set; }

        internal ParsedContent(string name, string description, string userScore, string urlFromDataBaseProvider,
            IEnumerable<IContentSource> contentSources, [AllowNull] ContentTypeEnum contentType = default)
        {
            Name = name;
            Description = description;
            UserScore = userScore;
            UrlFromDataBaseProvider = urlFromDataBaseProvider;
            ContentSources = contentSources;
            ContentType = contentType;
            
            if (ContentType == default)
            {
                ContentType  = ContentTypeEnum.Other;
            }
        }

    }
}