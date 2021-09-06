using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Neptunia_library;
using Neptunia_library.Builders;
using Neptunia_library.ContentSources;
using Neptunia_library.DataBaseProviders;
using Neptunia_library.DTOs;
using Neptunia_library.Enums;
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
                    options.RegisterContentSourceProvider<YummiAnimeContentProvider>(ContentTypeEnum.Anime);
                }).ConfigureDataBaseProviders(options =>
                {
                    options.RegisterDataBaseProvider<ShikimoriContentDataBaseProvider>(ContentTypeEnum.Anime);
                }).ConfigureServices(collection =>
                {
                    collection.AddSingleton<ClientSettings, ClientSettings>(sp => new ClientSettings("NETptunia",
                        "rfCMhnjMK4tHpdhCfQdkZINmtCQlusiNIAug-td6YuM", "ny2JtKgQujhVzLg1N6IM9frLrVqUbQEfhc0Q6x0cWiM"));
                })
                .SetSearchEngine<SearxSearchEngine>((sp) =>
                    new SearxSearchEngine("https://searx.roughs.ru/", sp.GetService<IUserAgentStorage>()))
                .SetUserAgentStorage<InMemoryUserAgentStorage>()
                .Build();
        }

        [Test]
        public void FullTest()
        {
            ParsedContentBuilder contentBuilder = new ParsedContentBuilder();
            DataBaseProviderInfo info =
                _engine.GetContentInfoFromDataBaseProvider("Fate Stay Night", "Anime");
            IEnumerable<IContent> contentSources =
                _engine.GetContentInfoFromContentSources("Fate Stay Night", "Anime");

            ParsedContent result = contentBuilder.SetMainInfo(info).SetContentFromContentSources(contentSources)
                .BuildContent("Anime");
            
            Assert.NotNull(info);
            Assert.NotNull(contentSources);

            string test = JsonConvert.SerializeObject(result);
            TestContext.WriteLine(test);

        }
    }
}