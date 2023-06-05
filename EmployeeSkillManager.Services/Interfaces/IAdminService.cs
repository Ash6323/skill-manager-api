using EmployeeSkillManager.Data.DTOs;

namespace EmployeeSkillManager.Services.Interfaces
{
    public interface IAdminService
    {
        List<UserDTO> GetAdmins();
        UserDTO GetAdmin(string id);
        string UpdateAdmin(string id, AdminUpdateDTO updatedAdmin);
        string DeleteAdmin(string id);
    }
}
