using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.DependencyInjection;
using Neptunia_library.DTOs.ContentSourceStructs;
using Neptunia_library.Enums;
using Neptunia_library.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Neptunia_library.ContentSources
{
    public class YummiAnimeContentSourceProvider : IContentSourceProvider
    {
        private HttpClient _client;
        private IUserAgentStorage _userAgentStorage;

        public string SiteUrl => defaultUrl.Replace("https://", string.Empty).Replace("/", string.Empty);
        

        public ContentTypeEnum ContentType => ContentTypeEnum.Anime;
        
        private const string defaultUrl = "https://yummyanime.club/";

        private const string searchUrl = "https://yummyanime.club/search?word=";

        public YummiAnimeContentSourceProvider(IUserAgentStorage userAgentStorage)
        {
            _userAgentStorage = userAgentStorage;
            _client = new HttpClient();
        }




        public IContentSource GetContent(string contentname, string userAgent = null)
        {
            ContentSourceWithSubSources result = new ContentSourceWithSubSources();
            result.ContentSourceName = "Yummy Anime";
            result.UrlToContentPage = contentname;
            
            HtmlDocument document = new HtmlDocument();

            _client.DefaultRequestHeaders.Add("user-agent", _userAgentStorage.GetRandomUserAgent());
            string contentPage = _client.GetStringAsync(contentname).Result;
            document = new HtmlDocument() { Text = contentPage };
            document.LoadHtml(contentPage);

            HtmlNodeCollection seriesCollecion = document.DocumentNode.SelectNodes("//div[@class='video-block']");

            result.ContentName = document.DocumentNode.SelectSingleNode("//h1[1]").InnerText;
           
            

            for (int i = 1; i <= seriesCollecion.Count; i++)
            {
                SubContentSource videodub = new SubContentSource();
                videodub.Urls = new List<ContentSourceUrl>();
                videodub.SubContentSourceName = document.DocumentNode
                    .SelectSingleNode($"//div[@class='video-block'][{i}]/div/div").InnerText;
                HtmlNodeCollection videoNodes =
                    document.DocumentNode.SelectNodes(
                        $"//div[@class='video-block'][{i}]//div[@class=\"block-episodes\"]/div");
                for (int j = 1; j < videoNodes.Count; j++)
                {
                    videodub.Urls.Add(new ContentSourceUrl()
                    {
                        UrlToContent = document.DocumentNode.SelectSingleNode($"//div[@class='video-block'][{i}]//div[@class=\"block-episodes\"]/div[{j}]").Attributes["data-href"].Value,
                        ItsCached = false,
                    });
                }
                result.SubContentSources.Add(videodub);
            }

            return result;
        }

      

        public Task<IContentSource> GetContentAsync(string contentname)
        {
            throw new System.NotImplementedException();
        }
    }
}