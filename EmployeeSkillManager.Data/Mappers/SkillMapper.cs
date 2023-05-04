using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Models;

namespace EmployeeSkillManager.Data.Mappers
{
    public class SkillMapper
    {
        public SkillDTO Map(Skill entity)
        {
            return new SkillDTO
            {
                Id = entity.Id,
                SkillName = entity.SkillName,
                Description = entity.Description,
            };
        }
    }
}
