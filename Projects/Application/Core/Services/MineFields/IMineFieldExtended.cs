using Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Services.MineFields
{
    public interface IMineFieldExtended
    {
        int MaxPositionX { get; }
        int MaxPositionY { get; }
        TileType this[int x, int y] { get; }
    }
}
