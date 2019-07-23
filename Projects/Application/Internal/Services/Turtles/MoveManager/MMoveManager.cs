using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Domain.Core.Factories;

namespace Application.Internal.Services.Turtles.MoveManager
{
    internal class MMoveManager : IMoveManager
    {
        private readonly ITurtleFactory _turtleFactory;

        public MMoveManager(ITurtleFactory turtleFactory)
        {
            _turtleFactory = turtleFactory;
        }

        public bool TryMoveTurtle(Turtle turtle, int maxPositionX, int maxPositionY)
        {
            var currentPositionX = turtle.CurrentPositionX;
            var currentPositionY = turtle.CurrentPositionY;
            switch (turtle.CurrentDirection)
            {
                case DirectionType.N:
                    currentPositionY--;
                    break;
                case DirectionType.E:
                    currentPositionX++;
                    break;
                case DirectionType.S:
                    currentPositionY++;
                    break;
                case DirectionType.W:
                    currentPositionX--;
                    break;
                default:
                    throw new NotImplementedException();
            }
            if (currentPositionX > maxPositionX
                || currentPositionX < 0
                || currentPositionY > maxPositionY
                || currentPositionY < 0)
            {
                // Turtle keeps hitting a wall untill it changes direction, 
                // otherwise we can introduce a new exception for this case
                return false;
            }
            else
            {
                _turtleFactory.UpdatePosition(turtle, currentPositionX, currentPositionY);
                return true;
            }
        }
    }
}
