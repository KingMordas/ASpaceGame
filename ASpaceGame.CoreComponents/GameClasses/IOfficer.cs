using ASpaceGame.CoreComponents.Enums;

namespace ASpaceGame.CoreComponents.GameClasses;
public interface IOfficer
{
    string Name { get; set; }

    bool EvaluateSkill(OfficerSkillsEnum skill);
    double GetSkill(OfficerSkillsEnum skill);
    void ModifySkill(OfficerSkillsEnum skill, double percModifier);
}
