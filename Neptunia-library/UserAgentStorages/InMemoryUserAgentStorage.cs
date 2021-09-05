using System;
using System.Collections.Generic;
using Neptunia_library.Interfaces;

namespace Neptunia_library.UserAgentStorages
{
    public class InMemoryUserAgentStorage : IUserAgentStorage
    {
        private List<string> useragents = new List<string>()
        {
            "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.107 Safari/537.36",
            "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.107 Safari/537.36",
            "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.107 Safari/537.36",
            "Mozilla/5.0 (Windows NT 6.1; rv:91.0) Gecko/20100101 Firefox/91.0",
        };
        public string GetRandomUserAgent()
        {
            Random random = new Random();

            return useragents[random.Next(useragents.Count)];
        }

        public string GetUserAgentById(object id)
        {
            throw new System.NotImplementedException();
        }
    }
}