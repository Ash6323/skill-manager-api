using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeSkillManager.Data.Models
{
    public class EmployeeSkill
    {
        [ForeignKey("User")]
        public string EmployeeId { get; set; }
        [ForeignKey("Skill")]
        public int SkillId { get; set; }
        [Required]
        public string Expertise { get; set; }
        public Employee Employee { get; set; } = null!;
        public Skill Skill { get; set; } = null!;
    }
}
