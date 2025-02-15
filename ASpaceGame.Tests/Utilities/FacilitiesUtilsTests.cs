/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

using ASpaceGame.CoreComponents.Enums;
using ASpaceGame.CoreComponents.Utilities;

namespace ASpaceGame.Tests.Utilities;

public class FacilitiesUtilsTests
{
    [Theory]
    [InlineData(FacilityTypesEnum.Defense_CloakingDevice, 1)]
    [InlineData(FacilityTypesEnum.Defense_ShieldGenerator, 1)]
    [InlineData(FacilityTypesEnum.Facilities_CrewQuarters, 1)]
    [InlineData(FacilityTypesEnum.Device_SublightEngine, 1)]
    [InlineData(FacilityTypesEnum.Device_AntimatterEngine, 1)]
    [InlineData(FacilityTypesEnum.Facilities_AuxiliaryControl, 1)]
    [InlineData(FacilityTypesEnum.Facilities_Launcher, int.MaxValue)]
    [InlineData(FacilityTypesEnum.Facilities_Arboretum, int.MaxValue)]
    [InlineData(FacilityTypesEnum.Facilities_CrewLounge, int.MaxValue)]
    [InlineData(FacilityTypesEnum.Facilities_HoloChamber, int.MaxValue)]
    [InlineData(FacilityTypesEnum.Facilities_Salon, int.MaxValue)]
    [InlineData(FacilityTypesEnum.Facilities_DisplacementRoom, int.MaxValue)]
    [InlineData(FacilityTypesEnum.Facilities_CargoBay, int.MaxValue)]
    [InlineData(FacilityTypesEnum.Facilities_ForceManipulatorBeamDevice, int.MaxValue)]
    [InlineData(FacilityTypesEnum.Facilities_HangarBay, int.MaxValue)]
    [InlineData(FacilityTypesEnum.Facilities_Sickbay, int.MaxValue)]
    [InlineData(FacilityTypesEnum.Facilities_CommunicationArray, int.MaxValue)]
    [InlineData(FacilityTypesEnum.Facilities_DeuteriumTank, int.MaxValue)]
    [InlineData(FacilityTypesEnum.Facilities_AntimatterTank, int.MaxValue)]
    [InlineData(FacilityTypesEnum.Facilities_SensorArray, int.MaxValue)]
    [InlineData(FacilityTypesEnum.Facilities_CulturalAnthropologyLab, int.MaxValue)]
    [InlineData(FacilityTypesEnum.Facilities_CyberneticsLab, int.MaxValue)]
    [InlineData(FacilityTypesEnum.Facilities_ExobiologyLab, int.MaxValue)]
    [InlineData(FacilityTypesEnum.Facilities_PlanetaryLab, int.MaxValue)]
    [InlineData(FacilityTypesEnum.Facilities_StellarCartographyLab, int.MaxValue)]
    public void GetMaxInstallableFacilityByType_ReturnsExpectedValue(FacilityTypesEnum facilityType, int expected)
    {
        // Act
        int result = FacilitiesUtils.GetMaxInstallableFacilityByType(facilityType);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(FacilityTypesEnum.Defense_CloakingDevice, 0)]
    [InlineData(FacilityTypesEnum.Defense_ShieldGenerator, 0)]
    [InlineData(FacilityTypesEnum.Facilities_CrewQuarters, 1)]
    [InlineData(FacilityTypesEnum.Device_SublightEngine, 1)]
    [InlineData(FacilityTypesEnum.Device_AntimatterEngine, 1)]
    [InlineData(FacilityTypesEnum.Facilities_AuxiliaryControl, 1)]
    [InlineData(FacilityTypesEnum.Facilities_Launcher, 0)]
    [InlineData(FacilityTypesEnum.Facilities_Arboretum, 0)]
    [InlineData(FacilityTypesEnum.Facilities_CrewLounge, 0)]
    [InlineData(FacilityTypesEnum.Facilities_HoloChamber, 0)]
    [InlineData(FacilityTypesEnum.Facilities_Salon, 0)]
    [InlineData(FacilityTypesEnum.Facilities_DisplacementRoom, 1)]
    [InlineData(FacilityTypesEnum.Facilities_CargoBay, 0)]
    [InlineData(FacilityTypesEnum.Facilities_ForceManipulatorBeamDevice, 0)]
    [InlineData(FacilityTypesEnum.Facilities_HangarBay, 0)]
    [InlineData(FacilityTypesEnum.Facilities_Sickbay, 0)]
    [InlineData(FacilityTypesEnum.Facilities_CommunicationArray, 1)]
    [InlineData(FacilityTypesEnum.Facilities_DeuteriumTank, 1)]
    [InlineData(FacilityTypesEnum.Facilities_AntimatterTank, 1)]
    [InlineData(FacilityTypesEnum.Facilities_SensorArray, 0)]
    [InlineData(FacilityTypesEnum.Facilities_CulturalAnthropologyLab, 0)]
    [InlineData(FacilityTypesEnum.Facilities_CyberneticsLab, 0)]
    [InlineData(FacilityTypesEnum.Facilities_ExobiologyLab, 0)]
    [InlineData(FacilityTypesEnum.Facilities_PlanetaryLab, 0)]
    [InlineData(FacilityTypesEnum.Facilities_StellarCartographyLab, 0)]
    public void GetMinRequiredFacilityByType_ReturnsExpectedValue(FacilityTypesEnum facilityType, int expected)
    {
        // Act
        int result = FacilitiesUtils.GetMinRequiredFacilityByType(facilityType);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetAllRequiredFacilities_ReturnsExpectedFacilities()
    {
        // Act
        List<FacilityTypesEnum> result = FacilitiesUtils.GetAllRequiredFacilities();

        // Assert
        var expectedFacilities = new List<FacilityTypesEnum>
        {
            FacilityTypesEnum.Device_AntimatterEngine,
            FacilityTypesEnum.Device_SublightEngine,
            FacilityTypesEnum.Facilities_AuxiliaryControl,
            FacilityTypesEnum.Facilities_CrewQuarters,
            FacilityTypesEnum.Facilities_AntimatterTank,
            FacilityTypesEnum.Facilities_DeuteriumTank,
            FacilityTypesEnum.Facilities_CommunicationArray,
            FacilityTypesEnum.Facilities_DisplacementRoom
        };

        Assert.True(expectedFacilities.All(facility => result.Contains(facility)) && expectedFacilities.Count == result.Count);
    }
}
