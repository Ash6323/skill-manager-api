using EmployeeSkillManager.Data.DTOs;

namespace EmployeeSkillManager.Services.Interfaces
{
    public interface ISkillService
    {
        List<SkillDTO> GetSkills();
        SkillDTO GetSkill(int id);
        int AddSkill(SkillDTO skill);
        int UpdateSkill(int id, SkillDTO updatedSkill);
        int DeleteSkill(int id);
        List<string> GetExpertiseEnum();
    }
}
