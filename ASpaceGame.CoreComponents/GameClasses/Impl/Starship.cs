using ASpaceGame.CoreComponents.DTOs;
using ASpaceGame.CoreComponents.Enums;

namespace ASpaceGame.CoreComponents.GameClasses.Impl;

public class Starship(
    string name,
    IShipClass shipClass,
    int sciencePersonnelPerc,
    int engineeringPersonnelPerc,
    int medicalPersonnelPerc,
    int securityPersonnelPerc,
    int operationsPersonnelPerc) : IStarship
{
    private string _name = name;
    private IShipClass _shipClass = shipClass;
    private readonly Dictionary<OfficerRolesEnum, IOfficer> _officers = [];
    private readonly Dictionary<CrewDivisionsEnum, int> _crewAllocation = new()
    {
        { CrewDivisionsEnum.Science, sciencePersonnelPerc },
        { CrewDivisionsEnum.Engineering, engineeringPersonnelPerc },
        { CrewDivisionsEnum.Medical, medicalPersonnelPerc },
        { CrewDivisionsEnum.Security, securityPersonnelPerc },
        { CrewDivisionsEnum.Operations, operationsPersonnelPerc }
    };

    public string Name => _name;
    public IShipClass ShipClass => _shipClass;

    public void RenameStarship(string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            _name = name;
        }
    }
    public void ChangeShipClass(IShipClass shipClass)
    {
        if (shipClass != null)
        {
            _shipClass = shipClass;
        }
    }
    public IOfficer GetOfficerByRole(OfficerRolesEnum role) => _officers[role];
    public void SetOfficerInRole(IOfficer officer, OfficerRolesEnum role)
    {
        if (officer != null && !_officers.TryAdd(role, officer))
        {
            _officers[role] = officer;
        }
    }
    public void RemoveOfficerFromRole(OfficerRolesEnum role) => _officers.Remove(role);
    public int GetCrewCountInDivision(CrewDivisionsEnum division)
    {
        int totalCrew = _shipClass.CrewComplement;
        int totalAllocated = 0;
        var divisionCounts = new Dictionary<CrewDivisionsEnum, int>();

        foreach (var div in _crewAllocation.Keys)
        {
            int allocated = (int)(totalCrew * _crewAllocation[div] / 100d);
            divisionCounts[div] = allocated;
            totalAllocated += allocated;
        }

        int remaining = totalCrew - totalAllocated;
        foreach (var div in _crewAllocation.Keys)
        {
            if (remaining == 0)
                break;
            divisionCounts[div]++;
            remaining--;
        }

        return divisionCounts[division];
    }
    public void SetCrewAllocation(CrewDivisionsEnum division, int percentage)
    {
        if (percentage >= 0 && percentage <= 100)
        {
            _crewAllocation[division] = percentage;
        }
    }

    public double GetIdlePowerBalance() => _shipClass.Facilities.Where(facility => facility != null).Sum(facility => facility!.IdlePowerConsumption);
    public double GetMaxAvailablePower() => _shipClass.Facilities.Where(facility => facility != null && facility.PowerProvided > 0).Sum(facility => facility!.PowerProvided);
    public bool AddShipFacility(IShipFacility facility) => AddRemoveShipFacility(facility, true);
    public bool RemoveShipFacility(IShipFacility facility) => AddRemoveShipFacility(facility, false);
    public int GetFacilityCount() => _shipClass.Facilities.Count(f => f != null);
    public int GetFreeFacilitySlots() => _shipClass.Facilities.Count(f => f == null);
    public bool TestOfficerSkill(OfficerRolesEnum role, OfficerSkillsEnum skill) => _officers.ContainsKey(role) && _officers[role].EvaluateSkill(skill);
    public void ModifyOfficerSkill(OfficerRolesEnum role, OfficerSkillsEnum skill, double percModifier)
    {
        if (_officers.TryGetValue(role, out IOfficer? value))
        {
            value.ModifySkill(skill, percModifier);
        }
    }
    public bool TestFacility(IShipFacility facility, int facilitiesRequired = 1, TestFacilityDto? testParameters = null) => facility != null && _shipClass.TestFacility(facility, GetIdlePowerBalance(), GetMaxAvailablePower(), facilitiesRequired, testParameters);
    public bool TestAllRequiredFacilitiesInstallation() => _shipClass.TestAllRequiredFacilitiesInstallation();

    private bool AddRemoveShipFacility(IShipFacility facility, bool add)
    {
        int index = Array.FindIndex(_shipClass.Facilities, f => f == (add ? null : facility));

        if (index != -1)
        {
            _shipClass.Facilities[index] = add ? facility : null;
        }

        return index != -1;
    }
}
