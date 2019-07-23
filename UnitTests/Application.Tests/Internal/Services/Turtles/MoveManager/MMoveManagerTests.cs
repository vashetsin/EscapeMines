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
    public class MMoveManagerTests
    {
        private readonly MMoveManager _instance;
        private readonly Mock<ITurtleFactory> _turtleFactoryMock;

        public MMoveManagerTests()
        {
            _turtleFactoryMock = new Mock<ITurtleFactory>(MockBehavior.Strict);
            _instance = new MMoveManager(_turtleFactoryMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _turtleFactoryMock.Reset();
        }

        [TestCase(DirectionType.N, 1, 1, 1, 0, true)]
        [TestCase(DirectionType.E, 1, 1, 2, 1, true)]
        [TestCase(DirectionType.S, 1, 1, 1, 2, true)]
        [TestCase(DirectionType.W, 1, 1, 0, 1, true)]
        [TestCase(DirectionType.N, 0, 0, 0, 0, false)]
        [TestCase(DirectionType.W, 0, 0, 0, 0, false)]
        [TestCase(DirectionType.E, 5, 5, 5, 5, false)]
        [TestCase(DirectionType.S, 5, 5, 5, 5, false)]
        public void TryMoveTurtle_ValidStatement_ShouldExecuteCorrectly(
            DirectionType direction, int oldX, int oldY, int newX, int newY, bool expected)
        {
            // Arrange
            var maxPositionX = 5;
            var maxPositionY = 5;
            var turtle = new Turtle
            {
                CurrentDirection = direction,
                CurrentPositionX = oldX,
                CurrentPositionY = oldY
            };
            _turtleFactoryMock.Setup(x => x.UpdatePosition(turtle, newX, newY)).Returns(It.IsAny<Turtle>());

            // Act
            var result = _instance.TryMoveTurtle(turtle, maxPositionX, maxPositionY);

            // Assert
            result.ShouldBe(expected);
            _turtleFactoryMock.Verify(x => x.UpdatePosition(turtle, newX, newY), Times.Exactly(expected ? 1 : 0));
        }
    }
}
