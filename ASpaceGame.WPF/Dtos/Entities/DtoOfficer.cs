/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas - https://github.com/KingMordas/ASpaceGame
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

namespace ASpaceGame.WPF.Dtos.Entities;

public class DtoOfficer : IGameEntityUI
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? Rank { get; set; }
    public string? Biography { get; set; }
    public int CommandSkill { get; set; }
    public int ScienceSkill { get; set; }
    public int OperationSkill { get; set; }
    public int EngineeringSkill { get; set; }
    public int MedicalSkill { get; set; }
    public int SecuritySkill { get; set; }
    public byte[]? Picture { get; set; }
}
