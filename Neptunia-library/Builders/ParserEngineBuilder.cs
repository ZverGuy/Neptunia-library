using System;
using Neptunia_library.Builders.BuilderOptions;
using Neptunia_library.Interfaces;

namespace Neptunia_library.Builders
{
    public class ParserEngineBuilder
    {
        private DataBaseProviderOptions _dataBaseProviderOptions;
        private ContentSourceProviderOptions _contentSourceProviderOptions;
        private ICacheService _cacheService;
        
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

        public ParserEngineBuilder ConfigureCacheService(ICacheService cacheService)
        {
            _cacheService = cacheService;
            return this;
        }

        public ParserEngine Build()
        {
            ParserEngine engine =
                new ParserEngine(_contentSourceProviderOptions, _dataBaseProviderOptions, _cacheService);
            return engine;

        }

        public FluentParserEngine BuildFluent()
        {
            ParserEngine engine =
                new ParserEngine(_contentSourceProviderOptions, _dataBaseProviderOptions, _cacheService);
            FluentParserEngine fluentParserEngine = new FluentParserEngine(engine);
            return fluentParserEngine;
        }
    }
}