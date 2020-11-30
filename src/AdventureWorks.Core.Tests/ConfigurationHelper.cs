using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureWorks.Core.Tests
{
    public class ConfigurationHelper
    {
        public static Lazy<IConfiguration> Configuration;
        static ConfigurationHelper()
        {
            Configuration = new Lazy<IConfiguration>(() => GetConfiguration());
        }

        private static IConfiguration GetConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }
    }
}
