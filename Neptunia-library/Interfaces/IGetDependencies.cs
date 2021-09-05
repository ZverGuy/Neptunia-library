using System;

namespace Neptunia_library.Interfaces
{
    public interface IGetDependencies
    {
        void OnGettingDependencyServices(IServiceProvider provider);
    }
}