using Domain.Core.Entities;
using Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Factories
{
    public interface ITurtleFactory
    {
        Turtle Create(int positionX, int positionY, DirectionType direction);

        Turtle UpdateDirection(Turtle turtle, DirectionType direction);

        Turtle UpdatePosition(Turtle turtle, int positionX, int positionY);
    }
}
