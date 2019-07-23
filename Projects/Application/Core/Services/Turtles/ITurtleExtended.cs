using Domain.Core.Entities;
using Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Services.Turtles
{
    public interface ITurtleExtended
    {
        Turtle Turtle { get; }
        IEnumerable<MoveType> Moves { get; }
    }
}
