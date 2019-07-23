using Application.Internal.Services.Turtles.MoveManager;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Domain.Core.Factories;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Internal.Services.Turtles.MoveManager
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class LMoveManagerTests
    {
        private readonly LMoveManager _instance;
        private readonly Mock<ITurtleFactory> _turtleFactoryMock;

        public LMoveManagerTests()
        {
            _turtleFactoryMock = new Mock<ITurtleFactory>(MockBehavior.Strict);
            _instance = new LMoveManager(_turtleFactoryMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _turtleFactoryMock.Reset();
        }

        [TestCase(DirectionType.N, DirectionType.W)]
        [TestCase(DirectionType.W, DirectionType.S)]
        [TestCase(DirectionType.S, DirectionType.E)]
        [TestCase(DirectionType.E, DirectionType.N)]
        public void TryMoveTurtle_ValidStatement_ShouldExecuteCorrectly(
            DirectionType oldDirection, DirectionType newDirection)
        {
            // Arrange
            var turtle = new Turtle { CurrentDirection = oldDirection };
            _turtleFactoryMock.Setup(x => x.UpdateDirection(turtle, newDirection)).Returns(It.IsAny<Turtle>());

            // Act
            var result = _instance.TryMoveTurtle(turtle, It.IsAny<int>(), It.IsAny<int>());

            // Assert
            result.ShouldBeTrue();
            _turtleFactoryMock.Verify(x => x.UpdateDirection(turtle, newDirection), Times.Once);
        }
    }
}
