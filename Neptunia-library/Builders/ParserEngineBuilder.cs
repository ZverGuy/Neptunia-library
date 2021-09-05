using System;
using Microsoft.Extensions.DependencyInjection;
using Neptunia_library.Builders.BuilderOptions;
using Neptunia_library.Interfaces;
using OpenQA.Selenium;

namespace Neptunia_library.Builders
{
    public class ParserEngineBuilder
    {
        private DataBaseProviderOptions _dataBaseProviderOptions;
        private ContentSourceProviderOptions _contentSourceProviderOptions;
        private ICacheService _cacheService;
        private IUserAgentStorage _userAgentStorage;
        private IWebDriver _webDriver;
        private ISearchEngine _searchEngine;
        private IServiceCollection _dependencyInjection;

        public ParserEngineBuilder ConfigureDataBaseProviders(Action<DataBaseProviderOptions> options)
        {
            DataBaseProviderOptions dataBaseProviderOptions = new DataBaseProviderOptions();
            options.Invoke(dataBaseProviderOptions);
            _dataBaseProviderOptions = dataBaseProviderOptions;
            return this;
        }

        public ParserEngineBuilder ConfigureContentSourceProviders(Action<ContentSourceProviderOptions> options)
        {
            ContentSourceProviderOptions contentSourceProviderOptions = new ContentSourceProviderOptions();
            options.Invoke(contentSourceProviderOptions);
            _contentSourceProviderOptions = contentSourceProviderOptions;
            return this;
        }
        
        ///<summary>
        /// Configurating Services (Dependencies) for DataBase Or ContentSource Providers.
        /// </summary>
        public ParserEngineBuilder ConfigureServices(Action<IServiceCollection> config)
        {
            if (_dependencyInjection == null)
            {
                _dependencyInjection = new ServiceCollection();
            }
            config.Invoke(_dependencyInjection);
            return this;
        }

        public ParserEngineBuilder SetCacheService(ICacheService cacheService)
        {
            _cacheService = cacheService;
            return this;
        }

        public ParserEngineBuilder SetUserAgentStorage(IUserAgentStorage storage)
        {
            _userAgentStorage = storage;
            return this;
        }

        public ParserEngineBuilder SetWebDriver(IWebDriver driver)
        {
            _webDriver = driver;
            return this;
        }

        public ParserEngineBuilder SetSearchEngine(ISearchEngine searchEngine)
        {
            _searchEngine = searchEngine;
            return this;
        }
        
        
        

        public ParserEngine Build()
        {
            ParserEngine engine =
                new ParserEngine(_contentSourceProviderOptions, _dataBaseProviderOptions, _dependencyInjection,_searchEngine, _cacheService, _userAgentStorage, _webDriver);
            return engine;

        }

        public FluentParserEngine BuildFluent()
        {
            ParserEngine engine =
                new ParserEngine(_contentSourceProviderOptions, _dataBaseProviderOptions,_dependencyInjection,_searchEngine, _cacheService, _userAgentStorage, _webDriver);
            FluentParserEngine fluentParserEngine = new FluentParserEngine(engine);
            return fluentParserEngine;
        }
    }
}