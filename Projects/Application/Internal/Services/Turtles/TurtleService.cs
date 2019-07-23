using Application.Core.Services.MineFields;
using Application.Core.Services.Turtles;
using Application.Internal.Services.Turtles.MoveManager;
using Application.Internal.Services.Turtles.Validator;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Infrastructure.Core.Persistence;
using Shared.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Internal.Services.Turtles
{
    internal class TurtleService : ITurtleService
    {
        private readonly ITurtleRepository _turtleRepository;
        private readonly TurtleValidator _validator;
        private readonly IDIFactory _diFactory;

        public TurtleService(
            ITurtleRepository turtleRepository,
            TurtleValidator validator,
            IDIFactory diFactory)
        {
            _turtleRepository = turtleRepository;
            _validator = validator;
            _diFactory = diFactory;
        }

        public ITurtleExtended InitTurtle(IMineFieldExtended mineField)
        {
            var turtle = _turtleRepository.GetTurtle();
            _validator.ValidateTurtle(turtle, mineField);
            var moves = _turtleRepository.GetMoves();

            return new TurtleExtended(turtle, moves);
        }

        public bool TryMoveTurtle(Turtle turtle, MoveType moveType, int maxPositionX, int maxPositionY)
        {
            var moveManager = _diFactory.GetRegisteredType<IMoveManager, MoveType>(moveType);
            return moveManager.TryMoveTurtle(turtle, maxPositionX, maxPositionY);
        }
    }
}
