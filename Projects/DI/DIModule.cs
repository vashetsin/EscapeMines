using Application.Core.Services.MineFields;
using Application.Core.Services.Tiles;
using Application.Core.Services.Turtles;
using Application.Internal.Services.MineFields;
using Application.Internal.Services.MineFields.Validator;
using Application.Internal.Services.Tiles;
using Application.Internal.Services.Tiles.TileManager;
using Application.Internal.Services.Turtles;
using Application.Internal.Services.Turtles.MoveManager;
using Application.Internal.Services.Turtles.Validator;
using Autofac;
using Domain.Core.Enums;
using Domain.Core.Factories;
using Domain.Internal.Factories;
using Infrastructure.Core.Persistence;
using Infrastructure.Internal.Persistence;
using Shared.DI;
using Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI
{
    public class DIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Domain
            builder.RegisterType<MineFieldFactory>().As<IMineFieldFactory>();
            builder.RegisterType<TileFactory>().As<ITileFactory>();
            builder.RegisterType<TurtleFactory>().As<ITurtleFactory>();

            // Infrastructure
            builder.RegisterType<TextFileMineFieldRepository>().As<IMineFieldRepository>();
            builder.RegisterType<TextFileTileRepository>().As<ITileRepository>();
            builder.RegisterType<TextFileTurtleRepository>().As<ITurtleRepository>();

            // Application
            builder.RegisterType<MineFieldService>().As<IMineFieldService>();
            builder.RegisterType<TileService>().As<ITileService>();
            builder.RegisterType<TurtleService>().As<ITurtleService>();

            builder.RegisterType<MineFieldValidator>().AsSelf();
            builder.RegisterType<TurtleValidator>().AsSelf();

            builder.RegisterType<LMoveManager>().Keyed<IMoveManager>(MoveType.L);
            builder.RegisterType<RMoveManager>().Keyed<IMoveManager>(MoveType.R);
            builder.RegisterType<MMoveManager>().Keyed<IMoveManager>(MoveType.M);

            builder.RegisterType<EmptyTileManager>().Keyed<ITileManager>(TileType.Empty);
            builder.RegisterType<MineTileManager>().Keyed<ITileManager>(TileType.Mine);
            builder.RegisterType<ExitTileManager>().Keyed<ITileManager>(TileType.Exit);

            // Shared
            builder.RegisterType<FileUtility>().AsSelf();

            // DI
            builder.RegisterType<DIFactory>().As<IDIFactory>();
        }
    }
}
