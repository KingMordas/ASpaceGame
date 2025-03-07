﻿/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas - https://github.com/KingMordas/ASpaceGame
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

namespace ASpaceGame.CoreComponents.Enums;

public enum FacilityTypesEnum
{
    // Defense: devices that protect the ship
    Defense_CloakingDevice, // Used to hide the ship from sensors
    Defense_ShieldGenerator, // Used to create a layer of protection around the ship and prevent hull damage

    // Offense: devices that attack other ships or stations
    Offense_EnergyArray, // Used to create an energy beam to attack other ships or stations

    // Facilities: rooms that provide services to the crew
    Facilities_Launcher, // Used to launch probes and torpedoes
    Facilities_Arboretum, // Used to grow plants and provide scientific data on them
    Facilities_CrewLounge, // Used to provide a place for the crew to relax
    Facilities_HoloChamber, // Used to provide a place for the crew to relax, play games, or simulate environments
    Facilities_Salon, // Used to provide a place for the crew to get haircuts, shaves, and other grooming
    Facilities_DisplacementRoom, // Used to transport crew or goods between places
    Facilities_CargoBay, // Used to store goods and equipment
    Facilities_ForceManipulatorBeamDevice, // Used to attract or repel objects in space
    Facilities_HangarBay, // Used to store and launch shuttles and fighters
    Facilities_Sickbay, // Used to provide medical care to the crew
    Facilities_CommunicationArray, // Used to communicate with other ships or stations
    Facilities_DeuteriumTank, // Used to store deuterium for the ship's engines
    Facilities_AntimatterTank, // Used to store antimatter for the ship's engines
    Facilities_SensorArray, // Used to detect objects in space
    Facilities_CrewQuarters, // Used to provide living space for the crew
    Facilities_AuxiliaryControl, // Used to provide a backup control room for the ship
    Facilities_CulturalAnthropologyLab, // Used to study other cultures
    Facilities_CyberneticsLab, // Used to study cybernetics
    Facilities_ExobiologyLab, // Used to study alien life forms
    Facilities_PlanetaryLab, // Used to study planets
    Facilities_StellarCartographyLab, // Used to study stars

    // Devices: equipment that provides power or propulsion to the ship
    Device_SublightEngine, // Used to move the ship at sublight speeds
    Device_AntimatterEngine, // Used to move the ship at warp speeds
    Device_PowerCore, // Used to provide power to the ship
    Device_AuxiliaryPowerCore, // Used to provide backup power to the ship
    Device_LifeSupport // Used to provide air, water, and food to the crew
}
