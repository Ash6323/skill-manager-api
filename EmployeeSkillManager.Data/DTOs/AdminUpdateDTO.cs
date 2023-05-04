namespace EmployeeSkillManager.Data.DTOs
{
    public class AdminUpdateDTO
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public DateTime DateOfBirth { get; set; }
        //public string PreviousOrganisation { get; set; }
        //public string PreviousDesignation { get; set; }
    }
}
