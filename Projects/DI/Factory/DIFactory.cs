using Autofac;
using Shared.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI
{
    internal class DIFactory : IDIFactory
    {
        private readonly ILifetimeScope _lifetimeScope;

        public DIFactory(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public R GetRegisteredType<R, T>(T type)
        {
            return _lifetimeScope.ResolveKeyed<R>(type);
        }
    }
}
