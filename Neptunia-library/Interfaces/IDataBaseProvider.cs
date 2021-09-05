using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Neptunia_library.DTOs;
using OpenQA.Selenium;

namespace Neptunia_library.Interfaces
{
    public interface IDataBaseProvider
    {
       
        DataBaseProviderInfo GetInfoFromDataBaseService(string contentName, [AllowNull] string userAgent = null);
        Task<DataBaseProviderInfo> GetInfoFromDataBaseServiceAsync(string contentName);
    }
}