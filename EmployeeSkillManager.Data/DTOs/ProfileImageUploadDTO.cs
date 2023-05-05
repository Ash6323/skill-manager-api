using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeSkillManager.Data.DTOs
{
    public class ProfileImageUploadDTO
    {
        public string UserId { get; set; }

        public IFormFile Image { get; set; }
    }
}
