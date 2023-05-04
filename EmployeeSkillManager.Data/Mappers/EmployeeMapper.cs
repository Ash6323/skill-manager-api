using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Models;

namespace EmployeeSkillManager.Data.Mappers
{
    public class EmployeeMapper
    {
        public EmployeeDTO Map(Employee entity)
        {
            return new EmployeeDTO
            {
                Id = entity.Id,
                UserName = entity.User.UserName,
                FullName = entity.User.FullName,
                Gender = entity.Gender,
                PhoneNumber = entity.User.PhoneNumber,
                Email = entity.User.Email,
                IsActive = entity.IsActive,
                DateOfBirth = entity.User.DateOfBirth,
                Street = entity.User.Street,
                Town = entity.User.Town,
                City = entity.User.City,
                Zipcode = entity.User.Zipcode,
                PreviousOrganisation = entity.User.PreviousOrganisation,
                PreviousDesignation = entity.User.PreviousDesignation,
            };
        }
    }
}
