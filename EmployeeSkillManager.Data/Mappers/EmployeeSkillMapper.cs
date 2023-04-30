using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Models;

namespace EmployeeSkillManager.Data.Mappers
{
    public class EmployeeSkillMapper
    {
        public SkillDTO Map(EmployeeSkill entity)
        {
            return new SkillDTO
            {
                Id = entity.SkillId,
                SkillName = entity.Skill.SkillName,
                Expertise = entity.Expertise,
            };
        }
    }
}
