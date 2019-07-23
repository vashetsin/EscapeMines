using Application.Core.Services.Turtles;
using Domain.Core.Entities;
using Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Internal.Services.Turtles
{
    internal class TurtleExtended : ITurtleExtended
    {
        private readonly List<MoveType> _moves;

        public TurtleExtended(
            Turtle turtle,
            IEnumerable<MoveType> moves)
        {
            Turtle = turtle;
            _moves = moves.ToList();
        }

        public Turtle Turtle { get; }
        public IEnumerable<MoveType> Moves => _moves.ToList();
    }
}
