using System;
using Microsoft.Extensions.DependencyInjection;
using Neptunia_library.Builders.BuilderOptions;
using Neptunia_library.Interfaces;
using Neptunia_library.SearchEngines;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Neptunia_library.Builders
{
    public class ParserEngineBuilder
    {
        private DataBaseProviderOptions _dataBaseProviderOptions;
        private ContentSourceProviderOptions _contentSourceProviderOptions;
        private Type _cacheService;
        private Type _userAgentStorage;
        private Type _webDriver;
        private DriverOptions _webDriverOptions;
        private Type _searchEngineType;
        private IServiceCollection _dependencyInjection;

        public ParserEngineBuilder()
        {
            _dependencyInjection = new ServiceCollection();
        }

        #region BuilderConfiguration

        public ParserEngineBuilder ConfigureDataBaseProviders(Action<DataBaseProviderOptions> options)
        {
            DataBaseProviderOptions dataBaseProviderOptions = new DataBaseProviderOptions(_dependencyInjection);
            options.Invoke(dataBaseProviderOptions);
            _dataBaseProviderOptions = dataBaseProviderOptions;
            return this;
        }

        public ParserEngineBuilder ConfigureContentSourceProviders(Action<ContentSourceProviderOptions> options)
        {
            ContentSourceProviderOptions contentSourceProviderOptions = new ContentSourceProviderOptions(_dependencyInjection);
            options.Invoke(contentSourceProviderOptions);
            _contentSourceProviderOptions = contentSourceProviderOptions;
            return this;
        }
        
        ///<summary>
        /// Configurating Services (Dependencies) for DataBase Or ContentSource Providers.
        /// </summary>
        public ParserEngineBuilder ConfigureServices(Action<IServiceCollection> config)
        {
            config.Invoke(_dependencyInjection);
            return this;
        }

        public ParserEngineBuilder SetCacheService<TCacheService>() where TCacheService: class, ICacheService
        {
            _dependencyInjection.AddSingleton<ICacheService, TCacheService>();
            return this;
        }

        public ParserEngineBuilder SetUserAgentStorage<TUserAgentStorage>() where TUserAgentStorage: class, IUserAgentStorage
        {
            _dependencyInjection.AddSingleton<IUserAgentStorage, TUserAgentStorage>();
            return this;
        }

        public ParserEngineBuilder SetWebDriver<TWebDriver, TWebDriverOptions>(TWebDriverOptions webDriverOptions) where TWebDriver: class, IWebDriver, new() where TWebDriverOptions: DriverOptions
        {
            _dependencyInjection.AddSingleton<IWebDriver, TWebDriver>(sp => (TWebDriver)Activator.CreateInstance(typeof(TWebDriver), webDriverOptions));
            return this;
        }
        public ParserEngineBuilder SetWebDriver<TWebDriver>() where TWebDriver: class, IWebDriver
        {
            _dependencyInjection.AddSingleton<IWebDriver, TWebDriver>();
            return this;
        }

        public ParserEngineBuilder SetSearchEngine<TSearchEngine>() where TSearchEngine : class, ISearchEngine
        {
            _dependencyInjection.AddSingleton<ISearchEngine, TSearchEngine>();
            return this;
        }
        public ParserEngineBuilder SetSearchEngine<TSearchEngine>(Func<IServiceProvider,TSearchEngine> buildfunc) where TSearchEngine : class, ISearchEngine
        {
            _dependencyInjection.AddSingleton<ISearchEngine, TSearchEngine>(sp => buildfunc.Invoke(sp));
            return this;
        }

        #endregion
        
        
        
        

        public ParserEngine Build()
        {
            
            
            ParserEngine engine =
                new ParserEngine( _dependencyInjection);
            return engine;

        }

        public FluentParserEngine BuildFluent()
        {
            ParserEngine engine = this.Build();
            FluentParserEngine fluentParserEngine = new FluentParserEngine(engine);
            return fluentParserEngine;
        }
    }
}