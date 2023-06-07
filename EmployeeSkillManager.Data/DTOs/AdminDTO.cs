namespace EmployeeSkillManager.Data.DTOs
{
    public class AdminDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PreviousOrganisation { get; set; }
        public string PreviousDesignation { get; set; }
    }
}
