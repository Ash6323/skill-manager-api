using Microsoft.AspNetCore.Http;

namespace EmployeeSkillManager.Data.DTOs
{
    public class ProfileImageUploadDTO
    {
        public string UserId { get; set; }

        public IFormFile Image { get; set; }
    }
}
