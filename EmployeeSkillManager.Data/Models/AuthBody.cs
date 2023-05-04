namespace EmployeeSkillManager.Data.Models
{
    public class AuthBody
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string Role { get; set; }
        public string UserId { get; set; }
        public string UserFullName { get; set; }
    }
}
