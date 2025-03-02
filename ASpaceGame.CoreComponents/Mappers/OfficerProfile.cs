/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas - https://github.com/KingMordas/ASpaceGame
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

using AutoMapper;

namespace ASpaceGame.CoreComponents.Mappers;

public class OfficerProfile : Profile
{
    public OfficerProfile()
    {
        _ = CreateMap<Officer, Officer>();
    }
}
