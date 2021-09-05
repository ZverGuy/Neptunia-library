namespace Neptunia_library.Interfaces
{
    public interface IUserAgentStorage
    {
        string GetRandomUserAgent();
        string GetUserAgentById(object id);
    }
}