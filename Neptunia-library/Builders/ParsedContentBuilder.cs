using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Neptunia_library.DTOs;
using Neptunia_library.Enums;
using Neptunia_library.Interfaces;

namespace Neptunia_library.Builders
{
    public class ParsedContentBuilder
    {
        private DataBaseProviderInfo _dataBaseProviderInfo;
        private IEnumerable<IContent> _contentSources;

        public ParsedContentBuilder SetMainInfo(DataBaseProviderInfo info)
        {
            _dataBaseProviderInfo = info;
            return this;
        }

        public ParsedContentBuilder SetContentFromContentSources(IEnumerable<IContent> contentSources)
        {
            _contentSources = contentSources;
            return this;
        }

        public ParsedContent BuildContent(string contentType)
        {
            ParsedContent content = new ParsedContent(
                _dataBaseProviderInfo.Name,
                _dataBaseProviderInfo.Description,
                _dataBaseProviderInfo.UserScore,
                _dataBaseProviderInfo.UrlToContent,
                _contentSources, contentType);
            return content;
        }

    }
}