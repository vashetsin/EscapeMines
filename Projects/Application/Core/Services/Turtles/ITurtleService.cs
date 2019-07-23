using Application.Core.Services.MineFields;
using Domain.Core.Entities;
using Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Services.Turtles
{
    public interface ITurtleService
    {
        ITurtleExtended InitTurtle(IMineFieldExtended mineField);

        bool TryMoveTurtle(Turtle turtle, MoveType moveType, int maxPositionX, int maxPositionY);
    }
}
