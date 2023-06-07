using EmployeeSkillManager.Data.Context;
using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Enums;
using EmployeeSkillManager.Data.Mappers;
using EmployeeSkillManager.Data.Models;
using EmployeeSkillManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSkillManager.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _dbContext;
        public EmployeeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<UserDTO> GetEmployees()
        {
            List<Employee> employees = _dbContext.Employees.Include(e => e.User).Where(e=> e.IsActive.Equals(true)).ToList();
            return employees.Select(e => new EmployeeUserMapper().Map(e)).ToList();
        }
        public UserDTO GetEmployee(string id)
        {
            Employee result = _dbContext.Employees.Include(e => e.User)
                                                  .FirstOrDefault(e => e.Id.Equals(id) && e.IsActive.Equals(true))!;
            if (result != null)
            {
                UserDTO employee = new EmployeeUserMapper().Map(result);
                return employee;
            }
            return null;
        }
        public string UpdateEmployee(string id, EmployeeUpdateDTO updatedEmployee)
        {
            Employee employee = _dbContext.Employees.Include(e => e.User)
                                                    .FirstOrDefault(e => e.Id.Equals(id) && e.IsActive.Equals(true))!;

            if (employee != null)
            {
                if(string.IsNullOrEmpty(updatedEmployee.FirstName))
                    updatedEmployee.FirstName = employee.User.FirstName;

                if(string.IsNullOrEmpty(updatedEmployee.LastName))
                    updatedEmployee.LastName = employee.User.LastName;

                if (string.IsNullOrEmpty(updatedEmployee.Email))
                    updatedEmployee.Email = employee.User.Email;

                if (string.IsNullOrEmpty(updatedEmployee.PhoneNumber))
                    updatedEmployee.PhoneNumber = employee.User.PhoneNumber;

                if (string.IsNullOrEmpty(updatedEmployee.ProfilePictureUrl))
                    updatedEmployee.ProfilePictureUrl = null;

                if (string.IsNullOrEmpty(updatedEmployee.Gender))
                    updatedEmployee.Gender = employee.Gender;

                if (string.IsNullOrEmpty(updatedEmployee.Street))
                    updatedEmployee.Street = employee.User.Street;

                if (string.IsNullOrEmpty(updatedEmployee.Town))
                    updatedEmployee.Town = employee.User.Town;

                if (string.IsNullOrEmpty(updatedEmployee.City))
                    updatedEmployee.City = employee.User.City;

                if (string.IsNullOrEmpty(updatedEmployee.Zipcode))
                    updatedEmployee.Zipcode = employee.User.Zipcode;

                if (updatedEmployee.DateOfBirth == null)
                    updatedEmployee.DateOfBirth = employee.User.DateOfBirth;

                employee.User.FirstName = updatedEmployee.FirstName;
                employee.User.LastName = updatedEmployee.LastName;
                employee.User.Email = updatedEmployee.Email;
                employee.User.PhoneNumber = updatedEmployee.PhoneNumber;
                employee.User.ProfilePictureUrl = updatedEmployee.ProfilePictureUrl;
                employee.Gender = updatedEmployee.Gender;
                employee.User.Street = updatedEmployee.Street;
                employee.User.Town = updatedEmployee.Town;
                employee.User.City = updatedEmployee.City;
                employee.User.Zipcode = updatedEmployee.Zipcode;
                employee.User.DateOfBirth = updatedEmployee.DateOfBirth;
                employee.User.UpdatedAt = DateTime.UtcNow;
                _dbContext.SaveChanges();
                return employee.Id;
            }
            else
                return "0";
        }
        public string DeleteEmployee(string id)
        {
            Employee employee = _dbContext.Employees.Include(e => e.User)
                                                    .FirstOrDefault(e => e.Id.Equals(id) && e.IsActive.Equals(true))!;
            
            if (employee != null)
            {
                employee.IsActive = false;
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
