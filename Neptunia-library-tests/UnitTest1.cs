using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Neptunia_library;
using Neptunia_library.Builders;
using Neptunia_library.Builders.BuilderOptions;
using Neptunia_library.ContentSources;
using Neptunia_library.DataBaseProviders;
using Neptunia_library.DTOs;
using Neptunia_library.Enums;
using Neptunia_library.Extensions;
using Neptunia_library.Interfaces;
using Neptunia_library.SearchEngines;
using Neptunia_library.UserAgentStorages;
using NUnit.Framework;
using OpenQA.Selenium.Firefox;
using ShikimoriSharp;
using ShikimoriSharp.Bases;
using Newtonsoft.Json;
namespace Neptunia_library_tests
{
    public class InitializationTests
    {
        private ParserEngine _engine;
        [SetUp]
        public void Setup()
        {
            ParserEngineBuilder builder = new ParserEngineBuilder();
            FirefoxOptions ffOptions = new FirefoxOptions();

            _engine = builder.ConfigureContentSourceProviders(options =>
                {
                    options.RegisterContentSourceProvider<YummiAnimeContentProvider>();
                })
                .ConfigureDataBaseProviders(options =>
                {
                    options.RegisterDataBaseProvider<ShikimoriContentDataBaseProvider>();
                })
                .ConfigureServices(collection =>
                {
                    collection.AddSingleton<ClientSettings, ClientSettings>(sp => new ClientSettings("NETptunia",
                        "rfCMhnjMK4tHpdhCfQdkZINmtCQlusiNIAug-td6YuM", "ny2JtKgQujhVzLg1N6IM9frLrVqUbQEfhc0Q6x0cWiM"));
                })
                .UseSearxHtmlSearchEngine("https://searx.roughs.ru/")
                .SetUserAgentStorage<InMemoryUserAgentStorage>()
                .Build();
        }

        [Test]
        public void DataBaseProviderTest()
        {
            DataBaseProviderInfo info = _engine.SearchContentOnDataBase(new ContentRequestSettings()
            {
                ContentName = "Fate Stay Night",
                ContentType = "Anime",
                Language = "Russian"
            });
            
            Assert.NotNull(info);
            TestContext.WriteLine(JsonConvert.SerializeObject(info));
        }
        [Test]
        public async Task DataBaseProviderTestAsync()
        {
            DataBaseProviderInfo info = await _engine.SearchContentOnDataBaseAsync(new ContentRequestSettings()
            {
                ContentName = "Fate Stay Night",
                ContentType = "Anime",
                Language = "Russian"
            });
            Assert.NotNull(info);
            TestContext.WriteLine(JsonConvert.SerializeObject(info));
        }

        [Test]
        public void ContentProvidersTest()
        {
            IEnumerable<IContent> result = _engine.SearchContentByContentProviders(new ContentRequestSettings()
            {
                ContentName = "Fate Stay Night",
                ContentType = "Anime",
                Language = "Russian"
            });
            Assert.IsNotEmpty(result);
            TestContext.WriteLine(JsonConvert.SerializeObject(result));
        }
        
        [Test]
        public async Task ContentProvidersTestAsync()
        {
            IEnumerable<IContent> result = await _engine.SearchContentByContentProvidersAsync(new ContentRequestSettings()
            {
                ContentName = "Fate Stay Night",
                ContentType = "Anime",
                Language = "Russian"
            });
            Assert.IsNotEmpty(result);
            TestContext.WriteLine(JsonConvert.SerializeObject(result));
        }
    }
}