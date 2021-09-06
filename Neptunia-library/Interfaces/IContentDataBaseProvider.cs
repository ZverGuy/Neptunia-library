using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Neptunia_library.DTOs;
using Neptunia_library.Enums;
using OpenQA.Selenium;

namespace Neptunia_library.Interfaces
{
    public interface IContentDataBaseProvider
    {
        IEnumerable<string> ContentTypes { get; }
        IEnumerable<string> Languages { get; }

        DataBaseProviderInfo GetInfoFromDataBaseService(string contentName, [AllowNull] string userAgent = null);
        Task<DataBaseProviderInfo> GetInfoFromDataBaseServiceAsync(string contentName);
    }
}