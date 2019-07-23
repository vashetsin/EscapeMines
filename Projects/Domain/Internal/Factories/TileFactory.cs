using Domain.Core.Entities;
using Domain.Core.Enums;
using Domain.Core.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Internal.Factories
{
    internal class TileFactory : ITileFactory
    {
        public Tile Create(int positionX, int positionY, TileType tileType)
        {
            return new Tile
            {
                PositionX = positionX,
                PositionY = positionY,
                TileType = tileType
            };
        }
    }
}
