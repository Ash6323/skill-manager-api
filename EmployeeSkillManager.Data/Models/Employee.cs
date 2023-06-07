using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeSkillManager.Data.Models
{
    public class Employee
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }
        public User User { get; set; }
        public string Gender { get; set; }
        public string? CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Skill> Skills { get; set; }
        public ICollection<EmployeeSkill> EmployeeSkills { get; }
    }
}
