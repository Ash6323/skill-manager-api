using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Models;

namespace EmployeeSkillManager.Data.Mappers
{
    public class EmployeeUserMapper
    {
        public UserDTO Map(Employee entity)
        {
            return new UserDTO
            {
                Id = entity.Id,
                UserName = entity.User.UserName,
                FullName = entity.User.FullName,
                Gender = entity.Gender,
                PhoneNumber = entity.User.PhoneNumber,
                Email = entity.User.Email,
                //ProfilePictureUrl = entity.User.ProfilePictureUrl,
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
