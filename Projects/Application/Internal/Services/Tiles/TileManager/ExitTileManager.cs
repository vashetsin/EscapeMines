using Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Internal.Services.Tiles.TileManager
{
    internal class ExitTileManager : ITileManager
    {
        public ResultType GetResult() => ResultType.Success;
    }
}
