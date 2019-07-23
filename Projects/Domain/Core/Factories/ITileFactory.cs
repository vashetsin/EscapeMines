using Domain.Core.Entities;
using Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Factories
{
    public interface ITileFactory
    {
        Tile Create(int positionX, int positionY, TileType tileType);
    }
}
