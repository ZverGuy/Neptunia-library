using System;
using System.Collections.Generic;
using Neptunia_library.Builders;
using Neptunia_library.Builders.BuilderOptions;
using Neptunia_library.Enums;
using Neptunia_library.Interfaces;

namespace Neptunia_library
{
    public partial class ParserEngine
    {
        private void InitContentSourceProviders(List<(ContentTypeEnum, Type)> contentSourceProviders)
        {
            foreach (var variable in contentSourceProviders)
            {
                IContentSourceProvider contentprovider = (IContentSourceProvider)Activator.CreateInstance(variable.Item2);

                if (contentprovider is IGetDependencies dep)
                {
                    dep.OnGettingDependencyServices(_serviceProvider);
                }

                _contentSourceProviders.Add((variable.Item1, contentprovider));
            }
        }
        
        
        private void InitDataBaseProviders(List<(ContentTypeEnum, Type)> dataBaseProviders)
        {
            foreach (var variable in dataBaseProviders)
            {
                IDataBaseProvider databaseprovider = (IDataBaseProvider)Activator.CreateInstance(variable.Item2);

                if (databaseprovider is IGetDependencies dep)
                {
                    dep.OnGettingDependencyServices(_serviceProvider);
                }

                _dataBaseProviders.Add((variable.Item1, databaseprovider));
            }
        }
        
    }
}