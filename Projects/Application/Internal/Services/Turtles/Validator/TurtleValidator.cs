using Application.Core.Services.MineFields;
using Application.Core.Services.Turtles;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Internal.Services.Turtles.Validator
{
    internal class TurtleValidator
    {
        internal virtual void ValidateTurtle(Turtle turtle, IMineFieldExtended mineField)
        {
            Throw.If<TurtleOutOfFieldException>(() =>
                turtle.CurrentPositionX > mineField.MaxPositionX || turtle.CurrentPositionY > mineField.MaxPositionY);
            var currentTyle = mineField[turtle.CurrentPositionX, turtle.CurrentPositionY];
            Throw.If<TurtleInitialTileException>(() => currentTyle != TileType.Empty);
        }
    }
}
