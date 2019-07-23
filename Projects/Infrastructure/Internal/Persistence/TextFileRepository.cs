using AppConfig.Core;
using Infrastructure.Core.Exceptions;
using Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Internal.Persistence
{
    internal abstract class TextFileRepository
    {
        private readonly string _settingsFilePath;
        private readonly FileUtility _fileUtility;

        public TextFileRepository(
            ISettings settings,
            FileUtility fileUtility)
        {
            _settingsFilePath = settings.SettingsFilePath;
            _fileUtility = fileUtility;
        }

        protected string ReadLine(int number)
        {
            return _fileUtility.ReadLines(_settingsFilePath).Skip(number - 1).First();
        }

        protected IEnumerable<string> ReadLines(int startNumber)
        {
            return _fileUtility.ReadLines(_settingsFilePath).Skip(startNumber - 1);
        }

        protected T Parse<T>(Func<T> func)
        {
            try
            {
                return func();
            }
            catch
            {
                throw new InvalidSettingsException();
            }
        }
    }
}
