using Application.Internal.Services.Tiles;
using Application.Internal.Services.Tiles.TileManager;
using Domain.Core.Enums;
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

namespace Application.Tests.Internal.Services.Tiles
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class TileServiceTests
    {
        private readonly TileService _instance;
        private readonly Mock<IDIFactory> _diFactoryMock;

        public TileServiceTests()
        {
            _diFactoryMock = new Mock<IDIFactory>(MockBehavior.Strict);
            _instance = new TileService(_diFactoryMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _diFactoryMock.Reset();
        }

        [Test]
        public void GetResult_ValidStatement_ShouldExecuteCorrectly()
        {
            // Arrange
            var tileType = TileType.Mine;
            var tileManagerMock = new Mock<ITileManager>(MockBehavior.Strict);
            _diFactoryMock.Setup(x => x.GetRegisteredType<ITileManager, TileType>(tileType)).Returns(tileManagerMock.Object);
            var expected = ResultType.MineHit;
            tileManagerMock.Setup(x => x.GetResult()).Returns(expected);

            // Act
            var result = _instance.GetResult(tileType);

            // Assert
            result.ShouldBe(expected);
        }
    }
}
