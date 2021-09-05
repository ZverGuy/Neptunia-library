using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Neptunia_library.Builders;
using Neptunia_library.Builders.BuilderOptions;
using Neptunia_library.ContentSources;
using Neptunia_library.DataBaseProviders;
using Neptunia_library.DTOs;
using Neptunia_library.Enums;
using Neptunia_library.Interfaces;
using OpenQA.Selenium;

namespace Neptunia_library
{
    public partial class ParserEngine
    {
        private ICacheService? _service;
        private IUserAgentStorage? _userAgentStorage;
        private ISearchEngine? _searchEngine;
        private readonly IServiceProvider _serviceProvider;

        internal ParserEngine(IServiceCollection serviceCollection)
        {

            _serviceProvider = serviceCollection.BuildServiceProvider();

            _service = _serviceProvider.GetService<ICacheService>();
            _userAgentStorage = _serviceProvider.GetService<IUserAgentStorage>();
            _searchEngine = _serviceProvider.GetService<ISearchEngine>();


        }




        public DataBaseProviderInfo GetContentInfoFromDataBaseProvider(string contentname, ContentTypeEnum contentType)
        {
            DataBaseProviderInfo info = new DataBaseProviderInfo();
            List<IDataBaseProvider> dataBaseProviders = _serviceProvider.GetServices<IDataBaseProvider>()
                .Where(x => x.ContentType == contentType).ToList();

            foreach (IDataBaseProvider provider in dataBaseProviders)
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





        public IEnumerable<IContentSource> GetContentInfoFromContentSources(string contentName,
            ContentTypeEnum contentType)
        {
            List<IContentSource> resultList = new List<IContentSource>();
            Regex urlregex = new Regex(@"\/\/(.+?)\/");
            List<IContentSourceProvider> validProviders = _serviceProvider.GetServices<IContentSourceProvider>()
                .Where(x => x.ContentType == contentType).ToList();

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