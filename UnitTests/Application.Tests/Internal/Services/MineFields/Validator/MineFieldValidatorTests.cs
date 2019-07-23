using Application.Core.Services.MineFields;
using Application.Internal.Services.MineFields.Validator;
using Domain.Core.Entities;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Internal.Services.MineFields.Validator
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class MineFieldValidatorTests
    {
        private readonly MineFieldValidator _instance;

        public MineFieldValidatorTests()
        {
            _instance = new MineFieldValidator();
        }

        [Test]
        public void ValidateMineField_ValidStatement_ShouldExecuteCorrectly()
        {
            // Arrange
            var mineField = new MineField { TilesX = 2, TilesY = 2 };

            // Act
            Action action = () => _instance.ValidateMineField(mineField);

            // Assert
            action.ShouldNotThrow();
        }

        [Test]
        public void ValidateMineField_InvalidStatement_MineFieldInitializationException()
        {
            // Arrange
            var mineField = new MineField { TilesX = 0, TilesY = 1 };

            // Act
            Action action = () => _instance.ValidateMineField(mineField);

            // Assert
            action.ShouldThrow<MineFieldInitializationException>();
        }
    }
}
