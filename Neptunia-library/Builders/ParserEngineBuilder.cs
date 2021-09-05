using System;
using Microsoft.Extensions.DependencyInjection;
using Neptunia_library.Builders.BuilderOptions;
using Neptunia_library.Interfaces;
using Neptunia_library.SearchEngines;
using OpenQA.Selenium;

namespace Neptunia_library.Builders
{
    public class ParserEngineBuilder
    {
        private DataBaseProviderOptions _dataBaseProviderOptions;
        private ContentSourceProviderOptions _contentSourceProviderOptions;
        private Type _cacheService;
        private Type _userAgentStorage;
        private Type _webDriver;
        private Type _searchEngineType;
        private IServiceCollection _dependencyInjection;

        #region BuilderConfiguration

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

        public ParserEngineBuilder SetCacheService<TCacheService>() where TCacheService: ICacheService
        {
            _cacheService = typeof(TCacheService);
            return this;
        }

        public ParserEngineBuilder SetUserAgentStorage<TUserAgentStorate>() where TUserAgentStorate: IUserAgentStorage
        {
            _userAgentStorage = typeof(TUserAgentStorate);
            return this;
        }

        public ParserEngineBuilder SetWebDriver<TWebDriver>() where TWebDriver: IWebDriver
        {
            _webDriver = typeof(TWebDriver);
            return this;
        }

        public ParserEngineBuilder SetSearchEngine<TSearchEngine>() where TSearchEngine : ISearchEngine
        {
            _searchEngineType = typeof(TSearchEngine);
            return this;
        }

        #endregion
        
        
        
        

        public ParserEngine Build()
        {
            if (_dependencyInjection == null)
            {
                _dependencyInjection = new ServiceCollection();
            }

            _dependencyInjection.AddScoped<IWebDriver>(sp =>
            {
                if (_webDriver != null)
                {
                    var webdriver = (IWebDriver)Activator.CreateInstance(_webDriver);
                    IGetDependencies dep = webdriver as IGetDependencies;
                    if (dep != null)
                    {
                        dep.OnGettingDependencyServices(sp);
                    }

                    return webdriver;
                }

                return null;
            });
            _dependencyInjection.AddScoped<ICacheService>(sp =>
            {
                if (_cacheService != null)
                {
                    ICacheService cacheservice = (ICacheService)Activator.CreateInstance(_cacheService);
                    if (cacheservice is IGetDependencies dep)
                    {
                        dep.OnGettingDependencyServices(sp);
                    }

                    return cacheservice;
                }

                return null;
            });
            _dependencyInjection.AddScoped<IUserAgentStorage>(sp =>
            {
                if (_userAgentStorage != null)
                {
                    IUserAgentStorage userAgentStorage = (IUserAgentStorage)Activator.CreateInstance(_userAgentStorage);
                    if (userAgentStorage is IGetDependencies dep)
                    {
                        dep.OnGettingDependencyServices(sp);
                    }

                    return userAgentStorage;
                }

                return null;
            });

            _dependencyInjection.AddScoped<ISearchEngine>(sp =>
            {
                if (_searchEngineType != null)
                {
                    ISearchEngine searchEngine = (ISearchEngine)Activator.CreateInstance(_searchEngineType);
                    if (searchEngine is IGetDependencies dep)
                    {
                        dep.OnGettingDependencyServices(sp);
                    }

                    return searchEngine;
                }

                return null;
            });
            
            
            
            ParserEngine engine =
                new ParserEngine(_contentSourceProviderOptions, _dataBaseProviderOptions, _dependencyInjection);
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