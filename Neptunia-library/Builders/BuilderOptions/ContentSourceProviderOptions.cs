using System;
using System.Collections.Generic;
using System.Net.Mime;
using Neptunia_library.Enums;
using Neptunia_library.Interfaces;

namespace Neptunia_library.Builders
{
    public class ContentSourceProviderOptions
    {
        public List<ValueTuple<ContentTypeEnum, Type>> ContentSourceProviders { get; private set; }

        internal ContentSourceProviderOptions()
        {
            ContentSourceProviders = new List<(ContentTypeEnum, Type)>();
        }

        public void RegisterContentSourceProvider<T>(ContentTypeEnum typeEnum)
            where T : IContentSourceProvider
        {
            ContentSourceProviders.Add(new ValueTuple<ContentTypeEnum, Type>(typeEnum, typeof(T)));
        }
    }
}