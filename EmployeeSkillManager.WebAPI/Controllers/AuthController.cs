using EmployeeSkillManager.Data.Models;
using EmployeeSkillManager.Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using EmployeeSkillManager.Services.Interfaces;
using EmployeeSkillManager.Data.Constants;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EmployeeSkillManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("AdminRegistration")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminRegistration([FromBody] UserRegistrationDTO inputModel)
        {
            try
            {
                string result = await _authService.RegisterAdmin(inputModel);

                if (result.Equals("0"))
                {
                    Response userExistsResponse = new Response
                        (StatusCodes.Status400BadRequest, ConstantMessages.UserAlreadyExists, ConstantMessages.UserAlreadyExists);
                    return BadRequest(userExistsResponse);
                }
                else if (result.Equals("-1"))
                {
                    Response failedResponse = new Response
                        (StatusCodes.Status400BadRequest, ConstantMessages.UserCreationFailed, ConstantMessages.UserCreationFailed);
                    return BadRequest(failedResponse);
                }
                else
                {
                    Response response = new Response(StatusCodes.Status200OK, ConstantMessages.UserCreated, result);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                Response response = new Response
                    (StatusCodes.Status500InternalServerError, ConstantMessages.ErrorOccurred, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        [HttpPost]
        [Route("EmployeeRegistration")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EmployeeRegistration([FromBody] UserRegistrationDTO inputModel)
        {
            try
            {
                string result = await _authService.RegisterEmployee(inputModel);

                if (result.Equals("0"))
                {
                    Response userExistsResponse = new Response
                        (StatusCodes.Status400BadRequest, ConstantMessages.UserAlreadyExists, ConstantMessages.UserAlreadyExists);
                    return BadRequest(userExistsResponse);
                }
                else if (result.Equals("-1"))
                {
                    Response failedResponse = new Response
                        (StatusCodes.Status400BadRequest, ConstantMessages.UserCreationFailed, ConstantMessages.UserCreationFailed);
                    return BadRequest(failedResponse);
                }
                else
                {
                    Response response = new Response(StatusCodes.Status200OK, ConstantMessages.UserCreated, result);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                Response response = new Response
                    (StatusCodes.Status500InternalServerError, ConstantMessages.ErrorOccurred, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO loginModel)
        {
            try
            {
                AuthBody authBody = await _authService.LoginAuth(loginModel);
                if (authBody != null)
                {
                    return Ok(authBody);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                Response response = new Response
                    (StatusCodes.Status500InternalServerError, ConstantMessages.ErrorOccurred, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenDTO tokenModel)
        {
            try
            {
                if (tokenModel is null)
                {
                    return BadRequest(new Response
                            (StatusCodes.Status400BadRequest, ConstantMessages.InvalidRequest, ConstantMessages.InvalidRequest));
                }

                string accessToken = tokenModel.AccessToken;
                ClaimsPrincipal principal = await _authService.GetPrincipalFromExpiredToken(accessToken);
                if (principal == null)
                {
                    return BadRequest(new Response
                            (StatusCodes.Status400BadRequest, ConstantMessages.InvalidToken, ConstantMessages.InvalidToken));
                }

                JwtSecurityToken newAccessToken = await _authService.GetToken(principal.Claims.ToList());
                string newRefreshToken = await _authService.GenerateRefreshToken();
                RefreshTokenDTO tokens = new()
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                    RefreshToken = newRefreshToken
                };
                return Ok(new Response(StatusCodes.Status200OK, ConstantMessages.TokenGenerated, tokens));
            }
            catch (Exception ex)
            {
                Response response = new Response
                    (StatusCodes.Status500InternalServerError, ConstantMessages.ErrorOccurred, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
