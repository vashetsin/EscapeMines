using AppConfig.Core;
using Domain.Core.Entities;
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
    public class TextFileMineFieldRepositoryTests
    {
        private const string _settingsFilePath = "DummySettingsFilePath";
        private readonly TextFileMineFieldRepository _instance;
        private readonly Mock<ISettings> _settingsMock;
        private readonly Mock<FileUtility> _fileUtilityMock;
        private readonly Mock<IMineFieldFactory> _mineFieldFactoryMock;

        public TextFileMineFieldRepositoryTests()
        {
            _settingsMock = new Mock<ISettings>(MockBehavior.Strict);
            _settingsMock.Setup(x => x.SettingsFilePath).Returns(_settingsFilePath);
            _fileUtilityMock = new Mock<FileUtility>(MockBehavior.Strict);
            _mineFieldFactoryMock = new Mock<IMineFieldFactory>(MockBehavior.Strict);
            _instance = new TextFileMineFieldRepository(
                _settingsMock.Object,
                _fileUtilityMock.Object,
                _mineFieldFactoryMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _settingsMock.Reset();
            _fileUtilityMock.Reset();
            _mineFieldFactoryMock.Reset();
        }

        [Test]
        public void GetMineField_ValidSettings_ReturnsValue()
        {
            // Arrange
            _fileUtilityMock.Setup(x => x.ReadLines(_settingsFilePath)).Returns(TextFileRepositoryHelper.ValidFileLines);
            var expected = new MineField();
            _mineFieldFactoryMock.Setup(x => x.Create(5, 4)).Returns(expected);

            // Act
            var result = _instance.GetMineField();

            // Assert
            result.ShouldBeSameAs(expected);
        }

        [Test]
        public void GetMineField_InalidSettings1_InvalidSettingsException()
        {
            // Arrange
            _fileUtilityMock.Setup(x => x.ReadLines(_settingsFilePath)).Returns(TextFileRepositoryHelper.InvalidFileLines1);

            // Act
            Action action = () => _instance.GetMineField();

            // Assert
            action.ShouldThrow<InvalidSettingsException>();
        }

        [Test]
        public void GetMineField_InalidSettings2_InvalidSettingsException()
        {
            // Arrange
            _fileUtilityMock.Setup(x => x.ReadLines(_settingsFilePath)).Returns(TextFileRepositoryHelper.InvalidFileLines2);

            // Act
            Action action = () => _instance.GetMineField();

            // Assert
            action.ShouldThrow<InvalidSettingsException>();
        }

        [Test]
        public void GetMineField_ReadLinesFails_InvalidSettingsException()
        {
            // Arrange
            _fileUtilityMock.Setup(x => x.ReadLines(_settingsFilePath)).Throws<Exception>();

            // Act
            Action action = () => _instance.GetMineField();

            // Assert
            action.ShouldThrow<InvalidSettingsException>();
        }
    }
}
