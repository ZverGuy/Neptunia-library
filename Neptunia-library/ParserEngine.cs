using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using Serilog;

namespace Neptunia_library
{
    public partial class ParserEngine
    {
        private ICacheService? _service;
        private IUserAgentStorage? _userAgentStorage;
        private ISearchEngine? _searchEngine;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        internal ParserEngine(IServiceCollection serviceCollection)
        {

            _serviceProvider = serviceCollection.BuildServiceProvider();

            _service = _serviceProvider.GetService<ICacheService>();
            _userAgentStorage = _serviceProvider.GetService<IUserAgentStorage>();
            _searchEngine = _serviceProvider.GetService<ISearchEngine>();
            _logger = _serviceProvider.GetService<ILogger>();


        }

        public DataBaseProviderInfo SearchContentOnDataBase(ContentRequestSettings settings)
        {
            DataBaseProviderInfo result = null;
            IEnumerable<IContentDataBaseProvider> validProviders = _serviceProvider
                .GetServices<IContentDataBaseProvider>()
                .Where(provider => provider.Languages.Contains(settings.Language) &&
                                   provider.ContentTypes.Contains(settings.ContentType));

            foreach (IContentDataBaseProvider provider in validProviders)
            {
                try
                {
                    result = provider.GetInfoFromDataBaseService(settings.ContentName);
                    return result;
                }
                catch (Exception e)
                {
                    _logger.Warning($"[Warning][DataBaseProvider] \"{provider.ToString()}\" Failed to get info. Go Next");
                    continue;
                }
            }

            if (result == null)
            {
                _logger.Error("[Error][DataBaseProvider] Failed to get info from all DataBaseProviders");
            }

            return result;

        }

        public async Task<DataBaseProviderInfo> SearchContentOnDataBaseAsync(ContentRequestSettings settings)
        {
            DataBaseProviderInfo result = null;
            IEnumerable<IContentDataBaseProvider> validProviders = _serviceProvider
                .GetServices<IContentDataBaseProvider>()
                .Where(provider => provider.Languages.Contains(settings.Language) &&
                                   provider.ContentTypes.Contains(settings.ContentType));


            IContentDataBaseProvider first = validProviders.First();
            
            IContentDataBaseProvider second = validProviders.Skip(1).First();
            
                Task<DataBaseProviderInfo> asyncresult =
                NeptuniaTaskFactory.CreateTaskWithExceptionChain<DataBaseProviderInfo>(
                    mainfunc: () =>  first.GetInfoFromDataBaseService(settings.ContentName),
                    funcsAfterExcepion: new Func<DataBaseProviderInfo>[]
                    {
                        () => second.GetInfoFromDataBaseService(settings.ContentName)
                    }, continualtion: TaskScheduler.Default);


            result = await asyncresult;

               if (result == null)
            {
                _logger.Error("[DataBaseProviders] Failed to get info from all DataBaseProviders");
            }

            return result;

        }


        public IEnumerable<IContent> SearchContentByContentProviders(ContentRequestSettings settings)
        {
            List<IContent> result = new List<IContent>();
            Regex urlregex = new Regex(@"\/\/(.+?)\/");
            IEnumerable<IContentProvider> validProviders = _serviceProvider.GetServices<IContentProvider>()
                .AsParallel()
                .Where(provider => provider.ContentTypes.Contains(settings.ContentType) &&
                                   provider.Languages.Contains(settings.Language));
            IEnumerable<SearchEngineResult> searchEngineResults = _searchEngine.GetSearchResults(settings.ContentName, validProviders);

            foreach (SearchEngineResult searchEngineResult in searchEngineResults)
            {
                foreach (IContentProvider provider in validProviders)
                {
                   
                        if (urlregex.Match(searchEngineResult.Url).Groups[1].Value == provider.SiteUrl)
                        {
                            try
                            {
                                result.Add(provider.GetContent(searchEngineResult.Url));
                            }
                            catch (Exception e)
                            {
                                _logger.Warning($"[ContentProviders] \"{provider.ToString()}\" failed to parse content.\r\n Url: {searchEngineResult.Url} \r\nException: {e} ");
                                continue;
                            }
                        }
                    
                   
                }
            }
            
            if (result.Count == 0)
            {
                _logger.Error("[ContentProviders] failed get info from all contentproviders");
            }

            return result;
            
        }


        public async Task<IEnumerable<IContent>> SearchContentByContentProvidersAsync(ContentRequestSettings settings)
        {
            List<IContent> result = new List<IContent>();
            Regex urlregex = new Regex(@"\/\/(.+?)\/");
            IEnumerable<IContentProvider> validProviders = _serviceProvider.GetServices<IContentProvider>()
                .AsParallel()
                .Where(provider => provider.ContentTypes.Contains(settings.ContentType) &&
                                   provider.Languages.Contains(settings.Language));
            IEnumerable<SearchEngineResult> searchEngineResults = _searchEngine.GetSearchResults(settings.ContentName, validProviders);
            List<Task<IContent>> parsertasks = new List<Task<IContent>>();

            foreach (SearchEngineResult searchEngineResult in searchEngineResults)
            {
                foreach (IContentProvider provider in validProviders)
                {

                    if (urlregex.Match(searchEngineResult.Url).Groups[1].Value == provider.SiteUrl)
                    {
                        Task<IContent> task = Task<IContent>.Factory
                            .StartNew(() => provider.GetContent(searchEngineResult.Url))
                            .ContinueWith<IContent>((task1) =>
                                {
                                    return null;
                                }, 
                                TaskContinuationOptions.OnlyOnFaulted);
                        parsertasks.Add(task);

                    }

                }

            }

            Task.WaitAll(parsertasks.ToArray());

            foreach (var task in parsertasks)
            {
                if (task.Exception == null)
                {
                    result.Add(task.Result);
                }
            }

            if (result.Count == 0)
            {
                _logger.Error("[ContentProviders] failed get info from all contentproviders");
                _logger.Debug("adad");
            }

            return result;
            
        }
        }

    }