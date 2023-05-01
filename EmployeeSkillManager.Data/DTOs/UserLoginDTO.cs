using System.ComponentModel.DataAnnotations;

namespace EmployeeSkillManager.Data.DTOs
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
