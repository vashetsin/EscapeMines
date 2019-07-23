using Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Factories
{
    public interface IMineFieldFactory
    {
        MineField Create(int tilesX, int tilesY);
    }
}
