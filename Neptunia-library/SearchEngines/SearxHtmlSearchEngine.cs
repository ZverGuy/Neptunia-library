using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Neptunia_library.DTOs;
using Neptunia_library.Interfaces;

namespace Neptunia_library.SearchEngines
{
    public class SearxHtmlSearchEngine : ISearchEngine
    {
        private readonly string _sitesUrl;
        private string _searchUrl => _sitesUrl + "search?q=";
        private IUserAgentStorage _storage;

        public SearxHtmlSearchEngine(string searxInstancesUrl, IUserAgentStorage userAgentStorage)
        {
            _sitesUrl = searxInstancesUrl;
            _storage = userAgentStorage;
        }
        public IEnumerable<SearchEngineResult> GetSearchResults(string searchquery, IEnumerable<IContentProvider> sites)
        {
            List<SearchEngineResult> results = new List<SearchEngineResult>();
            using (HttpClient client = new HttpClient())
            {
                var searchstring = GetSearchQueryWithDorks(searchquery, sites);
                client.DefaultRequestHeaders.Add("user-agent", _storage.GetRandomUserAgent().Replace("\r\n", string.Empty));
                string html = client.PostAsync(_searchUrl + searchstring, new StringContent("1")).Result.Content.ReadAsStringAsync().Result;
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(html);

                List<HtmlNode> nodes = document.DocumentNode
                    .SelectNodes("//div[contains(@class, \"result result-default\")]").ToList();

                for (int i = 1; i <= nodes.Count; i++)
                {
                    try
                    {
                        SearchEngineResult result = new SearchEngineResult()
                        {
                            HeaderText = document.DocumentNode
                                .SelectSingleNode($"//div[contains(@class, \"result result-default\")][{i}]/h4/a").InnerText,
                            Description = document.DocumentNode
                                .SelectSingleNode($"//div[contains(@class, \"result result-default\")][{i}]/p").InnerText,
                            Url = document.DocumentNode
                                .SelectSingleNode($"//div[contains(@class, \"result result-default\")][{i}]/h4/a")
                                .Attributes["href"].Value
                        };
                        results.Add(result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    
                }

                return results;
            }
        }

        
        
        
        private string GetSearchQueryWithDorks(string searchtext, IEnumerable<IContentProvider> sites)
        {
            StringBuilder sb = new StringBuilder();
            Regex regex = new Regex(@"\/\/(.+?)\/");
            int i = 0;
            foreach (var VARIABLE in sites)
            {
                i++;
                string url = VARIABLE.SiteUrl;

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