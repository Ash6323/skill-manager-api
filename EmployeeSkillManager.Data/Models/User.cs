using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EmployeeSkillManager.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public string? ProfilePictureUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PreviousOrganisation { get; set; }
        public string PreviousDesignation { get; set; }
    }
}
