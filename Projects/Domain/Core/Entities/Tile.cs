using Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities
{
    public class Tile
    {
        internal Tile() { }

        public int PositionX { get; internal set; }
        public int PositionY { get; internal set; }
        public TileType TileType { get; internal set; }
    }
}
