using EmployeeSkillManager.Data.Constants;
using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace EmployeeSkillManager.Services.Interfaces
{
    public interface IAdminService
    {
        Task<string> RegisterAdmin(UserRegistrationDTO inputModel);
    }
    public class AdminService : IAdminService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AdminService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<string> RegisterAdmin(UserRegistrationDTO inputModel)
        {
            User userExists = await _userManager.FindByNameAsync(inputModel.Username);
            if (userExists != null)
            {
                return "0";
            }

            User user = new()
            {
                FirstName = inputModel.FirstName,
                LastName = inputModel.LastName,
                PhoneNumber = inputModel.PhoneNumber,
                Email = inputModel.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = inputModel.Username,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            IdentityResult result = await _userManager.CreateAsync(user, inputModel.Password);

            if (!result.Succeeded)
            {
                return "-1";
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return user.Id;
        }
        //public Admin AddManager(ManagerDTO matter)
        //{
        //    try
        //    {
        //        var newManager = new Manager
        //        {
        //            Id = matter.Id,
        //            Bio = matter.Bio,
        //            User = matter.User,
        //        };
        //        _dbContext.Managers.Add(newManager);
        //        _dbContext.SaveChanges();

        //        return newManager;
        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }

        //}
    }
}
