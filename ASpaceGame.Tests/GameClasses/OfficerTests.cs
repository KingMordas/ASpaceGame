/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

using ASpaceGame.CoreComponents.Attributes;
using ASpaceGame.CoreComponents.Enums;
using ASpaceGame.CoreComponents.GameClasses.Impl;
using Moq;
using System.Reflection;

namespace ASpaceGame.Tests.GameClasses
{
    public class OfficerTests
    {
        private readonly Officer _officer;

        public OfficerTests()
        {
            _officer = new Officer("John Doe", 75.5, 60.3, 85.2, 50.1, 90.0);
        }

        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            Assert.Equal("John Doe", _officer.Name);
            Assert.Equal(75.5, _officer.GetSkill(OfficerSkillsEnum.Science));
            Assert.Equal(60.3, _officer.GetSkill(OfficerSkillsEnum.Engineering));
            Assert.Equal(85.2, _officer.GetSkill(OfficerSkillsEnum.Medical));
            Assert.Equal(50.1, _officer.GetSkill(OfficerSkillsEnum.Security));
            Assert.Equal(90.0, _officer.GetSkill(OfficerSkillsEnum.Operation));
        }

        [Fact]
        public void GetSkill_ShouldReturnCorrectSkillValue()
        {
            Assert.Equal(75.5, _officer.GetSkill(OfficerSkillsEnum.Science));
            Assert.Equal(60.3, _officer.GetSkill(OfficerSkillsEnum.Engineering));
            Assert.Equal(85.2, _officer.GetSkill(OfficerSkillsEnum.Medical));
            Assert.Equal(50.1, _officer.GetSkill(OfficerSkillsEnum.Security));
            Assert.Equal(90.0, _officer.GetSkill(OfficerSkillsEnum.Operation));
        }

        [Fact]
        public void EvaluateSkill_ShouldReturnTrue_WhenRandomValueIsLessThanOrEqualToSkill()
        {
            var mockRandom = new Mock<Random>();
            mockRandom.Setup(r => r.NextDouble()).Returns(0.5);

            InjectRandom(_officer, mockRandom.Object);

            Assert.True(_officer.EvaluateSkill(OfficerSkillsEnum.Science));
        }

        [Fact]
        public void EvaluateSkill_ShouldReturnFalse_WhenRandomValueIsGreaterThanSkill()
        {
            var mockRandom = new Mock<Random>();
            mockRandom.Setup(r => r.NextDouble()).Returns(0.8);

            InjectRandom(_officer, mockRandom.Object);

            Assert.False(_officer.EvaluateSkill(OfficerSkillsEnum.Science));
        }

        [Fact]
        public void EvaluateSkill_ShouldDecreaseSkill_WhenCriticalFailure()
        {
            var mockRandom = new Mock<Random>();
            mockRandom.Setup(r => r.NextDouble()).Returns(0.0);

            InjectRandom(_officer, mockRandom.Object);

            double initialSkill = _officer.GetSkill(OfficerSkillsEnum.Science);
            _officer.EvaluateSkill(OfficerSkillsEnum.Science);
            Assert.Equal(Math.Round(initialSkill * (1.00d - GameConstants.OfficerSkillPercModifierForCriticals / 100d), 2), _officer.GetSkill(OfficerSkillsEnum.Science));
        }

        [Fact]
        public void EvaluateSkill_ShouldIncreaseSkill_WhenCriticalSuccess()
        {
            var mockRandom = new Mock<Random>();
            mockRandom.Setup(r => r.NextDouble()).Returns(1.0);

            InjectRandom(_officer, mockRandom.Object);

            double initialSkill = _officer.GetSkill(OfficerSkillsEnum.Science);
            _officer.EvaluateSkill(OfficerSkillsEnum.Science);
            Assert.Equal(Math.Round(initialSkill * (1.00d + GameConstants.OfficerSkillPercModifierForCriticals / 100d), 2), _officer.GetSkill(OfficerSkillsEnum.Science));
        }

        [Fact]
        public void ModifySkill_ShouldIncreaseSkillValue()
        {
            _officer.ModifySkill(OfficerSkillsEnum.Science, 10);
            Assert.Equal(83.05, _officer.GetSkill(OfficerSkillsEnum.Science));
        }

        [Fact]
        public void ModifySkill_ShouldDecreaseSkillValue()
        {
            _officer.ModifySkill(OfficerSkillsEnum.Science, -10);
            Assert.Equal(67.95, _officer.GetSkill(OfficerSkillsEnum.Science));
        }

        [Fact]
        public void ModifySkill_ShouldNotExceedBounds()
        {
            _officer.ModifySkill(OfficerSkillsEnum.Science, 100);
            Assert.Equal(100.00, _officer.GetSkill(OfficerSkillsEnum.Science));

            _officer.ModifySkill(OfficerSkillsEnum.Science, -200);
            Assert.Equal(0.00, _officer.GetSkill(OfficerSkillsEnum.Science));
        }

        private static void InjectRandom(Officer officer, Random random)
        {
            var randomField = typeof(Officer).GetField("_random", BindingFlags.NonPublic | BindingFlags.Instance);
            if (randomField != null)
            {
                randomField.SetValue(officer, random);
            }
            else
            {
                throw new InvalidOperationException("The _random field was not found in the Officer class.");
            }
        }
    }
}
