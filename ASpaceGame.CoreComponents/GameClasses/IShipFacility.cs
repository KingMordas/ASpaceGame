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

namespace ASpaceGame.CoreComponents.GameClasses;
public interface IShipFacility
{
    int Count { get; set; }
    int? CurrentCapacity { get; set; }
    FacilityTypesEnum FacilityType { get; }
    int? HoldingCapacity { get; }
    double IdlePowerConsumption { get; }
    string Name { get; }
    double PowerProvided { get; }
    double? Strength { get; set; }

    bool DamageFacility();
    bool IsDamaged();
    bool IsDamaged(int count);
    bool RepairFacility();
    bool TestFacility(double currentIdlePowerConsumption, double maxPowerAvailable, int requiredFacilities = 1, TestFacilityDto? testParameters = null);
}
