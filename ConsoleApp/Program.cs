using AppConfig.Core;
using Autofac;
using DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<App>().AsSelf();
            builder.RegisterType<AppSettings>().As<ISettings>().SingleInstance();
            builder.RegisterModule<DIModule>();

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<App>();
                app.Run();
            }
        }
    }
}
