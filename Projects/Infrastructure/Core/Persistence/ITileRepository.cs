using Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Core.Persistence
{
    public interface ITileRepository
    {
        IEnumerable<Tile> GetMines();

        Tile GetExit();
    }
}
