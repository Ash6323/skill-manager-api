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
        public int Expertise { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime? RemovedAt { get; set; }
        public Employee Employee { get; set; } = null!;
        public Skill Skill { get; set; } = null!;
    }
}
