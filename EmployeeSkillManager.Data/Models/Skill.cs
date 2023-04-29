using System.ComponentModel.DataAnnotations;

namespace EmployeeSkillManager.Data.Models
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string SkillName { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<EmployeeSkill> EmployeeSkills { get; }
    }
}
