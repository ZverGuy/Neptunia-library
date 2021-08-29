using System.Threading.Tasks;
using Neptunia_library.DTOs;

namespace Neptunia_library.Interfaces
{
    public interface IDataBaseProvider
    {
        DataBaseProviderInfo GetInfoFromDataBaseService(string contentName);
        Task<DataBaseProviderInfo> GetInfoFromDataBaseServiceAsync(string contentName);
    }
}