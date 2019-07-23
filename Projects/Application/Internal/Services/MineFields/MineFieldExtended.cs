using Application.Core.Services.MineFields;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Internal.Services.MineFields
{
    internal class MineFieldExtended : IMineFieldExtended
    {
        private readonly TileType[,] _tiles;

        public MineFieldExtended(
            MineField mineField,
            Tile exit,
            IEnumerable<Tile> mines)
        {
            MaxPositionX = mineField.TilesX - 1;
            MaxPositionY = mineField.TilesY - 1;
            _tiles = new TileType[mineField.TilesX, mineField.TilesY];
            _tiles[exit.PositionX, exit.PositionY] = exit.TileType;
            mines.ToList().ForEach(x => _tiles[x.PositionX, x.PositionY] = x.TileType);
        }

        public int MaxPositionX { get; }
        public int MaxPositionY { get; }

        public TileType this[int x, int y]
        {
            get
            {
                Throw.If<IndexOutOfRangeException>(() => x > MaxPositionX || y > MaxPositionY);
                return _tiles[x, y];
            }
        }
    }
}
