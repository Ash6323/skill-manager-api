using EmployeeSkillManager.Data.Context;
using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Enums;
using EmployeeSkillManager.Data.Models;
using EmployeeSkillManager.Services.Interfaces;

namespace EmployeeSkillManager.Services.Services
{
    public class SkillService : ISkillService
    {
        private ApplicationDbContext _context;
        public SkillService(ApplicationDbContext newContext)
        {
            _context = newContext;
        }
        public List<SkillDTO> GetSkills()
        {
            IQueryable<SkillDTO> skills = from s in _context.Skills where s.IsActive.Equals(true)
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
                              where s.Id == id && s.IsActive.Equals(true)
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
                newSkill.IsActive = true;
            };
            _context.Skills.Add(newSkill);
            _context.SaveChanges();
            return newSkill.Id;
        }
        public int UpdateSkill(int id, SkillDTO updatedSkill)
        {
            string duplicateSkill = _context.Skills.Select(s => s.SkillName).Where(s => s.Equals(updatedSkill.SkillName)).ToString()!;
            if (string.IsNullOrEmpty(duplicateSkill))
                return 1;

            Skill skill = _context.Skills.FirstOrDefault(s => s.Id.Equals(id) && s.IsActive.Equals(true))!;

            if (skill != null)
            {
                if (string.IsNullOrEmpty(updatedSkill.SkillName))
                    updatedSkill.SkillName = skill.SkillName;
                if (string.IsNullOrEmpty(updatedSkill.Description))
                    updatedSkill.Description = skill.Description;

                skill.SkillName = updatedSkill.SkillName;
                skill.Description = updatedSkill.Description;
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
                skill.IsActive = false;
                _context.SaveChanges();
                return skill.Id;
            }
            else
                return 0;
        }
        public List<string> GetExpertiseEnum()
        {
            //List<int> expertises = Enum.GetValues(typeof(Expertise)).Cast<int>().ToList();
            List<string> expertises = Enum.GetNames(typeof(Expertise)).ToList();
            return expertises;
        }
    }
}
