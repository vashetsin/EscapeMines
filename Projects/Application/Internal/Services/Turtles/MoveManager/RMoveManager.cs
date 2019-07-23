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
    internal class RMoveManager : IMoveManager
    {
        private readonly ITurtleFactory _turtleFactory;

        public RMoveManager(ITurtleFactory turtleFactory)
        {
            _turtleFactory = turtleFactory;
        }

        public bool TryMoveTurtle(Turtle turtle, int maxPositionX, int maxPositionY)
        {
            DirectionType newDrection;
            switch (turtle.CurrentDirection)
            {
                case DirectionType.N:
                    newDrection = DirectionType.E;
                    break;
                case DirectionType.E:
                    newDrection = DirectionType.S;
                    break;
                case DirectionType.S:
                    newDrection = DirectionType.W;
                    break;
                case DirectionType.W:
                    newDrection = DirectionType.N;
                    break;
                default:
                    throw new NotImplementedException();
            }
            _turtleFactory.UpdateDirection(turtle, newDrection);
            return true;
        }
    }
}
