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
using Moq;

namespace ASpaceGame.Tests.GameClasses;

public class StarshipTests
{
    private readonly Mock<IShipClass> _mockShipClass;
    private readonly Starship _starship;

    public StarshipTests()
    {
        _mockShipClass = new Mock<IShipClass>();
        _mockShipClass.Setup(s => s.CrewComplement).Returns(433);
        _mockShipClass.Setup(s => s.Facilities).Returns(new IShipFacility?[10]);

        _starship = new Starship(
            "USS Enterprise",
            _mockShipClass.Object,
            20, 20, 20, 20, 20);
    }

    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        Assert.Equal("USS Enterprise", _starship.Name);
        Assert.Equal(_mockShipClass.Object, _starship.ShipClass);
    }

    [Fact]
    public void RenameStarship_ShouldChangeName()
    {
        _starship.RenameStarship("USS Cleopatra");
        Assert.Equal("USS Cleopatra", _starship.Name);
    }

    [Fact]
    public void ChangeShipClass_ShouldChangeShipClass()
    {
        var newMockShipClass = new Mock<IShipClass>();
        _starship.ChangeShipClass(newMockShipClass.Object);
        Assert.Equal(newMockShipClass.Object, _starship.ShipClass);
    }

    [Fact]
    public void SetOfficerInRole_ShouldAddOrUpdateOfficer()
    {
        var mockOfficer = new Mock<IOfficer>();
        _starship.SetOfficerInRole(mockOfficer.Object, OfficerRolesEnum.Captain);
        Assert.Equal(mockOfficer.Object, _starship.GetOfficerByRole(OfficerRolesEnum.Captain));
    }

    [Fact]
    public void RemoveOfficerFromRole_ShouldRemoveOfficer()
    {
        var mockOfficer = new Mock<IOfficer>();
        _starship.SetOfficerInRole(mockOfficer.Object, OfficerRolesEnum.Captain);
        _starship.RemoveOfficerFromRole(OfficerRolesEnum.Captain);
        Assert.Throws<KeyNotFoundException>(() => _starship.GetOfficerByRole(OfficerRolesEnum.Captain));
    }

    [Fact]
    public void GetCrewCountInDivision_ShouldReturnCorrectCount()
    {
        int count = _starship.GetCrewCountInDivision(CrewDivisionsEnum.Science);
        Assert.Equal(87, count);
    }

    [Fact]
    public void GetTotalCrewComplement_ShouldReturnCorrectCount()
    {
        // Arrange
        int expectedTotalCrew = 433; // This is the sum of the initial crew allocations

        // Act
        int actualTotalCrew = 0;
        foreach (CrewDivisionsEnum division in Enum.GetValues(typeof(CrewDivisionsEnum)))
        {
            actualTotalCrew += _starship.GetCrewCountInDivision(division);
        }

        // Assert
        Assert.Equal(expectedTotalCrew, actualTotalCrew);
    }

    [Fact]
    public void SetCrewAllocation_ShouldUpdateAllocation()
    {
        _starship.SetCrewAllocation(CrewDivisionsEnum.Science, 30);
        int count = _starship.GetCrewCountInDivision(CrewDivisionsEnum.Science);
        Assert.Equal(130, count);
    }

    [Fact]
    public void AddShipFacility_ShouldAddFacility()
    {
        var mockFacility = new Mock<IShipFacility>();
        bool result = _starship.AddShipFacility(mockFacility.Object);
        Assert.True(result);
    }

    [Fact]
    public void RemoveShipFacility_ShouldRemoveFacility()
    {
        var mockFacility = new Mock<IShipFacility>();
        _starship.AddShipFacility(mockFacility.Object);
        bool result = _starship.RemoveShipFacility(mockFacility.Object);
        Assert.True(result);
    }

    [Fact]
    public void TestOfficerSkill_ShouldReturnCorrectResult()
    {
        var mockOfficer = new Mock<IOfficer>();
        mockOfficer.Setup(o => o.EvaluateSkill(It.IsAny<OfficerSkillsEnum>())).Returns(true);
        _starship.SetOfficerInRole(mockOfficer.Object, OfficerRolesEnum.Captain);
        bool result = _starship.TestOfficerSkill(OfficerRolesEnum.Captain, OfficerSkillsEnum.Science);
        Assert.True(result);
    }

    [Fact]
    public void ModifyOfficerSkill_ShouldModifySkill()
    {
        var mockOfficer = new Mock<IOfficer>();
        _starship.SetOfficerInRole(mockOfficer.Object, OfficerRolesEnum.Captain);
        _starship.ModifyOfficerSkill(OfficerRolesEnum.Captain, OfficerSkillsEnum.Science, 10);
        mockOfficer.Verify(o => o.ModifySkill(OfficerSkillsEnum.Science, 10), Times.Once);
    }

    [Fact]
    public void TestFacility_ShouldReturnCorrectResult()
    {
        var mockFacility = new Mock<IShipFacility>();
        _mockShipClass.Setup(s => s.TestFacility(It.IsAny<IShipFacility>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<int>(), It.IsAny<TestFacilityDto>())).Returns(true);
        bool result = _starship.TestFacility(mockFacility.Object);
        Assert.True(result);
    }

    [Fact]
    public void TestAllRequiredFacilitiesInstallation_ShouldReturnCorrectResult()
    {
        _mockShipClass.Setup(s => s.TestAllRequiredFacilitiesInstallation()).Returns(true);
        bool result = _starship.TestAllRequiredFacilitiesInstallation();
        Assert.True(result);
    }

    [Fact]
    public void GetIdlePowerBalance_ShouldReturnCorrectValue()
    {
        var mockFacility1 = new Mock<IShipFacility>();
        mockFacility1.Setup(f => f.IdlePowerConsumption).Returns(10);
        var mockFacility2 = new Mock<IShipFacility>();
        mockFacility2.Setup(f => f.IdlePowerConsumption).Returns(20);

        _mockShipClass.Setup(s => s.Facilities).Returns(new IShipFacility?[] { mockFacility1.Object, mockFacility2.Object, null });

        var result = _starship.GetIdlePowerBalance();

        Assert.Equal(30, result);
    }

    [Fact]
    public void GetMaxAvailablePower_ShouldReturnCorrectValue()
    {
        var mockFacility1 = new Mock<IShipFacility>();
        mockFacility1.Setup(f => f.PowerProvided).Returns(50);
        var mockFacility2 = new Mock<IShipFacility>();
        mockFacility2.Setup(f => f.PowerProvided).Returns(100);

        _mockShipClass.Setup(s => s.Facilities).Returns(new IShipFacility?[] { mockFacility1.Object, mockFacility2.Object, null });

        var result = _starship.GetMaxAvailablePower();

        Assert.Equal(150, result);
    }

    [Fact]
    public void GetFacilityCount_ShouldReturnCorrectValue()
    {
        var mockFacility1 = new Mock<IShipFacility>();
        var mockFacility2 = new Mock<IShipFacility>();

        _mockShipClass.Setup(s => s.Facilities).Returns(new IShipFacility?[] { mockFacility1.Object, mockFacility2.Object, null });

        var result = _starship.GetFacilityCount();

        Assert.Equal(2, result);
    }

    [Fact]
    public void GetFreeFacilitySlots_ShouldReturnCorrectValue()
    {
        var mockFacility1 = new Mock<IShipFacility>();
        var mockFacility2 = new Mock<IShipFacility>();

        _mockShipClass.Setup(s => s.Facilities).Returns(new IShipFacility?[] { mockFacility1.Object, mockFacility2.Object, null, null });

        var result = _starship.GetFreeFacilitySlots();

        Assert.Equal(2, result);
    }
}
