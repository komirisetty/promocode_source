using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Alphasource.Libs.Promocodes.Utilities
{
    public static class ConfigHelper
    {
        public static string ContentRootPath { get; set; }
        public static IConfiguration Configurations()
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configurationFile = string.Empty;
            if (string.IsNullOrEmpty(environment))
            {
                configurationFile = string.Format("appsettings.json");
            }
            else configurationFile = string.Format("appsettings.{0}.json", environment);

            Console.WriteLine("Configuration File Loaded {0}", configurationFile);

            var builder = new ConfigurationBuilder()
                .SetBasePath(ContentRootPath)
                .AddJsonFile(configurationFile, optional: false, reloadOnChange: true);

            return builder.Build();
        }
    }
}
