using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Neptunia_library.Enums;
using Neptunia_library.Interfaces;

namespace Neptunia_library.Builders.BuilderOptions
{
    public class DataBaseProviderOptions
    {
        private IServiceCollection _collection;

       

        public void RegisterDataBaseProvider<T>(ContentTypeEnum contentType) where T : class, IDataBaseProvider
        {
            _collection.AddSingleton<IDataBaseProvider, T>();
        }

        internal DataBaseProviderOptions(IServiceCollection collection)
        {
            _collection = collection;
        }
    }
}