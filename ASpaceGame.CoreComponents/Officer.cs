/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas - https://github.com/KingMordas/ASpaceGame
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

using ASpaceGame.CoreComponents.Enums;
using ASpaceGame.CoreComponents.Interfaces;
using ASpaceGame.CoreComponents.Mappers;
using ASpaceGame.CoreComponents.Repositories;
using ASpaceGame.CoreComponents.Utilities;
using AutoMapper;

namespace ASpaceGame.CoreComponents;

public class Officer : IGameEntity
{
    private readonly IRepoManagement _repoManagement;
    private readonly IMapper _mapper;
    private readonly SkillsUtilities _skillsUtilities;

    public Guid Id { get; set; }
    public string Name { get; set; }
    public OfficerRanksEnum Rank { get; set; }
    public byte[] Picture { get; set; }
    public Dictionary<OfficerSkillsEnum, int> Skills { get; set; }
    public string Biography { get; set; }

    public Officer()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        Rank = OfficerRanksEnum.Ensign;
        Picture = [];
        Skills = new Dictionary<OfficerSkillsEnum, int>()
        {
            { OfficerSkillsEnum.Command, 1 },
            { OfficerSkillsEnum.Science, 1 },
            { OfficerSkillsEnum.Engineering, 1 },
            { OfficerSkillsEnum.Medical, 1 },
            { OfficerSkillsEnum.Security, 1 },
            { OfficerSkillsEnum.Operation, 1 }
        };
        Biography = string.Empty;
        _repoManagement = ASpaceGameStarter.RepoManagement;
        _mapper = AutoMapperConfig.Initialize();
        _skillsUtilities = new SkillsUtilities();
    }

    public void Save()
    {
        _repoManagement.Save(this);
    }
    public void Load(Guid id)
    {
        Officer? loadedOfficer = _repoManagement.Load<Officer>(id);

        if (loadedOfficer != null)
        {
            _ = _mapper.Map(loadedOfficer, this);
        }
    }
    public void Delete()
    {
        _repoManagement.Delete<Officer>(Id);
    }

    public void SetSkill(OfficerSkillsEnum skill, int value)
    {
        int normalizedValue = SkillsUtilities.NormalizeSkill(value);

        if (!Skills.TryAdd(skill, normalizedValue))
        {
            Skills[skill] = normalizedValue;
        }
    }
    public bool TestSkill(OfficerSkillsEnum skillToEvaluate, int bonus = 0)
    {
        int skillValue = Skills[skillToEvaluate];
        int diceResult = _skillsUtilities.RollDice(100);
        bool success;

        if (diceResult == 1)
        {
            ModifySkill(skillToEvaluate, 0.02);
            success = true;
        }
        else if (diceResult == 100)
        {
            ModifySkill(skillToEvaluate, -0.02);
            success = false;
        }
        else
        {
            success = skillValue + bonus >= diceResult;
        }

        return success;
    }
    public void ModifySkill(OfficerSkillsEnum skillToModify, double percModifier)
    {
        double pointsOfReduction = Math.Round(Skills[skillToModify] * percModifier, 0);

        if (pointsOfReduction == 0)
        {
            pointsOfReduction = 1;
        }

        SetSkill(skillToModify, Skills[skillToModify] + (int)pointsOfReduction);
    }
}
