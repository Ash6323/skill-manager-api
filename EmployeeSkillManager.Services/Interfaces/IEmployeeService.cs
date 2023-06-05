using EmployeeSkillManager.Data.DTOs;

namespace EmployeeSkillManager.Services.Interfaces
{
    public interface IEmployeeService
    {
        List<UserDTO> GetEmployees();
        UserDTO GetEmployee(string id);
        string UpdateEmployee(string id, EmployeeUpdateDTO updatedEmployee);
        string DeleteEmployee(string id);
        List<string> GetGenderEnum();
    }
}
