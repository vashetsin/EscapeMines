using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConfig.Core
{
    public interface ISettings
    {
        string SettingsFilePath { get; }
    }
}
