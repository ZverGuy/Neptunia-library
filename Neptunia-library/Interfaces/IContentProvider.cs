using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Neptunia_library.Enums;
using OpenQA.Selenium;

namespace Neptunia_library.Interfaces
{
    public interface IContentProvider
    {
        IEnumerable<string> ContentTypes { get; }
        IEnumerable<string> Languages { get; }

        string SiteUrl { get; }
       
        IContent GetContent(string contentname, [AllowNull] string userAgent = null);
        Task<IContent> GetContentAsync(string contentname);
    }
}