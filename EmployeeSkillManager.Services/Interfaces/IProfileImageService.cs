using EmployeeSkillManager.Data.DTOs;

namespace EmployeeSkillManager.Services.Interfaces
{
    public interface IProfileImageService
    {
        Task<int> UploadImage(ProfileImageUploadDTO imageEntity);
        string GetImage(string userId);
        int DeleteImage(string userId);
    }
}
