﻿using System.ComponentModel.DataAnnotations;

namespace EmployeeSkillManager.Data.Models
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string SkillName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int IsActive { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<EmployeeSkill> EmployeeSkills { get; set; }
    }
}
