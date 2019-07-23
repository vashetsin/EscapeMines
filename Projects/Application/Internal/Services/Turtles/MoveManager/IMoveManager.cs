using Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Internal.Services.Turtles.MoveManager
{
    internal interface IMoveManager
    {
        bool TryMoveTurtle(Turtle turtle, int maxPositionX, int maxPositionY);
    }
}
