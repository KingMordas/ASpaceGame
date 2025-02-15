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
