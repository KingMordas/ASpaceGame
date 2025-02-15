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

namespace ASpaceGame.CoreComponents.GameClasses.Impl;

public class Officer(
    string name,
    double scienceSkill,
    double engineeringSkill,
    double medicalSkill,
    double securitySkill,
    double operationSkill) : IOfficer
{
    private readonly Random _random = new();
    public Dictionary<OfficerSkillsEnum, double> _skills = new()
    {
        { OfficerSkillsEnum.Science, Math.Round(scienceSkill, 2) },
        { OfficerSkillsEnum.Engineering, Math.Round(engineeringSkill, 2) },
        { OfficerSkillsEnum.Medical, Math.Round(medicalSkill, 2) },
        { OfficerSkillsEnum.Security, Math.Round(securitySkill, 2) },
        { OfficerSkillsEnum.Operation, Math.Round(operationSkill, 2) }
    };

    public string Name { get; set; } = name;
    public double GetSkill(OfficerSkillsEnum skill) => Math.Round(_skills[skill], 2);

    public bool EvaluateSkill(OfficerSkillsEnum skill)
    {
        double randomValue = Math.Round(_random.NextDouble() * 100, 2); // Generates a random double value between 0.00 and 100.00 inclusive

        if (randomValue == 0.00)
        {
            // Critical failure, ability is reduced by 5%
            ModifySkill(skill, -GameConstants.OfficerSkillPercModifierForCriticals);
            return false;
        }
        if (randomValue == 100.00)
        {
            // Critical success, ability is increased by 5%
            ModifySkill(skill, GameConstants.OfficerSkillPercModifierForCriticals);
            return true;
        }

        return randomValue <= _skills[skill];
    }
    public void ModifySkill(OfficerSkillsEnum skill, double percModifier)
    {
        _skills[skill] = Math.Round(_skills[skill] + Math.Round(_skills[skill] * percModifier / 100d, 2), 2, MidpointRounding.ToEven);

        // Check if the skill is within the bounds of 0 and 100
        if (_skills[skill] < 0.00)
        {
            _skills[skill] = 0.00;
        }
        else if (_skills[skill] > 100.00)
        {
            _skills[skill] = 100.00;
        }
    }
}
