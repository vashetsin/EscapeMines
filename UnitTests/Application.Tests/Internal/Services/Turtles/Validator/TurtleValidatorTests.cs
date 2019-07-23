using Application.Core.Services.MineFields;
using Application.Core.Services.Turtles;
using Application.Internal.Services.Turtles.Validator;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Internal.Services.Turtles.Validator
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class TurtleValidatorTests
    {
        private readonly TurtleValidator _instance;
        private readonly Mock<IMineFieldExtended> _mineFieldMock;

        public TurtleValidatorTests()
        {
            _mineFieldMock = new Mock<IMineFieldExtended>(MockBehavior.Strict);
            _instance = new TurtleValidator();
        }

        [Test]
        public void ValidateMineField_ValidStatement_ShouldExecuteCorrectly()
        {
            // Arrange
            var mineFieldMock = new Mock<IMineFieldExtended>(MockBehavior.Strict);
            mineFieldMock.Setup(x => x.MaxPositionX).Returns(2);
            mineFieldMock.Setup(x => x.MaxPositionY).Returns(2);
            mineFieldMock.Setup(x => x[1, 2]).Returns(TileType.Empty);
            var turtle = new Turtle { CurrentPositionX = 1, CurrentPositionY = 2 };

            // Act
            Action action = () => _instance.ValidateTurtle(turtle, mineFieldMock.Object);

            // Assert
            action.ShouldNotThrow();
        }

        [Test]
        public void ValidateMineField_InvalidStatement_MineFieldInitializationException()
        {
            // Arrange
            var mineFieldMock = new Mock<IMineFieldExtended>(MockBehavior.Strict);
            mineFieldMock.Setup(x => x.MaxPositionX).Returns(2);
            mineFieldMock.Setup(x => x.MaxPositionY).Returns(2);
            mineFieldMock.Setup(x => x[3, 3]).Returns(TileType.Empty);
            var turtle = new Turtle { CurrentPositionX = 3, CurrentPositionY = 3 };

            // Act
            Action action = () => _instance.ValidateTurtle(turtle, mineFieldMock.Object);

            // Assert
            action.ShouldThrow<TurtleOutOfFieldException>();
        }

        [Test]
        public void ValidateMineField_NotEmptyTile_TurtleInitialTileException()
        {
            // Arrange
            var mineFieldMock = new Mock<IMineFieldExtended>(MockBehavior.Strict);
            mineFieldMock.Setup(x => x.MaxPositionX).Returns(2);
            mineFieldMock.Setup(x => x.MaxPositionY).Returns(2);
            mineFieldMock.Setup(x => x[1, 2]).Returns(TileType.Mine);
            var turtle = new Turtle { CurrentPositionX = 1, CurrentPositionY = 2 };

            // Act
            Action action = () => _instance.ValidateTurtle(turtle, mineFieldMock.Object);

            // Assert
            action.ShouldThrow<TurtleInitialTileException>();
        }
    }
}
