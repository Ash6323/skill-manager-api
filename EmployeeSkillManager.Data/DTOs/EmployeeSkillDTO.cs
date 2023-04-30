namespace EmployeeSkillManager.Data.DTOs
{
    public class EmployeeSkillDTO
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public List<SkillDTO> EmployeeSkills { get; set; }
    }
}
