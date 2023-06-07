using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeSkillManager.Data.Models
{
    public class Admin
    {
        [ForeignKey("User")]
        public string Id { get; set; }
        public User User { get; set; }
        public string Gender { get; set; }
        public bool IsActive { get; set; }
    }
}
