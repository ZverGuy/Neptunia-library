using System;
using System.Threading.Tasks;
using Neptunia_library.DTOs;
using Neptunia_library.Interfaces;
using OpenQA.Selenium;

namespace Neptunia_library.DataBaseProviders
{
    public abstract class AbstractDataBaseProvider : IDataBaseProvider
    {
        public AbstractDataBaseProvider(IServiceProvider provider)
        {
            GetServices(provider: provider);
        }

        public AbstractDataBaseProvider() {}
        public abstract void GetServices(IServiceProvider provider);
        public abstract DataBaseProviderInfo GetInfoFromDataBaseService(string contentName, string userAgent = null);

        public abstract Task<DataBaseProviderInfo> GetInfoFromDataBaseServiceAsync(string contentName);

    }
}