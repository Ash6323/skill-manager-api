using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Models;

namespace EmployeeSkillManager.Data.Mappers
{
    public class EmployeeSkillMapper
    {
        public SkillExpertiseDTO Map(EmployeeSkill entity)
        {
            return new SkillExpertiseDTO
            {
                Id = entity.SkillId,
                SkillName = entity.Skill.SkillName,
                Expertise = entity.Expertise,
            };
        }
    }
}
