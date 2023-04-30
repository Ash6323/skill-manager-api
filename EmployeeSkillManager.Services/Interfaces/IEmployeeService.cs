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
    public interface IEmployeeService
    {
        Task<string> RegisterEmployee(UserRegistrationDTO inputModel);
        List<EmployeeDTO> GetEmployees();
        EmployeeDTO GetEmployee(string id);
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public EmployeeService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, 
                                IConfiguration configuration, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public EmployeeDTO GetEmployee(string id)
        {
            Employee result = _dbContext.Employees.Include(e => e.User).FirstOrDefault(e => e.Id.Equals(id));
            if (result != null)
            {
                EmployeeDTO employee = new EmployeeMapper().Map(result);
                return employee;
            }
            return null;
        }
        public List<EmployeeDTO> GetEmployees()
        {
            List<Employee> employees = _dbContext.Employees.Include(c => c.User).ToList();
            return employees.Select(e => new EmployeeMapper().Map(e)).ToList();
        }
        public async Task<string> RegisterEmployee(UserRegistrationDTO inputModel)
        {
            User userExists = await _userManager.FindByNameAsync(inputModel.Username);
            if (userExists != null)
            {
                return "0";
            }

            User user = new User()
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

            if (!await _roleManager.RoleExistsAsync(UserRoles.Employee))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Employee));
            }

            if (await _roleManager.RoleExistsAsync(UserRoles.Employee))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Employee);
            }

            try
            {
                Employee newEmployee = new Employee()
                {
                    Id = user.Id,
                    Gender = inputModel.Gender,
                    IsActive = 1,
                };
                _dbContext.Employees.Add(newEmployee);
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
