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
using ASpaceGame.WPF.Dtos.Entities;
using AutoMapper;

namespace ASpaceGame.WPF.Mappers;

public class AutoMapperConfiguration
{
    public static IMapper? Mapper { get; private set; }

    public static void Initialize()
    {
        MapperConfiguration config = new(cfg =>
        {
            _ = cfg.CreateMap<Officer, DtoOfficer>()
                .ForMember(dest => dest.ScienceSkill, opt => opt.MapFrom(src => src.Skills[OfficerSkillsEnum.Science]))
                .ForMember(dest => dest.EngineeringSkill, opt => opt.MapFrom(src => src.Skills[OfficerSkillsEnum.Engineering]))
                .ForMember(dest => dest.MedicalSkill, opt => opt.MapFrom(src => src.Skills[OfficerSkillsEnum.Medical]))
                .ForMember(dest => dest.SecuritySkill, opt => opt.MapFrom(src => src.Skills[OfficerSkillsEnum.Security]))
                .ForMember(dest => dest.OperationSkill, opt => opt.MapFrom(src => src.Skills[OfficerSkillsEnum.Operation]))
                .ForMember(dest => dest.CommandSkill, opt => opt.MapFrom(src => src.Skills[OfficerSkillsEnum.Command]));

            _ = cfg.CreateMap<DtoOfficer, Officer>()
                .ConstructUsing((src, context) =>
                {
                    Officer officer = new()
                    {
                        Name = src.Name ?? string.Empty,
                        Picture = src.Picture ?? [],
                        Biography = src.Biography ?? string.Empty,
                        Rank = Enum.TryParse(src.Rank, out OfficerRanksEnum rank) ? rank : OfficerRanksEnum.Ensign
                    };

                    officer.SetSkill(OfficerSkillsEnum.Science, src.ScienceSkill);
                    officer.SetSkill(OfficerSkillsEnum.Engineering, src.EngineeringSkill);
                    officer.SetSkill(OfficerSkillsEnum.Medical, src.MedicalSkill);
                    officer.SetSkill(OfficerSkillsEnum.Security, src.SecuritySkill);
                    officer.SetSkill(OfficerSkillsEnum.Operation, src.OperationSkill);
                    officer.SetSkill(OfficerSkillsEnum.Command, src.CommandSkill);

                    return officer;
                });
        });

        Mapper = config.CreateMapper();
    }
}
