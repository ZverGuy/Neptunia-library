using System.Collections.Generic;

namespace Neptunia_library.DTOs.ContentSourceStructs
{
    public class SubContentSource
    {
        public string SubContentSourceName { get; set; }
        public List<ContentSourceUrl> Urls { get; set; }

        public SubContentSource()
        {
            Urls = new List<ContentSourceUrl>();
        }
    }
}