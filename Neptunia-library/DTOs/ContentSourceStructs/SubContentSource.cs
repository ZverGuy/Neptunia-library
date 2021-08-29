using System.Collections.Generic;

namespace Neptunia_library.DTOs.ContentSourceStructs
{
    public struct SubContentSource
    {
        public string SubContentSourceName { get; set; }
        public IEnumerable<EnumerableContentSourceUrl> Urls { get; set; }
    }
}