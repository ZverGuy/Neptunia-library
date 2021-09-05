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
using NUnit.Framework;
using OpenQA.Selenium.Firefox;
using ShikimoriSharp;
using ShikimoriSharp.Bases;

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
                    options.RegisterContentSourceProvider<YummiAnimeContentSourceProvider>(ContentTypeEnum.Anime);
                }).ConfigureDataBaseProviders(options =>
                {
                    options.RegisterDataBaseProvider<ShikimoriDataBaseProvider>(ContentTypeEnum.Anime);
                }).ConfigureServices(collection =>
                {
                    collection.AddSingleton<ClientSettings, ClientSettings>(sp => new ClientSettings("NETptunia",
                        "rfCMhnjMK4tHpdhCfQdkZINmtCQlusiNIAug-td6YuM", "ny2JtKgQujhVzLg1N6IM9frLrVqUbQEfhc0Q6x0cWiM"));
                }).SetSearchEngine<SearxSearchEngine>()
                .Build();
        }

        [Test]
        public void DataBaseProviderTest()
        {
            DataBaseProviderInfo parsedinfo = _engine.GetContentInfoFromDataBaseProvider("JoJo no Kimyou na Bouken", ContentTypeEnum.Anime);
            TestContext.WriteLine(parsedinfo.Name);
            TestContext.WriteLine(parsedinfo.Description);
            TestContext.WriteLine(parsedinfo.UrlToContent);
            TestContext.WriteLine(parsedinfo.UserScore);
            Assert.NotNull(parsedinfo);
        }

        [Test]
        public void ContentSourceProviderTest()
        {
            List<IContentSource> content = _engine.GetContentInfoFromContentSources("JoJo no Kimyou na Bouken", ContentTypeEnum.Anime).ToList();
            foreach (var VARIABLE in content)
            {
                Assert.IsNotEmpty(VARIABLE.UrlToContentPage);
            }
        }
    }
}