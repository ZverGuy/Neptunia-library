using System.Threading.Tasks;
using Neptunia_library.DTOs;

namespace Neptunia_library.Interfaces
{
    public interface ICacheService
    {
        ParsedContent GetCachedContent(object id);
        Task<ParsedContent> GetCachedContentAsync(object id);
    }
}