using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Neptunia_library.DTOs;
using Neptunia_library.Interfaces;
using OpenQA.Selenium;
using ShikimoriSharp;
using ShikimoriSharp.Bases;
using ShikimoriSharp.Classes;
using ShikimoriSharp.Settings;

namespace Neptunia_library.DataBaseProviders
{
    public class ShikimoriDataBaseProvider : IDataBaseProvider, IGetDependencies
    {
        private ShikimoriClient _client;
        
        private IWebDriver WebDriver { get;  set; }
        
        


        public void OnGettingDependencyServices(IServiceProvider provider)
        {
            WebDriver = provider.GetService<IWebDriver>();
            var setting = provider.GetService<ClientSettings>();
            _client = new ShikimoriClient(null, setting);
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
                Description = "Where Description????",
                UrlToContent = "shikimori.one" +info.Url,
                UserScore = info.Score
            };
            return result;
        }
        

        public  Task<DataBaseProviderInfo> GetInfoFromDataBaseServiceAsync(string contentName)
        {
            throw new System.NotImplementedException();
        }
    }
}