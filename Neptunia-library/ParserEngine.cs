using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
    public class ParserEngine
    {
        private readonly List<ValueTuple<ContentTypeEnum, IContentSourceProvider>> _contentSourceProviders;
        private readonly List<(ContentTypeEnum, IDataBaseProvider)> _dataBaseProviders;
        private readonly ICacheService _service;
        private readonly IUserAgentStorage _userAgentStorage;
        private readonly ISearchEngine _searchEngine;
        private readonly IServiceProvider _serviceProvider;

        internal ParserEngine(ContentSourceProviderOptions contentOptions, 
                              DataBaseProviderOptions dataBaseOptions, 
                              IServiceCollection serviceCollection, 
                              [AllowNull] ISearchEngine searchEngine = null, 
                              [AllowNull] ICacheService service = null, 
                              [AllowNull] IUserAgentStorage userAgentStorage = null, 
                              [AllowNull] IWebDriver webDriver = null)
        {
            _contentSourceProviders = new List<(ContentTypeEnum, IContentSourceProvider)>();
            _dataBaseProviders = new List<(ContentTypeEnum, IDataBaseProvider)>();
            
            _serviceProvider = serviceCollection.BuildServiceProvider();
            
            //Getting ContentProvicers
            foreach (var variable in contentOptions.ContentSourceProviders)
            {
                IContentSourceProvider contentprovider = (IContentSourceProvider)Activator.CreateInstance(variable.Item2);
                contentprovider.OnGettingDependencyServices(_serviceProvider);
                _contentSourceProviders.Add((variable.Item1, contentprovider));
            }
            
            //Getting DataBaseProviders
            foreach (var variable in dataBaseOptions.DataBaseProvider)
            {
                IDataBaseProvider databaseprovider = (IDataBaseProvider)Activator.CreateInstance(variable.Item2);
                databaseprovider.OnGettingDependencyServices(_serviceProvider);
                _dataBaseProviders.Add((variable.Item1, databaseprovider));
            }
        }

        public DataBaseProviderInfo GetContentInfoFromDataBaseProvider(string contentname, ContentTypeEnum contentType)
        {
            DataBaseProviderInfo info = new DataBaseProviderInfo();
            List<IDataBaseProvider> dataBaseProviders = _dataBaseProviders
                .Where(x => x.Item1.HasFlag(contentType))
                .Select(x => x.Item2).ToList();
            
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
                    continue;      //sorry
                }
            }

            return default;
        }





        public IEnumerable<IContentSource> GetContentInfoFromContentSources(string contentName,
            ContentTypeEnum contentType)
        {
            List<IContentSource> resultList = new List<IContentSource>();
            List<IContentSourceProvider> validProviders = _contentSourceProviders
                .Where(x => x.Item1.HasFlag(contentType))
                .Select(y => y.Item2)
                .ToList();

            SearchEngineResult searchEngineResult = _searchEngine.GetSearchResults(contentName, validProviders).First();
            foreach (IContentSourceProvider provider in validProviders)
            {
                try
                {
                    IContentSource parserResult = provider.GetContent(searchEngineResult.Url);
                    resultList.Add(parserResult);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    continue;      //sorry
                }
            }

            return resultList;
        }
        }

}