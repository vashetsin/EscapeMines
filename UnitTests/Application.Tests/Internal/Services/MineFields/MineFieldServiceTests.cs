using Application.Internal.Services.MineFields;
using Application.Internal.Services.MineFields.Validator;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Infrastructure.Core.Persistence;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Internal.Services.MineFields
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class MineFieldServiceTests
    {
        private readonly MineFieldService _instance;
        private readonly Mock<IMineFieldRepository> _mineFieldRepositoryMock;
        private readonly Mock<ITileRepository> _tileRepositoryMock;
        private readonly Mock<MineFieldValidator> _validatorMock;

        public MineFieldServiceTests()
        {
            _mineFieldRepositoryMock = new Mock<IMineFieldRepository>(MockBehavior.Strict);
            _tileRepositoryMock = new Mock<ITileRepository>(MockBehavior.Strict);
            _validatorMock = new Mock<MineFieldValidator>(MockBehavior.Strict);
            _instance = new MineFieldService(
                _mineFieldRepositoryMock.Object,
                _tileRepositoryMock.Object,
                _validatorMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mineFieldRepositoryMock.Reset();
            _tileRepositoryMock.Reset();
            _validatorMock.Reset();
        }

        [Test]
        public void InitMineField_ValidStatement_ShouldExecuteCorrectly()
        {
            // Arrange
            var mineField = new MineField { TilesX = 3, TilesY = 3 };
            var exit = new Tile { PositionX = 2, PositionY = 2, TileType = TileType.Exit };
            var mines = new List<Tile>
            {
                new Tile { PositionX = 0, PositionY = 0, TileType = TileType.Mine },
                new Tile { PositionX = 1, PositionY = 1, TileType = TileType.Mine }
            };
            _mineFieldRepositoryMock.Setup(x => x.GetMineField()).Returns(mineField);
            _tileRepositoryMock.Setup(x => x.GetExit()).Returns(exit);
            _tileRepositoryMock.Setup(x => x.GetMines()).Returns(mines);
            _validatorMock.Setup(x => x.ValidateMineField(mineField)).Verifiable();
            _validatorMock.Setup(x => x.ValidateExit(mineField, exit)).Verifiable();
            _validatorMock.Setup(x => x.ValidateMines(mineField, mines)).Verifiable();

            // Act
            var result = _instance.InitMineField();

            // Assert
            this.ShouldSatisfyAllConditions(
                () => result.ShouldNotBeNull(),
                () => result[0, 0].ShouldBe(TileType.Mine),
                () => result[1, 1].ShouldBe(TileType.Mine),
                () => result[2, 2].ShouldBe(TileType.Exit),
                () => result[0, 1].ShouldBe(TileType.Empty),
                () => result[0, 2].ShouldBe(TileType.Empty),
                () => result[1, 0].ShouldBe(TileType.Empty),
                () => result[1, 2].ShouldBe(TileType.Empty),
                () => result[2, 0].ShouldBe(TileType.Empty),
                () => result[2, 1].ShouldBe(TileType.Empty));
            _validatorMock.VerifyAll();
        }
    }
}
