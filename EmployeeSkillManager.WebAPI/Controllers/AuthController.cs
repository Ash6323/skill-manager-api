using EmployeeSkillManager.Data.Models;
using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeSkillManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        //private readonly IAdminService _adminService;
        //private readonly IEmployeeService _employeeService;
        public AuthController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration,
                                IAdminService adminService, IEmployeeService employeeService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            //_adminService = adminService;
            //_employeeService = employeeService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO loginModel)
        {
            User user = await _userManager.FindByNameAsync(loginModel.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                IList<string> userRoles = await _userManager.GetRolesAsync(user);
                string userRole = userRoles[0];

                List<Claim> authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (string role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                JwtSecurityToken token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    role = userRole,
                    userId = user.Id
                });
            }
            return Unauthorized();
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:secret"]));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["JwtConfig:validIssuer"],
                audience: _configuration["JwtConfig:validAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
