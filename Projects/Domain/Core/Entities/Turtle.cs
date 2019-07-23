using Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities
{
    public class Turtle
    {
        internal Turtle() { }
        
        public int CurrentPositionX { get; internal set; }
        public int CurrentPositionY { get; internal set; }
        public DirectionType CurrentDirection { get; internal set; }
    }
}
