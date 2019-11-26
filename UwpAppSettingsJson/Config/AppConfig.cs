using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UwpAppSettingsJson.Config.Sections;
using Windows.ApplicationModel;

namespace UwpAppSettingsJson.Config
{
    public class AppConfig
    {
        private readonly IConfigurationRoot _configurationRoot;

        public AppConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Package.Current.InstalledLocation.Path)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.production.json", optional: true);

            _configurationRoot = builder.Build();
        }

        public Example Example => GetSection<Example>(nameof(Example));

        private T GetSection<T>(string key) => _configurationRoot.GetSection(key).Get<T>();
    }
}
