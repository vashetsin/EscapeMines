using Application.Core.Services.MineFields;
using Application.Internal.Services.Turtles;
using Application.Internal.Services.Turtles.MoveManager;
using Application.Internal.Services.Turtles.Validator;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Infrastructure.Core.Persistence;
using Moq;
using NUnit.Framework;
using Shared.DI;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Internal.Services.Turtles
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class TurtleServiceTests
    {
        private readonly TurtleService _instance;
        private readonly Mock<ITurtleRepository> _turtleRepositoryMock;
        private readonly Mock<TurtleValidator> _validatorMock;
        private readonly Mock<IDIFactory> _diFactoryMock;

        public TurtleServiceTests()
        {
            _turtleRepositoryMock = new Mock<ITurtleRepository>(MockBehavior.Strict);
            _validatorMock = new Mock<TurtleValidator>(MockBehavior.Strict);
            _diFactoryMock = new Mock<IDIFactory>(MockBehavior.Strict);
            _instance = new TurtleService(
                _turtleRepositoryMock.Object,
                _validatorMock.Object,
                _diFactoryMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _turtleRepositoryMock.Reset();
            _validatorMock.Reset();
            _diFactoryMock.Reset();
        }

        [Test]
        public void InitMineField_ValidStatement_ShouldExecuteCorrectly()
        {
            // Arrange
            var mineFieldMock = new Mock<IMineFieldExtended>(MockBehavior.Strict);
            var turtle = new Turtle { CurrentPositionX = 3, CurrentPositionY = 3, CurrentDirection = DirectionType.N };
            var moves = new List<MoveType> { MoveType.L, MoveType.M, MoveType.R };
            _turtleRepositoryMock.Setup(x => x.GetTurtle()).Returns(turtle);
            _turtleRepositoryMock.Setup(x => x.GetMoves()).Returns(moves);
            _validatorMock.Setup(x => x.ValidateTurtle(turtle, mineFieldMock.Object)).Verifiable();

            // Act
            var result = _instance.InitTurtle(mineFieldMock.Object);

            // Assert
            result.ShouldNotBeNull();
            var turtleMoves = result.Moves.ToList();
            this.ShouldSatisfyAllConditions(
                () => result.Turtle.ShouldBeSameAs(turtle),
                () => turtleMoves.Count.ShouldBe(3),
                () => turtleMoves[0].ShouldBe(MoveType.L),
                () => turtleMoves[1].ShouldBe(MoveType.M),
                () => turtleMoves[2].ShouldBe(MoveType.R));
            _validatorMock.VerifyAll();

            // ******************************** IMPORTANT! ******************************** //

            turtleMoves[0] = MoveType.M;
            turtleMoves.Add(MoveType.M);
            var turtleMoves2 = result.Moves.ToList();
            this.ShouldSatisfyAllConditions(
                () => turtleMoves2.Count.ShouldBe(3), // Still 3
                () => turtleMoves2[0].ShouldBe(MoveType.L), // Wasn't changed
                () => turtleMoves2[1].ShouldBe(MoveType.M),
                () => turtleMoves2[2].ShouldBe(MoveType.R));

            // ******************************** IMPORTANT! ******************************** //
        }

        [Test]
        public void TryMoveTurtle_ValidStatement_ShouldExecuteCorrectly()
        {
            // Arrange
            var turtle = new Turtle();
            var moveType = It.IsAny<MoveType>();
            var maxPositionX = It.IsAny<int>();
            var maxPositionY = It.IsAny<int>();
            var moveManagerMock = new Mock<IMoveManager>(MockBehavior.Strict);
            _diFactoryMock.Setup(x => x.GetRegisteredType<IMoveManager, MoveType>(moveType)).Returns(moveManagerMock.Object);
            moveManagerMock.Setup(x => x.TryMoveTurtle(turtle, maxPositionX, maxPositionY)).Returns(true);

            // Act
            var result = _instance.TryMoveTurtle(turtle, moveType, maxPositionX, maxPositionY);

            // Assert
            result.ShouldBeTrue();
        }
    }
}
