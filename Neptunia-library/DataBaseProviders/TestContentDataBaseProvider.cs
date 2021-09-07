using System;
using System.Collections.Generic;
using Neptunia_library.DTOs;
using Neptunia_library.Interfaces;

namespace Neptunia_library.DataBaseProviders
{
    public class TestContentDataBaseProvider : IContentDataBaseProvider
    {
        public IEnumerable<string> ContentTypes => new[]
        {
            "Anime"
        };

        public IEnumerable<string> Languages => new[]
        {
            "Russian"
        };
        public DataBaseProviderInfo GetInfoFromDataBaseService(string contentName, string userAgent = null)
        {
            throw new Exception();
        }
    }
}