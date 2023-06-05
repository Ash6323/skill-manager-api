using EmployeeSkillManager.Data.Context;
using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Models;
using EmployeeSkillManager.Services.Interfaces;

namespace EmployeeSkillManager.Services.Services
{
    public class EmployeeSkillService : IEmployeeSkillService
    {
        private ApplicationDbContext _context;
        public EmployeeSkillService(ApplicationDbContext newContext)
        {
            _context = newContext;
        }
        public List<EmployeeSkillDTO> GetAllEmployeeSkills()
        {
            List<EmployeeSkillDTO> employeeSkills = (from e in _context.Employees
                                                     where e.IsActive.Equals(1)
                                                     select new EmployeeSkillDTO
                                                     {
                                                         EmployeeId = e.Id,
                                                         EmployeeName = e.User.FullName,
                                                         EmployeeSkills =
                                                            e.EmployeeSkills.Select(es => new SkillExpertiseDTO()
                                                            {
                                                                Id = es.Skill.Id,
                                                                SkillName = es.Skill.SkillName,
                                                                Expertise = es.Expertise
                                                            }).ToList()
                                                     }).ToList();

            return employeeSkills;
        }
        public EmployeeSkillDTO GetEmployeeSkills(string id)
        {
            EmployeeSkillDTO employeeSkills = (from e in _context.Employees 
                                      where e.IsActive.Equals(1)
                                      select new EmployeeSkillDTO
                                      {
                                          EmployeeId = e.Id,
                                          EmployeeName = e.User.FullName,
                                          EmployeeSkills =
                                            e.EmployeeSkills.Select(es => new SkillExpertiseDTO()
                                            {
                                                Id = es.Skill.Id,
                                                SkillName = es.Skill.SkillName,
                                                Expertise = es.Expertise
                                            }).ToList()
                                      }).FirstOrDefault(e => e.EmployeeId.Equals(id))!;

            return employeeSkills;
        }
        public int AddEmployeeSkill(EmployeeAddSkillDTO employeeSkill)
            {
            if(employeeSkill.SkillId.Equals(0) || employeeSkill.EmployeeId.Equals("") || employeeSkill.Expertise.Equals("")) 
            {
                return 1;
            }

            EmployeeSkill duplicateSkill = _context.EmployeeSkills
                                        .FirstOrDefault(x => x.EmployeeId
                                        .Equals(employeeSkill.EmployeeId) && x.SkillId.Equals(employeeSkill.SkillId))!;
            if(duplicateSkill != null)
            {
                return 0;
            }

            EmployeeSkill newEmployeeSkill = new EmployeeSkill();
            {
                newEmployeeSkill.EmployeeId = employeeSkill.EmployeeId;
                newEmployeeSkill.SkillId = employeeSkill.SkillId;
                newEmployeeSkill.Expertise = employeeSkill.Expertise;
                newEmployeeSkill.AddedAt = DateTime.UtcNow;
            };
            _context.EmployeeSkills.Add(newEmployeeSkill);
            _context.SaveChanges();
            return newEmployeeSkill.SkillId;
        }
        public int UpdateEmployeeSkill(EmployeeSkillUpdateDTO employeeSkill)
        {
            EmployeeSkill skill = _context.EmployeeSkills
                                        .FirstOrDefault(x => x.EmployeeId.Equals(employeeSkill.EmployeeId) 
                                                        && x.SkillId.Equals(employeeSkill.SkillId))!;
            if(skill == null)
            {
                return 0;
            }

            skill.Expertise = employeeSkill.Expertise;
            _context.SaveChanges();
            return skill.SkillId;
        }
    }
}
