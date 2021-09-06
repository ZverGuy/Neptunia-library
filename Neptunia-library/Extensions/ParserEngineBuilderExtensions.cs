using Microsoft.Extensions.DependencyInjection;
using Neptunia_library.Builders;
using Neptunia_library.Interfaces;
using Neptunia_library.SearchEngines;

namespace Neptunia_library.Extensions
{
    public static class ParserEngineBuilderExtensions
    {

        public static ParserEngineBuilder UseSearxHtmlSearchEngine(this ParserEngineBuilder builder,string searxInstanseUrl)
        {
            builder.ConfigureServices(cfg =>
            {
                cfg.AddSingleton<ISearchEngine, SearxHtmlSearchEngine>(sp =>
                {
                    return new SearxHtmlSearchEngine(searxInstanseUrl, sp.GetService<IUserAgentStorage>());
                });
            });
            return builder;
        }
        
    }
}