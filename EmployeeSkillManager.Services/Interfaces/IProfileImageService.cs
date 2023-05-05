using EmployeeSkillManager.Data.Context;
using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Helpers;
using EmployeeSkillManager.Data.Models;
using Microsoft.AspNetCore.Hosting;
using System;

namespace EmployeeSkillManager.Services.Interfaces
{
    public interface IProfileImageService
    {
        Task<int> UploadImageAsync(ProfileImageUploadDTO imageEntity);
        Task<int> SaveImageAsync(ProfileImageUploadDTO saveImageEntity);
    }
    public class ProfileImageService : IProfileImageService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public ProfileImageService(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public async Task<int> UploadImageAsync(ProfileImageUploadDTO imageEntity)
        {
            ProfileImage post = new ProfileImage
            {  
                UserId = imageEntity.UserId,
                ImagePath = imageEntity.ImagePath
            };

            var postEntry = await _context.ProfileImages.AddAsync(post);
            var saveResponse = await _context.SaveChangesAsync();

            if (saveResponse < 0)
            {
                return 0;
            }
            var postEntity = postEntry.Entity;
            ProfileImageDTO postModel = new ProfileImageDTO
            {
                UserId = postEntity.UserId,
                ImagePath = Path.Combine(postEntity.ImagePath)
            };
            return 1;
        }
        public async Task<int> SaveImageAsync(ProfileImageUploadDTO postRequest)
        {
            string uniqueFileName = ProfileImageHelper.GetUniqueFileName(postRequest.Image.FileName);
            string uploads = Path.Combine(_environment.WebRootPath, "users", "images", postRequest.UserId.ToString());
            string filePath = Path.Combine(uploads, uniqueFileName);
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            await postRequest.Image.CopyToAsync(new FileStream(filePath, FileMode.Create));
            postRequest.ImagePath = filePath;
            return 0;
        }
    }
}
