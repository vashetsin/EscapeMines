using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities
{
    public class MineField
    {
        internal MineField() { }

        public int TilesX { get; internal set; }
        public int TilesY { get; internal set; }
    }
}
