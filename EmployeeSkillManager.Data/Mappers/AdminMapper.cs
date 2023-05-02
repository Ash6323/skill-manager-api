using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Models;

namespace EmployeeSkillManager.Data.Mappers
{
    public class AdminMapper
    {
        public AdminDTO Map(Admin entity)
        {
            return new AdminDTO
            {
                Id = entity.Id,
                UserName = entity.User.UserName,
                FullName = entity.User.FullName,
                Gender = entity.Gender,
                PhoneNumber = entity.User.PhoneNumber,
                Email = entity.User.Email,
                IsActive = entity.IsActive,
                DateOfBirth = entity.User.DateOfBirth,
                Address = entity.User.Address,
                Zipcode = entity.User.Zipcode,
                PreviousOrganisation = entity.User.PreviousOrganisation,
                PreviousDesignation = entity.User.PreviousDesignation,
            };
        }
    }
}
