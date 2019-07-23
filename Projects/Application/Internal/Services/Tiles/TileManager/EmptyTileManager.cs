using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Enums;

namespace Application.Internal.Services.Tiles.TileManager
{
    internal class EmptyTileManager : ITileManager
    {
        public ResultType GetResult() => ResultType.StillInDanger;
    }
}
