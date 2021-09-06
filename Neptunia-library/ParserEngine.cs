using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Neptunia_library.Builders;
using Neptunia_library.Builders.BuilderOptions;
using Neptunia_library.ContentSources;
using Neptunia_library.DataBaseProviders;
using Neptunia_library.DTOs;
using Neptunia_library.Enums;
using Neptunia_library.Interfaces;
using OpenQA.Selenium;

namespace Neptunia_library
{
    public partial class ParserEngine
    {
        private ICacheService? _service;
        private IUserAgentStorage? _userAgentStorage;
        private ISearchEngine? _searchEngine;
        private readonly IServiceProvider _serviceProvider;

        internal ParserEngine(IServiceCollection serviceCollection)
        {

            _serviceProvider = serviceCollection.BuildServiceProvider();

            _service = _serviceProvider.GetService<ICacheService>();
            _userAgentStorage = _serviceProvider.GetService<IUserAgentStorage>();
            _searchEngine = _serviceProvider.GetService<ISearchEngine>();


        }

    }
}