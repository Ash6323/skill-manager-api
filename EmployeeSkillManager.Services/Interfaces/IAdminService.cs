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
        List<AdminDTO> GetAdmins();
        AdminDTO GetAdmin(string id);
        string UpdateAdmin(string id, AdminUpdateDTO updatedAdmin);
        string DeleteAdmin(string id);
    }
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _dbContext;
        public AdminService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<AdminDTO> GetAdmins()
        {
            List<Admin> admins = _dbContext.Admins.Include(a => a.User).Where(e => e.IsActive.Equals(1)).ToList();
            return admins.Select(a => new AdminMapper().Map(a)).ToList();
        }
        public AdminDTO GetAdmin(string id)
        {
            Admin result = _dbContext.Admins.Include(e => e.User).FirstOrDefault(e => e.Id.Equals(id) && e.IsActive.Equals(1));
            if (result != null)
            {
                AdminDTO admin = new AdminMapper().Map(result);
                return admin;
            }
            return null;
        }
        
        public string UpdateAdmin(string id, AdminUpdateDTO updatedAdmin)
        {
            Admin admin = _dbContext.Admins.Include(e => e.User).FirstOrDefault(e => e.Id.Equals(id));

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

                if (string.IsNullOrEmpty(updatedAdmin.Address))
                    updatedAdmin.Address = admin.User.Address;

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
                admin.User.Address = updatedAdmin.Address;
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
                                                    .FirstOrDefault(a => a.Id.Equals(id) && a.IsActive.Equals(1));

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
