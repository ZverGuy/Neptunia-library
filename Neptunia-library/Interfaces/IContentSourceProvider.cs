using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Neptunia_library.Interfaces
{
    public interface IContentSourceProvider
    {
        string SiteUrl { get; }

       
        IContentSource GetContent(string contentname, [AllowNull] string userAgent = null);
        Task<IContentSource> GetContentAsync(string contentname);
    }
}