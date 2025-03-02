/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas - https://github.com/KingMordas/ASpaceGame
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

using ASpaceGame.CoreComponents.Attributes;

namespace ASpaceGame.CoreComponents.Enums;

public enum ShipClassPurposesEnum
{
    [StringValue("Exploration")]
    Exploration,
    [StringValue("Military")]
    Military,
    [StringValue("Cargo")]
    Cargo,
    [StringValue("Passenger")]
    Passenger,
    [StringValue("Mining")]
    Mining,
    [StringValue("Salvage")]
    Salvage,
    [StringValue("Medical")]
    Medical,
    [StringValue("Science")]
    Science
}
