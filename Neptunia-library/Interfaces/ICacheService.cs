using System.Threading.Tasks;
using Neptunia_library.DTOs;

namespace Neptunia_library.Interfaces
{
    public interface ICacheService
    {
        ParsedContent GetContent(string id);
        Task<ParsedContent> GetContentAsync(string id);
    }
}