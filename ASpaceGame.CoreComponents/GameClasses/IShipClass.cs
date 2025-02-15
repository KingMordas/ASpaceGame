/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

using ASpaceGame.CoreComponents.DTOs;

namespace ASpaceGame.CoreComponents.GameClasses;
public interface IShipClass
{
    int CrewComplement { get; }
    IShipFacility?[] Facilities { get; }
    string Name { get; }

    bool TestAllRequiredFacilitiesInstallation();
    bool TestFacility(IShipFacility facility, double idlePowerConsumption, double maxPowerAvailable, int requiredFacilities = 1, TestFacilityDto? testParameters = null);
}
