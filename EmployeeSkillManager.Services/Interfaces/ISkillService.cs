using EmployeeSkillManager.Data.Context;
using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Enums;
using EmployeeSkillManager.Data.Models;

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
    public class SkillService : ISkillService
    {
        private ApplicationDbContext _context;
        public SkillService(ApplicationDbContext newContext)
        {
            _context = newContext;
        }
        public List<SkillDTO> GetSkills()
        {
            IQueryable<SkillDTO> skills = from s in _context.Skills where s.IsActive.Equals(1)
                                        select new SkillDTO()
                                        {
                                            Id = s.Id,
                                            SkillName = s.SkillName,
                                            Description = s.Description
                                        };
            return skills.ToList();
        }
        public SkillDTO GetSkill(int id)
        {
            SkillDTO skill = (from s in _context.Skills
                              where s.Id == id && s.IsActive.Equals(1)
                              select new SkillDTO()
                              {
                                  Id = s.Id,
                                  SkillName = s.SkillName,
                                  Description = s.Description
                              }).FirstOrDefault()!;
            return skill;
        }
        public int AddSkill(SkillDTO skill)
        {
            Skill newSkill = new Skill();
            {
                newSkill.SkillName = skill.SkillName;
                newSkill.Description = skill.Description;
                newSkill.CreatedAt = DateTime.UtcNow;
                newSkill.UpdatedAt = DateTime.UtcNow;
                newSkill.IsActive = 1;
            };
            _context.Skills.Add(newSkill);
            _context.SaveChanges();
            return newSkill.Id;
        }
        public int UpdateSkill(int id, SkillDTO updatedSkill)
        {
            Skill skill = _context.Skills.FirstOrDefault(s => s.Id.Equals(id) && s.IsActive.Equals(1))!;

            if (skill != null)
            {
                if (skill.IsActive.Equals(0))
                    return 0;

                if (string.IsNullOrEmpty(updatedSkill.SkillName))
                    updatedSkill.SkillName = skill.SkillName;
                if (string.IsNullOrEmpty(updatedSkill.Description))
                    updatedSkill.Description = skill.Description;

                skill.SkillName = updatedSkill.SkillName;
                skill.UpdatedAt = DateTime.UtcNow;
                _context.SaveChanges();
                return skill.Id;
            }
            else
                return 0;
        }
        public int DeleteSkill(int id)
        {
            Skill skill = _context.Skills.FirstOrDefault(s => s.Id.Equals(id))!;
            if (skill != null)
            {
                skill.IsActive = 0;
                _context.SaveChanges();
                return skill.Id;
            }
            else
                return 0;
        }
        public List<string> GetExpertiseEnum()
        {
            List<string> expertises = new List<string>();
            foreach (var data in Enum.GetNames(typeof(Expertise)))
            {
                string value = data.ToString();
                expertises.Add(value);
            }
            return expertises;
        }
    }
}
