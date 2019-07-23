using Domain.Core.Entities;
using Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Core.Persistence
{
    public interface ITurtleRepository
    {
        Turtle GetTurtle();

        IEnumerable<MoveType> GetMoves();
    }
}
