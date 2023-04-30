using EmployeeSkillManager.Data.Constants;
using EmployeeSkillManager.Data.Context;
using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Mappers;
using EmployeeSkillManager.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EmployeeSkillManager.Services.Interfaces
{
    public interface IAdminService
    {
        Task<string> RegisterAdmin(UserRegistrationDTO inputModel);
        List<AdminDTO> GetAdmins();
        AdminDTO GetAdmin(string id);
    }
    public class AdminService : IAdminService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public AdminService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, 
                            IConfiguration configuration, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public List<AdminDTO> GetAdmins()
        {
            List<Admin> admins = _dbContext.Admins.Include(a => a.User).ToList();
            return admins.Select(a => new AdminMapper().Map(a)).ToList();
        }
        public AdminDTO GetAdmin(string id)
        {
            Admin result = _dbContext.Admins.Include(e => e.User).FirstOrDefault(e => e.Id.Equals(id));
            if (result != null)
            {
                AdminDTO admin = new AdminMapper().Map(result);
                return admin;
            }
            return null;
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

            try
            {
                Admin newAdmin = new Admin
                {
                    Id = user.Id,
                    Gender = inputModel.Gender,
                    IsActive = 1,
                };
                _dbContext.Admins.Add(newAdmin);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return user.Id;
        }
    }
}
