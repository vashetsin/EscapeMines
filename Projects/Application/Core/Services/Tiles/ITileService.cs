using Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Services.Tiles
{
    public interface ITileService
    {
        ResultType GetResult(TileType tileType);
    }
}
