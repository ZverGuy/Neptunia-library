using System;
using System.Collections.Generic;
using Neptunia_library.Enums;
using Neptunia_library.Interfaces;

namespace Neptunia_library.Builders.BuilderOptions
{
    public class DataBaseProviderOptions
    {
        public List<ValueTuple<ContentTypeEnum,IDataBaseProvider>> DataBaseProvider { get; private set; } = new List<(ContentTypeEnum, IDataBaseProvider)>();

        public void RegisterDataBaseProvider<T>(ContentTypeEnum contentType,object[] args) where T : IDataBaseProvider
        {
            IDataBaseProvider provider = (IDataBaseProvider)Activator.CreateInstance(typeof(T), args);
            DataBaseProvider.Add(new ValueTuple<ContentTypeEnum, IDataBaseProvider>(contentType, provider));
        }
        
        internal DataBaseProviderOptions() {}
    }
}