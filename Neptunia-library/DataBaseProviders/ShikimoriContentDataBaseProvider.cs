using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Neptunia_library.DTOs;
using Neptunia_library.Enums;
using Neptunia_library.Interfaces;
using OpenQA.Selenium;
using ShikimoriSharp;
using ShikimoriSharp.Bases;
using ShikimoriSharp.Classes;
using ShikimoriSharp.Settings;

namespace Neptunia_library.DataBaseProviders
{
    public class ShikimoriContentDataBaseProvider : IContentDataBaseProvider
    {
        private ShikimoriClient _client;
        
        private IWebDriver WebDriver { get;  set; }

        public IEnumerable<string> ContentTypes => new[]
        {
            "Anime"
        };

        public IEnumerable<string> Languages => new[]
        {
            "Russian"
        };


        public ShikimoriContentDataBaseProvider(ClientSettings shikimoriClientSettings)
        {
            _client = new ShikimoriClient(null,shikimoriClientSettings);
        }

       

        public  DataBaseProviderInfo GetInfoFromDataBaseService(string contentName, [AllowNull] string userAgent = null)
        {
            Anime info = _client.Animes.GetAnime(new AnimeRequestSettings()
            {
                search = contentName
            }).Result.First();

            DataBaseProviderInfo result = new DataBaseProviderInfo()
            {
                Name = info.Name,
                Description = "No Description",
                UrlToContent = "https://shikimori.one" + info.Url,
                UserScore = info.Score,
                UrlToImage = "https://shikimori.one" + info.Image.Original
            };
            return result;
        }
        

        public async Task<DataBaseProviderInfo> GetInfoFromDataBaseServiceAsync(string contentName)
        {
            Anime[] infos = await _client.Animes.GetAnime(new AnimeRequestSettings()
            {
                search = contentName
            }).ConfigureAwait(false);
            var info = infos.First();

            DataBaseProviderInfo result = new DataBaseProviderInfo()
            {
                Name = info.Name,
                Description = "No Description",
                UrlToContent = "https://shikimori.one" + info.Url,
                UserScore = info.Score,
                UrlToImage = "https://shikimori.one" + info.Image.Original
            };
            return result;
        }
    }
}