using EmployeeSkillManager.Data.Context;
using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Mappers;
using EmployeeSkillManager.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EmployeeSkillManager.Services.Interfaces
{
    public interface IEmployeeSkillService
    {
        List<EmployeeSkillDTO> GetAllEmployeeSkills();
        EmployeeSkillDTO GetEmployeeSkills(string id);
        int AddEmployeeSkill(EmployeeAddSkillDTO employeeSkill);
    }
    public class EmployeeSkillService : IEmployeeSkillService
    {
        private ApplicationDbContext _context;
        public EmployeeSkillService(ApplicationDbContext newContext)
        {
            _context = newContext;
        }
        public List<EmployeeSkillDTO> GetAllEmployeeSkills()
        {
            List<EmployeeSkill> employeeSkills = _context.EmployeeSkills.Include(e => e.Skill).ToList();
            List<SkillExpertiseDTO> skills = employeeSkills.Select(s => new EmployeeSkillMapper().Map(s)).ToList();

            List<EmployeeSkillDTO> allEmployeeSkills = (from e in _context.EmployeeSkills
                                          where e.Employee.IsActive.Equals(1) && e.Skill.isActive.Equals(1)
                                          select new EmployeeSkillDTO()
                                          {
                                              EmployeeId = e.Employee.Id,
                                              EmployeeName = e.Employee.User.FullName,
                                              EmployeeSkills = skills,
                                          }).Distinct().ToList();
            return allEmployeeSkills;
        }
        public EmployeeSkillDTO GetEmployeeSkills(string id)
        {
            List<EmployeeSkill> employeeSkills = _context.EmployeeSkills.Include(e => e.Skill).ToList();
            List<SkillExpertiseDTO> skills = employeeSkills.Select(s => new EmployeeSkillMapper().Map(s)).ToList();

            EmployeeSkillDTO employeeWithSkills = (from e in _context.EmployeeSkills
                                                        where e.Employee.IsActive.Equals(1) && e.Skill.isActive.Equals(1)
                                                        select new EmployeeSkillDTO()
                                                        {
                                                            EmployeeId = e.Employee.Id,
                                                            EmployeeName = e.Employee.User.FullName,
                                                            EmployeeSkills = skills,
                                                        }).Distinct().FirstOrDefault(e => e.EmployeeId.Equals(id));
            return employeeWithSkills;
        }
        public int AddEmployeeSkill(EmployeeAddSkillDTO employeeSkill)
        {
            EmployeeSkill duplicateSkill = _context.EmployeeSkills
                .FirstOrDefault(x => x.EmployeeId.Equals(employeeSkill.EmployeeId) && x.SkillId.Equals(employeeSkill.SkillId));

            if(duplicateSkill != null)
            {
                return 0;
            }

            EmployeeSkill newEmployeeSkill = new EmployeeSkill();
            {
                newEmployeeSkill.EmployeeId = employeeSkill.EmployeeId;
                newEmployeeSkill.SkillId = employeeSkill.SkillId;
                //newEmployeeSkill.Skill.SkillName = _context.Skills
                //                                   .Select(x => x.SkillName)
                //                                   .Where(s => s.Id.Equals(employeeSkill.EmployeeSkill.Id));
                newEmployeeSkill.Expertise = employeeSkill.Expertise;
                newEmployeeSkill.AddedAt = DateTime.Now;
            };
            _context.EmployeeSkills.Add(newEmployeeSkill);
            _context.SaveChanges();
            return 1;
        }
    }
}
