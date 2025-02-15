namespace ASpaceGame.CoreComponents.Enums;

public enum GameActionsEnum
{
    Event_CauseDamage, // Directly causes damage to the ship
    Event_HitShields, // Directly causes damage to the shields
    Event_SuddenDeath, // Directly causes the death of one officer

    Comm_UseInternalSys,
    Comm_UseRFTransceiver,
    Comm_UseSubspaceRadio,
    Comm_UseUniversalTranslator,

    Defense_EngageCloakingDevice,
    Defense_DisenageCloakingDevice,
    Defense_LowerShields,
    Defense_RaiseShields,

    Facilities_UseArboretum,
    Facilities_UseCrewLounge,
    Facilities_UseHolodeck,
    Facilities_UseSalon,
    Facilities_UseAuxiliaryControl,
    Facilities_UseSickbay,

    Flight_EngageImpulseEngine,
    Flight_EngageWarpEngine,
    Flight_PowerUp, // Generally used to check if the ship can power up and to leave space dock

    Offense_FireAnyEnergyWeapon,
    Offense_FirePhaserArray,
    Offense_FirePlasmaArray,
    Offense_FireDistruptorArray,
    Offense_FireAnyTorpedo,
    Offense_FirePhotonTorpedo,
    Offense_FireQuantumTorpedo,
    Offense_FireTriCobaltTorpedo,

    Messages_CaptainSpeaks,
    Messages_FirstOfficerSpeaks,
    Messages_CounselorSpeaks,
    Messages_ChiefOfOperationsSpeaks,
    Messages_HelmsmanSpeaks,
    Messages_TacticalOfficerSpeaks,
    Messages_ChiefScienceOfficerSpeaks,
    Messages_ChiefEngineerSpeaks,
    Messages_ChiefMedicalOfficerSpeaks,
    Messages_AwayTeamSpeaks,
    Messages_AnySpeaks,

    Officers_TestCaptain,
    Officers_TestFirstOfficer,
    Officers_TestCounselor,
    Officers_TestChiefOfOperations,
    Officers_TestHelmsman,
    Officers_TestTacticalOfficer,
    Officers_TestChiefScienceOfficer,
    Officers_TestChiefEngineer,
    Officers_TestChiefMedicalOfficer,

    Probes_LaunchProbe,
    Probes_GetProbeData,

    Science_UseCulturalAnthropologyLab,
    Science_UseCyberneticsLab,
    Science_UseExobiologyLab,
    Science_UsePlanetaryLab,
    Science_UseStellarCartographyLab,

    Sensors_ScanSpace,

    Facilities_UseTransporterRoom,
    Facilities_UseCargoBay,
    Facilities_UseTractorBeam,
    Facilities_LaunchShuttlecraft,
}
