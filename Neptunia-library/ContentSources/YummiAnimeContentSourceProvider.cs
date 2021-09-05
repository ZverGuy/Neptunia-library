using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.DependencyInjection;
using Neptunia_library.DTOs.ContentSourceStructs;
using Neptunia_library.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Neptunia_library.ContentSources
{
    public class YummiAnimeContentSourceProvider : AbstractContentSourceProvider
    {
        public  HttpClient _client;

        public override string SiteUrl => defaultUrl;
        public IWebDriver WebDriver { get; set; }
        
        private const string defaultUrl = "https://yummyanime.club/";

        private const string searchUrl = "https://yummyanime.club/search?word=";

        private const string _userAgent =
            "Mozilla/5.0 (Linux; Android 10; SM-A307FN Build/QP1A.190711.020; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/90.0.4430.82 Mobile Safari/537.36";

        public YummiAnimeContentSourceProvider()
        {
            
        }
        public override void GetServices(IServiceProvider provider)
        {
            WebDriver = provider.GetService<IWebDriver>();
        }

        public override IContentSource GetContent(string contentname, string userAgent = null)
        {
            ContentSourceWithSubSources result = new ContentSourceWithSubSources();
            
            List<SubContentSource> subContentSources = new List<SubContentSource>();
            
            
            WebDriver.Navigate().GoToUrl(searchUrl + contentname);
            new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10))
                .Until(x => x.FindElements(By.CssSelector("div.anime-column")));

            HtmlDocument document = new HtmlDocument();
            string xpath = WebDriver.PageSource.Replace("\r\n", String.Empty);
            document.Load(xpath);
            HtmlNode animeColumn = document.DocumentNode.SelectSingleNode("//div[@class=\"anime-column\"]");

            result.UrlToContentPage = defaultUrl + animeColumn.SelectSingleNode("//div/a").Attributes["href"].Value;
            
            result.ContentName = animeColumn.SelectSingleNode("/html/body/div[2]/div[2]/div[3]/div/div/div/div[1]/div/a").InnerText;

            _client.DefaultRequestHeaders.Add("user-agent", userAgent ?? _userAgent);
            
            string contentPage = _client.GetStringAsync(result.UrlToContentPage).Result;
            
            document = new HtmlDocument() { Text = contentPage };
            document.LoadHtml(contentPage);
            HtmlNodeCollection seriesCollecion = document.DocumentNode.SelectNodes("//div[@class='video-block']");

            foreach (HtmlNode videoblock in seriesCollecion)
            {
                SubContentSource videoUrls = new SubContentSource();
                List<ContentSourceUrl> urls = new List<ContentSourceUrl>();
                videoUrls.SubContentSourceName = videoblock.SelectSingleNode("/div/div").InnerText;
                
                foreach (var urlNode in videoblock.SelectNodes("/div[2]/div/div/div"))
                {
                    urls.Add(new ContentSourceUrl()
                    {
                        UrlToContent = urlNode.Attributes["data-href"].Value,
                        ItsCached = false,
                    });
                }

                videoUrls.Urls = urls;
                subContentSources.Add(videoUrls);
            }

            return result;
        }

      

        public override Task<IContentSource> GetContentAsync(string contentname)
        {
            throw new System.NotImplementedException();
        }
    }
}