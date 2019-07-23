using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tests.Internal.Persistence
{
    internal static class TextFileRepositoryHelper
    {
        public static IEnumerable<string> ValidFileLines = new string[]
        {
            "5 4",
            "1,1 1,3 3,3",
            "4 2",
            "0 1 N",
            "R M L M M",
            "R M M M"
        };
        public static IEnumerable<string> InvalidFileLines1 = new string[]
        {
            "dummy",
            "dummy",
            "dummy",
            "dummy",
            "dummy",
            "dummy"
        };
        public static IEnumerable<string> InvalidFileLines2 = new string[]
        {
            "5 4 3",
            "1,1,1",
            "4 2 0",
            "0 1 N S",
            "X Y"
        };
    }
}
