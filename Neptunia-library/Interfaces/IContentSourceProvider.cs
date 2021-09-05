using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Neptunia_library.Enums;
using OpenQA.Selenium;

namespace Neptunia_library.Interfaces
{
    public interface IContentSourceProvider
    {
        ContentTypeEnum ContentType { get; }
        
        string SiteUrl { get; }
       
        IContentSource GetContent(string contentname, [AllowNull] string userAgent = null);
        Task<IContentSource> GetContentAsync(string contentname);
    }
}