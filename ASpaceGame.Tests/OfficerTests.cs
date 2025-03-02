/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas - https://github.com/KingMordas/ASpaceGame
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

using ASpaceGame.CoreComponents;
using ASpaceGame.CoreComponents.Enums;
using ASpaceGame.CoreComponents.Repositories;
using ASpaceGame.CoreComponents.Utilities;
using AutoMapper;
using Moq;
using System.Reflection;

namespace ASpaceGame.Tests;

public class OfficerTests
{
    private readonly Mock<IRepoManagement> _repoManagementMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<SkillsUtilities> _skillsUtilitiesMock;
    private readonly Officer _officer;

    public OfficerTests()
    {
        _repoManagementMock = new Mock<IRepoManagement>();
        _mapperMock = new Mock<IMapper>();
        _skillsUtilitiesMock = new Mock<SkillsUtilities>();
        _officer = new Officer();

        // Use reflection to set the private fields
        typeof(Officer).GetField("_repoManagement", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.SetValue(_officer, _repoManagementMock.Object);
        typeof(Officer).GetField("_mapper", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.SetValue(_officer, _mapperMock.Object);
        typeof(Officer).GetField("_skillsUtilities", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.SetValue(_officer, _skillsUtilitiesMock.Object);
    }

    [Fact]
    public void Save_ShouldCallRepoManagementSave()
    {
        // Act
        _officer.Save();

        // Assert
        _repoManagementMock.Verify(r => r.Save(_officer), Times.Once);
    }

    [Fact]
    public void Load_ShouldCallRepoManagementLoadAndMap()
    {
        // Arrange
        Guid officerId = Guid.NewGuid();
        Officer loadedOfficer = new()
        {
            Id = officerId
        };
        typeof(Officer).GetField("_repoManagement", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.SetValue(loadedOfficer, _repoManagementMock.Object);
        typeof(Officer).GetField("_mapper", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.SetValue(loadedOfficer, _mapperMock.Object);

        _ = _repoManagementMock.Setup(r => r.Load<Officer>(officerId)).Returns(loadedOfficer);

        // Act
        _officer.Load(officerId);

        // Assert
        _repoManagementMock.Verify(r => r.Load<Officer>(officerId), Times.Once);
        _mapperMock.Verify(m => m.Map(loadedOfficer, _officer), Times.Once);
    }

    [Fact]
    public void Delete_ShouldCallRepoManagementDelete()
    {
        // Act
        _officer.Delete();

        // Assert
        _repoManagementMock.Verify(r => r.Delete<Officer>(_officer.Id), Times.Once);
    }

    [Fact]
    public void SetSkill_ShouldAddOrUpdateSkill()
    {
        // Act
        _officer.SetSkill(OfficerSkillsEnum.Command, 5);

        // Assert
        Assert.Equal(5, _officer.Skills[OfficerSkillsEnum.Command]);
    }

    [Fact]
    public void TestSkill_ShouldReturnTrueIfSkillPlusBonusIsGreaterThanDiceResult()
    {
        // Arrange
        _officer.SetSkill(OfficerSkillsEnum.Command, 50);
        int bonus = 10;
        _ = _skillsUtilitiesMock.Setup(s => s.RollDice(It.IsAny<int>(), It.IsAny<int>())).Returns(40);

        // Act
        bool result = _officer.TestSkill(OfficerSkillsEnum.Command, bonus);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestSkill_ShouldReturnTrueIfSkillPlusBonusIsGreaterThanDiceResultWithCriticalSuccess()
    {
        // Arrange
        _officer.SetSkill(OfficerSkillsEnum.Command, 50);
        int bonus = 10;
        _ = _skillsUtilitiesMock.Setup(s => s.RollDice(It.IsAny<int>(), It.IsAny<int>())).Returns(1);

        // Act
        bool result = _officer.TestSkill(OfficerSkillsEnum.Command, bonus);

        // Assert
        Assert.True(result);
        Assert.Equal(51, _officer.Skills[OfficerSkillsEnum.Command]);
    }

    [Fact]
    public void TestSkill_ShouldReturnFalseIfSkillPlusBonusIsLessThanDiceResult()
    {
        // Arrange
        _officer.SetSkill(OfficerSkillsEnum.Command, 30);
        int bonus = 10;
        _ = _skillsUtilitiesMock.Setup(s => s.RollDice(It.IsAny<int>(), It.IsAny<int>())).Returns(50);

        // Act
        bool result = _officer.TestSkill(OfficerSkillsEnum.Command, bonus);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TestSkill_ShouldReturnFalseIfSkillPlusBonusIsLessThanDiceResultWithCriticalFailure()
    {
        // Arrange
        _officer.SetSkill(OfficerSkillsEnum.Command, 30);
        int bonus = 10;
        _ = _skillsUtilitiesMock.Setup(s => s.RollDice(It.IsAny<int>(), It.IsAny<int>())).Returns(100);

        // Act
        bool result = _officer.TestSkill(OfficerSkillsEnum.Command, bonus);

        // Assert
        Assert.False(result);
        Assert.Equal(29, _officer.Skills[OfficerSkillsEnum.Command]);
    }

    [Fact]
    public void ModifySkill_ShouldIncreaseSkillByPercentage()
    {
        // Arrange
        _officer.SetSkill(OfficerSkillsEnum.Command, 50);

        // Act
        _officer.ModifySkill(OfficerSkillsEnum.Command, 0.1);

        // Assert
        Assert.Equal(55, _officer.Skills[OfficerSkillsEnum.Command]);
    }

    [Fact]
    public void ModifySkill_ShouldDecreaseSkillByPercentage()
    {
        // Arrange
        _officer.SetSkill(OfficerSkillsEnum.Command, 50);

        // Act
        _officer.ModifySkill(OfficerSkillsEnum.Command, -0.1);

        // Assert
        Assert.Equal(45, _officer.Skills[OfficerSkillsEnum.Command]);
    }

    [Fact]
    public void SetSkill_MinValue()
    {
        // Act
        _officer.SetSkill(OfficerSkillsEnum.Command, -20);

        // Assert
        Assert.Equal(1, _officer.Skills[OfficerSkillsEnum.Command]);
    }

    [Fact]
    public void SetSkill_MaxValue()
    {
        // Act
        _officer.SetSkill(OfficerSkillsEnum.Command, 101);

        // Assert
        Assert.Equal(100, _officer.Skills[OfficerSkillsEnum.Command]);
    }
}
