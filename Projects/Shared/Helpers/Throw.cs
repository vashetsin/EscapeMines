﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers
{
    public static class Throw
    {
        public static void If<T>(Func<bool> func) where T : Exception, new()
        {
            if (func()) throw new T();
        }
    }
}
