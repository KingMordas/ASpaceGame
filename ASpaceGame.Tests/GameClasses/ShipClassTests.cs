/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

using ASpaceGame.CoreComponents.DTOs;
using ASpaceGame.CoreComponents.Enums;
using ASpaceGame.CoreComponents.GameClasses;
using ASpaceGame.CoreComponents.GameClasses.Impl;
using ASpaceGame.CoreComponents.Utilities;
using Moq;

namespace ASpaceGame.Tests.GameClasses
{
    public class ShipClassTests
    {
        private readonly ShipClass _shipClass;
        private readonly Mock<IShipFacility> _mockFacility;

        public ShipClassTests()
        {
            _mockFacility = new Mock<IShipFacility>();
            _mockFacility.Setup(f => f.FacilityType).Returns(FacilityTypesEnum.Offense_EnergyArray);
            _mockFacility.Setup(f => f.TestFacility(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<int>(), It.IsAny<TestFacilityDto>())).Returns(true);

            _shipClass = new ShipClass("Milky Way Class", 1000, 10);
            _shipClass.Facilities[0] = _mockFacility.Object;
        }

        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            Assert.Equal("Milky Way Class", _shipClass.Name);
            Assert.Equal(1000, _shipClass.CrewComplement);
            Assert.Equal(10, _shipClass.Facilities.Length);
        }

        [Fact]
        public void TestFacility_ShouldReturnTrue_WhenFacilityIsUsable()
        {
            bool result = _shipClass.TestFacility(_mockFacility.Object, 50, 200);
            Assert.True(result);
        }

        [Fact]
        public void TestFacility_ShouldReturnFalse_WhenFacilityIsNotInShip()
        {
            var anotherMockFacility = new Mock<IShipFacility>();
            anotherMockFacility.Setup(f => f.FacilityType).Returns(FacilityTypesEnum.Facilities_Launcher);

            bool result = _shipClass.TestFacility(anotherMockFacility.Object, 50, 200);
            Assert.False(result);
        }

        [Fact]
        public void TestAllRequiredFacilitiesInstallation_ShouldReturnTrue_WhenAllRequiredFacilitiesAreInstalled()
        {
            // Arrange
            List<FacilityTypesEnum> requiredFacilities = FacilitiesUtils.GetAllRequiredFacilities();

            // Mock the required facilities
            for (int i = 0; i < requiredFacilities.Count; i++)
            {
                var mockFacility = new Mock<IShipFacility>();
                mockFacility.Setup(f => f.FacilityType).Returns(requiredFacilities[i]);
                _shipClass.Facilities[i] = mockFacility.Object;
            }

            // Act
            bool result = _shipClass.TestAllRequiredFacilitiesInstallation();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void TestAllRequiredFacilitiesInstallation_ShouldReturnFalse_WhenNotAllRequiredFacilitiesAreInstalled()
        {
            // Arrange
            List<FacilityTypesEnum> requiredFacilities = FacilitiesUtils.GetAllRequiredFacilities();

            // Mock only some of the required facilities
            for (int i = 0; i < requiredFacilities.Count - 1; i++)
            {
                var mockFacility = new Mock<IShipFacility>();
                mockFacility.Setup(f => f.FacilityType).Returns(requiredFacilities[i]);
                _shipClass.Facilities[i] = mockFacility.Object;
            }

            // Act
            bool result = _shipClass.TestAllRequiredFacilitiesInstallation();

            // Assert
            Assert.False(result);
        }
    }
}
