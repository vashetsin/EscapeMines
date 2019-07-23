using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utils
{
    public class FileUtility
    {
        public virtual IEnumerable<string> ReadLines(string path) => File.ReadLines(path);
    }
}
