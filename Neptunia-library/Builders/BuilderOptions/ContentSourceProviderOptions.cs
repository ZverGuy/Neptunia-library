using System;
using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.Extensions.DependencyInjection;
using Neptunia_library.Enums;
using Neptunia_library.Interfaces;

namespace Neptunia_library.Builders
{
    public class ContentSourceProviderOptions
    {
        private IServiceCollection _collection;

        internal ContentSourceProviderOptions(IServiceCollection collection)
        {
            _collection = collection;
        }

        public void RegisterContentSourceProvider<TContentSourceProvider>()
            where TContentSourceProvider : class, IContentProvider
        {
            _collection.AddScoped<IContentProvider, TContentSourceProvider>();
        }
    }
}