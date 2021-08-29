using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Neptunia_library.Builders;
using Neptunia_library.Builders.BuilderOptions;
using Neptunia_library.DTOs;
using Neptunia_library.Enums;
using Neptunia_library.Interfaces;

namespace Neptunia_library
{
    public class ParserEngine
    {
        public readonly IEnumerable<ValueTuple<ContentTypeEnum, IContentSourceProvider>> _contentSourceProviders;
        public readonly List<(ContentTypeEnum, IDataBaseProvider)> _DataBaseProviders;
        public readonly ICacheService _Service;

        internal ParserEngine(ContentSourceProviderOptions contentOptions, DataBaseProviderOptions dataBaseOptions, [AllowNull] ICacheService cacheService = null)
        {
            _contentSourceProviders = contentOptions.ContentSourceProviders ??
                                      throw new ArgumentNullException(nameof(contentOptions));
            
            _DataBaseProviders =
                dataBaseOptions.DataBaseProvider ?? throw new ArgumentNullException(nameof(dataBaseOptions));
            
        }

        public DataBaseProviderInfo GetContentInfo(string contentname, ContentTypeEnum contentType)
        {
            DataBaseProviderInfo info = new DataBaseProviderInfo();
            List<IDataBaseProvider> dataBaseProviders = _DataBaseProviders
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

    }
}