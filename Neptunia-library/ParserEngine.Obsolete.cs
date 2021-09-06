using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using Neptunia_library.Builders;
using Neptunia_library.Builders.BuilderOptions;
using Neptunia_library.DTOs;
using Neptunia_library.Enums;
using Neptunia_library.Interfaces;

namespace Neptunia_library
{
    public partial class ParserEngine
    {
        [Obsolete("Use method with ContentDataBaseRequestSettings")]
        public DataBaseProviderInfo GetContentInfoFromDataBaseProvider(string contentname, string contentType)
        {
            DataBaseProviderInfo info = new DataBaseProviderInfo();
            List<IContentDataBaseProvider> dataBaseProviders = _serviceProvider.GetServices<IContentDataBaseProvider>()
                .Where(x => x.ContentTypes.Contains(contentType)).ToList();

            foreach (IContentDataBaseProvider provider in dataBaseProviders)
            {
                try
                {
                    info = provider.GetInfoFromDataBaseService(contentname);
                    return info;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    continue; //sorry
                }
            }

            return default;
        }



        
        [Obsolete("Use method with ContentRequestSettings")]
        public IEnumerable<IContent> GetContentInfoFromContentSources(string contentName,
            string contentType)
        {
            List<IContent> resultList = new List<IContent>();
            Regex urlregex = new Regex(@"\/\/(.+?)\/");
            List<IContentProvider> validProviders = _serviceProvider.GetServices<IContentProvider>()
                .Where(x => x.ContentTypes.Contains(contentType)).ToList();

            IEnumerable<SearchEngineResult> searchEngineResult =
                _searchEngine.GetSearchResults(contentName, validProviders);
            foreach (SearchEngineResult result in searchEngineResult)
            {
                try
                {
                    foreach (var contentSourceProvider in validProviders)
                    {
                        if (urlregex.Match(result.Url).Groups[1].Value == contentSourceProvider.SiteUrl)
                        {
                            resultList.Add(contentSourceProvider.GetContent(result.Url));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    continue;
                }
              
            }

            return resultList;
        }
    }
}