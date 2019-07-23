using AppConfig.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class AppSettings : ISettings
    {
        public string SettingsFilePath => ConfigurationManager.AppSettings["SettingsFilePath"];
    }
}
