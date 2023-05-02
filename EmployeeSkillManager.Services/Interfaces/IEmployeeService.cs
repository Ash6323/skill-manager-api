using EmployeeSkillManager.Data.Constants;
using EmployeeSkillManager.Data.Context;
using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Enums;
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
        string UpdateEmployee(string id, EmployeeUpdateDTO updatedEmployee);
        string DeleteEmployee(string id);
        List<string> GetGenderEnum();
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

        public List<EmployeeDTO> GetEmployees()
        {
            List<Employee> employees = _dbContext.Employees.Include(e => e.User).Where(e=> e.IsActive.Equals(1)).ToList();
            return employees.Select(e => new EmployeeMapper().Map(e)).ToList();
        }
        public EmployeeDTO GetEmployee(string id)
        {
            Employee result = _dbContext.Employees.Include(e => e.User)
                                                  .FirstOrDefault(e => e.Id.Equals(id) && e.IsActive.Equals(1));
            if (result != null)
            {
                EmployeeDTO employee = new EmployeeMapper().Map(result);
                return employee;
            }
            return null;
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
                UpdatedAt = DateTime.Now
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
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return user.Id;
        }
        public string UpdateEmployee(string id, EmployeeUpdateDTO updatedEmployee)
        {
            Employee employee = _dbContext.Employees.Include(e => e.User)
                                                    .FirstOrDefault(e => e.Id.Equals(id) && e.IsActive.Equals(1));

            if (employee != null)
            {
                if(string.IsNullOrEmpty(updatedEmployee.FirstName))
                    updatedEmployee.FirstName = employee.User.FirstName;

                if(string.IsNullOrEmpty(updatedEmployee.LastName))
                    updatedEmployee.LastName = employee.User.LastName;

                if (string.IsNullOrEmpty(updatedEmployee.Email))
                    updatedEmployee.Email = employee.User.Email;

                if (string.IsNullOrEmpty(updatedEmployee.PhoneNumber))
                    updatedEmployee.Email = employee.User.PhoneNumber;

                if (string.IsNullOrEmpty(updatedEmployee.ProfilePictureUrl))
                    updatedEmployee.ProfilePictureUrl = null;

                if (string.IsNullOrEmpty(updatedEmployee.Gender))
                    updatedEmployee.ProfilePictureUrl = employee.Gender;

                employee.User.FirstName = updatedEmployee.FirstName;
                employee.User.LastName = updatedEmployee.LastName;
                employee.User.Email = updatedEmployee.Email;
                employee.User.PhoneNumber = updatedEmployee.PhoneNumber;
                employee.User.ProfilePictureUrl = updatedEmployee.ProfilePictureUrl;
                employee.Gender = updatedEmployee.Gender;
                employee.User.UpdatedAt = DateTime.Now;
                _dbContext.SaveChanges();
                return employee.Id;
            }
            else
                return "0";
        }
        public string DeleteEmployee(string id)
        {
            Employee employee = _dbContext.Employees.Include(e => e.User)
                                                    .FirstOrDefault(e => e.Id.Equals(id) && e.IsActive.Equals(1));
            
            if (employee != null)
            {
                employee.IsActive = 0;
                _dbContext.SaveChanges();
                return employee.User.Id;
            }
            else
                return "0";
        }
        public List<string> GetGenderEnum()
        {
            List<string> genders = new List<string>();
            foreach (var data in Enum.GetNames(typeof(Gender)))
            {
                string value = data.ToString();
                genders.Add(value);
            }
            return genders;
        }
    }
}
