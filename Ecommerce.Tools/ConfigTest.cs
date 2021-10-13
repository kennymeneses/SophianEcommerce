using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System;

namespace Ecommerce.Tools
{
    public class ConfigTest
    {
        public ServiceProvider ServiceProvider { get; private set; }

        public ConfigTest()
        {
            var serviceCollection = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(
                     path: "TestAppsettings.json",
                     optional: false,
                     reloadOnChange: true
                     )
               .Build();
            serviceCollection.AddSingleton<IConfiguration>(configuration);

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

    }
}
