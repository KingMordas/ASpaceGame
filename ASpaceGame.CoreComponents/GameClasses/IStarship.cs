using ASpaceGame.CoreComponents.DTOs;
using ASpaceGame.CoreComponents.Enums;

namespace ASpaceGame.CoreComponents.GameClasses;
public interface IStarship
{
    string Name { get; }
    IShipClass ShipClass { get; }

    bool AddShipFacility(IShipFacility facility);
    void ChangeShipClass(IShipClass shipClass);
    int GetCrewCountInDivision(CrewDivisionsEnum division);
    int GetFacilityCount();
    int GetFreeFacilitySlots();
    double GetIdlePowerBalance();
    double GetMaxAvailablePower();
    IOfficer GetOfficerByRole(OfficerRolesEnum role);
    void ModifyOfficerSkill(OfficerRolesEnum role, OfficerSkillsEnum skill, double percModifier);
    void RemoveOfficerFromRole(OfficerRolesEnum role);
    bool RemoveShipFacility(IShipFacility facility);
    void RenameStarship(string name);
    void SetCrewAllocation(CrewDivisionsEnum division, int percentage);
    void SetOfficerInRole(IOfficer officer, OfficerRolesEnum role);
    bool TestAllRequiredFacilitiesInstallation();
    bool TestFacility(IShipFacility facility, int facilitiesRequired = 1, TestFacilityDto? testParameters = null);
    bool TestOfficerSkill(OfficerRolesEnum role, OfficerSkillsEnum skill);
}
