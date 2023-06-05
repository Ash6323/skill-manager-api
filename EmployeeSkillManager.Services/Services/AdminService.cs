﻿using EmployeeSkillManager.Data.Context;
using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Mappers;
using EmployeeSkillManager.Data.Models;
using EmployeeSkillManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSkillManager.Services.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _dbContext;
        public AdminService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<UserDTO> GetAdmins()
        {
            List<Admin> admins = _dbContext.Admins.Include(a => a.User).Where(e => e.IsActive.Equals(1)).ToList();
            return admins.Select(a => new AdminUserMapper().Map(a)).ToList();
        }
        public UserDTO GetAdmin(string id)
        {
            Admin result = _dbContext.Admins.Include(e => e.User).FirstOrDefault(e => e.Id.Equals(id) && e.IsActive.Equals(1))!;
            if (result != null)
            {
                UserDTO admin = new AdminUserMapper().Map(result);
                return admin;
            }
            return null;
        }
        
        public string UpdateAdmin(string id, AdminUpdateDTO updatedAdmin)
        {
            Admin admin = _dbContext.Admins.Include(e => e.User).FirstOrDefault(e => e.Id.Equals(id))!;

            if (admin != null)
            {
                if (string.IsNullOrEmpty(updatedAdmin.UserName))
                    updatedAdmin.UserName = admin.User.UserName;

                if (string.IsNullOrEmpty(updatedAdmin.FirstName))
                    updatedAdmin.FirstName = admin.User.FirstName;

                if (string.IsNullOrEmpty(updatedAdmin.LastName))
                    updatedAdmin.LastName = admin.User.LastName;

                if (string.IsNullOrEmpty(updatedAdmin.Email))
                    updatedAdmin.Email = admin.User.Email;

                if (string.IsNullOrEmpty(updatedAdmin.PhoneNumber))
                    updatedAdmin.PhoneNumber = admin.User.PhoneNumber;

                if (string.IsNullOrEmpty(updatedAdmin.ProfilePictureUrl))
                    updatedAdmin.ProfilePictureUrl = null;

                if (string.IsNullOrEmpty(updatedAdmin.Gender))
                    updatedAdmin.Gender = admin.Gender;

                if (string.IsNullOrEmpty(updatedAdmin.Street))
                    updatedAdmin.Street= admin.User.Street;

                if (string.IsNullOrEmpty(updatedAdmin.Town))
                    updatedAdmin.Town = admin.User.Town;

                if (string.IsNullOrEmpty(updatedAdmin.City))
                    updatedAdmin.City= admin.User.City;

                if (string.IsNullOrEmpty(updatedAdmin.Zipcode))
                    updatedAdmin.Zipcode = admin.User.Zipcode;

                if (updatedAdmin.DateOfBirth == null)
                    updatedAdmin.DateOfBirth = admin.User.DateOfBirth;

                admin.User.UserName = updatedAdmin.UserName;
                admin.User.FirstName = updatedAdmin.FirstName;
                admin.User.LastName = updatedAdmin.LastName;
                admin.User.Email = updatedAdmin.Email;
                admin.User.PhoneNumber = updatedAdmin.PhoneNumber;
                admin.User.ProfilePictureUrl = updatedAdmin.ProfilePictureUrl;
                admin.Gender = updatedAdmin.Gender;
                admin.User.Street = updatedAdmin.Street;
                admin.User.Town = updatedAdmin.Town;
                admin.User.City = updatedAdmin.City;
                admin.User.Zipcode = updatedAdmin.Zipcode;
                admin.User.DateOfBirth = updatedAdmin.DateOfBirth;
                //admin.User.PreviousOrganisation = updatedAdmin.PreviousOrganisation;
                //admin.User.PreviousDesignation = updatedAdmin.PreviousDesignation;
                admin.User.UpdatedAt = DateTime.UtcNow;
                _dbContext.SaveChanges();
                return admin.Id;
            }
            else
                return "0";
        }
        public string DeleteAdmin(string id)
        {
            Admin admin = _dbContext.Admins.Include(a => a.User)
                                                    .FirstOrDefault(a => a.Id.Equals(id) && a.IsActive.Equals(1))!;

            if (admin != null)
            {
                admin.IsActive = 0;
                _dbContext.SaveChanges();
                return admin.User.Id;
            }
            else
                return "0";
        }
    }
}
