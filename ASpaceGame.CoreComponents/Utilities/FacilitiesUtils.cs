using ASpaceGame.CoreComponents.Enums;

namespace ASpaceGame.CoreComponents.Utilities;

internal static class FacilitiesUtils
{
    internal static int GetMaxInstallableFacilityByType(FacilityTypesEnum facilityType)
    {
        int maxInstallableFacility = facilityType switch
        {
            FacilityTypesEnum.Defense_CloakingDevice
                or FacilityTypesEnum.Defense_ShieldGenerator
                or FacilityTypesEnum.Facilities_CrewQuarters
                or FacilityTypesEnum.Device_SublightEngine
                or FacilityTypesEnum.Device_AntimatterEngine
                or FacilityTypesEnum.Facilities_AuxiliaryControl
              => 1,

            _ => int.MaxValue
        };

        return maxInstallableFacility;
    }

    internal static int GetMinRequiredFacilityByType(FacilityTypesEnum facilityType)
    {
        int minInstallableFacility = facilityType switch
        {
            FacilityTypesEnum.Device_AntimatterEngine
                or FacilityTypesEnum.Device_SublightEngine
                or FacilityTypesEnum.Facilities_AuxiliaryControl
                or FacilityTypesEnum.Facilities_CrewQuarters
                or FacilityTypesEnum.Facilities_AntimatterTank
                or FacilityTypesEnum.Facilities_DeuteriumTank
                or FacilityTypesEnum.Facilities_CommunicationArray
                or FacilityTypesEnum.Facilities_DisplacementRoom
              => 1,

            _ => 0
        };

        return minInstallableFacility;
    }

    internal static List<FacilityTypesEnum> GetAllRequiredFacilities()
    {
        List<FacilityTypesEnum> facilities = [];

        foreach (FacilityTypesEnum facilityType in Enum.GetValues<FacilityTypesEnum>())
        {
            if (GetMinRequiredFacilityByType(facilityType) == 1)
            {
                facilities.Add(facilityType);
            }
        }

        return facilities;
    }
}
