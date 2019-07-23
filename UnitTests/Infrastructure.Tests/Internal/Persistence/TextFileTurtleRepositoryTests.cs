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
    public class TextFileTurtleRepositoryTests
    {
        private const string _settingsFilePath = "DummySettingsFilePath";
        private readonly TextFileTurtleRepository _instance;
        private readonly Mock<ISettings> _settingsMock;
        private readonly Mock<FileUtility> _fileUtilityMock;
        private readonly Mock<ITurtleFactory> _turtleFactoryMock;

        public TextFileTurtleRepositoryTests()
        {
            _settingsMock = new Mock<ISettings>(MockBehavior.Strict);
            _settingsMock.Setup(x => x.SettingsFilePath).Returns(_settingsFilePath);
            _fileUtilityMock = new Mock<FileUtility>(MockBehavior.Strict);
            _turtleFactoryMock = new Mock<ITurtleFactory>(MockBehavior.Strict);
            _instance = new TextFileTurtleRepository(
                _settingsMock.Object,
                _fileUtilityMock.Object,
                _turtleFactoryMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _settingsMock.Reset();
            _fileUtilityMock.Reset();
            _turtleFactoryMock.Reset();
        }

        [Test]
        public void GetTurtleIncludingTile_ValidSettings_ReturnsValue()
        {
            // Arrange
            _fileUtilityMock.Setup(x => x.ReadLines(_settingsFilePath)).Returns(TextFileRepositoryHelper.ValidFileLines);
            var expected = new Turtle();
            _turtleFactoryMock.Setup(x => x.Create(0, 1, DirectionType.N)).Returns(expected);

            // Act
            var result = _instance.GetTurtle();

            // Assert
            result.ShouldBeSameAs(expected);
        }

        [Test]
        public void GetTurtleIncludingTile_InalidSettings1_InvalidSettingsException()
        {
            // Arrange
            _fileUtilityMock.Setup(x => x.ReadLines(_settingsFilePath)).Returns(TextFileRepositoryHelper.InvalidFileLines1);

            // Act
            Action action = () => _instance.GetTurtle();

            // Assert
            action.ShouldThrow<InvalidSettingsException>();
        }

        [Test]
        public void GetTurtleIncludingTile_InalidSettings2_InvalidSettingsException()
        {
            // Arrange
            _fileUtilityMock.Setup(x => x.ReadLines(_settingsFilePath)).Returns(TextFileRepositoryHelper.InvalidFileLines2);

            // Act
            Action action = () => _instance.GetTurtle();

            // Assert
            action.ShouldThrow<InvalidSettingsException>();
        }

        [Test]
        public void GetTurtleIncludingTile_ReadLinesFails_InvalidSettingsException()
        {
            // Arrange
            _fileUtilityMock.Setup(x => x.ReadLines(_settingsFilePath)).Throws<Exception>();

            // Act
            Action action = () => _instance.GetTurtle();

            // Assert
            action.ShouldThrow<InvalidSettingsException>();
        }

        [Test]
        public void GetMoves_ValidSettings_ReturnsValue()
        {
            // Arrange
            _fileUtilityMock.Setup(x => x.ReadLines(_settingsFilePath)).Returns(TextFileRepositoryHelper.ValidFileLines);

            // Act
            var result = _instance.GetMoves().ToList();

            // Assert
            this.ShouldSatisfyAllConditions(
                () => result.ShouldNotBeNull(),
                () => result.Count.ShouldBe(9),
                () => result[0].ShouldBe(MoveType.R),
                () => result[1].ShouldBe(MoveType.M),
                () => result[2].ShouldBe(MoveType.L),
                () => result[3].ShouldBe(MoveType.M),
                () => result[4].ShouldBe(MoveType.M),
                () => result[5].ShouldBe(MoveType.R),
                () => result[6].ShouldBe(MoveType.M),
                () => result[7].ShouldBe(MoveType.M),
                () => result[8].ShouldBe(MoveType.M));
        }

        [Test]
        public void GetMoves_InalidSettings1_InvalidSettingsException()
        {
            // Arrange
            _fileUtilityMock.Setup(x => x.ReadLines(_settingsFilePath)).Returns(TextFileRepositoryHelper.InvalidFileLines1);

            // Act
            Action action = () => _instance.GetMoves();

            // Assert
            action.ShouldThrow<InvalidSettingsException>();
        }

        [Test]
        public void GetMoves_InalidSettings2_InvalidSettingsException()
        {
            // Arrange
            _fileUtilityMock.Setup(x => x.ReadLines(_settingsFilePath)).Returns(TextFileRepositoryHelper.InvalidFileLines2);

            // Act
            Action action = () => _instance.GetMoves();

            // Assert
            action.ShouldThrow<InvalidSettingsException>();
        }

        [Test]
        public void GetMoves_ReadLinesFails_InvalidSettingsException()
        {
            // Arrange
            _fileUtilityMock.Setup(x => x.ReadLines(_settingsFilePath)).Throws<Exception>();

            // Act
            Action action = () => _instance.GetMoves();

            // Assert
            action.ShouldThrow<InvalidSettingsException>();
        }
    }
}

