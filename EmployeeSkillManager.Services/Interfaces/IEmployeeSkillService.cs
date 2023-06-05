using EmployeeSkillManager.Data.DTOs;

namespace EmployeeSkillManager.Services.Interfaces
{
    public interface IEmployeeSkillService
    {
        List<EmployeeSkillDTO> GetAllEmployeeSkills();
        EmployeeSkillDTO GetEmployeeSkills(string id);
        int AddEmployeeSkill(EmployeeAddSkillDTO employeeSkill);
        int UpdateEmployeeSkill(EmployeeSkillUpdateDTO employeeSkill);
    }
}
