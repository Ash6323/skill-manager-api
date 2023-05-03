﻿using EmployeeSkillManager.Data.Context;
using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Enums;
using EmployeeSkillManager.Data.Mappers;
using EmployeeSkillManager.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSkillManager.Services.Interfaces
{
    public interface IEmployeeService
    {
        List<EmployeeDTO> GetEmployees();
        EmployeeDTO GetEmployee(string id);
        string UpdateEmployee(string id, EmployeeUpdateDTO updatedEmployee);
        string DeleteEmployee(string id);
        List<string> GetGenderEnum();
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _dbContext;
        public EmployeeService(ApplicationDbContext dbContext)
        {
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

                if (string.IsNullOrEmpty(updatedEmployee.Address))
                    updatedEmployee.Address = employee.User.Address;

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
                employee.User.Address = updatedEmployee.Address;
                employee.User.Zipcode = updatedEmployee.Zipcode;
                employee.User.DateOfBirth = updatedEmployee.DateOfBirth;
                //employee.User.PreviousOrganisation = updatedEmployee.PreviousOrganisation;
                //employee.User.PreviousDesignation = updatedEmployee.PreviousDesignation;
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
