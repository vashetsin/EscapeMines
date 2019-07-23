using AppConfig.Core;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Domain.Core.Factories;
using Infrastructure.Core.Persistence;
using Shared.Helpers;
using Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Internal.Persistence
{
    internal class TextFileTurtleRepository : TextFileRepository, ITurtleRepository
    {
        private readonly ITurtleFactory _turtleFactory;

        public TextFileTurtleRepository(
               ISettings settings,
               FileUtility fileUtility,
               ITurtleFactory turtleFactory) : base(settings, fileUtility)
        {
            _turtleFactory = turtleFactory;
        }

        public IEnumerable<MoveType> GetMoves()
        {
            return Parse(() =>
            {
                var retVal = new List<MoveType>();

                var lines = ReadLines(5);
                lines.ToList().ForEach(line =>
                {
                    line.Split(' ').ToList().ForEach(x =>
                    {
                        var moveType = (MoveType)Enum.Parse(typeof(MoveType), x);
                        retVal.Add(moveType);
                    });
                });

                return retVal;
            });
        }

        public Turtle GetTurtle()
        {
            return Parse(() =>
            {
                var line = ReadLine(4);
                var arr = line.Split(' ');
                Throw.If<Exception>(() => arr.Length != 3);
                var positionX = int.Parse(arr[0]);
                var positionY = int.Parse(arr[1]);
                var direction = (DirectionType)Enum.Parse(typeof(DirectionType), arr[2]);
                return _turtleFactory.Create(positionX, positionY, direction);
            });
        }
    }
}
