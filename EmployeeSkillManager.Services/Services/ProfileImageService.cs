using EmployeeSkillManager.Data.Context;
using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Models;
using EmployeeSkillManager.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSkillManager.Services.Services
{
    public class ProfileImageService : IProfileImageService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public ProfileImageService(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public async Task<int> UploadImage([FromForm] ProfileImageUploadDTO imageEntity)
        {
            if (imageEntity.Image.FileName == null || imageEntity.Image.FileName.Length == 0)
            {
                return 0;
            }

            string randomFileName = Guid.NewGuid().ToString();
            string rootPath = Path.Combine(_environment.WebRootPath,"Images", randomFileName + imageEntity.Image.FileName);
            string databaseImagePath = "\\Images\\" + randomFileName + imageEntity.Image.FileName;

            using (FileStream stream = new FileStream(rootPath, FileMode.Create))
            {
                await imageEntity.Image.CopyToAsync(stream);
                stream.Close();
            }

            ProfileImage imageData = _context.ProfileImages.FirstOrDefault(e => e.UserId.Equals(imageEntity.UserId))!;
            if(imageData == null)
            {
                ProfileImage userProfileModel = new ProfileImage
                {
                    UserId = imageEntity.UserId,
                    ImagePath = databaseImagePath
                };
                _context.Add(userProfileModel);
            }
            else
            {
                File.Delete(_environment.WebRootPath + imageData.ImagePath);
                imageData.ImagePath = databaseImagePath;
            }
            _context.SaveChanges();
            return 1;
        }
        public string GetImage(string userId)
        {
            string imagePath = _context.ProfileImages.FirstOrDefault(e => e.UserId.Equals(userId))!.ImagePath!;
            if (string.IsNullOrEmpty(imagePath))
            {
                return null!;
            }
            else
                return imagePath;
        }
        public int DeleteImage(string userId)
        {
            ProfileImage imageData = _context.ProfileImages.FirstOrDefault(e => e.UserId.Equals(userId))!;
            if (imageData != null)
            {
                File.Delete(_environment.WebRootPath + imageData.ImagePath);
                _context.Remove(imageData);
                _context.SaveChanges();
            }
            return 0;
        }
    }
}
