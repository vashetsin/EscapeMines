using Domain.Core.Entities;
using Domain.Core.Enums;
using Domain.Core.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Internal.Factories
{
    internal class TurtleFactory : ITurtleFactory
    {
        public Turtle Create(int positionX, int positionY, DirectionType direction)
        {
            return new Turtle
            {
                CurrentPositionX = positionX,
                CurrentPositionY = positionY,
                CurrentDirection = direction
            };
        }

        public Turtle UpdateDirection(Turtle turtle, DirectionType direction)
        {
            turtle.CurrentDirection = direction;
            return turtle;
        }

        public Turtle UpdatePosition(Turtle turtle, int positionX, int positionY)
        {
            turtle.CurrentPositionX = positionX;
            turtle.CurrentPositionY = positionY;
            return turtle;
        }
    }
}
