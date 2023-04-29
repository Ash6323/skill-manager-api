using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeSkillManager.Data.Models
{
    public class Admin
    {
        [ForeignKey("User")]
        public Guid Id { get; set; }
        public User User { get; set; }
    }
}
