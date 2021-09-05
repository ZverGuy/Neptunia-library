using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using Neptunia_library.DTOs;
using Neptunia_library.Interfaces;

namespace Neptunia_library.SearchEngines
{
    public class SearxSearchEngine : ISearchEngine
    {
        private readonly string _sitesUrl;
        private string _searchUrl => _sitesUrl + "search?q=";

        public SearxSearchEngine(string searxInstancesUrl)
        {
            _sitesUrl = searxInstancesUrl;
        }
        public IEnumerable<SearchEngineResult> GetSearchResults(string searchquery, IEnumerable<IContentSourceProvider> sites)
        {
            using (HttpClient client = new HttpClient())
            {
                var searchstring = GetSearchQueryWithDorks(searchquery, sites);
                //client.DefaultRequestHeaders.Add("user-agent",);
                throw new NotImplementedException();
            }
        }

        
        
        
        private string GetSearchQueryWithDorks(string searchtext, IEnumerable<IContentSourceProvider> sites)
        {
            StringBuilder sb = new StringBuilder();
            Regex regex = new Regex(@"\/\/(.+?)\/");
            int i = 0;
            foreach (var VARIABLE in sites)
            {
                i++;
                string url = regex.Match(VARIABLE.SiteUrl).Value;

                sb.Append($"site:{url} ");

                if (i != sites.ToList().Count)
                {
                    sb.Append("OR ");
                }
            }

            string searchstring = sb.Append($" {searchtext}").ToString();
            return searchstring;
        }
    }
}