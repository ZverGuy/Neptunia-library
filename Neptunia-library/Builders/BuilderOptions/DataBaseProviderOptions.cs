using System;
using System.Collections.Generic;
using Neptunia_library.Enums;
using Neptunia_library.Interfaces;

namespace Neptunia_library.Builders.BuilderOptions
{
    public class DataBaseProviderOptions
    {

        public List<ValueTuple<ContentTypeEnum, Type>> DataBaseProvider { get; private set; } =
            new List<(ContentTypeEnum, Type)>();

        public void RegisterDataBaseProvider<T>(ContentTypeEnum contentType) where T : IDataBaseProvider
        {
            DataBaseProvider.Add(new ValueTuple<ContentTypeEnum, Type>(contentType, typeof(T)));
        }
        
        internal DataBaseProviderOptions() {}
    }
}