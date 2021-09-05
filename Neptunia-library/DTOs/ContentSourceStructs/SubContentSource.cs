using System.Collections.Generic;

namespace Neptunia_library.DTOs.ContentSourceStructs
{
    public class SubContentSource
    {
        public string SubContentSourceName { get; set; }
        public IEnumerable<ContentSourceUrl> Urls { get; set; }
    }
}