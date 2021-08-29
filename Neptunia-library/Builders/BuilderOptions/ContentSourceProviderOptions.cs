using System;
using System.Collections.Generic;
using System.Net.Mime;
using Neptunia_library.Enums;
using Neptunia_library.Interfaces;

namespace Neptunia_library.Builders
{
    public class ContentSourceProviderOptions
    {
        public List<ValueTuple<ContentTypeEnum, IContentSourceProvider>> ContentSourceProviders { get; private set; }

        internal ContentSourceProviderOptions()
        {
            ContentSourceProviders = new List<(ContentTypeEnum, IContentSourceProvider)>();
        }

        public void RegisterContentSourceProvider<T>(ContentTypeEnum typeEnum, object[] args)
            where T : IContentSourceProvider
        {
            IContentSourceProvider contentSourceProvider = (IContentSourceProvider)Activator.CreateInstance(typeof(T), args);
            ContentSourceProviders.Add(new ValueTuple<ContentTypeEnum, IContentSourceProvider>(typeEnum, contentSourceProvider));
        }
    }
}