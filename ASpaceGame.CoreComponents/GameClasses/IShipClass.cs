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
