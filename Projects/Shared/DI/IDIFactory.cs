using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DI
{
    public interface IDIFactory
    {
        R GetRegisteredType<R, T>(T type);
    }
}
