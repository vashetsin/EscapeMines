using AppConfig.Core;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Domain.Core.Factories;
using Infrastructure.Core.Exceptions;
using Infrastructure.Internal.Persistence;
using Moq;
using NUnit.Framework;
using Shared.Utils;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tests.Internal.Persistence
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class TextFileTileRepositoryTests
    {
        private const string _settingsFilePath = "DummySettingsFilePath";
        private readonly TextFileTileRepository _instance;
        private readonly Mock<ISettings> _settingsMock;
        private readonly Mock<FileUtility> _fileUtilityMock;
        private readonly Mock<ITileFactory> _tileFactoryMock;

        public TextFileTileRepositoryTests()
        {
            _settingsMock = new Mock<ISettings>(MockBehavior.Strict);
            _settingsMock.Setup(x => x.SettingsFilePath).Returns(_settingsFilePath);
            _fileUtilityMock = new Mock<FileUtility>(MockBehavior.Strict);
            _tileFactoryMock = new Mock<ITileFactory>(MockBehavior.Strict);
            _instance = new TextFileTileRepository(
                _settingsMock.Object,
                _fileUtilityMock.Object,
                _tileFactoryMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _settingsMock.Reset();
            _fileUtilityMock.Reset();
            _tileFactoryMock.Reset();
        }

        [Test]
        public void GetExit_ValidSettings_ReturnsValue()
        {
            // Arrange
            _fileUtilityMock.Setup(x => x.ReadLines(_settingsFilePath)).Returns(TextFileRepositoryHelper.ValidFileLines);
            var expected = new Tile();
            _tileFactoryMock.Setup(x => x.Create(4, 2, TileType.Exit)).Returns(expected);

            // Act
            var result = _instance.GetExit();

            // Assert
            result.ShouldBeSameAs(expected);
        }

        [Test]
        public void GetMineField_InalidSettings1_InvalidSettingsException()
        {
            // Arrange
            _fileUtilityMock.Setup(x => x.ReadLines(_settingsFilePath)).Returns(TextFileRepositoryHelper.InvalidFileLines1);

            // Act
            Action action = () => _instance.GetExit();

            // Assert
            action.ShouldThrow<InvalidSettingsException>();
        }

        [Test]
        public void GetMineField_InalidSettings2_InvalidSettingsException()
        {
            // Arrange
            _fileUtilityMock.Setup(x => x.ReadLines(_settingsFilePath)).Returns(TextFileRepositoryHelper.InvalidFileLines2);

            // Act
            Action action = () => _instance.GetExit();

            // Assert
            action.ShouldThrow<InvalidSettingsException>();
        }

        [Test]
        public void GetMineField_ReadLinesFails_InvalidSettingsException()
        {
            // Arrange
            _fileUtilityMock.Setup(x => x.ReadLines(_settingsFilePath)).Throws<Exception>();

            // Act
            Action action = () => _instance.GetExit();

            // Assert
            action.ShouldThrow<InvalidSettingsException>();
        }

        [Test]
        public void GetMines_ValidSettings_ReturnsValue()
        {
            // Arrange
            _fileUtilityMock.Setup(x => x.ReadLines(_settingsFilePath)).Returns(TextFileRepositoryHelper.ValidFileLines);
            var mine1 = new Tile();
            var mine2 = new Tile();
            var mine3 = new Tile();
            _tileFactoryMock.Setup(x => x.Create(1, 1, TileType.Mine)).Returns(mine1);
            _tileFactoryMock.Setup(x => x.Create(1, 3, TileType.Mine)).Returns(mine2);
            _tileFactoryMock.Setup(x => x.Create(3, 3, TileType.Mine)).Returns(mine3);

            // Act
            var result = _instance.GetMines().ToList();

            // Assert
            this.ShouldSatisfyAllConditions(
                () => result.ShouldNotBeNull(),
                () => result.Count.ShouldBe(3),
                () => result[0].ShouldBeSameAs(mine1),
                () => result[1].ShouldBeSameAs(mine2),
                () => result[2].ShouldBeSameAs(mine3));
        }

        [Test]
        public void GetMines_InalidSettings1_InvalidSettingsException()
        {
            // Arrange
            _fileUtilityMock.Setup(x => x.ReadLines(_settingsFilePath)).Returns(TextFileRepositoryHelper.InvalidFileLines1);

            // Act
            Action action = () => _instance.GetMines();

            // Assert
            action.ShouldThrow<InvalidSettingsException>();
        }

        [Test]
        public void GetMines_InalidSettings2_InvalidSettingsException()
        {
            // Arrange
            _fileUtilityMock.Setup(x => x.ReadLines(_settingsFilePath)).Returns(TextFileRepositoryHelper.InvalidFileLines2);

            // Act
            Action action = () => _instance.GetMines();

            // Assert
            action.ShouldThrow<InvalidSettingsException>();
        }

        [Test]
        public void GetMines_ReadLinesFails_InvalidSettingsException()
        {
            // Arrange
            _fileUtilityMock.Setup(x => x.ReadLines(_settingsFilePath)).Throws<Exception>();

            // Act
            Action action = () => _instance.GetMines();

            // Assert
            action.ShouldThrow<InvalidSettingsException>();
        }
    }
}
