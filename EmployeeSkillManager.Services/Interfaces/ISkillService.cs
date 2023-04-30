using EmployeeSkillManager.Data.Context;
using EmployeeSkillManager.Data.DTOs;
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
            IQueryable<SkillDTO> skills = from s in _context.Skills where s.isActive.Equals(1)
                                        select new SkillDTO()
                                        {
                                            Id = s.Id,
                                            SkillName = s.SkillName,
                                        };
            return skills.ToList();
        }
        public SkillDTO GetSkill(int id)
        {
            SkillDTO skill = (from s in _context.Skills
                              where s.Id == id && s.isActive.Equals(1)
                              select new SkillDTO()
                              {
                                  Id = s.Id,
                                  SkillName = s.SkillName,
                              }).FirstOrDefault();
            return skill;
        }
        public int AddSkill(SkillDTO skill)
        {
            Skill newSkill = new Skill();
            {
                newSkill.SkillName = skill.SkillName;
                newSkill.CreatedAt = DateTime.Now;
                newSkill.UpdatedAt = DateTime.Now;
                newSkill.isActive = 1;
            };
            _context.Skills.Add(newSkill);
            _context.SaveChanges();
            return newSkill.Id;
        }
        public int UpdateSkill(int id, SkillDTO updatedSkill)
        {
            Skill skill = _context.Skills.FirstOrDefault(s => s.Id.Equals(id));

            if (skill != null)
            {
                if (skill.isActive.Equals(0))
                    return 0;

                if (string.IsNullOrEmpty(updatedSkill.SkillName))
                    updatedSkill.SkillName = skill.SkillName;

                skill.SkillName = updatedSkill.SkillName;
                skill.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
                return skill.Id;
            }
            else
                return 0;
        }
        public int DeleteSkill(int id)
        {
            Skill skill = _context.Skills.FirstOrDefault(s => s.Id.Equals(id));
            if (skill != null)
            {
                skill.isActive = 0;
                _context.SaveChanges();
                return skill.Id;
            }
            else
                return 0;
        }
    }
}
