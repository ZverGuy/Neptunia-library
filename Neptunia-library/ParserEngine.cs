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
    public partial class ParserEngine
    {
        private readonly List<ValueTuple<ContentTypeEnum, IContentSourceProvider>> _contentSourceProviders;
        private readonly List<(ContentTypeEnum, IDataBaseProvider)> _dataBaseProviders;
        private ICacheService _service;
        private IUserAgentStorage _userAgentStorage;
        private ISearchEngine _searchEngine;
        private readonly IServiceProvider _serviceProvider;

        internal ParserEngine(ContentSourceProviderOptions contentOptions, 
                              DataBaseProviderOptions dataBaseOptions, 
                              IServiceCollection serviceCollection)
        {
            _contentSourceProviders = new List<(ContentTypeEnum, IContentSourceProvider)>();
            _dataBaseProviders = new List<(ContentTypeEnum, IDataBaseProvider)>();
            _serviceProvider = serviceCollection.BuildServiceProvider();
            
            
            
            //Getting ContentProvicers
            InitContentSourceProviders(contentOptions.ContentSourceProviders);
            
            //Getting DataBaseProviders
            InitDataBaseProviders(dataBaseOptions.DataBaseProvider);
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