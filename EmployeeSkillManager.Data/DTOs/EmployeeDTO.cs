namespace EmployeeSkillManager.Data.DTOs
{
    public class EmployeeDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int IsActive { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PreviousOrganisation { get; set; }
        public string PreviousDesignation { get; set; }
    }
}
