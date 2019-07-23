using Application.Core.Services.Tiles;
using Application.Internal.Services.Tiles.TileManager;
using Domain.Core.Enums;
using Shared.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Internal.Services.Tiles
{
    internal class TileService : ITileService
    {
        private readonly IDIFactory _diFactory;

        public TileService(IDIFactory diFactory)
        {
            _diFactory = diFactory;
        }

        public ResultType GetResult(TileType tileType)
        {
            var tileManager = _diFactory.GetRegisteredType<ITileManager, TileType>(tileType);
            return tileManager.GetResult();
        }
    }
}
