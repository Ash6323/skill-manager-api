using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EmployeeSkillManager.Services.Interfaces
{
    public interface IAuthService
    {
        Task<JwtSecurityToken> GetToken(List<Claim> authClaim);
        Task<string> RegisterAdmin(UserRegistrationDTO inputModel);
        Task<AuthBody> LoginAuth(UserLoginDTO loginModel);
        Task<string> RegisterEmployee(UserRegistrationDTO inputModel);
        public Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
        public Task<string> GenerateRefreshToken();
    }
}
