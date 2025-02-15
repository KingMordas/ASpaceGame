/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

using ASpaceGame.CoreComponents.Enums;

namespace ASpaceGame.CoreComponents.GameClasses;
public interface IOfficer
{
    string Name { get; set; }

    bool EvaluateSkill(OfficerSkillsEnum skill);
    double GetSkill(OfficerSkillsEnum skill);
    void ModifySkill(OfficerSkillsEnum skill, double percModifier);
}
