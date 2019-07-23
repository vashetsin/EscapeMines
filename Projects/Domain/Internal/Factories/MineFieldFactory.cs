using Domain.Core.Entities;
using Domain.Core.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Internal.Factories
{
    internal class MineFieldFactory : IMineFieldFactory
    {
        public MineField Create(int tilesX, int tilesY)
        {
            return new MineField
            {
                TilesX = tilesX,
                TilesY = tilesY
            };
        }
    }
}