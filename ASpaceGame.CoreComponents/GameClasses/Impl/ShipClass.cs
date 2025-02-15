using ASpaceGame.CoreComponents.DTOs;
using ASpaceGame.CoreComponents.Utilities;

namespace ASpaceGame.CoreComponents.GameClasses.Impl;

public class ShipClass(
    string name,
    int crewComplement,
    int maxFacilities) : IShipClass
{
    private readonly string _name = name;
    private readonly int _crewComplement = crewComplement;
    private readonly IShipFacility?[] _facilities = new IShipFacility?[maxFacilities];

    public string Name => _name;
    public int CrewComplement => _crewComplement;
    public IShipFacility?[] Facilities => _facilities;

    /// <summary>
    /// Test if a facility can operate on the current starship
    /// </summary>
    /// <param name="facility">Facility to test</param>
    /// <param name="idlePowerConsumption">Starship total idle power consumption</param>
    /// <param name="maxPowerAvailable">Starship max power available</param>
    /// <param name="requiredFacilities">Number of required facilities to test (optional, default 1)</param>
    /// <param name="testParameters">Test parameters, when necessary (optional, default not available)</param>
    /// <returns>TRUE if the facility is working fine, FALSE otherwise</returns>
    public bool TestFacility(IShipFacility facility, double idlePowerConsumption, double maxPowerAvailable, int requiredFacilities = 1, TestFacilityDto? testParameters = null) => facility != null && _facilities.Contains(facility) && _facilities.FirstOrDefault(f => f?.FacilityType == facility.FacilityType)?.TestFacility(idlePowerConsumption, maxPowerAvailable, requiredFacilities, testParameters) == true;

    /// <summary>
    /// Check if all required facilities are installed on the starship
    /// </summary>
    /// <returns>TRUE if all required facilities are there, FALSE otherwise</returns>
    public bool TestAllRequiredFacilitiesInstallation() => FacilitiesUtils.GetAllRequiredFacilities().All(facility => _facilities.Any(f => f?.FacilityType == facility));
}
