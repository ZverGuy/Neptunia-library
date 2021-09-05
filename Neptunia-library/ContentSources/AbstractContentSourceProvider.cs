using System;
using System.Threading.Tasks;
using Neptunia_library.Interfaces;

namespace Neptunia_library.ContentSources
{
    public abstract class AbstractContentSourceProvider : IContentSourceProvider
    {
        public abstract string SiteUrl { get; }

       
        public abstract void GetServices(IServiceProvider provider);
       

        public abstract IContentSource GetContent(string contentname, string userAgent = null);
        
        public abstract Task<IContentSource> GetContentAsync(string contentname);

    }
}