using ASpaceGame.CoreComponents.Attributes;
using ASpaceGame.CoreComponents.DTOs;
using ASpaceGame.CoreComponents.Enums;
using ASpaceGame.CoreComponents.Utilities;

namespace ASpaceGame.CoreComponents.GameClasses.Impl;

public class ShipFacility(
    string name,
    FacilityTypesEnum facilityType,
    double powerProvided,
    int? holdingCapacity) : IShipFacility
{
    private readonly string _name = name;
    private readonly FacilityTypesEnum _facilityType = facilityType;
    private readonly double _powerProvided = powerProvided;
    private readonly double _idlePowerConsumption = powerProvided < 0 ? Math.Abs(powerProvided * GameConstants.IdlePowerConsumptionFactor) : 0;
    private int _count = 0;
    private bool[]? _isDamaged = null;

    private double? _strength = null;
    private readonly int? _holdingCapacity = holdingCapacity;
    private int? _currentCapacity = null;

    public string Name => _name;
    public FacilityTypesEnum FacilityType => _facilityType;
    public double PowerProvided => _powerProvided * Count;
    public double IdlePowerConsumption => _idlePowerConsumption * Count;
    public int Count
    {
        get => _count;
        set
        {
            int minRequiredFacilities = FacilitiesUtils.GetMinRequiredFacilityByType(_facilityType);
            int maxInstallableFacility = FacilitiesUtils.GetMaxInstallableFacilityByType(_facilityType);

            if (value < minRequiredFacilities)
            {
                throw new ArgumentOutOfRangeException(nameof(value), $"There must be at least {minRequiredFacilities} facility/ies installed.");
            }

            if (value > maxInstallableFacility)
            {
                throw new ArgumentOutOfRangeException(nameof(value), $"There can only be a maximum of {maxInstallableFacility} facility/ies installed.");
            }

            _count = value;
            _isDamaged = new bool[_count];
        }
    }

    public double? Strength { get => _strength; set => _strength = value; }
    public int? HoldingCapacity { get => _holdingCapacity; }
    public int? CurrentCapacity
    {
        get
        {
            return _currentCapacity;
        }
        set
        {
            if (_holdingCapacity == null)
            {
                throw new InvalidOperationException("This facility does not have a holding capacity.");
            }

            if (value < 0)
            {
                _currentCapacity = 0;
            }
            else if (value > _holdingCapacity)
            {
                _currentCapacity = _holdingCapacity;
            }
            else
            {
                _currentCapacity = value;
            }
        }
    }

    /// <summary>
    /// Tests the facility to see if it can be used.
    /// </summary>
    /// <param name="currentIdlePowerConsumption">The ship current power need</param>
    /// <param name="maxPowerAvailable">The max ship available power</param>
    /// <param name="requiredFacilities">The number of facilities to be tested</param>
    /// <returns>TRUE if the facilities have enough power and can be used, FALSE otherwise</returns>
    public bool TestFacility(double currentIdlePowerConsumption, double maxPowerAvailable, int requiredFacilities = 1, TestFacilityDto? testParameters = null)
    {
        bool result = !IsDamaged(requiredFacilities) && CheckAvailablePower(currentIdlePowerConsumption, maxPowerAvailable, requiredFacilities);

        switch (_facilityType)
        {
            default:
            case FacilityTypesEnum.Defense_CloakingDevice:
            case FacilityTypesEnum.Defense_ShieldGenerator:
            case FacilityTypesEnum.Offense_EnergyArray:
            case FacilityTypesEnum.Facilities_Arboretum:
            case FacilityTypesEnum.Facilities_CrewLounge:
            case FacilityTypesEnum.Facilities_HoloChamber:
            case FacilityTypesEnum.Facilities_Salon:
            case FacilityTypesEnum.Facilities_CrewQuarters:
            case FacilityTypesEnum.Facilities_AuxiliaryControl:
            case FacilityTypesEnum.Facilities_CulturalAnthropologyLab:
            case FacilityTypesEnum.Facilities_CyberneticsLab:
            case FacilityTypesEnum.Facilities_ExobiologyLab:
            case FacilityTypesEnum.Facilities_PlanetaryLab:
            case FacilityTypesEnum.Facilities_StellarCartographyLab:
            case FacilityTypesEnum.Facilities_ForceManipulatorBeamDevice:
            case FacilityTypesEnum.Facilities_Sickbay:
            case FacilityTypesEnum.Facilities_CommunicationArray:
            case FacilityTypesEnum.Device_SublightEngine:
            case FacilityTypesEnum.Device_AntimatterEngine:
                break;

            case FacilityTypesEnum.Facilities_Launcher:
                result = result && CheckAvailableAmmunition(testParameters);
                break;

            case FacilityTypesEnum.Facilities_DisplacementRoom:
                result = result && CheckAvailableTransporterPads(testParameters);
                break;

            case FacilityTypesEnum.Facilities_CargoBay:
            case FacilityTypesEnum.Facilities_DeuteriumTank:
            case FacilityTypesEnum.Facilities_AntimatterTank:
                result = result && CheckAvailableCargoSpace(testParameters);
                break;

            case FacilityTypesEnum.Facilities_SensorArray:
                result = result && CheckAvailableRange(testParameters);
                break;

            case FacilityTypesEnum.Facilities_HangarBay:
                result = result && CheckAvailableShuttle(testParameters);
                break;
        }

        return result;
    }

    /// <summary>
    /// Checks if the facility can be used based on the number of facilities required.
    /// 
    /// Ex.
    /// I have 5 facilities total
    /// I need to use 3
    /// I have 4 damaged
    /// Result: TRUE
    /// 
    /// Ex.
    /// I have 5 facilities total
    /// I need to use 3
    /// I have 1 damaged
    /// Result: FALSE
    /// </summary>
    /// <param name="count">Number of facilities required to be undamaged</param>
    /// <returns>TRUE if I have no sufficient undamaged facilities available, FALSE otherwise</returns>
    public bool IsDamaged(int count)
    {
        int damagedCount = _isDamaged?.Count(d => d) ?? 0;
        int undamagedCount = _count - damagedCount;

        return undamagedCount < count;
    }

    /// <summary>
    /// Checks if all facilities are damaged, therefore unusable.
    /// </summary>
    /// <returns>TRUE if ALL installed facilities are damaged, FALSE if there's at least one available</returns>
    public bool IsDamaged()
    {
        return IsDamaged(_count);
    }

    /// <summary>
    /// Damages the first undamaged facility found.
    /// </summary>
    /// <returns>The status of the installed facilities</returns>
    public bool DamageFacility()
    {
        if (_isDamaged != null)
        {
            int index = Array.FindIndex(_isDamaged, d => !d);

            if (index != -1)
            {
                _isDamaged[index] = true;
            }
        }

        return IsDamaged();
    }

    /// <summary>
    /// Repairs the first damaged facility found.
    /// </summary>
    /// <returns>The status of the installed facilities</returns>
    public bool RepairFacility()
    {
        if (_isDamaged != null)
        {
            int index = Array.FindIndex(_isDamaged, d => d);

            if (index != -1)
            {
                _isDamaged[index] = false;
            }
        }

        return IsDamaged();
    }

    private bool CheckAvailablePower(double currentIdlePowerConsumption, double maxPowerAvailable, int facilitiesRequired = 1)
    {
        if (_powerProvided < 0)
        {
            double powerConsumptionWithoutFacilities = currentIdlePowerConsumption - IdlePowerConsumption;
            double singleIdlePowerConsumption = IdlePowerConsumption / Count;
            double requiredPowerConsumption = _powerProvided * facilitiesRequired;
            double otherIdlePowerConsumption = singleIdlePowerConsumption * (Count - facilitiesRequired);
            double netPowerConsumption = powerConsumptionWithoutFacilities + requiredPowerConsumption + otherIdlePowerConsumption;

            return netPowerConsumption <= maxPowerAvailable;
        }
        else
        {
            return true;
        }
    }
    private bool CheckAvailableAmmunition(TestFacilityDto? testParameters) => testParameters != null
            && _currentCapacity != null
            && _currentCapacity.HasValue
            && (testParameters.IsFiringTorpedoes && testParameters.TorpedoesRequired <= _currentCapacity.Value
            || testParameters.IsFiringProbes && testParameters.ProbesRequired <= _currentCapacity.Value);

    private bool CheckAvailableTransporterPads(TestFacilityDto? testParameters) => testParameters != null && _currentCapacity != null && _currentCapacity.HasValue && testParameters.TransporterPadsRequired <= _currentCapacity.Value;
    private bool CheckAvailableCargoSpace(TestFacilityDto? testParameters) => testParameters != null && _currentCapacity != null && _currentCapacity.HasValue && testParameters.CargoSpaceRequired <= _currentCapacity.Value;
    private bool CheckAvailableRange(TestFacilityDto? testParameters) => testParameters != null && _currentCapacity != null && _currentCapacity.HasValue && testParameters.RangeRequired <= _currentCapacity.Value;
    private bool CheckAvailableShuttle(TestFacilityDto? testParameters) => testParameters != null && _currentCapacity != null && _currentCapacity.HasValue && testParameters.ShuttlesRequired <= _currentCapacity.Value;
}
