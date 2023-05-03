using EmployeeSkillManager.Data.Constants;
using EmployeeSkillManager.Data.Context;
using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeSkillManager.Services.Interfaces
{
    public interface IAuthService
    {
        JwtSecurityToken GetToken(List<Claim> authClaim);
        Task<string> RegisterAdmin(UserRegistrationDTO inputModel);
        Task<AuthBody> LoginAuth(UserLoginDTO loginModel);
        Task<string> RegisterEmployee(UserRegistrationDTO inputModel);
    }
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;
        public AuthService(IConfiguration configuration, UserManager<User> userManager, 
                            RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }
        public JwtSecurityToken GetToken(List<Claim> authClaim)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:secret"]));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["JwtConfig:validIssuer"],
                audience: _configuration["JwtConfig:validAudience"],
                expires: DateTime.UtcNow.AddHours(1),
                claims: authClaim,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        public async Task<string> RegisterAdmin(UserRegistrationDTO inputModel)
        {
            User userExists = await _userManager.FindByNameAsync(inputModel.Username);
            if (userExists != null)
            {
                return "0";
            }

            User user = new()
            {
                FirstName = inputModel.FirstName,
                LastName = inputModel.LastName,
                PhoneNumber = inputModel.PhoneNumber,
                Email = inputModel.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = inputModel.Username,
                Address = inputModel.Address,
                Zipcode = inputModel.Zipcode,
                DateOfBirth = inputModel.DateOfBirth,
                PreviousOrganisation = inputModel.PreviousOrganisation,
                PreviousDesignation = inputModel.PreviousDesignation,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            IdentityResult result = await _userManager.CreateAsync(user, inputModel.Password);

            if (!result.Succeeded)
            {
                return "-1";
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            try
            {
                Admin newAdmin = new Admin
                {
                    Id = user.Id,
                    Gender = inputModel.Gender,
                    IsActive = 1
                };
                _dbContext.Admins.Add(newAdmin);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return user.Id;
        }

        public async Task<string> RegisterEmployee(UserRegistrationDTO inputModel)
        {
            User userExists = await _userManager.FindByNameAsync(inputModel.Username);
            if (userExists != null)
            {
                return "0";
            }

            User user = new User()
            {
                FirstName = inputModel.FirstName,
                LastName = inputModel.LastName,
                PhoneNumber = inputModel.PhoneNumber,
                Email = inputModel.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = inputModel.Username,
                Address = inputModel.Address,
                Zipcode = inputModel.Zipcode,
                DateOfBirth = inputModel.DateOfBirth,
                PreviousOrganisation = inputModel.PreviousOrganisation,
                PreviousDesignation = inputModel.PreviousDesignation,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            IdentityResult result = await _userManager.CreateAsync(user, inputModel.Password);

            if (!result.Succeeded)
            {
                return "-1";
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Employee))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Employee));
            }

            if (await _roleManager.RoleExistsAsync(UserRoles.Employee))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Employee);
            }

            try
            {
                Employee newEmployee = new Employee()
                {
                    Id = user.Id,
                    Gender = inputModel.Gender,
                    IsActive = 1,
                };
                _dbContext.Employees.Add(newEmployee);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return user.Id;
        }

        public async Task<AuthBody> LoginAuth(UserLoginDTO loginModel)
        {
            User user = await _userManager.FindByNameAsync(loginModel.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                IList<string> userRoles = await _userManager.GetRolesAsync(user);
                string userRole = userRoles.FirstOrDefault();

                List<Claim> authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, userRole)
                };

                JwtSecurityToken token = GetToken(authClaims);

                return new AuthBody
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo,
                    Role = userRole,
                    UserId = user.Id
                };
            }
            return null;
        }
    }
}
