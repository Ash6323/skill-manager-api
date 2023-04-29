using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeSkillManager.Data.Models
{
    public class Employee
    {
        [Key]
        [ForeignKey("User")]
        public Guid Id { get; set; }
        public User User { get; set; }
        public string Gender { get; set; }
        public Guid CreatedBy { get; set; }
        public int isActive { get; set; }
        public ICollection<Skill> Skills { get; set; }
        public ICollection<EmployeeSkill> EmployeeSkills { get; }
    }
}
